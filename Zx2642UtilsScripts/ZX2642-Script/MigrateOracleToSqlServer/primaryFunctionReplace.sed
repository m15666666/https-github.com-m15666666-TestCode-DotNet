#! /bin/sed -f
#��Oracle�ĺ����滻Ϊ�����������еĺ���

#�滻decode����
s/\<decode\>/DBDiffPackage.StringDecode/Ig

#�滻to_number����
s/\<to_number\>/DBDiffPackage.StringToInt64/Ig
#�滻Sysdate����
s/\<Sysdate\>/DBDiffPackage.GetSystemTime()/Ig
#�滻substr����
s/\<SUBSTR\>/DBDiffPackage.GetSubstring/Ig
#�滻mod����
s/\<mod\>/DBDiffPackage.GetMod/Ig
#�滻Dbms_Output.Put_Line����
s/Dbms_Output.Put_Line/DBDiffPackage.Pr_DebugPrint/Ig
#�滻length����
s/\<length\>/DBDiffPackage.GetLength/Ig
