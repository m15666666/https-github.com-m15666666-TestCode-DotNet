function ret = AmpDegree2Complex( amp, degree )
% 将幅值和角度（度）转化为复数

rad = Degree2Rad( degree );
ret = complex( amp * cos( rad ), amp * sin( rad ) );