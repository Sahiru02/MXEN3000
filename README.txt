Line Follower — Bang‑Bang Controller (with GUI protocol)
=======================================================
• Safe-start: motors OFF until PidEnable=1
• Active-low toggle via SetInvert //Doesn't work
• Base speed: SetBase (0..255)
• Deadband: (0..255)
• Turn boost: (0..255)


Pins:
  LEFT  DAC bus: 9,8,7,6,5,4,3,2
  RIGHT DAC bus: A2,A3,A4,A5,A1,A0,11,10
  Sensors: LEFT=A7, RIGHT=A6

