CREATE OR REPLACE Package HistoryDataPackage As
Type T_Cursor Is Ref Cursor;

--��ȡ��ʷ���ݱ�Ŷ�Ӧ�ķ��������
Function F_GetPartitionIDByHistoryID(
       P_HistoryID in zx_history_summary.history_id%type)return BS_MetaFieldType.BigInt_NR%type;

--��ȡ��ʷժҪ����
--P_HistoryID�� ��ʷ���ݱ��
--P_Cursor���������������ʷժҪ����
procedure Pr_GetSummaryByKey
          ( P_HistoryID         In    zx_history_summary.history_id%type, P_Cursor          out   T_Cursor);
		  
--��ȡ��ͬͬ���ŵ���ʷժҪ����
--P_SyncNR: ͬ����
--P_HistoryID�� ��ʷ���ݱ��
--P_Cursor���������������ʷժҪ����
procedure Pr_GetSummaryBySyncNR
          ( P_SyncNR         In    zx_history_summary.synch_nr%type, 
			P_HistoryID         In    zx_history_summary.history_id%type,
			P_Cursor          out   T_Cursor);
            
--ɾ����ʷժҪ����
--P_HistoryID�� ��ʷ���ݱ��
--P_Emsg: OUTPUT ������Ϣ
procedure Pr_DeleteSummaryByKey( 
            P_HistoryID  In    ZX_History_Summary.history_id%type,
            P_Emsg Out BS_MetaFieldType.Emsg_TX%type);  
            
-- ��ȡ����µ���ʷժҪ����            
-- P_PointID �����
--P_MinPartitionID��������� ��С�������
--P_MaxPartitionID��������� ���������
-- P_Cursor ���������������ʷժҪ�����б�
procedure Pr_GetSummaryListByPoint(
            P_PointID         In    pnt_point.point_id%type,      
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type, P_Cursor        out   T_Cursor);   
            
           
-- ��ȡ�����ָ��ת�ٷ�Χ����ʷժҪ����            
-- P_PointID �����
--P_MinPartitionID��������� ��С�������
--P_MaxPartitionID��������� ���������
-- P_LowerSpeed ת������
-- P_UpperSpeed ת������
-- P_Cursor ���������������ʷժҪ�����б�
procedure Pr_GetSummaryByPointAndSpeed(
            P_PointID         In    pnt_point.point_id%type,      
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,
            P_LowerSpeed    In    BS_MetaFieldType.Int_NR%type,
            P_UpperSpeed    In    BS_MetaFieldType.Int_NR%type,  P_Cursor        out   T_Cursor);    
            
--��ȡ��ʷ��������
--P_HistoryID�� ��ʷ���ݱ��
--P_ChannelNumber: ͨ����
-- P_FeatureValueID ����ָ����
--P_Cursor���������������ʷ��������
procedure Pr_GetFeatureValueByKey
          ( P_HistoryID         In    zx_history_summary.history_id%type,
            P_ChannelNumber     In    zx_history_waveform.chnno_nr%type,
            P_FeatureValueID In    zx_history_featurevalue.featurevalue_id%type, P_Cursor          out   T_Cursor);                          
            
            
-- ��ȡָ��ָ�����ʷ��������   
--P_MinPartitionID��������� ��С�������
--P_MaxPartitionID��������� ���������
-- P_ChannelNumber ͨ����
-- P_FeatureValueID ����ָ����
-- P_Cursor �������������ָ��ָ�����ʷ��������  
procedure Pr_GetFeaturesByChnNrAndFeat(
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,
            P_ChannelNumber  In    zx_history_featurevalue.chnno_nr%type,
            P_FeatureValueID In    zx_history_featurevalue.featurevalue_id%type, P_Cursor        out   T_Cursor); 
            
-- ��ȡ��ʷ�������� 
--P_MinPartitionID��������� ��С�������
--P_MaxPartitionID��������� ���������
-- P_ChannelNumber ͨ����
-- P_Cursor ���������������ʷ��������      
procedure Pr_GetFeaturesByChannelNr(
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,
            P_ChannelNumber  In    zx_history_featurevalue.chnno_nr%type, P_Cursor        out   T_Cursor);                       
            
