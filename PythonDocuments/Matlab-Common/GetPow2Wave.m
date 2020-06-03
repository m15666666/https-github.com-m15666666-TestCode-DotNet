function pow2Wave = GetPow2Wave( timeWave )
% 获得2的幂次长度的波形

waveLength = length( timeWave );
retLength = GetPow2Number( waveLength );

pow2Wave = timeWave( 1 : retLength );