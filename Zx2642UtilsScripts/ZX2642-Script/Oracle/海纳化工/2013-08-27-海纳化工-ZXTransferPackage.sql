--用于海纳化工西区Oracle服务器与东区Oracle服务器之间数据同步的定制脚本。
--该脚本在海钠化工西区Oracle服务器上执行，西区服务器作为主服务器。
--西区Oracle服务器通过DBLink方式访问东区Oracle服务器。
--DBLink的参数需要根据现场具体的服务器配置情况进行设置。
--DBLink的参数在“创建远程Oracle的DBLink”的脚本段落中进行设置，主要设置项：V_DBLinkName、V_RemoteIP、V_RemotePort、V_RemoteService、V_RemoteDBName、V_RemoteDBPassword。
--每天凌晨1点的时候将西区Oracle服务器的设备和测点等数据批量同步到东区Oracle服务器。
--每隔5分钟将东区Oracle服务器的历史数据与报警数据同步到西区Oracle服务器。


--创建远程Oracle的DBLink
declare
	V_DBLinkName BS_MetaFieldType.Fifty_TX%type;
	V_RemoteIP BS_MetaFieldType.Fifty_TX%type;
	V_RemotePort BS_MetaFieldType.Fifty_TX%type;
	V_RemoteService BS_MetaFieldType.Fifty_TX%type;
	V_RemoteDBName BS_MetaFieldType.Fifty_TX%type;
	V_RemoteDBPassword BS_MetaFieldType.Fifty_TX%type;
	V_Sql BS_MetaFieldType.FiveKilo_TX%type;
	V_Count BS_MetaFieldType.Int_NR%type;
begin

	V_DBLinkName := 'to_remote';
	V_RemoteIP := '10.3.2.33';
	V_RemotePort := '1521';
	V_RemoteService := 'online';
	V_RemoteDBName := 'st';
	V_RemoteDBPassword := 'db877350';
	
	V_Sql := 'select count(*) from dba_objects where object_type = ''DATABASE LINK'' and upper(object_name) = ''' || upper(V_DBLinkName) || '''';
	execute immediate V_Sql into V_Count;
	
	if (V_Count > 0) then
		V_Sql := 'drop database link ' || V_DBLinkName;
		execute immediate V_Sql;
	end if;
	
	V_Sql := 'create database link ' || V_DBLinkName || 
		' connect to ' || V_RemoteDBName || ' identified by ' || V_RemoteDBPassword || 
		' using ''(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)' || 
		'(HOST=' || V_RemoteIP || ')(PORT= ' || V_RemotePort || ')))' || 
		'(CONNECT_DATA=(SERVICE_NAME=' || V_RemoteService || ')))''';
	execute immediate V_Sql;
	
end;
/

select owner, object_name from dba_objects where object_type='DATABASE LINK';


--创建Oracle本地服务器上的数据表
declare
	V_Sql BS_MetaFieldType.FiveKilo_TX%type;
begin

	--Oracle本地服务器与远程服务器数据ID对照表。
	if OracleObjectMM.f_CheckTableExists('Transfer_IDMapping') = '1' then
		V_Sql := 'drop table Transfer_IDMapping';
		execute immediate V_Sql;
	end if;

	V_Sql := 'create table Transfer_IDMapping 
				(
					IDType integer NOT NULL, 
					SourceID number(19) NOT NULL, 
					TargetID number(19) NOT NULL, 
					CONSTRAINT PK_Transfer_IDMapping PRIMARY KEY (IDType, SourceID)
				)';
	execute immediate V_Sql;

	--Oracle本地服务器的需要同步数据到远程Oracle服务器的表数据
	if OracleObjectMM.f_CheckTableExists('Transfer_TableName') = '1' then
		V_Sql := 'drop table Transfer_TableName';
		execute immediate V_Sql;
	end if;

	V_Sql := 'create table Transfer_TableName 
				(
					Table_ID number(19) NOT NULL, 
					TableName_TX varchar2(1000) NULL, 
					CONSTRAINT PK_Transfer_TableName PRIMARY KEY (Table_ID)
				)';
	execute immediate V_Sql;

end;
/


--ID映射表的初始化数据
declare
	V_SynchNRMappingIDType BS_MetaFieldType.Int_NR%type;
	V_AlmIDMappingIDType BS_MetaFieldType.Int_NR%type;
begin

	delete from Transfer_IDMapping;
	commit;
	
	V_SynchNRMappingIDType := 0;
	V_AlmIDMappingIDType := 1;

	insert into Transfer_IDMapping( IDType, SourceID, TargetID ) values( V_SynchNRMappingIDType, 0, 0 );
	insert into Transfer_IDMapping( IDType, SourceID, TargetID ) values( V_AlmIDMappingIDType, 0, 0 );
	commit;

end;
/


--Oracle本地服务器上数据要转移到Oracle远程服务器上的相关数据表的初始化数据
declare
	V_TableID BS_MetaFieldType.Int_NR%type;
