function [] = DrawTimeWave( timeWave, fs )
% ��ʱ�䲨��

t = CreateDTs( fs, length( timeWave ) );

DrawWave( t, timeWave, 'ʱ�䲨��', '����', '��ֵ', [] );
