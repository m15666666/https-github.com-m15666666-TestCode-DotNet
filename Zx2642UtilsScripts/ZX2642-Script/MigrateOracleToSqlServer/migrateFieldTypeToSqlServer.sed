    
#表 AlmStand_CommonSetting 
s/AlmStand_CommonSetting.CommonSetting_ID%type/int/Ig
s/AlmStand_CommonSetting.AlmType_ID%type/tinyint/Ig
s/AlmStand_CommonSetting.LowLimit1_NR%type/float/Ig
s/AlmStand_CommonSetting.HighLimit1_NR%type/float/Ig
s/AlmStand_CommonSetting.LowLimit2_NR%type/float/Ig
s/AlmStand_CommonSetting.HighLimit2_NR%type/float/Ig
s/AlmStand_CommonSetting.LowLimit3_NR%type/float/Ig
s/AlmStand_CommonSetting.HighLimit3_NR%type/float/Ig
s/AlmStand_CommonSetting.LowLimit4_NR%type/float/Ig
s/AlmStand_CommonSetting.HighLimit4_NR%type/float/Ig
s/AlmStand_CommonSetting.Description_TX%type/varchar(200)/Ig
    
#表 AlmStand_PntCommon 
s/AlmStand_PntCommon.Point_ID%type/int/Ig
s/AlmStand_PntCommon.FeatureValue_ID%type/int/Ig
s/AlmStand_PntCommon.CommonSetting_ID%type/int/Ig
    
#表 AlmStand_SixFreqBnd 
s/AlmStand_SixFreqBnd.Point_ID%type/int/Ig
s/AlmStand_SixFreqBnd.SixFreqBndType_ID%type/tinyint/Ig
s/AlmStand_SixFreqBnd.LowAlmLevel_ID%type/tinyint/Ig
s/AlmStand_SixFreqBnd.HighAlmLevel_ID%type/tinyint/Ig
s/AlmStand_SixFreqBnd.FreqBnd_NR%type/int/Ig
s/AlmStand_SixFreqBnd.LowFreq1_NR%type/float/Ig
s/AlmStand_SixFreqBnd.HighFreq1_NR%type/float/Ig
s/AlmStand_SixFreqBnd.LowLimit1_NR%type/float/Ig
s/AlmStand_SixFreqBnd.HighLimit1_NR%type/float/Ig
s/AlmStand_SixFreqBnd.LowFreq2_NR%type/float/Ig
s/AlmStand_SixFreqBnd.HighFreq2_NR%type/float/Ig
s/AlmStand_SixFreqBnd.LowLimit2_NR%type/float/Ig
s/AlmStand_SixFreqBnd.HighLimit2_NR%type/float/Ig
s/AlmStand_SixFreqBnd.LowFreq3_NR%type/float/Ig
s/AlmStand_SixFreqBnd.HighFreq3_NR%type/float/Ig
s/AlmStand_SixFreqBnd.LowLimit3_NR%type/float/Ig
s/AlmStand_SixFreqBnd.HighLimit3_NR%type/float/Ig
s/AlmStand_SixFreqBnd.LowFreq4_NR%type/float/Ig
s/AlmStand_SixFreqBnd.HighFreq4_NR%type/float/Ig
s/AlmStand_SixFreqBnd.LowLimit4_NR%type/float/Ig
s/AlmStand_SixFreqBnd.HighLimit4_NR%type/float/Ig
s/AlmStand_SixFreqBnd.LowFreq5_NR%type/float/Ig
s/AlmStand_SixFreqBnd.HighFreq5_NR%type/float/Ig
s/AlmStand_SixFreqBnd.LowLimit5_NR%type/float/Ig
s/AlmStand_SixFreqBnd.HighLimit5_NR%type/float/Ig
s/AlmStand_SixFreqBnd.LowFreq6_NR%type/float/Ig
s/AlmStand_SixFreqBnd.HighFreq6_NR%type/float/Ig
s/AlmStand_SixFreqBnd.LowLimit6_NR%type/float/Ig
s/AlmStand_SixFreqBnd.HighLimit6_NR%type/float/Ig
s/AlmStand_SixFreqBnd.Description_TX%type/varchar(200)/Ig
    
#表 Analysis_BarDiagram 
s/Analysis_BarDiagram.BarDiagram_ID%type/int/Ig
s/Analysis_BarDiagram.AppUser_ID%type/int/Ig
    
#表 Analysis_BarGroup 
s/Analysis_BarGroup.BarGroup_ID%type/int/Ig
s/Analysis_BarGroup.GroupName_TX%type/varchar(100)/Ig
s/Analysis_BarGroup.BarDiagram_ID%type/int/Ig
s/Analysis_BarGroup.SortNo_NR%type/int/Ig
    
#表 Analysis_BarPoint 
s/Analysis_BarPoint.BarPoint_ID%type/int/Ig
s/Analysis_BarPoint.Point_ID%type/int/Ig
s/Analysis_BarPoint.BarGroup_ID%type/int/Ig
s/Analysis_BarPoint.PointName_TX%type/varchar(100)/Ig
s/Analysis_BarPoint.Low_NR%type/float/Ig
s/Analysis_BarPoint.High_NR%type/float/Ig
s/Analysis_BarPoint.SortNo_NR%type/int/Ig
    
#表 Analysis_FeatureFreq 
s/Analysis_FeatureFreq.FeatureFreq_ID%type/int/Ig
s/Analysis_FeatureFreq.Group_ID%type/int/Ig
s/Analysis_FeatureFreq.FeatureFreqValue_NR%type/float/Ig
s/Analysis_FeatureFreq.Name_TX%type/varchar(100)/Ig
s/Analysis_FeatureFreq.Unit_NR%type/int/Ig
    