begin

	delete from Transfer_TableName;
	commit;
	
	V_TableID := 0;

	V_TableID := V_TableID + 1;
	insert into Transfer_TableName( Table_ID, TableName_TX ) values( V_TableID, 'BS_Org' );
	V_TableID := V_TableID + 1;
	insert into Transfer_TableName( Table_ID, TableName_TX ) values( V_TableID, 'BS_Dept' );
	V_TableID := V_TableID + 1;
	insert into Transfer_TableName( Table_ID, TableName_TX ) values( V_TableID, 'BS_AppRole' );
	V_TableID := V_TableID + 1;
	insert into Transfer_TableName( Table_ID, TableName_TX ) values( V_TableID, 'BS_Post' );
	V_TableID := V_TableID + 1;
	insert into Transfer_TableName( Table_ID, TableName_TX ) values( V_TableID, 'BS_AppRoleAction' );
	V_TableID := V_TableID + 1;
	insert into Transfer_TableName( Table_ID, TableName_TX ) values( V_TableID, 'BS_PostAppAction' );
	V_TableID := V_TableID + 1;
	insert into Transfer_TableName( Table_ID, TableName_TX ) values( V_TableID, 'BS_AppUser' );
	V_TableID := V_TableID + 1;
	insert into Transfer_TableName( Table_ID, TableName_TX ) values( V_TableID, 'BS_AppuserPost' );
	V_TableID := V_TableID + 1;
	insert into Transfer_TableName( Table_ID, TableName_TX ) values( V_TableID, 'BS_PostChange' );
	V_TableID := V_TableID + 1;
	insert into Transfer_TableName( Table_ID, TableName_TX ) values( V_TableID, 'BS_AppUserSpec' );
	V_TableID := V_TableID + 1;
	insert into Transfer_TableName( Table_ID, TableName_TX ) values( V_TableID, 'Mob_MObject' );
	V_TableID := V_TableID + 1;
	insert into Transfer_TableName( Table_ID, TableName_TX ) values( V_TableID, 'Mob_MobjectStructure' );
	V_TableID := V_TableID + 1;
	insert into Transfer_TableName( Table_ID, TableName_TX ) values( V_TableID, 'Pnt_Point' );
	V_TableID := V_TableID + 1;
	insert into Transfer_TableName( Table_ID, TableName_TX ) values( V_TableID, 'Pnt_PointGroup' );
	V_TableID := V_TableID + 1;
	insert into Transfer_TableName( Table_ID, TableName_TX ) values( V_TableID, 'AlmStand_CommonSetting' );
	V_TableID := V_TableID + 1;
	insert into Transfer_TableName( Table_ID, TableName_TX ) values( V_TableID, 'AlmStand_PntCommon' );
	V_TableID := V_TableID + 1;
	insert into Transfer_TableName( Table_ID, TableName_TX ) values( V_TableID, 'Analysis_OrgPicture' );
	V_TableID := V_TableID + 1;
	insert into Transfer_TableName( Table_ID, TableName_TX ) values( V_TableID, 'Analysis_MObjPicture' );
	V_TableID := V_TableID + 1;
	insert into Transfer_TableName( Table_ID, TableName_TX ) values( V_TableID, 'Analysis_MObjPosition' );
	V_TableID := V_TableID + 1;
	insert into Transfer_TableName( Table_ID, TableName_TX ) values( V_TableID, 'Analysis_PntPosition' );
	commit;

end;
/


--建立在本地Oracle服务器上的存储过程，通过DBLink方式连接远程Oracle服务器，
--从远程Oracle服务器上将历史数据和报警数据转移到本地Oracle服务器上。
--将本地Oracle服务器上的设备和测点数据转移到远程Oracle服务器上。
CREATE OR REPLACE PACKAGE HNZXTransferPackage
As
Type T_Cursor Is Ref Cursor;

procedure Pr_TransferHistoryData( 
            P_Emsg Out BS_MetaFieldType.Emsg_TX%type);
			
procedure Pr_TransferAlarmData( 
            P_Emsg Out BS_MetaFieldType.Emsg_TX%type);
			
procedure Pr_TransferPointData( 
			P_Emsg Out BS_MetaFieldType.Emsg_TX%type);

Procedure Pr_TransferDataMain;

Procedure Pr_TransferPointMain;
			
End HNZXTransferPackage;
/


CREATE OR REPLACE PACKAGE BODY HNZXTransferPackage
As

procedure Pr_TransferHistoryData( 
            P_Emsg Out BS_MetaFieldType.Emsg_TX%type)
as
	V_HistoryID BS_MetaFieldType.BigInt_NR%type;
	V_NewHistoryID BS_MetaFieldType.BigInt_NR%type;
	V_WaveformID BS_MetaFieldType.BigInt_NR%type;
	V_NewWaveformID BS_MetaFieldType.BigInt_NR%type;
	V_FeatureValuePKID BS_MetaFieldType.BigInt_NR%type;
	V_NewFeatureValuePKID BS_MetaFieldType.BigInt_NR%type;
	V_AlmID BS_MetaFieldType.BigInt_NR%type;
	V_NewAlmID BS_MetaFieldType.BigInt_NR%type;
	V_SynchNR BS_MetaFieldType.BigInt_NR%type;
	V_NewSynchNR BS_MetaFieldType.BigInt_NR%type;
	
	V_RemoteDBLinkName BS_MetaFieldType.Fifty_TX%type;
	
	V_Summary BS_MetaFieldType.Fifty_TX%type;
	V_Waveform BS_MetaFieldType.Fifty_TX%type;
	V_FeatureValue BS_MetaFieldType.Fifty_TX%type;
	V_DataMapping BS_MetaFieldType.Fifty_TX%type;
	V_HistoryAlm BS_MetaFieldType.Fifty_TX%type;
	V_MobjectWarning BS_MetaFieldType.Fifty_TX%type;
	V_IDMapping BS_MetaFieldType.Fifty_TX%type;
	
	V_SummaryLocal BS_MetaFieldType.Fifty_TX%type;
	V_WaveformLocal BS_MetaFieldType.Fifty_TX%type;
	V_FeatureValueLocal BS_MetaFieldType.Fifty_TX%type;
	V_HistoryAlmLocal BS_MetaFieldType.Fifty_TX%type;
	V_MobjectWarningLocal BS_MetaFieldType.Fifty_TX%type;
	V_IDMappingLocal BS_MetaFieldType.Fifty_TX%type;
	
	V_SummaryRemote BS_MetaFieldType.Fifty_TX%type;
	V_WaveformRemote BS_MetaFieldType.Fifty_TX%type;
	V_FeatureValueRemote BS_MetaFieldType.Fifty_TX%type;
	V_HistoryAlmRemote BS_MetaFieldType.Fifty_TX%type;
	V_MobjectWarningRemote BS_MetaFieldType.Fifty_TX%type;
	V_DataMappingRemote BS_MetaFieldType.Fifty_TX%type;
	
	V_Sql BS_MetaFieldType.FiveKilo_TX%type;
	
	V_SynchNRMappingIDType BS_MetaFieldType.Int_NR%type;
	V_AlmIDMappingIDType BS_MetaFieldType.Int_NR%type;
	V_Count BS_MetaFieldType.Int_NR%type;
	
	V_SummaryCursor T_Cursor;
	V_WavefromCursor T_Cursor;
	V_FeatureValueCursor T_Cursor;
