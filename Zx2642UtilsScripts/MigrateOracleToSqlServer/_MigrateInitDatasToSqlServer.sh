#!/bin/bash
#
# 将所有的初始化数据脚本向SqlServer移植。
#
. migrateInitDataScript.sh ../Oracle/22.InitAppAction.sql > ../SqlServer/22.InitAppAction.sql
. migrateInitDataScript.sh ../Oracle/23.InitAppConfig.sql > ../SqlServer/23.InitAppConfig.sql
. migrateInitDataScript.sh ../Oracle/24.InitDDType.sql > ../SqlServer/24.InitDDType.sql
. migrateInitDataScript.sh ../Oracle/25.InitQueryViewConfig.sql > ../SqlServer/25.InitQueryViewConfig.sql
. migrateInitDataScript.sh ../Oracle/21.InitSysData.sql > ../SqlServer/21.InitSysData.sql
. migrateInitDataScript.sh ../Oracle/26.InitDBStructDesc.sql > ../SqlServer/26.InitDBStructDesc.sql
. migrateInitDataScript.sh ../Oracle/27.InitPromptMessage.sql > ../SqlServer/27.InitPromptMessage.sql
. migrateInitDataScript.sh ../Oracle/28.InitControlTitle.sql > ../SqlServer/28.InitControlTitle.sql
. migrateInitDataScript.sh ../Oracle/29.InitModule.sql > ../SqlServer/29.InitModule.sql
. migrateInitDataScript.sh ../Oracle/30.InitPartition.sql > ../SqlServer/30.InitPartition.sql
