IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CompressDataPackage].[Pr_DeleteZXHistoryDatas]') AND type in (N'P', N'PC'))
DROP PROCEDURE [CompressDataPackage].[Pr_DeleteZXHistoryDatas]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CompressDataPackage].[Pr_UpdateCompressID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [CompressDataPackage].[Pr_UpdateCompressID]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CompressDataPackage].[Pr_GetZXHistoryDatas]') AND type in (N'P', N'PC'))
DROP PROCEDURE [CompressDataPackage].[Pr_GetZXHistoryDatas]
GO

IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'CompressDataPackage')
DROP SCHEMA [CompressDataPackage]
GO


/****** 对象:  Schema [CommonPackage]    脚本日期: 04/01/2011 10:25:27 ******/
CREATE SCHEMA [CompressDataPackage] AUTHORIZATION [dbo]
GO

--获取在线摘要数据列表
--Point_ID输入参数： 测点编号
--@P_MinPartitionID输入参数： 最小分区编号
--@P_MaxPartitionID输入参数： 最大分区编号
--@P_CompressTypeID输入参数： 压缩类型编号
--@P_CompressID输入参数： 压缩编号
--@P_Cursor输出参数：返回公司列表
create procedure CompressDataPackage.Pr_GetZXHistoryDatas
          ( @P_PointID    int,
            @P_MinPartitionID    bigint,
            @P_MaxPartitionID    bigint,
            @P_CompressTypeID    int,
            @P_CompressID    int)
as begin
     declare @V_count int;     
     declare @V_Sql varchar(1000);
     declare @V_TableName varchar(50);
     declare V_Cursor cursor local for Select rp.tablename_tx
     From BS_RangePartition rp Where 
     upper(rp.tablebasename_tx) = upper('zx_history_summary') 
     and ((rp.partitionmin_id <= @P_MinPartitionID and rp.partitionmax_id >= @P_MinPartitionID)
     or (rp.partitionmin_id <= @P_MaxPartitionID and rp.partitionmax_id >= @P_MaxPartitionID)
     or (rp.partitionmin_id >= @P_MinPartitionID and rp.partitionmax_id <= @P_MaxPartitionID));
     set @V_Sql =  'Select * From zx_history_summary';
     set @V_count =  0;
     select @V_count = Count(*)  
     From BS_RangePartition rp Where 
     upper(rp.tablebasename_tx) = upper('zx_history_summary') 
     and ((rp.partitionmin_id <= @P_MinPartitionID and rp.partitionmax_id >= @P_MinPartitionID)
     or (rp.partitionmin_id <= @P_MaxPartitionID and rp.partitionmax_id >= @P_MaxPartitionID)
     or (rp.partitionmin_id >= @P_MinPartitionID and rp.partitionmax_id <= @P_MaxPartitionID));

     If @V_count > 0 begin
		open V_Cursor;
		while (1=1)  begin
			fetch next from V_Cursor into @V_TableName;
			if (@@Fetch_Status <> 0) begin
break;
			end;
			set @V_Sql =  @V_Sql + ' union all Select * from ' + @V_TableName;
		end;
close V_Cursor;
deallocate V_Cursor;
    end;
    
    set @V_Sql =  'Select * from (  select * from ( ' + @V_Sql + ' ) t where t.point_id =' + DBDiffPackage.IntToString(@P_PointID) + 
             ' and t.partition_id >=' + DBDiffPackage.IntToString(@P_MinPartitionID) + 
             ' and t.partition_id <=' + DBDiffPackage.IntToString(@P_MaxPartitionID) +
             ' and t.compresstype_id =' + DBDiffPackage.IntToString(@P_CompressTypeID) +
             ' and t.compress_id <=' + DBDiffPackage.IntToString(@P_CompressID);
    set @V_Sql =  @V_Sql + ' ) V order by partition_id'; 
exec DBDiffPackage.Pr_DebugPrint @V_Sql;
    exec (@V_Sql);
    return;    
end;
GO

--删除历史表中的数据
--@P_MinPartitionID输入参数： 最小分区编号
--@P_MaxPartitionID输入参数： 最大分区编号
--@P_CompressTypeID输入参数： 压缩类型编号
--@P_CompressID输入参数： 压缩编号
create procedure CompressDataPackage.Pr_DeleteZXHistoryDatas
          ( @P_MinPartitionID    bigint,
            @P_MaxPartitionID    bigint,
            @P_CompressTypeID    int,
            @P_CompressID    int)
