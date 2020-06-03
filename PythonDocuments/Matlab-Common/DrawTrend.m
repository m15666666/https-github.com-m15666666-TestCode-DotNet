function [] = DrawTrend( wave, title )
% 画趋势

x = 1 : length( wave );

if( ~exist( 'title', 'var') )
    title = '趋势';
end

DrawWave( x, wave, title, '点数', '幅值', [] );
