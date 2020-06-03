function pow2Number = GetPow2Number( inputNumber )
% 获得2的幂次长度的数值

retLength = 2;
while retLength < inputNumber
    retLength = retLength * 2;
end

if inputNumber < retLength
    retLength = retLength / 2;
end

pow2Number = retLength;