warning('off','MATLAB:dispatcher:InexactCaseMatch');
% testBalance2 单面动平衡计算scratch

% 试验11
% vb7，使用公司转子台，WR(加重角度与转动方向相同)，单面动平衡试验，去试重
% 初始振动：0.76 mm/s , 311度
% 
% 试重：1.6g, 45度
% 试重振动：1.28 mm/s , 270度
% 给出的矫正质量和角度：1.45g, 147度

% Xa0 = AmpDegree2Complex( 0.78, 360 - 311);
% Xa1 = AmpDegree2Complex( 1.28, 360 - 270 );
% U1 = AmpDegree2Complex( 1.6, 45 );
% a1 = ( Xa1 - Xa0 ) / U1;
% U0 = Xa0 / a1;
% [amp, degree] = Complex2AmpDegree( -U0 ) %#ok<NOPTS>

% vb7，使用公司转子台，WR(加重角度与转动方向相同)，单面动平衡试验，不去试重
% 初始振动：0.76 mm/s , 326度
% 
% 试重：1.6g, 0度
% 试重振动：1.28 mm/s , 270度
% 给出的矫正质量和角度：1.99g, 143度

Xa0 = AmpDegree2Complex( 0.76, 360 - 326);
Xa1 = AmpDegree2Complex( 1.28, 360 - 270 );
U1 = AmpDegree2Complex( 1.6, 0 );

a1 = ( Xa1 - Xa0 ) / U1;
U0 = Xa0 / a1;

[amp, degree] = Complex2AmpDegree( -U0 - U1 ) %#ok<NOPTS>