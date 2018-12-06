/********����������ݿ��ʼ�汾��1.0.0.000000**********/
declare 
	V_Count BS_MetaFieldType.Int_NR%type;
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
begin

	V_DBType := 'ZXDBVERSION';

	--����������ݿ�汾��
    V_Count := 0;
    select count(*) into V_Count from BS_AppConfig where AppConfig_CD = V_DBType;
	if V_Count = 0 then
		insert into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
			values(V_DBType, '�������ݿ�汾��', '1.0.0.000000', null, 'String', '���ݿ�����', '1', 4, 'TextBox', null);
	end if;

    commit;	
	
end;
/

/********��һ���汾:1.0.0.000000**********/
/**********��ǰ�汾:1.0.1.120509**********/
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

	--1.0.1.120509�汾���ݿ������ű�����Ҫ����������ʷ���ݱ���صĶ��ű�񣬰������Ӧ�ķ�����
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then

		V_SummaryTableBaseName := 'ZX_History_Summary';



		--*****************����������ʷ��������ֵ��ZX_History_FeatureValue���������*****************--
		V_MaxID := 1000000;
		V_TableBaseName := 'ZX_History_FeatureValue';

		--��ȡ����Լ������
		V_ConstraintName := 'ZXFeatvalue';
		V_Count := 0;
		select count(*) into V_Count from BS_RangePartitionMeta where upper(TableBaseName_TX) = upper(V_TableBaseName);
		if V_Count > 0 then
			select ConstraintName_TX into V_ConstraintName from BS_RangePartitionMeta where upper(TableBaseName_TX) = upper(V_TableBaseName);
		end if;

		--�������ֵ�ڱ��в����ڣ����������ֵ�����������ȡ������IDֵ
		V_Count := 0;
		select count(*) into V_Count from BS_BigObjectKey where Source_CD = V_TableBaseName and KeyName = 'FeatureValuePK_ID';
		if V_Count = 0 then
			insert into  BS_BigObjectKey(Source_CD, KeyName, KeyValue, LockTime_TM) 
				values (V_TableBaseName, 'FeatureValuePK_ID', V_MaxID, DBDiffPackage.GetSystemTime());
		else
			select KeyValue into V_MaxID from BS_BigObjectKey where Source_CD = V_TableBaseName and KeyName = 'FeatureValuePK_ID';
		end if;

		--����ZX_History_FeatureValue�ķ�����
		for V_DataRow in ( select TableName_TX from BS_RangePartition where upper(TableBaseName_TX) = upper(V_TableBaseName) order by TableBaseName_TX, PartitionMin_ID ) loop
			
			V_TableName := V_DataRow.TableName_TX;
			
			--�����ֶΣ�
			--FeatureValuePK_ID	����ֵ�������	bigint	8	PK,Not Null	��ʾһ������ֵ���ݵı��
			--FeatureValueType_ID	����ֵ����ID	int	4	Not Null	0��������502�ķǰ��������ʹ���ʱ��ʹ�ø����ͣ���102��Packs�ļ��ٶȣ�101��Packs���ٶȣ�103��Packs��λ�ƣ�502�İ��������ʹ���ʱ��ʹ�ø����ͣ���
			--SigType_NR	�ź�����	Int	4	Not Null	502����ͨ/�ױȣ�101�ٶȡ�102���ٶȡ�103λ�ơ�113������118��ѹ�������102���ٶȡ��ٶȰ�����Ŀ��101�����ٶȰ�����Ŀ��102��λ�ư�����Ŀ��103�����������Ŀ��102����Ƶ������Ŀ��102����
			--EngUnit_ID	���̵�λID	Int	4	Not Null	�����ź�������ȷ����λ
			execute immediate 'alter table ' || V_TableName || ' add FeatureValuePK_ID number(19) default 0 not null';
			execute immediate 'alter table ' || V_TableName || ' add FeatureValueType_ID integer default 0 not null';
			execute immediate 'alter table ' || V_TableName || ' add SigType_NR integer default 0 not null';
			execute immediate 'alter table ' || V_TableName || ' add EngUnit_ID integer default 0 not null';

			--Ϊ�ֶ�FeatureValuePK_ID����˳��ţ�ʹ��ROWNUMʵ�������޸�
			execute immediate 'update ' || V_TableName || ' set FeatureValuePK_ID = ( select b.RowNumber + ' || V_MaxID || ' from ( select History_ID, ChnNo_NR, FeatureValue_ID, RowNum as RowNumber from ' || V_TableName || ' order by History_ID, ChnNo_NR, FeatureValue_ID ) b where b.History_ID = ' || V_TableName || '.History_ID and b.ChnNo_NR = ' || V_TableName || '.ChnNo_NR and b.FeatureValue_ID = ' || V_TableName || '.FeatureValue_ID )';

			--����SigType_NR��EngUnit_ID�ֶ�����
			V_PartitionName := DBDiffPackage.GetSubstring( V_TableName, DBDiffPackage.GetLength( V_TableBaseName ) + 1, 6 );
			V_SummaryTableName := V_SummaryTableBaseName || V_PartitionName;
			execute immediate 'update ' || V_TableName || ' set SigType_NR = ( select case when b.SigType_NR is null then 0 else b.SigType_NR end from ' || V_SummaryTableName || ' b where b.History_ID = ' || V_TableName || '.History_ID )';
			execute immediate 'update ' || V_TableName || ' set EngUnit_ID = ( select case when b.EngUnit_ID is null then 0 else b.EngUnit_ID end from ' || V_SummaryTableName || ' b where b.History_ID = ' || V_TableName || '.History_ID )';

			--�޸��ֶΣ�
			--History_ID��ȡ����������������FK,Not Null
			--ChnNo_NR��ȡ����������������Not Null
			--FeatureValue_ID��ȡ����������������Not Null
			--FeatureValuePK_ID����������ֵ�������
			V_PrimaryKeyName := 'PK_' || V_ConstraintName || V_PartitionName;
			execute immediate 'alter table ' || V_TableName || ' drop constraint ' || V_PrimaryKeyName;
			execute immediate 'alter table ' || V_TableName || ' add constraint ' || V_PrimaryKeyName || ' primary key ( FeatureValuePK_ID )';

			--ʹ�õ�ǰ������FeatureValuePK_ID�ֶ����ֵ����V_MaxIDֵ
			execute immediate 'select case when max( FeatureValuePK_ID ) is null then 0 else max( FeatureValuePK_ID ) end from ' || V_TableName into V_TableMaxID;
			if V_TableMaxID > 0 then
				V_MaxID := V_TableMaxID;
			end if;

			--�޸ķ����������
			--ZX_History_Featurevalue******��ΪZX_History_FeatureValue******
			V_NewTableName := V_TableBaseName || V_PartitionName;
			execute immediate 'alter table ' || V_TableName || ' rename to ' || V_NewTableName;

	    end loop;


		--��ʼ�����Ƿ�����ZX_History_FeatureValue
		--�����Ƿ������ֶ�
		execute immediate 'alter table ' || V_TableBaseName || ' add FeatureValuePK_ID number(19) default 0 not null';
		execute immediate 'alter table ' || V_TableBaseName || ' add FeatureValueType_ID integer default 0 not null';
		execute immediate 'alter table ' || V_TableBaseName || ' add SigType_NR integer default 0 not null';
		execute immediate 'alter table ' || V_TableBaseName || ' add EngUnit_ID integer default 0 not null';

		--Ϊ�Ƿ������ֶ�FeatureValuePK_ID����˳��ţ�ʹ��ROWNUMʵ�������޸�
		execute immediate 'update ' || V_TableBaseName || ' set FeatureValuePK_ID = ( select b.RowNumber + ' || V_MaxID || ' from ( select History_ID, ChnNo_NR, FeatureValue_ID, RowNum as RowNumber from ' || V_TableBaseName || ' order by History_ID, ChnNo_NR, FeatureValue_ID ) b where b.History_ID = ' || V_TableBaseName || '.History_ID and b.ChnNo_NR = ' || V_TableBaseName || '.ChnNo_NR and b.FeatureValue_ID = ' || V_TableBaseName || '.FeatureValue_ID )';

		--���·Ƿ�����SigType_NR��EngUnit_ID�ֶ�����
		execute immediate 'update ' || V_TableBaseName || ' set SigType_NR = ( select case when b.SigType_NR is null then 0 else b.SigType_NR end from ' || V_SummaryTableBaseName || ' b where b.History_ID = ' || V_TableBaseName || '.History_ID )';
		execute immediate 'update ' || V_TableBaseName || ' set EngUnit_ID = ( select case when b.EngUnit_ID is null then 0 else b.EngUnit_ID end from ' || V_SummaryTableBaseName || ' b where b.History_ID = ' || V_TableBaseName || '.History_ID )';

		--�޸ķǷ������ֶ�
		V_PrimaryKeyName := upper('PK_' || V_TableBaseName);
		execute immediate 'alter table ' || V_TableBaseName || ' drop constraint ' || V_PrimaryKeyName;
		execute immediate 'alter table ' || V_TableBaseName || ' add constraint ' || V_PrimaryKeyName || ' primary key ( FeatureValuePK_ID )';

		--ʹ�÷Ƿ�����FeatureValuePK_ID�ֶ����ֵ����V_MaxIDֵ
		execute immediate 'select case when max( FeatureValuePK_ID ) is null then 0 else max( FeatureValuePK_ID ) end from ' || V_TableBaseName into V_TableMaxID;
		if V_TableMaxID > 0 then
			V_MaxID := V_TableMaxID;
		end if;

		--����BigObjectKey�й���ZX_History_FeatureValue������ֵ
		update BS_BigObjectKey set KeyValue = V_MaxID where Source_CD = V_TableBaseName and KeyName = 'FeatureValuePK_ID';



		--*****************����������ʷ����Ƶ�ױ�ZX_History_Freq���������*****************--
		V_TableBaseName := 'ZX_History_Freq';

		--����ZX_History_Freq�ķ�����
		for V_DataRow in ( select TableName_TX from BS_RangePartition where upper(TableBaseName_TX) = upper(V_TableBaseName) order by TableBaseName_TX, PartitionMin_ID ) loop
			
			V_TableName := V_DataRow.TableName_TX;

			--ɾ��������
			execute immediate 'drop table ' || V_TableName || ' cascade constraints';

		end loop;


		--��ʼ�����Ƿ�����ZX_History_Freq
		--ɾ���Ƿ�����
		execute immediate 'drop table ' || V_TableBaseName || ' cascade constraints';



		--*****************�޸ı�BS_BigObjectKey�е�����*****************--
		--����ZX_History_Summary.Synch_NR��ֵ��¼
		V_MaxID := 1000000;
		V_Count := 0;
		select count(*) into V_Count from BS_BigObjectKey where Source_CD = V_SummaryTableBaseName and KeyName = 'Synch_NR';
		if V_Count = 0 then
			insert into  BS_BigObjectKey(Source_CD, KeyName, KeyValue, LockTime_TM) 
				values (V_SummaryTableBaseName, 'Synch_NR', V_MaxID, DBDiffPackage.GetSystemTime());
		end if;
		
		--ɾ��ZX_History_Freq��ֵ��¼
		delete from BS_BigObjectKey where Source_CD = 'ZX_History_Freq';



		--*****************���������Ϣ*****************--
		--�޸ķ��������������BS_RangePartition��Ӧ�ļ�¼��TableBaseName_TX��TableName_TX�����ֶ���ZX_History_Featurevalue����ΪZX_History_FeatureValue��
		update BS_RangePartition set TableBaseName_TX = 'ZX_History_FeatureValue' where upper(TableBaseName_TX) = upper('ZX_History_Featurevalue');
		update BS_RangePartition set TableName_TX = TableBaseName_TX || DBDiffPackage.GetSubstring( TableName_TX, DBDiffPackage.GetLength( TableBaseName_TX ) + 1, 6 ) where upper(TableBaseName_TX) = upper('ZX_History_Featurevalue');

		--ɾ��������ر��е�����
		delete from BS_TableColumns;
		delete from BS_CreateTableIndexInfo;
		delete from BS_CreateTableInfo;
		delete from BS_RangePartition where TableBaseName_TX = 'ZX_History_Freq';
		delete from BS_RangePartitionMeta where TableBaseName_TX in ( 'ZX_History_Summary', 'ZX_History_Waveform', 'ZX_History_Freq', 'ZX_History_Featurevalue' );
		delete from BS_TransPartionTable where TransKey_CD = 'ZX_HistoryData';

		--��ʼ��������ر��е�����
		insert into BS_RangePartitionMeta (TableBaseName_TX,PartitionField_TX,Category_CD,ConstraintName_TX)
			values('ZX_History_Summary','Partition_ID','����ժҪ����','ZXSummary');
		insert into BS_RangePartitionMeta (TableBaseName_TX,PartitionField_TX,Category_CD,ConstraintName_TX)
			values('ZX_History_Waveform','Partition_ID','���߲�������','ZXWaveform');
		insert into BS_RangePartitionMeta (TableBaseName_TX,PartitionField_TX,Category_CD,ConstraintName_TX)
			values('ZX_History_FeatureValue','Partition_ID','����ָ������','ZXFeatvalue');

		insert into BS_TransPartionTable(TransKey_CD,TableBaseNameList_TX,ViewBaseNameList_TX,PartitionType_CD,PartitionType_NR,MoveAfterDays,MoveType)
			values('ZX_HistoryData','ZX_History_Summary,ZX_History_Waveform,ZX_History_FeatureValue','','M',1,'2,2,2','1');

		--��ʼ����������
		PartitionPackage.InitTableStruct( v_emsg );



		--��������������������ݿ�汾��
	    update BS_appconfig set value_TX = V_DBVersion where appconfig_CD = V_DBType;

		commit;
	
	end if;
 
