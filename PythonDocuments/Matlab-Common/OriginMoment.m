function ret = OriginMoment( timeWave, order )
% ���������n��ԭ���

ret = mean( power( timeWave, order ) );
