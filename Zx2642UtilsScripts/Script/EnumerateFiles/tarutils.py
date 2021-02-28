# -*- coding: utf-8 -*-
"""
tarutils
tar的实用工具类
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

tar = '"C:\\Program Files\\Git\\usr\\bin\\tar.exe"' if isWindows else 'tar'
gzAdd = tar + ' -zcvf {}.tgz {}'
gzExtract = tar + ' -zxvf {}'


def add(filename):
    parts = os.path.splitext(filename)
    os.system(gzAdd.format(parts[0], filename))

def adddir(dirname):
    os.system(gzAdd.format(dirname, dirname))

def extract(filename):
    os.system(gzExtract.format(filename))

if __name__ == "__main__":
    basePath = sys.path[0]

    basePath = """D:\\1\\imgs2""" # compress dir
    os.chdir(basePath) # 设置当前目录
    addhandler = lambda dir, file : add(file)
    pathwalkutils.walkfiles('.', addhandler, '*.img')

    #basePath = """D:\\1\\imgs""" # 7z files dir
    #basePath = """D:\\1\\imgs2""" # compress dir
    #os.chdir(basePath) # 设置当前目录
    extracthandler = lambda dir, file : extract(file)
    pathwalkutils.walkfiles('.', extracthandler, '.gz;*.tgz')

