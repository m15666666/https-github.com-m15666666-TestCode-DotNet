CREATE OR REPLACE Package HistoryDataPackage As
Type T_Cursor Is Ref Cursor;

--获取历史数据编号对应的分区代理键
Function F_GetPartitionIDByHistoryID(
       P_HistoryID in zx_history_summary.history_id%type)return BS_MetaFieldType.BigInt_NR%type;

--获取历史摘要数据
--P_HistoryID： 历史数据编号
--P_Cursor输出参数：返回历史摘要数据
procedure Pr_GetSummaryByKey
          ( P_HistoryID         In    zx_history_summary.history_id%type, P_Cursor          out   T_Cursor);
		  
--获取相同同步号的历史摘要数据
--P_SyncNR: 同步号
--P_HistoryID： 历史数据编号
--P_Cursor输出参数：返回历史摘要数据
procedure Pr_GetSummaryBySyncNR
          ( P_SyncNR         In    zx_history_summary.synch_nr%type, 
			P_HistoryID         In    zx_history_summary.history_id%type,
			P_Cursor          out   T_Cursor);
            
--删除历史摘要数据
--P_HistoryID： 历史数据编号
--P_Emsg: OUTPUT 错误信息
procedure Pr_DeleteSummaryByKey( 
            P_HistoryID  In    ZX_History_Summary.history_id%type,
            P_Emsg Out BS_MetaFieldType.Emsg_TX%type);  
            
-- 获取测点下的历史摘要数据            
-- P_PointID 测点编号
--P_MinPartitionID输入参数： 最小分区编号
--P_MaxPartitionID输入参数： 最大分区编号
-- P_Cursor 输出参数：返回历史摘要数据列表
procedure Pr_GetSummaryListByPoint(
            P_PointID         In    pnt_point.point_id%type,      
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type, P_Cursor        out   T_Cursor);   
            
           
-- 获取测点下指定转速范围的历史摘要数据            
-- P_PointID 测点编号
--P_MinPartitionID输入参数： 最小分区编号
--P_MaxPartitionID输入参数： 最大分区编号
-- P_LowerSpeed 转速下限
-- P_UpperSpeed 转速上限
-- P_Cursor 输出参数：返回历史摘要数据列表
procedure Pr_GetSummaryByPointAndSpeed(
            P_PointID         In    pnt_point.point_id%type,      
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,
            P_LowerSpeed    In    BS_MetaFieldType.Int_NR%type,
            P_UpperSpeed    In    BS_MetaFieldType.Int_NR%type,  P_Cursor        out   T_Cursor);    
            
--获取历史特征数据
--P_HistoryID： 历史数据编号
--P_ChannelNumber: 通道号
-- P_FeatureValueID 特征指标编号
--P_Cursor输出参数：返回历史特征数据
procedure Pr_GetFeatureValueByKey
          ( P_HistoryID         In    zx_history_summary.history_id%type,
            P_ChannelNumber     In    zx_history_waveform.chnno_nr%type,
            P_FeatureValueID In    zx_history_featurevalue.featurevalue_id%type, P_Cursor          out   T_Cursor);                          
            
            
-- 获取指定指标的历史特征数据   
--P_MinPartitionID输入参数： 最小分区编号
--P_MaxPartitionID输入参数： 最大分区编号
-- P_ChannelNumber 通道号
-- P_FeatureValueID 特征指标编号
-- P_Cursor 输出参数：返回指定指标的历史特征数据  
procedure Pr_GetFeaturesByChnNrAndFeat(
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,
            P_ChannelNumber  In    zx_history_featurevalue.chnno_nr%type,
            P_FeatureValueID In    zx_history_featurevalue.featurevalue_id%type, P_Cursor        out   T_Cursor); 
            
-- 获取历史特征数据 
--P_MinPartitionID输入参数： 最小分区编号
--P_MaxPartitionID输入参数： 最大分区编号
-- P_ChannelNumber 通道号
-- P_Cursor 输出参数：返回历史特征数据      
procedure Pr_GetFeaturesByChannelNr(
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,
            P_ChannelNumber  In    zx_history_featurevalue.chnno_nr%type, P_Cursor        out   T_Cursor);                       
            
-- 检查指定测点是否存在历史摘要数据        
-- P_PointID 测点编号
--P_MinPartitionID输入参数： 最小分区编号
--P_MaxPartitionID输入参数： 最大分区编号
-- P_HasResult 输出参数：1-存在摘要数据，0-不存在
procedure Pr_CheckPointHistories(
            P_PointID         In    pnt_point.point_id%type,
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,       
            P_HasResult out BS_MetaFieldType.Int_NR%type);   
            
--获取历史波形数据
--P_HistoryID： 历史数据编号
--P_ChannelNumber: 通道号
--P_Cursor输出参数：返回历史摘要数据
procedure Pr_GetWaveformByKey
          ( P_HistoryID         In    zx_history_summary.history_id%type,
            P_Cursor          out   T_Cursor);   
            
--获取历史频谱波形数据
--P_HistoryID： 历史数据编号
--P_Cursor输出参数：返回历史摘要数据
procedure Pr_GetFreqWaveformByKey
          ( P_HistoryID         In    zx_history_summary.history_id%type, P_Cursor          out   T_Cursor);            
            
-- 获取包含特征参数值的趋势数据   
-- P_PointID 测点编号
--P_MinPartitionID输入参数： 最小分区编号
--P_MaxPartitionID输入参数： 最大分区编号
-- P_LowerSpeed 转速下限
-- P_UpperSpeed 转速上限
-- P_ChannelNumber 通道号
-- P_RecordCount    最多返回记录数
-- P_Count 实际记录条数
-- P_Cursor 输出参数：返回历史特征数据      
procedure Pr_GetAllFeatTrendsByChnNSpeed( 
            P_PointID         In    pnt_point.point_id%type,      
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,
            P_LowerSpeed    In    BS_MetaFieldType.Int_NR%type,
            P_UpperSpeed    In    BS_MetaFieldType.Int_NR%type,
            P_ChannelNumber  In    zx_history_featurevalue.chnno_nr%type,            
            P_RecordCount    In    BS_MetaFieldType.Int_NR%type,
            P_QueryCountFlag In BS_MetaFieldType.Int_NR%type,
            P_Count          Out BS_MetaFieldType.Int_NR%type, P_Cursor        out   T_Cursor);   
            
