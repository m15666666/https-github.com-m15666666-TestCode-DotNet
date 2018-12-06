--注：数据字典的初始化分为2部分（1、全局数据字典的初始化（所有公司使用）2、局部数据字典的初始化（公司内部使用））
--DDType_CD：数据字典类型CD
--Org_ID：组织机构ID
--Parent_CD：父类型CD
--DDTier_YN：是否分层
--DDFlag_YN：是否包含特征值
--FlagDes_TX：特征值描述
--格式：【特征字段名称，特征字段类型|特征字段名称，特征字段类型，特征字段选项】；特征字段类型：【TXT，DDL】；特征字段选项：【甲.乙】
--例如【特征值;TXT|特征值2;DDL;下拉项1@下拉值1，下拉项2@下拉值2，下拉项3@下拉值3】
--IsUsing_YN：是否启用
--IsSystem_YN：是否是系统类型

--**************************全局数据字典初始化-----------------------------
Declare 
V_ID BS_MetaFieldType.Int_NR%type;

begin    
	
    Select max(dd_id + 1) into V_ID from bs_ddlist;
    
    if V_ID is null then
       V_ID := 1;
    end if; 
	
--轴承生产厂家 
	
	Delete From BS_DDType Where DDType_CD = 'BearingFactory' and Org_ID = 0;
	Insert Into BS_DDType(DDType_CD,Org_ID,Name_TX,Parent_CD,DDTier_YN,DDFlag_YN,FlagDes_TX,IsUsing_YN,IsSystem_YN,SortNo_NR)
		Values('BearingFactory',0,'轴承生产厂家',Null,'0','1',Null,'1','1',4); 	 
		
    Delete From BS_DDList Where DDType_CD = 'BearingFactory' and Org_ID = 0;
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
		Values(V_ID,'BearingFactory',0,Null,'FAG',0,Null,V_ID,Null,Null,Null,Null,Null,'1','1');V_ID := V_ID +1;		
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
			Values(V_ID,'BearingFactory',0,Null,'BAR',0,Null,V_ID,Null,Null,Null,Null,Null,'1','1');V_ID := V_ID +1;		
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
			Values(V_ID,'BearingFactory',0,Null,'COO',0,Null,V_ID,Null,Null,Null,Null,Null,'1','1');V_ID := V_ID +1;		
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
			Values(V_ID,'BearingFactory',0,Null,'NDH',0,Null,V_ID,Null,Null,Null,Null,Null,'1','1');V_ID := V_ID +1;								
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
	Values(V_ID,'BearingFactory',0,Null,'McG',0,Null,V_ID,Null,Null,Null,Null,Null,'1','1');V_ID := V_ID +1;
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
	Values(V_ID,'BearingFactory',0,Null,'MES',0,Null,V_ID,Null,Null,Null,Null,Null,'1','1');V_ID := V_ID +1;		
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
	Values(V_ID,'BearingFactory',0,Null,'NSK',0,Null,V_ID,Null,Null,Null,Null,Null,'1','1');V_ID := V_ID +1;		
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
	Values(V_ID,'BearingFactory',0,Null,'SKF',0,Null,V_ID,Null,Null,Null,Null,Null,'1','1');V_ID := V_ID +1;		
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
	Values(V_ID,'BearingFactory',0,Null,'MRC',0,Null,V_ID,Null,Null,Null,Null,Null,'1','1');V_ID := V_ID +1;		
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
	Values(V_ID,'BearingFactory',0,Null,'NTN',0,Null,V_ID,Null,Null,Null,Null,Null,'1','1');V_ID := V_ID +1;		
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
	Values(V_ID,'BearingFactory',0,Null,'ROL',0,Null,V_ID,Null,Null,Null,Null,Null,'1','1');V_ID := V_ID +1;		
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
	Values(V_ID,'BearingFactory',0,Null,'SEA',0,Null,V_ID,Null,Null,Null,Null,Null,'1','1');V_ID := V_ID +1;		
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
	Values(V_ID,'BearingFactory',0,Null,'LBT',0,Null,V_ID,Null,Null,Null,Null,Null,'1','1');V_ID := V_ID +1;		
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
	Values(V_ID,'BearingFactory',0,Null,'DGE',0,Null,V_ID,Null,Null,Null,Null,Null,'1','1');V_ID := V_ID +1;		
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
	Values(V_ID,'BearingFactory',0,Null,'INA	',0,Null,V_ID,Null,Null,Null,Null,Null,'1','1');V_ID := V_ID +1;
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
	Values(V_ID,'BearingFactory',0,Null,'其它	',0,Null,V_ID,Null,Null,Null,Null,Null,'1','1');V_ID := V_ID +1;
	
commit;

update Bs_Objectkey set Keyvalue = V_ID+1 where Source_Cd = 'BS_DDList';
 
commit;

end;
/