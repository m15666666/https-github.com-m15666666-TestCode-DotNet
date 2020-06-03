warning('off','MATLAB:dispatcher:InexactCaseMatch');
% testCSharpCorrelation ����c#��д������㷨


row = 6;
column = 2;
index = 1;

xArray = ReadTxtWave('e:\temp\xArray.txt');
yArray = ReadTxtWave('e:\temp\yArray.txt');

rxy = ReadTxtWave('e:\temp\rxy.txt');
tau = ReadTxtWave('e:\temp\tau.txt');
rxyAmpSpectrum = ReadTxtWave('e:\temp\rxyAmpSpectrum.txt');
rxyPhaseSpectrum = ReadTxtWave('e:\temp\rxyPhaseSpectrum.txt');
rxxAmpSpectrum = ReadTxtWave('e:\temp\rxxAmpSpectrum.txt');
rxxPhaseSpectrum = ReadTxtWave('e:\temp\rxxPhaseSpectrum.txt');
coherence = ReadTxtWave('e:\temp\coherence.txt');
freqResponseAmpSpectrum = ReadTxtWave('e:\temp\freqResponseAmpSpectrum.txt');
freqResponsePhaseSpectrum = ReadTxtWave('e:\temp\freqResponsePhaseSpectrum.txt');

fs = 2560;
dfs = CreateDFs(length(tau) + 1, fs, length(rxyAmpSpectrum));

subplot(row,column,index);index = index + 1;
plot(xArray);
title('x data');

subplot(row,column,index);index = index + 1;
plot(yArray);
title('y data');

subplot(row,column,index);index = index + 1;
plot(tau,rxy);
title('Rxy');

subplot(row,column,index);index = index + 1;
plot(coherence);
title('���');

subplot(row,column,index);index = index + 1;
plot(rxyAmpSpectrum);
title('Rxy amp spectrum');

subplot(row,column,index);index = index + 1;
plot(rxyPhaseSpectrum);
title('Rxy phase spectrum');

subplot(row,column,index);index = index + 1;
plot(rxxAmpSpectrum);
title('Rxx amp spectrum');

subplot(row,column,index);index = index + 1;
plot(rxxPhaseSpectrum);
title('Rxx phase spectrum');

subplot(row,column,index);index = index + 1;
plot(freqResponseAmpSpectrum);
title('��Ƶ����');

subplot(row,column,index);index = index + 1;
plot(freqResponsePhaseSpectrum);
title('��Ƶ����');

Txy = tfestimate(xArray, yArray);
freqResponseAmpSpectrum = abs(Txy);
freqResponsePhaseSpectrum = Rad2Degree(angle(Txy));

%subplot(row,column,index);index = index + 1;
%plot(dfs,freqResponseAmp(1:length(dfs)));

%subplot(row,column,index);index = index + 1;
%plot(dfs,freqResponsePhase(1:length(dfs)));

subplot(row,column,index);index = index + 1;
plot(freqResponseAmpSpectrum);
title('��Ƶ����');

subplot(row,column,index);index = index + 1;
plot(freqResponsePhaseSpectrum);
title('��Ƶ����');
