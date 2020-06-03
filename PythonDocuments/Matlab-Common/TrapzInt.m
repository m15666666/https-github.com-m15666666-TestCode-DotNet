function intWave = TrapzInt( timeWave, dt )
% ʹ��matlab��trapz�������л���

N = length( timeWave );
timeWave = ACCoupling(timeWave);
dts = [0, dt];

intWave = zeros(1, N);
intWave(1) = 0; 
for k = 1:N-1
    intWave(k+1) = trapz( dts, timeWave( k : k+1 ) ) + intWave(k); 
end

intWave = ACCoupling( intWave );