end;
/

/********��һ���汾:1.0.1.120509**********/
/**********��ǰ�汾:1.0.2.120518**********/
declare
	V_Count BS_MetaFieldType.Int_NR%type;
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
	V_MaxID  BS_MetaFieldType.BigInt_NR%type;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.0.2.120518';

	--1.0.2.120518�汾���ݿ������ű�����Ҫ�������Ӳ������
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then

		--*****************�޸ı�BS_ObjectKey�е�����*****************--
		--����Pnt_PointGroup.PointGroup_ID��ֵ��¼
		V_MaxID := 1000000;
		V_Count := 0;
		select count(*) into V_Count from BS_ObjectKey where Source_CD = 'Pnt_PointGroup' and KeyName = 'PointGroup_ID';
		if V_Count = 0 then
			insert into  BS_ObjectKey(Source_CD, KeyName, KeyValue, LockTime_TM) 
				values ('Pnt_PointGroup', 'PointGroup_ID', V_MaxID, DBDiffPackage.GetSystemTime());
		end if;
		
		--����Pnt_PointGroupNo.GroupNo_NR��ֵ��¼
		V_MaxID := 0;
		V_Count := 0;
		select count(*) into V_Count from BS_ObjectKey where Source_CD = 'Pnt_PointGroupNo' and KeyName = 'GroupNo_NR';
		if V_Count = 0 then
			insert into  BS_ObjectKey(Source_CD, KeyName, KeyValue, LockTime_TM) 
				values ('Pnt_PointGroupNo', 'GroupNo_NR', V_MaxID, DBDiffPackage.GetSystemTime());
		end if;
		


		--*****************������Pnt_PointGroup*****************--
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



		--��������������������ݿ�汾��
	    update BS_appconfig set value_TX = V_DBVersion where appconfig_CD = V_DBType;

		commit;
	
	end if;
 
