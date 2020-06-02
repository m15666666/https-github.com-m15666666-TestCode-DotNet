# -*- coding:gb2312 -*-
# �˴�����Ϊgb2312������ʹ��utf-8���룬����os.walk�������þͻ����ԭ��֪����

import os, fnmatch


def iterateDirGenerateImagesPath():
     """����Ŀ¼����Images���е�ͼƬ·������Ƭ��"""
     
     region= '#region %s Ŀ¼\n'
     regionend= '#endregion\n'
     code= """public static readonly string %s = HttpContextUtils.ToAbsolute( "~/%s" );\n"""
     patterns = '*.jpg;*.gif;*.png'
     outfile="E:\\out.cs"
     imagesDir = 'Images'
     
     root="E:\\lchen\\Project\\VSS\\14_MSPJ02642_D\\MSPJ-D0642�������\\�汾���ƿ�\\����\\MEMS\\MEMSWebApp\\"
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
                         filePath = filePath.replace(root,'')   #ȥ��ǰ׺                 
                         
                         #������
                         varname = filePath.replace("\\",'_').replace('.','_')
                         
                         relativePath = filePath.replace("\\",'/')
                         
                         f.write( code % (varname,relativePath)   )   
                         
          if createRegion:
               f.write( regionend)

     f.close()
     print  '--OK--'

if __name__ == "__main__":
     iterateDirGenerateImagesPath()

     
   