begin

	V_RemoteDBLinkName := 'to_remote';
	
	V_Summary := 'ZX_History_Summary';
	V_Waveform := 'ZX_History_Waveform';
	V_FeatureValue := 'ZX_History_FeatureValue';
	V_DataMapping := 'ZX_History_DataMapping';
	V_HistoryAlm := 'ZX_History_Alm';
	V_MobjectWarning := 'ZT_MobjectWarning';
	V_IDMapping := 'Transfer_IDMapping';
	
	V_SummaryLocal := V_Summary;
	V_WaveformLocal := V_Waveform;
	V_FeatureValueLocal := V_FeatureValue;
	V_HistoryAlmLocal := V_HistoryAlm;
	V_MobjectWarningLocal := V_MobjectWarning;
	V_IDMappingLocal := V_IDMapping;
	
	V_SummaryRemote := V_Summary || '@' || V_RemoteDBLinkName;
	V_WaveformRemote := V_Waveform || '@' || V_RemoteDBLinkName;
	V_FeatureValueRemote := V_FeatureValue || '@' || V_RemoteDBLinkName;
	V_HistoryAlmRemote := V_HistoryAlm || '@' || V_RemoteDBLinkName;
	V_MobjectWarningRemote := V_MobjectWarning || '@' || V_RemoteDBLinkName;
	V_DataMappingRemote := V_DataMapping || '@' || V_RemoteDBLinkName;
	
	V_SynchNRMappingIDType := 0;
	V_AlmIDMappingIDType := 1;
	
    begin
	
		V_Sql := 'select History_ID, Alm_ID, Synch_NR from ' || V_SummaryRemote || ' order by History_ID';
		--DBDiffPackage.Pr_DebugPrint(V_Sql);
		open V_SummaryCursor for V_Sql;
		
		while (1=1) loop
			
			fetch V_SummaryCursor into V_HistoryID, V_AlmID, V_SynchNR;
			if (V_SummaryCursor%NOTFOUND) then
				exit;
			end if;
			
			V_Sql := 'select count(*) from ' || V_IDMappingLocal || 
				' where IDType = ' || DBDiffPackage.IntToString(V_AlmIDMappingIDType) || 
				' and SourceID = ' || DBDiffPackage.IntToString(V_AlmID);
			--DBDiffPackage.Pr_DebugPrint(V_Sql);
			execute immediate V_Sql into V_Count;
			
			if (V_AlmID > 0 and V_Count = 0) then
				XTGLPackage.Pr_GetLastBigObjectID( V_HistoryAlm, 'Alm_ID', 1, V_NewAlmID );
				V_Sql := 'insert into ' || V_IDMappingLocal || '(IDType, SourceID, TargetID)' || 
					' values(' || DBDiffPackage.IntToString(V_AlmIDMappingIDType) || 
					', ' || DBDiffPackage.IntToString(V_AlmID) || 
					', ' || DBDiffPackage.IntToString(V_NewAlmID) || ')';
				--DBDiffPackage.Pr_DebugPrint(V_Sql);
				execute immediate V_Sql;
				
				/*
				V_Sql := 'insert into ' || V_MobjectWarningLocal || 
					'( MobjectWarning_ID, Warning_CD, Mobject_ID, DJOwner_ID, AlmRecType_CD, AlmLevel_ID, Spec_ID,' || 
                    ' Content_TX, DutyPost_ID, CheckUser_ID, CheckDate_DT, WarningNum_NR, DealWithType_TX, Close_YN )' || 
					' select ' || V_NewAlmID || ' as MobjectWarning_ID, Warning_CD, Mobject_ID, DJOwner_ID, AlmRecType_CD,' || 
					' AlmLevel_ID, Spec_ID, Content_TX, DutyPost_ID, CheckUser_ID, CheckDate_DT, WarningNum_NR, DealWithType_TX, Close_YN' || 
					' from ' || V_MobjectWarningRemote || 
					' where MobjectWarning_ID = ' || DBDiffPackage.IntToString(V_AlmID);
				--DBDiffPackage.Pr_DebugPrint(V_Sql);
				execute immediate V_Sql;
				
				V_Sql := 'delete from ' || V_MobjectWarningRemote || 
					' where MobjectWarning_ID = ' || DBDiffPackage.IntToString(V_AlmID);
				--DBDiffPackage.Pr_DebugPrint(V_Sql);
				execute immediate V_Sql;
				
				V_Sql := 'insert into ' || V_HistoryAlmLocal || 
					' select Partition_ID, ' || V_NewAlmID || ' as Alm_ID, FeatureValue_ID, AlmLevel_ID, Alm_DT, MObject_ID,' || 
					' Point_ID, AlmDesc_TX, DealWithStatus_CD, Memo_TX, OwnerDept_ID, OwnerUser_ID, DealWithUser_ID, DealWith_DT,' || 
					' DealWithRel_CD, DealWithType_NR, DealWithResult_TX, DealWithFromRel_CD' || 
					' from ' || V_HistoryAlmRemote || 
					' where Alm_ID = ' || DBDiffPackage.IntToString(V_AlmID);
				--DBDiffPackage.Pr_DebugPrint(V_Sql);
				execute immediate V_Sql;
				
				V_Sql := 'delete from ' || V_HistoryAlmRemote || 
					' where Alm_ID = ' || DBDiffPackage.IntToString(V_AlmID);
				--DBDiffPackage.Pr_DebugPrint(V_Sql);
				execute immediate V_Sql;
				*/
			end if;
			
			V_Sql := 'select TargetID from ' || V_IDMappingLocal || 
				' where IDType = ' || DBDiffPackage.IntToString(V_AlmIDMappingIDType) || 
				' and SourceID = ' || DBDiffPackage.IntToString(V_AlmID);
			--DBDiffPackage.Pr_DebugPrint(V_Sql);
			execute immediate V_Sql into V_NewAlmID;
			
			V_Sql := 'select count(*) from ' || V_IDMappingLocal || 
				' where IDType = ' || DBDiffPackage.IntToString(V_SynchNRMappingIDType) || 
				' and SourceID = ' || DBDiffPackage.IntToString(V_SynchNR);
			--DBDiffPackage.Pr_DebugPrint(V_Sql);
			execute immediate V_Sql into V_Count;
			
			if (V_Count = 0) then
				XTGLPackage.Pr_GetLastBigObjectID( V_Summary, 'Synch_NR', 1, V_NewSynchNR );
				V_Sql := 'insert into ' || V_IDMappingLocal || '(IDType, SourceID, TargetID)' || 
					' values(' || DBDiffPackage.IntToString(V_SynchNRMappingIDType) || 
					', ' || DBDiffPackage.IntToString(V_SynchNR) || 
					', ' || DBDiffPackage.IntToString(V_NewSynchNR) || ')';
				--DBDiffPackage.Pr_DebugPrint(V_Sql);
				execute immediate V_Sql;
			end if;
			
			V_Sql := 'select TargetID from ' || V_IDMappingLocal || 
				' where IDType = ' || DBDiffPackage.IntToString(V_SynchNRMappingIDType) || 
				' and SourceID = ' || DBDiffPackage.IntToString(V_SynchNR);
			--DBDiffPackage.Pr_DebugPrint(V_Sql);
			execute immediate V_Sql into V_NewSynchNR;
			
			XTGLPackage.Pr_GetLastBigObjectID( V_Summary, 'History_ID', 1, V_NewHistoryID );
			
			V_Sql := 'insert into ' || V_SummaryLocal || 
				' select Partition_ID, ' || V_NewHistoryID || ' as History_ID, Point_ID, Compress_ID, CompressType_ID,' || 
				' PntDim_NR, DatType_NR, SigType_NR, SampTime_DT, SampTimeGMT_DT, DatLen_NR, RotSpeed_NR, MinFreq_NR,' || 
				' SampleFreq_NR, SampMod_NR, Result_TX, AlmLevel_ID, DataGroup_NR, ' || 
				V_NewAlmID || ' as Alm_ID, EngUnit_ID, ' || V_NewSynchNR || ' as Synch_NR' || 
				' from ' || V_SummaryRemote || 
				' where History_ID = ' || DBDiffPackage.IntToString(V_HistoryID);
			--DBDiffPackage.Pr_DebugPrint(V_Sql);
			execute immediate V_Sql;
			
			V_Sql := 'select Waveform_ID from ' || V_WaveformRemote || 
				' where History_ID = ' || DBDiffPackage.IntToString(V_HistoryID) || 
				' order by Waveform_ID';
			--DBDiffPackage.Pr_DebugPrint(V_Sql);
			open V_WavefromCursor for V_Sql;
			
			while (1=1) loop
			
				fetch V_WavefromCursor into V_WaveformID;
				if (V_WavefromCursor%NOTFOUND) then
					exit;
				end if;
			
				XTGLPackage.Pr_GetLastBigObjectID( V_Waveform, 'Waveform_ID', 1, V_NewWaveformID );
				
				V_Sql := 'insert into ' || V_WaveformLocal || 
					' select ' || V_NewWaveformID || ' as Waveform_ID, ' || V_NewHistoryID || ' as History_ID, ChnNo_NR, Partition_ID, WaveformType_ID,' || 
					' SigType_NR, DatLen_NR, RotSpeed_NR, MinFreq_NR, SampleFreq_NR, SampMod_NR, EngUnit_ID, Demod_YN,' || 
					' DMinFreq_NR, DMaxFreq_NR, Wave_GR, Compress_NR, WaveScale_NR' || 
					' from ' || V_WaveformRemote || 
					' where Waveform_ID = ' || DBDiffPackage.IntToString(V_WaveformID);
				--DBDiffPackage.Pr_DebugPrint(V_Sql);
				execute immediate V_Sql;
			
			end loop;
			
			close V_WavefromCursor;
			
			V_Sql := 'select FeatureValuePK_ID from ' || V_FeatureValueRemote || 
				' where History_ID = ' || DBDiffPackage.IntToString(V_HistoryID) || 
				' order by FeatureValuePK_ID';
			--DBDiffPackage.Pr_DebugPrint(V_Sql);
			open V_FeatureValueCursor for V_Sql;
			
			while (1=1) loop
				
				fetch V_FeatureValueCursor into V_FeatureValuePKID;
				if (V_FeatureValueCursor%NOTFOUND) then
					exit;
				end if;
			
				XTGLPackage.Pr_GetLastBigObjectID( V_FeatureValue, 'FeatureValuePK_ID', 1, V_NewFeatureValuePKID );
				
				V_Sql := 'insert into ' || V_FeatureValueLocal || 
					' select ' || V_NewFeatureValuePKID || ' as FeatureValuePK_ID, ' || V_NewHistoryID || ' as History_ID, ChnNo_NR, FeatureValueType_ID, FeatureValue_ID,' || 
					' Partition_ID, FeatureValue_NR, SigType_NR, EngUnit_ID' || 
					' from ' || V_FeatureValueRemote || 
					' where FeatureValuePK_ID = ' || DBDiffPackage.IntToString(V_FeatureValuePKID);
				--DBDiffPackage.Pr_DebugPrint(V_Sql);
				execute immediate V_Sql;
				
			end loop;
			
			close V_FeatureValueCursor;
			
			V_Sql := 'delete from ' || V_DataMappingRemote || 
				' where History_ID = ' || DBDiffPackage.IntToString(V_HistoryID);
			--DBDiffPackage.Pr_DebugPrint(V_Sql);
			execute immediate V_Sql;
			
			V_Sql := 'delete from ' || V_SummaryRemote || 
				' where History_ID = ' || DBDiffPackage.IntToString(V_HistoryID);
			--DBDiffPackage.Pr_DebugPrint(V_Sql);
			execute immediate V_Sql;
			
			commit;
			
		end loop;
		
		close V_SummaryCursor;
		
    exception
	when others then
	
      P_Emsg := SQLERRM;
      rollback;
	  return;
	  
    end;
	
	commit;
	
