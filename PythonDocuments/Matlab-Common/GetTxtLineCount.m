function lineCount = GetTxtLineCount( path )
% ���ļ��ļ���������Ч����

fid = fopen( path );
while 1
    tline = fgetl(fid);
    if ~ischar(tline),   break,   end
    disp(tline)
end
fclose(fid);