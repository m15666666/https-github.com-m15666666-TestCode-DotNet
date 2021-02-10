# -*- coding: utf-8 -*-
"""
Created on Mon Aug 13 10:26:38 2012

ParseBytesFrameUtils: 解析帧字节数组的实用工具类

@author: 陈良
"""

import NumberUtils

class ParseBytesFrameUtils(object):
    """解析帧字节数组的实用工具类"""
    
    def __init__(self, frameHexString):
        """使用一个完整的帧来初始化对象."""
        self.frameHexString = frameHexString
        self.frameHexBytes = frameHexString.split("-")
        self.index = self.endIndex = 0

    def printBytesData(self, increment, dataName):
        """打印字节数组数据的值"""
        bytesValue = self.getData(increment)
        print dataName + ": " + str(bytesValue)
        return bytesValue
    
    def printFloatData(self, dataName):
        """打印浮点型数据的值"""
        (retString, retValue) = self.getFloatData()[1:]
        print dataName + ": %f(0x%s)" % (retValue, retString)
        return (retString, retValue)
        
    def printIntData(self, increment, dataName):
        """打印整型数据的值"""
        (intString, intValue) = self.getIntData(increment)[1:]
        print dataName + ": %d(0x%s)" % (intValue, intString)
        return (intString, intValue)
        
    def changIndex(self):
        """调整index的值"""
        self.index += ( self.endIndex - self.index )
        
    def changEndIndex(self, increment):
        """调整endindex的值"""
        self.endIndex = self.index + increment
    
    def getFloatData(self):
        """获得字节数组中的浮点数据"""
        retBytes = self.getData(4)
        retValue = NumberUtils.hexBytes2Floats(retBytes)[0]
        retString = ''.join(retBytes)
        return (retBytes, retString, retValue)
        
    def getIntData(self, increment):
        """获得字节数组中的整型数据"""
        retBytes = self.getData(increment)
        retValue = NumberUtils.hexBytes2Int(retBytes)
        #retBytes.reverse()
        retString = ''.join(retBytes)
        return (retBytes, retString, retValue)
        
    def getData(self, increment):
            """获得字节数组中的数据"""
            self.changEndIndex(increment)
            retBytes = self.frameHexBytes[self.index : self.endIndex]
            self.changIndex()
            return retBytes
            
    def getIDDescription(self, dictionary, ID):
        """获得ID的描述"""
        if( dictionary.has_key(ID) ):
            return dictionary[ID]
        return "unknown"