end;

procedure Pr_TransferAlarmData( 
            P_Emsg Out BS_MetaFieldType.Emsg_TX%type)
as
	V_PartitionID ZX_History_Alm.Partition_ID%type;
	V_FeatureValueID ZX_History_Alm.FeatureValue_ID%type;
	V_PointID ZX_History_Alm.Point_ID%type;
	V_AlmDT ZX_History_Alm.Alm_Dt%type;
	V_AlmLevelID ZX_History_Alm.Almlevel_Id%type;
	V_AlmDescTX ZX_History_Alm.Almdesc_Tx%type;
	V_MobjectID mob_mobject.mobject_id%type;
	V_OwnerPostID Mob_Mobject.Djowner_Id%type;
	V_MobSpecID Mob_Mobject.Spec_Id%type;
	V_UserID Bs_Appuser.Appuser_Id%type;

	V_AlmID BS_MetaFieldType.BigInt_NR%type;
	V_NewAlmID BS_MetaFieldType.BigInt_NR%type;
	
	V_RemoteDBLinkName BS_MetaFieldType.Fifty_TX%type;
	
	V_HistoryAlm BS_MetaFieldType.Fifty_TX%type;
	V_MobjectWarning BS_MetaFieldType.Fifty_TX%type;
	V_IDMapping BS_MetaFieldType.Fifty_TX%type;
	
	V_HistoryAlmLocal BS_MetaFieldType.Fifty_TX%type;
	V_MobjectWarningLocal BS_MetaFieldType.Fifty_TX%type;
	V_IDMappingLocal BS_MetaFieldType.Fifty_TX%type;
	
	V_HistoryAlmRemote BS_MetaFieldType.Fifty_TX%type;
	V_MobjectWarningRemote BS_MetaFieldType.Fifty_TX%type;
	
	V_Sql BS_MetaFieldType.FiveKilo_TX%type;
	
	V_AlmIDMappingIDType BS_MetaFieldType.Int_NR%type;
	V_Count BS_MetaFieldType.Int_NR%type;
	
	V_HistoryAlmCursor T_Cursor;
