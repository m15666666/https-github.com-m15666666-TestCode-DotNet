CREATE OR REPLACE Package ZXSamplePackage As

 --获取测点维数专用
Function F_GetPntDim(
         P_PntDim BS_MetaFieldType.Int_NR%type
         ) Return BS_MetaFieldType.Fifty_TX%type;

--获取测点方向专用
Function F_GetPntDirect(
         P_PntDirect BS_MetaFieldType.Int_NR%type
         ) Return BS_MetaFieldType.Fifty_TX%type;

--获取测点旋转方向专用
Function F_GetPntRotation(
         P_PntRotation BS_MetaFieldType.Int_NR%type
         ) Return BS_MetaFieldType.Fifty_TX%type;

--获取测点存储模式专用
Function F_GetStoreType(
         P_PntStoreType BS_MetaFieldType.Int_NR%type
         ) Return BS_MetaFieldType.Fifty_TX%type;

--获取通道类型专用
Function F_GetChannelType(
         P_ChannelType BS_MetaFieldType.Int_NR%type
         )  Return BS_MetaFieldType.Fifty_TX%type;                                                      


--插入在线报警数据
--P_PartitionID 分区键值
--P_AlmID 报警ID 
--P_FeatureValueID 报警来源
--P_PointID 测点ID
--P_AlmDT 报警时间
--P_AlmLevelID 报警等级
--P_AlmDescTX  报警描述
--p_MobjectID 设备ID
-- P_OwnerPostID 设备点检分工ID
--P_MobSpecID    设备分类ID
--P_UserID 用户ID
procedure InsertAlmRecord(
          P_PartitionID In ZX_History_Alm.Partition_ID%type,
          P_AlmID In ZX_History_Alm.Alm_Id%type,
          P_FeatureValueID In ZX_History_Alm.FeatureValue_ID%type,
          P_PointID In ZX_History_Alm.Point_ID%type,
          P_AlmDT In ZX_History_Alm.Alm_Dt%type, 
          P_AlmLevelID In ZX_History_Alm.Almlevel_Id%type, 
          P_AlmDescTX In ZX_History_Alm.Almdesc_Tx%type,
          P_MobjectID In mob_mobject.mobject_id%type, 
          P_OwnerPostID In Mob_Mobject.Djowner_Id%type, 
          P_MobSpecID In Mob_Mobject.Spec_Id%type, 
          P_UserID In Bs_Appuser.Appuser_Id%type);
                            
End ZXSamplePackage;
/

CREATE OR REPLACE Package Body ZXSamplePackage As

--获取测点维数专用
Function F_GetPntDim(
         P_PntDim BS_MetaFieldType.Int_NR%type
         ) Return BS_MetaFieldType.Fifty_TX%type
as
Begin
if P_PntDim = 1 then
  return '一维';
elsif P_PntDim = 2 then
  return '二维';
end if;
return '';
End;

--获取测点方向专用
Function F_GetPntDirect(
         P_PntDirect BS_MetaFieldType.Int_NR%type
         ) Return BS_MetaFieldType.Fifty_TX%type
as
Begin
if P_PntDirect = 0 then
  return '水平';
elsif P_PntDirect = 1 then
  return '垂直';
elsif P_PntDirect = 2 then
  return '轴向';
elsif P_PntDirect = 3 then
  return '45度';
elsif P_PntDirect = 4 then
  return '135度';
elsif P_PntDirect = 99 then
  return '';
end if;
return '';
End;

--获取测点旋转方向专用
Function F_GetPntRotation(
         P_PntRotation BS_MetaFieldType.Int_NR%type
         ) Return BS_MetaFieldType.Fifty_TX%type
as
Begin
if P_PntRotation = 0 then
  return '逆时针';
elsif P_PntRotation = 1 then
  return '顺时针';
elsif P_PntRotation = 99 then
  return '';
end if;
return '';
End;

--获取测点存储模式专用
Function F_GetStoreType(
         P_PntStoreType BS_MetaFieldType.Int_NR%type
         ) Return BS_MetaFieldType.Fifty_TX%type
as
Begin
if P_PntStoreType = 0 then
  return '报警存储模式';
elsif P_PntStoreType = 1 then
  return '常规存储模式';
end if;
return '';
End;

--获取通道类型专用
Function F_GetChannelType(
         P_ChannelType BS_MetaFieldType.Int_NR%type
         ) Return BS_MetaFieldType.Fifty_TX%type
as
Begin
if P_ChannelType = 1 then
  return '波形';
elsif P_ChannelType = 2 then
  return '数值';
elsif P_ChannelType = 3 then
  return '转速';
elsif P_ChannelType = 5 then
  return '开关量';
end if;
return '';
End;

