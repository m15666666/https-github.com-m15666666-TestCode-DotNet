function [] = DrawTrend( wave, title )
% ������

x = 1 : length( wave );

if( ~exist( 'title', 'var') )
    title = '����';
end

DrawWave( x, wave, title, '����', '��ֵ', [] );
