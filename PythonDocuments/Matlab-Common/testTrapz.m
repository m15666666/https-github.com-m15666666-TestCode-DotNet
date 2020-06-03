% testTrapz

clear;
testSinInput

dt = t(2) - t(1);

intRet = TrapzInt( sinWave, dt );
intRet = TrapzInt( intRet, dt );
% intRet = zeros(1, N);
% intRet(1) = 0; 
% for k = 1:N-1
% intRet(k+1) = trapz(t(k:k+1),sinWave(k:k+1))+intRet(k); 
% end

% sinWave = ACCoupling(sinWave);
% dt = t(2) - t(1);
% for k = [1:N-1]
% % intRet(k+1) = ( (sinWave(k) + sinWave(k+1))*(t(k+1)-t(k))/2 )+intRet(k); 
% intRet(k+1) = ( (sinWave(k) + sinWave(k+1))*dt/2 )+intRet(k); 
% end

%intRet = ACCoupling(intRet);

DrawTimeWave( intRet, fs );
DrawSingleFFT( intRet, fs );