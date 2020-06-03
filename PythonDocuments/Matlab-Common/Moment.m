function ret = Moment( timeWave, order )
% 计算数组的n阶矩

ret = mean( power( timeWave - mean( timeWave ), order ) );