begin

	V_RemoteDBLinkName := 'to_remote';
	
	V_HistoryAlm := 'ZX_History_Alm';
	V_MobjectWarning := 'ZT_MobjectWarning';
	V_IDMapping := 'Transfer_IDMapping';
	
	V_HistoryAlmLocal := V_HistoryAlm;
	V_MobjectWarningLocal := V_MobjectWarning;
	V_IDMappingLocal := V_IDMapping;
	
	V_HistoryAlmRemote := V_HistoryAlm || '@' || V_RemoteDBLinkName;
	V_MobjectWarningRemote := V_MobjectWarning || '@' || V_RemoteDBLinkName;

	V_AlmIDMappingIDType := 1;
	
    begin
	
		V_Sql := 'select Partition_ID, Alm_ID, FeatureValue_ID, AlmLevel_ID,' || 
			' Alm_DT, MObject_ID, Point_ID, AlmDesc_TX, OwnerUser_ID from ' || V_HistoryAlmRemote || 
			' where FeatureValue_ID <= 10000 order by Alm_ID';
		--DBDiffPackage.Pr_DebugPrint(V_Sql);
		open V_HistoryAlmCursor for V_Sql;
		
		while (1=1) loop
			
			fetch V_HistoryAlmCursor into V_PartitionID, V_AlmID, V_FeatureValueID, V_AlmLevelID, V_AlmDT, V_MobjectID, V_PointID, V_AlmDescTX, V_OwnerPostID;
			if (V_HistoryAlmCursor%NOTFOUND) then
				exit;
			end if;
			
			V_Sql := 'select count(*) from ' || V_IDMappingLocal || 
				' where IDType = ' || DBDiffPackage.IntToString(V_AlmIDMappingIDType) || 
				' and SourceID = ' || DBDiffPackage.IntToString(V_AlmID);
			--DBDiffPackage.Pr_DebugPrint(V_Sql);
			execute immediate V_Sql into V_Count;
			
			if (V_AlmID > 0 and V_Count = 0) then
				XTGLPackage.Pr_GetLastBigObjectID( V_HistoryAlm, 'Alm_ID', 1, V_NewAlmID );
				V_Sql := 'insert into ' || V_IDMappingLocal || '(IDType, SourceID, TargetID)' || 
					' values(' || DBDiffPackage.IntToString(V_AlmIDMappingIDType) || 
					', ' || DBDiffPackage.IntToString(V_AlmID) || 
					', ' || DBDiffPackage.IntToString(V_NewAlmID) || ')';
				--DBDiffPackage.Pr_DebugPrint(V_Sql);
				execute immediate V_Sql;
			end if;

			V_Sql := 'select TargetID from ' || V_IDMappingLocal || 
				' where IDType = ' || DBDiffPackage.IntToString(V_AlmIDMappingIDType) || 
				' and SourceID = ' || DBDiffPackage.IntToString(V_AlmID);
			--DBDiffPackage.Pr_DebugPrint(V_Sql);
			execute immediate V_Sql into V_NewAlmID;

			V_Sql := 'select count(*) from ' || V_MobjectWarningRemote || 
				' where MobjectWarning_ID = ' || DBDiffPackage.IntToString(V_AlmID);
			execute immediate V_Sql into V_Count;

			if (V_Count = 1) then
				V_Sql := 'select Spec_ID, CheckUser_ID from ' || V_MobjectWarningRemote || 
					' where MobjectWarning_ID = ' || DBDiffPackage.IntToString(V_AlmID);
				execute immediate V_Sql into V_MobSpecID, V_UserID;

				ZXSamplePackage.InsertAlmRecord(V_PartitionID, V_NewAlmID, V_FeatureValueID, V_PointID, V_AlmDT, V_AlmLevelID, V_AlmDescTX, V_MobjectID, V_OwnerPostID, V_MobSpecID, V_UserID); 
			end if;
			
			V_Sql := 'delete from ' || V_MobjectWarningRemote || 
				' where MobjectWarning_ID = ' || DBDiffPackage.IntToString(V_AlmID);
			--DBDiffPackage.Pr_DebugPrint(V_Sql);
			execute immediate V_Sql;
			
			V_Sql := 'delete from ' || V_HistoryAlmRemote || 
				' where Alm_ID = ' || DBDiffPackage.IntToString(V_AlmID);
			--DBDiffPackage.Pr_DebugPrint(V_Sql);
			execute immediate V_Sql;
			
			commit;
			
		end loop;
		
		close V_HistoryAlmCursor;

		--转移电池电量低和传感器失效报警相关的报警记录，这些记录无需关联历史数据，也无需记录已经映射的Alm_ID，在本地库中根据报警类型等信息判断计数。
		V_Sql := 'select Partition_ID, Alm_ID, FeatureValue_ID, AlmLevel_ID,' || 
			' Alm_DT, MObject_ID, Point_ID, AlmDesc_TX, OwnerUser_ID from ' || V_HistoryAlmRemote || 
			' where FeatureValue_ID > 10000 order by Alm_ID';
		--DBDiffPackage.Pr_DebugPrint(V_Sql);
		open V_HistoryAlmCursor for V_Sql;
		
		while (1=1) loop
			
			fetch V_HistoryAlmCursor into V_PartitionID, V_AlmID, V_FeatureValueID, V_AlmLevelID, V_AlmDT, V_MobjectID, V_PointID, V_AlmDescTX, V_OwnerPostID;
			if (V_HistoryAlmCursor%NOTFOUND) then
				exit;
			end if;
			
			XTGLPackage.Pr_GetLastBigObjectID( V_HistoryAlm, 'Alm_ID', 1, V_NewAlmID );

			V_Sql := 'select count(*) from ' || V_MobjectWarningRemote || 
				' where MobjectWarning_ID = ' || DBDiffPackage.IntToString(V_AlmID);
			execute immediate V_Sql into V_Count;

			if (V_Count = 1) then
				V_Sql := 'select Spec_ID, CheckUser_ID from ' || V_MobjectWarningRemote || 
					' where MobjectWarning_ID = ' || DBDiffPackage.IntToString(V_AlmID);
				execute immediate V_Sql into V_MobSpecID, V_UserID;

				ZXSamplePackage.InsertAlmRecord(V_PartitionID, V_NewAlmID, V_FeatureValueID, V_PointID, V_AlmDT, V_AlmLevelID, V_AlmDescTX, V_MobjectID, V_OwnerPostID, V_MobSpecID, V_UserID); 
			end if;
			
			V_Sql := 'delete from ' || V_MobjectWarningRemote || 
				' where MobjectWarning_ID = ' || DBDiffPackage.IntToString(V_AlmID);
			--DBDiffPackage.Pr_DebugPrint(V_Sql);
			execute immediate V_Sql;
			
			V_Sql := 'delete from ' || V_HistoryAlmRemote || 
				' where Alm_ID = ' || DBDiffPackage.IntToString(V_AlmID);
			--DBDiffPackage.Pr_DebugPrint(V_Sql);
			execute immediate V_Sql;
			
			commit;
			
		end loop;
		
		close V_HistoryAlmCursor;
		
    exception
	when others then
	
      P_Emsg := SQLERRM;
      rollback;
	  return;
	  
    end;
	
	commit;
	
