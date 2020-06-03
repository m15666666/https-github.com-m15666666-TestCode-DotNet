function DTs = CreateDTs( fs, dtLength )
% 创建delta t的数组
DTs = ( 0 : dtLength - 1 ) / fs;