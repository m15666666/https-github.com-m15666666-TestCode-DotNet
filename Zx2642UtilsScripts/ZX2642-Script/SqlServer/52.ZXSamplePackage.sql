IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ZXSamplePackage].[F_GetPntDim]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [ZXSamplePackage].[F_GetPntDim]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ZXSamplePackage].[F_GetPntDirect]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [ZXSamplePackage].[F_GetPntDirect]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ZXSamplePackage].[F_GetPntRotation]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [ZXSamplePackage].[F_GetPntRotation]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ZXSamplePackage].[F_GetStoreType]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [ZXSamplePackage].[F_GetStoreType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ZXSamplePackage].[F_GetChannelType]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [ZXSamplePackage].[F_GetChannelType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ZXSamplePackage].[InsertAlmRecord]') AND type in (N'P', N'PC'))
DROP PROCEDURE [ZXSamplePackage].[InsertAlmRecord]
GO

IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'ZXSamplePackage')
DROP SCHEMA ZXSamplePackage
GO


/****** ����:  Schema [ZXSamplePackage]    �ű�����: 04/01/2011 10:25:27 ******/
CREATE SCHEMA [ZXSamplePackage] AUTHORIZATION [dbo]
GO

--��ȡ���ά��ר��
create function ZXSamplePackage.F_GetPntDim(
         @P_PntDim int
         ) RETURNS varchar(50)
as begin
if @P_PntDim = 1 begin
  return 'һά';
end else if @P_PntDim = 2 begin
  return '��ά';
end;
return '';
end;
GO

--��ȡ��㷽��ר��
create function ZXSamplePackage.F_GetPntDirect(
         @P_PntDirect int
         ) RETURNS varchar(50)
as begin
if @P_PntDirect = 0 begin
  return 'ˮƽ';
end else if @P_PntDirect = 1 begin
  return '��ֱ';
end else if @P_PntDirect = 2 begin
  return '����';
end else if @P_PntDirect = 3 begin
  return '45��';
end else if @P_PntDirect = 4 begin
  return '135��';
end else if @P_PntDirect = 99 begin
  return '';
end;
return '';
end;
GO

--��ȡ�����ת����ר��
create function ZXSamplePackage.F_GetPntRotation(
         @P_PntRotation int
         ) RETURNS varchar(50)
as begin
if @P_PntRotation = 0 begin
  return '��ʱ��';
end else if @P_PntRotation = 1 begin
  return '˳ʱ��';
end else if @P_PntRotation = 99 begin
  return '';
end;
return '';
end;
GO

--��ȡ���洢ģʽר��
create function ZXSamplePackage.F_GetStoreType(
         @P_PntStoreType int
         ) RETURNS varchar(50)
as begin
if @P_PntStoreType = 0 begin
  return '�����洢ģʽ';
end else if @P_PntStoreType = 1 begin
  return '����洢ģʽ';
end;
return '';
end;
GO

--��ȡͨ������ר��
create function ZXSamplePackage.F_GetChannelType(
         @P_ChannelType int
         ) RETURNS varchar(50)
as begin
if @P_ChannelType = 1 begin
  return '����';
end else if @P_ChannelType = 2 begin
  return '��ֵ';
end else if @P_ChannelType = 3 begin
  return 'ת��';
end else if @P_ChannelType = 5 begin
  return '������';
end;
return '';
end;
GO


--�������߱�������
--@P_PartitionID ������ֵ
--@P_AlmID ����ID 
--@P_FeatureValueID ������Դ
--@P_PointID ���ID
--@P_AlmDT ����ʱ��
--@P_AlmLevelID �����ȼ�
--@P_AlmDescTX  ��������
--@p_MobjectID �豸ID
--@P_OwnerPostID �豸���ֹ�ID
--@P_MobSpecID    �豸����ID
--@P_UserID �û�ID
create procedure ZXSamplePackage.InsertAlmRecord(
		@P_PartitionID bigint,
          @P_AlmID bigint,
          @P_FeatureValueID int,
          @P_PointID int,
          @P_AlmDT datetime,
          @P_AlmLevelID int, 
          @P_AlmDescTX varchar(500),
          @P_MobjectID int,
          @P_OwnerPostID int,
          @P_MobSpecID int,
          @P_UserID int) 
as  begin
  declare @V_WarningCD varchar(60); 
  declare @V_Count int;
  declare @V_MobjectWarningID bigint;

  set @V_Count = 0;

  begin try
  begin tran;
  -- ֻ�жԴ�����ʧЧ�ʹ�������ص����ͲŻ�Ա��������жϼ�����������������롣
  select @V_Count = count(*) from ZX_History_Alm ha 
	inner join ZT_MobjectWarning mw on ha.Alm_ID = mw.MobjectWarning_ID 
	where ha.Point_ID = @P_PointID and ha.FeatureValue_ID = @P_FeatureValueID 
	--2016-04-14 �����ȱ�������
	and ha.FeatureValue_ID > 10000 
		and ha.AlmLevel_ID = @P_AlmLevelID and mw.Close_YN <> '1';

  if @V_Count = 0 begin
	Exec ecmsCommonPackage.Pr_GetSNCode 'WarningDealWithMM','ZT_MobjectWarning','Warning_CD',@P_AlmDT,@P_UserID,0,@P_MobSpecID,@p_MobjectID,@V_WarningCD output;
	Insert Into ZT_MobjectWarning(MobjectWarning_ID, Warning_CD, Mobject_ID, DJOwner_ID, AlmRecType_CD, AlmLevel_ID, Spec_ID,
				Content_TX,DutyPost_ID,CheckUser_ID,CheckDate_DT,WarningNum_NR,DealWithType_TX,Close_YN,FeatureValue_ID)
				Values(@P_AlmID, @V_WarningCD, @P_MobjectID, @P_OwnerPostID, '2', @P_AlmLevelID, @P_MobSpecID,
				@P_AlmDescTX, @P_OwnerPostID, @P_UserID, @P_AlmDT, '1','0','0', @P_FeatureValueID);
	Insert Into ZX_History_Alm(Partition_ID,Alm_ID,FeatureValue_ID,AlmLevel_ID,Alm_DT,MObject_ID,Point_ID,AlmDesc_TX,OwnerUser_ID)
				Values(@P_PartitionID,@P_AlmID, @P_FeatureValueID, @P_AlmLevelID, @P_AlmDT, @P_MobjectID, @P_PointID, @P_AlmDescTX,@P_OwnerPostID);
  end else begin
	select top 1 @V_MobjectWarningID = Alm_ID from ZX_History_Alm ha 
		inner join ZT_MobjectWarning mw on ha.Alm_ID = mw.MobjectWarning_ID 
		where ha.Point_ID = @P_PointID and ha.FeatureValue_ID = @P_FeatureValueID and ha.AlmLevel_ID = @P_AlmLevelID and mw.Close_YN <> '1' 
		order by ha.Alm_DT desc;
	update ZT_MobjectWarning set WarningNum_NR = WarningNum_NR + 1,
	--2016-04-14 �����ȱ�������
	CheckDate_DT = @P_AlmDT,
	DJOwner_ID = @P_OwnerPostID,DutyPost_ID = @P_OwnerPostID,CheckUser_ID = @P_UserID where MobjectWarning_ID = @V_MobjectWarningID And Close_YN <> '1';
	update ZX_History_Alm set Alm_DT = @P_AlmDT,OwnerUser_ID = @P_OwnerPostID where Alm_ID = @V_MobjectWarningID;
  end;
  commit tran;
  end try
	begin catch   
      Rollback tran;
      return;
	end catch;
end;
GO