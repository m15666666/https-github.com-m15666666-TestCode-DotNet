function [] = DrawSingleFFT( timeWave, fs )
% 画单边谱
timeWave = GetPow2Wave( timeWave );
spectrum = SingleFFT( timeWave, length( timeWave ) );
dfs = CreateDFs( length( timeWave ), fs, length( spectrum ) );%进行对应的频率转换

DrawWave( dfs, spectrum, '单边谱', '频率', '幅值', [] );