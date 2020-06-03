warning('off','MATLAB:dispatcher:InexactCaseMatch');
% testPowerSpectralDensity ���Թ������ܶȺ���

% 1��ֱ�ӷ��ֳ�����ͼ�������ǰ��������x(n)��N���۲�������Ϊһ�������޵����У�ֱ�Ӽ���x(n)����ɢ����Ҷ�任����X(k)��Ȼ����ȡ���ֵ��ƽ
% ����������N����Ϊ����x(n)��ʵ�����׵Ĺ��ơ� 
% clear;
% Fs=1000; %����Ƶ��
% n=0:1/Fs:1;
% 
% %������������������
% xn=cos(2*pi*40*n)+3*cos(2*pi*100*n)+randn(size(n));
% 
% subplot(3,1,1);
% plot(xn); 
% 
% window=boxcar(length(xn)); %���δ�
% nfft=1024;
% [Pxx,f]=periodogram(xn,window,nfft,Fs); %ֱ�ӷ�
% 
% subplot(3,1,2);
% plot(f,Pxx); 
% 
% subplot(3,1,3);
% plot(f,10*log10(Pxx)); 


% 2����ӷ���
% ��ӷ���������x(n)���Ƴ�����غ���R(n)��Ȼ���R(n)���и���Ҷ�任����õ�x(n)�Ĺ����׹��ơ� 
% clear;
% Fs=1000; %����Ƶ��
% n=0:1/Fs:1;
% 
% %������������������
% xn=cos(2*pi*40*n)+3*cos(2*pi*100*n)+randn(size(n));
% 
% subplot(3,1,1);
% plot(xn); 
% 
% nfft=1024;
% cxn=xcorr(xn,'unbiased'); %�������е�����غ���
% CXk=fft(cxn,nfft);
% Pxx=abs(CXk);
% index=0:round(nfft/2-1);
% Pxx = Pxx(index+1)/nfft;
% k=index*Fs/nfft;
% %plot_Pxx=10*log10(Pxx(index+1));
% plot_Pxx=10*log10(Pxx);
% 
% subplot(3,1,2);
% plot(k,Pxx); 
% 
% subplot(3,1,3);
% plot(k,plot_Pxx); 



% 3��Bartlett�� 
% clear;
% Fs=1000;
% n=0:1/Fs:1;
% xn=cos(2*pi*40*n)+3*cos(2*pi*100*n)+randn(size(n));
% nfft=1024;
% window=boxcar(length(n)); %���δ�
% noverlap=0; %�������ص�
% p=0.9; %���Ÿ���
% 
% [Pxx,Pxxc]=psd(xn,nfft,Fs,window,noverlap,p);
% 
% index=0:round(nfft/2-1);
% k=index*Fs/nfft;
% plot_Pxx=10*log10(Pxx(index+1));
% plot_Pxxc=10*log10(Pxxc(index+1));
% figure(1)
% plot(k,plot_Pxx);
% 
% pause;
% 
% figure(2)
% plot(k,[plot_Pxx plot_Pxx-plot_Pxxc plot_Pxx+plot_Pxxc]); 


% 4��Welch�� 
clear;
Fs=1000;
n=0:1/Fs:1;
xn=cos(2*pi*40*n)+3*cos(2*pi*100*n)+randn(size(n));
nfft=1024;
window=boxcar(100); %���δ�
window1=hamming(100); %������
window2=blackman(100); %blackman��
noverlap=20; %�������ص�
range='half'; %Ƶ�ʼ��Ϊ[0 Fs/2]��ֻ����һ���Ƶ��

[Pxx,f]=pwelch(xn,window,noverlap,nfft,Fs,range);
[Pxx1,f]=pwelch(xn,window1,noverlap,nfft,Fs,range);
[Pxx2,f]=pwelch(xn,window2,noverlap,nfft,Fs,range);

plot_Pxx=10*log10(Pxx);
plot_Pxx1=10*log10(Pxx1);
plot_Pxx2=10*log10(Pxx2);

figure(1)
plot(f,plot_Pxx);

pause;

figure(2)
plot(f,plot_Pxx1);

pause;

figure(3)
plot(f,plot_Pxx2);