function [] = WriteWave2MultiFiles( wave, dirpath, fileWaveLength )
% 将波形数组写入文本文件
[~ ,totalLength] = size(wave);
beginIndex = 1;
endIndex = fileWaveLength;
digitCount = floor(log10(floor(totalLength / fileWaveLength))) + 1;
fileIndex = 1;
while fileWaveLength <= totalLength
	totalLength = totalLength - fileWaveLength;
	
	fileName = num2str( fileIndex );
	for index = 1 : (digitCount - length(fileName) )
		fileName = ['0', fileName];
	end;
	
	WriteWave2File( wave(beginIndex : endIndex), [Path_AppendSlash(dirpath), fileName, '.txt']);
	
	fileIndex = fileIndex + 1;
	beginIndex = beginIndex + fileWaveLength;
	endIndex = endIndex + fileWaveLength;
end; 
