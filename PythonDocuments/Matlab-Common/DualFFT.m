function amplitude = DualFFT( timeWave, waveLength )
% 双边FFT的幅值
spectrum = fft( timeWave, waveLength ); %进行fft变换

amplitude = abs( spectrum ) / waveLength; %求幅值