--插入在线报警数据
--P_PartitionID 分区键值
--P_AlmID 报警ID 
--P_FeatureValueID 报警来源
--P_PointID 测点ID
--P_AlmDT 报警时间
--P_AlmLevelID 报警等级
--P_AlmDescTX  报警描述
--p_MobjectID 设备ID
-- P_OwnerPostID 设备点检分工ID
--P_MobSpecID    设备分类ID
--P_UserID 用户ID
procedure InsertAlmRecord(
          P_PartitionID In ZX_History_Alm.Partition_ID%type,
          P_AlmID In ZX_History_Alm.Alm_Id%type,
          P_FeatureValueID In ZX_History_Alm.FeatureValue_ID%type,
          P_PointID In ZX_History_Alm.Point_ID%type,
          P_AlmDT In ZX_History_Alm.Alm_Dt%type, 
          P_AlmLevelID In ZX_History_Alm.Almlevel_Id%type, 
          P_AlmDescTX In ZX_History_Alm.Almdesc_Tx%type,
          P_MobjectID In mob_mobject.mobject_id%type, 
          P_OwnerPostID In Mob_Mobject.Djowner_Id%type, 
          P_MobSpecID In Mob_Mobject.Spec_Id%type, 
          P_UserID In Bs_Appuser.Appuser_Id%type) as          
  V_WarningCD varchar2(60); 
  V_Count BS_MetaFieldType.Int_NR%type;
  V_MobjectWarningID ZX_History_Alm.Alm_Id%type;
  begin
	V_Count := 0;

	begin

	-- 只有对传感器失效和传感器电池电量低才会对报警进行判断计数，其他的有则插入。
	select count(*) into V_Count from ZX_History_Alm ha 
		inner join ZT_MobjectWarning mw on ha.Alm_ID = mw.MobjectWarning_ID 
		where ha.Point_ID = P_PointID and ha.FeatureValue_ID = P_FeatureValueID 
		--2016-04-14 电量等报警计数
		and ha.FeatureValue_ID > 10000 
			and ha.AlmLevel_ID = P_AlmLevelID and mw.Close_YN <> '1';

	if V_Count = 0 then
		ecmsCommonPackage.Pr_GetSNCode('WarningDealWithMM','ZT_MobjectWarning','Warning_CD',
        P_AlmDT,P_UserID,0,P_MobSpecID,p_MobjectID,V_WarningCD);
        Insert Into ZT_MobjectWarning(MobjectWarning_ID, Warning_CD, Mobject_ID, DJOwner_ID, AlmRecType_CD, AlmLevel_ID, Spec_ID,
                    Content_TX,DutyPost_ID,CheckUser_ID,CheckDate_DT,WarningNum_NR,DealWithType_TX,Close_YN,FeatureValue_ID)
                    Values(P_AlmID, V_WarningCD, P_MobjectID, P_OwnerPostID, '2', P_AlmLevelID, P_MobSpecID,
                    P_AlmDescTX, P_OwnerPostID, P_UserID, P_AlmDT, '1','0','0',P_FeatureValueID);
        Insert Into ZX_History_Alm(Partition_ID,Alm_ID,FeatureValue_ID,AlmLevel_ID,Alm_DT,MObject_ID,Point_ID,AlmDesc_TX,OwnerUser_ID)
                    Values(P_PartitionID,P_AlmID, P_FeatureValueID, P_AlmLevelID, P_AlmDT, P_MobjectID, P_PointID, P_AlmDescTX,P_OwnerPostID);
	else
		select Alm_ID into V_MobjectWarningID from 
			(select * from ZX_History_Alm ha 
				inner join ZT_MobjectWarning mw on ha.Alm_ID = mw.mobjectwarning_id 
				where ha.Point_ID = P_PointID and ha.FeatureValue_ID = P_FeatureValueID and ha.AlmLevel_ID = P_AlmLevelID and mw.Close_YN <> '1' 
				order by ha.Alm_DT desc) where rownum = 1;
		update ZT_MobjectWarning set WarningNum_NR = WarningNum_NR + 1,
		--2016-04-14 电量等报警计数
		CheckDate_DT = P_AlmDT,
		DJOwner_ID = P_OwnerPostID,DutyPost_ID = P_OwnerPostID,CheckUser_ID = P_UserID where MobjectWarning_ID = V_MobjectWarningID And Close_YN <> '1';
		update ZX_History_Alm set Alm_DT = P_AlmDT,OwnerUser_ID = P_OwnerPostID where Alm_ID = V_MobjectWarningID;
	end if;

	EXCEPTION
          WHEN OTHERS THEN
			Rollback;
            return;
      end;
  commit;
end;

End ZXSamplePackage;

/