as begin
     declare @V_count int;     
     declare @V_Sql varchar(1000);
     declare @V_Condition varchar(1000);
     declare @V_TableName varchar(50);     
     declare V_Cursor cursor local for Select rp.tablename_tx
     From BS_RangePartition rp Where 
     upper(rp.tablebasename_tx) = upper('zx_history_summary') 
     and ((rp.partitionmin_id <= @P_MinPartitionID and rp.partitionmax_id >= @P_MinPartitionID)
     or (rp.partitionmin_id <= @P_MaxPartitionID and rp.partitionmax_id >= @P_MaxPartitionID)
     or (rp.partitionmin_id >= @P_MinPartitionID and rp.partitionmax_id <= @P_MaxPartitionID));

     set @V_Condition =  ' where partition_id >=' + DBDiffPackage.IntToString(@P_MinPartitionID) +
             ' and partition_id <=' + DBDiffPackage.IntToString(@P_MaxPartitionID) +
             ' and compresstype_id =' + DBDiffPackage.IntToString(@P_CompressTypeID) + ' and compress_id <' + DBDiffPackage.IntToString(@P_CompressID);
     set @V_Sql =  ' Delete From zx_history_summary' + @V_Condition;
     set @V_count =  0;
     select @V_count = Count(*)  
     From BS_RangePartition rp Where
     upper(rp.tablebasename_tx) = upper('zx_history_summary')
     and ((rp.partitionmin_id <= @P_MinPartitionID and rp.partitionmax_id >= @P_MinPartitionID)
     or (rp.partitionmin_id <= @P_MaxPartitionID and rp.partitionmax_id >= @P_MaxPartitionID)
     or (rp.partitionmin_id >= @P_MinPartitionID and rp.partitionmax_id <= @P_MaxPartitionID));
     begin try
		begin tran;
		exec DBDiffPackage.Pr_DebugPrint @V_Sql;
		exec (@V_Sql);
		If @V_count > 0 begin
			open V_Cursor;
			while (1=1)  begin
				fetch next from V_Cursor into @V_TableName;
				if (@@Fetch_Status <> 0) begin
					break;
				end;
				set @V_Sql =  ' Delete from ' + @V_TableName + @V_Condition;
				exec DBDiffPackage.Pr_DebugPrint @V_Sql;
			    exec (@V_Sql);
			end;
			close V_Cursor;
			deallocate V_Cursor;
		end;
		commit tran;
		end try
		begin catch
			Rollback tran;
            return;
        end catch;
end;
GO
            
--更新压缩标记
--@P_HistoryID输入参数： 历史数据编号
--@P_PartitionID输入参数： 分区编号
--@P_CompressID输入参数： 压缩编号
create procedure CompressDataPackage.Pr_UpdateCompressID
          ( @P_HistoryID    bigint,
            @P_PartitionID    bigint,            
            @P_CompressID    int)   
as begin
     declare @V_Sql varchar(1000);
     declare @V_TableBaseName varchar(50);
     declare @V_TableName varchar(50);
     declare @V_Condition varchar(1000);     
     set @V_Condition =  ' where history_id = ' + DBDiffPackage.IntToString(@P_HistoryID) ; 
     set @V_TableBaseName =  'Zx_History_Summary';
     set @V_TableName =  @V_TableBaseName;
     set @V_Sql =  ' update ' + @V_TableName +  ' set compress_id = ' + DBDiffPackage.IntToString(@P_CompressID) + @V_Condition;
     begin try
		begin tran;
		exec DBDiffPackage.Pr_DebugPrint @V_Sql;
		exec (@V_Sql); 
        set @V_TableName =  Commonpackage.F_GetPartitionTable(@V_TableBaseName, @P_PartitionID);
        if DBDiffPackage.GetLength(@V_TableName) > 0 begin            
             set @V_Sql =  ' update ' + @V_TableName +  ' set compress_id = ' + DBDiffPackage.IntToString(@P_CompressID) + @V_Condition;
		exec DBDiffPackage.Pr_DebugPrint @V_Sql;
             exec (@V_Sql);          
        end;
		commit tran;
        end try
        begin catch
			Rollback tran;
            return;
            end catch
end; 
GO
