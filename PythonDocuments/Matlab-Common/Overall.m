function ret = Overall( timeWave, overallLength )
% ���׼�����Чֵ

% ����˫����
ampSpectrum = DualFFT( timeWave, length( timeWave ) );

powerSpectrum = power( ampSpectrum, 2 );

DrawPlot( powerSpectrum );

ret = sqrt( 2 * sum( powerSpectrum( 1 : overallLength ) ) );
