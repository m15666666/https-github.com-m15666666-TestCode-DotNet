/********添加在线数据库初始版本号1.0.0.000000**********/
declare 
	V_Count BS_MetaFieldType.Int_NR%type;
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
begin

	V_DBType := 'ZXDBVERSION';

	--添加在线数据库版本号
    V_Count := 0;
    select count(*) into V_Count from BS_AppConfig where AppConfig_CD = V_DBType;
	if V_Count = 0 then
		insert into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
			values(V_DBType, '在线数据库版本号', '1.0.0.000000', null, 'String', '数据库设置', '1', 4, 'TextBox', null);
	end if;

    commit;	
	
end;
/

/********上一个版本:1.0.0.000000**********/
/**********当前版本:1.0.1.120509**********/
declare
	Type T_Cursor Is Ref Cursor;
	V_Sql BS_MetaFieldType.FiveKilo_TX%type;
	V_Count BS_MetaFieldType.Int_NR%type;
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
	V_SummaryTableBaseName BS_MetaFieldType.OneHundred_TX%type;
	V_SummaryTableName BS_MetaFieldType.OneHundred_TX%type;
	V_TableBaseName BS_MetaFieldType.OneHundred_TX%type;
	V_PartitionName BS_MetaFieldType.OneHundred_TX%type;
	V_TableName BS_MetaFieldType.OneHundred_TX%type;
	V_NewTableName BS_MetaFieldType.OneHundred_TX%type;
	V_MaxID  BS_MetaFieldType.BigInt_NR%type;
	V_TableMaxID  BS_MetaFieldType.BigInt_NR%type;
	V_ConstraintName BS_MetaFieldType.OneHundred_TX%type;
	V_PrimaryKeyName BS_MetaFieldType.TwoHundred_TX%type;
	V_emsg BS_MetaFieldType.FiveKilo_TX%type;
	cur T_Cursor;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.0.1.120509';

	--1.0.1.120509版本数据库升级脚本，主要升级在线历史数据表相关的多张表格，包括其对应的分区表
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then

		V_SummaryTableBaseName := 'ZX_History_Summary';



		--*****************升级在线历史数据特征值表ZX_History_FeatureValue及其分区表*****************--
		V_MaxID := 1000000;
		V_TableBaseName := 'ZX_History_FeatureValue';

		--获取主键约束名称
		V_ConstraintName := 'ZXFeatvalue';
		V_Count := 0;
		select count(*) into V_Count from BS_RangePartitionMeta where upper(TableBaseName_TX) = upper(V_TableBaseName);
		if V_Count > 0 then
			select ConstraintName_TX into V_ConstraintName from BS_RangePartitionMeta where upper(TableBaseName_TX) = upper(V_TableBaseName);
		end if;

		--如果主键值在表中不存在，则插入主键值配置项，存在则取出主键ID值
		V_Count := 0;
		select count(*) into V_Count from BS_BigObjectKey where Source_CD = V_TableBaseName and KeyName = 'FeatureValuePK_ID';
		if V_Count = 0 then
			insert into  BS_BigObjectKey(Source_CD, KeyName, KeyValue, LockTime_TM) 
				values (V_TableBaseName, 'FeatureValuePK_ID', V_MaxID, DBDiffPackage.GetSystemTime());
		else
			select KeyValue into V_MaxID from BS_BigObjectKey where Source_CD = V_TableBaseName and KeyName = 'FeatureValuePK_ID';
		end if;

		--升级ZX_History_FeatureValue的分区表
		for V_DataRow in ( select TableName_TX from BS_RangePartition where upper(TableBaseName_TX) = upper(V_TableBaseName) order by TableBaseName_TX, PartitionMin_ID ) loop
			
			V_TableName := V_DataRow.TableName_TX;
			
			--新增字段：
			--FeatureValuePK_ID	特征值主键编号	bigint	8	PK,Not Null	表示一条特征值数据的编号
			--FeatureValueType_ID	特征值类型ID	int	4	Not Null	0：正常（502的非包数据类型存入时都使用该类型）。102：Packs的加速度，101：Packs的速度，103：Packs的位移（502的包数据类型存入时都使用该类型）。
			--SigType_NR	信号类型	Int	4	Not Null	502（普通/阶比：101速度、102加速度、103位移、113电流、118电压。解调：102加速度。速度包内项目：101。加速度包内项目：102。位移包内项目：103。解调包内项目：102。高频包内项目：102）。
			--EngUnit_ID	工程单位ID	Int	4	Not Null	根据信号类型来确定单位
			execute immediate 'alter table ' || V_TableName || ' add FeatureValuePK_ID number(19) default 0 not null';
			execute immediate 'alter table ' || V_TableName || ' add FeatureValueType_ID integer default 0 not null';
			execute immediate 'alter table ' || V_TableName || ' add SigType_NR integer default 0 not null';
			execute immediate 'alter table ' || V_TableName || ' add EngUnit_ID integer default 0 not null';

			--为字段FeatureValuePK_ID设置顺序号，使用ROWNUM实现批量修改
			execute immediate 'update ' || V_TableName || ' set FeatureValuePK_ID = ( select b.RowNumber + ' || V_MaxID || ' from ( select History_ID, ChnNo_NR, FeatureValue_ID, RowNum as RowNumber from ' || V_TableName || ' order by History_ID, ChnNo_NR, FeatureValue_ID ) b where b.History_ID = ' || V_TableName || '.History_ID and b.ChnNo_NR = ' || V_TableName || '.ChnNo_NR and b.FeatureValue_ID = ' || V_TableName || '.FeatureValue_ID )';

			--更新SigType_NR，EngUnit_ID字段内容
			V_PartitionName := DBDiffPackage.GetSubstring( V_TableName, DBDiffPackage.GetLength( V_TableBaseName ) + 1, 6 );
			V_SummaryTableName := V_SummaryTableBaseName || V_PartitionName;
			execute immediate 'update ' || V_TableName || ' set SigType_NR = ( select case when b.SigType_NR is null then 0 else b.SigType_NR end from ' || V_SummaryTableName || ' b where b.History_ID = ' || V_TableName || '.History_ID )';
			execute immediate 'update ' || V_TableName || ' set EngUnit_ID = ( select case when b.EngUnit_ID is null then 0 else b.EngUnit_ID end from ' || V_SummaryTableName || ' b where b.History_ID = ' || V_TableName || '.History_ID )';

			--修改字段：
			--History_ID：取消联合主键，保留FK,Not Null
			--ChnNo_NR：取消联合主键，保留Not Null
			--FeatureValue_ID：取消联合主键，保留Not Null
			--FeatureValuePK_ID：设置特征值主键编号
			V_PrimaryKeyName := 'PK_' || V_ConstraintName || V_PartitionName;
			execute immediate 'alter table ' || V_TableName || ' drop constraint ' || V_PrimaryKeyName;
			execute immediate 'alter table ' || V_TableName || ' add constraint ' || V_PrimaryKeyName || ' primary key ( FeatureValuePK_ID )';

			--使用当前分区表FeatureValuePK_ID字段最大值更新V_MaxID值
			execute immediate 'select case when max( FeatureValuePK_ID ) is null then 0 else max( FeatureValuePK_ID ) end from ' || V_TableName into V_TableMaxID;
			if V_TableMaxID > 0 then
				V_MaxID := V_TableMaxID;
			end if;

			--修改分区表表名：
			--ZX_History_Featurevalue******改为ZX_History_FeatureValue******
			V_NewTableName := V_TableBaseName || V_PartitionName;
			execute immediate 'alter table ' || V_TableName || ' rename to ' || V_NewTableName;

	    end loop;


		--开始调整非分区表ZX_History_FeatureValue
		--新增非分区表字段
		execute immediate 'alter table ' || V_TableBaseName || ' add FeatureValuePK_ID number(19) default 0 not null';
		execute immediate 'alter table ' || V_TableBaseName || ' add FeatureValueType_ID integer default 0 not null';
		execute immediate 'alter table ' || V_TableBaseName || ' add SigType_NR integer default 0 not null';
		execute immediate 'alter table ' || V_TableBaseName || ' add EngUnit_ID integer default 0 not null';

		--为非分区表字段FeatureValuePK_ID设置顺序号，使用ROWNUM实现批量修改
		execute immediate 'update ' || V_TableBaseName || ' set FeatureValuePK_ID = ( select b.RowNumber + ' || V_MaxID || ' from ( select History_ID, ChnNo_NR, FeatureValue_ID, RowNum as RowNumber from ' || V_TableBaseName || ' order by History_ID, ChnNo_NR, FeatureValue_ID ) b where b.History_ID = ' || V_TableBaseName || '.History_ID and b.ChnNo_NR = ' || V_TableBaseName || '.ChnNo_NR and b.FeatureValue_ID = ' || V_TableBaseName || '.FeatureValue_ID )';

		--更新非分区表SigType_NR，EngUnit_ID字段内容
		execute immediate 'update ' || V_TableBaseName || ' set SigType_NR = ( select case when b.SigType_NR is null then 0 else b.SigType_NR end from ' || V_SummaryTableBaseName || ' b where b.History_ID = ' || V_TableBaseName || '.History_ID )';
		execute immediate 'update ' || V_TableBaseName || ' set EngUnit_ID = ( select case when b.EngUnit_ID is null then 0 else b.EngUnit_ID end from ' || V_SummaryTableBaseName || ' b where b.History_ID = ' || V_TableBaseName || '.History_ID )';

		--修改非分区表字段
		V_PrimaryKeyName := upper('PK_' || V_TableBaseName);
		execute immediate 'alter table ' || V_TableBaseName || ' drop constraint ' || V_PrimaryKeyName;
		execute immediate 'alter table ' || V_TableBaseName || ' add constraint ' || V_PrimaryKeyName || ' primary key ( FeatureValuePK_ID )';

		--使用非分区表FeatureValuePK_ID字段最大值更新V_MaxID值
		execute immediate 'select case when max( FeatureValuePK_ID ) is null then 0 else max( FeatureValuePK_ID ) end from ' || V_TableBaseName into V_TableMaxID;
		if V_TableMaxID > 0 then
			V_MaxID := V_TableMaxID;
		end if;

		--更新BigObjectKey中关于ZX_History_FeatureValue表主键值
		update BS_BigObjectKey set KeyValue = V_MaxID where Source_CD = V_TableBaseName and KeyName = 'FeatureValuePK_ID';



		--*****************升级在线历史数据频谱表ZX_History_Freq及其分区表*****************--
		V_TableBaseName := 'ZX_History_Freq';

		--升级ZX_History_Freq的分区表
		for V_DataRow in ( select TableName_TX from BS_RangePartition where upper(TableBaseName_TX) = upper(V_TableBaseName) order by TableBaseName_TX, PartitionMin_ID ) loop
			
			V_TableName := V_DataRow.TableName_TX;

			--删除分区表
			execute immediate 'drop table ' || V_TableName || ' cascade constraints';

		end loop;


		--开始调整非分区表ZX_History_Freq
		--删除非分区表
		execute immediate 'drop table ' || V_TableBaseName || ' cascade constraints';



		--*****************修改表BS_BigObjectKey中的数据*****************--
		--增加ZX_History_Summary.Synch_NR键值记录
		V_MaxID := 1000000;
		V_Count := 0;
		select count(*) into V_Count from BS_BigObjectKey where Source_CD = V_SummaryTableBaseName and KeyName = 'Synch_NR';
		if V_Count = 0 then
			insert into  BS_BigObjectKey(Source_CD, KeyName, KeyValue, LockTime_TM) 
				values (V_SummaryTableBaseName, 'Synch_NR', V_MaxID, DBDiffPackage.GetSystemTime());
		end if;
		
		--删除ZX_History_Freq键值记录
		delete from BS_BigObjectKey where Source_CD = 'ZX_History_Freq';



		--*****************清理分区信息*****************--
		--修改分区表表名：更新BS_RangePartition对应的记录（TableBaseName_TX、TableName_TX两个字段中ZX_History_Featurevalue均改为ZX_History_FeatureValue）
		update BS_RangePartition set TableBaseName_TX = 'ZX_History_FeatureValue' where upper(TableBaseName_TX) = upper('ZX_History_Featurevalue');
		update BS_RangePartition set TableName_TX = TableBaseName_TX || DBDiffPackage.GetSubstring( TableName_TX, DBDiffPackage.GetLength( TableBaseName_TX ) + 1, 6 ) where upper(TableBaseName_TX) = upper('ZX_History_Featurevalue');

		--删除分区相关表中的数据
		delete from BS_TableColumns;
		delete from BS_CreateTableIndexInfo;
		delete from BS_CreateTableInfo;
		delete from BS_RangePartition where TableBaseName_TX = 'ZX_History_Freq';
		delete from BS_RangePartitionMeta where TableBaseName_TX in ( 'ZX_History_Summary', 'ZX_History_Waveform', 'ZX_History_Freq', 'ZX_History_Featurevalue' );
		delete from BS_TransPartionTable where TransKey_CD = 'ZX_HistoryData';

		--初始化分区相关表中的数据
		insert into BS_RangePartitionMeta (TableBaseName_TX,PartitionField_TX,Category_CD,ConstraintName_TX)
			values('ZX_History_Summary','Partition_ID','在线摘要数据','ZXSummary');
		insert into BS_RangePartitionMeta (TableBaseName_TX,PartitionField_TX,Category_CD,ConstraintName_TX)
			values('ZX_History_Waveform','Partition_ID','在线波形数据','ZXWaveform');
		insert into BS_RangePartitionMeta (TableBaseName_TX,PartitionField_TX,Category_CD,ConstraintName_TX)
			values('ZX_History_FeatureValue','Partition_ID','在线指标数据','ZXFeatvalue');

		insert into BS_TransPartionTable(TransKey_CD,TableBaseNameList_TX,ViewBaseNameList_TX,PartitionType_CD,PartitionType_NR,MoveAfterDays,MoveType)
			values('ZX_HistoryData','ZX_History_Summary,ZX_History_Waveform,ZX_History_FeatureValue','','M',1,'2,2,2','1');

		--初始化分区数据
		PartitionPackage.InitTableStruct( v_emsg );



		--完成升级，更新在线数据库版本号
	    update BS_appconfig set value_TX = V_DBVersion where appconfig_CD = V_DBType;

		commit;
	
	end if;
 
