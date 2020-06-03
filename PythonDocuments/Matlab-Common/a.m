% From��http://www.chinavib.com/forum/viewthread.php?tid=51332

%*************************************************************************%
% <FAQ>

% ���Ҳ��̶�������ô���������׷�ֵ
% û������
% ���а���������ô����ģ�

% (1)��ģ���ź�AD������Ƶ�������԰��ƵĽ����
% ģ���źŲ����� Ƶ���Բ���Ƶ��Ϊ���ڽ���Ƶ�װ��ơ����������ź�Ƶ����-10��10���������������-10��Ƶ��+100=90��������90��������һ����
% ��2�������������Ĳ��Ǹ�˹��������ʱ���Σ����������˹����������غ���һ�ֽ��ƣ�����Ӧ���������ģ�������ȡ��һ��10000��Ϊ���ƣ�Ȼ��FFT����Ǻܰ׵Ĺ����ס�
% x(50)=100000;
% 
% plot(t(1:100),x);%����������ʱ����    %Ӧ���޶�Ϊ�����˹����������غ���
% xlabel('t');
% ylabel('y');
% title('������ʱ����');
% grid;
% 
% �������Գ�ѧ����˵ͦ���õģ���������ϸ���¡�
% �и����ʣ�ΪʲôƵ��ͼ�;���������һ���£� ����������ʲô��û������
% ���Գ������£�
% %%��Ƶ��ͼ
% mag=abs(y);%���ֵ
% 
% %���������
% sq=abs(y);
% 
% ���飺��ҿ�����
% %����FFT�任����Ƶ��ͼ
% y=fft(x);%����fft�任              �����޸�Ϊy=fftshift(fft(x));
% mag=abs(y);%���ֵsq=abs(y);
% ����Կ�����˵�е�SINC��������ֱ�ۣ�Ҳ���Խ�˻����˽���FFTSHIFT��

% </FAQ>


%*************************************************************************%
%                              FFTʵ����Ƶ�׷���                          %
%*************************************************************************%
%*************************************************************************%

%***************1.���Ҳ�****************%
fs=100;%�趨����Ƶ��
N=128;
n=0:N-1;
t=n/fs;
f0=10;%�趨�����ź�Ƶ��

%���������ź�
x=sin(2*pi*f0*t);
figure(1);
subplot(231);
plot(t,x);%�������źŵ�ʱ����
xlabel('t');
ylabel('y');
title('�����ź�y=2*pi*10tʱ����');
grid;

%����FFT�任����Ƶ��ͼ
y=fft(x,N);%����fft�任
mag=abs(y);%���ֵ
%f=(0:length(y)-1)'*fs/length(y);%���ж�Ӧ��Ƶ��ת��
f=(0:length(y)-1)*fs/length(y);%���ж�Ӧ��Ƶ��ת��
figure(1);
%subplot(232);
subplot(2,3,2);
plot(f,mag);%��Ƶ��ͼ
axis([0,100,0,80]);
xlabel('Ƶ��(Hz)');
ylabel('��ֵ');
title('�����ź�y=2*pi*10t��Ƶ��ͼN=128');
grid;

%���������
sq=abs(y);
figure(1);
subplot(2,3,3);
plot(f,sq);
xlabel('Ƶ��(Hz)');
ylabel('��������');
title('�����ź�y=2*pi*10t��������');
grid;

%������
power=sq.^2;
figure(1);
subplot(2,3,4);
plot(f,power);
xlabel('Ƶ��(Hz)');
ylabel('������');
title('�����ź�y=2*pi*10t������');
grid;

%�������
ln=log(sq);
figure(1);
subplot(2,3,5);
plot(f,ln);
xlabel('Ƶ��(Hz)');
ylabel('������');
title('�����ź�y=2*pi*10t������');
grid;

%��IFFT�ָ�ԭʼ�ź�
xifft=ifft(y);
magx=real(xifft);
ti=[0:length(xifft)-1]/fs;
figure(1);
subplot(2,3,6);
plot(ti,magx);
xlabel('t');
ylabel('y');
title('ͨ��IFFTת���������źŲ���');
grid;

%****************2.���β�****************%
fs=10;%�趨����Ƶ��
t=-5:0.1:5;
x=rectpuls(t,2);
x=x(1:99);
figure(2);
subplot(231);
plot(t(1:99),x);%�����β���ʱ����
xlabel('t');
ylabel('y');
title('���β�ʱ����');
grid;
%����FFT�任����Ƶ��ͼ
y=fft(x);%����fft�任
mag=abs(y);%���ֵ
f=(0:length(y)-1)'*fs/length(y);%���ж�Ӧ��Ƶ��ת��
figure(2);
subplot(232);
plot(f,mag);%��Ƶ��ͼ
xlabel('Ƶ��(Hz)');
ylabel('��ֵ');
title('���β���Ƶ��ͼ');
grid;
%���������
sq=abs(y);
figure(2);
subplot(233);
plot(f,sq);
xlabel('Ƶ��(Hz)');
ylabel('��������');
title('���β���������');
grid;
%������
power=sq.^2;
figure(2);
subplot(234);
plot(f,power);
xlabel('Ƶ��(Hz)');
ylabel('������');
title('���β�������');
grid;
%�������
ln=log(sq);
figure(2);
subplot(235);
plot(f,ln);
xlabel('Ƶ��(Hz)');
ylabel('������');
title('���β�������');
grid;
%��IFFT�ָ�ԭʼ�ź�
xifft=ifft(y);
magx=real(xifft);
ti=[0:length(xifft)-1]/fs;
figure(2);
subplot(236);
plot(ti,magx);
xlabel('t');
ylabel('y');
title('ͨ��IFFTת���ľ��β�����');
grid;

%****************3.������****************%
fs=10;%�趨����Ƶ��
t=-5:0.1:5;
x=zeros(1,100);
x(50)=100000;
figure(3);
subplot(231);
plot(t(1:100),x);%����������ʱ����
xlabel('t');
ylabel('y');
title('������ʱ����');
grid;
%����FFT�任����Ƶ��ͼ
y=fft(x);%����fft�任
mag=abs(y);%���ֵ
f=(0:length(y)-1)'*fs/length(y);%���ж�Ӧ��Ƶ��ת��
figure(3);
subplot(232);
plot(f,mag);%��Ƶ��ͼ
xlabel('Ƶ��(Hz)');
ylabel('��ֵ');
title('��������Ƶ��ͼ');
grid;
%���������
sq=abs(y);
figure(3);
subplot(233);
plot(f,sq);
xlabel('Ƶ��(Hz)');
ylabel('��������');
title('��������������');
grid;
%������
power=sq.^2;
figure(3);
subplot(234);
plot(f,power);
xlabel('Ƶ��(Hz)');
ylabel('������');
title('������������');
grid;
%�������
ln=log(sq);
figure(3);
subplot(235);
plot(f,ln);
xlabel('Ƶ��(Hz)');
ylabel('������');
title('������������');
grid;
%��IFFT�ָ�ԭʼ�ź�
xifft=ifft(y);
magx=real(xifft);
ti=[0:length(xifft)-1]/fs;
figure(3);
subplot(236);
plot(ti,magx);
xlabel('t');
ylabel('y');
title('ͨ��IFFTת���İ���������');
grid;