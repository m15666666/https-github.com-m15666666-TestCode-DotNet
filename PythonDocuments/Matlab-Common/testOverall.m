warning('off','MATLAB:dispatcher:InexactCaseMatch');
% testOverall

CloseFigures();

%***************1.正弦波****************%
% fs = 4096;%设定采样频率
% N = 8192;
% t = CreateDTs( fs, N );
% f0 = 50;%设定正弦信号频率
% 
% %生成正弦信号
% sinWave = sin( 2 * pi * f0 * t );
% 
% DrawTimeWave( sinWave, fs );
% 
% DrawSingleFFT( sinWave, fs );

f0 = 51.345;%设定正弦信号频率
fs = f0 * 250.3333;%设定采样频率
% f0 = 3;%51.345;%设定正弦信号频率
% fs = 12;%f0 * 250.3333;%设定采样频率
multiFreq = 64;
cycleCount = 1;
N1 = round( fs / f0 * cycleCount );
N = N1;
N = 1024;
t = CreateDTs( fs, N );
% for iN = N1 + 1 : N
%     t(iN) = 0;
% end

%生成正弦信号
sinWave = cos( 2 * pi * f0 * t );
RMS(sinWave)
Overall(sinWave, N/2.56)