end;
/

/********��һ���汾:1.0.2.120518**********/
/**********��ǰ�汾:1.0.3.121130**********/
declare
	V_Count BS_MetaFieldType.Int_NR%type;
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
	V_MaxID  BS_MetaFieldType.BigInt_NR%type;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.0.3.121130';

	--1.0.3.121130�汾���ݿ������ű�����Ҫ����ȷ��23.InitAppConfig.sql�е����ݱ���ȷִ��
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then

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
			Values('ZXDBVERSION', '�������ݿ�汾��', '1.0.3.121130', null, 'String', '���ݿ�����', '1', 4, 'TextBox', null);
  
		commit;
	
	end if;
 
end;
/

/********��һ���汾:1.0.3.121130**********/
/**********��ǰ�汾:1.0.4.130313**********/
declare
	V_Count BS_MetaFieldType.Int_NR%type;
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
	V_MaxID  BS_MetaFieldType.BigInt_NR%type;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.0.4.130313';

	--1.0.4.130313�汾���ݿ������ű�����Ҫ�������Ӷ��Ź������������ñ�������ʷ�����ڶ�����ʱ��
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then
		
		--*****************�޸ı�BS_ObjectKey�е�����*****************--
		--����ZX_SMS.SMS_ID��ֵ��¼
		V_MaxID := 1000000;
		V_Count := 0;
		select count(*) into V_Count from BS_ObjectKey where Source_CD = 'ZX_SMS' and KeyName = 'SMS_ID';
		if V_Count = 0 then
			insert into  BS_ObjectKey(Source_CD, KeyName, KeyValue, LockTime_TM) 
				values ('ZX_SMS', 'SMS_ID', V_MaxID, DBDiffPackage.GetSystemTime());
		end if;

		--����ZX_SMSConfig.SMSConfig_ID��ֵ��¼
		V_MaxID := 1000000;
		V_Count := 0;
		select count(*) into V_Count from BS_ObjectKey where Source_CD = 'ZX_SMSConfig' and KeyName = 'SMSConfig_ID';
		if V_Count = 0 then
			insert into  BS_ObjectKey(Source_CD, KeyName, KeyValue, LockTime_TM) 
				values ('ZX_SMSConfig', 'SMSConfig_ID', V_MaxID, DBDiffPackage.GetSystemTime());
		end if;

		--����ZX_SMSHistory.SMSHistory_ID��ֵ��¼
		V_MaxID := 1000000;
		V_Count := 0;
		select count(*) into V_Count from BS_ObjectKey where Source_CD = 'ZX_SMSHistory' and KeyName = 'SMSHistory_ID';
		if V_Count = 0 then
			insert into  BS_ObjectKey(Source_CD, KeyName, KeyValue, LockTime_TM) 
				values ('ZX_SMSHistory', 'SMSHistory_ID', V_MaxID, DBDiffPackage.GetSystemTime());
		end if;

		--*****************������ZX_SMS*****************--
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
		
		--*****************������ZX_SMSConfig*****************--
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
		
		--*****************������ZX_SMSHistory*****************--
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
		
		--*****************������ZX_SMSNewAlm*****************--
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
			Values('ZXSMSTel', '�ƶ�����ѯ����', '10086', null, 'String', '���Ų�ѯ', '0', 200904, 'TextBox', null);

		Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
			Values('ZXSMSCode', '�ƶ�����ѯ����', '���', null, 'String', '���Ų�ѯ', '0', 200905, 'TextBox', null);

		Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
			Values('ZXSMSContent', '�������Ÿ�ʽ(C/D/M/P/L/V/T/N)', 'L/M/P/V/T', null, 'String', '���Ų�ѯ', '0', 200906, 'TextBox', null);

		--��������������������ݿ�汾��
	    update BS_appconfig set value_TX = V_DBVersion where appconfig_CD = V_DBType;

		commit;
	
	end if;
 
