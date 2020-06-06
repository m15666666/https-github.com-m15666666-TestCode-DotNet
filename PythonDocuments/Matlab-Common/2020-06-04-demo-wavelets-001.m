
% https://blog.csdn.net/cqfdcw/article/details/84995904
% 小波与小波包、小波包分解与信号重构、小波包能量特征提取 暨 小波包分解后实现按频率大小分布重新排列(Matlab 程序详解）
clear all  
clc
fs=1024;  %采样频率
f1=100;   %信号的第一个频率
f2=300;   %信号第二个频率
t=0:1/fs:1;
s=sin(2*pi*f1*t)+sin(2*pi*f2*t);  %生成混合信号
[tt]=wpdec(s,3,'dmey');  %小波包分解，3代表分解3层，'dmey'使用meyr小波
plot(tt)               %画小波包树图
wpviewcf(tt,1);        %画出时间频率图

wave=s;
t1=wpdec(wave,3,'dmey');
plot(t1);
t2 = wpjoin(t1,[3;4;5;6]);
sNod = read(t1,'sizes',[3,4,5,6]);

cfs3  = zeros(sNod(1,:));
cfs4  = zeros(sNod(2,:));
cfs5  = zeros(sNod(3,:));
cfs6  = zeros(sNod(4,:));


t3 = write(t2,'cfs',3,cfs3,'cfs',4,cfs4,'cfs',5,cfs5,'cfs',6,cfs6);

wave2=wprec(t3);
