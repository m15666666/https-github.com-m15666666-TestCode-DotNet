clc
clear
fs = 2560;
fr = 50;
fa = 10;
L = 1024;
t = [0:L-1]/fs;
a = (1+sin(2*pi*fa*t));
x = a.*sin(2*pi*fr*t);
figure
subplot(211)
plot(t,x)
subplot(212)
f_show = [0:round(L/(2.56))-1]*fs/L;
fft_x = abs(fft(x))/L;
fft_x_show = [fft_x(1) 2*fft_x(2:round(L/(2.56)))];
plot(f_show,fft_x_show)

figure
subplot(211)
plot(f_show,fft_x_show)
subplot(212)
Hilbert_x = abs(fft(abs(hilbert(x))))/L;
plot(f_show,Hilbert_x(1:round(L/(2.56))))