end;
/

/********��һ���汾:1.0.4.130313**********/
/**********��ǰ�汾:1.0.5.130613**********/
declare
	V_Count BS_MetaFieldType.Int_NR%type;
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
	V_MaxID  BS_MetaFieldType.BigInt_NR%type;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.0.5.130613';

	--1.0.5.130613�汾���ݿ������ű�����Ҫ������������Դ��������������
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then

		--*****************�޸ı�BS_ObjectKey�е�����*****************--
		--����Pnt_DataVar.Var_ID��ֵ��¼
		V_MaxID := 1000000;
		V_Count := 0;
		select count(*) into V_Count from BS_ObjectKey where Source_CD = 'Pnt_DataVar' and KeyName = 'Var_ID';
		if V_Count = 0 then
			insert into  BS_ObjectKey(Source_CD, KeyName, KeyValue, LockTime_TM) 
				values ('Pnt_DataVar', 'Var_ID', V_MaxID, DBDiffPackage.GetSystemTime());
		end if;

		--*****************������Pnt_DataVar*****************--
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
		
		--*****************������Pnt_PntDataVar*****************--
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
		
		--��������������������ݿ�汾��
	    update BS_appconfig set value_TX = V_DBVersion where appconfig_CD = V_DBType;

		commit;
	
	end if;
 
