function [] = WriteWave2File( wave, path )
% ����������д���ı��ļ�

fid = fopen( path, 'wt' );

fprintf( fid, '%f\n', wave );

% fprintf( fid, '%6.4f\n', wave );

fclose( fid );