#表 Analysis_FeatureFreqGroup 
s/Analysis_FeatureFreqGroup.Group_ID%type/int/Ig
s/Analysis_FeatureFreqGroup.GroupName_TX%type/varchar(100)/Ig
s/Analysis_FeatureFreqGroup.Org_ID%type/int/Ig
    
#表 Analysis_MObjPicture 
s/Analysis_MObjPicture.MObject_ID%type/int/Ig
s/Analysis_MObjPicture.Picture_GR%type/image/Ig
s/Analysis_MObjPicture.FileName_TX%type/varchar(200)/Ig
s/Analysis_MObjPicture.FileSuffix_TX%type/varchar(4)/Ig
    
#表 Analysis_MObjPosition 
s/Analysis_MObjPosition.MObjPosition_ID%type/int/Ig
s/Analysis_MObjPosition.Mobject_ID%type/int/Ig
s/Analysis_MObjPosition.PosX_NR%type/float/Ig
s/Analysis_MObjPosition.PosY_NR%type/float/Ig
s/Analysis_MObjPosition.TagX_NR%type/float/Ig
s/Analysis_MObjPosition.TagY_NR%type/float/Ig
    
#表 Analysis_OrgPicture 
s/Analysis_OrgPicture.Org_ID%type/int/Ig
s/Analysis_OrgPicture.Picture_GR%type/image/Ig
s/Analysis_OrgPicture.FileName_TX%type/varchar(200)/Ig
s/Analysis_OrgPicture.FileSuffix_TX%type/varchar(4)/Ig
    
#表 Analysis_PntPosition 
s/Analysis_PntPosition.PntPosition_ID%type/int/Ig
s/Analysis_PntPosition.Point_ID%type/int/Ig
s/Analysis_PntPosition.Mobject_ID%type/int/Ig
s/Analysis_PntPosition.PosX_NR%type/float/Ig
s/Analysis_PntPosition.PosY_NR%type/float/Ig
s/Analysis_PntPosition.TagX_NR%type/float/Ig
s/Analysis_PntPosition.TagY_NR%type/float/Ig
    
#表 BS_AppAction 
s/BS_AppAction.AppAction_CD%type/varchar(60)/Ig
s/BS_AppAction.AppAction_CD_Ref%type/varchar(60)/Ig
s/BS_AppAction.Name_TX%type/varchar(60)/Ig
s/BS_AppAction.Desc_TX%type/varchar(600)/Ig
s/BS_AppAction.ActionType_CD%type/varchar(10)/Ig
s/BS_AppAction.SysAction_YN%type/char/Ig
s/BS_AppAction.Level_NR%type/int/Ig
s/BS_AppAction.SortNo_NR%type/int/Ig
s/BS_AppAction.DataRange_YN%type/char/Ig
s/BS_AppAction.DataRangeList_TX%type/varchar(30)/Ig
s/BS_AppAction.Visible_YN%type/varchar(100)/Ig
s/BS_AppAction.InvokeObject_TX%type/varchar(300)/Ig
s/BS_AppAction.SpecDataRange_YN%type/char/Ig
s/BS_AppAction.Flow_YN%type/char/Ig
    
#表 BS_AppConfig 
s/BS_AppConfig.AppConfig_CD%type/varchar(100)/Ig
s/BS_AppConfig.Name_TX%type/varchar(100)/Ig
s/BS_AppConfig.Value_TX%type/varchar(3000)/Ig
s/BS_AppConfig.DefaultValue_TX%type/varchar(100)/Ig
s/BS_AppConfig.DataType_CD%type/varchar(10)/Ig
s/BS_AppConfig.Category_CD%type/varchar(30)/Ig
s/BS_AppConfig.SysConfig_YN%type/char/Ig
s/BS_AppConfig.SortNo_NR%type/int/Ig
s/BS_AppConfig.ControlType_CD%type/varchar(30)/Ig
s/BS_AppConfig.Other_TX%type/varchar(3000)/Ig
    
#表 BS_AppLog 
s/BS_AppLog.AppLog_ID%type/int/Ig
s/BS_AppLog.AppUserName_TX%type/varchar(30)/Ig
s/BS_AppLog.Action_TM%type/datetime/Ig
s/BS_AppLog.IPAddress_TX%type/varchar(100)/Ig
s/BS_AppLog.AppAction_CD%type/varchar(60)/Ig
s/BS_AppLog.UserAction_TX%type/varchar(300)/Ig
s/BS_AppLog.Log_TX%type/varchar(2000)/Ig
    
#表 BS_AppRole 
s/BS_AppRole.AppRole_ID%type/int/Ig
s/BS_AppRole.Name_TX%type/varchar(60)/Ig
s/BS_AppRole.Desc_TX%type/varchar(300)/Ig
s/BS_AppRole.SysRole_YN%type/char/Ig
    
#表 BS_AppRoleAction 
s/BS_AppRoleAction.AppRole_ID%type/int/Ig
s/BS_AppRoleAction.AppAction_CD%type/varchar(60)/Ig
s/BS_AppRoleAction.DataRange_CD%type/char/Ig
s/BS_AppRoleAction.ShortCut_YN%type/char/Ig
s/BS_AppRoleAction.RangeIDList_TX%type/varchar(100)/Ig
s/BS_AppRoleAction.Spec_YN%type/char/Ig
s/BS_AppRoleAction.SpecList_TX%type/varchar(100)/Ig
    
#表 BS_AppUser 
s/BS_AppUser.AppUser_ID%type/int/Ig
s/BS_AppUser.Name_TX%type/varchar(20)/Ig
s/BS_AppUser.Account_TX%type/varchar(30)/Ig
s/BS_AppUser.AppUser_CD%type/varchar(30)/Ig
s/BS_AppUser.PassWordHash_TX%type/varchar(100)/Ig
s/BS_AppUser.Sex_CD%type/char/Ig
s/BS_AppUser.Email_TX%type/varchar(100)/Ig
s/BS_AppUser.Tel1_TX%type/varchar(60)/Ig
s/BS_AppUser.Tel2_TX%type/varchar(60)/Ig
s/BS_AppUser.Position_TX%type/varchar(20)/Ig
s/BS_AppUser.Create_TM%type/datetime/Ig
s/BS_AppUser.Active_YN%type/char/Ig
s/BS_AppUser.Org_ID%type/int/Ig
s/BS_AppUser.Dept_ID%type/int/Ig
s/BS_AppUser.SysUser_YN%type/char/Ig
s/BS_AppUser.Spec_ID%type/int/Ig
s/BS_AppUser.UserType_ID%type/int/Ig
    
