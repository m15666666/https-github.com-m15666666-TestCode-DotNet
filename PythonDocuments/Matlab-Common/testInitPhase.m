warning('off','MATLAB:dispatcher:InexactCaseMatch');
% testInitPhase �������Ҳ���ʼ��λ��fft����ǶȵĹ�ϵ

CloseFigures();

f0 = 4;%�趨�����ź�Ƶ��
fs = 16;%�趨����Ƶ��

% 16 / 4 = 4��cos����ʱ���ڶ���Ԫ�ؾ��ǳ�ʼ�Ƕ�( 4 * 1 = 4 Hz)����һ��Ԫ����ֱ��
N = 4;

t = CreateDTs( fs, N );

%���������ź�
initPhase = pi / 4;
sinWave = cos( 2 * pi * f0 * t + initPhase );
%sinWave = sin( 2 * pi * f0 * t + initPhase + pi / 2  );

% fft��ifft�Ƕȹ�ϵ�ǣ�angle<fft> = 360 - angle<ifft>��
angles = angle( fft( sinWave, N ) );
%angles = angle( ifft( sinWave, N ) );

for index = 1 : N
    angles(index) = Rad2Degree( angles(index) );
end

% ��ʾ�Ƕ�
angles