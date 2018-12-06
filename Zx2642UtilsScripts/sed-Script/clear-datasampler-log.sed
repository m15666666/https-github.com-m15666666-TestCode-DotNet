# 
# 2018-12-05 陈良
# 清理在线2642 datasampler 的日志的脚本
# 使用方式：sed -f clear-datasampler-log.sed log.txt > logout.txt


# 删除下面三行
#SocketLib.PackageStructException: 包头分界符错误(48-65-61-72-74-62-00-00-00-00-00)！
#   在 SocketLib.PackageSendReceive.CheckPart1() 位置 E:\svn\MSPJ-D02642\code\branches\MEMS_AllSource_Dev1.0\OnlineSample\SocketLib\PackageSendReceive.cs:行号 280
#   在 SocketLib.PackageSendReceive.OnReceiveHeadSuccess_Part1(SocketWrapper socket) 位置 E:\svn\MSPJ-D02642\code\branches\MEMS_AllSource_Dev1.0\OnlineSample\SocketLib\PackageSendReceive.cs:行号 413
#
#
#
/包头分界符错误/,/OnReceiveHeadSuccess_Part1/d

# 删除下面一行
#2018-12-05 13:57:19,154 ERROR DefaultLogger 读取包出错((Remote = 10.3.2.14:33799, Local = 10.3.2.59:1283))！ 
#
/读取包出错/d

# 删除下面一行
#2018-12-05 13:57:20,154 INFO  DefaultLogger OnAcceptSuccess 接入成功((Remote = 10.3.2.14:33800, Local = 10.3.2.59:1283))！ 
#
/接入成功/d

# 删除
/连接关闭/d

# 删除空行
/^[ \t]*$/d