-- 获取包含特征参数值的趋势数据   
-- P_PointID 测点编号
--P_MinPartitionID输入参数： 最小分区编号
--P_MaxPartitionID输入参数： 最大分区编号
-- P_ChannelNumber 通道号
-- P_RecordCount    最多返回记录数
-- P_Count 实际记录条数
-- P_Cursor 输出参数：返回历史特征数据      
procedure Pr_GetAllFeatTrendsByChnNr( 
            P_PointID         In    pnt_point.point_id%type,      
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,
            P_ChannelNumber  In    zx_history_featurevalue.chnno_nr%type,
            P_RecordCount    In    BS_MetaFieldType.Int_NR%type,
            P_QueryCountFlag In BS_MetaFieldType.Int_NR%type,
            P_Count          Out BS_MetaFieldType.Int_NR%type, P_Cursor        out   T_Cursor); 
            
-- 获取趋势数据   
-- P_PointID 测点编号
--P_MinPartitionID输入参数： 最小分区编号
--P_MaxPartitionID输入参数： 最大分区编号
-- P_LowerSpeed 转速下限
-- P_UpperSpeed 转速上限
-- P_ChannelNumber 通道号
-- P_FeatureValueID 特征指标编号
-- P_Cursor 输出参数：返回历史特征数据      
procedure Pr_GetTrendDatasByChnNSpeed( 
            P_PointID         In    pnt_point.point_id%type,      
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,
            P_LowerSpeed    In    BS_MetaFieldType.Int_NR%type,
            P_UpperSpeed    In    BS_MetaFieldType.Int_NR%type,
            P_ChannelNumber  In    zx_history_featurevalue.chnno_nr%type,
            P_FeatureValueID In    zx_history_featurevalue.featurevalue_id%type, P_Cursor        out   T_Cursor);   
            
-- 获取趋势数据   
-- P_PointID 测点编号
--P_MinPartitionID输入参数： 最小分区编号
--P_MaxPartitionID输入参数： 最大分区编号
-- P_ChannelNumber 通道号
-- P_FeatureValueID 特征指标编号
-- P_Cursor 输出参数：返回历史特征数据      
procedure Pr_GetTrendDatasByChnNr( 
            P_PointID         In    pnt_point.point_id%type,      
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,
            P_ChannelNumber  In    zx_history_featurevalue.chnno_nr%type,
            P_FeatureValueID In    zx_history_featurevalue.featurevalue_id%type, P_Cursor        out   T_Cursor);                                                                       
			
-- 获取设备的工况数据   
-- P_MobjectID 设备编号
-- P_MinPartitionID输入参数： 最小分区编号
-- P_MaxPartitionID输入参数： 最大分区编号
-- P_PageSize 分页大小
-- P_PageIndex 页码
-- P_Count 实际记录条数
-- P_Cursor 输出参数：返回设备的工况数据    
procedure Pr_GetMObjectWorkingDatas( 
			P_MobjectID		in		Mob_MObject.Mobject_ID%type, 
            P_MinPartitionID    in		ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    in		ZX_History_Summary.Partition_ID%type,
            P_PageSize		in		BS_MetaFieldType.Int_NR%type,
			P_PageIndex in		BS_MetaFieldType.Int_NR%type,
			P_Count out		BS_MetaFieldType.Int_NR%type, P_Cursor            out		T_Cursor);

-- 获取设备的工况数据测点   
-- P_MobjectID 设备编号
-- P_PageSize 分页大小
-- P_PageIndex 页码
-- P_Count 实际记录条数
-- P_Cursor 输出参数：返回设备的工况数据测点    
procedure Pr_GetMObjectWorkingDataPoints( 
			P_MobjectID		in		Mob_MObject.Mobject_ID%type, 
            P_PageSize		in		BS_MetaFieldType.Int_NR%type,
			P_PageIndex in		BS_MetaFieldType.Int_NR%type,
			P_Count out		BS_MetaFieldType.Int_NR%type, P_Cursor            out		T_Cursor);

-- 获取报警等级对应的数量   
-- P_AlmLevelID 报警等级
-- P_Cursor 输出参数：返回报警等级对应的数量  
procedure Pr_GetHistoryAlmCount( 
			P_AlmLevelID In ZX_History_Alm.Almlevel_Id%type, 
			P_Count out		BS_MetaFieldType.Int_NR%type, P_Cursor            out		T_Cursor);
			
End HistoryDataPackage;

/

CREATE OR REPLACE Package Body HistoryDataPackage As

--获取历史数据编号对应的分区代理键
Function F_GetPartitionIDByHistoryID(
       P_HistoryID in zx_history_summary.history_id%type)return BS_MetaFieldType.BigInt_NR%type
As
V_PartitionID BS_MetaFieldType.BigInt_NR%type;
Begin
       Select Partition_ID Into V_PartitionID From ZX_History_DataMapping Where History_ID = P_HistoryID;
       return V_PartitionID;
End;


--获取历史摘要数据
--P_HistoryID： 历史数据编号
--P_Cursor输出参数：返回历史摘要数据
procedure Pr_GetSummaryByKey
          ( P_HistoryID         In    zx_history_summary.history_id%type, P_Cursor          out   T_Cursor)
as
    V_PartitionID BS_MetaFieldType.BigInt_NR%type;
    V_QueryObjSql BS_MetaFieldType.FiveKilo_TX%type;
    V_TableBaseName BS_MetaFieldType.Fifty_TX%type;
    V_Sql         BS_MetaFieldType.FiveKilo_TX%type;
begin
    V_PartitionID := HistoryDataPackage.F_GetPartitionIDByHistoryID(P_HistoryID);
    V_TableBaseName := 'Zx_History_Summary';
    V_QueryObjSql := Commonpackage.F_GetPartitionSQL(V_PartitionID, V_PartitionID, V_TableBaseName);
    V_Sql := 
      ' Select * From ' || V_QueryObjSql ||
      ' Where History_ID = ' || DBDiffPackage.IntToString(P_HistoryID);
    open P_Cursor for V_Sql;    
end;

