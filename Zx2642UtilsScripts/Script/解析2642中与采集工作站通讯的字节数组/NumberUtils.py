# -*- coding: utf-8 -*-
"""
NumberUtils
数字的实用工具类
"""

#from struct import *
import struct

isBigEndian = True

# 特殊的格式串前缀
__SpecialFormatPrefixs = {
            '@': 1,
            '=': 1,
            '<': 1,
            '>': 1,
            '!': 1,
            };

__fmt_Int16 = 'h'
__fmt_Int32 = 'i'
__fmt_Single = 'f'

def __getFmtWithPrefix(fmt):
    """获得加上了前缀的格式字符串"""
    #if(__SpecialFormatPrefixs.has_key(fmt[0])):
    if(fmt[0] in __SpecialFormatPrefixs):
        return fmt
    return ( '>' if isBigEndian else '<' ) + fmt

def floats2Bytes(values):
    """将float数组转化为字节(整数)数组"""
    return values2Bytes(values, __fmt_Single)

def ints2Bytes(values):
    """将int数组转化为字节(整数)数组"""
    return values2Bytes(values, __fmt_Int32)

def shorts2Bytes(values):
    """将short数组转化为字节(整数)数组"""
    return values2Bytes(values, __fmt_Int16)

def values2Bytes(values, fmt):
    """将T类型数组转化为字节(整数)数组"""
    chars = []
    fmt = __getFmtWithPrefix(fmt)
    for v in values:
        chars.extend(struct.pack(fmt, v))
    return chars2Bytes(chars)

def hexBytes2String(values):
    """将十六进制字符串数组转化为字符串"""
    retChars = []
    for v in hexBytes2Bytes(values):
        if v == 0:
            break;
        retChars.append( chr( v ) )
    return ''.join( retChars )
    
def hexBytes2Floats(values):
    """将十六进制字符串数组转化为float数组"""
    return hexBytes2Values(values, __fmt_Single, 4)

def hexBytes2Int(values):
    """将十六进制字符串数组转化为int"""
    retBytes= []
    retBytes.extend(values)
    if( not isBigEndian ):
        retBytes.reverse()
    retString = ''.join(retBytes)
    return int(retString, 16)

def hexBytes2Ints(values):
    """将十六进制字符串数组转化为int数组"""
    return hexBytes2Values(values, __fmt_Int32, 4)

def hexBytes2Shorts(values):
    """将十六进制字符串数组转化为short数组"""
    return hexBytes2Values(values, __fmt_Int16, 2)
    
def hexBytes2Values(values, fmt, byteCountPerItem):
    """将十六进制字符串数组转化为float数组"""
    itemCount = (int)(len(values) / byteCountPerItem)
    fmt2 = ''
    for index in range(itemCount):
        fmt2 += fmt
    #fmt = __getFmtWithPrefix(fmt * ( len(values) / byteCountPerItem ) )
    fmt = __getFmtWithPrefix(fmt2)
    #chars = [chr(v) for v in hexBytes2Bytes( values )]
    #return struct.unpack(fmt, ''.join(chars))
    buffer = bytes(hexBytes2Bytes( values ))
    return struct.unpack(fmt, buffer)
    
def chars2Bytes(chars):
    """将字符数组转化为字节(整数)数组"""
    return [ord(v) for v in chars]

def hexBytes2Bytes(hexBytes):
    """将十六进制字符串数组转化为字节(整数)数组"""
    return [int(v, 16) for v in hexBytes]
    
def bytes2HexBytes(byteValues):
    """将字节(整数)数组转化为十六进制字符串数组"""
    #r = []
    #for v in byteValues:
    #    if v < 16:
    #        r.append("0%X" %v)
    #    else:
    #        r.append("%X" %v)
    return ["0%X" %v if v < 16 else "%X" %v for v in byteValues]
    
def bytes2HexString(byteValues):
    """将字节(整数)数组转化为十六进制字符串"""
    return "".join(bytes2HexBytes(byteValues))
    
if __name__ == '__main__':
    #print isBigEndian
    isBigEndian = False
    print (hexBytes2String(['30', '48', '00', '20', '45']))
    print (hexBytes2Floats(['00', '00', '20', '45']))
    print (hexBytes2Ints(['00', '00', '20', '45']))
    print (chars2Bytes('01234'))
    print (hexBytes2Bytes(['01', '00', '0a', '0F']))
    print (bytes2HexString([1,2,13]))
    print (__getFmtWithPrefix('f'))
    print (ints2Bytes([1]))
    print ('ok')