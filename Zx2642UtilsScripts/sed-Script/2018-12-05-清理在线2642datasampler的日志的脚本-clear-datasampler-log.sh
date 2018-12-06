#!/bin/bash
# 
# 2018-12-05 陈良
# 清理在线2642 datasampler 的日志的脚本
# 使用方式：. 2018-12-05-清理在线2642datasampler的日志的脚本-clear-datasampler-log.sh 或 ./2018-12-05-清理在线2642datasampler的日志的脚本-clear-datasampler-log.sh



cat log.txt | sed -f clear-datasampler-log.sed > logout.txt
