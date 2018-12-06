#! /bin/sed -f
#s/[ \t]\{1,\}//g
#替换create or replace view
s/\<create\>[ \t]*\<or\>[ \t]*\<replace\>[ \t]*\<view\>[ \t]*\(\<[a-z_]\{1,\}\>\)/create view \1/I
# 在每个视图后面加GO
s/;[ \t]*$/;\nGO/g
