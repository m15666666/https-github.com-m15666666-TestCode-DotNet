function [amp,degree] = Complex2AmpDegree( complex )
% 将复数转化为幅值和角度（度）

amp = abs( complex );
degree = Rad2Degree( angle( complex ) );