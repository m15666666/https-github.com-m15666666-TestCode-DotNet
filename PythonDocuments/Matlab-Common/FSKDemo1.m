% FSKDemo1
% From：http://www.chinavib.com/forum/thread-46775-1-1.html

clear all;
x=randn(10,1)>0;
fl=5000;fh=8000;fs=100000;
ts=1/200;%码元速率200波特
tt=(0:1/fs:ts);
t=[tt;tt+ts;tt+2*ts;tt+3*ts;tt+4*ts;tt+5*ts;tt+6*ts;tt+7*ts;tt+8*ts;tt+9*ts];
y=zeros(10,length(tt));
i=1;
%对该输入信号FSK调制
while i<=10
y(i,:)=x(i)*sin(2*pi*fh*t(i,:))+~x(i)*sin(2*pi*fl*t(i,:));
i=i+1;
end
t=reshape(t',length(tt)*10,1);
y=reshape(y',length(tt)*10,1);
subplot(211);plot(t,y);
title('FSK信号的时域图形');
%该输入信号的频域图形
n=length(y);
r=fft(y)/n;r=fftshift(r);
f=linspace(-fs/2,fs/2,n);
subplot(212);
plot(f,abs(r));
set(gca,'XTick',-fs/2:5000:fs/2);
title('FSK信号的频谱图');