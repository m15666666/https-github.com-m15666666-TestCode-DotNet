function amplitude = DualFFT( timeWave, waveLength )
% ˫��FFT�ķ�ֵ
spectrum = fft( timeWave, waveLength ); %����fft�任

amplitude = abs( spectrum ) / waveLength; %���ֵ