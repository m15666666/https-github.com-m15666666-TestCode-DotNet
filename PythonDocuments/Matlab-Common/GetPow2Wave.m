function pow2Wave = GetPow2Wave( timeWave )
% ���2���ݴγ��ȵĲ���

waveLength = length( timeWave );
retLength = GetPow2Number( waveLength );

pow2Wave = timeWave( 1 : retLength );