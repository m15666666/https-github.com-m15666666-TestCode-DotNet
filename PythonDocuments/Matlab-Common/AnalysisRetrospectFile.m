function [] = AnalysisRetrospectFile( path )
% ��׷���ļ��е����ݣ����򿪲��κ�Ƶ�״���

[ sampleTime, dataLength, fs, rev, timeWave ] = ReadRetrospectFile( path );
DrawSingleFFT(timeWave, fs );
DrawTimeWave( timeWave, fs );