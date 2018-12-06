#! /bin/sed -f
# commit替换
/^[ \t]*\<commit\>[ \t]*;[ \t]*$/Id
# /替换
s#^[ \t]*/[ \t]*$##g
#用select t = s from 替换select s into t from BS_RangePartition结构
s/\<select\>[ \t]*\(.*\)[ \t]*\<into\>[ \t]*\([a-z_]\{1,\}\)[ \t]*/select \2 = \1 /Ig
#替换if结构
s/\<then\>/begin/Ig
s/\<else\>/end else begin/Ig
s/\<elsif\>/end else if/Ig
s/\<end[ \t]*if\>/end/Ig
