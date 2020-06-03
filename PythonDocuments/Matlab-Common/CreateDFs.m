function DFs = CreateDFs( waveLength, fs, dfLength )
% 创建delta f的数组
DFs = ( 0 : dfLength - 1 )* fs / waveLength;