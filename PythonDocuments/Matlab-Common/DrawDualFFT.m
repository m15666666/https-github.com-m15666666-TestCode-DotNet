function [] = DrawDualFFT( timeWave, fs )
% ��˫����
timeWave = GetPow2Wave( timeWave );
spectrum = DualFFT( timeWave, length( timeWave ) );
dfs = CreateDFs( length( timeWave ), fs, length( spectrum ) );%���ж�Ӧ��Ƶ��ת��

DrawWave( dfs, spectrum, '˫����', 'Ƶ��', '��ֵ', [] );