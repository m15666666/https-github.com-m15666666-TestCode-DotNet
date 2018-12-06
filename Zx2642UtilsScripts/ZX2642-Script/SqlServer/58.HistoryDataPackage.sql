IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[F_GetPartitionIDByHistoryID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [HistoryDataPackage].[F_GetPartitionIDByHistoryID]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetSummaryByKey]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetSummaryByKey]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetSummaryBySyncNR]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetSummaryBySyncNR]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_DeleteSummaryByKey]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_DeleteSummaryByKey]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetSummaryListByPoint]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetSummaryListByPoint]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetSummaryByPointAndSpeed]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetSummaryByPointAndSpeed]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_CheckPointHistories]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_CheckPointHistories]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetWaveformByKey]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetWaveformByKey]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetFreqWaveformByKey]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetFreqWaveformByKey]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetFeatureValueByKey]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetFeatureValueByKey]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetFeaturesByChnNrAndFeat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetFeaturesByChnNrAndFeat]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetFeaturesByChannelNr]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetFeaturesByChannelNr]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetAllFeatTrendsByChnNSpeed]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetAllFeatTrendsByChnNSpeed]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetAllFeatTrendsByChnNr]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetAllFeatTrendsByChnNr]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetTrendDatasByChnNSpeed]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetTrendDatasByChnNSpeed]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetTrendDatasByChnNr]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetTrendDatasByChnNr]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetMObjectWorkingDatas]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetMObjectWorkingDatas]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetMObjectWorkingDataPoints]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetMObjectWorkingDataPoints]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetHistoryAlmCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetHistoryAlmCount]
GO


IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'HistoryDataPackage')
DROP SCHEMA [HistoryDataPackage]
GO


/****** ����:  Schema [CommonPackage]    �ű�����: 04/01/2011 10:25:27 ******/
CREATE SCHEMA [HistoryDataPackage] AUTHORIZATION [dbo]
GO

--��ȡ��ʷ���ݱ�Ŷ�Ӧ�ķ��������
create function HistoryDataPackage.F_GetPartitionIDByHistoryID(
       @P_HistoryID bigint)RETURNS bigint
as begin
declare @V_PartitionID bigint;
       select @V_PartitionID = Partition_ID  From ZX_History_DataMapping Where History_ID = @P_HistoryID;
       return @V_PartitionID;
end;
GO


--��ȡ��ʷժҪ����
--@P_HistoryID�� ��ʷ���ݱ��
--@P_Cursor���������������ʷժҪ����
create procedure HistoryDataPackage.Pr_GetSummaryByKey
          ( @P_HistoryID    bigint)
as begin
    declare @V_PartitionID bigint;
    declare @V_QueryObjSql varchar(5000);
    declare @V_TableBaseName varchar(50);
    declare @V_Sql varchar(5000);
    set @V_PartitionID =  HistoryDataPackage.F_GetPartitionIDByHistoryID(@P_HistoryID);
    set @V_TableBaseName =  'Zx_History_Summary';
    set @V_QueryObjSql =  Commonpackage.F_GetPartitionSQL(@V_PartitionID, @V_PartitionID, @V_TableBaseName);
    set @V_Sql =  
      ' Select * From ' + @V_QueryObjSql +
      ' Where History_ID = ' + DBDiffPackage.IntToString(@P_HistoryID);
    exec (@V_Sql);    
end;
GO

--��ȡ��ͬͬ���ŵ���ʷժҪ����
--P_SyncNR: 		ͬ����
--@P_HistoryID�� ��ʷ���ݱ��
--@P_Cursor���������������ʷժҪ����
create procedure HistoryDataPackage.Pr_GetSummaryBySyncNR
          ( @P_SyncNR		bigint,
			@P_HistoryID    bigint)
as begin
    declare @V_PartitionID bigint;
    declare @V_QueryObjSql varchar(5000);
    declare @V_TableBaseName varchar(50);
    declare @V_Sql varchar(5000);
    set @V_PartitionID =  HistoryDataPackage.F_GetPartitionIDByHistoryID(@P_HistoryID);
    set @V_TableBaseName =  'Zx_History_Summary';
    set @V_QueryObjSql =  Commonpackage.F_GetPartitionSQL(@V_PartitionID, @V_PartitionID, @V_TableBaseName);
    set @V_Sql =  
      ' Select * From ' + @V_QueryObjSql +
      ' Where Synch_NR = ' + DBDiffPackage.IntToString(@P_SyncNR);
    exec (@V_Sql);    
end;
GO

--ɾ����ʷժҪ����
--@P_HistoryID�� ��ʷ���ݱ��
--@P_Emsg: OUTPUT ������Ϣ
create procedure HistoryDataPackage.Pr_DeleteSummaryByKey( 
            @P_HistoryID    bigint,
            @P_Emsg varchar(300) output)