end;
/

/********上一个版本:1.0.1.120509**********/
/**********当前版本:1.0.2.120518**********/
declare
	V_Count BS_MetaFieldType.Int_NR%type;
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
	V_MaxID  BS_MetaFieldType.BigInt_NR%type;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.0.2.120518';

	--1.0.2.120518版本数据库升级脚本，主要用于增加测点分组表
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then

		--*****************修改表BS_ObjectKey中的数据*****************--
		--增加Pnt_PointGroup.PointGroup_ID键值记录
		V_MaxID := 1000000;
		V_Count := 0;
		select count(*) into V_Count from BS_ObjectKey where Source_CD = 'Pnt_PointGroup' and KeyName = 'PointGroup_ID';
		if V_Count = 0 then
			insert into  BS_ObjectKey(Source_CD, KeyName, KeyValue, LockTime_TM) 
				values ('Pnt_PointGroup', 'PointGroup_ID', V_MaxID, DBDiffPackage.GetSystemTime());
		end if;
		
		--增加Pnt_PointGroupNo.GroupNo_NR键值记录
		V_MaxID := 0;
		V_Count := 0;
		select count(*) into V_Count from BS_ObjectKey where Source_CD = 'Pnt_PointGroupNo' and KeyName = 'GroupNo_NR';
		if V_Count = 0 then
			insert into  BS_ObjectKey(Source_CD, KeyName, KeyValue, LockTime_TM) 
				values ('Pnt_PointGroupNo', 'GroupNo_NR', V_MaxID, DBDiffPackage.GetSystemTime());
		end if;
		


		--*****************新增表Pnt_PointGroup*****************--
		if OracleObjectMM.f_CheckTableExists('Pnt_PointGroup') = '0' then
	       execute immediate 
			'create table Pnt_PointGroup (
			   PointGroup_ID        INTEGER                         not null,
			   Point_ID             INTEGER                         not null,
			   Mobject_ID           INTEGER                         not null,
			   GroupNo_NR           INTEGER                         not null,
			   Level_NR             INTEGER                         not null,
			   TopClearance_NR      FLOAT,
			   constraint PK_PNT_POINTGROUP primary key (PointGroup_ID)
			 )';

			 execute immediate 'create index FK_PointGroup_Point_FK on Pnt_PointGroup ( Point_ID ASC )';
			 execute immediate 'create index FK_PointGroup_Mobject_FK on Pnt_PointGroup ( Mobject_ID ASC )';

			 execute immediate 'alter table Pnt_PointGroup add constraint FK_PNT_POINTGRO_PNT_POINT foreign key ( Point_ID ) references Pnt_Point ( Point_ID ) on delete cascade';
			 execute immediate 'alter table Pnt_PointGroup add constraint FK_PNT_POINTGRO_MOB_MOBJECT foreign key ( Mobject_ID ) references Mob_MObject ( Mobject_ID ) on delete cascade';
	    end if;



		--完成升级，更新在线数据库版本号
	    update BS_appconfig set value_TX = V_DBVersion where appconfig_CD = V_DBType;

		commit;
	
	end if;
 
