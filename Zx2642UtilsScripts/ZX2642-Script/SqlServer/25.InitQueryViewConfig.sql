--���ù淶
--(1)Bs_Viewinfo(��ͼ������Ϣ��)���ù淶
-- �ֶΣ�Viewname_Tx �ֶΣ�V_ģ������ĸ���_ģ����ĸ���
-- �ֶΣ�Viewdesc_Tx �ֶΣ�ģ������������->ģ������->��ͼ���� 
-- Example:
--Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
--values(@V_ViewID,'V_XTGL_XTSZ', 'ϵͳ����->ϵͳ����->��ѯ��ͼ');

--(2)Bs_Viewfunction��������ͼ�����ù淶
--�ֶΣ�




declare @V_Count int;
declare @V_StartID int;
declare @V_EndID int;
declare @V_StartName varchar(100);
declare @V_EndName varchar(100);

declare @V_MaxID int;
declare @V_MaxCount int;

declare @V_ViewID int;
declare @V_FunctionID int;
declare @V_ViewcolumnID int;
declare @V_Sortno_Nr int;

begin

--����ZX2642�б���������@V_ViewID��ʼֵ��ʼ--

set @V_MaxCount = 1000000;
set @V_StartID = 40000000;
set @V_StartName = 'ViewIDStart_ZX2642';
set @V_EndName = 'ViewIDEnd_ZX2642';

select @V_Count = count(*) from Bs_Viewinfo where Viewname_Tx = @V_StartName;
if @V_Count = 0 begin 

	select @V_ViewID = max(View_Id) from Bs_Viewinfo;
	select @V_FunctionID = max(Function_Id) from Bs_Viewfunction;
	select @V_ViewcolumnID = max(Viewcolumn_Id) from Bs_Viewcolumn;

	set @V_MaxID = @V_StartID;

	if @V_MaxID < @V_ViewID begin
		set @V_MaxID = @V_ViewID;
	end;

	if @V_MaxID < @V_FunctionID begin
		set @V_MaxID = @V_FunctionID;
	end;

	if @V_MaxID < @V_ViewcolumnID begin
		set @V_MaxID = @V_ViewcolumnID;
	end;

	set @V_StartID = @V_MaxID + 1;
	set @V_EndID = @V_StartID + @V_MaxCount - 1;

	insert into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx) values(@V_StartID, @V_StartName, @V_StartName);
	insert into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx) values(@V_EndID, @V_EndName, @V_EndName);

end;

select @V_StartID = View_Id from Bs_Viewinfo where Viewname_Tx = @V_StartName;
select @V_EndID = View_Id from Bs_Viewinfo where Viewname_Tx = @V_EndName;

set @V_ViewID = @V_StartID;
set @V_FunctionID = @V_ViewID;
set @V_ViewcolumnID = @V_ViewID;

--����ZX2642�б���������@V_ViewID��ʼֵ����--


--ɾ���û�����
delete From Bs_Listconfigforuser;

delete From Bs_Viewfunccolumns where Viewcolumn_Id >= @V_StartID and Viewcolumn_Id <= @V_EndID;
delete From Bs_Viewfunction where Function_Id >= @V_StartID and Function_Id <= @V_EndID;
delete From Bs_Viewcolumn where Viewcolumn_Id >= @V_StartID and Viewcolumn_Id <= @V_EndID;
delete From BS_ViewParameter where View_ID >= @V_StartID and View_ID <= @V_EndID;
delete From Bs_Viewinfo where View_Id > @V_StartID and View_Id < @V_EndID;


/*---------------------�豸�����ѯ��ͼ(��ʼ)---------------------------*/

set @V_ViewID =  @V_ViewID + 1;
set @V_FunctionID =  @V_FunctionID + 1;
set @V_Sortno_Nr = 1;
--������ͼ������ϢBs_Viewinfo(1)

Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(@V_ViewID,'V_JCXX_ZXMOBJECT', '������Ϣ->�豸���� ��ѯ��ͼ');

--�����ѯ�б���ͼbs_viewfunction(2)
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(@V_FunctionID,@V_ViewID,'�豸����','ZXMObjMM','mobject_id','wgv_AppBaseInfo_ZXMObject','0');

