warning('off','MATLAB:dispatcher:InexactCaseMatch');
% testOverall

CloseFigures();

%***************1.���Ҳ�****************%
% fs = 4096;%�趨����Ƶ��
% N = 8192;
% t = CreateDTs( fs, N );
% f0 = 50;%�趨�����ź�Ƶ��
% 
% %���������ź�
% sinWave = sin( 2 * pi * f0 * t );
% 
% DrawTimeWave( sinWave, fs );
% 
% DrawSingleFFT( sinWave, fs );

f0 = 51.345;%�趨�����ź�Ƶ��
fs = f0 * 250.3333;%�趨����Ƶ��
% f0 = 3;%51.345;%�趨�����ź�Ƶ��
% fs = 12;%f0 * 250.3333;%�趨����Ƶ��
multiFreq = 64;
cycleCount = 1;
N1 = round( fs / f0 * cycleCount );
N = N1;
N = 1024;
t = CreateDTs( fs, N );
% for iN = N1 + 1 : N
%     t(iN) = 0;
% end

%���������ź�
sinWave = cos( 2 * pi * f0 * t );
RMS(sinWave)
Overall(sinWave, N/2.56)