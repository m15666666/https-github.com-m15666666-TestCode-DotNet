#!/bin/bash
#
# 处理视图脚本
#

#sed 's/\<create\>[ \t]\{1,\}\<or\>[ \t]\{1,\}\<replace\>[ \t]\{1,\}\<view\>/create view/g' $1
cat $1 | sed -f migrateView.sed | sed -f primaryFunctionReplace.sed | sed -f SqlServerFunctionReplace.sed | sed -f SqlServerSymbolReplace.sed
#cat $1 | sed -f 1.sed
