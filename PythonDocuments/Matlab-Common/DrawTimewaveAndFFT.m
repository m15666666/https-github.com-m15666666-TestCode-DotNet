function [] = DrawTimewaveAndFFT( timeWave, fs )

timeWave = GetPow2Wave( timeWave );
spectrum = SingleFFT( timeWave, length( timeWave ) );
dfs = CreateDFs( length( timeWave ), fs, length( spectrum ) );%进行对应的频率转换
t = CreateDTs( fs, length( timeWave ) );

% 画波形曲线
handle = figure();

subplot( 2, 1, 1 );
plot( dfs, spectrum );

xlabel( '频率' );
ylabel( '幅值' );
title( '频谱' );
grid;

subplot( 2, 1, 2 );
plot( t, timeWave );

xlabel( 'ms' );
ylabel( '幅值' );
title( '时间波形' );
grid;