--������ͼ������ϢBs_Viewcolumn(3)��Bs_Viewfunccolumns(4)
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'MOBJECT_ID','�豸ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','1','1',2);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'ORG_ID','��˾ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'MOBJECT_CD','�豸����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',60,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'MOBJECTNAME_TX','�豸����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',80,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'SPEC_ID','רҵID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'GGXH_TX','����ͺ�','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'REPAIRTYPE_ID','ά�޲���ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'PROPERTY_ID','�豸����ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'DJOWNER_ID','��췽ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'JXDEPT_ID','���޷�ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'SBMS_TX','�豸����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'JSCS_TX','��������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'ZJL_NR','װ����','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'ZJLDW_TX','װ������λ','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'STATUS_ID','״̬ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'ZZCJ_TX','���쳧��','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'TYRQ_DT','��������','DateTime');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'ADDUSER_TX','������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'ADD_DT','����ʱ��','DateTime');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'ACTIVE_YN','�Ƿ�ɾ��','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'SORTNO_NR','�����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','1','1',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'AREA_ID','����ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'SPEC_TX','רҵ','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',30,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'REPAIRTYPE_TX','ά�޲���','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',30,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'DJOWNER_TX','���ֹ�','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',35,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'JXDEPT_TX','���޷ֹ�','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',35,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'AREA_TX','����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',40,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'STATUS_TX','�豸״̬','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',40,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'PROPERTYName_TX','�豸����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',35,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
/*---------------------�豸�����ѯ��ͼ(����)---------------------------*/ 

/*---------------------��Ź����ѯ��ͼ(��ʼ)---------------------------*/
set @V_ViewID =  @V_ViewID + 1;
set @V_FunctionID =  @V_FunctionID + 1;
set @V_Sortno_Nr = 1;
--������ͼ������ϢBs_Viewinfo(1)

Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(@V_ViewID,'V_JCXX_CodeRule', '��Ź��� ��ѯ��ͼ');

--�����ѯ�б���ͼbs_viewfunction(2)
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(@V_FunctionID,@V_ViewID,'��Ź���','CodeRuleMM','SNRule_ID','wgv_AppBaseInfo_CodeRuleList','0');

--������ͼ������ϢBs_Viewcolumn(3)��Bs_Viewfunccolumns(4)
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'SNRULE_ID','��Ź���ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'SNRULENAME_TX','��������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',100,'Left','1','1',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'RULE_TX','��Ź���','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',100,'Left','1','1',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
/*---------------------��Ź����ѯ��ͼ(����)---------------------------*/ 


/*---------------------��Ź��������ѯ��ͼ(��ʼ)---------------------------*/
set @V_ViewID =  @V_ViewID + 1;
set @V_FunctionID =  @V_FunctionID + 1;
set @V_Sortno_Nr = 1;
--������ͼ������ϢBs_Viewinfo(1)

Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(@V_ViewID,'V_JCXX_CodeRuleConfig', '��Ź������ ��ѯ��ͼ');

--�����ѯ�б���ͼbs_viewfunction(2)
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(@V_FunctionID,@V_ViewID,'��Ź������','CodeRuleMM','Module_CD','wgv_AppBaseInfo_CodeRuleConfig','0');

--������ͼ������ϢBs_Viewcolumn(3)��Bs_Viewfunccolumns(4)
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'SNRULE_ID','��Ź���ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,1,'1','1','0',100,'Left','1','1',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'Module_CD','ģ������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,2,'1','1','1',150,'Left','0','0',2);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'SNRULENAME_TX','��������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,3,'1','1','1',150,'Left','0','0',3);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'RULE_TX','��Ź���','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,4,'1','1','1',100,'Left','0','0',4);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
/*---------------------��Ź��������ѯ��ͼ(����)---------------------------*/ 

/*---------------------�û�ѡ�񴰿ڲ�ѯ�б��ѯ��ͼ(��ʼ)---------------------------*/
set @V_ViewID =  @V_ViewID + 1;
set @V_FunctionID =  @V_FunctionID + 1;
set @V_Sortno_Nr = 1;
--������ͼ������ϢBs_Viewinfo(1)

Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(@V_ViewID,'V_appuser', '�û�ѡ�񴰿ڲ�ѯ�б� ��ѯ��ͼ');

--�����ѯ�б���ͼbs_viewfunction(2)
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(@V_FunctionID,@V_ViewID,'�û�ѡ�񴰿ڲ�ѯ�б�','UserMM','appuser_id','wgv_AppBaseInfo_UCUserList','0');

