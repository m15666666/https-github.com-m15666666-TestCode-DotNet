# 
# 2018-12-05 ����
# ��������2642 datasampler ����־�Ľű�
# ʹ�÷�ʽ��sed -f clear-datasampler-log.sed log.txt > logout.txt


# ɾ����������
#SocketLib.PackageStructException: ��ͷ�ֽ������(48-65-61-72-74-62-00-00-00-00-00)��
#   �� SocketLib.PackageSendReceive.CheckPart1() λ�� E:\svn\MSPJ-D02642\code\branches\MEMS_AllSource_Dev1.0\OnlineSample\SocketLib\PackageSendReceive.cs:�к� 280
#   �� SocketLib.PackageSendReceive.OnReceiveHeadSuccess_Part1(SocketWrapper socket) λ�� E:\svn\MSPJ-D02642\code\branches\MEMS_AllSource_Dev1.0\OnlineSample\SocketLib\PackageSendReceive.cs:�к� 413
#
#
#
/��ͷ�ֽ������/,/OnReceiveHeadSuccess_Part1/d

# ɾ������һ��
#2018-12-05 13:57:19,154 ERROR DefaultLogger ��ȡ������((Remote = 10.3.2.14:33799, Local = 10.3.2.59:1283))�� 
#
/��ȡ������/d

# ɾ������һ��
#2018-12-05 13:57:20,154 INFO  DefaultLogger OnAcceptSuccess ����ɹ�((Remote = 10.3.2.14:33800, Local = 10.3.2.59:1283))�� 
#
/����ɹ�/d

# ɾ��
/���ӹر�/d

# ɾ������
/^[ \t]*$/d
