function ret = Rad2Degree( rad )
% ������ת��Ϊ��

ret = rad / pi * 180;
if ret < 0
    ret = ret + 360;
elseif 360 <= ret
    ret = ret - 360;
end
