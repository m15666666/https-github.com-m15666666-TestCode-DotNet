% AnalysisRetrospectFile( 'E:\��������\���ֵ�������\�и�����\���ٻ�1#����\Total\���ٻ�1#������ˮƽ��_6.dat' );
% testReadRetrospectFile
clc;clear;

path = 'E:\��������\���ֵ�������\�и�����\���ٻ�1#����\Total\���ٻ�1#������ˮƽ��_6.dat';
%path = 'E:\��������\���ֵ�������\�и�����\���ٻ�1#����\����\���ٻ�1#������ˮƽ��_����_1.dat';
% path = 'E:\��������\���ֵ�������\�и�����\���ٻ�1#����\Total\���ٻ�1#������ˮƽ��_1.dat';
% path = 'E:\��������\���ֵ�������\�и�����\23#������_3#4#�������ˮƽ��\Total\23#������3#4#�������ˮƽ��_1.dat';
% path = 'H:\������������\�и�����\F1���ٻ�\F1���ٻ�����������ഹֱ��\Total\F1���ٻ�����������ഹֱ��_1.dat';
% path = 'H:\F4���ֻ��������������ˮƽ��\Total\F4���ֻ��������������ˮƽ��_1.dat';
% path = 'H:\1700�и�����\F3���ٻ��������������ˮƽ��\Total\F3���ٻ��������������ˮƽ��_1.dat';
path = 'E:\lchen\PC_Setting\����\28000.dat';
path = 'E:\��������\���ֵ�������\�и�����\16#������_1#�������45����\Total\16#������1#�������45����_1.dat';
path = 'E:\lchen\PC_Setting\����\8k.dat';

[ sampleTime, dataLength, fs, rev, timeWave ] = ReadRetrospectFile( path );
DrawSingleFFT(timeWave, fs );
DrawTimeWave( timeWave, fs );
% DrawFeaturesTrend( timeWave, 8192 );
