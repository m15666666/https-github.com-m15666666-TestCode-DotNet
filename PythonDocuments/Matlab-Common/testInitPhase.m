warning('off','MATLAB:dispatcher:InexactCaseMatch');
% testInitPhase 测试正弦波初始相位与fft计算角度的关系

CloseFigures();

f0 = 4;%设定正弦信号频率
fs = 16;%设定采样频率

% 16 / 4 = 4，cos函数时，第二个元素就是初始角度( 4 * 1 = 4 Hz)，第一个元素是直流
N = 4;

t = CreateDTs( fs, N );

%生成正弦信号
initPhase = pi / 4;
sinWave = cos( 2 * pi * f0 * t + initPhase );
%sinWave = sin( 2 * pi * f0 * t + initPhase + pi / 2  );

% fft与ifft角度关系是：angle<fft> = 360 - angle<ifft>。
angles = angle( fft( sinWave, N ) );
%angles = angle( ifft( sinWave, N ) );

for index = 1 : N
    angles(index) = Rad2Degree( angles(index) );
end

% 显示角度
angles