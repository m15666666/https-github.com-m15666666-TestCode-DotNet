#! /bin/sed -f
#将Oracle的函数替换为公共函数包中的函数

#替换decode函数
s/\<decode\>/DBDiffPackage.StringDecode/Ig

#替换to_number函数
s/\<to_number\>/DBDiffPackage.StringToInt64/Ig
#替换Sysdate函数
s/\<Sysdate\>/DBDiffPackage.GetSystemTime()/Ig
#替换substr函数
s/\<SUBSTR\>/DBDiffPackage.GetSubstring/Ig
#替换mod函数
s/\<mod\>/DBDiffPackage.GetMod/Ig
#替换Dbms_Output.Put_Line函数
s/Dbms_Output.Put_Line/DBDiffPackage.Pr_DebugPrint/Ig
#替换length函数
s/\<length\>/DBDiffPackage.GetLength/Ig
