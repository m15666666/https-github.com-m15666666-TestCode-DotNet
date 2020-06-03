warning('off','MATLAB:dispatcher:InexactCaseMatch');
% testCorrelation �������

% ����1
% dt=.1;
% t=[0:dt:100];
% x=cos(t);
% [a,b]=xcorr(x,'unbiased');
% plot(b*dt,a)

% % ����2
% dt=.1;
% t=[0:dt:100];
% x=3*cos(t);
% % y=cos(3*t);
% y=3*cos(t);
% 
% xLength = length(x);
% %�˵�ЧӦ����ʱ��������
% divLengths = [1:1:xLength xLength-1:-1:1];
% 
% % x,y�ı�׼��
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


% ����3
f0 = 1;%�趨�����ź�Ƶ��
fs = f0 * 16;%�趨����Ƶ��
N = 32;
t = CreateDTs( fs, N );
sinWave = 1 * sin( 2 * pi * f0 * t );

dt=1/fs;
x = sinWave;
y = sinWave;

xLength = length(x);
%�˵�ЧӦ����ʱ��������
divLengths = [1:1:xLength xLength-1:-1:1];

% x,y�ı�׼��
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
% ��ת������
yy=fliplr(y);

% ���о��
z=conv(x,yy) ./ divLengths / ( xStdDeviation * yStdDeviation );

% % pause;
% % subplot(3,1,3);
% % plot(b*dt,z,'r');