
% MATLAB 包络线 例1
% From：http://it.wenda.sogou.com/question/32959517.html

% MATLAB 包络线
% 悬赏分：0 - 解决时间： 2009年05月28日 05时54分
% 怎么画y=2*exp（-0.5*x）.*sin(2*pi*x)的包络线
% 提问者： feelamei123 - 中级魔法师 六级  加为好友 加为好友
% 最佳答案
% >> x = 0:.01:5;
% >> y=2*exp(-0.5*x).*sin(2*pi*x);
% >> f1 = 2*exp(-0.5*x);
% >> f2 = -2*exp(-0.5*x);
% >> plot(x,y,x,f1,':r',x,f2,':r')


x = 0:.01:5;
y=2*exp(-0.5*x).*sin(2*pi*x);
f1 = 2*exp(-0.5*x);
f2 = -2*exp(-0.5*x);
plot(x,y,x,f1,':r',x,f2,':r')