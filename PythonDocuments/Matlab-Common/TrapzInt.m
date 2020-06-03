function intWave = TrapzInt( timeWave, dt )
% 使用matlab的trapz函数进行积分

N = length( timeWave );
timeWave = ACCoupling(timeWave);
dts = [0, dt];

intWave = zeros(1, N);
intWave(1) = 0; 
for k = 1:N-1
    intWave(k+1) = trapz( dts, timeWave( k : k+1 ) ) + intWave(k); 
end

intWave = ACCoupling( intWave );