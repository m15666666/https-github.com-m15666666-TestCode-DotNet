function ret = Moment( timeWave, order )
% ���������n�׾�

ret = mean( power( timeWave - mean( timeWave ), order ) );
