function ret = Div( x, y )
% �����������( x / y )����ֹ��0��ʩ,���뼫Сֵ��

if y == 0
    y = 1e-6;
end

ret = x / y;