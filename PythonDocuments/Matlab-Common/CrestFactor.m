function ret = CrestFactor( timeWave )
% ��ֵָ��

ret = Div( AbsMax( timeWave ), RMS( timeWave ) );
