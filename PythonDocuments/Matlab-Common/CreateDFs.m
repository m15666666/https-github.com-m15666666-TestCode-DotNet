function DFs = CreateDFs( waveLength, fs, dfLength )
% ����delta f������
DFs = ( 0 : dfLength - 1 )* fs / waveLength;