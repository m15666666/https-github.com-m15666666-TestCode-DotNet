
fs=256;%����Ƶ��
N=512;%��������
nfft=512;
n=0:1:N-1;%ʱ�����к�
%n/fs:����Ƶ���¶�Ӧ��ʱ������ֵ
n1=fs*(0:nfft/2-1)/nfft;%F F T��Ӧ��Ƶ������
x=3*cos(2*pi*100*n/fs)+3*cos(2*pi*101.45*n/fs)+2*cos(2*pi*102.3*n/fs)+4*cos(2*pi*103.8*n/fs)+5*cos(2*pi*104.5*n/fs);
figure;
plot(n,x);
xlabel('ʱ��t');
ylabel('value');
title('�źŵ�ʱ����');
%-------
XK=fft(x,nfft);%���߷�ֵ��
figure;
subplot(211);stem(n1,abs(XK(1:(nfft/2))));%�ø�״����FFT��ͼ��Ҳ����
axis([95,110,0,1500]);
title('ֱ������FFT�任���Ƶ��');
subplot(212);plot(n1,abs(XK(1:(N/2))));
axis([95,110,0,1500]);
title('ֱ������FFT�任���Ƶ��');
%-----------
f1=100;%ϸ��Ƶ�ʶ����
f2=110;%ϸ��Ƶ�ʶ��յ�
M=256;%ϸ��Ƶ�ε�Ƶ��������������ʵ����ϸ�����ȣ�
w=exp(-j*2*pi*(f2-f1)/(fs*M));%ϸ��Ƶ�εĿ�ȣ�������
a=exp(j*2*pi*f1/fs);%ϸ��Ƶ�ε���ʼ�㣬������Ҫ����һ�²��ܴ���czt����

xk=czt(x,M,w,a);

h=0:1:M-1;%ϸ��Ƶ������
f0=(f2-f1)/M*h+100;%ϸ����Ƶ��ֵ
figure;
subplot(211);stem(f0,abs(xk));
xlabel('f');
ylabel('value');
title('����CZT�任���ϸ��Ƶ��');
subplot(212);plot(f0,abs(xk));
xlabel('f');
ylabel('value');
title('����CZT�任���ϸ��Ƶ��');