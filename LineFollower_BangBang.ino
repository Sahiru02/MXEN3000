// ---- Line Follower: Safe-Start + Host-tunable Bang-Bang + Dual-sensor turning ----
// Motors remain OFF until GUI sends PidEnable=1. Manual Output1/2 are ignored unless enabled.
// Supports active-low motor interface (0 => full) via SetInvert command,
// and per-bus bit-order reversal for R-2R ladders wired MSB<->LSB.

#include <Arduino.h>

// ---- Serial Protocol (must match GUI) ----
const byte START = 255;
enum Port : byte {
  Input1 = 0,            // query: returns scaled sensor LEFT (0..255)
  Input2 = 1,            // query: returns scaled sensor RIGHT (0..255)
  Output1 = 2,           // command: manual LEFT duty 0..255 (only if pidEnabled)
  Output2 = 3,           // command: manual RIGHT duty 0..255 (only if pidEnabled)
  RequestOutput1 = 4,    // query: returns current left output byte
  RequestOutput2 = 5,    // query: returns current right output byte
  // 6 unused
  PidEnable = 7,         // 0 = stop + safe idle, 1 = enable controller/manual outputs
  // 8,9 unused
  SetKp = 10,            // **Bang-Bang**: Deadband  (0..255)
  SetKi = 11,            // **Bang-Bang**: TurnBoost (0..255)
  SetKd = 12,            // unused
  SetBase = 13,          // byte 0..255 direct base speed
  SetThresh = 14,        // byte 0..255 -> mapped 0..1023 (optional binarization)
  SetInvert = 15         // 0 = active-high, 1 = active-low (invert outputs)
};

// ---- Hardware mapping (edit if your pins differ) ----
const byte DACPIN1[8] = {9, 8, 7, 6, 5, 4, 3, 2};         // LEFT motor DAC bus bit0..bit7 (LSB..MSB)
const byte DACPIN2[8] = {A2, A3, A4, A5, A1, A0, 11, 10}; // RIGHT motor DAC bus bit0..bit7 (LSB..MSB)
// const byte SENSOR_LEFT  = A6;    // original comment
// const byte SENSOR_RIGHT = A7;    // original comment
const byte SENSOR_LEFT  = A7;      // actual pins in your last sketch
const byte SENSOR_RIGHT = A6;

// ---- Wiring correction toggles (runtime) ----
bool REVERSE_BITS_BUS1 = false;  // LEFT bus bit order fix
bool REVERSE_BITS_BUS2 = false;  // RIGHT bus bit order fix

// Active-low interface (0 => full power). Set via GUI (SetInvert).
bool ACTIVE_LOW_DAC = false;

// ---- Control state ----
volatile bool pidEnabled = false;     // (name kept for protocol compatibility)

// Bang-Bang tunables (host-set)
uint8_t BASE_SPEED = 150;             // SetBase (0..255)
uint8_t deadband   = 10;              // SetKp   (0..255) minimum |L-R| before turning
uint8_t turnBoost  = 80;              // SetKi   (0..255) added/subtracted on turns

// Optional threshold (if you want to binarize sensors 0/255 before diff)
int SENSOR_THRESHOLD = 512;           // SetThresh mapped to 0..1023; 0 disables binarization

byte  outputL = 0, outputR = 0;

// ---- Helpers ----
static inline byte clamp255(int v) { return (v < 0) ? 0 : (v > 255) ? 255 : (byte)v; }

// We treat "logical 0" as STOP; physical inversion happens in the writer.
inline byte STOP_VALUE() { return 0; }

inline byte maybeInvert(byte logicalVal) {
  // If ACTIVE_LOW_DAC is true, 0 (STOP) becomes 255 on the physical lines, etc.
  return ACTIVE_LOW_DAC ? (byte)(255 - logicalVal) : logicalVal;
}

void writeDACBusRaw(const byte p[8], byte physicalVal, bool reverseBits) {
  // Writes a PHYSICAL byte to pins. reverseBits=true if your ladder maps MSB to p[0], etc.
  for (byte i = 0; i < 8; i++) {
    byte bitIndex = reverseBits ? (7 - i) : i;
    digitalWrite(p[i], ((physicalVal >> bitIndex) & 1) ? HIGH : LOW);
  }
}

void outputToDAC1(byte logicalVal) { writeDACBusRaw(DACPIN1, maybeInvert(logicalVal), REVERSE_BITS_BUS1); } // LEFT
void outputToDAC2(byte logicalVal) { writeDACBusRaw(DACPIN2, maybeInvert(logicalVal), REVERSE_BITS_BUS2); } // RIGHT

void motorsStop() {
  byte s = STOP_VALUE();
  outputL = s;
  outputR = s;
  outputToDAC1(s);
  outputToDAC2(s);
}

void initDACs() {
  for (byte i = 0; i < 8; i++) {
    pinMode(DACPIN1[i], OUTPUT);
    pinMode(DACPIN2[i], OUTPUT);
  }
  motorsStop(); // Safe at power-up
}