end;
/

/********上一个版本:1.0.2.120518**********/
/**********当前版本:1.0.3.121130**********/
declare
	V_Count BS_MetaFieldType.Int_NR%type;
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
	V_MaxID  BS_MetaFieldType.BigInt_NR%type;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.0.3.121130';

	--1.0.3.121130版本数据库升级脚本，主要用于确保23.InitAppConfig.sql中的内容被正确执行
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then

		Delete From BS_AppConfig Where AppConfig_CD = 'ProductName';
		Delete From BS_AppConfig Where AppConfig_CD = 'ZXDBVERSION';
		Delete From BS_AppConfig Where SortNo_NR > 200000;
  
		-------------------------'软件界面自定义数据(序号从200801开始)----------------------------------------------
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
			Values('ZXDBVERSION', '在线数据库版本号', '1.0.3.121130', null, 'String', '数据库设置', '1', 4, 'TextBox', null);
  
		commit;
	
	end if;
 
end;
/

/********上一个版本:1.0.3.121130**********/
/**********当前版本:1.0.4.130313**********/
declare
	V_Count BS_MetaFieldType.Int_NR%type;
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
	V_MaxID  BS_MetaFieldType.BigInt_NR%type;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.0.4.130313';

	--1.0.4.130313版本数据库升级脚本，主要用于增加短信工作表、短信配置表、短信历史表、用于短信临时表
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then
		
		--*****************修改表BS_ObjectKey中的数据*****************--
		--增加ZX_SMS.SMS_ID键值记录
		V_MaxID := 1000000;
		V_Count := 0;
		select count(*) into V_Count from BS_ObjectKey where Source_CD = 'ZX_SMS' and KeyName = 'SMS_ID';
		if V_Count = 0 then
			insert into  BS_ObjectKey(Source_CD, KeyName, KeyValue, LockTime_TM) 
				values ('ZX_SMS', 'SMS_ID', V_MaxID, DBDiffPackage.GetSystemTime());
		end if;

		--增加ZX_SMSConfig.SMSConfig_ID键值记录
		V_MaxID := 1000000;
		V_Count := 0;
		select count(*) into V_Count from BS_ObjectKey where Source_CD = 'ZX_SMSConfig' and KeyName = 'SMSConfig_ID';
		if V_Count = 0 then
			insert into  BS_ObjectKey(Source_CD, KeyName, KeyValue, LockTime_TM) 
				values ('ZX_SMSConfig', 'SMSConfig_ID', V_MaxID, DBDiffPackage.GetSystemTime());
		end if;

		--增加ZX_SMSHistory.SMSHistory_ID键值记录
		V_MaxID := 1000000;
		V_Count := 0;
		select count(*) into V_Count from BS_ObjectKey where Source_CD = 'ZX_SMSHistory' and KeyName = 'SMSHistory_ID';
		if V_Count = 0 then
			insert into  BS_ObjectKey(Source_CD, KeyName, KeyValue, LockTime_TM) 
				values ('ZX_SMSHistory', 'SMSHistory_ID', V_MaxID, DBDiffPackage.GetSystemTime());
		end if;

		--*****************新增表ZX_SMS*****************--
		if OracleObjectMM.f_CheckTableExists('ZX_SMS') = '0' then
	       execute immediate 
			'create table ZX_SMS 
				(
					SMS_ID integer NOT NULL,
					Alm_ID number(19) NULL,
					Point_ID integer NULL,
					Mobject_ID integer NULL,
					AppUser_ID integer NULL,
					SendCount_NR integer NULL,
					SendTime_TM date NULL,
					SMSContent_TX varchar2(200) NULL,
					CONSTRAINT PK_ZX_SMSFailure PRIMARY KEY (SMS_ID)
				)';
	    end if;
		
		--*****************新增表ZX_SMSConfig*****************--
		if OracleObjectMM.f_CheckTableExists('ZX_SMSConfig') = '0' then
	       execute immediate 
			'create table ZX_SMSConfig 
				(
					SMSConfig_ID integer NOT NULL,
					Mobject_ID integer NOT NULL,
					AppUser_ID integer NOT NULL,
					AlmLevel_ID integer NULL,
					CONSTRAINT PK_ZX_SMSConfig PRIMARY KEY (SMSConfig_ID)
				)';
	    end if;
		
		--*****************新增表ZX_SMSHistory*****************--
		if OracleObjectMM.f_CheckTableExists('ZX_SMSHistory') = '0' then
	       execute immediate 
			'create table ZX_SMSHistory 
				(
					SMSHistory_ID integer NOT NULL,
					Partition_ID number(19) NOT NULL,
					Alm_ID number(19) NOT NULL,
					Point_ID integer NOT NULL,
					Mobject_ID integer NOT NULL,
					AppUser_ID integer NOT NULL,
					SendCount_NR integer NULL,
					Status_YN char(1) NULL,
					SendTime_TM date NULL,
					SMSContent_TX varchar2(200) NULL,
					CONSTRAINT PK_ZX_SMSHistory PRIMARY KEY (SMSHistory_ID)
				)';
	    end if;
		
		--*****************新增表ZX_SMSNewAlm*****************--
		if OracleObjectMM.f_CheckTableExists('ZX_SMSNewAlm') = '0' then
	       execute immediate 
			'create table ZX_SMSNewAlm 
				(
					Alm_ID number(19) NOT NULL,
					CONSTRAINT PK_ZX_SMS PRIMARY KEY (Alm_ID)
				)';
	    end if;

		Delete From BS_AppConfig Where AppConfig_CD = 'ZXSMSTel';
		Delete From BS_AppConfig Where AppConfig_CD = 'ZXSMSCode';
		Delete From BS_AppConfig Where AppConfig_CD = 'ZXSMSContent';
		
		Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
			Values('ZXSMSTel', '移动余额查询号码', '10086', null, 'String', '短信查询', '0', 200904, 'TextBox', null);

		Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
			Values('ZXSMSCode', '移动余额查询代码', '余额', null, 'String', '短信查询', '0', 200905, 'TextBox', null);

		Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
			Values('ZXSMSContent', '报警短信格式(C/D/M/P/L/V/T/N)', 'L/M/P/V/T', null, 'String', '短信查询', '0', 200906, 'TextBox', null);

		--完成升级，更新在线数据库版本号
	    update BS_appconfig set value_TX = V_DBVersion where appconfig_CD = V_DBType;

		commit;
	
	end if;
 
