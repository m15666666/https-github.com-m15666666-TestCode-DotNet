#! /bin/sed -f
#将Oracle的符号替换为SqlServer中的符号
# 将:=替换为=并添加set关键字
s/\(\<P_[a-z_]\{1,\}\>\)[ \t]*:=/set \1 = /Ig
s/\(\<V_[a-z_]\{1,\}\>\)[ \t]*:=/set \1 = /Ig
# 添加声明变量的declare关键字
/\<declare\>/!s/\(\<V_[a-z_]\{1,\}\>\)[ \t]\{1,\}\(varchar\|int\>\|integer\|number\|t_array\|Date\|char\)/declare \1 \2/Ig
/\<declare\>/!s/\(\<V_[a-z_]\{1,\}\>\)[ \t]\{1,\}\(\<[a-z_.]\{1,\}\>\)%type/declare \1 \2%type/Ig
# 删除单行的declare
/^[ \t]*declare[ \t]*$/Id
# varchar2替换为varchar
s/\<varchar2\>/varchar/Ig
# integer替换为int
s/\<integer\>/int/Ig
# Date替换为DateTime
s/\<Date\>/DateTime/Ig
# 替换参数前缀（P_）
s/\<P_/@P_/Ig
# 将单引号中的P_替换回去
s/'@P_/'P_/Ig
# 替换变量前缀（V_）
/view\|join/!s/\<V_/@V_/Ig
/@/!s/||[ \t]*\<V_/|| @V_/Ig
/@/!s/\(\<V_[a-z_]*\)[ \t]*||/@\1 ||/Ig
s/@V_Cursor/V_Cursor/Ig
s/\<from\>[ \t]*@\(\<V_[a-z_z]*\>\)/from \1/Ig
s/'@V_/'V_/Ig
# 替换Execute Immediate命令
s/\<Execute[ \t]*Immediate\>[ \t]*\(@[a-z_]\{1,\}\)[ \t]*\<into\>[ \t]*\(@[a-z_]\{1,\}\)/exec ('declare _ScalarCuror cursor global for ' + \1);open _ScalarCuror; fetch next from _ScalarCuror into \2; close _ScalarCuror; deallocate _ScalarCuror/Ig
s/\<Execute[ \t]*Immediate\>[ \t]*\(@[a-z_]\{1,\}\)/exec (\1)/Ig
#替换||（字符串连接符）
s/'||||'/__StringConnector__/g
s/||/+/g
s/__StringConnector__/'||||'/g
#赋值符号
s/:=/=/g