--������ͼ������ϢBs_Viewcolumn(3)��Bs_Viewfunccolumns(4)
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'APPUSER_ID','�û�ID','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,0,'1','1','0',150,'Left','1','1',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'DEPT_ID','����ID','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,1,'1','1','0',150,'Left','1','1',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'DEPTNAME_TX','��������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,2,'1','1','1',100,'Left','1','1',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'NAME_TX','����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,3,'1','1','1',80,'Left','1','1',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'ACCOUNT_TX','�˺�','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,4,'1','1','1',80,'Left','1','1',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'APPUSER_CD','����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,5,'1','1','1',80,'Left','1','1',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'ORG_ID','��˾ID','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
/*---------------------�û�ѡ�񴰿ڲ�ѯ�б��ѯ��ͼ(����)---------------------------*/ 

/*---------------------ϵͳ����->��ͼ����  ��ѯ��ͼ(��ʼ)---------------------------*/
set @V_ViewID =  @V_ViewID + 1;
set @V_FunctionID =  @V_FunctionID + 1;
set @V_Sortno_Nr = 1;
--������ͼ������ϢBs_Viewinfo(1)

Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(@V_ViewID,'V_ViewInfo_ViewFunction', 'ϵͳ����->��ͼ���� ��ѯ��ͼ');

--�����ѯ�б���ͼbs_viewfunction(2)
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(@V_FunctionID,@V_ViewID,'ϵͳ����->��ͼ����','ViewFuncMM','ListName_TX','wgv_SysGridList','0');

--������ͼ������ϢBs_Viewcolumn(3)��Bs_Viewfunccolumns(4)
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'VIEWNAME_TX','��ͼ����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'VIEWDESC_TX','��ͼ����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'NAME_TX','��������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'APPACTION_CD','Ȩ��','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'DATAKEY_CD','����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'LISTNAME_TX','�б�����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'DATARANGE_YN','���ݷ�Χ','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'ENTITYTYPE_CD','����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'Function_ID','����ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;

/*---------------------ϵͳ����->��ͼ����  ��ѯ��ͼ(����)---------------------------*/


/*---------------------�������ѯ�б���ͼ(��ʼ)-----------------------------*/
set @V_ViewID =  @V_ViewID + 1;
set @V_FunctionID =  @V_FunctionID + 1;
--002�����ͼ������Ϣ
--������ͼ������Ϣ
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(@V_ViewID,'V_JCCS_CDGL', '����������->������ ��ѯ��ͼ');

--�����ѯ�б���ͼ
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(@V_FunctionID,@V_ViewID,'������','PointMM','Point_ID','wgv_JCCS_PointList','0');

--������ͼ������Ϣ
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'Point_ID','����','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,1,'1','1','0',5,'Left','1','1',3);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'MObjectName_TX','�豸����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,2,'1','1','0',80,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'PointName_TX','�������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,3,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'Desc_TX','������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,4,'1','1','1',120,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'DataType_TX','��������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,5,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'SignalType_TX','�ź�����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,6,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'EngUnit_TX','���̵�λ','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,7,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'PntDirect_TX','��㷽��','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,8,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'Rotation_TX','��ת����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,9,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'FeatureValue_TX','����ֵ����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,10,'1','1','1',80,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'StoreType_TX','�洢ģʽ','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,11,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'VarName_TX','����Դ����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,12,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'StationName_TX','�ɼ�����վ','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,13,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'ChannelNumber1_NR','�ɼ�ͨ��','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,14,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'ChannelType_TX','ͨ������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,15,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'MObject_ID','�豸���','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,16,'1','1','0',5,'Left','1','1',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'SortNo_NR','��������','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,17,'1','1','0',5,'Left','1','1',2);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
/*---------------------�������ѯ�б���ͼ(����)-----------------------------*/

/*---------------------����������ѯ�б���ͼ(��ʼ)-----------------------------*/
set @V_ViewID =  @V_ViewID + 1;
set @V_FunctionID =  @V_FunctionID + 1;
--002�����ͼ������Ϣ
--������ͼ������Ϣ
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(@V_ViewID,'V_JCCS_CDFZGL', '����������->��������� ��ѯ��ͼ');

--�����ѯ�б���ͼ
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(@V_FunctionID,@V_ViewID,'���������','PointGroupMM','Group_ID','wgv_JCCS_PointGroupList','0');

--������ͼ������Ϣ
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'Group_ID','����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,1,'1','1','0',5,'Left','1','1',4);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'StationName_TX','�ɼ�����վ','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,2,'1','1','1',60,'Left','1','1',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'LeftPointName_TX','���X','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,3,'1','1','1',60,'Left','1','1',2);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'LeftChannelNumber_NR','�ɼ�ͨ��X','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,4,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'RightPointName_TX','���Y','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,5,'1','1','1',60,'Left','1','1',3);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'RightChannelNumber_NR','�ɼ�ͨ��Y','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,6,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'LeftTopClearance_NR','����϶','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,7,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

