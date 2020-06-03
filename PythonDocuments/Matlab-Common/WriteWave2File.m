function [] = WriteWave2File( wave, path )
% 将波形数组写入文本文件

fid = fopen( path, 'wt' );

fprintf( fid, '%f\n', wave );

% fprintf( fid, '%6.4f\n', wave );

fclose( fid );