as begin
    declare @V_PartitionID bigint;
    declare @V_TableName varchar(50);
    declare @V_TableBaseName varchar(50);
    declare @V_Sql varchar(5000);
    set @V_PartitionID =  HistoryDataPackage.F_GetPartitionIDByHistoryID(@P_HistoryID);
    set @V_TableBaseName =  'Zx_History_Summary';
    set @V_TableName =  @V_TableBaseName;
    set @V_Sql =  
      ' Delete From ' + @V_TableName +
      ' Where History_ID = ' + DBDiffPackage.IntToString(@P_HistoryID); 
    begin try
        begin tran         
		exec (@V_SQL);
		set @V_TableName =  Commonpackage.F_GetPartitionTable(@V_TableBaseName, @V_PartitionID);
		if DBDiffPackage.GetLength(@V_TableName) > 0 begin   
			set @V_Sql =  
			' Delete From ' + @V_TableName +
			' Where History_ID = ' + DBDiffPackage.IntToString(@P_HistoryID);    
			exec (@V_SQL);
		end;
		commit tran; 
	end try
	begin catch   
      Rollback tran;
      return;
	end catch;    
end;
GO

-- ��ȡ����µ���ʷժҪ����            
-- @P_PointID �����
--@P_MinPartitionID��������� ��С�������
--@P_MaxPartitionID��������� ���������
-- @P_Cursor ���������������ʷժҪ�����б�
create procedure HistoryDataPackage.Pr_GetSummaryListByPoint(
            @P_PointID    int,      
            @P_MinPartitionID    bigint,
            @P_MaxPartitionID    bigint)
as begin
    declare @V_TableBaseName varchar(50);
    declare @V_QueryObjSql varchar(5000);
    declare @V_Sql varchar(5000);
    set @V_TableBaseName =  'Zx_History_Summary';
    set @V_QueryObjSql =  Commonpackage.F_GetPartitionSQL(@P_MinPartitionID, @P_MaxPartitionID, @V_TableBaseName);
    set @V_Sql =  
      ' Select * From ' + @V_QueryObjSql +
      ' Where Point_ID = ' + DBDiffPackage.IntToString(@P_PointID) + 
      ' And Partition_ID <= ' + DBDiffPackage.IntToString(@P_MaxPartitionID) + ' And Partition_ID >= ' + DBDiffPackage.IntToString(@P_MinPartitionID);
    exec (@V_Sql);    
end;
GO
            
-- ��ȡ�����ָ��ת�ٷ�Χ����ʷժҪ����            
-- @P_PointID �����
--@P_MinPartitionID��������� ��С�������
--@P_MaxPartitionID��������� ���������
-- @P_LowerSpeed ת������
-- @P_UpperSpeed ת������
-- @P_Cursor ���������������ʷժҪ�����б�
create procedure HistoryDataPackage.Pr_GetSummaryByPointAndSpeed(
            @P_PointID    int,      
            @P_MinPartitionID    bigint,
            @P_MaxPartitionID    bigint,
            @P_LowerSpeed    int,
            @P_UpperSpeed    int)
as begin
    declare @V_TableBaseName varchar(50);
    declare @V_QueryObjSql varchar(5000);
    declare @V_Sql varchar(5000);
    set @V_TableBaseName =  'Zx_History_Summary';
    set @V_QueryObjSql =  Commonpackage.F_GetPartitionSQL(@P_MinPartitionID, @P_MaxPartitionID, @V_TableBaseName);
    set @V_Sql =  
      ' Select * From ' + @V_QueryObjSql +
      ' Where Point_ID = ' + DBDiffPackage.IntToString(@P_PointID) + ' And RotSpeed_NR >= ' + DBDiffPackage.IntToString(@P_LowerSpeed) + 'and RotSpeed_NR <=' + DBDiffPackage.IntToString(@P_UpperSpeed) + 
      ' And Partition_ID <= ' + DBDiffPackage.IntToString(@P_MaxPartitionID) + ' And Partition_ID >= ' + DBDiffPackage.IntToString(@P_MinPartitionID);
    exec (@V_Sql);    
end;
GO

-- ���ָ������Ƿ������ʷժҪ����        
-- @P_PointID �����
--@P_MinPartitionID��������� ��С�������
--@P_MaxPartitionID��������� ���������
-- @P_HasResult ���������1-����ժҪ���ݣ�0-������
create procedure HistoryDataPackage.Pr_CheckPointHistories(
            @P_PointID    int,
            @P_MinPartitionID    bigint,
            @P_MaxPartitionID    bigint,      
            @P_HasResult int output)
as begin
     declare @V_TableBaseName varchar(50);
     declare @V_QueryObjSql varchar(5000);
     declare @V_Sql varchar(5000);
     set @V_TableBaseName =  'Zx_History_Summary';
     set @V_QueryObjSql =  Commonpackage.F_GetPartitionSQL(@P_MinPartitionID, @P_MaxPartitionID, @V_TableBaseName);
     set @V_Sql =  
      ' Select * From ' + @V_QueryObjSql +
      ' Where Point_ID = ' + DBDiffPackage.IntToString(@P_PointID) + 
      ' And Partition_ID <= ' + DBDiffPackage.IntToString(@P_MaxPartitionID) + ' And Partition_ID >= ' + DBDiffPackage.IntToString(@P_MinPartitionID);
     set @V_SQL =  'Select Count(*) From (' + @V_Sql + ') V';
exec DBDiffPackage.Pr_DebugPrint @V_Sql;
     exec ('declare _ScalarCuror cursor for ' + @V_SQL);open _ScalarCuror; fetch next from _ScalarCuror into @P_HasResult; close _ScalarCuror; deallocate _ScalarCuror;
