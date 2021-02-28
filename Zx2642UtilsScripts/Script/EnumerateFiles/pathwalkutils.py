# -*- coding: utf-8 -*-
"""
pathwalkutils
遍历目录的实用工具类
"""


import platform
import sys
import os, fnmatch

"""
dirpath: dir need to walk
handler: lamda deal with dirname, filename
patterns: like: '*.jpg;*.gif;*.png'
"""
def walkfiles(dirpath, handler, patterns = '*' ):
    patterns = patterns.split(';')
    for path, subdirs, files in os.walk(dirpath):
        for name in files:
            for pattern in patterns:
                if fnmatch.fnmatch(name, pattern):
                    #filePath = os.path.join(path,name)
                    handler(path, name)
        break


if __name__ == "__main__":
    handler = lambda dir, file : print(os.path.join(dir,file))
    walkfiles('.', handler)