/*---------------------����������ѯ�б���ͼ(����)-----------------------------*/

/*---------------------�ɼ�����վ�����ѯ�б���ͼ(��ʼ)-----------------------------*/
set @V_ViewID =  @V_ViewID + 1;
set @V_FunctionID =  @V_FunctionID + 1;
--002�ɼ�����վ��ͼ������Ϣ
--������ͼ������Ϣ
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(@V_ViewID,'V_SCZ_GZZGL', '����վ����->�ɼ�����վ���� ��ѯ��ͼ');

--�����ѯ�б���ͼ
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(@V_FunctionID,@V_ViewID,'�ɼ�����վ����','StationMM','Station_ID','wgv_SCZ_StationList','0');

--������ͼ������Ϣ
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'Station_ID','����','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,1,'1','1','0',5,'Left','1','1',4);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'StationName_TX','�ɼ�����վ����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,2,'1','1','1',50,'Left','1','1',3);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'StationType_TX','����վ����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,3,'1','1','1',50,'Left','1','1',2);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'StationSN_TX','����վ���','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,4,'1','1','1',50,'Left','1','1',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'IP_TX','����վIP','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,5,'1','1','1',50,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'QueryInterval_TX','���ݲ�ѯ���','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,6,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'WaveInterval_TX','�������ݱ�����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,7,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'StaticInterval_TX','��̬���ݱ�����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,8,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
/*---------------------�ɼ�����վ�����ѯ�б���ͼ(����)-----------------------------*/

/*---------------------�ɼ������������ѯ�б���ͼ(��ʼ)-----------------------------*/
set @V_ViewID =  @V_ViewID + 1;
set @V_FunctionID =  @V_FunctionID + 1;
--002�ɼ���������ͼ������Ϣ
--������ͼ������Ϣ
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(@V_ViewID,'V_SCZ_FWQGL', '����վ����->�ɼ����������� ��ѯ��ͼ');

--�����ѯ�б���ͼ
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(@V_FunctionID,@V_ViewID,'�ɼ�����������','ServerMM','Server_ID','wgv_SCZ_ServerList','0');

--������ͼ������Ϣ
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'Server_ID','����','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,1,'1','1','0',5,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'ServerName_TX','�ɼ�����������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,2,'1','1','1',50,'Left','1','1',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'ServerURL_TX','�����ַ','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,3,'1','1','1',50,'Left','1','1',2);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'ServerIP_TX','IP��ַ','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,4,'1','1','1',50,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

/*---------------------�ɼ������������ѯ�б���ͼ(����)-----------------------------*/

/*---------------------���ݲɼ��������ѯ�б���ͼ(��ʼ)-----------------------------*/
set @V_ViewID =  @V_ViewID + 1;
set @V_FunctionID =  @V_FunctionID + 1;
--002���ݲɼ�����ͼ������Ϣ
--������ͼ������Ϣ
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(@V_ViewID,'V_SCZ_CJQGL', '����վ����->���ݲɼ������� ��ѯ��ͼ');

--�����ѯ�б���ͼ
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(@V_FunctionID,@V_ViewID,'���ݲɼ�������','ServerDAUMM','DAU_ID','wgv_SCZ_DAUList','0');

--������ͼ������Ϣ
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'DAU_ID','����','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,1,'1','1','0',5,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'DAUName_TX','���ݲɼ�������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,2,'1','1','1',50,'Left','1','1',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'DAUURL_TX','�����ַ','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,3,'1','1','1',50,'Left','1','1',2);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'DAUIP_TX','IP��ַ','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,4,'1','1','1',50,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

/*---------------------���ݲɼ��������ѯ�б���ͼ(����)-----------------------------*/

/*---------------------���ݲɼ����ɼ�����վ�����ѯ�б���ͼ(��ʼ)-----------------------------*/
set @V_ViewID =  @V_ViewID + 1;
set @V_FunctionID =  @V_FunctionID + 1;
--002���ݲɼ����ɼ�����վ��ͼ������Ϣ
--������ͼ������Ϣ
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(@V_ViewID,'V_SCZ_CJQGZZGL', '����վ����->���ݲɼ����ɼ�����վ���� ��ѯ��ͼ');

