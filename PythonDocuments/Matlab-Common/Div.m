function ret = Div( x, y )
% 将两个数相除( x / y )，防止除0措施,引入极小值。

if y == 0
    y = 1e-6;
end

ret = x / y;