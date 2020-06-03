
% 信号经过FFT后，该怎样计算幅值和相位
% http://www.chinavib.com/forum/viewthread.php?tid=53683&page=1#pid305973
% testWholeCycleSample2

fs=1;
N=100;  %频率分辨率为fs/N＝0.01Hz，下面信号的频率0.05是0.01的整数倍，即为整周期采样
n=0:N-1;
t=n/fs;
f0=0.05;%设定余弦信号频率
x=cos(2*pi*f0*t);%生成正弦信号 %FFT是余弦类变换，最后得到的初始相位是余弦信号的初时相位，在这里为0。如果信号
figure(1);                                   %为x=sin(2*pi*f0*t);则初时相位应该是－90度而非0度。
subplot(211);
plot(t,x);%作余弦信号的时域波形
xlabel('t');
ylabel('y');
title('余弦信号 时域波形');
grid;
%进行FFT变换并做频谱图
y=fft(x,N);%进行fft变换
mag=abs(y)*2/N;%求幅值 乘上后面的2/N得到正确幅值
f=(0:length(y)-1)'*fs/length(y);%进行对应的频率转换
subplot(212);
plot(f(1:N/2),mag(1:N/2));%做频谱图
xlabel('频率(Hz)');
ylabel('幅值');
title('余弦信号 幅频谱图');
grid;
angle(y(6))*180/pi %求信号初时相位。频率坐标f为[0 0.01 0.02 0.03 0.04 0.05 0.06 ...]，所以谱线y中第6根谱线和信号x对应。