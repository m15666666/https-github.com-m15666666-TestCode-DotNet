# -*- coding: utf-8 -*-
"""
p7zutils
7z的实用工具类
"""


import platform
import sys
import os, fnmatch
import pathwalkutils

#https://blog.csdn.net/gatieme/article/details/45674367
osType = platform.system()
print(osType)
isWindows = osType == 'Windows'
isLinux = osType == 'Linux'


p7z = '"C:\\Program Files\\7-Zip\\7z.exe"' if isWindows else '7z'
currentDir7z = os.path.join('.', '7z.exe')
if(isWindows and os.path.exists(currentDir7z)):
    p7z = currentDir7z

#p7zTest = p7z + ' t {}'
p7zTest = '%s t {}' % p7z
p7zAdd = p7z + ' a {}.7z {}'
p7zExtract = p7z + ' e {}'

"""
"""
def test(filename):
    os.system(p7zTest.format(filename))

def add(filename):
    parts = os.path.splitext(filename)
    os.system(p7zAdd.format(parts[0],filename))

def extract(filename):
    os.system(p7zExtract.format(filename))

if __name__ == "__main__":
    basePath = sys.path[0]
    basePath = """D:\\1\\imgs""" # 7z files dir
    #basePath = """D:\\1\\imgs2""" # compress dir
    os.chdir(basePath) # 设置当前目录
    testhandler = lambda dir, file : test(file)
    #pathwalkutils.walkfiles('.', testhandler, '*.7z')
    extracthandler = lambda dir, file : extract(file)
    #pathwalkutils.walkfiles('.', extracthandler, '*.7z')

    basePath = """D:\\1\\imgs2""" # compress dir
    os.chdir(basePath) # 设置当前目录
    addhandler = lambda dir, file : add(file)
    pathwalkutils.walkfiles('.', addhandler, '*.img')
