function ret = ClearanceFactor( timeWave )
% ‘£∂»÷∏±Í

ret = Div( AbsMax( timeWave ), SMR( timeWave ) );
