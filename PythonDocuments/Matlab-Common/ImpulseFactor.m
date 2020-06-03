function ret = ImpulseFactor( timeWave )
% Âö³åÖ¸±ê

ret = Div( AbsMax( timeWave ), AbsMean( timeWave ) );
