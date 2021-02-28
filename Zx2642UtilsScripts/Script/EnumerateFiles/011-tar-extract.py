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
    import tarutils
    import pathwalkutils
    #pathwalkutils.walkfiles('.', testhandler, '*.7z')
    extracthandler = lambda dir, file :  tarutils.extract(file)
    pathwalkutils.walkfiles('.', extracthandler, '*.gz;*.tgz')
