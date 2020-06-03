function [ sampleTime, dataLength, fs, rev, timeWave ] = ReadRetrospectFile( path )
% ��׷���ļ��е�����

fid = fopen( path );

sampleTime = fread( fid, 14, '*char' );

dataLength = fread( fid, 1, '*int32' );

freqBand = fread( fid, 1, '*double' );

fs = freqBand * 2.56;

rev = fread( fid, 1, '*int32' );

waveScale = fread( fid, 1, '*double' );

% ѭ����ȡ������
% timeWave = zeros(1,dataLength);
% for index = 1 : dataLength
%     timeWave( index ) = fread( fid, 1, '*int16' ) * waveScale;
% end

% һ�ζ�ȡ���ܿ죬ע��'int16=>double'
timeWave = fread( fid, double(dataLength), 'int16=>double' ) * waveScale;

fclose( fid );