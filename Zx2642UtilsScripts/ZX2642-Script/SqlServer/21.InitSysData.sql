--【报警级别】
Delete From ZX_AlmLevel;

Insert Into ZX_AlmLevel(AlmLevel_ID,Name_TX,RefAlmLevel_ID) Values(0,'正常',0);
Insert Into ZX_AlmLevel(AlmLevel_ID,Name_TX,RefAlmLevel_ID) Values(1,'警告',2);
Insert Into ZX_AlmLevel(AlmLevel_ID,Name_TX,RefAlmLevel_ID) Values(2,'危险',4);

GO

--【对象键值】
  declare @V_InitialObjectKey Int;
  declare @V_InitialBigObjectKey Int;
Begin
delete From BS_BigObjectKey;
delete From BS_ObjectKey

set @V_InitialObjectKey =  1000000;
set @V_InitialBigObjectKey =  1000000;

Insert Into  BS_BigObjectKey(Source_CD, KeyName, KeyValue, LockTime_TM) values ('ZX_History_Summary','History_ID',@V_InitialBigObjectKey,DBDiffPackage.GetSystemTime());
Insert Into  BS_BigObjectKey(Source_CD, KeyName, KeyValue, LockTime_TM) values ('ZX_History_Alm','Alm_ID',@V_InitialBigObjectKey,DBDiffPackage.GetSystemTime());
Insert Into  BS_BigObjectKey(Source_CD, KeyName, KeyValue, LockTime_TM) values ('ZX_History_Waveform','Waveform_ID',@V_InitialBigObjectKey,DBDiffPackage.GetSystemTime());
Insert Into  BS_BigObjectKey(Source_CD, KeyName, KeyValue, LockTime_TM) values ('ZX_History_FeatureValue','FeatureValuePK_ID',@V_InitialBigObjectKey,DBDiffPackage.GetSystemTime());
Insert Into  BS_BigObjectKey(Source_CD, KeyName, KeyValue, LockTime_TM) values ('ZX_History_Summary','Synch_NR',@V_InitialBigObjectKey,DBDiffPackage.GetSystemTime());

Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('AlmStand_CommonSetting','CommonSetting_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('AlmStand_PntCommon','Point_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('AlmStand_SixFreqBnd','Point_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('Analysis_BarDiagram','BarDiagram_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('Analysis_BarGroup','BarGroup_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('Analysis_BarPoint','BarPoint_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('Analysis_FeatureFreq','FeatureFreq_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('Analysis_FeatureFreqGroup','Group_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('Analysis_MObjPosition','MObjPosition_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('Analysis_PntPosition','PntPosition_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());

Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('BS_AppLog','AppLog_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('BS_AppRole','AppRole_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('BS_AppRoleAction','AppRole_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('BS_AppUser','AppUser_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('BS_AppuserPost','AppuserPost_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('BS_AppUserSpec','AppUserSpec_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());

Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('BS_DDList','DD_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('BS_Dept','Dept_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('BS_Org','Org_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('BS_Post','Post_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('BS_PostChange','PostChange_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('BS_SNRule','SNRule_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());

Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('Mob_MObject','Mobject_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('Mob_MobjectProperty','MobjectProperty_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('Mob_MobjectStructure','MobjectStructure_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());

Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('Pnt_Point','Point_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());

Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('Sample_DAUStation','Station_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('Sample_PntChannel','StationChannel_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('Sample_Server','Server_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('Sample_ServerDAU','ServerDAU_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('Sample_Station','Station_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('Sample_StationChannel','StationChannel_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());

Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('ZX_Bearing','Bearing_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());

Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('Pnt_PointGroup','PointGroup_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('Pnt_PointGroupNo','GroupNo_NR',0,DBDiffPackage.GetSystemTime());

Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('Pnt_DataVar','Var_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());

Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('ZX_SMS','SMS_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('ZX_SMSConfig','SMSConfig_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());
Insert Into Bs_Objectkey(Source_Cd,Keyname,Keyvalue,Locktime_Tm) values ('ZX_SMSHistory','SMSHistory_ID',@V_InitialObjectKey,DBDiffPackage.GetSystemTime());

End;