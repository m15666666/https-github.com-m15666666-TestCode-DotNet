function ret = ShapeFactor( timeWave )
% ≤®–Œ÷∏±Í

ret = Div( RMS( timeWave ), AbsMean( timeWave ) );