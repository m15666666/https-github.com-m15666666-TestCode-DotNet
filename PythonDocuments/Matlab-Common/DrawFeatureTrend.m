function [] = DrawFeatureTrend( timeWave, method, analysisLength )
% ��ָ�������

timeWaves = Array2Matrix( timeWave, analysisLength );

[ m, n ] = size( timeWaves );

feature = 1 : n;
for index = 1 : n
    feature( index ) = feval( method, timeWaves( :, index ) );
end

maxValue = max( feature );
minValue = min( feature );

DrawTrend( feature, ['����( ', method, ', ', num2str( analysisLength ), '��, ', '���ֵ:', num2str( maxValue ), ', ', '��Сֵ:', num2str( minValue ), ')'] );
% DrawTrend( feval( method, timeWaves ), ['����( ', method, ', ', num2str( analysisLength ), '�� )' ] );
