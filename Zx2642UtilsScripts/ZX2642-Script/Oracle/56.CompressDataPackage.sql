CREATE OR REPLACE Package CompressDataPackage As
Type T_Cursor Is Ref Cursor;

--获取在线摘要数据列表
--Point_ID输入参数： 测点编号
--P_MinPartitionID输入参数： 最小分区编号
--P_MaxPartitionID输入参数： 最大分区编号
--P_CompressTypeID输入参数： 压缩类型编号
--P_CompressID输入参数： 压缩编号
--P_Cursor输出参数：返回公司列表
procedure Pr_GetZXHistoryDatas
          ( P_PointID         In    Pnt_Point.Point_ID%type,
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,
            P_CompressTypeID  In    ZX_History_Summary.CompressType_ID%type,
            P_CompressID      In    ZX_History_Summary.Compress_ID%type, P_Cursor          out   T_Cursor);
            
--删除历史表中的数据
--P_MinPartitionID输入参数： 最小分区编号
--P_MaxPartitionID输入参数： 最大分区编号
--P_CompressTypeID输入参数： 压缩类型编号
--P_CompressID输入参数： 压缩编号
procedure Pr_DeleteZXHistoryDatas
          ( P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,
            P_CompressTypeID  In    ZX_History_Summary.CompressType_ID%type,
            P_CompressID      In    ZX_History_Summary.Compress_ID%type);  
            
--更新压缩标记
--P_HistoryID输入参数： 历史数据编号
--P_PartitionID输入参数： 分区编号
--P_CompressID输入参数： 压缩编号
procedure Pr_UpdateCompressID
          ( P_HistoryID      In    ZX_History_Summary.History_ID%type,
            P_PartitionID    In    ZX_History_Summary.Partition_ID%type,            
            P_CompressID      In    ZX_History_Summary.Compress_ID%type);                       

End CompressDataPackage;

/

CREATE OR REPLACE Package Body CompressDataPackage As

--获取在线摘要数据列表
--Point_ID输入参数： 测点编号
--P_MinPartitionID输入参数： 最小分区编号
--P_MaxPartitionID输入参数： 最大分区编号
--P_CompressTypeID输入参数： 压缩类型编号
--P_CompressID输入参数： 压缩编号
--P_Cursor输出参数：返回公司列表
procedure Pr_GetZXHistoryDatas
          ( P_PointID         In    Pnt_Point.Point_ID%type,
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_CompressTypeID  In    ZX_History_Summary.CompressType_ID%type,
            P_CompressID      In    ZX_History_Summary.Compress_ID%type,  P_Cursor          out   T_Cursor)
as
     V_count  BS_MetaFieldType.Int_NR%type;     
     V_Sql    BS_MetaFieldType.OneKilo_TX%type;
     V_TableName BS_MetaFieldType.Fifty_TX%type;
     Cursor V_Cursor is Select rp.tablename_tx
     From BS_RangePartition rp Where 
     upper(rp.tablebasename_tx) = upper('zx_history_summary') 
     and ((rp.partitionmin_id <= P_MinPartitionID and rp.partitionmax_id >= P_MinPartitionID)
     or (rp.partitionmin_id <= P_MaxPartitionID and rp.partitionmax_id >= P_MaxPartitionID)
     or (rp.partitionmin_id >= P_MinPartitionID and rp.partitionmax_id <= P_MaxPartitionID));
begin
     V_Sql := 'Select * From zx_history_summary';
     V_count := 0;
     Select Count(*) Into V_count
     From BS_RangePartition rp Where 
     upper(rp.tablebasename_tx) = upper('zx_history_summary') 
     and ((rp.partitionmin_id <= P_MinPartitionID and rp.partitionmax_id >= P_MinPartitionID)
     or (rp.partitionmin_id <= P_MaxPartitionID and rp.partitionmax_id >= P_MaxPartitionID)
     or (rp.partitionmin_id >= P_MinPartitionID and rp.partitionmax_id <= P_MaxPartitionID));

     If V_count > 0 Then
		open V_Cursor;
		while (1=1) loop
			fetch V_Cursor into V_TableName;
			if (V_Cursor%NOTFOUND) then
				exit;
			end if;
			V_Sql := V_Sql || ' union all Select * from ' || V_TableName;
		end loop;
        close V_Cursor;
    end if;
    
    V_Sql := 'Select * from ( select * from ( ' || V_Sql || ' ) t where t.point_id =' || DBDiffPackage.IntToString(P_PointID) || 
             ' and t.partition_id >=' || DBDiffPackage.IntToString(P_MinPartitionID) || 
             ' and t.partition_id <=' || DBDiffPackage.IntToString(P_MaxPartitionID) ||
             ' and t.compresstype_id =' || DBDiffPackage.IntToString(P_CompressTypeID) ||
             ' and t.compress_id <=' || DBDiffPackage.IntToString(P_CompressID);
    V_Sql := V_Sql || ' ) V order by partition_id'; 
    DBDiffPackage.Pr_DebugPrint(V_Sql);             
    open P_Cursor for V_Sql;
    return;    
