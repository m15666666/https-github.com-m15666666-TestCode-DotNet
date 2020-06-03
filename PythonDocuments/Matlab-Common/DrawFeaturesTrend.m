function [] = DrawFeaturesTrend( timeWave, analysisLength )
% 画多指标的趋势

DrawFeatureTrend( timeWave, 'AbsMax', analysisLength );
DrawFeatureTrend( timeWave, 'AbsMean', analysisLength );
DrawFeatureTrend( timeWave, 'RMS', analysisLength );
DrawFeatureTrend( timeWave, 'SMR', analysisLength );
DrawFeatureTrend( timeWave, 'ShapeFactor', analysisLength );
DrawFeatureTrend( timeWave, 'ImpulseFactor', analysisLength );
DrawFeatureTrend( timeWave, 'CrestFactor', analysisLength );
DrawFeatureTrend( timeWave, 'ClearanceFactor', analysisLength );
DrawFeatureTrend( timeWave, 'SkewFactor', analysisLength );
DrawFeatureTrend( timeWave, 'KurtoFactor', analysisLength );
