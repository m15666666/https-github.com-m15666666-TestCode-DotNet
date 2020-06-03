function timeWave = ReadTxtWave( path )
% 从文件文件读出波形数据，每行是一个浮点数

timeWave = dlmread( path );