--�����ѯ�б���ͼ
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(@V_FunctionID,@V_ViewID,'���ݲɼ����ɼ�����վ����','DAUStationMM','Station_ID','wgv_SCZ_DAUStationList','0');

--������ͼ������Ϣ
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'Station_ID','����','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,1,'1','1','0',5,'Left','1','1',4);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'OrgName_TX','��˾����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,2,'1','1','1',50,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'StationName_TX','�ɼ�����վ����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,3,'1','1','1',50,'Left','1','1',3);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'StationType_TX','����վ����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,4,'1','1','1',50,'Left','1','1',2);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'StationSN_TX','����վ���','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,5,'1','1','1',50,'Left','1','1',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'IP_TX','����վIP','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,6,'1','1','1',50,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'QueryInterval_TX','���ݲ�ѯ���','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,7,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'WaveInterval_TX','�������ݱ�����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,8,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'StaticInterval_TX','��̬���ݱ�����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,9,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
/*---------------------���ݲɼ����ɼ�����վ�����ѯ�б���ͼ(����)-----------------------------*/

/*---------------------���ݲɼ���ѡ��ɼ�����վ�б���ͼ(��ʼ)-----------------------------*/
set @V_ViewID =  @V_ViewID + 1;
set @V_FunctionID =  @V_FunctionID + 1;
--002�ɼ�����վ��ͼ������Ϣ
--������ͼ������Ϣ
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(@V_ViewID,'V_SCZ_GZZXZ', '����վ����->���ݲɼ�������->ѡ��ɼ�����վ ��ѯ��ͼ');

--�����ѯ�б���ͼ
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(@V_FunctionID,@V_ViewID,'���ݲɼ���ѡ��ɼ�����վ','StationMM','Station_ID','wgv_SCZ_StationSelectList','0');

--������ͼ������Ϣ
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'Station_ID','����','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,1,'1','1','0',5,'Left','1','1',4);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'DAUName_TX','���ݲɼ���','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,2,'1','1','1',50,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'StationName_TX','�ɼ�����վ����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,3,'1','1','1',50,'Left','1','1',3);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'StationType_TX','����վ����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,4,'1','1','1',50,'Left','1','1',2);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'StationSN_TX','����վ���','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,5,'1','1','1',50,'Left','1','1',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'IP_TX','����վIP','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,6,'1','1','1',50,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'QueryInterval_TX','���ݲ�ѯ���','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,7,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'WaveInterval_TX','�������ݱ�����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,8,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'StaticInterval_TX','��̬���ݱ�����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,9,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
/*---------------------���ݲɼ���ѡ��ɼ�����վ�б���ͼ(����)-----------------------------*/

/*---------------------�������ݲ�ѯ ��ʷժҪ���ݲ�ѯ�б���ͼ(��ʼ)------------------------*/
set @V_ViewID =  @V_ViewID + 1;
set @V_FunctionID =  @V_FunctionID + 1;
--001��ʷժҪ���ݲ�ѯ��ͼ������Ϣ
--������ͼ������Ϣ
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx,EntityType_CD)
values(@V_ViewID,'DataQueryPackage.Pr_QueryHistorySummary','�������ݲ�ѯ��ͼ','P');

Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_AppactionCD','ģ��CD','String',1,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_PostID','��λID','Int',2,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_OrgID','��˾ID','Int',3,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_MobjectID','�豸ID','Int',4,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_MobjectCD','�豸����','String',5,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_MobjectName','�豸����','String',6,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_PointID','���ID','Int',7,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_MinPartitionID','��ʼ����','Int',8,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_MaxPartitionID','��������','Int',9,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_IncludeSubMob','�Ƿ�������豸','Int',10,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_AlmLevel','�����ȼ�','Int',11,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_DataType','��������','Int',12,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_SignalTypeID','�ź�����ID','Int',13,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_PageSize','��ҳ��С','Int',14,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_PageIndex','ҳ��','Int',15,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_Count','��¼����','Int',16,'Output');

--�����ѯ�б���ͼ
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(@V_FunctionID,@V_ViewID,'�������ݲ�ѯ','HistoryDataMM','history_id','wgv_JCSJ_HistoryData','0');

