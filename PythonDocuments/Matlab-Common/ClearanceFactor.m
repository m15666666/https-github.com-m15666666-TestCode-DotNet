function ret = ClearanceFactor( timeWave )
% ԣ��ָ��

ret = Div( AbsMax( timeWave ), SMR( timeWave ) );
