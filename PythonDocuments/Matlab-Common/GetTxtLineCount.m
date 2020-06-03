function lineCount = GetTxtLineCount( path )
% 从文件文件包含的有效行数

fid = fopen( path );
while 1
    tline = fgetl(fid);
    if ~ischar(tline),   break,   end
    disp(tline)
end
fclose(fid);