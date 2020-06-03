% From：http://www.chinavib.com/forum/viewthread.php?tid=51332

%*************************************************************************%
% <FAQ>

% 正弦波固定周期怎么出现两个谱峰值
% 没看明白
% 还有白噪声是怎么定义的？

% (1)是模拟信号AD采样后频谱周期性搬移的结果。
% 模拟信号采样后 频谱以采样频率为周期进行频谱搬移。本来正弦信号频谱在-10和10处有两个冲击，但-10的频率+100=90，所以在90处出现了一个。
% （2）程序中所画的不是高斯白噪声的时域波形，而是理想高斯白噪声的相关函数一种近似，本来应该是无穷大的，程序中取了一个10000作为近似，然后FFT后才是很白的功率谱。
% x(50)=100000;
% 
% plot(t(1:100),x);%作白噪声的时域波形    %应该修订为理想高斯白噪声的相关函数
% xlabel('t');
% ylabel('y');
% title('白噪声时域波形');
% grid;
% 
% 这个程序对初学者来说挺有用的，建议大家仔细看下。
% 有个疑问：为什么频谱图和均方根谱是一回事？ 均方根谱是什么？没听过。
% 各自程序如下：
% %%做频谱图
% mag=abs(y);%求幅值
% 
% %求均方根谱
% sq=abs(y);
% 
% 建议：大家可以在
% %进行FFT变换并做频谱图
% y=fft(x);%进行fft变换              此行修改为y=fftshift(fft(x));
% mag=abs(y);%求幅值sq=abs(y);
% 便可以看到传说中的SINC函数，更直观，也可以借此机会了解下FFTSHIFT。

% </FAQ>


%*************************************************************************%
%                              FFT实践及频谱分析                          %
%*************************************************************************%
%*************************************************************************%

%***************1.正弦波****************%
fs=100;%设定采样频率
N=128;
n=0:N-1;
t=n/fs;
f0=10;%设定正弦信号频率

%生成正弦信号
x=sin(2*pi*f0*t);
figure(1);
subplot(231);
plot(t,x);%作正弦信号的时域波形
xlabel('t');
ylabel('y');
title('正弦信号y=2*pi*10t时域波形');
grid;

%进行FFT变换并做频谱图
y=fft(x,N);%进行fft变换
mag=abs(y);%求幅值
%f=(0:length(y)-1)'*fs/length(y);%进行对应的频率转换
f=(0:length(y)-1)*fs/length(y);%进行对应的频率转换
figure(1);
%subplot(232);
subplot(2,3,2);
plot(f,mag);%做频谱图
axis([0,100,0,80]);
xlabel('频率(Hz)');
ylabel('幅值');
title('正弦信号y=2*pi*10t幅频谱图N=128');
grid;

%求均方根谱
sq=abs(y);
figure(1);
subplot(2,3,3);
plot(f,sq);
xlabel('频率(Hz)');
ylabel('均方根谱');
title('正弦信号y=2*pi*10t均方根谱');
grid;

%求功率谱
power=sq.^2;
figure(1);
subplot(2,3,4);
plot(f,power);
xlabel('频率(Hz)');
ylabel('功率谱');
title('正弦信号y=2*pi*10t功率谱');
grid;

%求对数谱
ln=log(sq);
figure(1);
subplot(2,3,5);
plot(f,ln);
xlabel('频率(Hz)');
ylabel('对数谱');
title('正弦信号y=2*pi*10t对数谱');
grid;

%用IFFT恢复原始信号
xifft=ifft(y);
magx=real(xifft);
ti=[0:length(xifft)-1]/fs;
figure(1);
subplot(2,3,6);
plot(ti,magx);
xlabel('t');
ylabel('y');
title('通过IFFT转换的正弦信号波形');
grid;

%****************2.矩形波****************%
fs=10;%设定采样频率
t=-5:0.1:5;
x=rectpuls(t,2);
x=x(1:99);
figure(2);
subplot(231);
plot(t(1:99),x);%作矩形波的时域波形
xlabel('t');
ylabel('y');
title('矩形波时域波形');
grid;
%进行FFT变换并做频谱图
y=fft(x);%进行fft变换
mag=abs(y);%求幅值
f=(0:length(y)-1)'*fs/length(y);%进行对应的频率转换
figure(2);
subplot(232);
plot(f,mag);%做频谱图
xlabel('频率(Hz)');
ylabel('幅值');
title('矩形波幅频谱图');
grid;
%求均方根谱
sq=abs(y);
figure(2);
subplot(233);
plot(f,sq);
xlabel('频率(Hz)');
ylabel('均方根谱');
title('矩形波均方根谱');
grid;
%求功率谱
power=sq.^2;
figure(2);
subplot(234);
plot(f,power);
xlabel('频率(Hz)');
ylabel('功率谱');
title('矩形波功率谱');
grid;
%求对数谱
ln=log(sq);
figure(2);
subplot(235);
plot(f,ln);
xlabel('频率(Hz)');
ylabel('对数谱');
title('矩形波对数谱');
grid;
%用IFFT恢复原始信号
xifft=ifft(y);
magx=real(xifft);
ti=[0:length(xifft)-1]/fs;
figure(2);
subplot(236);
plot(ti,magx);
xlabel('t');
ylabel('y');
title('通过IFFT转换的矩形波波形');
grid;

%****************3.白噪声****************%
fs=10;%设定采样频率
t=-5:0.1:5;
x=zeros(1,100);
x(50)=100000;
figure(3);
subplot(231);
plot(t(1:100),x);%作白噪声的时域波形
xlabel('t');
ylabel('y');
title('白噪声时域波形');
grid;
%进行FFT变换并做频谱图
y=fft(x);%进行fft变换
mag=abs(y);%求幅值
f=(0:length(y)-1)'*fs/length(y);%进行对应的频率转换
figure(3);
subplot(232);
plot(f,mag);%做频谱图
xlabel('频率(Hz)');
ylabel('幅值');
title('白噪声幅频谱图');
grid;
%求均方根谱
sq=abs(y);
figure(3);
subplot(233);
plot(f,sq);
xlabel('频率(Hz)');
ylabel('均方根谱');
title('白噪声均方根谱');
grid;
%求功率谱
power=sq.^2;
figure(3);
subplot(234);
plot(f,power);
xlabel('频率(Hz)');
ylabel('功率谱');
title('白噪声功率谱');
grid;
%求对数谱
ln=log(sq);
figure(3);
subplot(235);
plot(f,ln);
xlabel('频率(Hz)');
ylabel('对数谱');
title('白噪声对数谱');
grid;
%用IFFT恢复原始信号
xifft=ifft(y);
magx=real(xifft);
ti=[0:length(xifft)-1]/fs;
figure(3);
subplot(236);
plot(ti,magx);
xlabel('t');
ylabel('y');
title('通过IFFT转换的白噪声波形');
grid;