--获取相同同步号的历史摘要数据
--P_SyncNR 同步号
--P_Cursor输出参数：返回历史摘要数据
procedure Pr_GetSummaryBySyncNR
          ( P_SyncNR         In    zx_history_summary.synch_nr%type,
			P_HistoryID         In    zx_history_summary.history_id%type,
			P_Cursor out   T_Cursor)
as
    V_PartitionID BS_MetaFieldType.BigInt_NR%type;
    V_QueryObjSql BS_MetaFieldType.FiveKilo_TX%type;
    V_TableBaseName BS_MetaFieldType.Fifty_TX%type;
    V_Sql         BS_MetaFieldType.FiveKilo_TX%type;
begin
    V_PartitionID := HistoryDataPackage.F_GetPartitionIDByHistoryID(P_HistoryID);
    V_TableBaseName := 'Zx_History_Summary';
    V_QueryObjSql := Commonpackage.F_GetPartitionSQL(V_PartitionID, V_PartitionID, V_TableBaseName);
    V_Sql := 
      ' Select * From ' || V_QueryObjSql ||
      ' Where Synch_NR = ' || DBDiffPackage.IntToString(P_SyncNR);
    open P_Cursor for V_Sql;    
end;  

--删除历史摘要数据
--P_HistoryID： 历史数据编号
--P_Emsg: OUTPUT 错误信息
procedure Pr_DeleteSummaryByKey( 
            P_HistoryID  In    ZX_History_Summary.history_id%type,
            P_Emsg Out BS_MetaFieldType.Emsg_TX%type)
as
    V_PartitionID BS_MetaFieldType.BigInt_NR%type;
    V_TableName BS_MetaFieldType.Fifty_TX%type;
    V_TableBaseName BS_MetaFieldType.Fifty_TX%type;
    V_Sql         BS_MetaFieldType.FiveKilo_TX%type;
begin
    V_PartitionID := HistoryDataPackage.F_GetPartitionIDByHistoryID(P_HistoryID);
    V_TableBaseName := 'Zx_History_Summary';
    V_TableName := V_TableBaseName;
    V_Sql := 
      ' Delete From ' || V_TableName ||
      ' Where History_ID = ' || DBDiffPackage.IntToString(P_HistoryID);   
             
    Begin
    Execute Immediate V_SQL;
    V_TableName := Commonpackage.F_GetPartitionTable(V_TableBaseName, V_PartitionID);
	if DBDiffPackage.GetLength(V_TableName) > 0 Then  
		V_Sql := 
		  ' Delete From ' || V_TableName ||
		  ' Where History_ID = ' || DBDiffPackage.IntToString(P_HistoryID);    
		Execute Immediate V_SQL;
	end if;
    Exception
    When OTHERS Then
      P_Emsg := SQLERRM;
      Rollback;
    End;
    commit; 
end;   

-- 获取测点下的历史摘要数据            
-- P_PointID 测点编号
--P_MinPartitionID输入参数： 最小分区编号
--P_MaxPartitionID输入参数： 最大分区编号
-- P_Cursor 输出参数：返回历史摘要数据列表
procedure Pr_GetSummaryListByPoint(
            P_PointID         In    pnt_point.point_id%type,      
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type, P_Cursor        out   T_Cursor)
as
    V_TableBaseName BS_MetaFieldType.Fifty_TX%type;
    V_QueryObjSql BS_MetaFieldType.FiveKilo_TX%type;
    V_Sql         BS_MetaFieldType.FiveKilo_TX%type;
begin
    V_TableBaseName := 'Zx_History_Summary';
    V_QueryObjSql := Commonpackage.F_GetPartitionSQL(P_MinPartitionID, P_MaxPartitionID, V_TableBaseName);
    V_Sql := 
      ' Select * From ' || V_QueryObjSql ||
      ' Where Point_ID = ' || DBDiffPackage.IntToString(P_PointID) || 
      ' And Partition_ID <= ' || DBDiffPackage.IntToString(P_MaxPartitionID) || ' And Partition_ID >= ' || DBDiffPackage.IntToString(P_MinPartitionID);
    open P_Cursor for V_Sql;    
end;                                  
            
-- 获取测点下指定转速范围的历史摘要数据            
-- P_PointID 测点编号
--P_MinPartitionID输入参数： 最小分区编号
--P_MaxPartitionID输入参数： 最大分区编号
-- P_LowerSpeed 转速下限
-- P_UpperSpeed 转速上限
-- P_Cursor 输出参数：返回历史摘要数据列表
procedure Pr_GetSummaryByPointAndSpeed(
            P_PointID         In    pnt_point.point_id%type,      
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,
            P_LowerSpeed    In    BS_MetaFieldType.Int_NR%type,
            P_UpperSpeed    In    BS_MetaFieldType.Int_NR%type, P_Cursor        out   T_Cursor)
as
    V_TableBaseName BS_MetaFieldType.Fifty_TX%type;
    V_QueryObjSql BS_MetaFieldType.FiveKilo_TX%type;
    V_Sql         BS_MetaFieldType.FiveKilo_TX%type;
begin
    V_TableBaseName := 'Zx_History_Summary';
    V_QueryObjSql := Commonpackage.F_GetPartitionSQL(P_MinPartitionID, P_MaxPartitionID, V_TableBaseName);
    V_Sql := 
      ' Select * From ' || V_QueryObjSql ||
      ' Where Point_ID = ' || DBDiffPackage.IntToString(P_PointID) || ' And RotSpeed_NR >= ' || DBDiffPackage.IntToString(P_LowerSpeed) || 'and RotSpeed_NR <=' || DBDiffPackage.IntToString(P_UpperSpeed) || 
      ' And Partition_ID <= ' || DBDiffPackage.IntToString(P_MaxPartitionID) || ' And Partition_ID >= ' || DBDiffPackage.IntToString(P_MinPartitionID);
    open P_Cursor for V_Sql;    
end;   

-- 检查指定测点是否存在历史摘要数据        
-- P_PointID 测点编号
--P_MinPartitionID输入参数： 最小分区编号
--P_MaxPartitionID输入参数： 最大分区编号
-- P_HasResult 输出参数：1-存在摘要数据，0-不存在
procedure Pr_CheckPointHistories(
            P_PointID         In    pnt_point.point_id%type,
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,      
            P_HasResult out BS_MetaFieldType.Int_NR%type)
