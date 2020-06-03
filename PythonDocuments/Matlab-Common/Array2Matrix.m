function ret = Array2Matrix( dimOneArray, rowCount )
% 将一维数组转为2维矩阵，元素排列按照列优先的原则

columnCount = floor( length( dimOneArray ) / rowCount );
ret = reshape( dimOneArray( 1 : rowCount * columnCount ), rowCount, columnCount );
