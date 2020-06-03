function ret = SMR( timeWave )
% ·½¸ù·ùÖµ

sqrtMean = mean( sqrt( abs( timeWave ) ) );

ret = sqrtMean .^ 2;