#表 BS_AppuserPost 
s/BS_AppuserPost.AppuserPost_ID%type/int/Ig
s/BS_AppuserPost.AppUser_ID%type/int/Ig
s/BS_AppuserPost.Post_ID%type/int/Ig
s/BS_AppuserPost.PostType_CD%type/char/Ig
s/BS_AppuserPost.Create_DT%type/datetime/Ig
    
#表 BS_AppUserSpec 
s/BS_AppUserSpec.AppUserSpec_ID%type/int/Ig
s/BS_AppUserSpec.AppUser_ID%type/int/Ig
s/BS_AppUserSpec.Spec_ID%type/int/Ig
    
#表 BS_BigObjectKey 
s/BS_BigObjectKey.Source_CD%type/varchar(100)/Ig
s/BS_BigObjectKey.KeyName%type/varchar(100)/Ig
s/BS_BigObjectKey.KeyValue%type/bigint/Ig
s/BS_BigObjectKey.LockTime_TM%type/datetime/Ig
s/BS_BigObjectKey.GenerateMode%type/varchar(10)/Ig
    
#表 BS_CreateTableIndexInfo 
s/BS_CreateTableIndexInfo.TableName_TX%type/varchar(50)/Ig
s/BS_CreateTableIndexInfo.IndexName_TX%type/varchar(30)/Ig
s/BS_CreateTableIndexInfo.CreateIndexSql%type/varchar(60)/Ig
    
#表 BS_CreateTableInfo 
s/BS_CreateTableInfo.TableName_TX%type/varchar(100)/Ig
s/BS_CreateTableInfo.CreateTableSql_TX%type/varchar(2000)/Ig
s/BS_CreateTableInfo.CreatePKSql_TX%type/varchar(500)/Ig
s/BS_CreateTableInfo.CreateFKSql_TX%type/varchar(500)/Ig
s/BS_CreateTableInfo.CreateIndexSql_TX%type/varchar(1000)/Ig
    
#表 BS_CustomDBColumnType 
s/BS_CustomDBColumnType.CustomTypeName_TX%type/varchar(60)/Ig
s/BS_CustomDBColumnType.TransFunctionName_TX%type/varchar(60)/Ig
    
#表 BS_DDList 
s/BS_DDList.DD_ID%type/int/Ig
s/BS_DDList.DDType_CD%type/varchar(30)/Ig
s/BS_DDList.Org_ID%type/int/Ig
s/BS_DDList.DD_CD%type/varchar(100)/Ig
s/BS_DDList.Name_TX%type/varchar(100)/Ig
s/BS_DDList.Parent_ID%type/int/Ig
s/BS_DDList.ParentIDList_TX%type/varchar(100)/Ig
s/BS_DDList.SortNo_NR%type/int/Ig
s/BS_DDList.DDFlag1_TX%type/varchar(100)/Ig
s/BS_DDList.DDFlag2_TX%type/varchar(100)/Ig
s/BS_DDList.DDFlag3_TX%type/varchar(100)/Ig
s/BS_DDList.DDFlag4_TX%type/varchar(100)/Ig
s/BS_DDList.Memo_TX%type/varchar(100)/Ig
s/BS_DDList.SysType_YN%type/char/Ig
s/BS_DDList.Active_YN%type/char/Ig
    
#表 BS_DDType 
s/BS_DDType.DDType_CD%type/varchar(30)/Ig
s/BS_DDType.Org_ID%type/int/Ig
s/BS_DDType.Name_TX%type/varchar(50)/Ig
s/BS_DDType.DDFlag_YN%type/char/Ig
s/BS_DDType.FlagDes_TX%type/varchar(200)/Ig
s/BS_DDType.Parent_CD%type/varchar(30)/Ig
s/BS_DDType.DDTier_YN%type/char/Ig
s/BS_DDType.IsUsing_YN%type/char/Ig
s/BS_DDType.IsSystem_YN%type/char/Ig
s/BS_DDType.SortNo_NR%type/int/Ig
s/BS_DDType.MaxCount%type/int/Ig
    
#表 BS_Dept 
s/BS_Dept.Dept_ID%type/int/Ig
s/BS_Dept.Org_ID%type/int/Ig
s/BS_Dept.Dept_CD%type/varchar(60)/Ig
s/BS_Dept.DeptName_TX%type/varchar(100)/Ig
s/BS_Dept.Property_ID%type/int/Ig
s/BS_Dept.ParentDept_ID%type/int/Ig
s/BS_Dept.SortNo_NR%type/int/Ig
s/BS_Dept.ParentDept1_ID%type/int/Ig
s/BS_Dept.ParentDept2_ID%type/int/Ig
s/BS_Dept.ParentDept3_ID%type/int/Ig
s/BS_Dept.ParentDept4_ID%type/int/Ig
s/BS_Dept.ParentDept5_ID%type/int/Ig
s/BS_Dept.Lever_NR%type/int/Ig
s/BS_Dept.Memo_TX%type/varchar(500)/Ig
s/BS_Dept.AddUser_TX%type/varchar(20)/Ig
s/BS_Dept.Add_DT%type/datetime/Ig
s/BS_Dept.EditUser_TX%type/varchar(20)/Ig
s/BS_Dept.Edit_DT%type/datetime/Ig
s/BS_Dept.Active_YN%type/char/Ig
s/BS_Dept.SN_CD%type/varchar(20)/Ig
s/BS_Dept.ParentIDList_TX%type/varchar(500)/Ig
    