end;
/

/********上一个版本:1.0.4.130313**********/
/**********当前版本:1.0.5.130613**********/
declare
	V_Count BS_MetaFieldType.Int_NR%type;
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
	V_MaxID  BS_MetaFieldType.BigInt_NR%type;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.0.5.130613';

	--1.0.5.130613版本数据库升级脚本，主要用于增加数据源变量表、测点变量表
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then

		--*****************修改表BS_ObjectKey中的数据*****************--
		--增加Pnt_DataVar.Var_ID键值记录
		V_MaxID := 1000000;
		V_Count := 0;
		select count(*) into V_Count from BS_ObjectKey where Source_CD = 'Pnt_DataVar' and KeyName = 'Var_ID';
		if V_Count = 0 then
			insert into  BS_ObjectKey(Source_CD, KeyName, KeyValue, LockTime_TM) 
				values ('Pnt_DataVar', 'Var_ID', V_MaxID, DBDiffPackage.GetSystemTime());
		end if;

		--*****************新增表Pnt_DataVar*****************--
		if OracleObjectMM.f_CheckTableExists('Pnt_DataVar') = '0' then
	       execute immediate 
			'create table Pnt_DataVar 
				(
					Var_ID integer NOT NULL,
					VarName_TX varchar2(50) NOT NULL,
					VarDesc_TX varchar2(300) NULL,
					VarScale_NR float DEFAULT 1 NOT NULL,
					VarOffset_NR float DEFAULT 0 NOT NULL,
					CONSTRAINT PK_Pnt_DataVar PRIMARY KEY (Var_ID)
				)';
	    end if;
		
		--*****************新增表Pnt_PntDataVar*****************--
		if OracleObjectMM.f_CheckTableExists('Pnt_PntDataVar') = '0' then
	       execute immediate 
			'create table Pnt_PntDataVar 
				(
					Point_ID integer NOT NULL,
					Var_ID integer NOT NULL,
					CONSTRAINT PK_Pnt_PntDataVar PRIMARY KEY (Point_ID, Var_ID)
				)';

			execute immediate 'create unique index FK_PntDataVar_Pnt_FK on Pnt_PntDataVar ( Point_ID ASC )';
			execute immediate 'create unique index FK_PntDataVar_Var_FK on Pnt_PntDataVar ( Var_ID ASC )';
	    end if;
		
		--完成升级，更新在线数据库版本号
	    update BS_appconfig set value_TX = V_DBVersion where appconfig_CD = V_DBType;

		commit;
	
	end if;
 
