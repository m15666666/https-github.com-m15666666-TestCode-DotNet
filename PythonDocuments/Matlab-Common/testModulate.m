%testModulate
% ���Ե��ƺ͹�����

samples = 1024;
fs = 2560;
f0 = 10;
%ts = 1;
fc = 50;
t = CreateDTs( fs, samples );
x = sin( 2 * pi * f0 * t);
y = modulate( x, fc, fs,'am');

% DrawTimeWave( x, fs );
% DrawTimeWave( y, fs );
% DrawSingleFFT( y, fs );
% DrawTimeWave( HilbertEnvelop( y ), fs );
% DrawTimeWave( demod( y, fc, fs, 'am') , fs );

DrawSingleFFT( x, fs );
DrawSingleFFT( demod( y, fc, fs, 'am') , fs );

%DrawSingleFFT( HilbertEnvelop( y ), fs );
%demod(y,fc,fs,'method') 

% t = (0:1/1023:1);
% x = sin(2*pi*60*t);
% y = hilbert(x);
% plot(t(1:50),real(y(1:50))), hold on
% plot(t(1:50),imag(y(1:50)),':')
% hold off

% clear all
% 
% %�������������
% 
% x=[0 1 1 0 0 1 0 1 0 1];%����һ��������10Ԫ�����飬��ΪFSK�ź��������ݡ�
% figure(1);
% stem(x,'.');
% title('�������������');xlabel('ʱ��');ylabel('����');
% 
% %FSK�źŵĵ���
% 
% f0=1000;f1=2000;fs=8000;ts=1/125;%0��ӦƵ��f0��1��ӦƵ��f1������Ƶ��fs����Ԫ����125���ء�
% tt=(0:1/fs:ts); 
% t=[tt;tt+ts;tt+2*ts;tt+3*ts;tt+4*ts;tt+5*ts;tt+6*ts;tt+7*ts;tt+8*ts;tt+9*ts;]; 
% y=zeros(10,length(tt)); 
% i=1; 
% %��ʼ���� 
% while i<=10 
% y(i,:)=x(i)*cos(2*pi*f1*t(i,:))+~x(i)*cos(2*pi*f0*t(i,:)); 
% i=i+1; 
% end 
% t=reshape(t',length(tt)*10,1);%������������ 
% y=reshape(y',length(tt)*10,1); 
% figure(2); 
% plot(t,y); 
% title('FSK�ź�ʱ����');xlabel('ʱ��');ylabel('����'); 