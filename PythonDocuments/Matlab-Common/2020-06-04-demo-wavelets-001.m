
% https://blog.csdn.net/cqfdcw/article/details/84995904
% С����С������С�����ֽ����ź��ع���С��������������ȡ �� С�����ֽ��ʵ�ְ�Ƶ�ʴ�С�ֲ���������(Matlab ������⣩
clear all  
clc
fs=1024;  %����Ƶ��
f1=100;   %�źŵĵ�һ��Ƶ��
f2=300;   %�źŵڶ���Ƶ��
t=0:1/fs:1;
s=sin(2*pi*f1*t)+sin(2*pi*f2*t);  %���ɻ���ź�
[tt]=wpdec(s,3,'dmey');  %С�����ֽ⣬3����ֽ�3�㣬'dmey'ʹ��meyrС��
plot(tt)               %��С������ͼ
wpviewcf(tt,1);        %����ʱ��Ƶ��ͼ

wave=s;
t1=wpdec(wave,3,'dmey');
plot(t1);
t2 = wpjoin(t1,[3;4;5;6]);