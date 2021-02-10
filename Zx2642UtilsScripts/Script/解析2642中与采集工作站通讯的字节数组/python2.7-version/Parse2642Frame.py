# -*- coding: gb2312 -*-
"""
Created on Fri Jun 15 14:09:08 2012

解析2642项目中上位机与采集工作站通讯的字节数组

@author: 陈良
"""

import re
import os
import NumberUtils
import ParseBytesFrameUtils

class ParserFor2642(ParseBytesFrameUtils.ParseBytesFrameUtils):
    """解析2642项目中上位机与采集工作站通讯的字节数组"""
    
    CommandIDBase = 1000
    CommandID_DownloadSampleConfig = CommandIDBase + 30
    CommandID_UploadData2DB = CommandIDBase + 72
    CommandID2Description = {
        CommandID_DownloadSampleConfig:"下载采集配置文件",
        CommandID_UploadData2DB:"上传数据到数据库，包括：趋势数据（温度、电流等没有波形测点数据）、波形数据（1维，2维等振动测点数据）、报警事件、报警数据。",
    }
    
    StructTypeIDBase = 1000
    StructTypeID_TimeWaveData_1D = StructTypeIDBase + 2
    StructTypeID_SampleStationConfigData = StructTypeIDBase + 115
    StructTypeID2Description = {
        StructTypeID_TimeWaveData_1D:"一维时间波形数据",
        StructTypeID_SampleStationConfigData:"采集工作站配置数据",
    }
    
    def __init__(self, frameHexString):
        '''使用一个完整的帧来初始化对象.'''
        super(ParserFor2642, self).__init__(frameHexString)
        NumberUtils.isBigEndian = False

    def printParseResult(self):
        '''打印解析结果.'''
        
        # 包头
        self.printBytesData(2, "Head")
        
        # 版本号
        self.printIntData(4, "Version")

        # 包体长度
        frameDataLength = self.printIntData(4, "Length")[1]
        
        # first crc
        self.printBytesData(1, "First crc")
        
        # 命令类型
        (cmdBytes, cmdString, cmdID) = self.getIntData(4)        
        print "cmdID: %d(0x%s, %s)" % (cmdID, cmdString, self.getCommandIDDescription(cmdID))
        
        # 结构类型
        (structBytes, structString, structID) = self.getIntData(4)
        print "structID: %d(0x%s, %s)" % (structID, structString, self.getStructTypeIDDescription(structID))
        
        # 返回false表示不支持的结构ID，则打印除了命令类型ID、结构类型ID之后的数据字节数组
        if ( not self.printStruct(structID) ):
            # unknown struct data
            self.printBytesData(frameDataLength - 4 - 4, "unknown struct data")
        
        self.printFollowingTail()
        
        # second crc
        print "Second crc: " + str(self.frameHexBytes[-3])
        
        # 包尾
        print "Tail: " + str(self.frameHexBytes[-2:])
        
    def printStruct(self, structID):
        """打印结构"""
        printHandlers = {
            ParserFor2642.StructTypeID_TimeWaveData_1D: lambda: self.printTimeWaveData1DStruct(),
            ParserFor2642.StructTypeID_SampleStationConfigData: lambda: self.printSampleStationConfigDataStruct(),
            };
        if(printHandlers.has_key(structID)):
            printHandlers[structID]()
            return True
        else:
            print "invalid struct id %d" % structID
            return False
    
    def printSampleStationConfigDataStruct(self):
        """打印StructTypeID_SampleStationConfigData结构"""
        # IPAddress.Count
        self.printIntData(4, "IPAddress.Count")

        # IPAddress.Data
        self.printBytesData(32, "IPAddress.Data")
        
        # DataSamplerPort
        self.printIntData(4, "DataSamplerPort")
        
        # UploadDBWaveIntervalInSecond
        self.printIntData(4, "UploadDBWaveIntervalInSecond")
        
        # UploadDBStaticIntervalInSecond
        self.printIntData(4, "UploadDBStaticIntervalInSecond")
        
        # PointConfigCount
        pointConfigCount = self.printIntData(4, "PointConfigCount")[1]
        
        # PointConfigData
        for i in range(0, pointConfigCount):
            self.printPointConfigDataStruct()
            
        # ChannelConfigCount
        channelConfigCount = self.printIntData(4, "ChannelConfigCount")[1]
        
        # ChannelConfigData
        for i in range(0, channelConfigCount):
            self.printChannelConfigDataStruct()

    def printChannelConfigDataStruct(self):
        """打印ChannelConfigData结构"""
        # ChannelConfigData.ChannelIdentifier
        self.printBytesData(12, "ChannelConfigData.ChannelIdentifier")

        # ChannelCD
        self.printChannelCD("ChannelConfigData.ChannelCD")
        
        # RevChannelCD
        self.printChannelCD("ChannelConfigData.RevChannelCD")
        
        # KeyPhaserRevChannelCD
        self.printChannelCD("ChannelConfigData.KeyPhaserRevChannelCD")
        
        # SwitchChannelCD
        self.printChannelCD("ChannelConfigData.SwitchChannelCD")
        
        # ChannelConfigData.SwitchTriggerStatus
        self.printIntData(4, "ChannelConfigData.SwitchTriggerStatus(开关量触发状态，1：开，0：关)")
        
        # ChannelConfigData.SwitchTriggerMethod
        self.printIntData(4, "ChannelConfigData.SwitchTriggerMethod(开关量触发方式，1：高电平触发，0：低电平触发)")

        # ChannelConfigData.ChannelTypeID
        self.printIntData(4, "ChannelConfigData.ChannelTypeID(通道类型ID，1：动态通道，2：静态通道，3：转速通道，5：开关量通道)")
        
        # ChannelConfigData.ChannelNumber
        self.printIntData(4, "ChannelConfigData.ChannelNumber")
        
        # ChannelConfigData.SignalTypeID
        self.printIntData(4, "ChannelConfigData.SignalTypeID")
        
        # ChannelConfigData.SampleFreq
        self.printFloatData("ChannelConfigData.SampleFreq")
        
        # ChannelConfigData.MultiFreq
        self.printIntData(4, "ChannelConfigData.MultiFreq")
        
        # ChannelConfigData.DataLength
        self.printIntData(4, "ChannelConfigData.DataLength")
        
        # ChannelConfigData.AverageCount
        self.printIntData(4, "ChannelConfigData.AverageCount")
        
        # ChannelConfigData.RevLowThreshold
        self.printIntData(4, "ChannelConfigData.RevLowThreshold")
        
        # ChannelConfigData.RevHighThreshold
        self.printIntData(4, "ChannelConfigData.RevHighThreshold")
        
        # ChannelConfigData.ReferenceRev
        self.printIntData(4, "ChannelConfigData.ReferenceRev")
        
        # ChannelConfigData.RevTypeID
        self.printIntData(4, "ChannelConfigData.RevTypeID")
        
        # ChannelConfigData.RevRatio
        self.printFloatData("ChannelConfigData.RevRatio")
        
        # ChannelConfigData.ScaleFactor
        self.printFloatData("ChannelConfigData.ScaleFactor")
        
        # ChannelConfigData.VoltageOffset
        self.printFloatData("ChannelConfigData.VoltageOffset")
        
        # ChannelConfigData.AlmCountDataCount
        almCountDataCount = self.printIntData(4, "ChannelConfigData.AlmCountDataCount")[1]
        
        # AlmCountData
        for i in range(0, almCountDataCount):
            self.printAlmCountDataStruct()

    def printAlmCountDataStruct(self):
        """打印AlmCountData结构"""
        # AlmCountData.AlmSourceID
        self.printIntData(4, "AlmCountData.AlmSourceID")
        
        # AlmCountData.AlmCount
        self.printIntData(4, "AlmCountData.AlmCount")
        
        
    def printPointConfigDataStruct(self):
        """打印PointConfigData结构"""
        # PointConfigData.PointID
        self.printIntData(4, "PointConfigData.PointID")
        
        # PointConfigData.MeasValueTypeID
        self.printIntData(4, "PointConfigData.MeasValueTypeID(13：有效值、11：峰值、12：峰峰值、15：平均值、16：波形指标)")
        
        # PointConfigData.Dimension
        self.printIntData(4, "PointConfigData.Dimension")
        
        # PointConfigData.ChannelCDs
        self.printChannelCD("PointConfigData.ChannelCDs")
        
        # PointConfigData.AlmStand_CommonSettingCount
        almStand_CommonSettingCount = self.printIntData(4, "PointConfigData.AlmStand_CommonSettingCount")[1]
        
        # AlmStand_CommonSettingDataStruct
        for i in range(0, almStand_CommonSettingCount):
            self.printAlmStand_CommonSettingDataStruct()

    def printAlmStand_CommonSettingDataStruct(self):
        """打印AlmStand_CommonSettingData结构"""
        # AlmStand_CommonSettingData.AlmSourceID
        self.printIntData(4, "AlmStand_CommonSettingData.AlmSourceID")

        # AlmStand_CommonSettingData.AlmTypeID
        self.printIntData(4, "AlmStand_CommonSettingData.AlmTypeID")
        
        # AlmStand_CommonSettingData LowLimits HighLimits
        self.printNullableSingleData("AlmStand_CommonSettingData.LowLimits[0]")
        self.printNullableSingleData("AlmStand_CommonSettingData.LowLimits[1]")
        self.printNullableSingleData("AlmStand_CommonSettingData.HighLimits[0]")
        self.printNullableSingleData("AlmStand_CommonSettingData.HighLimits[1]")

    def printTimeWaveData1DStruct(self):
        """打印StructTypeID_TimeWaveData_1D结构"""
        
        self.printHistoryDataHeadStruct()
        
        self.printVarByteArrayWaveStruct()
    
    def printFollowingTail(self):
        """打印打印接下来的crc和包尾"""
        
        # second crc
        self.printBytesData(1, "Following second crc")
        
        # Tail
        self.printBytesData(2, "Following tail")
        
        print "Index: %d, ByteCount: %d" % ( self.index, len( self.frameHexBytes ) )
        
    def printHistoryDataHeadStruct(self):
        """打印HistoryDataHead结构"""
        # 同步采集的唯一ID
        self.printBytesData(20, "SynUniqueID")
        #syncUniqueIDBytes = self.getData(20)
        #print "SyncUniqueID: " + str(syncUniqueIDBytes)
        
        # PointID
        self.printIntData(4, "PointID")

        # DataUsageID
        self.printIntData(4, "DataUsageID 0：监测、1：存储到数据库")
        
        # AlmLevelID
        self.printIntData(4, "AlmLevelID")
        
        # SampleTime
        print "Sample Time: %d-%d-%d %d %d:%d:%d" % (self.getIntData(2)[2], self.getIntData(1)[2], self.getIntData(1)[2], self.getIntData(1)[2], self.getIntData(1)[2], self.getIntData(1)[2], self.getIntData(1)[2])
        
        # DataLength
        self.printIntData(4, "DataLength")
        
        # Rev
        self.printIntData(4, "Rev")

        # SampleFreq
        self.printFloatData("SampleFreq")
        
        # MultiFreq
        self.printIntData(4, "MultiFreq")
        
        # MeasurementValueCount
        measurementValueCount = self.printIntData(4, "MeasurementValueCount")[1]
        
        # MeasurementValue
        for i in range(0, measurementValueCount):
            self.printFloatData("MeasurementValue " + str(i + 1) )
        
        # AxisOffsetValueCount
        axisOffsetValueCount = self.printIntData(4, "AxisOffsetValueCount")[1]
        
        # AxisOffsetValue
        if( 0 < axisOffsetValueCount ):
            for i in range(0, axisOffsetValueCount):
                self.printFloatData("AxisOffsetValue " + str(i + 1) )

    def printChannelCD(self, dataName):
        # ChannelCD
        bytesValue = self.getData(8)
        strValue = NumberUtils.hexBytes2String(bytesValue)
        print dataName + ": %s(%s)" %( strValue, str(bytesValue) )
        return (bytesValue, strValue)

    def printVarByteArrayWaveStruct(self):
        """打印VarByteArrayWave结构"""
        
        # VarByteArrayWave.WaveScale
        self.printFloatData("VarByteArrayWave.WaveScale")
        
        # VarByteArry.ItemLength
        self.printIntData(4, "VarByteArry.ItemLength")
                
        # VarByteArry.ByteLength
        byteLength = self.printIntData(4, "VarByteArry.ByteLength")[1]
        
        # VarByteArry.Data
        if( 0 < byteLength ):
            self.printBytesData(byteLength, "VarByteArry.Data")
            
    def printNullableSingleData(self, dataName):
        """打印NullableSingle数据的值"""
        (intString, intValue) = self.getIntData(4)[1:]
        (floatString, floatValue) = self.getFloatData()[1:]
        print dataName + ": %d(0x%s) %f(0x%s)" % (intValue, intString, floatValue, floatString)
        return (intValue, floatValue)

    def getCommandIDDescription(self, ID):
        """获得命令ID的描述"""
        return self.getIDDescription(ParserFor2642.CommandID2Description, ID)
        
    def getStructTypeIDDescription(self, ID):
        """获得命令ID的描述"""
        return self.getIDDescription(ParserFor2642.StructTypeID2Description, ID)

