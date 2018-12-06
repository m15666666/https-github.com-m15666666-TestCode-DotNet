#! /bin/sed -f
#s/[ \t]\{1,\}//g
# 创建procedure
s/\<procedure\>[ \t]*\(\<[a-z_]\{1,\}\>\)/create procedure __Package__.\1/Ig
# 创建function
s/\<function\>[ \t]*\(\<[a-z_]\{1,\}\>\)/create function __Package__.\1/Ig
# 替换函数返回的类型
/;/!s/return[ \t]\{1,\}\(varchar\|int\|number\|Date\|[a-z_.]*%type\)/RETURNS \1/Ig
# 删除函数开头的begin
/^[ \t]*\<begin\>[ \t]*$/Id
# 在函数和存储过程结尾，end;后面换行加GO
s/^[ \t]*\<end\>[ \t]*;[ \t]*$/end;\nGO/Ig
# 在函数开头的as后面添加begin
s/\<as\>[ \t]*$/as begin/Ig
#替换if结构
s/\<then\>/begin/Ig
s/\<else\>/end else begin/Ig
s/\<elsif\>/end else if/Ig
s/\<end[ \t]*if\>/end/Ig
#替换for和while结构end loop关键字
s/\<end[ \t]*Loop\>/end/Ig
#替换while结构
#/\<While\>/s/\<Loop\>/begin/Ig
s/\<While\>[ \t]*\(.*\)[ \t]*\<Loop\>/while \1 begin/Ig
#替换for结构
s/\<for\>[ \t]*\([a-z_]\{1,\}\)[ \t]*\<in\>[ \t]*\([a-z_0-9]\{1,\}\)[ \t]*..[ \t]*\([a-z_0-9]\{1,\}\)[ \t]*\<Loop\>/set \1 = \2; while \1 <= \3 begin/Ig
#用select t = s from 替换select s into t from BS_RangePartition结构
s/\<select\>[ \t]*\(.*\)[ \t]*\<into\>[ \t]*\([a-z_]\{1,\}\)[ \t]*/select \2 = \1 /Ig
#删除in关键字
s/\(\<P_[a-z_]\{1,\}\>\)[ \t]*\<in\>/\1/Ig
#将out关键字改为output并调整位置
s/\(\<P_[a-z_]\{1,\}\>\)[ \t]*\<out\>[ \t]*\(\<[0-9a-z_.%]\{1,\}\>\)[ \t]*/\1 \2 output/Ig
# 处理 P_Cursor
# 删除 open P_Cursor for select * From dual where 1 <> 1;
/\<open\>[ \t]*\<P_Cursor\>[ \t]*\<for\>[ \t]*select[ \t]*[*][ \t]*From[ \t]*dual[ \t]*where[ \t]*1[ \t]*<>[ \t]*1/Id
# 删除 open P_Cursor for
/^[ \t]*\<open\>[ \t]*\<P_Cursor\>[ \t]*\<for\>[ \t]*$/Id
# 将open p_CURSOR for select v_count From dual;替换为select v_count;
s/\<open\>[ \t]*\<P_Cursor\>[ \t]*\<for\>[ \t]*\(select[ \t]*.*\)[ \t]*From[ \t]*dual[ \t]*;/\1;/Ig
# 将open P_Cursor For Select o.org_ID替换为Select o.org_ID
s/\<open\>[ \t]*\<P_Cursor\>[ \t]*\<for\>[ \t]*\<select\>/select/Ig
# 将open P_Cursor for V_Sql;替换为exec (V_Sql);
s/\<open\>[ \t]*\<P_Cursor\>[ \t]*\<for\>[ \t]*\([a-z_]*\)[ \t]*;/exec (\1);/Ig
# 处理游标while结构
# 将Cursor cur Is select name_tx,替换为declare cur cursor for select name_tx
s/\<Cursor\>[ \t]*\([a-z_]*\)[ \t]*\<Is\>/declare \1 cursor local for/Ig
# 将Fetch cur Into V_Name替换为fetch next from cur into @V_Name
s/\<Fetch\>[ \t]*\([a-z_]*\)[ \t]*\<Into\>/fetch next from \1 into/Ig
# 将If (cur%NOTFOUND)替换为if (@@Fetch_Status <> 0)
/\<if\>/s/\(\<[a-z_]*\)%NOTFOUND\>/@@Fetch_Status <> 0/Ig
# 将Exit;替换为break;
s/^[ \t]*\<Exit\>[ \t]*;[ \t]*$/break;/Ig
# 将Close cur;替换为close cur;deallocate cur;
s/^[ \t]*\<Close\>[ \t]*\(\<[a-z_]*\>\)[ \t]*;[ \t]*$/close \1;\ndeallocate \1;/Ig
# 将Oracle调用存储过程的方式替换为SqlServer的方式
s/^[ \t]*\(\<[a-z_]*[.][a-z_]*\>\)(\(.*\))[ \t]*;[ \t]*$/exec \1 \2;/Ig
# /替换
# s#^[ \t]*/[ \t]*$#GO#g
s#^[ \t]*/[ \t]*$##g
