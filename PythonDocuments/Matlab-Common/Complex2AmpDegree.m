function [amp,degree] = Complex2AmpDegree( complex )
% ������ת��Ϊ��ֵ�ͽǶȣ��ȣ�

amp = abs( complex );
degree = Rad2Degree( angle( complex ) );