end;
GO


--��ȡ��ʷ��������
--@P_HistoryID�� ��ʷ���ݱ��
--@P_Cursor���������������ʷժҪ����
create procedure HistoryDataPackage.Pr_GetWaveformByKey
          ( @P_HistoryID    bigint)
as begin
    declare @V_PartitionID bigint;
    declare @V_QueryObjSql varchar(5000);
    declare @V_TableBaseName varchar(50);
    declare @V_Sql varchar(5000);
    set @V_PartitionID =  HistoryDataPackage.F_GetPartitionIDByHistoryID(@P_HistoryID);
    set @V_TableBaseName =  'Zx_History_WaveForm';
    set @V_QueryObjSql =  Commonpackage.F_GetPartitionSQL(@V_PartitionID, @V_PartitionID, @V_TableBaseName);
    set @V_Sql =  
      ' Select * From ' + @V_QueryObjSql +
      ' Where History_ID = ' + DBDiffPackage.IntToString(@P_HistoryID);
    exec (@V_Sql);    
end;
GO

--��ȡ��ʷƵ�ײ�������
--@P_HistoryID�� ��ʷ���ݱ��
--@P_Cursor���������������ʷժҪ����
create procedure HistoryDataPackage.Pr_GetFreqWaveformByKey
          ( @P_HistoryID    bigint)
as begin
    declare @V_PartitionID bigint;
    declare @V_QueryObjSql varchar(5000);
    declare @V_TableBaseName varchar(50);
    declare @V_Sql varchar(5000);
    set @V_PartitionID =  HistoryDataPackage.F_GetPartitionIDByHistoryID(@P_HistoryID);
    set @V_TableBaseName =  'Zx_History_Freq';
    set @V_QueryObjSql =  Commonpackage.F_GetPartitionSQL(@V_PartitionID, @V_PartitionID, @V_TableBaseName);
    set @V_Sql =  
      ' Select * From ' + @V_QueryObjSql +
      ' Where History_ID = ' + DBDiffPackage.IntToString(@P_HistoryID);
    exec (@V_Sql);    
end;
GO

--��ȡ��ʷ��������
--@P_HistoryID�� ��ʷ���ݱ��
--@P_ChannelNumber: ͨ����
-- @P_FeatureValueID ����ָ����
--@P_Cursor���������������ʷ��������
create procedure HistoryDataPackage.Pr_GetFeatureValueByKey
          ( @P_HistoryID    bigint,
            @P_ChannelNumber    tinyint,
            @P_FeatureValueID    int)
as begin
    declare @V_PartitionID bigint;
    declare @V_QueryObjSql varchar(5000);
    declare @V_TableBaseName varchar(50);
    declare @V_Sql varchar(5000);
    set @V_PartitionID =  HistoryDataPackage.F_GetPartitionIDByHistoryID(@P_HistoryID);
    set @V_TableBaseName =  'Zx_History_FeatureValue';
    set @V_QueryObjSql =  Commonpackage.F_GetPartitionSQL(@V_PartitionID, @V_PartitionID, @V_TableBaseName);
    set @V_Sql =  
      ' Select * From ' + @V_QueryObjSql +
      ' Where History_ID = ' + DBDiffPackage.IntToString(@P_HistoryID) + 'And ChnNo_NR = ' + DBDiffPackage.IntToString(@P_ChannelNumber) + ' And FeatureValue_ID = '
      + DBDiffPackage.IntToString(@P_FeatureValueID);
    exec (@V_Sql);    
end;
GO
             
-- ��ȡָ��ָ�����ʷ��������    
--@P_MinPartitionID��������� ��С�������
--@P_MaxPartitionID��������� ���������
-- @P_ChannelNumber ͨ����
-- @P_FeatureValueID ����ָ����
-- @P_Cursor �������������ָ��ָ�����ʷ��������  
create procedure HistoryDataPackage.Pr_GetFeaturesByChnNrAndFeat(  
            @P_MinPartitionID    bigint,
            @P_MaxPartitionID    bigint,
            @P_ChannelNumber    tinyint,
            @P_FeatureValueID    int)
as begin
    declare @V_TableBaseName varchar(50);
    declare @V_QueryObjSql varchar(5000);
    declare @V_Sql varchar(5000);
    set @V_TableBaseName =  'Zx_History_FeatureValue';
    set @V_QueryObjSql =  Commonpackage.F_GetPartitionSQL(@P_MinPartitionID, @P_MaxPartitionID, @V_TableBaseName);
    set @V_Sql =  
      ' Select * From ' + @V_QueryObjSql +
      ' Where chnno_nr = ' + DBDiffPackage.IntToString(@P_ChannelNumber) + ' And featurevalue_id = ' + DBDiffPackage.IntToString(@P_FeatureValueID) + 
      ' And Partition_ID <= ' + DBDiffPackage.IntToString(@P_MaxPartitionID) + ' And Partition_ID >= ' + DBDiffPackage.IntToString(@P_MinPartitionID);
    exec (@V_Sql);    
end;
GO
            
