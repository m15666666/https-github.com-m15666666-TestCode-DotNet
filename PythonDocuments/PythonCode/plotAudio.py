# -*- coding: utf-8 -*-
import wave
import pylab as pl
import numpy as np

# references
# http://bigsec.net/b52/scipydoc/wave_pyaudio.html
# https://ask.csdn.net/questions/759011

# ��WAV�ĵ�
f = wave.open(r"current.wav", "rb")

# ��ȡ��ʽ��Ϣ
# (nchannels, sampwidth, framerate, nframes, comptype, compname)
params = f.getparams()
nchannels, sampwidth, framerate, nframes = params[:4]

# ��ȡ��������
str_data = f.readframes(nframes)
f.close()

#����������ת��Ϊ����
wave_data = np.fromstring(str_data, dtype=np.short)
wave_data.shape = -1, 2
wave_data = wave_data.T
time = np.arange(0, nframes) * (1.0 / framerate)

# ���Ʋ���
pl.subplot(211) 
pl.plot(time[:len(wave_data[0])], wave_data[0])
pl.subplot(212) 
pl.plot(time[:len(wave_data[1])], wave_data[1], c="g")
pl.xlabel("time (seconds)")
pl.show()

pl.figure(1)
pl.subplot(2,1,1)
pl.plot(time[:len(wave_data[0])], wave_data[0])
pl.subplot(2,1,2)
pl.plot(time[:len(wave_data[1])], wave_data[1], c='r')
pl.xlabel("Time(s)")
pl.show()


# dummyInput = input("enter to exit\n")