as
     V_TableBaseName BS_MetaFieldType.Fifty_TX%type;
     V_QueryObjSql BS_MetaFieldType.FiveKilo_TX%type;
     V_Sql    BS_MetaFieldType.FiveKilo_TX%type;
begin
     V_TableBaseName := 'Zx_History_Summary';
     V_QueryObjSql := Commonpackage.F_GetPartitionSQL(P_MinPartitionID, P_MaxPartitionID, V_TableBaseName);
     V_Sql := 
      ' Select * From ' || V_QueryObjSql ||
      ' Where Point_ID = ' || DBDiffPackage.IntToString(P_PointID) || 
      ' And Partition_ID <= ' || DBDiffPackage.IntToString(P_MaxPartitionID) || ' And Partition_ID >= ' || DBDiffPackage.IntToString(P_MinPartitionID);
     V_SQL := 'Select Count(*) From (' || V_Sql || ') V';
     DBDiffPackage.Pr_DebugPrint(V_Sql);
     Execute Immediate V_SQL Into P_HasResult;
end;    


--获取历史波形数据
--P_HistoryID： 历史数据编号
--P_Cursor输出参数：返回历史摘要数据
procedure Pr_GetWaveformByKey
          ( P_HistoryID         In    zx_history_summary.history_id%type, P_Cursor          out   T_Cursor)
as
    V_PartitionID BS_MetaFieldType.BigInt_NR%type;
    V_QueryObjSql BS_MetaFieldType.FiveKilo_TX%type;
    V_TableBaseName BS_MetaFieldType.Fifty_TX%type;
    V_Sql         BS_MetaFieldType.FiveKilo_TX%type;
begin
    V_PartitionID := HistoryDataPackage.F_GetPartitionIDByHistoryID(P_HistoryID);
    V_TableBaseName := 'Zx_History_WaveForm';
    V_QueryObjSql := Commonpackage.F_GetPartitionSQL(V_PartitionID, V_PartitionID, V_TableBaseName);
    V_Sql := 
      ' Select * From ' || V_QueryObjSql ||
      ' Where History_ID = ' || DBDiffPackage.IntToString(P_HistoryID);
    open P_Cursor for V_Sql;    
end; 

--获取历史频谱波形数据
--P_HistoryID： 历史数据编号
--P_Cursor输出参数：返回历史摘要数据
procedure Pr_GetFreqWaveformByKey
          ( P_HistoryID         In    zx_history_summary.history_id%type, P_Cursor          out   T_Cursor)
as
    V_PartitionID BS_MetaFieldType.BigInt_NR%type;
    V_QueryObjSql BS_MetaFieldType.FiveKilo_TX%type;
    V_TableBaseName BS_MetaFieldType.Fifty_TX%type;
    V_Sql         BS_MetaFieldType.FiveKilo_TX%type;
begin
    V_PartitionID := HistoryDataPackage.F_GetPartitionIDByHistoryID(P_HistoryID);
    V_TableBaseName := 'Zx_History_Freq';
    V_QueryObjSql := Commonpackage.F_GetPartitionSQL(V_PartitionID, V_PartitionID, V_TableBaseName);
    V_Sql := 
      ' Select * From ' || V_QueryObjSql ||
      ' Where History_ID = ' || DBDiffPackage.IntToString(P_HistoryID);
    open P_Cursor for V_Sql;    
end; 

--获取历史特征数据
--P_HistoryID： 历史数据编号
--P_ChannelNumber: 通道号
-- P_FeatureValueID 特征指标编号
--P_Cursor输出参数：返回历史特征数据
procedure Pr_GetFeatureValueByKey
          ( P_HistoryID         In    zx_history_summary.history_id%type,
            P_ChannelNumber     In    zx_history_waveform.chnno_nr%type,
            P_FeatureValueID In    zx_history_featurevalue.featurevalue_id%type, P_Cursor          out   T_Cursor)
as
    V_PartitionID BS_MetaFieldType.BigInt_NR%type;
    V_QueryObjSql BS_MetaFieldType.FiveKilo_TX%type;
    V_TableBaseName BS_MetaFieldType.Fifty_TX%type;
    V_Sql         BS_MetaFieldType.FiveKilo_TX%type;
begin
    V_PartitionID := HistoryDataPackage.F_GetPartitionIDByHistoryID(P_HistoryID);
    V_TableBaseName := 'Zx_History_FeatureValue';
    V_QueryObjSql := Commonpackage.F_GetPartitionSQL(V_PartitionID, V_PartitionID, V_TableBaseName);
    V_Sql := 
      ' Select * From ' || V_QueryObjSql ||
      ' Where History_ID = ' || DBDiffPackage.IntToString(P_HistoryID) || 'And ChnNo_NR = ' || DBDiffPackage.IntToString(P_ChannelNumber) || ' And FeatureValue_ID = '
      || DBDiffPackage.IntToString(P_FeatureValueID);
    open P_Cursor for V_Sql;    
end;           
             
-- 获取指定指标的历史特征数据    
--P_MinPartitionID输入参数： 最小分区编号
--P_MaxPartitionID输入参数： 最大分区编号
-- P_ChannelNumber 通道号
-- P_FeatureValueID 特征指标编号
-- P_Cursor 输出参数：返回指定指标的历史特征数据  
procedure Pr_GetFeaturesByChnNrAndFeat(  
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,
            P_ChannelNumber  In    zx_history_featurevalue.chnno_nr%type,
            P_FeatureValueID In    zx_history_featurevalue.featurevalue_id%type, P_Cursor        out   T_Cursor)
as
    V_TableBaseName BS_MetaFieldType.Fifty_TX%type;
    V_QueryObjSql BS_MetaFieldType.FiveKilo_TX%type;
    V_Sql         BS_MetaFieldType.FiveKilo_TX%type;
begin
    V_TableBaseName := 'Zx_History_FeatureValue';
    V_QueryObjSql := Commonpackage.F_GetPartitionSQL(P_MinPartitionID, P_MaxPartitionID, V_TableBaseName);
    V_Sql := 
      ' Select * From ' || V_QueryObjSql ||
      ' Where chnno_nr = ' || DBDiffPackage.IntToString(P_ChannelNumber) || ' And featurevalue_id = ' || DBDiffPackage.IntToString(P_FeatureValueID) || 
      ' And Partition_ID <= ' || DBDiffPackage.IntToString(P_MaxPartitionID) || ' And Partition_ID >= ' || DBDiffPackage.IntToString(P_MinPartitionID);
    open P_Cursor for V_Sql;    