end;

procedure Pr_TransferPointData( 
			P_Emsg Out BS_MetaFieldType.Emsg_TX%type)
as
	V_RemoteDBLinkName BS_MetaFieldType.Fifty_TX%type;
	
	V_PntChannel BS_MetaFieldType.Fifty_TX%type;
	V_PntChannelTemp BS_MetaFieldType.Fifty_TX%type;
	V_TransferTable BS_MetaFieldType.Fifty_TX%type;
	V_TableName BS_MetaFieldType.Fifty_TX%type;
	
	V_PntChannelTempLocal BS_MetaFieldType.Fifty_TX%type;
	V_TransferTableLocal BS_MetaFieldType.Fifty_TX%type;
	V_TableNameLocal BS_MetaFieldType.Fifty_TX%type;
	
	V_PntChannelRemote BS_MetaFieldType.Fifty_TX%type;
	V_TableNameRemote BS_MetaFieldType.Fifty_TX%type;
	
	V_Sql BS_MetaFieldType.FiveKilo_TX%type;
	
	V_TransferTableCursor T_Cursor;
begin

	V_RemoteDBLinkName := 'to_remote';
	
	V_PntChannel := 'Sample_PntChannel';
	V_PntChannelTemp := 'Sample_PntChannelTemp';
	V_TransferTable := 'Transfer_TableName';
	
	V_PntChannelTempLocal := V_PntChannelTemp;
	V_TransferTableLocal := V_TransferTable;
	
	V_PntChannelRemote := V_PntChannel || '@' || V_RemoteDBLinkName;
	
	V_Sql := 'create global temporary table ' || V_PntChannelTempLocal || 
		' on commit preserve rows as select * from ' || V_PntChannelRemote;
	execute immediate V_Sql;
	
    begin
	
		V_Sql := 'delete from ' || V_PntChannelRemote;
		execute immediate V_Sql;
		
		V_Sql := 'select TableName_TX from ' || V_TransferTableLocal || ' order by Table_ID desc';
		open V_TransferTableCursor for V_Sql;
		
		while (1=1) loop
			
			fetch V_TransferTableCursor into V_TableName;
			if (V_TransferTableCursor%NOTFOUND) then
				exit;
			end if;
			
			V_TableNameRemote := V_TableName || '@' || V_RemoteDBLinkName;
			
			V_Sql := 'delete from ' || V_TableNameRemote;
			execute immediate V_Sql;
		
		end loop;
		
		close V_TransferTableCursor;
		
	
		V_Sql := 'select TableName_TX from ' || V_TransferTableLocal || ' order by Table_ID';
		open V_TransferTableCursor for V_Sql;
		
		while (1=1) loop
			
			fetch V_TransferTableCursor into V_TableName;
			if (V_TransferTableCursor%NOTFOUND) then
				exit;
			end if;
			
			V_TableNameLocal := V_TableName;
			V_TableNameRemote := V_TableName || '@' || V_RemoteDBLinkName;
			
			V_Sql := 'insert into ' || V_TableNameRemote || ' select * from ' ||  V_TableNameLocal;
			execute immediate V_Sql;
		
		end loop;
		
		close V_TransferTableCursor;
		
		V_Sql := 'insert into ' || V_PntChannelRemote || ' select * from ' || V_PntChannelTempLocal;
		execute immediate V_Sql;
		
		commit;
		
    exception
	when others then
	
      P_Emsg := SQLERRM;
      rollback;
	  
    end;
	
	V_Sql := 'truncate table ' || V_PntChannelTempLocal;
	execute immediate V_Sql;
	
	commit;
	
	V_Sql := 'drop table ' || V_PntChannelTempLocal;
	execute immediate V_Sql;
	
