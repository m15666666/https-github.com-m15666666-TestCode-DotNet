# -*- coding: utf-8 -*-
"""

enumerate a directory or extension, do some thing

https://www.jianshu.com/p/1a787ff721ba
https://www.jianshu.com/p/1b3f218e1e57

@author: 陈良
"""

import re
import os


if __name__ == '__main__':
    import sys
    p7zTest = '"C:\\Program Files\\7-Zip\\7z.exe" t {}'
    p7zAdd = '"C:\\Program Files\\7-Zip\\7z.exe" a {}.7z {}'
    p7zExact = '"C:\\Program Files\\7-Zip\\7z.exe" e {}'

    basePath = sys.path[0]
    #basePath = """D:\\1\\imgs""" # 7z files dir
    #basePath = """D:\\1\\imgs2""" # compress dir
    os.chdir(basePath) # 设置当前目录
    for parent, dirnames, filenames in os.walk(basePath):
        for dirname in dirnames:
            print('[DIR]', dirname)
        for filename in filenames:
            parts = os.path.splitext(filename)
            if(parts[1] == '.7z'):
                #print("test 7z file {}".format(filename))
                print("extract 7z file {}".format(filename))
                #continue
                #os.system(f'mspaint {img}')
                #os.system(p7zTest.format(filename))
                os.system(p7zExact.format(filename))
            if(parts[1] == '.img'):
                print("compress {}".format(filename))
                #os.system(f'mspaint {img}')
                os.system(p7zAdd.format(parts[0],filename))

            print('[FILE]', filename)
        break

    print("sys.path[0] = ", sys.path[0])
    print("sys.argv[0] = ", sys.argv[0])
    """
    print("__file__ = ", __file__)
    print("os.path.abspath(__file__) = ", os.path.abspath(__file__))
    print("os.path.realpath(__file__) = ", os.path.realpath(__file__))
    print("os.path.dirname(os.path.realpath(__file__)) = ", 
        os.path.dirname(os.path.realpath(__file__)))
    print("os.path.split(os.path.realpath(__file__)) = ", 
        os.path.split(os.path.realpath(__file__)))
    print("os.path.split(os.path.realpath(__file__))[0] = ", 
        os.path.split(os.path.realpath(__file__))[0])
    print("os.getcwd() = ", os.getcwd())

    """