-- ���ָ������Ƿ������ʷժҪ����        
-- P_PointID �����
--P_MinPartitionID��������� ��С�������
--P_MaxPartitionID��������� ���������
-- P_HasResult ���������1-����ժҪ���ݣ�0-������
procedure Pr_CheckPointHistories(
            P_PointID         In    pnt_point.point_id%type,
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,       
            P_HasResult out BS_MetaFieldType.Int_NR%type);   
            
--��ȡ��ʷ��������
--P_HistoryID�� ��ʷ���ݱ��
--P_ChannelNumber: ͨ����
--P_Cursor���������������ʷժҪ����
procedure Pr_GetWaveformByKey
          ( P_HistoryID         In    zx_history_summary.history_id%type,
            P_Cursor          out   T_Cursor);   
            
--��ȡ��ʷƵ�ײ�������
--P_HistoryID�� ��ʷ���ݱ��
--P_Cursor���������������ʷժҪ����
procedure Pr_GetFreqWaveformByKey
          ( P_HistoryID         In    zx_history_summary.history_id%type, P_Cursor          out   T_Cursor);            
            
-- ��ȡ������������ֵ����������   
-- P_PointID �����
--P_MinPartitionID��������� ��С�������
--P_MaxPartitionID��������� ���������
-- P_LowerSpeed ת������
-- P_UpperSpeed ת������
-- P_ChannelNumber ͨ����
-- P_RecordCount    ��෵�ؼ�¼��
-- P_Count ʵ�ʼ�¼����
-- P_Cursor ���������������ʷ��������      
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
            
-- ��ȡ������������ֵ����������   
-- P_PointID �����
--P_MinPartitionID��������� ��С�������
--P_MaxPartitionID��������� ���������
-- P_ChannelNumber ͨ����
-- P_RecordCount    ��෵�ؼ�¼��
-- P_Count ʵ�ʼ�¼����
-- P_Cursor ���������������ʷ��������      
procedure Pr_GetAllFeatTrendsByChnNr( 
            P_PointID         In    pnt_point.point_id%type,      
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,
            P_ChannelNumber  In    zx_history_featurevalue.chnno_nr%type,
            P_RecordCount    In    BS_MetaFieldType.Int_NR%type,
            P_QueryCountFlag In BS_MetaFieldType.Int_NR%type,
            P_Count          Out BS_MetaFieldType.Int_NR%type, P_Cursor        out   T_Cursor); 
            
-- ��ȡ��������   
-- P_PointID �����
--P_MinPartitionID��������� ��С�������
--P_MaxPartitionID��������� ���������
-- P_LowerSpeed ת������
-- P_UpperSpeed ת������
-- P_ChannelNumber ͨ����
-- P_FeatureValueID ����ָ����
-- P_Cursor ���������������ʷ��������      
procedure Pr_GetTrendDatasByChnNSpeed( 
            P_PointID         In    pnt_point.point_id%type,      
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,
            P_LowerSpeed    In    BS_MetaFieldType.Int_NR%type,
            P_UpperSpeed    In    BS_MetaFieldType.Int_NR%type,
            P_ChannelNumber  In    zx_history_featurevalue.chnno_nr%type,
            P_FeatureValueID In    zx_history_featurevalue.featurevalue_id%type, P_Cursor        out   T_Cursor);   
            
-- ��ȡ��������   
-- P_PointID �����
--P_MinPartitionID��������� ��С�������
--P_MaxPartitionID��������� ���������
-- P_ChannelNumber ͨ����
-- P_FeatureValueID ����ָ����
-- P_Cursor ���������������ʷ��������      
procedure Pr_GetTrendDatasByChnNr( 
            P_PointID         In    pnt_point.point_id%type,      
            P_MinPartitionID  In    ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    In    ZX_History_Summary.Partition_ID%type,
            P_ChannelNumber  In    zx_history_featurevalue.chnno_nr%type,
            P_FeatureValueID In    zx_history_featurevalue.featurevalue_id%type, P_Cursor        out   T_Cursor);                                                                       
			
-- ��ȡ�豸�Ĺ�������   
-- P_MobjectID �豸���
-- P_MinPartitionID��������� ��С�������
-- P_MaxPartitionID��������� ���������
-- P_PageSize ��ҳ��С
-- P_PageIndex ҳ��
-- P_Count ʵ�ʼ�¼����
-- P_Cursor ��������������豸�Ĺ�������    
procedure Pr_GetMObjectWorkingDatas( 
			P_MobjectID		in		Mob_MObject.Mobject_ID%type, 
            P_MinPartitionID    in		ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    in		ZX_History_Summary.Partition_ID%type,
            P_PageSize		in		BS_MetaFieldType.Int_NR%type,
			P_PageIndex in		BS_MetaFieldType.Int_NR%type,
			P_Count out		BS_MetaFieldType.Int_NR%type, P_Cursor            out		T_Cursor);

