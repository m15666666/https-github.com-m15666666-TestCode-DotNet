function amplitude = SingleFFT( timeWave, waveLength )
% ����FFT�ķ�ֵ
dualSpectrum = DualFFT( timeWave, waveLength );

% ȡһ������
dualSpectrum = dualSpectrum( 1 : waveLength / 2 );

amplitude = dualSpectrum * 2;
amplitude(1) = amplitude(1) / 2;
