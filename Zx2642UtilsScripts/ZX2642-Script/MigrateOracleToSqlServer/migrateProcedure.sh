#!/bin/bash
#
# ����洢���̽ű���$1��sql�ű��ļ���$2���ܹ���Schema����
#
# ��һ��ɾ�����з��ķ�ʽsed -nr 'H;${x;s/\n//g;p;}' createProcedure.sql > ,
#sed 's/\<create\>[ \t]\{1,\}\<or\>[ \t]\{1,\}\<replace\>[ \t]\{1,\}\<view\>/create view/g' $1
# ������̣�1��ɾ��",P_Cursor out T_Cursor";
#cat $1 | tr "\r\n" "~^" | sed 's/,\(~^\)*[ \t]*\<P_Cursor\>[ \t]*out\>[ \t]*\<T_Cursor\>//Ig' | tr "~^" "\r\n" | sed -f migrateProcedure.sed | sed -f primaryFunctionReplace.sed | sed -f SqlServerFunctionReplace.sed | sed -f migrateFieldTypeToSqlServer.sed | sed -f SqlServerSymbolReplace.sed | sed "s/__Package__/$2/Ig"
cat $1 | sed 's/[,]*[ \t]*\<P_Cursor\>[ \t]*out\>[ \t]*\<T_Cursor\>//Ig' | sed -f migrateProcedure.sed | sed -f primaryFunctionReplace.sed | sed -f SqlServerFunctionReplace.sed | sed -f SqlServerSymbolReplace.sed |sed -f migrateFieldTypeToSqlServer.sed | sed "s/__Package__/$2/Ig"
