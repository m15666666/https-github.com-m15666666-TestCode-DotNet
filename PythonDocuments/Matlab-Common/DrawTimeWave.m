function [] = DrawTimeWave( timeWave, fs )
% 画时间波形

t = CreateDTs( fs, length( timeWave ) );

DrawWave( t, timeWave, '时间波形', '毫秒', '幅值', [] );
