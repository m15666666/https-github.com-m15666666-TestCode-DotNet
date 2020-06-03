function [ sampleTime, dataLength, fs, rev, timeWave ] = ReadRetrospectFile( path )
% 读追忆文件中的数据

fid = fopen( path );

sampleTime = fread( fid, 14, '*char' );

dataLength = fread( fid, 1, '*int32' );

freqBand = fread( fid, 1, '*double' );

fs = freqBand * 2.56;

rev = fread( fid, 1, '*int32' );

waveScale = fread( fid, 1, '*double' );

% 循环读取，很慢
% timeWave = zeros(1,dataLength);
% for index = 1 : dataLength
%     timeWave( index ) = fread( fid, 1, '*int16' ) * waveScale;
% end

% 一次读取，很快，注意'int16=>double'
timeWave = fread( fid, double(dataLength), 'int16=>double' ) * waveScale;

fclose( fid );