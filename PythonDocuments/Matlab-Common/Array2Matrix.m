function ret = Array2Matrix( dimOneArray, rowCount )
% ��һά����תΪ2ά����Ԫ�����а��������ȵ�ԭ��

columnCount = floor( length( dimOneArray ) / rowCount );
ret = reshape( dimOneArray( 1 : rowCount * columnCount ), rowCount, columnCount );
