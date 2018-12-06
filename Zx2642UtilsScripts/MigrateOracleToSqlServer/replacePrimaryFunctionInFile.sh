#!/bin/bash
#
# 将oracle的内置函数替换为函数包中的函数
#

#cat $1 | sed -f primaryFunctionReplace.sed > $1
cat $1 | sed -f primaryFunctionReplace.sed > $1_