end;
/

/********��һ���汾:1.0.5.130613**********/
/**********��ǰ�汾:1.0.6.131212**********/
declare
	V_Count BS_MetaFieldType.Int_NR%type;
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
	V_MaxID  BS_MetaFieldType.BigInt_NR%type;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.0.6.131212';

	--1.0.6.131212�汾���ݿ������ű�����Ҫ���ڰ汾˵����1.0.7��1.1.0�ĸĶ����漰�����ݿ�仯
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then
		
		--��������������������ݿ�汾��
	    update BS_appconfig set value_TX = V_DBVersion where appconfig_CD = V_DBType;

		commit;
	
	end if;
 
end;
/

/********��һ���汾:1.0.6.131212**********/
/**********��ǰ�汾:1.0.7.140228**********/
declare
	V_Count BS_MetaFieldType.Int_NR%type;
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
	V_MaxID  BS_MetaFieldType.BigInt_NR%type;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.0.7.140228';

	--1.0.7.140228�汾���ݿ������ű�����Ҫ���ڵ���������BS_TransPartionTable.MoveAfterDays������ֵ
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then
		
		--����ZX_HistoryData�ķ�����BS_TransPartionTable.MoveAfterDays������ֵ����"2,2,2"->"5,5,5"
		update BS_TransPartionTable set MoveAfterDays = '5,5,5' where TransKey_CD = 'ZX_HistoryData';

		--��������������������ݿ�汾��
	    update BS_appconfig set value_TX = V_DBVersion where appconfig_CD = V_DBType;

		commit;
	
	end if;
 