-- ��ȡ��ʷ��������   
--@P_MinPartitionID��������� ��С�������
--@P_MaxPartitionID��������� ���������
-- @P_ChannelNumber ͨ����
-- @P_Cursor ���������������ʷ��������      
create procedure HistoryDataPackage.Pr_GetFeaturesByChannelNr(   
            @P_MinPartitionID    bigint,
            @P_MaxPartitionID    bigint,
            @P_ChannelNumber    tinyint)
as begin
    declare @V_TableBaseName varchar(50);
    declare @V_QueryObjSql varchar(1000);
    declare @V_Sql varchar(5000);
    set @V_TableBaseName =  'Zx_History_FeatureValue';
    set @V_QueryObjSql =  Commonpackage.F_GetPartitionSQL(@P_MinPartitionID, @P_MaxPartitionID, @V_TableBaseName);
    set @V_Sql =  
      ' Select * From ' + @V_QueryObjSql +
      ' Where chnno_nr = ' + DBDiffPackage.IntToString(@P_ChannelNumber) + 
      ' And Partition_ID <= ' + DBDiffPackage.IntToString(@P_MaxPartitionID) + ' And Partition_ID >= ' + DBDiffPackage.IntToString(@P_MinPartitionID);
    exec (@V_Sql);    
end;
GO

-- ��ȡ������������ֵ����������   
-- @P_PointID �����
--@P_MinPartitionID��������� ��С�������
--@P_MaxPartitionID��������� ���������
-- @P_LowerSpeed ת������
-- @P_UpperSpeed ת������
-- @P_ChannelNumber ͨ����
-- @P_RecordCount    ��෵�ؼ�¼��
-- @P_Count ʵ�ʼ�¼����
-- @P_Cursor ���������������ʷ��������      
create procedure HistoryDataPackage.Pr_GetAllFeatTrendsByChnNSpeed( 
            @P_PointID    int,      
            @P_MinPartitionID    bigint,
            @P_MaxPartitionID    bigint,
            @P_LowerSpeed    int,
            @P_UpperSpeed    int,
            @P_ChannelNumber    tinyint,
            @P_RecordCount   int,
            @P_QueryCountFlag int,
            @P_Count int output)
