function ret = ShapeFactor( timeWave )
% ����ָ��

ret = Div( RMS( timeWave ), AbsMean( timeWave ) );