end;
/

/********��һ���汾:1.0.7.140228**********/
/**********��ǰ�汾:1.0.8.140410**********/
declare
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.0.8.140410';

	-- ��Ҫ���ڣ������ϸֶ���ƽ̨�Ĺ���
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then
	
		Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
			Values('ZXSMSPlatformUrl', '����ƽ̨��ַ', '����ƽ̨��ַ', null, 'String', '���Ų�ѯ', '0', 200907, 'TextBox', null);

		Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
			Values('ZXSMSPlatformUserId', '����ƽ̨�û����', '����ƽ̨�û����', null, 'String', '���Ų�ѯ', '0', 200908, 'TextBox', null);

		Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX) 
			Values('ZXSMSPlatformPassword', '����ƽ̨�û�����', '����ƽ̨�û�����', null, 'Password', '���Ų�ѯ', '0', 200909, 'TextBox', null);

		Insert Into BS_AppConfig(AppConfig_CD, Name_TX, Value_TX, DefaultValue_TX, DataType_CD, Category_CD, SysConfig_YN, SortNo_NR, ControlType_CD, Other_TX)
			Values('ZXSMSPlatformType', '����ƽ̨����', '����è', null, 'String', '���Ų�ѯ', '0', 200910, 'ComboBox', '����è;�ϸֶ���ƽ̨|����è;�ϸֶ���ƽ̨');

		--��������������������ݿ�汾��
	    update BS_appconfig set value_TX = V_DBVersion where appconfig_CD = V_DBType;

		commit;
	
	end if;
 
end;
/

/********��һ���汾:1.0.8.140410**********/
/**********��ǰ�汾:1.0.9.140510**********/
declare
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.0.9.140510';

	-- ��Ҫ���ڣ������ϸֶ���ƽ̨�Ĺ���
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then
	
		update BS_Resource set Value_TX = 'ͨѶ¼', Remark_TX = 'ͨѶ¼' where upper(Key_TX) = upper('ZX_SMSConfig.Address');

		--��������������������ݿ�汾��
	    update BS_appconfig set value_TX = V_DBVersion where appconfig_CD = V_DBType;

		commit;
	
	end if;
 
end;
/

/********��һ���汾:1.0.9.140510**********/
/**********��ǰ�汾:1.1.0.140723**********/
declare
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.1.0.140723';

	-- ��Ҫ���ڣ����������豸�����Ƿ�̳�������
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then
	
		Insert Into BS_AppConfig(AppConfig_CD,Name_TX,Value_TX,DefaultValue_TX,DataType_CD,Category_CD,SysConfig_YN,SortNo_NR,ControlType_CD,Other_TX)	
			Values('ZXMObjectCDInherit','�����豸�����Ƿ�̳�','��','��','String','�����豸����','0',200911,'ComboBox','��;��|��;��');

		--��������������������ݿ�汾��
	    update BS_appconfig set value_TX = V_DBVersion where appconfig_CD = V_DBType;

		commit;
	
	end if;
 
