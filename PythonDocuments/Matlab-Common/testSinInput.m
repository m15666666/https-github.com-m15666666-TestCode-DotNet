warning('off','MATLAB:dispatcher:InexactCaseMatch');
% testSinInput

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
multiFreq = 64;
cycleCount = 1;
N1 = round( fs / f0 * cycleCount );
N = N1;
t = CreateDTs( fs, N );
% for iN = N1 + 1 : N
%     t(iN) = 0;
% end

%���������ź�
sinWave = cos( 2 * pi * f0 * t + pi / 3 );

dt1 = 1 / fs;

fs2 = multiFreq * f0;
dt2 = 1 / fs2;

t2 = CreateDTs( fs2, multiFreq * cycleCount );
wave2 = cos( 2 * pi * f0 * t2 );
wave2(1) = sinWave(1);

x1 = t(1);
y1 = sinWave(1);
for waveIndex = 2 : length(wave2)
    dx = dt2 * ( waveIndex - 1 );
    lowIndex = fix( dx / dt1 ) + 1;
    if N <= lowIndex 
        lowIndex = N - 1;
    end
    highIndex = lowIndex + 1;
    wave2(waveIndex) = GetLinearY( t(lowIndex), sinWave(lowIndex), t(highIndex), sinWave(highIndex), t2(1) + dx );
end

fs = fs2;
sinWave = wave2;
N = length(sinWave);

% N = round(fs / f0 * 80);
% t = CreateDTs( fs, N );

DrawTimeWave( sinWave, fs );

DrawSingleFFT( sinWave, fs );

sp = fft( sinWave, N ); 
ph = angle(sp) / pi * 180;
for i = 1 : length( ph )
    if ph(i) < 0 
        ph(i) = ph(i) + 360;
    end
end

DrawPlot( ph );

index = 1 + fix( f0 * N / fs ) %#ok<NOPTS>
ph( index ) %#ok<NOPTS>

% dftsp = DFT( sinWave, N );
% dftph = angle(sp) / pi * 180;
% for i = 1 : length( dftph )
%     if dftph(i) < 0 
%         dftph(i) = dftph(i) + 360;
%     end
% end
% 
% DrawPlot( dftph );

