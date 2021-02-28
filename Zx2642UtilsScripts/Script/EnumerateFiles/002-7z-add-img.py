# -*- coding: utf-8 -*-
"""

enumerate current directory and add img extension files to 7z files

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
    #pathwalkutils.walkfiles('.', testhandler, '*.7z')
    addhandler = lambda dir, file :  p7zutils.add(file)
    pathwalkutils.walkfiles('.', addhandler, '*.img')
