#! /bin/sed -f
#s/[ \t]\{1,\}//g
#�滻create or replace view
s/\<create\>[ \t]*\<or\>[ \t]*\<replace\>[ \t]*\<view\>[ \t]*\(\<[a-z_]\{1,\}\>\)/create view \1/I
# ��ÿ����ͼ�����GO
s/;[ \t]*$/;\nGO/g
