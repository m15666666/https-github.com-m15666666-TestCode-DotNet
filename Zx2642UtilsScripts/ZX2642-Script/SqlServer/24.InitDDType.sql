--ע�������ֵ�ĳ�ʼ����Ϊ2���֣�1��ȫ�������ֵ�ĳ�ʼ�������й�˾ʹ�ã�2���ֲ������ֵ�ĳ�ʼ������˾�ڲ�ʹ�ã���
--DDType_CD�������ֵ�����CD
--Org_ID����֯����ID
--Parent_CD��������CD
--DDTier_YN���Ƿ�ֲ�
--DDFlag_YN���Ƿ��������ֵ
--FlagDes_TX������ֵ����
--��ʽ���������ֶ����ƣ������ֶ�����|�����ֶ����ƣ������ֶ����ͣ������ֶ�ѡ��������ֶ����ͣ���TXT��DDL���������ֶ�ѡ�����.�ҡ�
--���硾����ֵ;TXT|����ֵ2;DDL;������1@����ֵ1��������2@����ֵ2��������3@����ֵ3��
--IsUsing_YN���Ƿ�����
--IsSystem_YN���Ƿ���ϵͳ����

--**************************ȫ�������ֵ��ʼ��-----------------------------
begin
    declare @V_ID int;
    set @V_ID = (Select max(dd_id) +1  from bs_ddlist);
    
    if @V_ID is null 
       set @V_ID = 1;
end

begin  
	
--����������� 
	
	Delete From BS_DDType Where DDType_CD = 'BearingFactory' and Org_ID = 0;
	Insert Into BS_DDType(DDType_CD,Org_ID,Name_TX,Parent_CD,DDTier_YN,DDFlag_YN,FlagDes_TX,IsUsing_YN,IsSystem_YN,SortNo_NR)
		Values('BearingFactory',0,'�����������',Null,'0','1',Null,'1','1',4); 	 
	
	Delete From BS_DDList Where DDType_CD = 'BearingFactory' and Org_ID = 0;
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
		Values(@V_ID,'BearingFactory',0,Null,'FAG',0,Null,@V_ID,Null,Null,Null,Null,Null,'1','1');set  @V_ID = @V_ID +1;	
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
			Values(@V_ID,'BearingFactory',0,Null,'BAR',0,Null,@V_ID,Null,Null,Null,Null,Null,'1','1');set  @V_ID = @V_ID +1;		
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
			Values(@V_ID,'BearingFactory',0,Null,'COO',0,Null,@V_ID,Null,Null,Null,Null,Null,'1','1');set  @V_ID = @V_ID +1;		
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
			Values(@V_ID,'BearingFactory',0,Null,'NDH',0,Null,@V_ID,Null,Null,Null,Null,Null,'1','1');set  @V_ID = @V_ID +1;								
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
	Values(@V_ID,'BearingFactory',0,Null,'McG',0,Null,@V_ID,Null,Null,Null,Null,Null,'1','1');set  @V_ID = @V_ID +1;
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
	Values(@V_ID,'BearingFactory',0,Null,'MES',0,Null,@V_ID,Null,Null,Null,Null,Null,'1','1');set  @V_ID = @V_ID +1;		
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
	Values(@V_ID,'BearingFactory',0,Null,'NSK',0,Null,@V_ID,Null,Null,Null,Null,Null,'1','1');set  @V_ID = @V_ID +1;		
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
	Values(@V_ID,'BearingFactory',0,Null,'SKF',0,Null,@V_ID,Null,Null,Null,Null,Null,'1','1');set  @V_ID = @V_ID +1;		
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
	Values(@V_ID,'BearingFactory',0,Null,'MRC',0,Null,@V_ID,Null,Null,Null,Null,Null,'1','1');set  @V_ID = @V_ID +1;		
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
	Values(@V_ID,'BearingFactory',0,Null,'NTN',0,Null,@V_ID,Null,Null,Null,Null,Null,'1','1');set  @V_ID = @V_ID +1;		
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
	Values(@V_ID,'BearingFactory',0,Null,'ROL',0,Null,@V_ID,Null,Null,Null,Null,Null,'1','1');set  @V_ID = @V_ID +1;		
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
	Values(@V_ID,'BearingFactory',0,Null,'SEA',0,Null,@V_ID,Null,Null,Null,Null,Null,'1','1');set  @V_ID = @V_ID +1;		
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
	Values(@V_ID,'BearingFactory',0,Null,'LBT',0,Null,@V_ID,Null,Null,Null,Null,Null,'1','1');set  @V_ID = @V_ID +1;		
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
	Values(@V_ID,'BearingFactory',0,Null,'DGE',0,Null,@V_ID,Null,Null,Null,Null,Null,'1','1');set  @V_ID = @V_ID +1;		
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
	Values(@V_ID,'BearingFactory',0,Null,'INA	',0,Null,@V_ID,Null,Null,Null,Null,Null,'1','1');set  @V_ID = @V_ID +1;
	Insert Into BS_DDList(DD_ID,DDType_CD,Org_ID,DD_CD,Name_TX,Parent_ID,ParentIDList_TX,SortNo_NR,Memo_TX,DDFlag1_TX,DDFlag2_TX,DDFlag3_TX,DDFlag4_TX,SysType_YN,Active_YN)
	Values(@V_ID,'BearingFactory',0,Null,'����	',0,Null,@V_ID,Null,Null,Null,Null,Null,'1','1');set  @V_ID = @V_ID +1;	
	
	update Bs_Objectkey set Keyvalue = @V_ID+1 where Source_Cd = 'BS_DDList';	

end;