#表 BS_HashTable 
s/BS_HashTable.Key_TX%type/varchar(100)/Ig
s/BS_HashTable.Sys_YN%type/char/Ig
s/BS_HashTable.Value_TX%type/varchar(4000)/Ig
    
#表 BS_ListConfigForUser 
s/BS_ListConfigForUser.AppUser_ID%type/int/Ig
s/BS_ListConfigForUser.ListName_TX%type/varchar(40)/Ig
s/BS_ListConfigForUser.Configuration_TX%type/varchar(4000)/Ig
    
#表 BS_MainMenu 
s/BS_MainMenu.MenuKey_TX%type/varchar(60)/Ig
s/BS_MainMenu.MenuKey_Ref%type/varchar(60)/Ig
s/BS_MainMenu.CustomMenuKey_Ref%type/varchar(60)/Ig
s/BS_MainMenu.Name_TX%type/varchar(60)/Ig
s/BS_MainMenu.CustomName_TX%type/varchar(60)/Ig
s/BS_MainMenu.ToolTip_TX%type/varchar(60)/Ig
s/BS_MainMenu.AppAction_CD%type/varchar(60)/Ig
s/BS_MainMenu.RedirectPageKey_TX%type/varchar(60)/Ig
s/BS_MainMenu.JsBlock_TX%type/varchar(300)/Ig
s/BS_MainMenu.SortNo_NR%type/int/Ig
    
#表 BS_MemuBrowse 
s/BS_MemuBrowse.MemuBrowse_ID%type/bigint/Ig
s/BS_MemuBrowse.Appaction_CD%type/varchar(40)/Ig
s/BS_MemuBrowse.AppuserName_TX%type/varchar(30)/Ig
s/BS_MemuBrowse.Org_ID%type/int/Ig
s/BS_MemuBrowse.Dept_ID%type/int/Ig
s/BS_MemuBrowse.IP_TX%type/varchar(40)/Ig
s/BS_MemuBrowse.BrowseDate_DT%type/datetime/Ig

#表 BS_MetaFieldType 
s/BS_MetaFieldType.TinyInt_NR%type/tinyint/Ig
s/BS_MetaFieldType.SmallInt_NR%type/smallint/Ig
s/BS_MetaFieldType.Int_NR%type/int/Ig
s/BS_MetaFieldType.BigInt_NR%type/bigint/Ig
s/BS_MetaFieldType.DateTime_DT%type/datetime/Ig
s/BS_MetaFieldType.Real_NR%type/float/Ig
s/BS_MetaFieldType.Bit_YN%type/char/Ig
s/BS_MetaFieldType.CD_CD%type/char/Ig
s/BS_MetaFieldType.Emsg_TX%type/varchar(300)/Ig
s/BS_MetaFieldType.Fifty_TX%type/varchar(50)/Ig
s/BS_MetaFieldType.OneHundred_TX%type/varchar(100)/Ig
s/BS_MetaFieldType.TwoHundred_TX%type/varchar(200)/Ig
s/BS_MetaFieldType.FiveHundred_TX%type/varchar(500)/Ig
s/BS_MetaFieldType.OneKilo_TX%type/varchar(1000)/Ig
s/BS_MetaFieldType.TwoKilo_TX%type/varchar(2000)/Ig
s/BS_MetaFieldType.FiveKilo_TX%type/varchar(5000)/Ig
s/BS_MetaFieldType.EightKilo_TX%type/varchar(8000)/Ig
s/BS_MetaFieldType.Blob_GR%type/image/Ig

#表 BS_ModulePage 
s/BS_ModulePage.PageKey_TX%type/varchar(60)/Ig
s/BS_ModulePage.Name_TX%type/varchar(60)/Ig
s/BS_ModulePage.PageUrl_TX%type/varchar(300)/Ig
s/BS_ModulePage.AppAction_CD%type/varchar(60)/Ig
    
#表 BS_ObjectKey 
s/BS_ObjectKey.Source_CD%type/varchar(100)/Ig
s/BS_ObjectKey.KeyName%type/varchar(100)/Ig
s/BS_ObjectKey.KeyValue%type/int/Ig
s/BS_ObjectKey.LockTime_TM%type/datetime/Ig
s/BS_ObjectKey.GenerateMode%type/varchar(10)/Ig
    
#表 BS_Org 
s/BS_Org.Org_ID%type/int/Ig
s/BS_Org.Org_CD%type/varchar(60)/Ig
s/BS_Org.OrgName_TX%type/varchar(100)/Ig
s/BS_Org.Property_ID%type/int/Ig
s/BS_Org.Memo_TX%type/varchar(500)/Ig
s/BS_Org.SortNo_NR%type/int/Ig
s/BS_Org.AddUser_TX%type/varchar(20)/Ig
s/BS_Org.Add_DT%type/datetime/Ig
s/BS_Org.EditUser_TX%type/varchar(20)/Ig
s/BS_Org.Edit_DT%type/datetime/Ig
s/BS_Org.Active_YN%type/char/Ig
    
#表 BS_PageControl 
s/BS_PageControl.PageControl_ID%type/int/Ig
s/BS_PageControl.CommandName_TX%type/varchar(60)/Ig
s/BS_PageControl.Name_TX%type/varchar(60)/Ig
s/BS_PageControl.PageControlType_CD%type/varchar(60)/Ig
s/BS_PageControl.PageKey_TX%type/varchar(60)/Ig
s/BS_PageControl.ToolTip_TX%type/varchar(60)/Ig
s/BS_PageControl.AppAction_CD%type/varchar(60)/Ig
s/BS_PageControl.PageControlImageKey_TX%type/varchar(60)/Ig
s/BS_PageControl.RedirectPageKey_TX%type/varchar(60)/Ig
s/BS_PageControl.SortNo_NR%type/int/Ig
s/BS_PageControl.ControlGroup_TX%type/varchar(60)/Ig
    