-- ��ȡ�豸�Ĺ������ݲ��   
-- P_MobjectID �豸���
-- P_PageSize ��ҳ��С
-- P_PageIndex ҳ��
-- P_Count ʵ�ʼ�¼����
-- P_Cursor ��������������豸�Ĺ������ݲ��    
procedure Pr_GetMObjectWorkingDataPoints( 
			P_MobjectID		in		Mob_MObject.Mobject_ID%type, 
            P_PageSize		in		BS_MetaFieldType.Int_NR%type,
			P_PageIndex in		BS_MetaFieldType.Int_NR%type,
			P_Count out		BS_MetaFieldType.Int_NR%type, P_Cursor            out		T_Cursor);

-- ��ȡ�����ȼ���Ӧ������   
-- P_AlmLevelID �����ȼ�
-- P_Cursor ������������ر����ȼ���Ӧ������  
procedure Pr_GetHistoryAlmCount( 
			P_AlmLevelID In ZX_History_Alm.Almlevel_Id%type, 
			P_Count out		BS_MetaFieldType.Int_NR%type, P_Cursor            out		T_Cursor);
			
End HistoryDataPackage;

/

CREATE OR REPLACE Package Body HistoryDataPackage As

--��ȡ��ʷ���ݱ�Ŷ�Ӧ�ķ��������
Function F_GetPartitionIDByHistoryID(
       P_HistoryID in zx_history_summary.history_id%type)return BS_MetaFieldType.BigInt_NR%type
As
V_PartitionID BS_MetaFieldType.BigInt_NR%type;
Begin
       Select Partition_ID Into V_PartitionID From ZX_History_DataMapping Where History_ID = P_HistoryID;
       return V_PartitionID;
End;


--��ȡ��ʷժҪ����
--P_HistoryID�� ��ʷ���ݱ��
--P_Cursor���������������ʷժҪ����
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

--��ȡ��ͬͬ���ŵ���ʷժҪ����
--P_SyncNR ͬ����
--P_Cursor���������������ʷժҪ����
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

--ɾ����ʷժҪ����
--P_HistoryID�� ��ʷ���ݱ��
--P_Emsg: OUTPUT ������Ϣ
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

-- ��ȡ����µ���ʷժҪ����            
-- P_PointID �����
--P_MinPartitionID��������� ��С�������
--P_MaxPartitionID��������� ���������
-- P_Cursor ���������������ʷժҪ�����б�
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
            
-- ��ȡ�����ָ��ת�ٷ�Χ����ʷժҪ����            
-- P_PointID �����
--P_MinPartitionID��������� ��С�������
--P_MaxPartitionID��������� ���������
-- P_LowerSpeed ת������
-- P_UpperSpeed ת������
-- P_Cursor ���������������ʷժҪ�����б�
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

-- ���ָ������Ƿ������ʷժҪ����        
-- P_PointID �����
--P_MinPartitionID��������� ��С�������
--P_MaxPartitionID��������� ���������
-- P_HasResult ���������1-����ժҪ���ݣ�0-������
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


--��ȡ��ʷ��������
--P_HistoryID�� ��ʷ���ݱ��
--P_Cursor���������������ʷժҪ����
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

--��ȡ��ʷƵ�ײ�������
--P_HistoryID�� ��ʷ���ݱ��
--P_Cursor���������������ʷժҪ����
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

--��ȡ��ʷ��������
--P_HistoryID�� ��ʷ���ݱ��
--P_ChannelNumber: ͨ����
-- P_FeatureValueID ����ָ����
--P_Cursor���������������ʷ��������
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
             
-- ��ȡָ��ָ�����ʷ��������    
--P_MinPartitionID��������� ��С�������
--P_MaxPartitionID��������� ���������
-- P_ChannelNumber ͨ����
-- P_FeatureValueID ����ָ����
-- P_Cursor �������������ָ��ָ�����ʷ��������  
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
            
