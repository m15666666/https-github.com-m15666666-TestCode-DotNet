function ret = ImpulseFactor( timeWave )
% ����ָ��

ret = Div( AbsMax( timeWave ), AbsMean( timeWave ) );