#表 BS_PageControlImage 
s/BS_PageControlImage.PageControlImageKey_TX%type/varchar(60)/Ig
s/BS_PageControlImage.Name_TX%type/varchar(60)/Ig
s/BS_PageControlImage.ImageUrl_TX%type/varchar(300)/Ig
    
#表 BS_PartitionViewMeta 
s/BS_PartitionViewMeta.BaseViewName_TX%type/varchar(30)/Ig
s/BS_PartitionViewMeta.TableName_TX%type/varchar(30)/Ig
    
#表 BS_Post 
s/BS_Post.Post_ID%type/int/Ig
s/BS_Post.AppRole_ID%type/int/Ig
s/BS_Post.Org_ID%type/int/Ig
s/BS_Post.Dept_ID%type/int/Ig
s/BS_Post.Post_CD%type/varchar(60)/Ig
s/BS_Post.PostName_TX%type/varchar(60)/Ig
s/BS_Post.Memo_TX%type/varchar(500)/Ig
s/BS_Post.AddUser_TX%type/varchar(20)/Ig
s/BS_Post.Add_DT%type/datetime/Ig
s/BS_Post.EditUser_TX%type/varchar(20)/Ig
s/BS_Post.Edit_DT%type/datetime/Ig
s/BS_Post.Active_YN%type/char/Ig
s/BS_Post.Sys_YN%type/char/Ig
s/BS_Post.SN_CD%type/varchar(20)/Ig
s/BS_Post.SortNo_NR%type/int/Ig
    
#表 BS_PostAppAction 
s/BS_PostAppAction.Post_ID%type/int/Ig
s/BS_PostAppAction.AppAction_CD%type/varchar(60)/Ig
s/BS_PostAppAction.DataRange_CD%type/char/Ig
s/BS_PostAppAction.ShortCut_YN%type/char/Ig
s/BS_PostAppAction.RangeIDList_TX%type/varchar(100)/Ig
s/BS_PostAppAction.Professional_YN%type/char/Ig
s/BS_PostAppAction.ProfessionalList_TX%type/varchar(100)/Ig
s/BS_PostAppAction.PostType_CD%type/char/Ig
    
#表 BS_PostChange 
s/BS_PostChange.PostChange_ID%type/int/Ig
s/BS_PostChange.Post_ID%type/int/Ig
s/BS_PostChange.AappUser_ID%type/int/Ig
s/BS_PostChange.AIn_DT%type/datetime/Ig
s/BS_PostChange.AOut_DT%type/datetime/Ig
s/BS_PostChange.Bappuser_ID%type/int/Ig
s/BS_PostChange.BIn_DT%type/datetime/Ig
s/BS_PostChange.BOut_DT%type/datetime/Ig
s/BS_PostChange.Memo_TX%type/varchar(500)/Ig
    
#表 BS_QueryPartition 
s/BS_QueryPartition.FunctionKey_CD%type/varchar(30)/Ig
s/BS_QueryPartition.FunctionDesc_TX%type/varchar(100)/Ig
s/BS_QueryPartition.TableBaseName_TX%type/varchar(100)/Ig
s/BS_QueryPartition.Category_CD%type/varchar(60)/Ig
    
#表 BS_RangePartition 
s/BS_RangePartition.TableBaseName_TX%type/varchar(100)/Ig
s/BS_RangePartition.TableName_TX%type/varchar(100)/Ig
s/BS_RangePartition.PartitionMin_ID%type/bigint/Ig
s/BS_RangePartition.PartitionMax_ID%type/bigint/Ig
    
#表 BS_RangePartitionMeta 
s/BS_RangePartitionMeta.TableBaseName_TX%type/varchar(100)/Ig
s/BS_RangePartitionMeta.PartitionField_TX%type/varchar(60)/Ig
s/BS_RangePartitionMeta.Category_CD%type/varchar(60)/Ig
s/BS_RangePartitionMeta.PartitionMin_ID%type/bigint/Ig
s/BS_RangePartitionMeta.ConstraintName_TX%type/varchar(30)/Ig
    
#表 BS_Resource 
s/BS_Resource.Key_TX%type/varchar(50)/Ig
s/BS_Resource.Value_TX%type/varchar(1000)/Ig
s/BS_Resource.Category_CD%type/varchar(30)/Ig
s/BS_Resource.Remark_TX%type/varchar(500)/Ig
    
#表 BS_Resourceloc 
s/BS_Resourceloc.RESID_TX%type/varchar(50)/Ig
s/BS_Resourceloc.Key_TX%type/varchar(50)/Ig
s/BS_Resourceloc.FilePath_TX%type/varchar(500)/Ig
s/BS_Resourceloc.Value_TX%type/varchar(1000)/Ig
s/BS_Resourceloc.Category_CD%type/varchar(30)/Ig
s/BS_Resourceloc.ReMark_TX%type/varchar(500)/Ig
    
#表 BS_TableColumns 
s/BS_TableColumns.TableName_TX%type/varchar(100)/Ig
s/BS_TableColumns.ColumnName_TX%type/varchar(60)/Ig
s/BS_TableColumns.ColumnType_TX%type/varchar(60)/Ig
s/BS_TableColumns.CustomTypeName_TX%type/varchar(60)/Ig
    
#表 BS_TransPartionTable 
s/BS_TransPartionTable.TransKey_CD%type/varchar(30)/Ig
s/BS_TransPartionTable.TableBaseNameList_TX%type/varchar(300)/Ig
s/BS_TransPartionTable.ViewBaseNameList_TX%type/varchar(300)/Ig
s/BS_TransPartionTable.PartitionType_CD%type/varchar(60)/Ig
s/BS_TransPartionTable.PartitionType_NR%type/int/Ig
s/BS_TransPartionTable.LastPartitonDate_DT%type/datetime/Ig
    
