# -*- coding:gb2312 -*-
# 此处必须为gb2312，不能使用utf-8编码，否则os.walk函数调用就会出错，原因不知道。

import os, fnmatch


def iterateDirGenerateImagesPath():
     """遍历目录生成Images类中的图片路径代码片段"""
     
     region= '#region %s 目录\n'
     regionend= '#endregion\n'
     code= """public static readonly string %s = HttpContextUtils.ToAbsolute( "~/%s" );\n"""
     patterns = '*.jpg;*.gif;*.png'
     outfile="E:\\out.cs"
     imagesDir = 'Images'
     
     root="E:\\lchen\\Project\\VSS\\14_MSPJ02642_D\\MSPJ-D0642（软件）\\版本控制库\\代码\\MEMS\\MEMSWebApp\\"
     imagesDirPath = root + imagesDir

     f = open(outfile,"wb")
     patterns = patterns.split(';')
     
     for path, subdirs, files in os.walk(imagesDirPath):
          createRegion = False
          for name in files:
               for pattern in patterns:
                    if fnmatch.fnmatch(name, pattern):
                         if not createRegion:
                              createRegion = True
                              f.write( region % (path.replace(root,"")))

                         filePath = os.path.join(path,name)
                         filePath = filePath.replace(root,'')   #去掉前缀                 
                         
                         #变量名
                         varname = filePath.replace("\\",'_').replace('.','_')
                         
                         relativePath = filePath.replace("\\",'/')
                         
                         f.write( code % (varname,relativePath)   )   
                         
          if createRegion:
               f.write( regionend)

     f.close()
     print  '--OK--'

if __name__ == "__main__":
     iterateDirGenerateImagesPath()

     
   