end;

--删除历史表中的数据
--P_MinPartitionID输入参数： 最小分区编号
--P_MaxPartitionID输入参数： 最大分区编号
--P_CompressTypeID输入参数： 压缩类型编号
--P_CompressID输入参数： 压缩编号
procedure Pr_DeleteZXHistoryDatas
          ( P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,
            P_CompressTypeID  In    ZX_History_Summary.CompressType_ID%type,
            P_CompressID      In    ZX_History_Summary.Compress_ID%type)
as
     V_count  BS_MetaFieldType.Int_NR%type;     
     V_Sql    BS_MetaFieldType.OneKilo_TX%type;
     V_Condition BS_MetaFieldType.OneKilo_TX%type;
     V_TableName BS_MetaFieldType.Fifty_TX%type;
     Cursor V_Cursor is Select rp.tablename_tx
     From BS_RangePartition rp Where 
     upper(rp.tablebasename_tx) = upper('zx_history_summary') 
     and ((rp.partitionmin_id <= P_MinPartitionID and rp.partitionmax_id >= P_MinPartitionID)
     or (rp.partitionmin_id <= P_MaxPartitionID and rp.partitionmax_id >= P_MaxPartitionID)
     or (rp.partitionmin_id >= P_MinPartitionID and rp.partitionmax_id <= P_MaxPartitionID));
begin

     V_Condition := ' where partition_id >=' || DBDiffPackage.IntToString(P_MinPartitionID) ||
             ' and partition_id <=' || DBDiffPackage.IntToString(P_MaxPartitionID) ||
             ' and compresstype_id =' || DBDiffPackage.IntToString(P_CompressTypeID) || ' and compress_id <' || DBDiffPackage.IntToString(P_CompressID);
     V_Sql := ' Delete From zx_history_summary' || V_Condition;
     V_count := 0;
     Select Count(*) Into V_count
     From BS_RangePartition rp Where
     upper(rp.tablebasename_tx) = upper('zx_history_summary')
     and ((rp.partitionmin_id <= P_MinPartitionID and rp.partitionmax_id >= P_MinPartitionID)
     or (rp.partitionmin_id <= P_MaxPartitionID and rp.partitionmax_id >= P_MaxPartitionID)
     or (rp.partitionmin_id >= P_MinPartitionID and rp.partitionmax_id <= P_MaxPartitionID));
     
     begin
       DBDiffPackage.Pr_DebugPrint(V_Sql);
       execute immediate   V_Sql;
       If V_count > 0 Then
			open V_Cursor;
			while (1=1) loop
				fetch V_Cursor into V_TableName;
				if (V_Cursor%NOTFOUND) then
					exit;
				end if;
				V_Sql := ' Delete from ' || V_TableName || V_Condition;
			    DBDiffPackage.Pr_DebugPrint(V_Sql);
			    execute immediate   V_Sql;
			end loop;
			close V_Cursor;
        end if;
          EXCEPTION
              WHEN OTHERS THEN
				Rollback;
                return;
     end;
     commit;      
end;          
            
--更新压缩标记
--P_HistoryID输入参数： 历史数据编号
--P_PartitionID输入参数： 分区编号
--P_CompressID输入参数： 压缩编号
procedure Pr_UpdateCompressID
          ( P_HistoryID      In    ZX_History_Summary.History_ID%type,
            P_PartitionID    In    ZX_History_Summary.Partition_ID%type,            
            P_CompressID      In    ZX_History_Summary.Compress_ID%type)   
as     
     V_Sql    BS_MetaFieldType.OneKilo_TX%type;
     V_TableBaseName BS_MetaFieldType.Fifty_TX%type;
     V_TableName BS_MetaFieldType.Fifty_TX%type;
     V_Condition BS_MetaFieldType.OneKilo_TX%type;     
begin     
     V_Condition := ' where history_id = ' || DBDiffPackage.IntToString(P_HistoryID) ; 
     V_TableBaseName := 'Zx_History_Summary';
     V_TableName := V_TableBaseName;
     V_Sql := ' update ' || V_TableName ||  ' set compress_id = ' || DBDiffPackage.IntToString(P_CompressID) || V_Condition;
     begin
         DBDiffPackage.Pr_DebugPrint(V_Sql);
         execute immediate   V_Sql; 
         V_TableName := Commonpackage.F_GetPartitionTable(V_TableBaseName, P_PartitionID);
         if DBDiffPackage.GetLength(V_TableName) > 0 Then            
             V_Sql := ' update ' || V_TableName ||  ' set compress_id = ' || DBDiffPackage.IntToString(P_CompressID) || V_Condition;
             DBDiffPackage.Pr_DebugPrint(V_Sql);
             execute immediate   V_Sql;          
         end if;
      EXCEPTION
          WHEN OTHERS THEN
			Rollback;
            return;
      end;
      commit;  
end;         

      

End CompressDataPackage;







/