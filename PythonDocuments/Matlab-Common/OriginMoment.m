function ret = OriginMoment( timeWave, order )
% 计算数组的n阶原点矩

ret = mean( power( timeWave, order ) );
