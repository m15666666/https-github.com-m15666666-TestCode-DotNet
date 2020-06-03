function [] = AnalysisRetrospectFile( path )
% 读追忆文件中的数据，并打开波形和频谱窗口

[ sampleTime, dataLength, fs, rev, timeWave ] = ReadRetrospectFile( path );
DrawSingleFFT(timeWave, fs );
DrawTimeWave( timeWave, fs );