#表 BS_ViewColumn 
s/BS_ViewColumn.ViewColumn_ID%type/int/Ig
s/BS_ViewColumn.View_ID%type/int/Ig
s/BS_ViewColumn.ColumnName_TX%type/varchar(100)/Ig
s/BS_ViewColumn.ColumnDesc_TX%type/varchar(200)/Ig
s/BS_ViewColumn.DataType_TX%type/varchar(20)/Ig
    
#表 BS_ViewFuncColumns 
s/BS_ViewFuncColumns.Function_ID%type/int/Ig
s/BS_ViewFuncColumns.ViewColumn_ID%type/int/Ig
s/BS_ViewFuncColumns.SortNo_NR%type/int/Ig
s/BS_ViewFuncColumns.SysCondition_YN%type/char/Ig
s/BS_ViewFuncColumns.Condition_YN%type/char/Ig
s/BS_ViewFuncColumns.Show_YN%type/char/Ig
s/BS_ViewFuncColumns.Width_NR%type/int/Ig
s/BS_ViewFuncColumns.Align_TX%type/varchar(10)/Ig
s/BS_ViewFuncColumns.Sort_YN%type/char/Ig
s/BS_ViewFuncColumns.Asc_YN%type/char/Ig
s/BS_ViewFuncColumns.RankSortNo_NR%type/int/Ig
    
#表 BS_ViewFunction 
s/BS_ViewFunction.Function_ID%type/int/Ig
s/BS_ViewFunction.View_ID%type/int/Ig
s/BS_ViewFunction.Name_TX%type/varchar(50)/Ig
s/BS_ViewFunction.AppAction_CD%type/varchar(60)/Ig
s/BS_ViewFunction.DataKey_CD%type/varchar(50)/Ig
s/BS_ViewFunction.ListName_TX%type/varchar(50)/Ig
s/BS_ViewFunction.DataRange_YN%type/char/Ig
s/BS_ViewFunction.DataRange_CD%type/varchar(50)/Ig
    
#表 BS_ViewInfo 
s/BS_ViewInfo.View_ID%type/int/Ig
s/BS_ViewInfo.ViewName_TX%type/varchar(100)/Ig
s/BS_ViewInfo.ViewDesc_TX%type/varchar(100)/Ig
s/BS_ViewInfo.ViewScript_TX%type/varchar(1000)/Ig
s/BS_ViewInfo.EntityType_CD%type/char/Ig
    
#表 BS_ViewParameter 
s/BS_ViewParameter.View_ID%type/int/Ig
s/BS_ViewParameter.ParameterName_TX%type/varchar(100)/Ig
s/BS_ViewParameter.ParameterDesc_TX%type/varchar(200)/Ig
s/BS_ViewParameter.DataType_TX%type/varchar(20)/Ig
s/BS_ViewParameter.SortNo_NR%type/int/Ig
s/BS_ViewParameter.ParamDirection_TX%type/varchar(20)/Ig
    
#表 MOb_FeatreFreqGroup 
s/MOb_FeatreFreqGroup.Group_ID%type/int/Ig
s/MOb_FeatreFreqGroup.MObject_ID%type/int/Ig
    
#表 Mob_MObject 
s/Mob_MObject.Mobject_ID%type/int/Ig
s/Mob_MObject.Org_ID%type/int/Ig
s/Mob_MObject.Mobject_CD%type/varchar(60)/Ig
s/Mob_MObject.MobjectName_TX%type/varchar(100)/Ig
s/Mob_MObject.Spec_ID%type/int/Ig
s/Mob_MObject.ControlLevel_ID%type/int/Ig
s/Mob_MObject.GGXH_TX%type/varchar(600)/Ig
s/Mob_MObject.PitureNo_TX%type/varchar(300)/Ig
s/Mob_MObject.CZ_TX%type/varchar(600)/Ig
s/Mob_MObject.RepairType_ID%type/int/Ig
s/Mob_MObject.Property_ID%type/int/Ig
s/Mob_MObject.DJOwner_ID%type/int/Ig
s/Mob_MObject.JXDept_ID%type/int/Ig
s/Mob_MObject.CZDept_ID%type/int/Ig
s/Mob_MObject.GNWZ_ID%type/int/Ig
s/Mob_MObject.AZWZ_TX%type/varchar(300)/Ig
s/Mob_MObject.XLH_TX%type/varchar(300)/Ig
s/Mob_MObject.SBMS_TX%type/varchar(300)/Ig
s/Mob_MObject.SBJC_TX%type/varchar(300)/Ig
s/Mob_MObject.EnglishName_TX%type/varchar(300)/Ig
s/Mob_MObject.JSCS_TX%type/varchar(600)/Ig
s/Mob_MObject.ZJL_NR%type/int/Ig
s/Mob_MObject.ZJLDW_TX%type/varchar(10)/Ig
s/Mob_MObject.Status_ID%type/int/Ig
s/Mob_MObject.ZZCJ_TX%type/varchar(100)/Ig
s/Mob_MObject.ZCZT_ID%type/int/Ig
s/Mob_MObject.CCRQ_DT%type/datetime/Ig
s/Mob_MObject.AZRQ_DT%type/datetime/Ig
s/Mob_MObject.TYRQ_DT%type/datetime/Ig
s/Mob_MObject.AddUser_TX%type/varchar(20)/Ig
s/Mob_MObject.Add_DT%type/datetime/Ig
s/Mob_MObject.EditUser_TX%type/varchar(20)/Ig
s/Mob_MObject.Edit_DT%type/datetime/Ig
s/Mob_MObject.Active_YN%type/char/Ig
s/Mob_MObject.SortNo_NR%type/int/Ig
s/Mob_MObject.Area_ID%type/int/Ig
s/Mob_MObject.CostCenter_ID%type/int/Ig
    
