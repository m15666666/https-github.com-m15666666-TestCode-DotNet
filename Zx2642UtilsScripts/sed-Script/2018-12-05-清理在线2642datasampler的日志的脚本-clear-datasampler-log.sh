#!/bin/bash
# 
# 2018-12-05 ����
# ��������2642 datasampler ����־�Ľű�
# ʹ�÷�ʽ��. 2018-12-05-��������2642datasampler����־�Ľű�-clear-datasampler-log.sh �� ./2018-12-05-��������2642datasampler����־�Ľű�-clear-datasampler-log.sh



cat log.txt | sed -f clear-datasampler-log.sed > logout.txt
