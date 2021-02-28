# -*- coding: utf-8 -*-
"""

enumerate current directory  and extract 7z files

@author: 陈良
"""

import re
import os
import sys


if __name__ == '__main__':
    basePath = sys.path[0]
    #basePath = """D:\\1\\imgs""" # 7z files dir
    #basePath = """D:\\1\\imgs2""" # compress dir
    os.chdir(basePath) # 设置当前目录
    import p7zutils
    import pathwalkutils
    testhandler = lambda dir, file : p7zutils.test(file)
    #pathwalkutils.walkfiles('.', testhandler, '*.7z')
    extracthandler = lambda dir, file :  p7zutils.extract(file)
    pathwalkutils.walkfiles('.', extracthandler, '*.7z')
