function ret = SkewFactor( timeWave )
% Õ·∂»÷∏±Í

ret = Div( Moment( timeWave, 3 ), power( std( timeWave ), 3 ) );