end;             
            
-- 获取历史特征数据   
--P_MinPartitionID输入参数： 最小分区编号
--P_MaxPartitionID输入参数： 最大分区编号
-- P_ChannelNumber 通道号
-- P_Cursor 输出参数：返回历史特征数据      
procedure Pr_GetFeaturesByChannelNr(   
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,
            P_ChannelNumber  In    zx_history_featurevalue.chnno_nr%type, P_Cursor        out   T_Cursor)
as
    V_TableBaseName BS_MetaFieldType.Fifty_TX%type;
    V_QueryObjSql BS_MetaFieldType.OneKilo_TX%type;
    V_Sql         BS_MetaFieldType.FiveKilo_TX%type;
begin
    V_TableBaseName := 'Zx_History_FeatureValue';
    V_QueryObjSql := Commonpackage.F_GetPartitionSQL(P_MinPartitionID, P_MaxPartitionID, V_TableBaseName);
    V_Sql := 
      ' Select * From ' || V_QueryObjSql ||
      ' Where chnno_nr = ' || DBDiffPackage.IntToString(P_ChannelNumber) || 
      ' And Partition_ID <= ' || DBDiffPackage.IntToString(P_MaxPartitionID) || ' And Partition_ID >= ' || DBDiffPackage.IntToString(P_MinPartitionID);
    open P_Cursor for V_Sql;    
end;   

-- 获取包含特征参数值的趋势数据   
-- P_PointID 测点编号
--P_MinPartitionID输入参数： 最小分区编号
--P_MaxPartitionID输入参数： 最大分区编号
-- P_LowerSpeed 转速下限
-- P_UpperSpeed 转速上限
-- P_ChannelNumber 通道号
-- P_RecordCount    最多返回记录数
-- P_Count 实际记录条数
-- P_Cursor 输出参数：返回历史特征数据      
procedure Pr_GetAllFeatTrendsByChnNSpeed( 
            P_PointID         In    pnt_point.point_id%type,      
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,
            P_LowerSpeed    In    BS_MetaFieldType.Int_NR%type,
            P_UpperSpeed    In    BS_MetaFieldType.Int_NR%type,
            P_ChannelNumber  In    zx_history_featurevalue.chnno_nr%type,
            P_RecordCount    In   BS_MetaFieldType.Int_NR%type,
            P_QueryCountFlag In BS_MetaFieldType.Int_NR%type,
            P_Count          Out    BS_MetaFieldType.Int_NR%type,     P_Cursor        out   T_Cursor)
as
    V_TableFeatBaseName BS_MetaFieldType.Fifty_TX%type;
    V_TableSummBaseName BS_MetaFieldType.Fifty_TX%type;
    V_QueryFeatureSql BS_MetaFieldType.OneKilo_TX%type;
    V_QuerySummarySql BS_MetaFieldType.FiveKilo_TX%type;
    V_Sql         BS_MetaFieldType.FiveKilo_TX%type;
    V_SqlQueryCount        BS_MetaFieldType.FiveKilo_TX%type;    
