warning('off','MATLAB:dispatcher:InexactCaseMatch');
% testResampleAndSpAngle
% 测试重采样和频谱的相位

CloseFigures();

f0 = 51.345;%设定正弦信号频率
fs = f0 * 250.3333;%设定采样频率
multiFreq = 64;
cycleCount = 1;
N1 = round( fs / f0 * cycleCount );
N = N1;
t = CreateDTs( fs, N );

%生成正弦信号
sinWave = cos( 2 * pi * f0 * t + pi / 3 );

% 写入文本文件
% WriteWave2File(sinWave, 'c:\sinWave.txt')

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