end;
/

/********上一个版本:1.0.5.130613**********/
/**********当前版本:1.0.6.131212**********/
declare
	V_Count BS_MetaFieldType.Int_NR%type;
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
	V_MaxID  BS_MetaFieldType.BigInt_NR%type;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.0.6.131212';

	--1.0.6.131212版本数据库升级脚本，主要用于版本说明中1.0.7至1.1.0的改动中涉及到数据库变化
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then
		
		--完成升级，更新在线数据库版本号
	    update BS_appconfig set value_TX = V_DBVersion where appconfig_CD = V_DBType;

		commit;
	
	end if;
 
end;
/

/********上一个版本:1.0.6.131212**********/
/**********当前版本:1.0.7.140228**********/
declare
	V_Count BS_MetaFieldType.Int_NR%type;
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
	V_MaxID  BS_MetaFieldType.BigInt_NR%type;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.0.7.140228';

	--1.0.7.140228版本数据库升级脚本，主要用于调整分区表BS_TransPartionTable.MoveAfterDays的设置值
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then
		
		--调整ZX_HistoryData的分区表BS_TransPartionTable.MoveAfterDays的设置值，从"2,2,2"->"5,5,5"
		update BS_TransPartionTable set MoveAfterDays = '5,5,5' where TransKey_CD = 'ZX_HistoryData';

		--完成升级，更新在线数据库版本号
	    update BS_appconfig set value_TX = V_DBVersion where appconfig_CD = V_DBType;

		commit;
	
	end if;
 