end;
/

/********��һ���汾:1.1.0.140723**********/
/**********��ǰ�汾:1.1.1.140826**********/
declare
	V_Count int;
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.1.1.140826';

	-- ��Ҫ���ڣ��ɼ�վ�������ͺ�����ݿ����������ݽ��м����Դ���
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then
	
		-- ��������ʹ�õ��ź����͡�������
		V_Count := 0;
		select Count(*) Into V_Count From Z_SignType where SignType_ID = 113;
		if V_Count = 0 then
			
			Insert Into Z_SignType(SignType_ID,Name_TX,DataType_ID,UnitType_ID,OnLine_YN,OffLine_YN,SortNo_NR) Values(113,'����',1,55,'0','0',4);
				
		end if;
		
		-- ��������ʹ�õ��ź����͡���λ�ơ�
		V_Count := 0;
		select Count(*) Into V_Count From Z_SignType where SignType_ID = 120;
		if V_Count = 0 then
			
			Insert Into Z_SignType(SignType_ID,Name_TX,DataType_ID,UnitType_ID,OnLine_YN,OffLine_YN,SortNo_NR) Values(120,'��λ��',1,10,'1','0',6);
				
		end if;
		
		-- ��������ʹ�õ��ź����͡���λ�á�
		V_Count := 0;
		select Count(*) Into V_Count From Z_SignType where SignType_ID = 121;
		if V_Count = 0 then
			
			Insert Into Z_SignType(SignType_ID,Name_TX,DataType_ID,UnitType_ID,OnLine_YN,OffLine_YN,SortNo_NR) Values(121,'��λ��',1,10,'1','0',7);
				
		end if;
		
		-- ��������ʹ�õ��ź����͡����������Ϊ�����������������ʹ�ü��ٶ��ź����͵ĵ�λ
		update Z_SignType set Name_TX = '�������', UnitType_ID = 21, SortNo_NR = 8 where SignType_ID = 119;

		delete from Z_EngUnit where EngUnit_ID = 503 and UnitType_ID =10;


		-- ���������������̵�λ
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnit where EngUnit_ID = 41 and UnitType_ID = 12;
		if V_Count = 0 then
			Insert Into Z_EngUnit(EngUnit_ID,UnitType_ID,NameC_TX,NameE_TX,Scale_NR,Offset_NR,IsDefault_YN,IsInner_YN) Values(41,12,'���϶�','�H',1.8,32,'0','1');
		end if;
		
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnit where EngUnit_ID = 79 and UnitType_ID = 21;
		if V_Count = 0 then
			Insert Into Z_EngUnit(EngUnit_ID,UnitType_ID,NameC_TX,NameE_TX,Scale_NR,Offset_NR,IsDefault_YN,IsInner_YN) Values(79,21,'�������ٶ�','g',0.1019762,0,'0','1');
		end if;
		
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnit where EngUnit_ID = 245 and UnitType_ID = 74;
		if V_Count = 0 then
			Insert Into Z_EngUnit(EngUnit_ID,UnitType_ID,NameC_TX,NameE_TX,Scale_NR,Offset_NR,IsDefault_YN,IsInner_YN) Values(245,74,'����','mV',1000,0,'0','1');
		end if;
		
		
		-- ��������Ҫʹ�õĴ����������ȵĵ�λ����
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnitType where UnitType_ID = 1001;
		if V_Count = 0 then
			Insert Into Z_EngUnitType(UnitType_ID,Name_TX,Description_TX) Values(1001,'���ٶȴ�����������','���ٶȴ�����������');
		end if;
		
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnitType where UnitType_ID = 1002;
		if V_Count = 0 then
			Insert Into Z_EngUnitType(UnitType_ID,Name_TX,Description_TX) Values(1002,'�ٶȴ�����������','�ٶȴ�����������');
		end if;
		
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnitType where UnitType_ID = 1003;
		if V_Count = 0 then
			Insert Into Z_EngUnitType(UnitType_ID,Name_TX,Description_TX) Values(1003,'λ�ƴ�����������','λ�ƴ�����������');
		end if;
		
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnitType where UnitType_ID = 1004;
		if V_Count = 0 then
			Insert Into Z_EngUnitType(UnitType_ID,Name_TX,Description_TX) Values(1004,'�¶ȴ�����������','�¶ȴ�����������');
		end if;
		
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnitType where UnitType_ID = 1005;
		if V_Count = 0 then
			Insert Into Z_EngUnitType(UnitType_ID,Name_TX,Description_TX) Values(1005,'����������������','����������������');
		end if;
		
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnitType where UnitType_ID = 1006;
		if V_Count = 0 then
			Insert Into Z_EngUnitType(UnitType_ID,Name_TX,Description_TX) Values(1006,'��ѹ������������','��ѹ������������');
		end if;
		
		
		-- ����������Ҫʹ�õĴ����������ȵ�λ
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
			Insert Into Z_EngUnit(EngUnit_ID,UnitType_ID,NameC_TX,NameE_TX,Scale_NR,Offset_NR,IsDefault_YN,IsInner_YN) Values(20041,1004,'mA/��','mA/��',1,0,'1','1');
		end if;
		
		V_Count := 0;
		select Count(*) Into V_Count From Z_EngUnit where EngUnit_ID = 20042 and UnitType_ID = 1004;
		if V_Count = 0 then
			Insert Into Z_EngUnit(EngUnit_ID,UnitType_ID,NameC_TX,NameE_TX,Scale_NR,Offset_NR,IsDefault_YN,IsInner_YN) Values(20042,1004,'mA/�H','mA/�H',1,0,'0','1');
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

		--��������������������ݿ�汾��
	    update BS_appconfig set value_TX = V_DBVersion where appconfig_CD = V_DBType;

		commit;
	
	end if;
 
