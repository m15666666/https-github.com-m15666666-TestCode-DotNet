--ϵͳ��������
--BS_AppConfig����
--ControlType_CD����TextBox��ComboBox
--Other_TXΪ������Ϣ������Numeric����������˵���Դ�����ݷ�Χ����ʽΪ:��Сֵ|���ֵ �� 100|1000��
--����ControlType_CD����ComboBox��������˵Other_TX���Դ�����������ݣ���ʽΪ:ֵ1;ֵ2;ֵ3|��ʾ1;��ʾ2;��ʾ3 �� 0;1;2|������;������;·��)
--SysConfig_YN = '1'��������Ϣ���û���˵���ɼ���ϵͳ�ڲ�ʹ��

Delete From BS_AppConfig Where AppConfig_CD = 'ProductName';
Delete From BS_AppConfig Where AppConfig_CD = 'ZXDBVERSION';
Delete From BS_AppConfig Where SortNo_NR > 200000;


-------------------------'��������Զ�������(��Ŵ�200801��ʼ)----------------------------------------------

Insert Into BS_AppConfig(AppConfig_CD,Name_TX,Value_TX,DefaultValue_TX,DataType_CD,Category_CD,SysConfig_YN,SortNo_NR,ControlType_CD,Other_TX)  
  Values('ProductName','��Ʒ����','С��̽�豸״̬�����Զ��������',Null,'String','��������Զ�������','0',200801,'TextBox',Null);
 
 
  
-------------------------����������(��Ŵ�200901��ʼ)----------------------------------------------

Insert Into BS_AppConfig(AppConfig_CD,Name_TX,Value_TX,DefaultValue_TX,DataType_CD,Category_CD,SysConfig_YN,SortNo_NR,ControlType_CD,Other_TX)  
	Values('TrendsSingleTimeMaxRecCount','������������ѯ��¼��','3000',Null,'Numeric','����������','0',200901,'TextBox','100|10000');

Insert Into BS_AppConfig(AppConfig_CD,Name_TX,Value_TX,DefaultValue_TX,DataType_CD,Category_CD,SysConfig_YN,SortNo_NR,ControlType_CD,Other_TX)  
	Values('HistoryAlarmMaxRecCount','������¼�����ʾ����','50',Null,'Numeric','����������','0',200902,'TextBox','10|255');

Insert Into BS_AppConfig(AppConfig_CD,Name_TX,Value_TX,DefaultValue_TX,DataType_CD,Category_CD,SysConfig_YN,SortNo_NR,ControlType_CD,Other_TX)  
	Values('DataQueryMaxMonth','�������ݲ�ѯʱ�䷶Χ���£�','6',Null,'Numeric','����������','0',200903,'TextBox','1|6');

Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
	Values('ZXDBVERSION', '�������ݿ�汾��', '1.1.2.141202', null, 'String', '���ݿ�����', '1', 4, 'TextBox', null);

Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
	Values('ZXSMSTel', '�ƶ�����ѯ����', '10086', null, 'String', '���Ų�ѯ', '0', 200904, 'TextBox', null);

Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
	Values('ZXSMSCode', '�ƶ�����ѯ����', '���', null, 'String', '���Ų�ѯ', '0', 200905, 'TextBox', null);

Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
	Values('ZXSMSContent', '�������Ÿ�ʽ(C/D/M/P/L/V/T/N)', 'L/M/P/V/T', null, 'String', '���Ų�ѯ', '0', 200906, 'TextBox', null);

Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
	Values('ZXSMSPlatformUrl', '����ƽ̨��ַ', '����ƽ̨��ַ', null, 'String', '���Ų�ѯ', '0', 200907, 'TextBox', null);

Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
	Values('ZXSMSPlatformUserId', '����ƽ̨�û����', '����ƽ̨�û����', null, 'String', '���Ų�ѯ', '0', 200908, 'TextBox', null);

Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
	Values('ZXSMSPlatformPassword', '����ƽ̨�û�����', '����ƽ̨�û�����', null, 'Password', '���Ų�ѯ', '0', 200909, 'TextBox', null);

Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX)
	Values('ZXSMSPlatformType', '����ƽ̨����', '����è', null, 'String', '���Ų�ѯ', '0', 200910, 'ComboBox', '����è;�ϸֶ���ƽ̨|����è;�ϸֶ���ƽ̨');

Insert Into BS_AppConfig(AppConfig_CD,Name_TX,Value_TX,DefaultValue_TX,DataType_CD,Category_CD,SysConfig_YN,SortNo_NR,ControlType_CD,Other_TX)	
	Values('ZXMObjectCDInherit','�����豸�����Ƿ�̳�','��','��','String','�����豸����','0',200911,'ComboBox','��;��|��;��');

commit;