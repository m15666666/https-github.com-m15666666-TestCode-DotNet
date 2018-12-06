#!/bin/bash
#
# 将所有的存储过程和函数向SqlServer移植。
#
. migrateProcedure.sh ../Oracle/51.CommonPackage.sql CommonPackage > ../SqlServer/51.CommonPackage.sql
. migrateProcedure.sh ../Oracle/52.XTGLPackage.sql XTGLPackage > ../SqlServer/52.XTGLPackage.sql
. migrateProcedure.sh ../Oracle/53.JCXXPackage.sql JCXXPackage >
../SqlServer/53.JCXXPackage.sql
. migrateProcedure.sh ../Oracle/54.PartitionPackage.sql PartitionPackage > ../SqlServer/54.PartitionPackage.sql
. migrateProcedure.sh ../Oracle/55.BGProcess.sql BGProcess > ../SqlServer/55.BGProcess.sql
. migrateProcedure.sh ../Oracle/56.CompressDataPackage.sql CompressDataPackage > ../SqlServer/56.CompressDataPackage.sql
. migrateProcedure.sh ../Oracle/58.HistoryDataPackage.sql HistoryDataPackage > ../SqlServer/58.HistoryDataPackage.sql
. migrateProcedure.sh ../Oracle/57.DataQueryPackage.sql DataQueryPackage > ../SqlServer/57.DataQueryPackage.sql
