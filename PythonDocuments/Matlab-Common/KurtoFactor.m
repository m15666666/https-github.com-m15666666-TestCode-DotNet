function ret = KurtoFactor( timeWave )
% �Ͷ�ָ��

ret = Div( Moment( timeWave, 4 ), power( std( timeWave ), 4 ) );
