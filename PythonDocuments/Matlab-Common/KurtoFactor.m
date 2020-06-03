function ret = KurtoFactor( timeWave )
% «Õ∂»÷∏±Í

ret = Div( Moment( timeWave, 4 ), power( std( timeWave ), 4 ) );