#表 Mob_MobjectProperty 
s/Mob_MobjectProperty.MobjectProperty_ID%type/int/Ig
s/Mob_MobjectProperty.Mobject_ID%type/int/Ig
s/Mob_MobjectProperty.PropertyName_TX%type/varchar(60)/Ig
s/Mob_MobjectProperty.PropertyValue_TX%type/varchar(60)/Ig
s/Mob_MobjectProperty.Unit_TX%type/varchar(50)/Ig
    
#表 Mob_MobjectStructure 
s/Mob_MobjectStructure.MobjectStructure_ID%type/int/Ig
s/Mob_MobjectStructure.Mobject_ID%type/int/Ig
s/Mob_MobjectStructure.Org_ID%type/int/Ig
s/Mob_MobjectStructure.Parent_ID%type/int/Ig
s/Mob_MobjectStructure.Lever_NR%type/int/Ig
s/Mob_MobjectStructure.ParentList_TX%type/varchar(300)/Ig
s/Mob_MobjectStructure.Memo_TX%type/varchar(1000)/Ig
    
#表 Pnt_Point 
s/Pnt_Point.Point_ID%type/int/Ig
s/Pnt_Point.Mobject_ID%type/int/Ig
s/Pnt_Point.PointName_TX%type/varchar(200)/Ig
s/Pnt_Point.DatType_ID%type/smallint/Ig
s/Pnt_Point.SigType_ID%type/smallint/Ig
s/Pnt_Point.PntDim_NR%type/smallint/Ig
s/Pnt_Point.PntDirect_NR%type/smallint/Ig
s/Pnt_Point.Rotation_NR%type/smallint/Ig
s/Pnt_Point.EngUnit_ID%type/int/Ig
s/Pnt_Point.FeatureValue_ID%type/int/Ig
s/Pnt_Point.StoreType_NR%type/smallint/Ig
s/Pnt_Point.SortNo_NR%type/int/Ig
    
#表 Sample_DAUStation 
s/Sample_DAUStation.Station_ID%type/int/Ig
s/Sample_DAUStation.ServerDAU_ID%type/int/Ig
    
#表 Sample_PntChannel 
s/Sample_PntChannel.ChnNo_NR%type/tinyint/Ig
s/Sample_PntChannel.StationChannel_ID%type/int/Ig
s/Sample_PntChannel.Point_ID%type/int/Ig
    
#表 Sample_Server 
s/Sample_Server.Server_ID%type/int/Ig
s/Sample_Server.Name_TX%type/varchar(60)/Ig
s/Sample_Server.URL_TX%type/varchar(100)/Ig
s/Sample_Server.ServerParam_TX%type/varchar(2000)/Ig
s/Sample_Server.IP_TX%type/varchar(100)/Ig
    
#表 Sample_ServerDAU 
s/Sample_ServerDAU.ServerDAU_ID%type/int/Ig
s/Sample_ServerDAU.Server_ID%type/int/Ig
s/Sample_ServerDAU.Name_TX%type/varchar(60)/Ig
s/Sample_ServerDAU.URL_TX%type/varchar(100)/Ig
s/Sample_ServerDAU.ServerDAUParam_TX%type/varchar(2000)/Ig
s/Sample_ServerDAU.IP_TX%type/varchar(100)/Ig
    
#表 Sample_Station 
s/Sample_Station.Station_ID%type/int/Ig
s/Sample_Station.Org_ID%type/int/Ig
s/Sample_Station.Name_TX%type/varchar(60)/Ig
s/Sample_Station.StationSN_TX%type/varchar(32)/Ig
s/Sample_Station.StationType_TX%type/varchar(100)/Ig
s/Sample_Station.IP_TX%type/varchar(100)/Ig
s/Sample_Station.StationParam_TX%type/varchar(2000)/Ig
    
#表 Sample_StationChannel 
s/Sample_StationChannel.StationChannel_ID%type/int/Ig
s/Sample_StationChannel.Station_ID%type/int/Ig
s/Sample_StationChannel.ChannelType_NR%type/int/Ig
s/Sample_StationChannel.ChannelNumber_NR%type/int/Ig
s/Sample_StationChannel.StationChannelParam_TX%type/varchar(2000)/Ig
    
#表 Z_AlmLevel 
s/Z_AlmLevel.AlmLevel_ID%type/int/Ig
s/Z_AlmLevel.Name_TX%type/varchar(40)/Ig
    
#表 Z_AlmType 
s/Z_AlmType.AlmType_ID%type/tinyint/Ig
s/Z_AlmType.Name_TX%type/varchar(40)/Ig
    
#表 Z_DataRange 
s/Z_DataRange.DataRange_CD%type/char/Ig
s/Z_DataRange.Name_TX%type/varchar(60)/Ig
    
#表 Z_DataType 
s/Z_DataType.DataType_ID%type/int/Ig
s/Z_DataType.Name_TX%type/varchar(20)/Ig
s/Z_DataType.OnLineName_TX%type/varchar(20)/Ig
s/Z_DataType.SortNo_NR%type/int/Ig
s/Z_DataType.OnLine_YN%type/char/Ig
s/Z_DataType.OffLine_YN%type/char/Ig
    
#表 Z_EngUnit 
s/Z_EngUnit.EngUnit_ID%type/int/Ig
s/Z_EngUnit.UnitType_ID%type/int/Ig
s/Z_EngUnit.NameC_TX%type/varchar(20)/Ig
s/Z_EngUnit.NameE_TX%type/varchar(20)/Ig
s/Z_EngUnit.Scale_NR%type/float/Ig
s/Z_EngUnit.Offset_NR%type/float/Ig
s/Z_EngUnit.IsDefault_YN%type/char/Ig
s/Z_EngUnit.IsInner_YN%type/char/Ig
    