as begin
    declare @V_TableFeatBaseName varchar(50);
    declare @V_TableSummBaseName varchar(50);
    declare @V_QueryFeatureSql varchar(1000);
    declare @V_QuerySummarySql varchar(5000);
    declare @V_Sql varchar(5000);
    declare @V_SqlQueryCount varchar(5000);    
    set @V_TableFeatBaseName =  'Zx_History_FeatureValue';
    set @V_TableSummBaseName =  'Zx_History_Summary';
    set @V_QueryFeatureSql =  Commonpackage.F_GetPartitionSQL(@P_MinPartitionID, @P_MaxPartitionID, @V_TableFeatBaseName);
    set @V_QuerySummarySql =  Commonpackage.F_GetPartitionSQL(@P_MinPartitionID, @P_MaxPartitionID, @V_TableSummBaseName);
    set @V_SqlQueryCount =  ' Select Count(history_id) From  ' + @V_QuerySummarySql +
      ' Where Point_ID = ' + DBDiffPackage.IntToString(@P_PointID) + ' And RotSpeed_NR >= ' + DBDiffPackage.IntToString(@P_LowerSpeed) + ' And RotSpeed_NR <=' + DBDiffPackage.IntToString(@P_UpperSpeed) + 
      ' And Partition_ID <= ' + DBDiffPackage.IntToString(@P_MaxPartitionID) + ' And Partition_ID >= ' + DBDiffPackage.IntToString(@P_MinPartitionID);
        -- ���ؼ�¼����
	if @P_QueryCountFlag = -1 begin
    exec ('declare _ScalarCuror cursor for ' + @V_SqlQueryCount);open _ScalarCuror; fetch next from _ScalarCuror into @P_Count; close _ScalarCuror; deallocate _ScalarCuror;
    return;
    end;
     set @V_Sql =   'Select 
                  Partition_ID,
                  Compress_ID,
                  SampTime_DT,
                  DatLen_NR,
                  History_ID,
                  Point_ID,
                  Alm_ID,
                  AlmLevel_ID,
                  RotSpeed_NR
                  From  ( Select * From ' + @V_QuerySummarySql + ' ) ' +
      ' Where Point_ID = ' + DBDiffPackage.IntToString(@P_PointID) + ' And RotSpeed_NR >= ' + DBDiffPackage.IntToString(@P_LowerSpeed) + ' And RotSpeed_NR <=' + DBDiffPackage.IntToString(@P_UpperSpeed) + 
      ' And Partition_ID <= ' + DBDiffPackage.IntToString(@P_MaxPartitionID) + ' And Partition_ID >= ' + DBDiffPackage.IntToString(@P_MinPartitionID) ;
    set @V_Sql =  DBDiffPackage.GetPagingSql(@V_Sql, 0, @P_RecordCount, null);
    set @V_Sql =  
      ' Select 
         PartitionID,
         CompressID,
         SampleTime,
         DataLength,
         DataId,
         MeasurementValue,
         FeatureValueID,
         Point_ID,
         Alm_ID,
         AlmLevel_ID,
         Rev         
         From ( ' +
      ' Select 
             s.Partition_ID as PartitionID,
             s.Compress_ID as CompressID,
             s.SampTime_DT as SampleTime,
             s.DatLen_NR as DataLength,
             s.History_ID as DataId,
             f.FeatureValue_NR as MeasurementValue,
             f.FeatureValue_ID as FeatureValueID,
             s.Point_ID,
             s.Alm_ID,
             s.AlmLevel_ID,
             s.RotSpeed_NR as Rev 
             From ( ' + @V_Sql + ' ) s ' + ' join ( ' + 
      ' Select 
            FeatureValue_NR,
            FeatureValue_ID,
            History_ID          
       From ' 
      + @V_QueryFeatureSql +
      ' Where chnno_nr = ' + DBDiffPackage.IntToString(@P_ChannelNumber) + 
      ' And Partition_ID <= ' + DBDiffPackage.IntToString(@P_MaxPartitionID) + ' And Partition_ID >= ' + DBDiffPackage.IntToString(@P_MinPartitionID) + ' ) f 
       on f.History_id = s.History_id ) V';
    exec (@V_Sql);    
end;
GO

-- ��ȡ������������ֵ����������   
-- @P_PointID �����
--@P_MinPartitionID��������� ��С�������
--@P_MaxPartitionID��������� ���������
-- @P_ChannelNumber ͨ����
-- @P_RecordCount    ��෵�ؼ�¼��
-- @P_Count ʵ�ʼ�¼����
-- @P_Cursor ���������������ʷ��������      
create procedure HistoryDataPackage.Pr_GetAllFeatTrendsByChnNr( 
            @P_PointID    int,      
            @P_MinPartitionID    bigint,
            @P_MaxPartitionID    bigint,
            @P_ChannelNumber    tinyint,
            @P_RecordCount    int,
            @P_QueryCountFlag int,
            @P_Count int output)
as begin
    declare @V_TableFeatBaseName varchar(50);
    declare @V_TableSummBaseName varchar(50);
    declare @V_QueryFeatureSql varchar(1000);
    declare @V_QuerySummarySql varchar(5000);
    declare @V_Sql varchar(5000);
    declare @V_SqlQueryCount varchar(5000);
    set @V_TableFeatBaseName =  'Zx_History_FeatureValue';
    set @V_TableSummBaseName =  'Zx_History_Summary';
    set @V_QueryFeatureSql =  Commonpackage.F_GetPartitionSQL(@P_MinPartitionID, @P_MaxPartitionID, @V_TableFeatBaseName);
    set @V_QuerySummarySql =  Commonpackage.F_GetPartitionSQL(@P_MinPartitionID, @P_MaxPartitionID, @V_TableSummBaseName);
    set @V_SqlQueryCount =  ' Select Count(history_id) From  ' + @V_QuerySummarySql +
      ' Where Point_ID = ' + DBDiffPackage.IntToString(@P_PointID) +  
      ' And Partition_ID <= ' + DBDiffPackage.IntToString(@P_MaxPartitionID) + ' And Partition_ID >= ' + DBDiffPackage.IntToString(@P_MinPartitionID);
    -- ���ؼ�¼����
	if @P_QueryCountFlag = -1 begin
		 exec ('declare _ScalarCuror cursor for ' + @V_SqlQueryCount);open _ScalarCuror; fetch next from _ScalarCuror into @P_Count; close _ScalarCuror; deallocate _ScalarCuror;
		 return
	end;
   
    set @V_Sql =  'Select 
                  Partition_ID,
                  Compress_ID,
                  SampTime_DT,
                  DatLen_NR,
                  History_ID,
                  Point_ID,
                  Alm_ID,
                  AlmLevel_ID,
                  RotSpeed_NR,
				  Synch_NR
                  From   ' + @V_QuerySummarySql  +
      ' Where Point_ID = ' + DBDiffPackage.IntToString(@P_PointID) +  
      ' And Partition_ID <= ' + DBDiffPackage.IntToString(@P_MaxPartitionID) + ' And Partition_ID >= ' + DBDiffPackage.IntToString(@P_MinPartitionID) ;
	set @V_Sql =  DBDiffPackage.GetPagingSQL(@V_Sql, 0, @P_RecordCount,'Partition_ID Desc');
    set @V_Sql =  
      ' Select 
         PartitionID,
         CompressID,
         SampleTime,
         DataLength,
         DataId,
         MeasurementValue,
         FeatureValueID,
         Point_ID,
         Alm_ID,
         AlmLevel_ID,
		 SynchNR,
         Rev         
         From ( ' +
      ' Select 
             s.Partition_ID as PartitionID,
             s.Compress_ID as CompressID,
             s.SampTime_DT as SampleTime,
             s.DatLen_NR as DataLength,
             s.History_ID as DataId,
			 s.Synch_NR as SynchNR,
             f.FeatureValue_NR as MeasurementValue,
             f.FeatureValue_ID as FeatureValueID,
             s.Point_ID,
             s.Alm_ID,
             s.AlmLevel_ID,
             s.RotSpeed_NR as Rev 
             From ( '+ @V_Sql  +' ) s ' + ' join ( ' + 
      ' Select 
            FeatureValue_NR,
            FeatureValue_ID,
            History_ID          
       From ' 
      + @V_QueryFeatureSql +
      ' Where chnno_nr = ' + DBDiffPackage.IntToString(@P_ChannelNumber) + 
      ' And Partition_ID <= ' + DBDiffPackage.IntToString(@P_MaxPartitionID) + ' And Partition_ID >= ' + DBDiffPackage.IntToString(@P_MinPartitionID) + ' ) f 
       on f.History_id = s.History_id ) V';
    exec (@V_Sql);   
end;
GO

-- ��ȡ��������   
-- @P_PointID �����
--@P_MinPartitionID��������� ��С�������
--@P_MaxPartitionID��������� ���������
-- @P_LowerSpeed ת������
-- @P_UpperSpeed ת������
-- @P_ChannelNumber ͨ����
-- @P_FeatureValueID ����ָ����
-- @P_Cursor ���������������ʷ��������      
create procedure HistoryDataPackage.Pr_GetTrendDatasByChnNSpeed( 
            @P_PointID    int,      
            @P_MinPartitionID    bigint,
            @P_MaxPartitionID    bigint,
            @P_LowerSpeed    int,
            @P_UpperSpeed    int,
            @P_ChannelNumber    tinyint,
            @P_FeatureValueID    int)
as begin
    declare @V_TableFeatBaseName varchar(50);
    declare @V_TableSummBaseName varchar(50);
    declare @V_QueryFeatureSql varchar(1000);
    declare @V_QuerySummarySql varchar(5000);
    declare @V_Sql varchar(5000);
    set @V_TableFeatBaseName =  'Zx_History_FeatureValue';
    set @V_TableSummBaseName =  'Zx_History_Summary';
    set @V_QueryFeatureSql =  Commonpackage.F_GetPartitionSQL(@P_MinPartitionID, @P_MaxPartitionID, @V_TableFeatBaseName);
    set @V_QuerySummarySql =  Commonpackage.F_GetPartitionSQL(@P_MinPartitionID, @P_MaxPartitionID, @V_TableSummBaseName);
    set @V_Sql =  
      ' Select 
         PartitionID,
         CompressID,
         SampleTime,
         DataLength,
         DataId,
         MeasurementValue,
         Point_ID,
         Rev         
         From ( ' +
      ' Select 
             s.Partition_ID as PartitionID,
             s.Compress_ID as CompressID,
             s.SampTime_DT as SampleTime,
             s.DatLen_NR as DataLength,
             s.History_ID as DataId,
             f.FeatureValue_NR as MeasurementValue,
             f.FeatureValue_ID as FeatureValueID,
             s.Point_ID,
             s.RotSpeed_NR as Rev 
             From ( Select 
                  Partition_ID,
                  Compress_ID,
                  SampTime_DT,
                  DatLen_NR,
                  History_ID,
                  Point_ID,
                  RotSpeed_NR
                  From  ' + @V_QuerySummarySql +
      ' Where Point_ID = ' + DBDiffPackage.IntToString(@P_PointID) + ' And RotSpeed_NR >= ' + DBDiffPackage.IntToString(@P_LowerSpeed) + ' And RotSpeed_NR <=' + DBDiffPackage.IntToString(@P_UpperSpeed) + 
      ' And Partition_ID <= ' + DBDiffPackage.IntToString(@P_MaxPartitionID) + ' And Partition_ID >= ' + DBDiffPackage.IntToString(@P_MinPartitionID) + ' ) s ' + ' join ( ' + 
      ' Select 
            FeatureValue_NR,
            History_ID          
       From ' 
      + @V_QueryFeatureSql +
      ' Where chnno_nr = ' + DBDiffPackage.IntToString(@P_ChannelNumber) + ' And featurevalue_id = ' + DBDiffPackage.IntToString(@P_FeatureValueID) + 
      ' And Partition_ID <= ' + DBDiffPackage.IntToString(@P_MaxPartitionID) + ' And Partition_ID >= ' + DBDiffPackage.IntToString(@P_MinPartitionID) + ' ) f 
       on f.History_id = s.History_id ) V';
    exec (@V_Sql);    
end;
GO

-- ��ȡ��������   
-- @P_PointID �����
--@P_MinPartitionID��������� ��С�������
--@P_MaxPartitionID��������� ���������
-- @P_ChannelNumber ͨ����
-- @P_FeatureValueID ����ָ����
-- @P_Cursor ���������������ʷ��������      
create procedure HistoryDataPackage.Pr_GetTrendDatasByChnNr( 
            @P_PointID    int,      
            @P_MinPartitionID    bigint,
            @P_MaxPartitionID    bigint,
            @P_ChannelNumber    tinyint,
            @P_FeatureValueID    int)
as begin
    declare @V_TableFeatBaseName varchar(50);
    declare @V_TableSummBaseName varchar(50);
    declare @V_QueryFeatureSql varchar(1000);
    declare @V_QuerySummarySql varchar(5000);
    declare @V_Sql varchar(5000);
    set @V_TableFeatBaseName =  'Zx_History_FeatureValue';
    set @V_TableSummBaseName =  'Zx_History_Summary';
    set @V_QueryFeatureSql =  Commonpackage.F_GetPartitionSQL(@P_MinPartitionID, @P_MaxPartitionID, @V_TableFeatBaseName);
    set @V_QuerySummarySql =  Commonpackage.F_GetPartitionSQL(@P_MinPartitionID, @P_MaxPartitionID, @V_TableSummBaseName);
    set @V_Sql =  
      ' Select 
         PartitionID,
         CompressID,
         SampleTime,
         DataLength,
         DataId,
         MeasurementValue,
         Point_ID,
         Rev         
         From ( ' +
      ' Select 
             s.Partition_ID as PartitionID,
             s.Compress_ID as CompressID,
             s.SampTime_DT as SampleTime,
             s.DatLen_NR as DataLength,
             s.History_ID as DataId,
             f.FeatureValue_NR as MeasurementValue,
             s.Point_ID,
             s.RotSpeed_NR as Rev 
             From ( Select 
                  Partition_ID,
                  Compress_ID,
                  SampTime_DT,
                  DatLen_NR,
                  History_ID,
                  Point_ID,
                  RotSpeed_NR
                  From  ' + @V_QuerySummarySql +
      ' Where Point_ID = ' + DBDiffPackage.IntToString(@P_PointID) +  
      ' And Partition_ID <= ' + DBDiffPackage.IntToString(@P_MaxPartitionID) + ' And Partition_ID >= ' + DBDiffPackage.IntToString(@P_MinPartitionID) + ' ) s ' + ' join ( ' + 
      ' Select 
            FeatureValue_NR,
            History_ID          
       From ' 
      + @V_QueryFeatureSql +
      ' Where chnno_nr = ' + DBDiffPackage.IntToString(@P_ChannelNumber) + ' And featurevalue_id = ' + DBDiffPackage.IntToString(@P_FeatureValueID) + 
      ' And Partition_ID <= ' + DBDiffPackage.IntToString(@P_MaxPartitionID) + ' And Partition_ID >= ' + DBDiffPackage.IntToString(@P_MinPartitionID) + ' ) f 
       on f.History_id = s.History_id ) V';
    exec (@V_Sql);   
end;
GO

-- ��ȡ�豸�Ĺ�������   
-- @P_MobjectID �豸���
-- @P_MinPartitionID��������� ��С�������
-- @P_MaxPartitionID��������� ���������
-- @P_PageSize ��ҳ��С
-- @P_PageIndex ҳ��
-- @P_Count ʵ�ʼ�¼����
-- @P_Cursor ��������������豸�Ĺ�������    
create procedure HistoryDataPackage.Pr_GetMObjectWorkingDatas( 
			@P_MobjectID		int, 
            @P_MinPartitionID    bigint,
            @P_MaxPartitionID    bigint,
            @P_PageSize		int,
			@P_PageIndex int,
			@P_Count int output)
as begin
    declare @V_TableSummBaseName varchar(50);
	declare @V_TableFeatBaseName varchar(50);
	declare @V_QuerySummarySql varchar(5000);
    declare @V_QueryFeatureSql varchar(5000);
	declare @V_Summary varchar(50);
	declare @V_FeatureValue varchar(50);
    declare @V_Sql varchar(5000);
    declare @V_SqlQueryCount varchar(5000);
	declare @V_PageIndex int;
	declare @V_ParentList varchar(200);

	set @V_TableSummBaseName =  'Zx_History_Summary';
    set @V_TableFeatBaseName =  'Zx_History_FeatureValue';
	set @V_Summary =  @V_TableSummBaseName + 'His';
	set @V_FeatureValue =  @V_TableFeatBaseName + 'His';
	set @V_QuerySummarySql =  Commonpackage.F_GetPartitionSQL(@P_MinPartitionID, @P_MaxPartitionID, @V_TableSummBaseName);
    set @V_QueryFeatureSql =  Commonpackage.F_GetPartitionSQL(@P_MinPartitionID, @P_MaxPartitionID, @V_TableFeatBaseName);

	set @V_Sql =  
		' select p.Point_ID from pnt_point p ' + 
			' inner join ( select * from mob_mobject mob where mob.active_yn = ''1'' ) m ' + 
				' inner join mob_mobjectstructure ms on m.mobject_id = ms.mobject_id ' + 
			' on p.mobject_id = m.mobject_id ' + 
			' inner join ( select dv.*, pdv.point_id from pnt_datavar dv inner join pnt_pntdatavar pdv on dv.var_id = pdv.var_id ) v on p.point_id = v.point_id ' + 
			' where p.pointname_tx is not null';

	select @V_ParentList = parentlist_tx  from mob_mobjectstructure where mobject_id = @P_MobjectID;
    if @V_ParentList is not null and DBDiffPackage.GetLength(@V_ParentList) > 0 begin
		set @V_Sql =  @V_Sql + ' and ms.parentlist_tx like ''' + @V_ParentList + '%''';
	end else begin
		set @V_Sql =  @V_Sql + ' and m.mobject_id = ' + DBDiffPackage.IntToString(@P_MobjectID);
	end;

	set @V_Sql = 
		' select hs.*, hf.FeatureValue_NR from ' + 
			' ( select * from ' + @V_QuerySummarySql + 
				' where partition_id <= ' + DBDiffPackage.IntToString(@P_MaxPartitionID) + ' and ' + 
					' partition_id >= ' + DBDiffPackage.IntToString(@P_MinPartitionID) + ' and ' + 
					' point_id in ( ' + @V_Sql + ' ) ) hs ' + 
		' left join ' + 
			' ( select * from ' + @V_QueryFeatureSql + ' where featurevaluetype_id = 0 and chnno_nr = 1 and featurevalue_id = 10 ) hf ' + 
		' on hs.history_id = hf.history_id';

	set @V_Sql = 
		'select Partition_ID, History_ID, Point_ID, SampTime_DT, FeatureValue_NR, Result_TX, ' + 
			'DatType_NR, SigType_NR, EngUnit_ID, Compress_ID, DatLen_NR, Alm_ID, AlmLevel_ID, RotSpeed_NR from ( ' + @V_Sql + ' ) V';

	exec DBDiffPackage.Pr_DebugPrint @V_Sql;

	-- ���ؼ�¼����
	if @P_PageIndex = -1 begin
		set @V_SqlQueryCount = 'select count(*) from (' + @V_Sql + ') V';
		exec ('declare _ScalarCuror cursor for ' + @V_SqlQueryCount);
		open _ScalarCuror;
		fetch next from _ScalarCuror into @P_Count;
		close _ScalarCuror;
		deallocate _ScalarCuror;
		return;
	end;
	
	-- �������������շ����Ž�������
	if @P_PageSize = -1 begin
		-- ��ѯ��������
		set @V_Sql = @V_Sql + ' order by partition_id desc';
	end else begin
		-- ��ѯָ��ҳ����
		set @V_Sql = DBDiffPackage.GetPagingSQL(@V_Sql, @P_PageIndex, @P_PageSize, 'partition_id desc');
	end;
    exec (@V_Sql);
end;
GO

-- ��ȡ�豸�Ĺ������ݲ��   
-- @P_MobjectID �豸���
-- @P_PageSize ��ҳ��С
-- @P_PageIndex ҳ��
-- @P_Count ʵ�ʼ�¼����
-- @P_Cursor ��������������豸�Ĺ������ݲ��    
create procedure HistoryDataPackage.Pr_GetMObjectWorkingDataPoints( 
			@P_MobjectID		int, 
            @P_PageSize		int,
			@P_PageIndex int,
			@P_Count int output)
as begin
    declare @V_Sql varchar(5000);
    declare @V_SqlQueryCount varchar(5000);
	declare @V_PageIndex int;
	declare @V_ParentList varchar(200);

	set @V_Sql =  
		' select m.Mobject_ID, ms.ParentList_TX, p.Point_ID, p.PointName_TX, v.Var_ID, v.VarName_TX, p.DatType_ID as DatType_NR, p.SigType_ID as SigType_NR, p.EngUnit_ID as PointEngUnit_ID, eng.NameE_TX as EngUnitName_TX from Pnt_Point p ' +   
			' inner join ( select * from mob_mobject mob where mob.active_yn = ''1'' ) m  ' + 
				' inner join mob_mobjectstructure ms on m.mobject_id = ms.mobject_id ' + 
			' on p.mobject_id = m.mobject_id ' + 
			' inner join ( select dv.*, pdv.point_id from pnt_datavar dv inner join pnt_pntdatavar pdv on dv.var_id = pdv.var_id ) v on p.point_id = v.point_id ' + 
			' inner join z_engunit eng on p.engunit_id = eng.engunit_id ' + 
			' where p.pointname_tx is not null ';

	select @V_ParentList = parentlist_tx  from mob_mobjectstructure where mobject_id = @P_MobjectID;
    if @V_ParentList is not null and DBDiffPackage.GetLength(@V_ParentList) > 0 begin
		set @V_Sql =  @V_Sql + ' and ms.parentlist_tx like ''' + @V_ParentList + '%''';
	end else begin
		set @V_Sql =  @V_Sql + ' and m.mobject_id = ' + DBDiffPackage.IntToString(@P_MobjectID);
	end;

	exec DBDiffPackage.Pr_DebugPrint @V_Sql;

	-- ���ؼ�¼����
	if @P_PageIndex = -1 begin
		set @V_SqlQueryCount = 'select count(*) from (' + @V_Sql + ') V';
		exec ('declare _ScalarCuror cursor for ' + @V_SqlQueryCount);
		open _ScalarCuror;
		fetch next from _ScalarCuror into @P_Count;
		close _ScalarCuror;
		deallocate _ScalarCuror;
		return;
	end;
	
	-- ���������������豸·�������������������
	if @P_PageSize = -1 begin
		-- ��ѯ��������
		set @V_Sql = @V_Sql + ' order by ParentList_TX, PointName_TX';
	end else begin
		-- ��ѯָ��ҳ����
		set @V_Sql = DBDiffPackage.GetPagingSQL(@V_Sql, @P_PageIndex, @P_PageSize, 'ParentList_TX, PointName_TX');
	end;
    exec (@V_Sql);
end;
GO

-- ��ȡ�����ȼ���Ӧ������   
-- @P_AlmLevelID �����ȼ�
-- @P_Cursor ������������ر����ȼ���Ӧ������
create procedure HistoryDataPackage.Pr_GetHistoryAlmCount( 
			@P_AlmLevelID int, 
			@P_Count int output)
as begin

	select @P_Count = count(*) from ZX_History_Alm ha 
		inner join ZT_MobjectWarning mw on ha.Alm_ID = mw.MobjectWarning_ID 
		where ha.AlmLevel_ID = @P_AlmLevelID and mw.Close_YN <> '1';

end;
GO