if __name__ == '__main__':
    
    # 将一维时间波形数据上传到数据库
    #frameHexString = """62-65-01-00-00-00-5C-10-00-00-4D-30-04-00-00-EA-03-00-00-DC-07-06-0F-0D-0F-16-3A-14-0B-09-00-0A-03-04-31-60-61-06-A0-B5-42-0F-00-01-00-00-00-00-00-00-00-DC-07-06-0F-0D-0F-16-3A-00-08-00-00-00-00-00-00-00-00-20-45-00-00-00-00-01-00-00-00-DF-F8-07-40-01-00-00-00-00-00-80-3F-0A-D7-43-3F-02-00-00-00-00-08-00-00-01-00-00-00-01-00-02-00-00-00-00-00-FF-FF-01-00-00-00-FF-FF-00-00-00-00-03-00-00-00-00-00-01-00-02-00-01-00-00-00-01-00-02-00-01-00-00-00-03-00-04-00-03-00-02-00-03-00-02-00-01-00-02-00-02-00-02-00-01-00-02-00-03-00-01-00-01-00-03-00-02-00-02-00-03-00-03-00-02-00-02-00-01-00-FF-FF-00-00-FF-FF-FE-FF-FE-FF-02-00-02-00-02-00-00-00-FF-FF-01-00-00-00-02-00-00-00-02-00-01-00-01-00-00-00-01-00-00-00-FF-FF-00-00-01-00-00-00-00-00-02-00-02-00-02-00-02-00-00-00-02-00-02-00-02-00-03-00-01-00-00-00-04-00-03-00-03-00-03-00-03-00-01-00-02-00-02-00-03-00-02-00-02-00-02-00-03-00-04-00-04-00-03-00-04-00-04-00-03-00-06-00-07-00-07-00-06-00-05-00-05-00-03-00-05-00-04-00-04-00-05-00-04-00-02-00-04-00-05-00-04-00-05-00-05-00-02-00-04-00-01-00-02-00-02-00-01-00-00-00-01-00-02-00-06-00-05-00-05-00-04-00-03-00-04-00-05-00-06-00-04-00-05-00-04-00-04-00-02-00-02-00-03-00-02-00-01-00-02-00-FF-FF-03-00-01-00-05-00-02-00-05-00-02-00-00-00-02-00-00-00-00-00-02-00-01-00-FF-FF-00-00-00-00-00-00-FE-FF-FF-FF-00-00-FE-FF-FF-FF-01-00-00-00-FF-FF-FF-FF-FE-FF-00-00-00-00-FC-FF-FF-FF-00-00-00-00-FF-FF-02-00-01-00-00-00-01-00-02-00-01-00-01-00-01-00-05-00-02-00-02-00-04-00-01-00-03-00-02-00-00-00-04-00-03-00-03-00-03-00-02-00-00-00-02-00-00-00-FF-FF-01-00-02-00-02-00-00-00-02-00-00-00-01-00-FF-FF-00-00-FF-FF-FF-FF-FF-FF-01-00-01-00-02-00-FF-FF-FE-FF-FF-FF-FF-FF-00-00-02-00-04-00-FF-FF-FF-FF-00-00-02-00-03-00-00-00-02-00-05-00-01-00-02-00-03-00-01-00-03-00-03-00-02-00-02-00-02-00-02-00-05-00-02-00-03-00-06-00-02-00-04-00-02-00-00-00-03-00-04-00-03-00-02-00-02-00-03-00-00-00-00-00-00-00-02-00-01-00-FE-FF-FE-FF-01-00-02-00-00-00-02-00-01-00-00-00-FF-FF-FE-FF-01-00-01-00-00-00-FE-FF-00-00-00-00-FF-FF-FF-FF-02-00-FF-FF-00-00-FF-FF-FE-FF-00-00-FE-FF-FC-FF-FF-FF-FD-FF-FB-FF-FD-FF-FE-FF-FF-FF-FF-FF-FE-FF-FF-FF-00-00-FF-FF-00-00-00-00-FE-FF-FE-FF-00-00-FE-FF-FE-FF-00-00-00-00-FF-FF-01-00-01-00-00-00-02-00-02-00-05-00-02-00-00-00-02-00-03-00-04-00-01-00-04-00-02-00-03-00-03-00-04-00-01-00-00-00-03-00-01-00-04-00-04-00-02-00-03-00-01-00-07-00-01-00-04-00-04-00-02-00-04-00-03-00-05-00-03-00-02-00-05-00-05-00-07-00-04-00-03-00-06-00-05-00-04-00-05-00-04-00-07-00-05-00-03-00-03-00-06-00-02-00-05-00-04-00-03-00-03-00-04-00-02-00-02-00-01-00-02-00-03-00-03-00-02-00-02-00-02-00-00-00-02-00-00-00-00-00-FF-FF-02-00-01-00-00-00-00-00-00-00-00-00-01-00-04-00-01-00-01-00-03-00-03-00-02-00-03-00-02-00-03-00-04-00-03-00-03-00-01-00-03-00-04-00-03-00-02-00-02-00-03-00-01-00-FF-FF-02-00-00-00-00-00-03-00-01-00-00-00-01-00-02-00-02-00-FF-FF-00-00-00-00-01-00-01-00-FE-FF-00-00-00-00-01-00-00-00-FF-FF-00-00-01-00-01-00-00-00-01-00-05-00-01-00-03-00-02-00-00-00-02-00-01-00-00-00-00-00-02-00-04-00-01-00-00-00-01-00-01-00-00-00-02-00-00-00-05-00-04-00-04-00-05-00-04-00-04-00-05-00-03-00-01-00-02-00-00-00-00-00-00-00-00-00-FF-FF-FF-FF-00-00-00-00-01-00-FD-FF-FF-FF-FF-FF-FF-FF-FC-FF-FE-FF-FF-FF-FF-FF-FD-FF-FE-FF-FC-FF-FF-FF-00-00-FF-FF-FF-FF-02-00-00-00-01-00-01-00-02-00-02-00-01-00-01-00-00-00-01-00-02-00-00-00-01-00-01-00-03-00-02-00-01-00-02-00-00-00-00-00-FF-FF-01-00-02-00-01-00-00-00-00-00-00-00-01-00-FF-FF-03-00-01-00-00-00-03-00-00-00-FE-FF-02-00-01-00-00-00-01-00-02-00-FF-FF-00-00-02-00-FF-FF-FF-FF-02-00-03-00-02-00-03-00-00-00-00-00-05-00-03-00-05-00-04-00-05-00-05-00-03-00-02-00-02-00-02-00-03-00-02-00-05-00-05-00-04-00-03-00-05-00-05-00-00-00-03-00-03-00-04-00-03-00-03-00-05-00-01-00-03-00-05-00-05-00-04-00-04-00-04-00-05-00-03-00-02-00-04-00-02-00-02-00-00-00-00-00-02-00-00-00-00-00-00-00-01-00-00-00-00-00-FF-FF-01-00-01-00-FE-FF-01-00-03-00-02-00-05-00-04-00-03-00-03-00-07-00-05-00-04-00-07-00-05-00-04-00-05-00-04-00-04-00-04-00-02-00-03-00-02-00-05-00-07-00-04-00-04-00-02-00-03-00-03-00-00-00-00-00-00-00-00-00-03-00-02-00-FE-FF-00-00-FF-FF-02-00-01-00-00-00-02-00-02-00-01-00-02-00-03-00-00-00-00-00-FF-FF-FF-FF-FF-FF-FE-FF-FC-FF-FF-FF-00-00-FF-FF-00-00-00-00-00-00-FE-FF-00-00-FE-FF-00-00-FE-FF-03-00-02-00-00-00-00-00-00-00-02-00-FF-FF-03-00-01-00-00-00-04-00-03-00-03-00-03-00-01-00-02-00-02-00-FF-FF-00-00-00-00-FF-FF-02-00-01-00-FF-FF-01-00-FF-FF-00-00-FE-FF-00-00-00-00-FF-FF-FE-FF-FE-FF-FE-FF-FF-FF-FE-FF-FF-FF-02-00-FF-FF-02-00-FF-FF-FF-FF-FF-FF-01-00-FF-FF-01-00-00-00-01-00-02-00-FF-FF-00-00-00-00-00-00-00-00-00-00-00-00-03-00-00-00-00-00-02-00-00-00-01-00-FE-FF-FF-FF-00-00-FF-FF-00-00-01-00-00-00-00-00-00-00-FE-FF-00-00-FE-FF-FE-FF-00-00-00-00-00-00-00-00-02-00-01-00-00-00-00-00-01-00-00-00-00-00-01-00-00-00-02-00-00-00-00-00-02-00-00-00-01-00-01-00-01-00-FF-FF-01-00-01-00-00-00-02-00-00-00-00-00-01-00-00-00-FF-FF-FF-FF-00-00-FE-FF-FF-FF-01-00-00-00-01-00-FF-FF-FF-FF-01-00-01-00-01-00-FF-FF-00-00-FF-FF-01-00-01-00-02-00-02-00-05-00-04-00-02-00-03-00-02-00-00-00-04-00-06-00-07-00-05-00-08-00-07-00-06-00-07-00-07-00-04-00-06-00-05-00-07-00-0A-00-09-00-06-00-08-00-06-00-08-00-0A-00-0A-00-0A-00-09-00-07-00-08-00-09-00-09-00-08-00-08-00-09-00-06-00-08-00-07-00-06-00-05-00-06-00-04-00-03-00-04-00-04-00-03-00-02-00-02-00-00-00-01-00-01-00-02-00-01-00-02-00-05-00-03-00-02-00-00-00-02-00-00-00-00-00-00-00-FF-FF-FE-FF-00-00-00-00-FF-FF-03-00-00-00-01-00-00-00-01-00-01-00-04-00-00-00-01-00-03-00-02-00-03-00-01-00-01-00-02-00-00-00-01-00-02-00-02-00-03-00-04-00-04-00-05-00-03-00-03-00-02-00-00-00-01-00-02-00-01-00-04-00-04-00-05-00-00-00-02-00-02-00-02-00-02-00-00-00-FE-FF-00-00-01-00-00-00-00-00-FE-FF-FF-FF-01-00-FF-FF-00-00-FE-FF-FE-FF-00-00-00-00-FF-FF-FD-FF-FE-FF-FF-FF-FF-FF-02-00-00-00-00-00-FF-FF-03-00-02-00-00-00-02-00-01-00-01-00-02-00-02-00-02-00-03-00-00-00-01-00-01-00-00-00-00-00-01-00-01-00-00-00-01-00-03-00-01-00-02-00-01-00-02-00-FF-FF-01-00-FF-FF-00-00-01-00-01-00-03-00-FF-FF-FF-FF-01-00-00-00-FF-FF-00-00-00-00-FF-FF-01-00-01-00-03-00-02-00-03-00-02-00-04-00-FF-FF-00-00-02-00-02-00-01-00-04-00-02-00-00-00-03-00-04-00-03-00-02-00-01-00-01-00-02-00-03-00-03-00-02-00-04-00-03-00-02-00-04-00-03-00-00-00-02-00-02-00-00-00-FF-FF-02-00-00-00-01-00-01-00-00-00-00-00-00-00-01-00-01-00-02-00-01-00-01-00-02-00-02-00-02-00-02-00-01-00-04-00-00-00-02-00-02-00-01-00-02-00-00-00-02-00-00-00-02-00-02-00-01-00-01-00-00-00-01-00-00-00-00-00-00-00-01-00-01-00-01-00-FF-FF-01-00-FE-FF-00-00-FF-FF-03-00-00-00-00-00-00-00-00-00-FF-FF-FE-FF-FF-FF-00-00-FF-FF-FE-FF-FF-FF-00-00-00-00-FF-FF-FD-FF-FA-FF-FD-FF-FC-FF-FB-FF-FC-FF-FD-FF-FE-FF-FF-FF-FF-FF-FC-FF-FE-FF-00-00-FF-FF-FF-FF-00-00-00-00-FF-FF-00-00-00-00-FD-FF-FF-FF-00-00-00-00-00-00-00-00-FF-FF-FE-FF-00-00-FF-FF-FD-FF-FE-FF-00-00-00-00-00-00-01-00-02-00-00-00-01-00-03-00-03-00-01-00-01-00-02-00-02-00-02-00-04-00-04-00-04-00-02-00-04-00-02-00-02-00-03-00-05-00-03-00-02-00-04-00-02-00-03-00-07-00-03-00-05-00-08-00-06-00-05-00-06-00-04-00-03-00-03-00-04-00-04-00-05-00-03-00-04-00-02-00-03-00-05-00-05-00-05-00-04-00-04-00-04-00-03-00-06-00-07-00-06-00-07-00-07-00-06-00-05-00-04-00-03-00-05-00-04-00-05-00-05-00-03-00-04-00-05-00-04-00-01-00-03-00-01-00-03-00-02-00-03-00-03-00-04-00-03-00-06-00-05-00-05-00-03-00-03-00-06-00-04-00-05-00-04-00-05-00-05-00-06-00-07-00-06-00-04-00-04-00-03-00-03-00-01-00-04-00-02-00-03-00-03-00-00-00-01-00-00-00-04-00-03-00-04-00-00-00-02-00-00-00-01-00-01-00-00-00-00-00-FF-FF-FE-FF-FC-FF-FC-FF-FF-FF-FE-FF-FF-FF-FD-FF-FF-FF-00-00-00-00-FE-FF-00-00-01-00-00-00-FE-FF-01-00-00-00-FE-FF-01-00-FF-FF-00-00-00-00-00-00-00-00-00-00-00-00-01-00-00-00-FE-FF-02-00-01-00-02-00-02-00-00-00-03-00-02-00-03-00-02-00-04-00-00-00-02-00-FF-FF-01-00-FF-FF-02-00-04-00-03-00-02-00-03-00-02-00-03-00-04-00-02-00-02-00-04-00-02-00-02-00-03-00-02-00-03-00-04-00-05-00-05-00-05-00-04-00-03-00-04-00-05-00-04-00-03-00-03-00-01-00-03-00-03-00-FF-FF-01-00-00-00-01-00-02-00-00-00-02-00-02-00-03-00-04-00-03-00-04-00-02-00-03-00-02-00-06-00-03-00-02-00-01-00-02-00-02-00-01-00-02-00-02-00-01-00-FF-FF-02-00-02-00-02-00-00-00-02-00-FE-FF-01-00-02-00-01-00-02-00-01-00-00-00-FF-FF-02-00-00-00-FD-FF-FF-FF-FF-FF-FE-FF-01-00-00-00-00-00-00-00-01-00-02-00-02-00-02-00-02-00-FE-FF-00-00-02-00-00-00-00-00-01-00-01-00-02-00-02-00-04-00-04-00-01-00-04-00-05-00-03-00-01-00-03-00-02-00-02-00-03-00-FF-FF-00-00-00-00-01-00-FF-FF-00-00-00-00-00-00-01-00-04-00-01-00-00-00-FF-FF-FD-FF-01-00-00-00-00-00-FF-FF-01-00-00-00-01-00-02-00-00-00-01-00-00-00-FF-FF-01-00-01-00-03-00-01-00-02-00-FE-FF-02-00-FF-FF-03-00-03-00-00-00-02-00-02-00-02-00-03-00-04-00-05-00-04-00-03-00-01-00-03-00-03-00-01-00-FF-FF-00-00-03-00-03-00-01-00-03-00-01-00-02-00-01-00-FF-FF-02-00-02-00-03-00-00-00-00-00-00-00-00-00-03-00-01-00-FF-FF-01-00-01-00-00-00-01-00-01-00-01-00-FE-FF-00-00-01-00-03-00-FF-FF-01-00-01-00-00-00-01-00-02-00-02-00-02-00-02-00-04-00-04-00-03-00-07-00-05-00-03-00-02-00-03-00-03-00-02-00-03-00-02-00-03-00-03-00-01-00-01-00-00-00-03-00-03-00-07-00-04-00-06-00-03-00-01-00-00-00-01-00-04-00-02-00-02-00-00-00-00-00-00-00-FF-FF-03-00-01-00-FF-FF-FF-FF-02-00-00-00-02-00-00-00-00-00-04-00-01-00-02-00-02-00-03-00-02-00-03-00-FF-FF-03-00-01-00-03-00-02-00-01-00-02-00-04-00-02-00-04-00-03-00-05-00-03-00-03-00-04-00-05-00-07-00-02-00-03-00-04-00-01-00-04-00-02-00-03-00-01-00-03-00-05-00-01-00-03-00-01-00-04-00-03-00-02-00-FF-FF-FF-FF-00-00-01-00-01-00-FF-FF-02-00-00-00-01-00-02-00-00-00-FE-FF-FE-FF-01-00-00-00-00-00-00-00-00-00-FF-FF-01-00-02-00-00-00-00-00-FE-FF-FE-FF-01-00-FE-FF-00-00-FF-FF-00-00-00-00-01-00-00-00-01-00-01-00-00-00-02-00-00-00-00-00-00-00-FF-FF-00-00-FF-FF-FF-FF-00-00-00-00-FF-FF-01-00-00-00-00-00-00-00-FF-FF-FF-FF-02-00-00-00-00-00-03-00-02-00-FF-FF-00-00-01-00-02-00-02-00-00-00-01-00-02-00-00-00-03-00-02-00-02-00-02-00-04-00-04-00-04-00-04-00-04-00-03-00-05-00-03-00-04-00-03-00-04-00-03-00-05-00-02-00-03-00-03-00-00-00-03-00-04-00-04-00-01-00-04-00-04-00-02-00-03-00-00-00-02-00-03-00-01-00-FF-FF-02-00-02-00-02-00-05-00-02-00-01-00-00-00-01-00-00-00-03-00-03-00-00-00-00-00-FF-FF-FF-FF-00-00-05-00-02-00-01-00-02-00-FF-FF-00-00-00-00-00-00-01-00-00-00-FE-FF-00-00-01-00-01-00-02-00-FE-FF-01-00-02-00-01-00-02-00-00-00-FF-FF-00-00-FE-FF-FF-FF-02-00-03-00-03-00-02-00-00-00-02-00-FF-FF-00-00-00-00-01-00-00-00-03-00-02-00-02-00-03-00-01-00-04-00-01-00-05-00-08-00-04-00-05-00-06-00-03-00-06-00-05-00-08-00-07-00-05-00-05-00-04-00-04-00-04-00-02-00-03-00-01-00-06-00-05-00-03-00-05-00-04-00-04-00-03-00-05-00-03-00-04-00-04-00-04-00-05-00-04-00-02-00-05-00-05-00-02-00-04-00-01-00-04-00-02-00-01-00-02-00-01-00-00-00-FF-FF-00-00-04-00-01-00-02-00-01-00-03-00-02-00-04-00-04-00-06-00-04-00-02-00-03-00-02-00-03-00-04-00-04-00-04-00-03-00-04-00-03-00-01-00-00-00-03-00-01-00-00-00-01-00-FD-FF-00-00-00-00-FE-FF-01-00-02-00-00-00-01-00-00-00-01-00-03-00-03-00-05-00-01-00-02-00-03-00-03-00-04-00-00-00-00-00-00-00-00-00-01-00-01-00-02-00-00-00-00-00-FE-FF-01-00-FF-FF-FF-FF-00-00-FD-FF-FF-FF-FD-FF-FD-FF-FC-FF-FF-FF-FF-FF-FF-FF-00-00-FF-FF-FE-FF-00-00-01-00-FE-FF-01-00-FF-FF-00-00-FF-FF-00-00-FC-FF-FE-FF-FC-FF-FF-FF-FF-FF-FE-FF-FF-FF-FE-FF-FF-FF-FE-FF-FE-FF-FD-FF-FE-FF-FC-FF-FD-FF-FF-FF-FE-FF-FE-FF-00-00-00-00-FF-FF-FE-FF-FE-FF-00-00-FC-FF-FD-FF-00-00-00-00-00-00-02-00-FE-FF-FF-FF-FF-FF-FF-FF-01-00-FE-FF-00-00-00-00-01-00-00-00-FF-FF-00-00-00-00-FF-FF-FE-FF-01-00-01-00-FF-FF-FE-FF-00-00-00-00-FF-FF-FE-FF-FE-FF-FF-FF-01-00-FE-FF-01-00-01-00-02-00-02-00-00-00-02-00-02-00-03-00-02-00-00-00-FF-FF-02-00-04-00-00-00-01-00-01-00-04-00-03-00-02-00-05-00-02-00-02-00-03-00-04-00-02-00-03-00-00-00-02-00-02-00-03-00-04-00-04-00-04-00-05-00-06-00-04-00-03-00-05-00-07-00-04-00-06-00-07-00-04-00-07-00-06-00-08-00-05-00-04-00-07-00-06-00-07-00-06-00-04-00-03-00-04-00-05-00-05-00-02-00-04-00-06-00-04-00-04-00-04-00-05-00-06-00-05-00-07-00-04-00-06-00-07-00-05-00-05-00-05-00-06-00-04-00-08-00-07-00-08-00-08-00-06-00-07-00-08-00-05-00-06-00-06-00-06-00-07-00-06-00-04-00-05-00-07-00-06-00-05-00-04-00-02-00-04-00-03-00-05-00-04-00-05-00-04-00-03-00-02-00-02-00-04-00-01-00-03-00-02-00-00-00-01-00-01-00-00-00-01-00-02-00-00-00-01-00-00-00-FF-FF-00-00-FF-FF-00-00-00-00-FF-FF-00-00-00-00-00-00-02-00-01-00-00-00-03-00-01-00-01-00-01-00-00-00-02-00-00-00-03-00-FF-FF-00-00-03-00-04-00-00-00-00-00-02-00-02-00-00-00-00-00-00-00-01-00-02-00-04-00-00-00-03-00-02-00-03-00-01-00-00-00-01-00-01-00-02-00-00-00-00-00-02-00-02-00-00-00-FF-FF-FF-FF-FE-FF-FF-FF-FE-FF-FF-FF-00-00-FE-FF-FF-FF-FF-FF-FF-FF-00-00-00-00-FE-FF-FF-FF-01-00-00-00-00-00-00-00-03-00-00-00-01-00-FE-FF-01-00-00-00-00-00-02-00-00-00-02-00-02-00-02-00-01-00-01-00-00-00-02-00-00-00-FF-FF-01-00-FF-FF-FE-FF-B3-6E-64"""
    
    # 下载采集工作站配置数据
    frameHexString = """62-65-01-00-00-00-98-09-00-00-90-06-04-00-00-5B-04-00-00-04-00-00-00-0A-03-04-6E-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-03-05-00-00-3C-00-00-00-58-02-00-00-08-00-00-00-52-42-0F-00-0D-00-00-00-01-00-00-00-31-5F-31-00-00-00-00-00-00-00-00-00-53-42-0F-00-0D-00-00-00-01-00-00-00-31-5F-32-00-00-00-00-00-00-00-00-00-54-42-0F-00-0D-00-00-00-01-00-00-00-31-5F-33-00-00-00-00-00-01-00-00-00-0D-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-01-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-55-42-0F-00-0D-00-00-00-01-00-00-00-31-5F-34-00-00-00-00-00-01-00-00-00-0D-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-01-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-56-42-0F-00-0D-00-00-00-01-00-00-00-31-5F-35-00-00-00-00-00-00-00-00-00-57-42-0F-00-0D-00-00-00-01-00-00-00-31-5F-36-00-00-00-00-00-00-00-00-00-58-42-0F-00-0D-00-00-00-01-00-00-00-31-5F-37-00-00-00-00-00-00-00-00-00-59-42-0F-00-0D-00-00-00-01-00-00-00-31-5F-38-00-00-00-00-00-00-00-00-00-10-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-31-5F-31-00-00-00-00-00-33-5F-31-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-01-00-00-00-01-00-00-00-01-00-00-00-65-00-00-00-00-00-20-45-00-00-00-00-00-08-00-00-00-00-00-00-3C-00-00-00-70-17-00-00-E8-03-00-00-00-00-00-00-00-00-80-3F-00-40-1C-46-00-00-00-00-05-00-00-00-0B-00-00-00-00-00-00-00-0C-00-00-00-00-00-00-00-0D-00-00-00-00-00-00-00-0F-00-00-00-00-00-00-00-10-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-31-5F-32-00-00-00-00-00-33-5F-31-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-01-00-00-00-01-00-00-00-02-00-00-00-65-00-00-00-00-00-20-45-00-00-00-00-00-08-00-00-00-00-00-00-3C-00-00-00-70-17-00-00-E8-03-00-00-00-00-00-00-00-00-80-3F-00-40-1C-46-00-00-00-00-05-00-00-00-0B-00-00-00-00-00-00-00-0C-00-00-00-00-00-00-00-0D-00-00-00-00-00-00-00-0F-00-00-00-00-00-00-00-10-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-31-5F-33-00-00-00-00-00-33-5F-31-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-01-00-00-00-01-00-00-00-03-00-00-00-65-00-00-00-00-00-20-45-00-00-00-00-00-08-00-00-00-00-00-00-3C-00-00-00-70-17-00-00-E8-03-00-00-00-00-00-00-00-00-80-3F-00-40-1C-46-00-00-00-00-05-00-00-00-0B-00-00-00-14-00-00-00-0C-00-00-00-00-00-00-00-0D-00-00-00-14-00-00-00-0F-00-00-00-00-00-00-00-10-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-31-5F-34-00-00-00-00-00-33-5F-31-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-01-00-00-00-01-00-00-00-04-00-00-00-65-00-00-00-00-00-20-45-00-00-00-00-00-08-00-00-00-00-00-00-3C-00-00-00-70-17-00-00-E8-03-00-00-00-00-00-00-00-00-80-3F-00-40-1C-46-00-00-00-00-05-00-00-00-0B-00-00-00-14-00-00-00-0C-00-00-00-00-00-00-00-0D-00-00-00-14-00-00-00-0F-00-00-00-00-00-00-00-10-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-31-5F-35-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-01-00-00-00-01-00-00-00-05-00-00-00-65-00-00-00-00-00-20-45-00-00-00-00-00-08-00-00-00-00-00-00-3C-00-00-00-70-17-00-00-E8-03-00-00-00-00-00-00-00-00-80-3F-00-40-1C-46-00-00-00-00-05-00-00-00-0B-00-00-00-00-00-00-00-0C-00-00-00-00-00-00-00-0D-00-00-00-00-00-00-00-0F-00-00-00-00-00-00-00-10-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-31-5F-36-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-01-00-00-00-01-00-00-00-06-00-00-00-65-00-00-00-00-00-20-45-00-00-00-00-00-08-00-00-00-00-00-00-3C-00-00-00-70-17-00-00-E8-03-00-00-00-00-00-00-00-00-80-3F-00-40-1C-46-00-00-00-00-05-00-00-00-0B-00-00-00-00-00-00-00-0C-00-00-00-00-00-00-00-0D-00-00-00-00-00-00-00-0F-00-00-00-00-00-00-00-10-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-31-5F-37-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-01-00-00-00-01-00-00-00-07-00-00-00-65-00-00-00-00-00-20-45-00-00-00-00-00-08-00-00-00-00-00-00-3C-00-00-00-70-17-00-00-E8-03-00-00-00-00-00-00-00-00-80-3F-00-40-1C-46-00-00-00-00-05-00-00-00-0B-00-00-00-00-00-00-00-0C-00-00-00-00-00-00-00-0D-00-00-00-00-00-00-00-0F-00-00-00-00-00-00-00-10-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-31-5F-38-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-01-00-00-00-01-00-00-00-08-00-00-00-65-00-00-00-00-00-20-45-00-00-00-00-00-08-00-00-00-00-00-00-3C-00-00-00-70-17-00-00-E8-03-00-00-00-00-00-00-00-00-80-3F-00-40-1C-46-00-00-00-00-05-00-00-00-0B-00-00-00-00-00-00-00-0C-00-00-00-00-00-00-00-0D-00-00-00-00-00-00-00-0F-00-00-00-00-00-00-00-10-00-00-00-00-00-00-00-00-AA-AA-AA-AA-AA-AA-AA-AA-AA-AA-AB-32-5F-31-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-01-00-00-00-02-00-00-00-01-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-80-3F-00-00-C8-42-00-00-00-00-01-00-00-00-0A-00-00-00-00-00-00-00-00-AA-AA-AA-AA-AA-AA-AA-AA-AA-AA-AC-33-5F-31-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-01-00-00-00-03-00-00-00-01-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-02-00-00-00-00-00-80-3F-00-00-80-3F-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-35-5F-31-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-01-00-00-00-05-00-00-00-01-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-80-3F-00-00-80-3F-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-35-5F-32-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-05-00-00-00-02-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-80-3F-00-00-80-3F-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-35-5F-33-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-05-00-00-00-03-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-80-3F-00-00-80-3F-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-35-5F-34-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-05-00-00-00-04-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-80-3F-00-00-80-3F-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-35-5F-35-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-01-00-00-00-05-00-00-00-05-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-80-3F-00-00-80-3F-00-00-00-00-00-00-00-00-00-AA-AA-AA-AA-AA-AA-AA-AA-AA-AA-AD-35-5F-36-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-01-00-00-00-05-00-00-00-06-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-80-3F-00-00-80-3F-00-00-00-00-00-00-00-00-DD-6E-64"""
    
    parser = ParserFor2642(frameHexString)
    parser.printParseResult()