warning('off','MATLAB:dispatcher:InexactCaseMatch');
% testCorrelation 测试相关

% 例子1
% dt=.1;
% t=[0:dt:100];
% x=cos(t);
% [a,b]=xcorr(x,'unbiased');
% plot(b*dt,a)

% % 例子2
% dt=.1;
% t=[0:dt:100];
% x=3*cos(t);
% % y=cos(3*t);
% y=3*cos(t);
% 
% xLength = length(x);
% %端点效应修正时除的数组
% divLengths = [1:1:xLength xLength-1:-1:1];
% 
% % x,y的标准差
% xStdDeviation = std(x);
% yStdDeviation = std(y);
% 
% subplot(3,1,1);
% plot(t,x);
% subplot(3,1,2);
% plot(t,y);
% %[a,b]=xcorr(x,y);
% [a,b]=xcorr(x,y, 'coeff');
% %a = a ./ divLengths;
% %a = a ./ divLengths / ( xStdDeviation * yStdDeviation );
% subplot(3,1,3);
% plot(b*dt,a );


% 例子3
f0 = 1;%设定正弦信号频率
fs = f0 * 16;%设定采样频率
N = 32;
t = CreateDTs( fs, N );
sinWave = 1 * sin( 2 * pi * f0 * t );

dt=1/fs;
x = sinWave;
y = sinWave;

xLength = length(x);
%端点效应修正时除的数组
divLengths = [1:1:xLength xLength-1:-1:1];

% x,y的标准差
xStdDeviation = std(x);
yStdDeviation = std(y);

subplot(3,1,1);
plot(t,x);
subplot(3,1,2);
plot(t,y);
%[a,b]=xcorr(x,y, 'coeff');
[a,b]=xcorr(x,y);
%a = a ./ divLengths  / ( xStdDeviation * yStdDeviation );
a = a ./ divLengths
%a = a / ( xStdDeviation * yStdDeviation );
subplot(3,1,3);
plot(b*dt,a );

% yy=cos(3*fliplr(t)); % or use: yy=fliplr(y);
% 翻转（倒序）
yy=fliplr(y);

% 进行卷积
z=conv(x,yy) ./ divLengths / ( xStdDeviation * yStdDeviation );

% % pause;
% % subplot(3,1,3);
% % plot(b*dt,z,'r');