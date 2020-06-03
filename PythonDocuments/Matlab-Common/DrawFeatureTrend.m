function [] = DrawFeatureTrend( timeWave, method, analysisLength )
% 画指标的趋势

timeWaves = Array2Matrix( timeWave, analysisLength );

[ m, n ] = size( timeWaves );

feature = 1 : n;
for index = 1 : n
    feature( index ) = feval( method, timeWaves( :, index ) );
end

maxValue = max( feature );
minValue = min( feature );

DrawTrend( feature, ['趋势( ', method, ', ', num2str( analysisLength ), '点, ', '最大值:', num2str( maxValue ), ', ', '最小值:', num2str( minValue ), ')'] );
% DrawTrend( feval( method, timeWaves ), ['趋势( ', method, ', ', num2str( analysisLength ), '点 )' ] );
