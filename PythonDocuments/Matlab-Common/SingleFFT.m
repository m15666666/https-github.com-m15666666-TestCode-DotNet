function amplitude = SingleFFT( timeWave, waveLength )
% 单边FFT的幅值
dualSpectrum = DualFFT( timeWave, waveLength );

% 取一半谱线
dualSpectrum = dualSpectrum( 1 : waveLength / 2 );

amplitude = dualSpectrum * 2;
amplitude(1) = amplitude(1) / 2;
