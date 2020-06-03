function Y = GetLinearY( x1, y1, x2, y2, x )
% 获得直线上对应X的Y值

dX = (x2 - x1);
if dX == 0
    Y = y1;
    return;
end

Y = (y2 - y1) * ( x - x1 ) / dX + y1;