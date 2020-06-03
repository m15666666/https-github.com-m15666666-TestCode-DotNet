% AnalysisRetrospectFile( 'E:\导出数据\二钢导出数据\切割数据\增速机1#输入\Total\增速机1#输入轴水平振动_6.dat' );
% testReadRetrospectFile
clc;clear;

path = 'E:\导出数据\二钢导出数据\切割数据\增速机1#输入\Total\增速机1#输入轴水平振动_6.dat';
%path = 'E:\导出数据\二钢导出数据\切割数据\增速机1#输入\过钢\增速机1#输入轴水平振动_过钢_1.dat';
% path = 'E:\导出数据\二钢导出数据\切割数据\增速机1#输入\Total\增速机1#输入轴水平振动_1.dat';
% path = 'E:\导出数据\二钢导出数据\切割数据\23#齿轮箱_3#4#轴输出端水平振动\Total\23#齿轮箱3#4#轴输出端水平振动_1.dat';
% path = 'H:\冷轧导出数据\切割数据\F1减速机\F1减速机上输出轴电机侧垂直振动\Total\F1减速机上输出轴电机侧垂直振动_1.dat';
% path = 'H:\F4齿轮基座输出轴轧机侧水平振动\Total\F4齿轮基座输出轴轧机侧水平振动_1.dat';
% path = 'H:\1700切割数据\F3减速机下输出轴轧机侧水平振动\Total\F3减速机下输出轴轧机侧水平振动_1.dat';
path = 'E:\lchen\PC_Setting\桌面\28000.dat';
path = 'E:\导出数据\二钢导出数据\切割数据\16#齿轮箱_1#轴输出端45°振动\Total\16#齿轮箱1#轴输出端45°振动_1.dat';
path = 'E:\lchen\PC_Setting\桌面\8k.dat';

[ sampleTime, dataLength, fs, rev, timeWave ] = ReadRetrospectFile( path );
DrawSingleFFT(timeWave, fs );
DrawTimeWave( timeWave, fs );
% DrawFeaturesTrend( timeWave, 8192 );
