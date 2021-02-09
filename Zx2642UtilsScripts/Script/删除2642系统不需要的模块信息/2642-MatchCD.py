# -*- coding: gb2312 -*-
"""
Created on Thu May 24 10:16:24 2012

从一个文本文件中抽取需要的CD字符串

@author: 陈良
"""

import re
import os

sqlScriptPath = 'c:\\Users\\Administrator\\Desktop\\报警状态管理新增部分.txt'
print sqlScriptPath

cdPattern = "Insert Into BS_MainMenu\(MenuKey_TX,MenuKey_Ref,CustomMenuKey_Ref,Name_TX,CustomName_TX,ToolTip_TX,AppAction_CD,RedirectPageKey_TX,JsBlock_TX,SortNo_NR,Type_CD \) Values\('(.+?)'";
cdPattern = "Insert Into BS_AppAction\(AppAction_CD,AppAction_CD_Ref,Desc_TX,Name_TX,ActionType_CD,SysAction_YN,Level_NR,SortNo_NR,DataRange_YN,DataRangeList_TX,Visible_YN,InvokeObject_TX,SpecDataRange_YN,Flow_YN,Type_CD \) Values\('(.+?)'";

cds = []
try: 
    fobj = open(sqlScriptPath, 'r') 
except IOError as e: 
    print "*** file open error:", e 
else: 
    for eachLine in fobj:
        m = re.search(cdPattern, eachLine)
        if m is not None:
            cds.append( "'" + m.group(1) + "'" )
    fobj.close()
    
print len(cds)
output = "[" + ", ".join(cds) + "]"
print output

"""
targetPath = 'c:\\Users\\Administrator\\Desktop\\target.txt'
targetFile = open(targetPath, 'w') 
targetFile.write( output )
# targetFile.writelines(['%s%s' % (x, ls) for x in all]) 
targetFile.close()
"""

print 'DONE!'