--������ͼ������Ϣ
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'history_id','����','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,1,'1','1','0',5,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'mobname_tx','�豸����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,2,'1','1','1',100,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'mob_cd','�豸����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,3,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'pntname_tx','�������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,4,'1','1','1',100,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'desc_tx','�������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,5,'1','1','1',100,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'datatypename_tx','��������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,6,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'signaltypename_tx','�ź�����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,7,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'featurename_tx','����ֵ����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,8,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'measvalue_tx','����ֵ','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,9,'1','1','1',40,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'engname_tx','��λ','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,10,'1','1','1',30,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'almname_tx','�����ȼ�','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,11,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'sampletime_dt','����ʱ��','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,12,'1','1','1',100,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
/*---------------------�������ݲ�ѯ ��ʷժҪ���ݲ�ѯ�б���ͼ(����)------------------------*/

/*---------------------�������ݲ�ѯ ����б��ѯ��ͼ(��ʼ)------------------------*/
set @V_ViewID =  @V_ViewID + 1;
set @V_FunctionID =  @V_FunctionID + 1;
--001����б��ѯ��ͼ������Ϣ
--������ͼ������Ϣ
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx,EntityType_CD)
values(@V_ViewID,'DataQueryPackage.Pr_QueryPointList','����б��ѯ��ͼ','P');

Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_AppactionCD','ģ��CD','String',1,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_PostID','��λID','Int',2,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_OrgID','��˾ID','Int',3,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_MobjectID','�豸ID','Int',4,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_PointName','�������','String',5,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_IncludeSubMob','�Ƿ�������豸','Int',6,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_PageSize','��ҳ��С','Int',7,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_PageIndex','ҳ��','Int',8,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_Count','��¼����','Int',9,'Output');

--�����ѯ�б���ͼ
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(@V_FunctionID,@V_ViewID,'����б��ѯ','PointMM','point_id','wgv_JCSJ_PointList','0');

--������ͼ������Ϣ
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'point_id','����','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,1,'1','1','0',5,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'mobname_tx','�豸����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,2,'1','1','1',80,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'mob_cd','�豸����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,3,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'pntname_tx','�������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,4,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'desc_tx','�������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,5,'1','1','1',200,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
/*---------------------�������ݲ�ѯ ����ѯ�б���ͼ(����)------------------------*/


/*---------------------���߰�ͼ�б��ѯ��ͼ(��ʼ)---------------------------*/
set @V_ViewID =  @V_ViewID + 1;
set @V_FunctionID =  @V_FunctionID + 1;
set @V_Sortno_Nr = 1;
--������ͼ������ϢBs_Viewinfo(1)

Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(@V_ViewID,'V_AnalysisBarGroup', '���߰�ͼ�б� ��ѯ��ͼ');

--�����ѯ�б���ͼbs_viewfunction(2)
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(@V_FunctionID,@V_ViewID,'���߰�ͼ�б�','BarDiagramMM','BARGROUP_ID','wgv_AnalysisBarGroup_BarGoupList','0');

--������ͼ������ϢBs_Viewcolumn(3)��Bs_Viewfunccolumns(4)
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'BARGROUP_ID','����ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'GROUPNAME_TX','��������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,1,'1','1','1',400,'Left','1','1',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'BARDIAGRAM_ID','��ͼID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'SORTNO_NR','�����','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr = @V_Sortno_Nr+1;
/*---------------------���߰�ͼ�б��ѯ��ͼ(����)---------------------------*/ 

/*---------------------����Դ���������б���ͼ(��ʼ)-----------------------------*/
set @V_ViewID =  @V_ViewID + 1;
set @V_FunctionID =  @V_FunctionID + 1;
--002����Դ����������ͼ������Ϣ
--������ͼ������Ϣ
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(@V_ViewID,'V_JCCS_SJYBLGL', '����վ����->����Դ�������� ��ѯ��ͼ');

--�����ѯ�б���ͼ
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(@V_FunctionID,@V_ViewID,'����Դ��������','DataVarMM','Var_ID','wgv_JCCS_DataVarList','0');

--������ͼ������Ϣ
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'Var_ID','����','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,1,'1','1','0',5,'Left','1','1',2);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'VarName_TX','������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,2,'1','1','1',100,'Left','1','1',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'VarScale_NR','������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,3,'1','1','1',100,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'VarOffset_NR','ƫ����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,4,'1','1','1',100,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'VarDesc_TX','��������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,5,'1','1','1',200,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'PointName_TX','���','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,6,'1','1','1',200,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
/*---------------------����Դ���������б���ͼ(����)-----------------------------*/


--004��п���ͼ������Ϣ

set @V_ViewID =  @V_ViewID + 1;
set @V_FunctionID =  @V_FunctionID + 1;
set @V_Sortno_Nr =  1;