end;
/

/********上一个版本:1.0.7.140228**********/
/**********当前版本:1.0.8.140410**********/
declare
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.0.8.140410';

	-- 主要用于：增加南钢短信平台的功能
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then
	
		Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
			Values('ZXSMSPlatformUrl', '短信平台网址', '短信平台网址', null, 'String', '短信查询', '0', 200907, 'TextBox', null);

		Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
			Values('ZXSMSPlatformUserId', '短信平台用户编号', '短信平台用户编号', null, 'String', '短信查询', '0', 200908, 'TextBox', null);

		Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
			Values('ZXSMSPlatformPassword', '短信平台用户密码', '短信平台用户密码', null, 'Password', '短信查询', '0', 200909, 'TextBox', null);

		Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX)
			Values('ZXSMSPlatformType', '短信平台类型', '短信猫', null, 'String', '短信查询', '0', 200910, 'ComboBox', '短信猫;南钢短信平台|短信猫;南钢短信平台');

		--完成升级，更新在线数据库版本号
	    update BS_appconfig set value_TX = V_DBVersion where appconfig_CD = V_DBType;

		commit;
	
	end if;
 
end;
/

/********上一个版本:1.0.8.140410**********/
/**********当前版本:1.0.9.140510**********/
declare
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.0.9.140510';

	-- 主要用于：增加南钢短信平台的功能
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then
	
		update BS_Resource set Value_TX = '通讯录', Remark_TX = '通讯录' where upper(Key_TX) = upper('ZX_SMSConfig.Address');

		--完成升级，更新在线数据库版本号
	    update BS_appconfig set value_TX = V_DBVersion where appconfig_CD = V_DBType;

		commit;
	
	end if;
 
end;
/

/********上一个版本:1.0.9.140510**********/
/**********当前版本:1.1.0.140723**********/
declare
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.1.0.140723';

	-- 主要用于：增加在线设备编码是否继承配置项
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then
	
		Insert Into BS_AppConfig(AppConfig_CD,Name_TX,Value_TX,DefaultValue_TX,DataType_CD,Category_CD,SysConfig_YN,SortNo_NR,ControlType_CD,Other_TX)	
			Values('ZXMObjectCDInherit','在线设备编码是否继承','是','是','String','在线设备管理','0',200911,'ComboBox','是;否|是;否');

		--完成升级，更新在线数据库版本号
	    update BS_appconfig set value_TX = V_DBVersion where appconfig_CD = V_DBType;

		commit;
	
	end if;
 
end;
/