end;

procedure Pr_TransferDataMain
as
	V_AppLogID BS_MetaFieldType.Int_NR%type;
	V_TaskTM BS_MetaFieldType.DateTime_DT%type;
	V_Emsg BS_MetaFieldType.FiveHundred_TX%type;
begin

	V_TaskTM := DBDiffPackage.GetSystemTime();
	XTGLPackage.Pr_GetLastObjectID('BS_AppLog', 'AppLog_ID', 1, V_AppLogID);
	insert into BS_AppLog(AppLog_ID,AppUserName_TX,Action_TM,IPAddress_TX,AppAction_CD,Log_TX)
		values (V_AppLogID, '数据库所有者', V_TaskTM, '数据库服务器', 'Pr_TransferHistoryData', 'Pr_TransferHistoryData Start');
	commit;
	
	HNZXTransferPackage.Pr_TransferHistoryData(V_Emsg);
	if (V_Emsg is not null) and (DBDiffPackage.GetLength(V_Emsg) > 0 ) then
		DBDiffPackage.Pr_DebugPrint(V_Emsg);
		V_TaskTM := DBDiffPackage.GetSystemTime();
		XTGLPackage.Pr_GetLastObjectID('BS_AppLog', 'AppLog_ID', 1, V_AppLogID);
		insert into BS_AppLog(AppLog_ID,AppUserName_TX,Action_TM,IPAddress_TX,AppAction_CD,Log_TX)
			values (V_AppLogID, '数据库所有者', V_TaskTM, '数据库服务器', 'Pr_TransferHistoryData', V_Emsg);
		commit;
	end if;
	
	V_TaskTM := DBDiffPackage.GetSystemTime();
	XTGLPackage.Pr_GetLastObjectID('BS_AppLog', 'AppLog_ID', 1, V_AppLogID);
	insert into BS_AppLog(AppLog_ID,AppUserName_TX,Action_TM,IPAddress_TX,AppAction_CD,Log_TX)
		values (V_AppLogID, '数据库所有者', V_TaskTM, '数据库服务器', 'Pr_TransferHistoryData', 'Pr_TransferHistoryData End');
	commit;
	
	V_TaskTM := DBDiffPackage.GetSystemTime();
	XTGLPackage.Pr_GetLastObjectID('BS_AppLog', 'AppLog_ID', 1, V_AppLogID);
	insert into BS_AppLog(AppLog_ID,AppUserName_TX,Action_TM,IPAddress_TX,AppAction_CD,Log_TX)
		values (V_AppLogID, '数据库所有者', V_TaskTM, '数据库服务器', 'Pr_TransferAlarmData', 'Pr_TransferAlarmData Start');
	commit;
	
	HNZXTransferPackage.Pr_TransferAlarmData(V_Emsg);
	if (V_Emsg is not null) and (DBDiffPackage.GetLength(V_Emsg) > 0 ) then
		DBDiffPackage.Pr_DebugPrint(V_Emsg);
		V_TaskTM := DBDiffPackage.GetSystemTime();
		XTGLPackage.Pr_GetLastObjectID('BS_AppLog', 'AppLog_ID', 1, V_AppLogID);
		insert into BS_AppLog(AppLog_ID,AppUserName_TX,Action_TM,IPAddress_TX,AppAction_CD,Log_TX)
			values (V_AppLogID, '数据库所有者', V_TaskTM, '数据库服务器', 'Pr_TransferAlarmData', V_Emsg);
		commit;
	end if;
	
	V_TaskTM := DBDiffPackage.GetSystemTime();
	XTGLPackage.Pr_GetLastObjectID('BS_AppLog', 'AppLog_ID', 1, V_AppLogID);
	insert into BS_AppLog(AppLog_ID,AppUserName_TX,Action_TM,IPAddress_TX,AppAction_CD,Log_TX)
		values (V_AppLogID, '数据库所有者', V_TaskTM, '数据库服务器', 'Pr_TransferAlarmData', 'Pr_TransferAlarmData End');
	commit;
	
