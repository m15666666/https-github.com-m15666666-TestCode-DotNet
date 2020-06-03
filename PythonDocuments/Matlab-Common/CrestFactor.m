function ret = CrestFactor( timeWave )
% ·åÖµÖ¸±ê

ret = Div( AbsMax( timeWave ), RMS( timeWave ) );