end;
/

/********��һ���汾:1.1.1.140826**********/
/**********��ǰ�汾:1.1.1.141021**********/
declare
	V_Count int;
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
	V_MaxID  BS_MetaFieldType.BigInt_NR%type;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.1.1.141021';

	-- ��Ҫ���ڣ���Բ�ͬ�豸���ò�ͬ�������ŷ�������
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then
		
		--*****************�޸ı�BS_ObjectKey�е�����*****************--
		--����Pnt_DataVar.Var_ID��ֵ��¼
		V_MaxID := 1000000;
		V_Count := 0;
		select count(*) into V_Count from BS_ObjectKey where Source_CD = 'ZX_MobSMSAlmConfig' and KeyName = 'Config_ID';
		if V_Count = 0 then
			insert into  BS_ObjectKey(Source_CD, KeyName, KeyValue, LockTime_TM) 
				values ('ZX_MobSMSAlmConfig', 'Config_ID', V_MaxID, DBDiffPackage.GetSystemTime());
		end if;

		--*****************������ZX_MobSMSAlmConfig*****************--
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

		--��������������������ݿ�汾��
	    update BS_appconfig set value_TX = V_DBVersion where appconfig_CD = V_DBType;
	
		commit;

	end if;
 
end;
/

/********��һ���汾:1.1.1.141021**********/
/**********��ǰ�汾:1.1.2.141202**********/
declare
	V_DBType BS_MetaFieldType.OneHundred_TX%type;
	V_DBVersion BS_MetaFieldType.OneHundred_TX%type;
begin
 
	V_DBType := 'ZXDBVERSION';
	V_DBVersion := '1.1.2.141202';

	-- ��Ҫ���ڣ���¼�������ŷ���ʧ�ܵ��쳣��Ϣ,ɾ��ԭ�������������ñ�
	if CommonPackage.F_CompareToDbVersion(V_DBType, V_DBVersion) > 0 then
		
		execute immediate 'drop table ZX_MobSMSAlmConfig';
		execute immediate 'alter table ZX_SMSHistory add Memo_TX varchar(2000) null';
		execute immediate 'update ZX_SMSHistory set Partition_ID=20000000000000+Partition_ID where Partition_ID<20000000000000';

		--��������������������ݿ�汾��
	    update BS_appconfig set value_TX = V_DBVersion where appconfig_CD = V_DBType;
	
		commit;

	end if;
 
end;
/


/**********���������ĸ��汾�����ݿ⣬���Ҫִ�еĽű�����**********/
declare
v_emsg BS_MetaFieldType.FiveKilo_TX%type;
begin
   PartitionPackage.InitTableStruct(v_emsg);
end;
/

commit;