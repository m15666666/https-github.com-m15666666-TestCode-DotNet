function wave = ACCoupling( timeWave )
% 去掉信号直流

wave = timeWave - mean( timeWave );

