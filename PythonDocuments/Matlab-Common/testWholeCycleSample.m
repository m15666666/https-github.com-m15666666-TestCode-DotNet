
% 整周期采样的信号为何还有频谱泄漏
% From：http://bbs.matwav.com/viewthread.php?tid=853252
% testWholeCycleSample

fs=20; 
t=0:1/fs:1; 
% 信号1
x1=sin(2*pi*t);
% 信号波形
subplot(2,2,1) 
stem(x1); 
title('正弦信号，初相0') 
xlabel('序列（n）') 
grid on 
% fft谱分析
number = 512;
%number = length(x1)
Y = fft(x1,number);

f = fs*(0:(number/2-1))/number; 
subplot(2,2,2) 
plot(f,abs(Y(1:number/2)));hold 
%stem(f,abs(Y(1:number/2)));
title('正弦信号的FFT') 
xlabel('频率Hz') 
grid on 

% 信号2
t=0:1/fs:2; 
x2=sin(2*pi*t);
% 信号2波形
subplot(2,2,3) 
stem(x2); 
title('正弦信号，初相0') 
xlabel('序列（n）') 
grid on 
Y = fft(x2,number);
f = fs* (0 :  (number/2-1))/number; 
% fft谱分析
subplot(2,2,4) 
plot(f,abs(Y(1:number/2)));hold 
%stem(f,abs(Y(1:number/2)));
title('正弦信号的FFT') 
xlabel('频率Hz') 
grid on 
 