end;

procedure Pr_TransferPointMain
as
	V_AppLogID BS_MetaFieldType.Int_NR%type;
	V_TaskTM BS_MetaFieldType.DateTime_DT%type;
	V_Emsg BS_MetaFieldType.FiveHundred_TX%type;
begin

	V_TaskTM := DBDiffPackage.GetSystemTime();
	XTGLPackage.Pr_GetLastObjectID('BS_AppLog', 'AppLog_ID', 1, V_AppLogID);
	insert into BS_AppLog(AppLog_ID,AppUserName_TX,Action_TM,IPAddress_TX,AppAction_CD,Log_TX)
		values (V_AppLogID, '数据库所有者', V_TaskTM, '数据库服务器', 'Pr_TransferPointData', 'Pr_TransferPointData Start');
	commit;
	
	HNZXTransferPackage.Pr_TransferPointData(V_Emsg);
	if (V_Emsg is not null) and (DBDiffPackage.GetLength(V_Emsg) > 0 ) then
		DBDiffPackage.Pr_DebugPrint(V_Emsg);
		V_TaskTM := DBDiffPackage.GetSystemTime();
		XTGLPackage.Pr_GetLastObjectID('BS_AppLog', 'AppLog_ID', 1, V_AppLogID);
		insert into BS_AppLog(AppLog_ID,AppUserName_TX,Action_TM,IPAddress_TX,AppAction_CD,Log_TX)
			values (V_AppLogID, '数据库所有者', V_TaskTM, '数据库服务器', 'Pr_TransferPointData', V_Emsg);
		commit;
	end If;
	
	V_TaskTM := DBDiffPackage.GetSystemTime();
	XTGLPackage.Pr_GetLastObjectID('BS_AppLog', 'AppLog_ID', 1, V_AppLogID);
	insert into BS_AppLog(AppLog_ID,AppUserName_TX,Action_TM,IPAddress_TX,AppAction_CD,Log_TX)
		values (V_AppLogID, '数据库所有者', V_TaskTM, '数据库服务器', 'Pr_TransferPointData', 'Pr_TransferPointData End');
	commit;
	
end;

End HNZXTransferPackage;
/


declare
	V_ExecDate date;
	V_JobName BS_MetaFieldType.Fifty_TX%type;
	V_JobNum BS_MetaFieldType.Int_NR%type;
begin

	begin
	
		V_JobName := 'HNZXTransferPackage.Pr_TransferPointMain;';
		Select Job Into V_JobNum from User_Jobs Where What = V_JobName and rownum = 1;
		
    exception
    when NO_DATA_FOUND then
	
        V_JobNum := 0;
		
    end;
	
    if V_JobNum > 0 then
		DBMS_JOB.Remove(V_JobNum);
    end if;
	
	V_JobNum := 10;
	V_ExecDate := DBDiffPackage.StringToTime( DBDiffPackage.ToDateString(DBDiffPackage.GetSystemTime(),'YYYY-MM-DD') || ' ' || DBDiffPackage.ToDateTimeString(to_date('01:00:00','hh24:mi:ss'),'HH24:MI:ss'));
    DBMS_JOB.Submit(V_JobNum, V_JobName, V_ExecDate, 'DBDiffPackage.StringToTime( DBDiffPackage.ToDateString(DBDiffPackage.GetSystemTime(),''YYYY-MM-DD'') || '' '' || ''' || DBDiffPackage.ToDateTimeString(to_date('01:00:00','hh24:mi:ss'),'HH24:MI:ss') || ''') + 1');
    commit;
	
	begin
	
		V_JobName := 'HNZXTransferPackage.Pr_TransferDataMain;';
		Select Job Into V_JobNum from User_Jobs Where What = V_JobName and rownum = 1;
		
    exception
    when NO_DATA_FOUND then
	
        V_JobNum := 0;
		
    end;
	
    if V_JobNum > 0 then
		DBMS_JOB.Remove(V_JobNum);
    end if;
	
	V_JobNum := 11;
	DBMS_JOB.Submit(V_JobNum, V_JobName, sysdate, 'sysdate+5/24/60');
	commit;
	
end; 
/
