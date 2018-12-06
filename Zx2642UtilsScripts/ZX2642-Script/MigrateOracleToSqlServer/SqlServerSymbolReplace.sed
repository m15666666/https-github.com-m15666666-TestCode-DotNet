#! /bin/sed -f
#��Oracle�ķ����滻ΪSqlServer�еķ���
# ��:=�滻Ϊ=�����set�ؼ���
s/\(\<P_[a-z_]\{1,\}\>\)[ \t]*:=/set \1 = /Ig
s/\(\<V_[a-z_]\{1,\}\>\)[ \t]*:=/set \1 = /Ig
# �������������declare�ؼ���
/\<declare\>/!s/\(\<V_[a-z_]\{1,\}\>\)[ \t]\{1,\}\(varchar\|int\>\|integer\|number\|t_array\|Date\|char\)/declare \1 \2/Ig
/\<declare\>/!s/\(\<V_[a-z_]\{1,\}\>\)[ \t]\{1,\}\(\<[a-z_.]\{1,\}\>\)%type/declare \1 \2%type/Ig
# ɾ�����е�declare
/^[ \t]*declare[ \t]*$/Id
# varchar2�滻Ϊvarchar
s/\<varchar2\>/varchar/Ig
# integer�滻Ϊint
s/\<integer\>/int/Ig
# Date�滻ΪDateTime
s/\<Date\>/DateTime/Ig
# �滻����ǰ׺��P_��
s/\<P_/@P_/Ig
# ���������е�P_�滻��ȥ
s/'@P_/'P_/Ig
# �滻����ǰ׺��V_��
/view\|join/!s/\<V_/@V_/Ig
/@/!s/||[ \t]*\<V_/|| @V_/Ig
/@/!s/\(\<V_[a-z_]*\)[ \t]*||/@\1 ||/Ig
s/@V_Cursor/V_Cursor/Ig
s/\<from\>[ \t]*@\(\<V_[a-z_z]*\>\)/from \1/Ig
s/'@V_/'V_/Ig
# �滻Execute Immediate����
s/\<Execute[ \t]*Immediate\>[ \t]*\(@[a-z_]\{1,\}\)[ \t]*\<into\>[ \t]*\(@[a-z_]\{1,\}\)/exec ('declare _ScalarCuror cursor global for ' + \1);open _ScalarCuror; fetch next from _ScalarCuror into \2; close _ScalarCuror; deallocate _ScalarCuror/Ig
s/\<Execute[ \t]*Immediate\>[ \t]*\(@[a-z_]\{1,\}\)/exec (\1)/Ig
#�滻||���ַ������ӷ���
s/'||||'/__StringConnector__/g
s/||/+/g
s/__StringConnector__/'||||'/g
#��ֵ����
s/:=/=/g
