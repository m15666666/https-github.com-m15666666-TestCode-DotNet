function ret = SMR( timeWave )
% ������ֵ

sqrtMean = mean( sqrt( abs( timeWave ) ) );

ret = sqrtMean .^ 2;
