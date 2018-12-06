--系统参数配置
--BS_AppConfig设置

--ControlType_CD包括TextBox和ComboBox
--Other_TX为附件信息，对于Numeric类型数据来说可以存放数据范围（格式为:最小值|最大值 如 100|1000）

--对于ControlType_CD等于ComboBox的数据来说Other_TX可以存放下拉框内容（格式为:值1;值2;值3|显示1;显示2;显示3 如 0;1;2|管理部门;管理组;路线)
--SysConfig_YN = '1'的配置信息对用户来说不可见，系统内部使用


-------------------------'软件界面自定义数据(序号从200801开始)----------------------------------------------
Delete From BS_AppConfig Where AppConfig_CD = 'ProductName';
Delete From BS_AppConfig Where AppConfig_CD = 'ZXDBVERSION';
Delete From BS_AppConfig Where SortNo_NR > 200000;

Insert Into BS_AppConfig(AppConfig_CD,Name_TX,Value_TX,DefaultValue_TX,DataType_CD,Category_CD,SysConfig_YN,SortNo_NR,ControlType_CD,Other_TX)  
  Values('ProductName','产品名称','小神探设备状态监测与远程诊断软件',Null,'String','软件界面自定义数据','0',200801,'TextBox',Null);
  
  -------------------------监测分析设置(序号从200901开始)----------------------------------------------
Insert Into BS_AppConfig(AppConfig_CD,Name_TX,Value_TX,DefaultValue_TX,DataType_CD,Category_CD,SysConfig_YN,SortNo_NR,ControlType_CD,Other_TX)  
	Values('TrendsSingleTimeMaxRecCount','趋势数据最多查询记录数','3000',Null,'Numeric','监测分析设置','0',200901,'TextBox','100|10000');

Insert Into BS_AppConfig(AppConfig_CD,Name_TX,Value_TX,DefaultValue_TX,DataType_CD,Category_CD,SysConfig_YN,SortNo_NR,ControlType_CD,Other_TX)  
	Values('HistoryAlarmMaxRecCount','报警记录最多显示条数','50',Null,'Numeric','监测分析设置','0',200902,'TextBox','10|255');

Insert Into BS_AppConfig(AppConfig_CD,Name_TX,Value_TX,DefaultValue_TX,DataType_CD,Category_CD,SysConfig_YN,SortNo_NR,ControlType_CD,Other_TX)  
	Values('DataQueryMaxMonth','在线数据查询时间范围（月）','6',Null,'Numeric','监测分析设置','0',200903,'TextBox','1|6');

Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
	Values('ZXDBVERSION', '在线数据库版本号', '1.1.2.141202', null, 'String', '数据库设置', '1', 4, 'TextBox', null);

Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
	Values('ZXSMSTel', '移动余额查询号码', '10086', null, 'String', '短信查询', '0', 200904, 'TextBox', null);

Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
	Values('ZXSMSCode', '移动余额查询代码', '余额', null, 'String', '短信查询', '0', 200905, 'TextBox', null);

Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
	Values('ZXSMSContent', '报警短信格式(C/D/M/P/L/V/T/N)', 'L/M/P/V/T', null, 'String', '短信查询', '0', 200906, 'TextBox', null);

Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
	Values('ZXSMSPlatformUrl', '短信平台网址', '短信平台网址', null, 'String', '短信查询', '0', 200907, 'TextBox', null);

Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
	Values('ZXSMSPlatformUserId', '短信平台用户编号', '短信平台用户编号', null, 'String', '短信查询', '0', 200908, 'TextBox', null);

Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
	Values('ZXSMSPlatformPassword', '短信平台用户密码', '短信平台用户密码', null, 'Password', '短信查询', '0', 200909, 'TextBox', null);

Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX)
	Values('ZXSMSPlatformType', '短信平台类型', '短信猫', null, 'String', '短信查询', '0', 200910, 'ComboBox', '短信猫;南钢短信平台|短信猫;南钢短信平台');

Insert Into BS_AppConfig(AppConfig_CD,Name_TX,Value_TX,DefaultValue_TX,DataType_CD,Category_CD,SysConfig_YN,SortNo_NR,ControlType_CD,Other_TX)	
	Values('ZXMObjectCDInherit','在线设备编码是否继承','是','是','String','在线设备管理','0',200911,'ComboBox','是;否|是;否');

 GO