begin
    V_TableFeatBaseName := 'Zx_History_FeatureValue';
    V_TableSummBaseName := 'Zx_History_Summary';
    V_QueryFeatureSql := Commonpackage.F_GetPartitionSQL(P_MinPartitionID, P_MaxPartitionID, V_TableFeatBaseName);
    V_QuerySummarySql := Commonpackage.F_GetPartitionSQL(P_MinPartitionID, P_MaxPartitionID, V_TableSummBaseName);
    V_SqlQueryCount := ' Select Count(history_id) From  ' || V_QuerySummarySql ||
      ' Where Point_ID = ' || DBDiffPackage.IntToString(P_PointID) || ' And RotSpeed_NR >= ' || DBDiffPackage.IntToString(P_LowerSpeed) || ' And RotSpeed_NR <=' || DBDiffPackage.IntToString(P_UpperSpeed) || 
      ' And Partition_ID <= ' || DBDiffPackage.IntToString(P_MaxPartitionID) || ' And Partition_ID >= ' || DBDiffPackage.IntToString(P_MinPartitionID);
    
    -- 查询P_PageIndex为-1的情况下，返回总记录数
	if P_QueryCountFlag = -1 then
		execute immediate  V_SqlQueryCount into P_Count;
		open P_Cursor for select * from dual where 1 <> 1;
		return;
	end if;
     V_Sql :=  'Select 
                  Partition_ID,
                  Compress_ID,
                  SampTime_DT,
                  DatLen_NR,
                  History_ID,
                  Point_ID,
                  Alm_ID,
                  AlmLevel_ID,
                  RotSpeed_NR
                  From   ' || V_QuerySummarySql ||
      ' Where Point_ID = ' || DBDiffPackage.IntToString(P_PointID) || ' And RotSpeed_NR >= ' || DBDiffPackage.IntToString(P_LowerSpeed) || ' And RotSpeed_NR <=' || DBDiffPackage.IntToString(P_UpperSpeed) || 
      ' And Partition_ID <= ' || DBDiffPackage.IntToString(P_MaxPartitionID) || ' And Partition_ID >= ' || DBDiffPackage.IntToString(P_MinPartitionID) ;
    V_Sql := DBDiffPackage.GetPagingSql(V_Sql, 0, P_RecordCount, null);
    V_Sql := 
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
         From ( ' ||
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
             From ( ' || V_Sql || ' ) s ' || ' join ( ' || 
      ' Select 
            FeatureValue_NR,
            FeatureValue_ID,
            History_ID          
       From ' 
      || V_QueryFeatureSql ||
      ' Where chnno_nr = ' || DBDiffPackage.IntToString(P_ChannelNumber) || 
      ' And Partition_ID <= ' || DBDiffPackage.IntToString(P_MaxPartitionID) || ' And Partition_ID >= ' || DBDiffPackage.IntToString(P_MinPartitionID) || ' ) f 
       on f.History_id = s.History_id ) V';
    open P_Cursor for V_Sql;    
end;     

-- 获取包含特征参数值的趋势数据   
-- P_PointID 测点编号
--P_MinPartitionID输入参数： 最小分区编号
--P_MaxPartitionID输入参数： 最大分区编号
-- P_ChannelNumber 通道号
-- P_RecordCount    最多返回记录数
-- P_Count 实际记录条数
-- P_Cursor 输出参数：返回历史特征数据      
procedure Pr_GetAllFeatTrendsByChnNr( 
            P_PointID         In    pnt_point.point_id%type,      
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,
            P_ChannelNumber  In    zx_history_featurevalue.chnno_nr%type,
            P_RecordCount    In    BS_MetaFieldType.Int_NR%type,
            P_QueryCountFlag In BS_MetaFieldType.Int_NR%type,
            P_Count          Out    BS_MetaFieldType.Int_NR%type, P_Cursor        out   T_Cursor)
as
    V_TableFeatBaseName BS_MetaFieldType.Fifty_TX%type;
    V_TableSummBaseName BS_MetaFieldType.Fifty_TX%type;
    V_QueryFeatureSql BS_MetaFieldType.OneKilo_TX%type;
    V_QuerySummarySql BS_MetaFieldType.FiveKilo_TX%type;
    V_Sql         BS_MetaFieldType.FiveKilo_TX%type;
    V_SqlQueryCount        BS_MetaFieldType.FiveKilo_TX%type;
begin
    V_TableFeatBaseName := 'Zx_History_FeatureValue';
    V_TableSummBaseName := 'Zx_History_Summary';
    V_QueryFeatureSql := Commonpackage.F_GetPartitionSQL(P_MinPartitionID, P_MaxPartitionID, V_TableFeatBaseName);
    V_QuerySummarySql := Commonpackage.F_GetPartitionSQL(P_MinPartitionID, P_MaxPartitionID, V_TableSummBaseName);
    V_SqlQueryCount := ' Select Count(history_id) From  ' || V_QuerySummarySql ||
      ' Where Point_ID = ' || DBDiffPackage.IntToString(P_PointID) ||  
      ' And Partition_ID <= ' || DBDiffPackage.IntToString(P_MaxPartitionID) || ' And Partition_ID >= ' || DBDiffPackage.IntToString(P_MinPartitionID);
    
    -- 查询P_PageIndex为-1的情况下，返回总记录数
	if P_QueryCountFlag = -1 then
		execute immediate  V_SqlQueryCount into P_Count;
		open P_Cursor for select * from dual where 1 <> 1;
		return;
	end if;
    V_Sql := 'Select 
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
                  From ' || V_QuerySummarySql ||
      ' Where Point_ID = ' || DBDiffPackage.IntToString(P_PointID) ||  
      ' And Partition_ID <= ' || DBDiffPackage.IntToString(P_MaxPartitionID) || ' And Partition_ID >= ' || DBDiffPackage.IntToString(P_MinPartitionID);
    V_Sql := DBDiffPackage.GetPagingSQL(V_Sql, 0, P_RecordCount,'Partition_ID Desc');
    V_Sql := 
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
         From ( ' ||
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
             From ( '|| V_Sql ||' ) s ' || ' join ( ' || 
      ' Select 
            FeatureValue_NR,
            FeatureValue_ID,
            History_ID          
       From ' 
      || V_QueryFeatureSql ||
      ' Where chnno_nr = ' || DBDiffPackage.IntToString(P_ChannelNumber) || 
      ' And Partition_ID <= ' || DBDiffPackage.IntToString(P_MaxPartitionID) || ' And Partition_ID >= ' || DBDiffPackage.IntToString(P_MinPartitionID) || ' ) f 
       on f.History_id = s.History_id ) V';
    open P_Cursor for V_Sql;   
end;      

-- 获取趋势数据   
-- P_PointID 测点编号
--P_MinPartitionID输入参数： 最小分区编号
--P_MaxPartitionID输入参数： 最大分区编号
-- P_LowerSpeed 转速下限
-- P_UpperSpeed 转速上限
-- P_ChannelNumber 通道号
-- P_FeatureValueID 特征指标编号
-- P_Cursor 输出参数：返回历史特征数据      
procedure Pr_GetTrendDatasByChnNSpeed( 
            P_PointID         In    pnt_point.point_id%type,      
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,
            P_LowerSpeed    In    BS_MetaFieldType.Int_NR%type,
            P_UpperSpeed    In    BS_MetaFieldType.Int_NR%type,
            P_ChannelNumber  In    zx_history_featurevalue.chnno_nr%type,
            P_FeatureValueID In    zx_history_featurevalue.featurevalue_id%type, P_Cursor        out   T_Cursor)
as
    V_TableFeatBaseName BS_MetaFieldType.Fifty_TX%type;
    V_TableSummBaseName BS_MetaFieldType.Fifty_TX%type;
    V_QueryFeatureSql BS_MetaFieldType.OneKilo_TX%type;
    V_QuerySummarySql BS_MetaFieldType.FiveKilo_TX%type;
    V_Sql         BS_MetaFieldType.FiveKilo_TX%type;
begin
    V_TableFeatBaseName := 'Zx_History_FeatureValue';
    V_TableSummBaseName := 'Zx_History_Summary';
    V_QueryFeatureSql := Commonpackage.F_GetPartitionSQL(P_MinPartitionID, P_MaxPartitionID, V_TableFeatBaseName);
    V_QuerySummarySql := Commonpackage.F_GetPartitionSQL(P_MinPartitionID, P_MaxPartitionID, V_TableSummBaseName);
    V_Sql := 
      ' Select 
         PartitionID,
         CompressID,
         SampleTime,
         DataLength,
         DataId,
         MeasurementValue,
         Point_ID,
         Rev         
         From ( ' ||
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
                  From  ' || V_QuerySummarySql ||
      ' Where Point_ID = ' || DBDiffPackage.IntToString(P_PointID) || ' And RotSpeed_NR >= ' || DBDiffPackage.IntToString(P_LowerSpeed) || ' And RotSpeed_NR <=' || DBDiffPackage.IntToString(P_UpperSpeed) || 
      ' And Partition_ID <= ' || DBDiffPackage.IntToString(P_MaxPartitionID) || ' And Partition_ID >= ' || DBDiffPackage.IntToString(P_MinPartitionID) || ' ) s ' || ' join ( ' || 
      ' Select 
            FeatureValue_NR,
            History_ID          
       From ' 
      || V_QueryFeatureSql ||
      ' Where chnno_nr = ' || DBDiffPackage.IntToString(P_ChannelNumber) || ' And featurevalue_id = ' || DBDiffPackage.IntToString(P_FeatureValueID) || 
      ' And Partition_ID <= ' || DBDiffPackage.IntToString(P_MaxPartitionID) || ' And Partition_ID >= ' || DBDiffPackage.IntToString(P_MinPartitionID) || ' ) f 
       on f.History_id = s.History_id ) V';
    open P_Cursor for V_Sql;    
end;      

-- 获取趋势数据   
-- P_PointID 测点编号
--P_MinPartitionID输入参数： 最小分区编号
--P_MaxPartitionID输入参数： 最大分区编号
-- P_ChannelNumber 通道号
-- P_FeatureValueID 特征指标编号
-- P_Cursor 输出参数：返回历史特征数据      
procedure Pr_GetTrendDatasByChnNr( 
            P_PointID         In    pnt_point.point_id%type,      
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,
            P_ChannelNumber  In    zx_history_featurevalue.chnno_nr%type,
            P_FeatureValueID In    zx_history_featurevalue.featurevalue_id%type, P_Cursor        out   T_Cursor)
as
    V_TableFeatBaseName BS_MetaFieldType.Fifty_TX%type;
    V_TableSummBaseName BS_MetaFieldType.Fifty_TX%type;
    V_QueryFeatureSql BS_MetaFieldType.OneKilo_TX%type;
    V_QuerySummarySql BS_MetaFieldType.FiveKilo_TX%type;
    V_Sql         BS_MetaFieldType.FiveKilo_TX%type;
begin
    V_TableFeatBaseName := 'Zx_History_FeatureValue';
    V_TableSummBaseName := 'Zx_History_Summary';
    V_QueryFeatureSql := Commonpackage.F_GetPartitionSQL(P_MinPartitionID, P_MaxPartitionID, V_TableFeatBaseName);
    V_QuerySummarySql := Commonpackage.F_GetPartitionSQL(P_MinPartitionID, P_MaxPartitionID, V_TableSummBaseName);
    V_Sql := 
      ' Select 
         PartitionID,
         CompressID,
         SampleTime,
         DataLength,
         DataId,
         MeasurementValue,
         Point_ID,
         Rev         
         From ( ' ||
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
                  From  ' || V_QuerySummarySql ||
      ' Where Point_ID = ' || DBDiffPackage.IntToString(P_PointID) ||  
      ' And Partition_ID <= ' || DBDiffPackage.IntToString(P_MaxPartitionID) || ' And Partition_ID >= ' || DBDiffPackage.IntToString(P_MinPartitionID) || ' ) s ' || ' join ( ' || 
      ' Select 
            FeatureValue_NR,
            History_ID          
       From ' 
      || V_QueryFeatureSql ||
      ' Where chnno_nr = ' || DBDiffPackage.IntToString(P_ChannelNumber) || ' And featurevalue_id = ' || DBDiffPackage.IntToString(P_FeatureValueID) || 
      ' And Partition_ID <= ' || DBDiffPackage.IntToString(P_MaxPartitionID) || ' And Partition_ID >= ' || DBDiffPackage.IntToString(P_MinPartitionID) || ' ) f 
       on f.History_id = s.History_id ) V';
    open P_Cursor for V_Sql;   
end;                           

-- 获取设备的工况数据   
-- P_MobjectID 设备编号
-- P_MinPartitionID输入参数： 最小分区编号
-- P_MaxPartitionID输入参数： 最大分区编号
-- P_PageSize 分页大小
-- P_PageIndex 页码
-- P_Count 实际记录条数
-- P_Cursor 输出参数：返回设备的工况数据    
procedure Pr_GetMObjectWorkingDatas( 
			P_MobjectID		in		Mob_MObject.Mobject_ID%type, 
            P_MinPartitionID    in		ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    in		ZX_History_Summary.Partition_ID%type,
            P_PageSize		in		BS_MetaFieldType.Int_NR%type,
			P_PageIndex in		BS_MetaFieldType.Int_NR%type,
			P_Count out		BS_MetaFieldType.Int_NR%type, P_Cursor            out		T_Cursor)
as 
    V_TableSummBaseName BS_MetaFieldType.Fifty_TX%type;
	V_TableFeatBaseName BS_MetaFieldType.Fifty_TX%type;
	V_QuerySummarySql BS_MetaFieldType.FiveKilo_TX%type;
    V_QueryFeatureSql BS_MetaFieldType.FiveKilo_TX%type;
	V_Summary BS_MetaFieldType.Fifty_TX%type;
	V_FeatureValue BS_MetaFieldType.Fifty_TX%type;
    V_Sql BS_MetaFieldType.FiveKilo_TX%type;
    V_SqlQueryCount BS_MetaFieldType.FiveKilo_TX%type;
	V_PageIndex BS_MetaFieldType.Int_NR%type;
	V_ParentList BS_MetaFieldType.TwoHundred_TX%type;
begin
	V_TableSummBaseName :=  'Zx_History_Summary';
    V_TableFeatBaseName :=  'Zx_History_FeatureValue';
	V_Summary :=  V_TableSummBaseName || 'His';
	V_FeatureValue :=  V_TableFeatBaseName || 'His';
	V_QuerySummarySql :=  Commonpackage.F_GetPartitionSQL(P_MinPartitionID, P_MaxPartitionID, V_TableSummBaseName);
    V_QueryFeatureSql :=  Commonpackage.F_GetPartitionSQL(P_MinPartitionID, P_MaxPartitionID, V_TableFeatBaseName);

	V_Sql :=  
		' select p.Point_ID from pnt_point p ' || 
			' inner join ( select * from mob_mobject mob where mob.active_yn = ''1'' ) m ' || 
				' inner join mob_mobjectstructure ms on m.mobject_id = ms.mobject_id ' || 
			' on p.mobject_id = m.mobject_id ' || 
			' inner join ( select dv.*, pdv.point_id from pnt_datavar dv inner join pnt_pntdatavar pdv on dv.var_id = pdv.var_id ) v on p.point_id = v.point_id ' || 
			' where p.pointname_tx is not null';

	select parentlist_tx into V_ParentList  from mob_mobjectstructure where mobject_id = P_MobjectID;
	if V_ParentList is not null and DBDiffPackage.GetLength(V_ParentList) > 0 then
	  V_Sql := V_Sql || ' and ms.parentlist_tx like ''' || V_ParentList || '%''';
	else
	  V_Sql := V_Sql || ' and m.mobject_id = ' || DBDiffPackage.IntToString(P_MobjectID);
	end if;

    V_Sql := 
		' select hs.*, hf.FeatureValue_NR from ' || 
			' ( select * from ' || V_QuerySummarySql || 
				' where partition_id <= ' || DBDiffPackage.IntToString(P_MaxPartitionID) || ' and ' || 
					' partition_id >= ' || DBDiffPackage.IntToString(P_MinPartitionID) || ' and ' || 
					' point_id in ( ' || V_Sql || ' ) ) hs ' || 
		' left join ' || 
			' ( select * from ' || V_QueryFeatureSql || ' where featurevaluetype_id = 0 and chnno_nr = 1 and featurevalue_id = 10 ) hf ' || 
		' on hs.history_id = hf.history_id';

	V_Sql := 
		'select Partition_ID, History_ID, Point_ID, SampTime_DT, FeatureValue_NR, Result_TX, ' || 
			'DatType_NR, SigType_NR, EngUnit_ID, Compress_ID, DatLen_NR, Alm_ID, AlmLevel_ID, RotSpeed_NR from ( ' || V_Sql || ' ) V';

	DBDiffPackage.Pr_DebugPrint(V_Sql);
	
	-- 查询P_PageIndex为-1的情况下，返回总记录数
    if P_PageIndex = -1 then
		V_SqlQueryCount := 'select count(*) from (' || V_Sql || ') V';
		execute immediate V_SqlQueryCount into P_Count;
		open P_Cursor for select * from dual where 1 <> 1;
		return;
	end if;
	
	-- 排序条件：按照分区号降序排列
	if P_PageSize = -1 then
		-- 查询所有数据
		V_Sql := V_Sql || ' order by partition_id desc';
	else
		-- 查询指定页、指定行数的数据
		V_Sql := DBDiffPackage.GetPagingSQL(V_Sql, P_PageIndex, P_PageSize, 'partition_id desc');
	end if;
    open P_Cursor for V_Sql;
    return;
end;

-- 获取设备的工况数据测点    
-- P_MobjectID 设备编号
-- P_PageSize 分页大小
-- P_PageIndex 页码
-- P_Count 实际记录条数
-- P_Cursor 输出参数：返回设备的工况数据测点    
procedure Pr_GetMObjectWorkingDataPoints( 
			P_MobjectID		in		Mob_MObject.Mobject_ID%type, 
            P_PageSize		in		BS_MetaFieldType.Int_NR%type,
			P_PageIndex in		BS_MetaFieldType.Int_NR%type,
			P_Count out		BS_MetaFieldType.Int_NR%type, P_Cursor            out		T_Cursor)
as 
    V_Sql BS_MetaFieldType.FiveKilo_TX%type;
    V_SqlQueryCount BS_MetaFieldType.FiveKilo_TX%type;
	V_PageIndex BS_MetaFieldType.Int_NR%type;
	V_ParentList BS_MetaFieldType.TwoHundred_TX%type;
begin

	V_Sql :=  
		' select m.Mobject_ID, ms.ParentList_TX, p.Point_ID, p.PointName_TX, v.Var_ID, v.VarName_TX, p.DatType_ID as DatType_NR, p.SigType_ID as SigType_NR, p.EngUnit_ID as PointEngUnit_ID, eng.NameE_TX as EngUnitName_TX from Pnt_Point p ' ||  
			' inner join ( select * from mob_mobject mob where mob.active_yn = ''1'' ) m  ' || 
				' inner join mob_mobjectstructure ms on m.mobject_id = ms.mobject_id ' || 
			' on p.mobject_id = m.mobject_id ' || 
			' inner join ( select dv.*, pdv.point_id from pnt_datavar dv inner join pnt_pntdatavar pdv on dv.var_id = pdv.var_id ) v on p.point_id = v.point_id ' || 
			' inner join z_engunit eng on p.engunit_id = eng.engunit_id ' || 
			' where p.pointname_tx is not null ';

	select parentlist_tx into V_ParentList  from mob_mobjectstructure where mobject_id = P_MobjectID;
	if V_ParentList is not null and DBDiffPackage.GetLength(V_ParentList) > 0 then
	  V_Sql := V_Sql || ' and ms.parentlist_tx like ''' || V_ParentList || '%''';
	else
	  V_Sql := V_Sql || ' and m.mobject_id = ' || DBDiffPackage.IntToString(P_MobjectID);
	end if;

	DBDiffPackage.Pr_DebugPrint(V_Sql);
	
	-- 查询P_PageIndex为-1的情况下，返回总记录数
    if P_PageIndex = -1 then
		V_SqlQueryCount := 'select count(*) from (' || V_Sql || ') V';
		execute immediate V_SqlQueryCount into P_Count;
		open P_Cursor for select * from dual where 1 <> 1;
		return;
	end if;
	
	-- 排序条件：按照分区号降序排列
	if P_PageSize = -1 then
		-- 查询所有数据
		V_Sql := V_Sql || ' order by ParentList_TX, PointName_TX';
	else
		-- 查询指定页、指定行数的数据
		V_Sql := DBDiffPackage.GetPagingSQL(V_Sql, P_PageIndex, P_PageSize, 'ParentList_TX, PointName_TX');
	end if;
    open P_Cursor for V_Sql;
    return;
end;

-- 获取报警等级对应的数量   
-- P_AlmLevelID 报警等级
-- P_Cursor 输出参数：返回报警等级对应的数量  
procedure Pr_GetHistoryAlmCount( 
			P_AlmLevelID In ZX_History_Alm.Almlevel_Id%type, 
			P_Count out		BS_MetaFieldType.Int_NR%type, P_Cursor            out		T_Cursor)as 
begin

	select count(*) into P_Count from ZX_History_Alm ha 
		inner join ZT_MobjectWarning mw on ha.Alm_ID = mw.MobjectWarning_ID 
		where ha.AlmLevel_ID = P_AlmLevelID and mw.Close_YN <> '1';

	open P_Cursor for select * from dual where 1 <> 1;
    return;
end;

End HistoryDataPackage;
/
