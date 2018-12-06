#!/bin/bash
#
# 处理初始化数据的脚本
#
cat $1 | sed -f migrateFieldTypeToSqlServer.sed | sed -f migrateInitDataScript.sed | sed -f primaryFunctionReplace.sed | sed -f SqlServerSymbolReplace.sed 
