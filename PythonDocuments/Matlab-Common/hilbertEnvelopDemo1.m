% hilbert对一个已知信号的包络分析例1
% http://zhidao.baidu.com/question/96253375.html?fr=qrl&cid=93&index=2

% 请问一下谁知道在matlab环境下怎么用hilbter实现对一个已知信号的包络分析，能给我一个简单的程序吗？ 
% 问题补充：
% 你给的网站看过了，还是不太清楚，
% 比如我有一个信号：
% Fs=1024;
% t=0:1/Fs:1;
% x=sin(16*pi*t)+5*sin(100*pi*t)+3*sin(260*pi*t+sin(80*pi*t))+10*sin(260*pi*t)+3*randn(size(t));
% 怎么求它的包络谱，再请你指教一下，因为以前没有做过，手边又没有可对比的资料所以我自己做了也不知道对不对，你能把程序写给我看一下吗？谢谢。
% 
% 提问者： snkhm - 试用期 二级
% 最佳答案
% 
% 这方面的内容忘得差不多了
% 
% 给你找一个程序 你看看吧：
% 
% close all
% % 
% fs=30;
% t=0:1/fs:200; 
% x6=sin(2*pi*2*t)+sin(2*pi*4*t);
% x66 = hilbert(x6);
% xx = abs(x66+j*x6);
% figure(1)
% hold on
% plot(t,x6);
% plot(t,xx,'r')
% xlim([0 5])
% hold off
% % 包络算法，未考虑边界条件
% d = diff(x6);
% n = length(d);
% d1 = d(1:n-1);
% d2 = d(2:n);
% indmin = find(d1.*d2<0 & d1<0)+1;
% indmax = find(d1.*d2<0 & d1>0)+1;
% envmin = spline(t(indmin),x6(indmin),t);
% envmax = spline(t(indmax),x6(indmax),t);
% figure
% hold on
% plot(t,x6);
% plot(t,envmin,'r');
% plot(t,envmax,'m');
% hold off
% xlim([0 5]) 

close all
% 
fs=30;
t=0:1/fs:200; 
x6=sin(2*pi*2*t)+sin(2*pi*4*t);
x66 = hilbert(x6);
xx = abs(x66+j*x6);
figure(1)
hold on
plot(t,x6);
plot(t,xx,'r')
xlim([0 5])
hold off

% 包络算法，未考虑边界条件
d = diff(x6);
n = length(d);
d1 = d(1:n-1);
d2 = d(2:n);
indmin = find(d1.*d2<0 & d1<0)+1;
indmax = find(d1.*d2<0 & d1>0)+1;
envmin = spline(t(indmin),x6(indmin),t);
envmax = spline(t(indmax),x6(indmax),t);
figure
hold on
plot(t,x6);
plot(t,envmin,'r');
plot(t,envmax,'m');
hold off
xlim([0 5]) 