-- ��ȡ��ʷ��������   
--P_MinPartitionID��������� ��С�������
--P_MaxPartitionID��������� ���������
-- P_ChannelNumber ͨ����
-- P_Cursor ���������������ʷ��������      
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

-- ��ȡ������������ֵ����������   
-- P_PointID �����
--P_MinPartitionID��������� ��С�������
--P_MaxPartitionID��������� ���������
-- P_LowerSpeed ת������
-- P_UpperSpeed ת������
-- P_ChannelNumber ͨ����
-- P_RecordCount    ��෵�ؼ�¼��
-- P_Count ʵ�ʼ�¼����
-- P_Cursor ���������������ʷ��������      
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
    
    -- ��ѯP_PageIndexΪ-1������£������ܼ�¼��
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

-- ��ȡ������������ֵ����������   
-- P_PointID �����
--P_MinPartitionID��������� ��С�������
--P_MaxPartitionID��������� ���������
-- P_ChannelNumber ͨ����
-- P_RecordCount    ��෵�ؼ�¼��
-- P_Count ʵ�ʼ�¼����
-- P_Cursor ���������������ʷ��������      
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
    
    -- ��ѯP_PageIndexΪ-1������£������ܼ�¼��
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

-- ��ȡ��������   
-- P_PointID �����
--P_MinPartitionID��������� ��С�������
--P_MaxPartitionID��������� ���������
-- P_LowerSpeed ת������
-- P_UpperSpeed ת������
-- P_ChannelNumber ͨ����
-- P_FeatureValueID ����ָ����
-- P_Cursor ���������������ʷ��������      
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

-- ��ȡ��������   
-- P_PointID �����
--P_MinPartitionID��������� ��С�������
--P_MaxPartitionID��������� ���������
-- P_ChannelNumber ͨ����
-- P_FeatureValueID ����ָ����
-- P_Cursor ���������������ʷ��������      
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

-- ��ȡ�豸�Ĺ�������   
-- P_MobjectID �豸���
-- P_MinPartitionID��������� ��С�������
-- P_MaxPartitionID��������� ���������
-- P_PageSize ��ҳ��С
-- P_PageIndex ҳ��
-- P_Count ʵ�ʼ�¼����
-- P_Cursor ��������������豸�Ĺ�������    
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
	
	-- ��ѯP_PageIndexΪ-1������£������ܼ�¼��
    if P_PageIndex = -1 then
		V_SqlQueryCount := 'select count(*) from (' || V_Sql || ') V';
		execute immediate V_SqlQueryCount into P_Count;
		open P_Cursor for select * from dual where 1 <> 1;
		return;
	end if;
	
	-- �������������շ����Ž�������
	if P_PageSize = -1 then
		-- ��ѯ��������
		V_Sql := V_Sql || ' order by partition_id desc';
	else
		-- ��ѯָ��ҳ��ָ������������
		V_Sql := DBDiffPackage.GetPagingSQL(V_Sql, P_PageIndex, P_PageSize, 'partition_id desc');
	end if;
    open P_Cursor for V_Sql;
    return;
end;

-- ��ȡ�豸�Ĺ������ݲ��    
-- P_MobjectID �豸���
-- P_PageSize ��ҳ��С
-- P_PageIndex ҳ��
-- P_Count ʵ�ʼ�¼����
-- P_Cursor ��������������豸�Ĺ������ݲ��    
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
	
	-- ��ѯP_PageIndexΪ-1������£������ܼ�¼��
    if P_PageIndex = -1 then
		V_SqlQueryCount := 'select count(*) from (' || V_Sql || ') V';
		execute immediate V_SqlQueryCount into P_Count;
		open P_Cursor for select * from dual where 1 <> 1;
		return;
	end if;
	
	-- �������������շ����Ž�������
	if P_PageSize = -1 then
		-- ��ѯ��������
		V_Sql := V_Sql || ' order by ParentList_TX, PointName_TX';
	else
		-- ��ѯָ��ҳ��ָ������������
		V_Sql := DBDiffPackage.GetPagingSQL(V_Sql, P_PageIndex, P_PageSize, 'ParentList_TX, PointName_TX');
	end if;
    open P_Cursor for V_Sql;
    return;
end;

-- ��ȡ�����ȼ���Ӧ������   
-- P_AlmLevelID �����ȼ�
-- P_Cursor ������������ر����ȼ���Ӧ������  
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