#表 Z_EngUnitType 
s/Z_EngUnitType.UnitType_ID%type/int/Ig
s/Z_EngUnitType.Name_TX%type/varchar(20)/Ig
s/Z_EngUnitType.Description_TX%type/varchar(20)/Ig
    
#表 Z_FeatureValue 
s/Z_FeatureValue.FeatureValue_ID%type/int/Ig
s/Z_FeatureValue.Name_TX%type/varchar(40)/Ig
    
#表 Z_SignType 
s/Z_SignType.SignType_ID%type/int/Ig
s/Z_SignType.DataType_ID%type/int/Ig
s/Z_SignType.Name_TX%type/varchar(20)/Ig
s/Z_SignType.UnitType_ID%type/int/Ig
s/Z_SignType.SortNo_NR%type/int/Ig
s/Z_SignType.OnLine_YN%type/char/Ig
s/Z_SignType.OffLine_YN%type/char/Ig
    
#表 ZX_Bearing 
s/ZX_Bearing.Bearing_ID%type/int/Ig
s/ZX_Bearing.Facotry_TX%type/varchar(100)/Ig
s/ZX_Bearing.Model_TX%type/varchar(100)/Ig
s/ZX_Bearing.RollerCount_NR%type/int/Ig
s/ZX_Bearing.FTF_NR%type/float/Ig
s/ZX_Bearing.BSF_NR%type/float/Ig
s/ZX_Bearing.BBPFO_NR%type/float/Ig
s/ZX_Bearing.BPFI_NR%type/float/Ig
s/ZX_Bearing.IsInlay_YN%type/char/Ig
s/ZX_Bearing.Description_TX%type/varchar(200)/Ig
    
#表 ZX_History_Alm 
s/ZX_History_Alm.Partition_ID%type/bigint/Ig
s/ZX_History_Alm.Alm_ID%type/bigint/Ig
s/ZX_History_Alm.FeatureValue_ID%type/int/Ig
s/ZX_History_Alm.AlmLevel_ID%type/smallint/Ig
s/ZX_History_Alm.Alm_DT%type/datetime/Ig
s/ZX_History_Alm.MObject_ID%type/int/Ig
s/ZX_History_Alm.Point_ID%type/int/Ig
s/ZX_History_Alm.AlmDesc_TX%type/varchar(1000)/Ig
s/ZX_History_Alm.DealWithStatus_CD%type/char/Ig
s/ZX_History_Alm.Memo_TX%type/varchar(1000)/Ig
s/ZX_History_Alm.OwnerDept_ID%type/int/Ig
s/ZX_History_Alm.OwnerUser_ID%type/int/Ig
s/ZX_History_Alm.DealWithUser_ID%type/int/Ig
s/ZX_History_Alm.DealWith_DT%type/datetime/Ig
s/ZX_History_Alm.DealWithRel_CD%type/varchar(60)/Ig
s/ZX_History_Alm.DealWithType_NR%type/int/Ig
s/ZX_History_Alm.DealWithResult_TX%type/varchar(200)/Ig
s/ZX_History_Alm.DealWithFromRel_CD%type/varchar(60)/Ig
    
#表 ZX_History_DataMapping 
s/ZX_History_DataMapping.Partition_ID%type/bigint/Ig
s/ZX_History_DataMapping.History_ID%type/bigint/Ig
    
#表 ZX_History_FeatureValue 
s/ZX_History_FeatureValue.Partition_ID%type/bigint/Ig
s/ZX_History_FeatureValue.FeatureValuePK_ID%type/bigint/Ig
s/ZX_History_FeatureValue.History_ID%type/bigint/Ig
s/ZX_History_FeatureValue.ChnNo_NR%type/tinyint/Ig
s/ZX_History_FeatureValue.FeatureValueType_ID%type/int/Ig
s/ZX_History_FeatureValue.FeatureValue_ID%type/int/Ig
s/ZX_History_FeatureValue.FeatureValue_NR%type/float/Ig
s/ZX_History_FeatureValue.SigType_NR%type/int/Ig
s/ZX_History_FeatureValue.EngUnit_ID%type/int/Ig
    
#表 ZX_History_Summary 
s/ZX_History_Summary.Partition_ID%type/bigint/Ig
s/ZX_History_Summary.History_ID%type/bigint/Ig
s/ZX_History_Summary.Point_ID%type/int/Ig
s/ZX_History_Summary.Compress_ID%type/tinyint/Ig
s/ZX_History_Summary.CompressType_ID%type/int/Ig
s/ZX_History_Summary.PntDim_NR%type/tinyint/Ig
s/ZX_History_Summary.DatType_NR%type/tinyint/Ig
s/ZX_History_Summary.SigType_NR%type/int/Ig
s/ZX_History_Summary.SampTime_DT%type/datetime/Ig
s/ZX_History_Summary.DatLen_NR%type/int/Ig
s/ZX_History_Summary.RotSpeed_NR%type/int/Ig
s/ZX_History_Summary.SampleFreq_NR%type/float/Ig
s/ZX_History_Summary.SampMod_NR%type/tinyint/Ig
s/ZX_History_Summary.AvgNum_NR%type/tinyint/Ig
s/ZX_History_Summary.Result_TX%type/varchar(100)/Ig
s/ZX_History_Summary.AlmLevel_ID%type/int/Ig
s/ZX_History_Summary.DataGroup_NR%type/int/Ig
s/ZX_History_Summary.Alm_ID%type/bigint/Ig
    
#表 ZX_History_Waveform 
s/ZX_History_Waveform.Partition_ID%type/bigint/Ig
s/ZX_History_Waveform.History_ID%type/bigint/Ig
s/ZX_History_Waveform.ChnNo_NR%type/tinyint/Ig
s/ZX_History_Waveform.Wave_GR%type/image/Ig
s/ZX_History_Waveform.Compress_NR%type/tinyint/Ig
s/ZX_History_Waveform.WaveScale_NR%type/float/Ig