--������ͼ������Ϣ
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx,EntityType_CD)
values(@V_ViewID,'QueryBearings', '������->��п� ��ѯ��ͼ','P');

Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_Factory','��������','String',1,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_Model','��Ʒ�ͺ�','String',2,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_Speed','ת��','Int',3,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_PageSize','��ҳ��С','Int',4,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_PageIndex','ҳ��','Int',5,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_Count','��¼����','Int',6,'Output');

--�����ѯ�б���ͼ
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn,Datarange_CD)
values(@V_FunctionID,@V_ViewID,'��п�','BearingMM','Bearing_ID','wgv_AppAnalysis_Bearing','0','');

--������ͼ������Ϣ
--����
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'Bearing_ID','����','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',10,'Left','1','1',2);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr =  @V_Sortno_Nr + 1;

--����ͺ�
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'Model_TX','����ͺ�','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',100,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr =  @V_Sortno_Nr + 1;

--��������
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'RollerCount_NR','��������','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',100,'Right','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr =  @V_Sortno_Nr + 1;

--������Ƶ��
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'BSF_TX','������Ƶ��(HZ)','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',80,'Right','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr =  @V_Sortno_Nr + 1;

--���ּ�Ƶ��
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'FTF_TX','���ּ�Ƶ��(HZ)','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',80,'Right','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr =  @V_Sortno_Nr + 1;

--��ȦƵ��
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'BBPFO_TX','��ȦƵ��(HZ)','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',80,'Right','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr =  @V_Sortno_Nr + 1;

--��ȦƵ��
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'BPFI_TX','��ȦƵ��(HZ)','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,4,'1','1','1',80,'Right','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr =  @V_Sortno_Nr + 1;

--�Ƿ�����
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'IsInLay_YN','�Ƿ�����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',80,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr =  @V_Sortno_Nr + 1;

--��ע
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'Description_TX','��ע','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',100,'Left','0','0',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr =  @V_Sortno_Nr + 1;


set @V_ViewID =  @V_ViewID + 1;
set @V_FunctionID =  @V_FunctionID + 1;
--005����Ƶ�ʷ�����ͼ������Ϣ
--������ͼ������Ϣ
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(@V_ViewID,'V_JCCS_TZPLFZ', '������ݹ���->����Ƶ�ʹ��� ����Ƶ�ʷ����ѯ��ͼ');

--�����ѯ�б���ͼ
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(@V_FunctionID,@V_ViewID,'����Ƶ�ʹ���','FeatureFreqMM','Group_ID','wgv_JCCS_FeatureFreqGroupList','0');

--������ͼ������Ϣ
--����
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'Group_ID','����','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,1,'1','1','0',10,'Left','1','1',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

--��������
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'GroupName_TX','��������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,2,'1','1','1',100,'Left','0','0',2);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

--��˾ID
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'Org_ID','��˾ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,3,'1','1','0',100,'Left','1','1',3);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;


set @V_ViewID =  @V_ViewID + 1;
set @V_FunctionID =  @V_FunctionID + 1;
--006��������Ƶ�ʷ�����ͼ������Ϣ
--������ͼ������Ϣ
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(@V_ViewID,'V_JCCS_GLTZPL', '������ݹ���->����Ƶ�ʹ��� ��������Ƶ�ʲ�ѯ��ͼ');

--�����ѯ�б���ͼ
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(@V_FunctionID,@V_ViewID,'����Ƶ�ʹ���','FeatureFreqMM','Group_ID','wgv_JCCS_RealatedFeatureFreqGroupList','0');

--������ͼ������Ϣ
--����
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'Group_ID','����','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,1,'1','1','0',10,'Left','1','1',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

--��������
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'GroupName_TX','��������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,2,'1','1','1',100,'Left','0','0',2);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

--�豸ID
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'MObject_ID','�豸ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,3,'1','1','0',100,'Left','1','1',3);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;


set @V_ViewID =  @V_ViewID + 1;
set @V_FunctionID =  @V_FunctionID + 1;
--005����Ƶ����ͼ������Ϣ
--������ͼ������Ϣ
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(@V_ViewID,'V_JCCS_TZPL', '������ݹ���->����Ƶ�ʹ��� ����Ƶ�ʲ�ѯ��ͼ');

--�����ѯ�б���ͼ
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(@V_FunctionID,@V_ViewID,'����Ƶ�ʹ���','FeatureFreqMM','FeatureFreq_ID','wgv_JCCS_FeatureFreqList','0');

