function [output1, output2] = FunctionDemo(input1,input2)
% 函数例子程序
% 参考：http://zhidao.baidu.com/question/69278332.html
% 在Matlab下输入：edit，然后将下面两行百分号之间的内容，复制进去，保存 
% %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% 
% function [sinx,cosx]=myfun_1(x)
% sinx=sin(x);
% cosx=cos(x);
% %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% 
% 
% 返回Matlab输入:
% x=0:1:2*pi;
% [sx,cs]=myfun_1(x)

output1 = input1 + 1;
output2 = input2 + 2;