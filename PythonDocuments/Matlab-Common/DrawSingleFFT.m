function [] = DrawSingleFFT( timeWave, fs )
% ��������
timeWave = GetPow2Wave( timeWave );
spectrum = SingleFFT( timeWave, length( timeWave ) );
dfs = CreateDFs( length( timeWave ), fs, length( spectrum ) );%���ж�Ӧ��Ƶ��ת��

DrawWave( dfs, spectrum, '������', 'Ƶ��', '��ֵ', [] );