function Xk = DFT( xn, N )
% Discrete Fourier Transform
% From：http://www.chinavib.com/forum/thread-71280-1-1.html
% From：http://www.ilovematlab.cn/viewthread.php?tid=57593

% 相位谱中提取初相位
% From：http://forum.eet-cn.com/FORUM_POST_10006_1200016196_0.HTM
% From：http://bbs.eccn.com/viewthread.php?tid=35231
% Matlab_FFT方法检测正弦波相位 : http://blog.chinaunix.net/u1/37798/showart_708940.html

n = 0:N-1;
k = n;
Wn = exp(-j*2*pi/N);
nk = n'*k;
Wnnk = Wn.^nk;
Xk = xn*Wnnk;