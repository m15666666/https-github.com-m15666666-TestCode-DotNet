function ret = SkewFactor( timeWave )
% ���ָ��

ret = Div( Moment( timeWave, 3 ), power( std( timeWave ), 3 ) );
