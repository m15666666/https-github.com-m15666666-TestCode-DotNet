#!usr/bin/env python
#coding=utf-8
 
import pywt
import math
import matplotlib.pyplot as plt
import numpy as np
 
def getdata():
        ret = [3, 7, 1, 1, -2, 5, 4, 6,3, 7, 1, 1, -2, 5, 4, 6] 
        return ret
        ret = []
        for i in range(1024):
                ret = np.append(ret, math.sin(2 * math.pi / 32 * i))
        return ret

def wavelets_db8(data):
        #ecg = pywt.data.ecg() # 生成心电信号
        #data = ecg
        waveletname = 'db8'
        wavelet = pywt.Wavelet(waveletname)

        maxlevel = pywt.dwt_max_level(len(data), wavelet.dec_len)
        print('maxlevel is ' + str(maxlevel))
        o = pywt.dwt(data, wavelet)
        print(o)
        r = pywt.waverec(o, wavelet);
        o1 = pywt.wavedec(data, wavelet)

        threshold = 0.04 # Threshold for filtering
        coeffs = pywt.wavedec(data, waveletname, level=maxlevel) # 将信号进行小波分解
        for i in range(1, len(coeffs)):
                coeffs[i] = pywt.threshold(coeffs[i], threshold * max(coeffs[i])) # 将噪声滤波
        datarec = pywt.waverec(coeffs, waveletname) # 将信号进行小波重构
        mintime = 0
        maxtime = mintime + len(data) + 1

        index = []
        for i in range(len(data)):
                index.append(i)
        plt.figure()

        plt.subplot(2, 1, 1)

        plt.plot(index[mintime:maxtime], data[mintime:maxtime])

        plt.xlabel('time (s)')

        plt.ylabel('microvolts (uV)')

        plt.title("Raw signal")

        plt.subplot(2, 1, 2)

        plt.plot(index[mintime:maxtime], datarec[mintime:maxtime-1])

        plt.xlabel('time (s)')

        plt.ylabel('microvolts (uV)')

        plt.title("De-noised signal using wavelet techniques")

        plt.tight_layout()

        plt.show()

        print(o1)
        r1 = pywt.waverec(o1, wavelet)
        print(r1)
        o2 = pywt.swt(data, wavelet)
        print(o2)
        
        
def main():
        data = getdata()
        wavelets_db8(data)
	#wavelets_db8(data)
	
 
if __name__ == "__main__":
	main()
