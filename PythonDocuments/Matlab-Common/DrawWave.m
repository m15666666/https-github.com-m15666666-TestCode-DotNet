function dummy = DrawWave( xWave, yWave, graphTitle, xAxisLabel, yAxislabel, axisRange )
% »­²¨ÐÎÇúÏß
handle = figure();

subplot( 1, 1, 1 );
plot( xWave, yWave );

if 0 < length( axisRange )
    echo "axisRange"
    axis( axisRange );
else
    echo "auto le"
    axis auto;
end

xlabel( xAxisLabel );
ylabel( yAxislabel );
title( graphTitle );
grid;

dummy = 0;