#!/bin/bash
#
# 将oracle的内置函数替换为函数包中的函数
#

. replacePrimaryFunctionInFile.sh ../oracle/02.CommonPackage.sql 
. replacePrimaryFunctionInFile.sh ../oracle/04.CreateView.sql
. replacePrimaryFunctionInFile.sh ../oracle/06.InitAppConfig.sql
. replacePrimaryFunctionInFile.sh ../oracle/14.XTGLPackage.Sql
. replacePrimaryFunctionInFile.sh ../oracle/18.XSTPTable.sql
. replacePrimaryFunctionInFile.sh ../oracle/19.BGCycle.sql
. replacePrimaryFunctionInFile.sh ../oracle/20.PartitionPackage.sql
. replacePrimaryFunctionInFile.sh ../oracle/21.BGProcess.sql
. replacePrimaryFunctionInFile.sh ../oracle/22.CompressDataPackage.sql
. replacePrimaryFunctionInFile.sh ../oracle/23.BGCyclePackage.sql
. replacePrimaryFunctionInFile.sh ../oracle/26.BGProcessPackage.sql
. replacePrimaryFunctionInFile.sh ../oracle/28.JOB.Sql
. replacePrimaryFunctionInFile.sh ../oracle/31.InitDataQueryPackage.sql
. replacePrimaryFunctionInFile.sh ../oracle/数据库初始化脚本.sql

