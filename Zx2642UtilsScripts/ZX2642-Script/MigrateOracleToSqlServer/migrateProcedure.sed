#! /bin/sed -f
#s/[ \t]\{1,\}//g
# ����procedure
s/\<procedure\>[ \t]*\(\<[a-z_]\{1,\}\>\)/create procedure __Package__.\1/Ig
# ����function
s/\<function\>[ \t]*\(\<[a-z_]\{1,\}\>\)/create function __Package__.\1/Ig
# �滻�������ص�����
/;/!s/return[ \t]\{1,\}\(varchar\|int\|number\|Date\|[a-z_.]*%type\)/RETURNS \1/Ig
# ɾ��������ͷ��begin
/^[ \t]*\<begin\>[ \t]*$/Id
# �ں����ʹ洢���̽�β��end;���滻�м�GO
s/^[ \t]*\<end\>[ \t]*;[ \t]*$/end;\nGO/Ig
# �ں�����ͷ��as�������begin
s/\<as\>[ \t]*$/as begin/Ig
#�滻if�ṹ
s/\<then\>/begin/Ig
s/\<else\>/end else begin/Ig
s/\<elsif\>/end else if/Ig
s/\<end[ \t]*if\>/end/Ig
#�滻for��while�ṹend loop�ؼ���
s/\<end[ \t]*Loop\>/end/Ig
#�滻while�ṹ
#/\<While\>/s/\<Loop\>/begin/Ig
s/\<While\>[ \t]*\(.*\)[ \t]*\<Loop\>/while \1 begin/Ig
#�滻for�ṹ
s/\<for\>[ \t]*\([a-z_]\{1,\}\)[ \t]*\<in\>[ \t]*\([a-z_0-9]\{1,\}\)[ \t]*..[ \t]*\([a-z_0-9]\{1,\}\)[ \t]*\<Loop\>/set \1 = \2; while \1 <= \3 begin/Ig
#��select t = s from �滻select s into t from BS_RangePartition�ṹ
s/\<select\>[ \t]*\(.*\)[ \t]*\<into\>[ \t]*\([a-z_]\{1,\}\)[ \t]*/select \2 = \1 /Ig
#ɾ��in�ؼ���
s/\(\<P_[a-z_]\{1,\}\>\)[ \t]*\<in\>/\1/Ig
#��out�ؼ��ָ�Ϊoutput������λ��
s/\(\<P_[a-z_]\{1,\}\>\)[ \t]*\<out\>[ \t]*\(\<[0-9a-z_.%]\{1,\}\>\)[ \t]*/\1 \2 output/Ig
# ���� P_Cursor
# ɾ�� open P_Cursor for select * From dual where 1 <> 1;
/\<open\>[ \t]*\<P_Cursor\>[ \t]*\<for\>[ \t]*select[ \t]*[*][ \t]*From[ \t]*dual[ \t]*where[ \t]*1[ \t]*<>[ \t]*1/Id
# ɾ�� open P_Cursor for
/^[ \t]*\<open\>[ \t]*\<P_Cursor\>[ \t]*\<for\>[ \t]*$/Id
# ��open p_CURSOR for select v_count From dual;�滻Ϊselect v_count;
s/\<open\>[ \t]*\<P_Cursor\>[ \t]*\<for\>[ \t]*\(select[ \t]*.*\)[ \t]*From[ \t]*dual[ \t]*;/\1;/Ig
# ��open P_Cursor For Select o.org_ID�滻ΪSelect o.org_ID
s/\<open\>[ \t]*\<P_Cursor\>[ \t]*\<for\>[ \t]*\<select\>/select/Ig
# ��open P_Cursor for V_Sql;�滻Ϊexec (V_Sql);
s/\<open\>[ \t]*\<P_Cursor\>[ \t]*\<for\>[ \t]*\([a-z_]*\)[ \t]*;/exec (\1);/Ig
# �����α�while�ṹ
# ��Cursor cur Is select name_tx,�滻Ϊdeclare cur cursor for select name_tx
s/\<Cursor\>[ \t]*\([a-z_]*\)[ \t]*\<Is\>/declare \1 cursor local for/Ig
# ��Fetch cur Into V_Name�滻Ϊfetch next from cur into @V_Name
s/\<Fetch\>[ \t]*\([a-z_]*\)[ \t]*\<Into\>/fetch next from \1 into/Ig
# ��If (cur%NOTFOUND)�滻Ϊif (@@Fetch_Status <> 0)
/\<if\>/s/\(\<[a-z_]*\)%NOTFOUND\>/@@Fetch_Status <> 0/Ig
# ��Exit;�滻Ϊbreak;
s/^[ \t]*\<Exit\>[ \t]*;[ \t]*$/break;/Ig
# ��Close cur;�滻Ϊclose cur;deallocate cur;
s/^[ \t]*\<Close\>[ \t]*\(\<[a-z_]*\>\)[ \t]*;[ \t]*$/close \1;\ndeallocate \1;/Ig
# ��Oracle���ô洢���̵ķ�ʽ�滻ΪSqlServer�ķ�ʽ
s/^[ \t]*\(\<[a-z_]*[.][a-z_]*\>\)(\(.*\))[ \t]*;[ \t]*$/exec \1 \2;/Ig
# /�滻
# s#^[ \t]*/[ \t]*$#GO#g
s#^[ \t]*/[ \t]*$##g
