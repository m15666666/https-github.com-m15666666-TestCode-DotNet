function [] = DrawTimewaveAndFFT( timeWave, fs )

timeWave = GetPow2Wave( timeWave );
spectrum = SingleFFT( timeWave, length( timeWave ) );
dfs = CreateDFs( length( timeWave ), fs, length( spectrum ) );%���ж�Ӧ��Ƶ��ת��
t = CreateDTs( fs, length( timeWave ) );

% ����������
handle = figure();

subplot( 2, 1, 1 );
plot( dfs, spectrum );

xlabel( 'Ƶ��' );
ylabel( '��ֵ' );
title( 'Ƶ��' );
grid;

subplot( 2, 1, 2 );
plot( t, timeWave );

xlabel( 'ms' );
ylabel( '��ֵ' );
title( 'ʱ�䲨��' );
grid;

