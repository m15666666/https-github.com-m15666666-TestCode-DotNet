function ret = Overall( timeWave, overallLength )
% 从谱计算有效值

% 计算双边谱
ampSpectrum = DualFFT( timeWave, length( timeWave ) );

powerSpectrum = power( ampSpectrum, 2 );

DrawPlot( powerSpectrum );

ret = sqrt( 2 * sum( powerSpectrum( 1 : overallLength ) ) );