/********上一个版本:1.1.0.140723**********/
/**********当前版本:1.1.1.140826**********/
declare
	V_Count int;
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.1.1.140826';

	-- 主要用于：采集站增加类型后对数据库中已有数据进行兼容性处理
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then
	
		-- 增加在线使用的信号类型“电流”
		V_Count := 0;
		select Count(*) Into V_Count From Z_SignType where SignType_ID = 113;
		if V_Count = 0 then
			
			Insert Into Z_SignType(SignType_ID,Name_TX,DataType_ID,UnitType_ID,OnLine_YN,OffLine_YN,SortNo_NR) Values(113,'电流',1,55,'0','0',4);
				
		end if;
		
		-- 增加在线使用的信号类型“轴位移”
		V_Count := 0;
		select Count(*) Into V_Count From Z_SignType where SignType_ID = 120;
		if V_Count = 0 then
			
			Insert Into Z_SignType(SignType_ID,Name_TX,DataType_ID,UnitType_ID,OnLine_YN,OffLine_YN,SortNo_NR) Values(120,'轴位移',1,10,'1','0',6);
				
		end if;
		
		-- 增加在线使用的信号类型“轴位置”
		V_Count := 0;
		select Count(*) Into V_Count From Z_SignType where SignType_ID = 121;
		if V_Count = 0 then
			
			Insert Into Z_SignType(SignType_ID,Name_TX,DataType_ID,UnitType_ID,OnLine_YN,OffLine_YN,SortNo_NR) Values(121,'轴位置',1,10,'1','0',7);
				
		end if;
		
		-- 更新在线使用的信号类型“冲击”改名为“冲击能量”，并且使用加速度信号类型的单位
		update Z_SignType set Name_TX = '冲击能量', UnitType_ID = 21, SortNo_NR = 8 where SignType_ID = 119;

		delete from Z_EngUnit where EngUnit_ID = 503 and UnitType_ID =10;


		-- 在线增加三个工程单位
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnit where EngUnit_ID = 41 and UnitType_ID = 12;
		if V_Count = 0 then
			Insert Into Z_EngUnit(EngUnit_ID,UnitType_ID,NameC_TX,NameE_TX,Scale_NR,Offset_NR,IsDefault_YN,IsInner_YN) Values(41,12,'华氏度','℉',1.8,32,'0','1');
		end if;
		
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnit where EngUnit_ID = 79 and UnitType_ID = 21;
		if V_Count = 0 then
			Insert Into Z_EngUnit(EngUnit_ID,UnitType_ID,NameC_TX,NameE_TX,Scale_NR,Offset_NR,IsDefault_YN,IsInner_YN) Values(79,21,'重力加速度','g',0.1019762,0,'0','1');
		end if;
		
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnit where EngUnit_ID = 245 and UnitType_ID = 74;
		if V_Count = 0 then
			Insert Into Z_EngUnit(EngUnit_ID,UnitType_ID,NameC_TX,NameE_TX,Scale_NR,Offset_NR,IsDefault_YN,IsInner_YN) Values(245,74,'毫伏','mV',1000,0,'0','1');
		end if;
		
		
		-- 在线增加要使用的传感器灵敏度的单位类型
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnitType where UnitType_ID = 1001;
		if V_Count = 0 then
			Insert Into Z_EngUnitType(UnitType_ID,Name_TX,Description_TX) Values(1001,'加速度传感器灵敏度','加速度传感器灵敏度');
		end if;
		
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnitType where UnitType_ID = 1002;
		if V_Count = 0 then
			Insert Into Z_EngUnitType(UnitType_ID,Name_TX,Description_TX) Values(1002,'速度传感器灵敏度','速度传感器灵敏度');
		end if;
		
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnitType where UnitType_ID = 1003;
		if V_Count = 0 then
			Insert Into Z_EngUnitType(UnitType_ID,Name_TX,Description_TX) Values(1003,'位移传感器灵敏度','位移传感器灵敏度');
		end if;
		
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnitType where UnitType_ID = 1004;
		if V_Count = 0 then
			Insert Into Z_EngUnitType(UnitType_ID,Name_TX,Description_TX) Values(1004,'温度传感器灵敏度','温度传感器灵敏度');
		end if;
		
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnitType where UnitType_ID = 1005;
		if V_Count = 0 then
			Insert Into Z_EngUnitType(UnitType_ID,Name_TX,Description_TX) Values(1005,'电流传感器灵敏度','电流传感器灵敏度');
		end if;
		
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnitType where UnitType_ID = 1006;
		if V_Count = 0 then
			Insert Into Z_EngUnitType(UnitType_ID,Name_TX,Description_TX) Values(1006,'电压传感器灵敏度','电压传感器灵敏度');
		end if;
		
		
		-- 在线增加需要使用的传感器灵敏度单位
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnit where EngUnit_ID = 20011 and UnitType_ID = 1001;
		if V_Count = 0 then
			Insert Into Z_EngUnit(EngUnit_ID,UnitType_ID,NameC_TX,NameE_TX,Scale_NR,Offset_NR,IsDefault_YN,IsInner_YN) Values(20011,1001,'mV/m/S^2','mV/m/S^2',1,0,'1','1');
		end if;
		
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnit where EngUnit_ID = 20012 and UnitType_ID = 1001;
		if V_Count = 0 then
			Insert Into Z_EngUnit(EngUnit_ID,UnitType_ID,NameC_TX,NameE_TX,Scale_NR,Offset_NR,IsDefault_YN,IsInner_YN) Values(20012,1001,'mV/g','mV/g',1,0,'0','1');
		end if;
		
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnit where EngUnit_ID = 20021 and UnitType_ID = 1002;
		if V_Count = 0 then
			Insert Into Z_EngUnit(EngUnit_ID,UnitType_ID,NameC_TX,NameE_TX,Scale_NR,Offset_NR,IsDefault_YN,IsInner_YN) Values(20021,1002,'mV/mm/S','mV/mm/S',1,0,'1','1');
		end if;
		
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnit where EngUnit_ID = 20031 and UnitType_ID = 1003;
		if V_Count = 0 then
			Insert Into Z_EngUnit(EngUnit_ID,UnitType_ID,NameC_TX,NameE_TX,Scale_NR,Offset_NR,IsDefault_YN,IsInner_YN) Values(20031,1003,'V/mm','V/mm',1,0,'1','1');
		end if;

		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnit where EngUnit_ID = 20032 and UnitType_ID = 1003;
		if V_Count = 0 then
			Insert Into Z_EngUnit(EngUnit_ID,UnitType_ID,NameC_TX,NameE_TX,Scale_NR,Offset_NR,IsDefault_YN,IsInner_YN) Values(20032,1003,'mV/um','mV/um',1,0,'0','1');
		end if;

		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnit where EngUnit_ID = 20041 and UnitType_ID = 1004;
		if V_Count = 0 then
			Insert Into Z_EngUnit(EngUnit_ID,UnitType_ID,NameC_TX,NameE_TX,Scale_NR,Offset_NR,IsDefault_YN,IsInner_YN) Values(20041,1004,'mA/℃','mA/℃',1,0,'1','1');
		end if;
		
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnit where EngUnit_ID = 20042 and UnitType_ID = 1004;
		if V_Count = 0 then
			Insert Into Z_EngUnit(EngUnit_ID,UnitType_ID,NameC_TX,NameE_TX,Scale_NR,Offset_NR,IsDefault_YN,IsInner_YN) Values(20042,1004,'mA/℉','mA/℉',1,0,'0','1');
		end if;
		
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnit where EngUnit_ID = 20051 and UnitType_ID = 1005;
		if V_Count = 0 then
			Insert Into Z_EngUnit(EngUnit_ID,UnitType_ID,NameC_TX,NameE_TX,Scale_NR,Offset_NR,IsDefault_YN,IsInner_YN) Values(20051,1005,'mA/A','mA/A',1,0,'1','1');
		end if;

		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnit where EngUnit_ID = 20061 and UnitType_ID = 1006;
		if V_Count = 0 then
			Insert Into Z_EngUnit(EngUnit_ID,UnitType_ID,NameC_TX,NameE_TX,Scale_NR,Offset_NR,IsDefault_YN,IsInner_YN) Values(20061,1006,'mV/V','mV/V',1,0,'1','1');
		end if;
		
		update Sample_Station set StationType_TX = '0';

		--完成升级，更新在线数据库版本号
	    update BS_appconfig set value_TX = V_DBVersion where appconfig_CD = V_DBType;

		commit;
	
	end if;
 