byte scale10bitTo8bit(int raw) {
  long v = (long)raw * 255L / 1023L;
  if (v < 0)   v = 0;
  if (v > 255) v = 255;
  return (byte)v;
}

float mapByteTo(float b, float mn, float mx) {
  return mn + (mx - mn) * (b / 255.0f);
}

// ---- Bang-Bang controller (uses differential error with deadband) ----
void BangBangController() {
  // Read sensors and scale to 0..255
  uint8_t sL = scale10bitTo8bit(analogRead(SENSOR_LEFT));
  uint8_t sR = scale10bitTo8bit(analogRead(SENSOR_RIGHT));

  // Optional binarization using threshold if non-zero (gives crisp behavior on high contrast)
  if (SENSOR_THRESHOLD > 0) {
    uint8_t th8 = scale10bitTo8bit(SENSOR_THRESHOLD); // convert threshold to 0..255 range
    sL = (sL >= th8) ? 255 : 0;
    sR = (sR >= th8) ? 255 : 0;
  }

  int err = (int)sL - (int)sR;  // positive => more left seen => steer right

  // Keep BASE_SPEED visible: cap boost to available headroom on each side
  uint8_t effectiveBoost = turnBoost;
  if (effectiveBoost > BASE_SPEED)                 effectiveBoost = BASE_SPEED;
  if (effectiveBoost > (uint8_t)(255 - BASE_SPEED)) effectiveBoost = (uint8_t)(255 - BASE_SPEED);

  if (err > (int)deadband) {
    // steer RIGHT: reduce left, increase right
    outputL = clamp255((int)BASE_SPEED - (int)effectiveBoost);
    outputR = clamp255((int)BASE_SPEED + (int)effectiveBoost);
  } else if (err < -(int)deadband) {
    // steer LEFT: increase left, reduce right
    outputL = clamp255((int)BASE_SPEED + (int)effectiveBoost);
    outputR = clamp255((int)BASE_SPEED - (int)effectiveBoost);
  } else {
    // go straight
    outputL = BASE_SPEED;
    outputR = BASE_SPEED;
  }

  outputToDAC1(outputL);
  outputToDAC2(outputR);
}

// ---- Serial protocol ----
void sendPacket(byte port, byte data) {
  byte chk = (byte)(START + port + data);
  Serial.write(START);
  Serial.write(port);
  Serial.write(data);
  Serial.write(chk);
}

void processPacket(byte port, byte data) {
  switch (port) {

    case Input1:
      sendPacket(Input1, scale10bitTo8bit(analogRead(SENSOR_LEFT)));
      break;

    case Input2:
      sendPacket(Input2, scale10bitTo8bit(analogRead(SENSOR_RIGHT)));
      break;

    // IMPORTANT: block any motion unless pidEnabled==true
    case Output1: // manual LEFT
      if (pidEnabled) {
        outputL = data;
        outputToDAC1(outputL);
      }
      break;

    case Output2: // manual RIGHT
      if (pidEnabled) {
        outputR = data;
        outputToDAC2(outputR);
      }
      break;

    case RequestOutput1:
      sendPacket(RequestOutput1, outputL);
      break;

    case RequestOutput2:
      sendPacket(RequestOutput2, outputR);
      break;

    case PidEnable: {
      bool newState = (data != 0);
      pidEnabled = newState;

      if (!pidEnabled) {
        motorsStop();
      }
    } break;

    // --- Bang-Bang tunables from GUI ---
    case SetKp:     // Deadband 0..255
      deadband = data;
      break;

    case SetKi:     // TurnBoost 0..255
      turnBoost = data;
      break;

    case SetKd:     // unused
      /* no-op */ 
      break;

    case SetBase:   // Base speed 0..255
      BASE_SPEED = data;
      break;

    case SetThresh: // 0..255 -> 0..1023 (optional binarization threshold; 0 disables)
      SENSOR_THRESHOLD = (int)mapByteTo(data, 0.0f, 1023.0f);
      break;

    case SetInvert:
      ACTIVE_LOW_DAC = (data != 0);
      // Re-assert STOP when disabled so polarity flips don't cause a surge
      if (!pidEnabled) motorsStop();
      break;

    default:
      // ignore unknown ports
      break;
  }
}

// ---- Arduino hooks ----
void setup() {
  pinMode(SENSOR_LEFT, INPUT);
  pinMode(SENSOR_RIGHT, INPUT);

  initDACs();          // sets outputs to STOP (safe-start)
  Serial.begin(9600);
}

void loop() {
  // Safety: keep STOP if not enabled
  if (!pidEnabled) {
    motorsStop();
  } else {
    BangBangController();
  }

  // Handle 4-byte packets: START, cmd, data, checksum
  while (Serial.available() >= 4) {
    byte b0 = Serial.read();
    if (b0 != START) continue;

    byte cmd = Serial.read();
    byte dat = Serial.read();
    byte chk = Serial.read();

    byte calc = (byte)(START + cmd + dat);
    if (chk == calc) {
      processPacket(cmd, dat);
    }
  }

  delay(2);
}