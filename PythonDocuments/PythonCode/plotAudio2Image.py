#!usr/bin/env python
#coding=utf-8
 
import wave  #��Ƶ�����
import numpy as np
import matplotlib.pyplot as plt   #רҵ��ͼ��
from PIL import Image
 

filepath = "current.wav"
fwav = wave.open(filepath,)
print(fwav)
 
params = fwav.getparams()
print(params)
 
nchannels,sampwidth,framerate,nframes = params[:4]
strData = fwav.readframes(nframes)
w= np.fromstring(strData,dtype=np.int16)
w = w*1.0/(max(abs(w)))
w = np.reshape(w,[nframes,nchannels])   #����תΪ��άֱ������
 
#���Ʋ���ͼ ��һ����������ͼ
time = np.arange(0,nframes)*(1.0 / framerate)
plt.figure()
plt.subplot(5,1,1)
plt.plot(time[:len(w[:,0])],w[:,0])
plt.xlabel("Time(s)")
plt.title("First Channel")
plt.show()
#img.save("result/First Channel.png")
 
#���Ƶڶ��������Ĳ���ͼ
plt.subplot(5,1,2)
plt.plot(time[:len(w[:,1])],w[:,1])
plt.xlabel("Time(s)")
plt.title("Second Channel")
#img.save("result/Second Channel.png")
 
#�Ӵ�����ͼ�ľ���
plt.subplot(5,1,3)
plt.plot(time[:len(w[:,1])],w[:,1])
plt.xlabel("Time(s)")
plt.title("Second Channel")
img.save("result/Second1 Channel.png")

#��ȡ����ͼƬ
img = Image.open("wavedata/spect_000.png")
img.show()  #ϵͳ�Դ��������ʾͼƬ
 
#matplotlib ��ʾͼƬ
plt.figure("spect_000")
plt.imshow(img)
plt.show()

# https://blog.csdn.net/weixin_32393347/java/article/details/80902676