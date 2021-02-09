# -*- coding: gb2312 -*-
"""
Created on Thu May 24 10:16:24 2012

生成一个sql语句文件，用于删除2642系统不需要的模块信息。
因此输出的sql脚本必须在所有的InitModule.sql执行过后再执行。

@author: 陈良
"""

import re
import os

def cds2SQLArray(cds):
    """ 将cd的数组转换为sql语句中的数组 """
    return "(" + ", ".join( ["'%s'" % (x) for x in cds] ) + ")"

def generateSQL( tableName, cdName, typeCD, cds, outputSQLs ):
    """ 针对一张表生成SQL语句，
    1、先修改需保留记录的Type_CD字段为：typeCD + "~"；
    2、然后删除所有 Type_CD == typeCD；
    3、最后恢复之前修改过的Type_CD字段。
    """
    # 将需要保留的记录临时改为这个类型CD
    typeCDOfKeep = typeCD + "~"
    
    outputSQLs.append("\n\n-- 处理表：" + tableName)
    outputSQLs.append( "update " + tableName + " set Type_CD='" + typeCDOfKeep + "' where " + cdName + " in " + cds2SQLArray(cds) + " and Type_CD='" + typeCD + "';")
    
    outputSQLs.append( "delete from " + tableName + " where Type_CD='" + typeCD + "';")
    
    outputSQLs.append( "update " + tableName + " set Type_CD='" + typeCD + "' where Type_CD='" + typeCDOfKeep + "';")

# 需要处理的类型CD
typeCD = '2641'

# 需要保留的主菜单-MainMenuCD
mainMenuCDsOfKeep = ['ZTGLMM_Group', 'BJGLMM_Group', 'WarningDealWithMM', 'WarningQueryMM', 'WarningStatisticMM', 'QXGLMM_Group', 'MobjectBugInputMM', 'MobjectBugDealWithMM', 'MobjectBugAuditMM', 'MobjectBugImplementMM', 'MobjectBugYanShouMM', 'MobjectBugDelayActiveMM', 'MobjectBugQueryMM']

# 需要保留的AppActionCD
appActionCDsOfKeep = ['WarningDealWithMM', 'WarningDealWithMM_QUE', 'WarningDealWithMM_JDCL', 'WarningDealWithMM_WXCL', 'WarningDealWithMM_ZRQX', 'WarningDealWithMM_ZC', 'WarningDealWithMM_DEL', 'WarningDealWithMM_DTL', 'WarningDealWithMM_EXP', 'WarningDealWithMM_EXA', 'WarningDealWithMM_LST', 'WarningQueryMM', 'WarningQueryMM_QUE', 'WarningQueryMM_DTL', 'WarningQueryMM_EXP', 'WarningQueryMM_EXA', 'WarningQueryMM_LST', 'WarningStatisticMM', 'WarningStatisticMM_QUE', 'WarningStatisticMM_DTL', 'WarningStatisticMM_EXPORT', 'MobjectBugInputMM', 'MobjectBugInputMM_QUE', 'MobjectBugInputMM_ADD', 'MobjectBugInputMM_EDT', 'MobjectBugInputMM_DEL', 'MobjectBugInputMM_SUB', 'MobjectBugInputMM_DTL', 'MobjectBugInputMM_EXP', 'MobjectBugInputMM_EXA', 'MobjectBugInputMM_LST', 'MobjectBugDealWithMM', 'MobjectBugDealWithMM_QUE', 'MobjectBugDealWithMM_CXZP', 'MobjectBugDealWithMM_WB', 'MobjectBugDealWithMM_JDCL', 'MobjectBugDealWithMM_SQDJ', 'MobjectBugDealWithMM_SQJX', 'MobjectBugDealWithMM_ZRGZ', 'MobjectBugDealWithMM_SQYQCL', 'MobjectBugDealWithMM_DTL', 'MobjectBugDealWithMM_EXP', 'MobjectBugDealWithMM_EXA', 'MobjectBugDealWithMM_LST', 'MobjectBugAuditMM', 'MobjectBugAuditMM_QUE', 'MobjectBugAuditMM_AUD', 'MobjectBugAuditMM_MAUD', 'MobjectBugAuditMM_DTL', 'MobjectBugAuditMM_EXP', 'MobjectBugAuditMM_EXA', 'MobjectBugAuditMM_LST', 'MobjectBugImplementMM', 'MobjectBugImplementMM_QUE', 'MobjectBugImplementMM_IMPL', 'MobjectBugImplementMM_BACK', 'MobjectBugImplementMM_SQYQSS', 'MobjectBugImplementMM_DTL', 'MobjectBugImplementMM_EXP', 'MobjectBugImplementMM_EXA', 'MobjectBugImplementMM_LST', 'MobjectBugYanShouMM', 'MobjectBugYanShouMM_QUE', 'MobjectBugYanShouMM_YS', 'MobjectBugYanShouMM_MYS', 'MobjectBugYanShouMM_DTL', 'MobjectBugYanShouMM_EXP', 'MobjectBugYanShouMM_EXA', 'MobjectBugYanShouMM_LST', 'MobjectBugDelayActiveMM', 'MobjectBugDelayActiveMM_QUE', 'MobjectBugDelayActiveMM_ACT', 'MobjectBugDelayActiveMM_DTL', 'MobjectBugDelayActiveMM_EXP', 'MobjectBugDelayActiveMM_EXA', 'MobjectBugDelayActiveMM_LST', 'MobjectBugQueryMM', 'MobjectBugQueryMM_QUE', 'MobjectBugQueryMM_DTL', 'MobjectBugQueryMM_ANA', 'MobjectBugQueryMM_EXP', 'MobjectBugQueryMM_EXA', 'MobjectBugQueryMM_LST']

outputSQLs = []
outputSQLs.append("""-- 用于删除2642系统不需要的模块信息。因此输出的sql脚本必须在所有的InitModule.sql执行过后再执行。""")

generateSQL('BS_MainMenu', 'MenuKey_TX', typeCD, mainMenuCDsOfKeep, outputSQLs)
generateSQL('BS_AppAction', 'AppAction_CD', typeCD, appActionCDsOfKeep, outputSQLs)
print outputSQLs

targetPath = 'c:\\Users\\Administrator\\Desktop\\target.txt'
targetFile = open(targetPath, 'w') 

targetFile.writelines( ["%s\n" % (sql) for sql in outputSQLs] )

targetFile.close()

print 'DONE!'