--������ͼ������Ϣ
--����
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'FeatureFreq_ID','����','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,1,'1','1','0',10,'Left','1','1',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

--����Ƶ������
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'Name_TX','����Ƶ������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,2,'1','1','1',200,'Left','0','0',2);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

--����Ƶ��ֵ
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'FeatureFreqValue_NR','����Ƶ��ֵ','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,3,'1','1','1',200,'Left','0','0',3);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

--��λ
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'Unit_NR','��λ','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,4,'1','1','1',200,'Left','0','0',4);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

--����ID
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'Group_ID','����ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,5,'1','1','0',100,'Left','1','1',5);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;


--004δ����������Ƶ�ʷ�����ͼ������Ϣ

set @V_ViewID =  @V_ViewID + 1;
set @V_FunctionID =  @V_FunctionID + 1;
set @V_Sortno_Nr =  1;

--������ͼ������Ϣ
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx,EntityType_CD)
values(@V_ViewID,'DataQueryPackage.pr_QueryNonRelatedFeatureGroup', '������ݹ���->����Ƶ�ʹ��� ����Ƶ�ʷ��������ͼ','P');

Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_OrgID','��˾ID','Int',1,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_MobjectID','�豸ID','Int',2,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_PageSize','��ҳ��С','Int',3,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_PageIndex','ҳ��','Int',4,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_Count','��¼����','Int',5,'Output');

--�����ѯ�б���ͼ
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn,Datarange_CD)
values(@V_FunctionID,@V_ViewID,'δ��������Ƶ�ʷ���','FeatureFreq_REL','Group_ID','wgv_JCCS_NonRelatedFreqGroupList','0','');

--������ͼ������Ϣ
--����
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'Group_ID','����','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','0',10,'Left','1','1',1);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_Sortno_Nr =  @V_Sortno_Nr + 1;

--��������
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'GroupName_TX','��������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,@V_Sortno_Nr,'1','1','1',200,'Left','0','0',2);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

/*---------------------����ͳ�Ʋ�ѯ ����ͳ�Ʋ�ѯ��ͼ(��ʼ)------------------------*/
set @V_ViewID =  @V_ViewID + 1;
set @V_FunctionID =  @V_FunctionID + 1;
--001���Ź����ѯ��ͼ������Ϣ
--������ͼ������Ϣ
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx,EntityType_CD)
values(@V_ViewID,'SMSQueryPackage.Pr_QuerySMSHistory','����ͳ�Ʋ�ѯ��ͼ','P');

Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_AppactionCD','ģ��CD','String',1,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_PostID','��λID','Int',2,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_OrgID','��˾ID','Int',3,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_MobjectID','�豸ID','Int',4,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_MobjectCD','�豸����','String',5,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_MobjectName','�豸����','String',6,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_PointID','���ID','Int',7,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_MinPartitionID','��ʼ����','Int',8,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_MaxPartitionID','��������','Int',9,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_IncludeSubMob','�Ƿ�������豸','Int',10,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_AlmLevel','�����ȼ�','Int',11,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_PageSize','��ҳ��С','Int',14,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_PageIndex','ҳ��','Int',15,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(@V_ViewID,'P_Count','��¼����','Int',16,'Output');

--�����ѯ�б���ͼ
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(@V_FunctionID,@V_ViewID,'����ͳ�Ʋ�ѯ','SMSHistoryMM','SMSHistory_ID','wgv_DXTJ_SMSHistory','0');

--������ͼ������Ϣ
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'SMSHistory_ID','����','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,1,'1','1','0',5,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'mobname_tx','�豸����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,2,'1','1','1',100,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'mob_cd','�豸����','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,3,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'pntname_tx','�������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,4,'1','1','1',100,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'smscontent_tx','��������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,4,'1','1','1',100,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'name_tx','������','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,4,'1','1','1',100,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'sendtime_tm','����ʱ��','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,4,'1','1','1',100,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'status_tx','����״̬','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,4,'1','1','1',100,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'almname_tx','�����ȼ�','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,11,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(@V_ViewcolumnID,@V_ViewID,'memo_tx','��ע','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(@V_FunctionID,@V_ViewcolumnID,11,'1','1','1',60,'Left','0','0',0);
set @V_ViewcolumnID =  @V_ViewcolumnID + 1;
/*---------------------����ͳ�Ʋ�ѯ ����ͳ���б���ͼ(����)------------------------*/


End;

