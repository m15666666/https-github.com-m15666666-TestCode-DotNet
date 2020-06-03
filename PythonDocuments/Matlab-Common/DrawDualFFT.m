function [] = DrawDualFFT( timeWave, fs )
% 画双边谱
timeWave = GetPow2Wave( timeWave );
spectrum = DualFFT( timeWave, length( timeWave ) );
dfs = CreateDFs( length( timeWave ), fs, length( spectrum ) );%进行对应的频率转换

DrawWave( dfs, spectrum, '双边谱', '频率', '幅值', [] );