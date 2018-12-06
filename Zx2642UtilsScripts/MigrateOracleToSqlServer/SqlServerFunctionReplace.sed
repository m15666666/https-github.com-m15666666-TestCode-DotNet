#! /bin/sed -f
#将Oracle的函数替换为SqlServer中的函数
#替换nvl函数
s/\<nvl\>/isnull/Ig