end;
/

/********上一个版本:1.1.1.140826**********/
/**********当前版本:1.1.1.141021**********/
declare
	V_Count int;
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
	V_MaxID  BS_MetaFieldType.BigInt_NR%type;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.1.1.141021';

	-- 主要用于：针对不同设备设置不同报警短信发送周期
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then
		
		--*****************修改表BS_ObjectKey中的数据*****************--
		--增加Pnt_DataVar.Var_ID键值记录
		V_MaxID := 1000000;
		V_Count := 0;
		select count(*) into V_Count from BS_ObjectKey where Source_CD = 'ZX_MobSMSAlmConfig' and KeyName = 'Config_ID';
		if V_Count = 0 then
			insert into  BS_ObjectKey(Source_CD, KeyName, KeyValue, LockTime_TM) 
				values ('ZX_MobSMSAlmConfig', 'Config_ID', V_MaxID, DBDiffPackage.GetSystemTime());
		end if;

		--*****************新增表ZX_MobSMSAlmConfig*****************--
		if OracleObjectMM.f_CheckTableExists('ZX_MobSMSAlmConfig') = '0' then
			execute immediate 
			'create table ZX_MobSMSAlmConfig 
			(
			   Config_ID		integer		not null,
			   Mobject_ID		integer		not null,
			   WarnAlmCycleHour_NR	integer		not null,
			   DangerAlmCycleHour_NR	integer		not null,
			   CONSTRAINT PK_ZX_MobSMSAlmConfig PRIMARY KEY (Config_ID)
			 )';

			execute immediate 'create index IZX_MobSMSAlmConfig_MobjectID on ZX_MobSMSAlmConfig ( Mobject_ID ASC )';

			execute immediate 'alter table ZX_MobSMSAlmConfig add constraint FK_ZX_MobSMSAlmCfg_Mojbect foreign key ( Mobject_ID ) references Mob_MObject ( Mobject_ID ) on delete cascade';
	    end if;

		--完成升级，更新在线数据库版本号
	    update BS_appconfig set value_TX = V_DBVersion where appconfig_CD = V_DBType;
	
		commit;

	end if;
 
end;
/

/********上一个版本:1.1.1.141021**********/
/**********当前版本:1.1.2.141202**********/
declare
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.1.2.141202';

	-- 主要用于：记录报警短信发送失败的异常信息,删除原来短信周期设置表
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then
		
		execute immediate 'drop table ZX_MobSMSAlmConfig';
		execute immediate 'alter table ZX_SMSHistory add Memo_TX varchar(2000) null';
		execute immediate 'update ZX_SMSHistory set Partition_ID=20000000000000+Partition_ID where Partition_ID<20000000000000';

		--完成升级，更新在线数据库版本号
	    update BS_appconfig set value_TX = V_DBVersion where appconfig_CD = V_DBType;
	
		commit;

	end if;
 
end;
/


/**********无论升级哪个版本的数据库，最后都要执行的脚本区域**********/
declare
v_emsg BS_MetaFieldType.FiveKilo_TX%type;
begin
   PartitionPackage.InitTableStruct(v_emsg);
end;
/

commit;