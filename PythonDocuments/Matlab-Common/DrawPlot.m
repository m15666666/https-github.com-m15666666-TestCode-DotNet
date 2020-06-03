function dummy = DrawPlot( yWave, graphTitle, xAxisLabel, yAxislabel )
% »­²¨ÐÎÇúÏß
handle = figure();

subplot( 1, 1, 1 );
xWave = 1 : length( yWave );
plot( xWave, yWave );


% xlabel( xAxisLabel );
% ylabel( yAxislabel );
% title( graphTitle );
grid;

dummy = 0;