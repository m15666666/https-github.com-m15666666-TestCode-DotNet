#! /bin/sed -f
# commit�滻
/^[ \t]*\<commit\>[ \t]*;[ \t]*$/Id
# /�滻
s#^[ \t]*/[ \t]*$##g
#��select t = s from �滻select s into t from BS_RangePartition�ṹ
s/\<select\>[ \t]*\(.*\)[ \t]*\<into\>[ \t]*\([a-z_]\{1,\}\)[ \t]*/select \2 = \1 /Ig
#�滻if�ṹ
s/\<then\>/begin/Ig
s/\<else\>/end else begin/Ig
s/\<elsif\>/end else if/Ig
s/\<end[ \t]*if\>/end/Ig
