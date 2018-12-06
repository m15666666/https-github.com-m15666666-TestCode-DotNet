--配置规范
--(1)Bs_Viewinfo(视图基本信息表)配置规范
-- 字段：Viewname_Tx 字段：V_模块组字母简称_模块字母简称
-- 字段：Viewdesc_Tx 字段：模块组中文名称->模块名称->视图名称 
-- Example:
--Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
--values(V_ViewID,'V_XTGL_XTSZ', '系统管理->系统参数->查询视图');

--(2)Bs_Viewfunction（功能视图）配置规范
--字段：




declare

V_Count BS_MetaFieldType.Int_NR%type;
V_StartID BS_MetaFieldType.Int_NR%type;
V_EndID BS_MetaFieldType.Int_NR%type;
V_StartName BS_MetaFieldType.Fifty_TX%type;
V_EndName BS_MetaFieldType.Fifty_TX%type;

V_MaxID BS_MetaFieldType.Int_NR%type;
V_MaxCount BS_MetaFieldType.Int_NR%type;

V_ViewID BS_MetaFieldType.Int_NR%type;
V_FunctionID BS_MetaFieldType.Int_NR%type;
V_ViewcolumnID BS_MetaFieldType.Int_NR%type;
V_Sortno_Nr BS_MetaFieldType.Int_NR%type;

begin

--分配ZX2642列表配置数据V_ViewID初始值开始--

V_MaxCount := 1000000;
V_StartID := 40000000;
V_StartName := 'ViewIDStart_ZX2642';
V_EndName := 'ViewIDEnd_ZX2642';

select count(*) into V_Count from Bs_Viewinfo where Viewname_Tx = V_StartName;
if V_Count = 0 then 

  select max(View_Id) into V_ViewID from Bs_Viewinfo;
  select max(Function_Id) into V_FunctionID from Bs_Viewfunction;
  select max(Viewcolumn_Id) into V_ViewcolumnID from Bs_Viewcolumn;

  V_MaxID := V_StartID;

  if V_MaxID < V_ViewID then
    V_MaxID := V_ViewID;
  end if;

  if V_MaxID < V_FunctionID then
    V_MaxID := V_FunctionID;
  end if;

  if V_MaxID < V_ViewcolumnID then
    V_MaxID := V_ViewcolumnID;
  end if;

  V_StartID := V_MaxID + 1;
  V_EndID := V_StartID + V_MaxCount - 1;

  insert into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx) values(V_StartID, V_StartName, V_StartName);
  insert into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx) values(V_EndID, V_EndName, V_EndName);

end if;

select View_Id into V_StartID from Bs_Viewinfo where Viewname_Tx = V_StartName;
select View_Id into V_EndID from Bs_Viewinfo where Viewname_Tx = V_EndName;

V_ViewID := V_StartID;
V_FunctionID := V_ViewID;
V_ViewcolumnID := V_ViewID;

--分配ZX2642列表配置数据V_ViewID初始值结束--


--删除用户配置
delete From Bs_Listconfigforuser;
commit;

delete From Bs_Viewfunccolumns where Viewcolumn_Id >= V_StartID and Viewcolumn_Id <= V_EndID;
commit;
delete From Bs_Viewfunction where Function_Id >= V_StartID and Function_Id <= V_EndID;
commit;
delete From Bs_Viewcolumn where Viewcolumn_Id >= V_StartID and Viewcolumn_Id <= V_EndID;
commit;
delete From BS_ViewParameter where View_ID >= V_StartID and View_ID <= V_EndID;
commit;
delete From Bs_Viewinfo where View_Id > V_StartID and View_Id < V_EndID;
commit;



/*---------------------设备管理查询视图(开始)---------------------------*/

V_ViewID := V_ViewID + 1;
V_FunctionID := V_FunctionID + 1;
V_Sortno_Nr :=1;
--插入视图基本信息Bs_Viewinfo(1)

Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(V_ViewID,'V_JCXX_ZXMOBJECT', '基础信息->设备管理 查询视图');
commit;

--插入查询列表视图bs_viewfunction(2)
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(V_FunctionID,V_ViewID,'设备管理','ZXMObjMM','mobject_id','wgv_AppBaseInfo_ZXMObject','0');

--插入视图的列信息Bs_Viewcolumn(3)和Bs_Viewfunccolumns(4)
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'MOBJECT_ID','设备ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','1','1',2);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'ORG_ID','公司ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'MOBJECT_CD','设备编码','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',60,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'MOBJECTNAME_TX','设备名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',80,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'SPEC_ID','专业ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'GGXH_TX','规格型号','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'REPAIRTYPE_ID','维修策略ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'PROPERTY_ID','设备属性ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'DJOWNER_ID','点检方ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'JXDEPT_ID','检修方ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'SBMS_TX','设备描述','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'JSCS_TX','技术参数','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'ZJL_NR','装机量','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'ZJLDW_TX','装机量单位','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'STATUS_ID','状态ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'ZZCJ_TX','制造厂家','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'TYRQ_DT','托运日期','Date');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'ADDUSER_TX','创建人','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'ADD_DT','创建时间','Date');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'ACTIVE_YN','是否删除','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'SORTNO_NR','排序号','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','1','1',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'AREA_ID','区域ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'SPEC_TX','专业','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',30,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'REPAIRTYPE_TX','维修策略','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',30,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'DJOWNER_TX','点检分工','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',35,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'JXDEPT_TX','检修分工','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',35,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'AREA_TX','区域','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',40,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'STATUS_TX','设备状态','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',40,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'PROPERTYName_TX','设备属性','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',35,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
commit;
/*---------------------设备管理查询视图(结束)---------------------------*/ 

/*---------------------编号规则查询视图(开始)---------------------------*/
V_ViewID := V_ViewID + 1;
V_FunctionID := V_FunctionID + 1;
V_Sortno_Nr :=1;
--插入视图基本信息Bs_Viewinfo(1)

Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(V_ViewID,'V_JCXX_CodeRule', '编号规则 查询视图');
commit;

--插入查询列表视图bs_viewfunction(2)
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(V_FunctionID,V_ViewID,'编号规则','CodeRuleMM','SNRule_ID','wgv_AppBaseInfo_CodeRuleList','0');

--插入视图的列信息Bs_Viewcolumn(3)和Bs_Viewfunccolumns(4)
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'SNRULE_ID','编号规则ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'SNRULENAME_TX','规则名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',100,'Left','1','1',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'RULE_TX','编号规则','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',100,'Left','1','1',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
commit;
/*---------------------编号规则查询视图(结束)---------------------------*/ 


/*---------------------编号规则关联查询视图(开始)---------------------------*/
V_ViewID := V_ViewID + 1;
V_FunctionID := V_FunctionID + 1;
V_Sortno_Nr :=1;
--插入视图基本信息Bs_Viewinfo(1)

Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(V_ViewID,'V_JCXX_CodeRuleConfig', '编号规则关联 查询视图');
commit;

--插入查询列表视图bs_viewfunction(2)
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(V_FunctionID,V_ViewID,'编号规则关联','CodeRuleMM','Module_CD','wgv_AppBaseInfo_CodeRuleConfig','0');

--插入视图的列信息Bs_Viewcolumn(3)和Bs_Viewfunccolumns(4)
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'SNRULE_ID','编号规则ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,1,'1','1','0',100,'Left','1','1',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'Module_CD','模块名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,2,'1','1','1',150,'Left','0','0',2);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'SNRULENAME_TX','规则名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,3,'1','1','1',150,'Left','0','0',3);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'RULE_TX','编号规则','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,4,'1','1','1',100,'Left','0','0',4);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
commit;
/*---------------------编号规则关联查询视图(结束)---------------------------*/ 

/*---------------------用户选择窗口查询列表查询视图(开始)---------------------------*/
V_ViewID := V_ViewID + 1;
V_FunctionID := V_FunctionID + 1;
V_Sortno_Nr :=1;
--插入视图基本信息Bs_Viewinfo(1)

Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(V_ViewID,'v_appuser', '用户选择窗口查询列表 查询视图');
commit;

--插入查询列表视图bs_viewfunction(2)
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(V_FunctionID,V_ViewID,'用户选择窗口查询列表','UserMM','appuser_id','wgv_AppBaseInfo_UCUserList','0');

--插入视图的列信息Bs_Viewcolumn(3)和Bs_Viewfunccolumns(4)
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'APPUSER_ID','用户ID','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,0,'1','1','0',150,'Left','1','1',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'DEPT_ID','部门ID','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,1,'1','1','0',150,'Left','1','1',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'DEPTNAME_TX','部门名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,2,'1','1','1',100,'Left','1','1',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'NAME_TX','姓名','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,3,'1','1','1',80,'Left','1','1',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'ACCOUNT_TX','账号','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,4,'1','1','1',80,'Left','1','1',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'APPUSER_CD','工号','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,5,'1','1','1',80,'Left','1','1',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'ORG_ID','公司ID','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
commit;
/*---------------------用户选择窗口查询列表查询视图(结束)---------------------------*/ 

/*---------------------系统管理->视图设置  查询视图(开始)---------------------------*/
V_ViewID := V_ViewID + 1;
V_FunctionID := V_FunctionID + 1;
V_Sortno_Nr :=1;
--插入视图基本信息Bs_Viewinfo(1)

Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(V_ViewID,'V_ViewInfo_ViewFunction', '系统管理->视图设置 查询视图');
commit;

--插入查询列表视图bs_viewfunction(2)
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(V_FunctionID,V_ViewID,'系统管理->视图设置','ViewFuncMM','ListName_TX','wgv_SysGridList','0');

--插入视图的列信息Bs_Viewcolumn(3)和Bs_Viewfunccolumns(4)
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'VIEWNAME_TX','视图名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'VIEWDESC_TX','视图描述','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'NAME_TX','功能名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'APPACTION_CD','权限','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'DATAKEY_CD','主键','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'LISTNAME_TX','列表名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'DATARANGE_YN','数据范围','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'ENTITYTYPE_CD','类型','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'Function_ID','功能ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;

commit;
/*---------------------系统管理->视图设置  查询视图(结束)---------------------------*/


/*---------------------测点管理查询列表视图(开始)-----------------------------*/
V_ViewID := V_ViewID + 1;
V_FunctionID := V_FunctionID + 1;
--002测点视图配置信息
--插入视图基本信息
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(V_ViewID,'V_JCCS_CDGL', '监测参数管理->测点管理 查询视图');
commit;

--插入查询列表视图
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(V_FunctionID,V_ViewID,'测点管理','PointMM','Point_ID','wgv_JCCS_PointList','0');

--插入视图的列信息
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'Point_ID','主键','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,1,'1','1','0',5,'Left','1','1',3);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'MObjectName_TX','设备名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,2,'1','1','0',80,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'PointName_TX','测点名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,3,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'Desc_TX','测点编码','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,4,'1','1','1',120,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'DataType_TX','数据类型','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,5,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'SignalType_TX','信号类型','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,6,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'EngUnit_TX','工程单位','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,7,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'PntDirect_TX','测点方向','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,8,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'Rotation_TX','旋转方向','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,9,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'FeatureValue_TX','测量值类型','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,10,'1','1','1',80,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'StoreType_TX','存储模式','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,11,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'VarName_TX','数据源变量','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,12,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'StationName_TX','采集工作站','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,13,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'ChannelNumber1_NR','采集通道','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,14,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'ChannelType_TX','通道类型','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,15,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'MObject_ID','设备编号','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,16,'1','1','0',5,'Left','1','1',1);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'SortNo_NR','测点排序号','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,17,'1','1','0',5,'Left','1','1',2);
V_ViewcolumnID := V_ViewcolumnID + 1;
commit;
/*---------------------测点管理查询列表视图(结束)-----------------------------*/

/*---------------------测点分组管理查询列表视图(开始)-----------------------------*/
V_ViewID := V_ViewID + 1;
V_FunctionID := V_FunctionID + 1;
--002测点视图配置信息
--插入视图基本信息
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(V_ViewID,'V_JCCS_CDFZGL', '监测参数管理->测点分组管理 查询视图');
commit;

--插入查询列表视图
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(V_FunctionID,V_ViewID,'测点分组管理','PointGroupMM','Group_ID','wgv_JCCS_PointGroupList','0');

--插入视图的列信息
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'Group_ID','主键','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,1,'1','1','0',5,'Left','1','1',4);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'StationName_TX','采集工作站','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,2,'1','1','1',60,'Left','1','1',1);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'LeftPointName_TX','测点X','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,3,'1','1','1',60,'Left','1','1',2);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'LeftChannelNumber_NR','采集通道X','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,4,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'RightPointName_TX','测点Y','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,5,'1','1','1',60,'Left','1','1',3);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'RightChannelNumber_NR','采集通道Y','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,6,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'LeftTopClearance_NR','顶间隙','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,7,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;
commit;
/*---------------------测点分组管理查询列表视图(结束)-----------------------------*/

/*---------------------采集工作站管理查询列表视图(开始)-----------------------------*/
V_ViewID := V_ViewID + 1;
V_FunctionID := V_FunctionID + 1;
--002采集工作站视图配置信息
--插入视图基本信息
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(V_ViewID,'V_SCZ_GZZGL', '数采站管理->采集工作站管理 查询视图');
commit;

--插入查询列表视图
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(V_FunctionID,V_ViewID,'采集工作站管理','StationMM','Station_ID','wgv_SCZ_StationList','0');

--插入视图的列信息
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'Station_ID','主键','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,1,'1','1','0',5,'Left','1','1',4);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'StationName_TX','采集工作站名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,2,'1','1','1',50,'Left','1','1',3);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'StationType_TX','工作站类型','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,3,'1','1','1',50,'Left','1','1',2);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'StationSN_TX','工作站序号','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,4,'1','1','1',50,'Left','1','1',1);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'IP_TX','工作站IP','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,5,'1','1','1',50,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'QueryInterval_TX','数据查询间隔','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,6,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'WaveInterval_TX','波形数据保存间隔','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,7,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'StaticInterval_TX','静态数据保存间隔','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,8,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;
commit;
/*---------------------采集工作站管理查询列表视图(结束)-----------------------------*/

/*---------------------采集服务器管理查询列表视图(开始)-----------------------------*/
V_ViewID := V_ViewID + 1;
V_FunctionID := V_FunctionID + 1;
--002采集服务器视图配置信息
--插入视图基本信息
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(V_ViewID,'V_SCZ_FWQGL', '数采站管理->采集服务器管理 查询视图');
commit;

--插入查询列表视图
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(V_FunctionID,V_ViewID,'采集服务器管理','ServerMM','Server_ID','wgv_SCZ_ServerList','0');

--插入视图的列信息
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'Server_ID','主键','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,1,'1','1','0',5,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'ServerName_TX','采集服务器名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,2,'1','1','1',50,'Left','1','1',1);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'ServerURL_TX','服务地址','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,3,'1','1','1',50,'Left','1','1',2);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'ServerIP_TX','IP地址','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,4,'1','1','1',50,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

commit;
/*---------------------采集服务器管理查询列表视图(结束)-----------------------------*/

/*---------------------数据采集器管理查询列表视图(开始)-----------------------------*/
V_ViewID := V_ViewID + 1;
V_FunctionID := V_FunctionID + 1;
--002数据采集器视图配置信息
--插入视图基本信息
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(V_ViewID,'V_SCZ_CJQGL', '数采站管理->数据采集器管理 查询视图');
commit;

--插入查询列表视图
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(V_FunctionID,V_ViewID,'数据采集器管理','ServerDAUMM','DAU_ID','wgv_SCZ_DAUList','0');

--插入视图的列信息
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'DAU_ID','主键','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,1,'1','1','0',5,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'DAUName_TX','数据采集器名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,2,'1','1','1',50,'Left','1','1',1);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'DAUURL_TX','服务地址','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,3,'1','1','1',50,'Left','1','1',2);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'DAUIP_TX','IP地址','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,4,'1','1','1',50,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

commit;
/*---------------------数据采集器管理查询列表视图(结束)-----------------------------*/

/*---------------------数据采集器采集工作站管理查询列表视图(开始)-----------------------------*/
V_ViewID := V_ViewID + 1;
V_FunctionID := V_FunctionID + 1;
--002数据采集器采集工作站视图配置信息
--插入视图基本信息
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(V_ViewID,'V_SCZ_CJQGZZGL', '数采站管理->数据采集器采集工作站管理 查询视图');
commit;

--插入查询列表视图
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(V_FunctionID,V_ViewID,'数据采集器采集工作站管理','DAUStationMM','Station_ID','wgv_SCZ_DAUStationList','0');

--插入视图的列信息
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'Station_ID','主键','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,1,'1','1','0',5,'Left','1','1',4);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'OrgName_TX','公司名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,2,'1','1','1',50,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'StationName_TX','采集工作站名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,3,'1','1','1',50,'Left','1','1',3);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'StationType_TX','工作站类型','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,4,'1','1','1',50,'Left','1','1',2);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'StationSN_TX','工作站序号','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,5,'1','1','1',50,'Left','1','1',1);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'IP_TX','工作站IP','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,6,'1','1','1',50,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'QueryInterval_TX','数据查询间隔','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,7,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'WaveInterval_TX','波形数据保存间隔','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,8,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'StaticInterval_TX','静态数据保存间隔','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,9,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;
commit;
/*---------------------数据采集器采集工作站管理查询列表视图(结束)-----------------------------*/

/*---------------------数据采集器选择采集工作站列表视图(开始)-----------------------------*/
V_ViewID := V_ViewID + 1;
V_FunctionID := V_FunctionID + 1;
--002采集工作站视图配置信息
--插入视图基本信息
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(V_ViewID,'V_SCZ_GZZXZ', '数采站管理->数据采集器管理->选择采集工作站 查询视图');
commit;

--插入查询列表视图
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(V_FunctionID,V_ViewID,'数据采集器选择采集工作站','StationMM','Station_ID','wgv_SCZ_StationSelectList','0');

--插入视图的列信息
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'Station_ID','主键','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,1,'1','1','0',5,'Left','1','1',4);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'DAUName_TX','数据采集器','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,2,'1','1','1',50,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'StationName_TX','采集工作站名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,3,'1','1','1',50,'Left','1','1',3);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'StationType_TX','工作站类型','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,4,'1','1','1',50,'Left','1','1',2);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'StationSN_TX','工作站序号','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,5,'1','1','1',50,'Left','1','1',1);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'IP_TX','工作站IP','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,6,'1','1','1',50,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'QueryInterval_TX','数据查询间隔','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,7,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'WaveInterval_TX','波形数据保存间隔','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,8,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'StaticInterval_TX','静态数据保存间隔','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,9,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;
commit;
/*---------------------数据采集器选择采集工作站列表视图(结束)-----------------------------*/

/*---------------------在线数据查询 历史摘要数据查询列表视图(开始)------------------------*/
V_ViewID := V_ViewID + 1;
V_FunctionID := V_FunctionID + 1;
--001历史摘要数据查询视图配置信息
--插入视图基本信息
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx,EntityType_CD)
values(V_ViewID,'DataQueryPackage.Pr_QueryHistorySummary','在线数据查询视图','P');
commit;

Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_AppactionCD','模块CD','String',1,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_PostID','岗位ID','Int',2,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_OrgID','公司ID','Int',3,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_MobjectID','设备ID','Int',4,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_MobjectCD','设备编码','String',5,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_MobjectName','设备名称','String',6,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_PointID','测点ID','Int',7,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_MinPartitionID','起始分区','Int',8,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_MaxPartitionID','结束分区','Int',9,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_IncludeSubMob','是否包含子设备','Int',10,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_AlmLevel','报警等级','Int',11,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_DataType','数据类型','Int',12,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_SignalTypeID','信号类型ID','Int',13,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_PageSize','分页大小','Int',14,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_PageIndex','页码','Int',15,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_Count','记录总数','Int',16,'Output');

--插入查询列表视图
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(V_FunctionID,V_ViewID,'在线数据查询','HistoryDataMM','history_id','wgv_JCSJ_HistoryData','0');

--插入视图的列信息
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'history_id','主键','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,1,'1','1','0',5,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'mobname_tx','设备名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,2,'1','1','1',100,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'mob_cd','设备编码','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,3,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'pntname_tx','测点名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,4,'1','1','1',100,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'desc_tx','测点描述','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,5,'1','1','1',100,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'datatypename_tx','数据类型','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,6,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'signaltypename_tx','信号类型','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,7,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'featurename_tx','特征值类型','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,8,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'measvalue_tx','测量值','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,9,'1','1','1',40,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'engname_tx','单位','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,10,'1','1','1',30,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'almname_tx','报警等级','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,11,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'sampletime_dt','采样时间','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,12,'1','1','1',100,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;
commit;
/*---------------------在线数据查询 历史摘要数据查询列表视图(结束)------------------------*/

/*---------------------在线数据查询 测点列表查询视图(开始)------------------------*/
V_ViewID := V_ViewID + 1;
V_FunctionID := V_FunctionID + 1;
--001测点列表查询视图配置信息
--插入视图基本信息
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx,EntityType_CD)
values(V_ViewID,'DataQueryPackage.Pr_QueryPointList','测点列表查询视图','P');
commit;

Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_AppactionCD','模块CD','String',1,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_PostID','岗位ID','Int',2,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_OrgID','公司ID','Int',3,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_MobjectID','设备ID','Int',4,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_PointName','测点名称','String',5,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_IncludeSubMob','是否包含子设备','Int',6,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_PageSize','分页大小','Int',7,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_PageIndex','页码','Int',8,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_Count','记录总数','Int',9,'Output');

--插入查询列表视图
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(V_FunctionID,V_ViewID,'测点列表查询','PointMM','point_id','wgv_JCSJ_PointList','0');

--插入视图的列信息
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'point_id','主键','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,1,'1','1','0',5,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'mobname_tx','设备名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,2,'1','1','1',80,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'mob_cd','设备编码','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,3,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'pntname_tx','测点名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,4,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'desc_tx','测点描述','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,5,'1','1','1',200,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;
commit;
/*---------------------在线数据查询 测点查询列表视图(结束)------------------------*/


/*---------------------在线棒图列表查询视图(开始)---------------------------*/
V_ViewID := V_ViewID + 1;
V_FunctionID := V_FunctionID + 1;
V_Sortno_Nr :=1;
--插入视图基本信息Bs_Viewinfo(1)

Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(V_ViewID,'V_AnalysisBarGroup', '在线棒图列表 查询视图');
commit;

--插入查询列表视图bs_viewfunction(2)
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(V_FunctionID,V_ViewID,'在线棒图列表','BarDiagramMM','BARGROUP_ID','wgv_AnalysisBarGroup_BarGoupList','0');

--插入视图的列信息Bs_Viewcolumn(3)和Bs_Viewfunccolumns(4)
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'BARGROUP_ID','分组ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'GROUPNAME_TX','分组名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,1,'1','1','1',400,'Left','1','1',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'BARDIAGRAM_ID','棒图ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'SORTNO_NR','排序号','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',150,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr :=V_Sortno_Nr+1;
commit;
/*---------------------在线棒图列表查询视图(结束)---------------------------*/ 

/*---------------------数据源变量管理列表视图(开始)-----------------------------*/
V_ViewID := V_ViewID + 1;
V_FunctionID := V_FunctionID + 1;
--002数据源变量管理视图配置信息
--插入视图基本信息
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(V_ViewID,'V_JCCS_SJYBLGL', '数采站管理->数据源变量管理 查询视图');
commit;

--插入查询列表视图
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(V_FunctionID,V_ViewID,'数据源变量管理','DataVarMM','Var_ID','wgv_JCCS_DataVarList','0');

--插入视图的列信息
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'Var_ID','主键','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,1,'1','1','0',5,'Left','1','1',2);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'VarName_TX','变量名','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,2,'1','1','1',100,'Left','1','1',1);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'VarOffset_NR','灵敏度','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,3,'1','1','1',100,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'VarOffset_NR','偏移量','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,4,'1','1','1',100,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'VarDesc_TX','变量描述','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,5,'1','1','1',200,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'PointName_TX','测点','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,6,'1','1','1',200,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;
commit;
/*---------------------数据源变量管理列表视图(结束)-----------------------------*/


--004轴承库视图配置信息

V_ViewID := V_ViewID + 1;
V_FunctionID := V_FunctionID + 1;
V_Sortno_Nr := 1;

--插入视图基本信息
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx,EntityType_CD)
values(V_ViewID,'QueryBearings', '监测分析->轴承库 查询视图','P');
commit;

Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_Factory','生产厂家','String',1,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_Model','产品型号','String',2,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_Speed','转速','Int',3,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_PageSize','分页大小','Int',4,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_PageIndex','页码','Int',5,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_Count','记录总数','Int',6,'Output');

--插入查询列表视图
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn,Datarange_CD)
values(V_FunctionID,V_ViewID,'轴承库','BearingMM','Bearing_ID','wgv_AppAnalysis_Bearing','0','');

--插入视图的列信息
--主键
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'Bearing_ID','主键','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',10,'Left','1','1',2);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr := V_Sortno_Nr + 1;

--轴承型号
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'Model_TX','轴承型号','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',100,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr := V_Sortno_Nr + 1;

--滚动体数
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'RollerCount_NR','滚动体数','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',100,'Right','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr := V_Sortno_Nr + 1;

--滚动体频率
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'BSF_TX','滚动体频率(HZ)','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',80,'Right','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr := V_Sortno_Nr + 1;

--保持架频率
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'FTF_TX','保持架频率(HZ)','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',80,'Right','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr := V_Sortno_Nr + 1;

--外圈频率
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'BBPFO_TX','外圈频率(HZ)','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',80,'Right','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr := V_Sortno_Nr + 1;

--内圈频率
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'BPFI_TX','内圈频率(HZ)','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,4,'1','1','1',80,'Right','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr := V_Sortno_Nr + 1;

--是否内置
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'IsInLay_YN','是否内置','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',80,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr := V_Sortno_Nr + 1;

--备注
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'Description_TX','备注','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',100,'Left','0','0',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr := V_Sortno_Nr + 1;

commit;

V_ViewID := V_ViewID + 1;
V_FunctionID := V_FunctionID + 1;
--005特征频率分组视图配置信息
--插入视图基本信息
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(V_ViewID,'V_JCCS_TZPLFZ', '监测数据管理->特征频率管理 特征频率分组查询视图');
commit;

--插入查询列表视图
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(V_FunctionID,V_ViewID,'特征频率管理','FeatureFreqMM','Group_ID','wgv_JCCS_FeatureFreqGroupList','0');

--插入视图的列信息
--主键
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'Group_ID','主键','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,1,'1','1','0',10,'Left','1','1',1);
V_ViewcolumnID := V_ViewcolumnID + 1;

--分组名称
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'GroupName_TX','分组名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,2,'1','1','1',100,'Left','0','0',2);
V_ViewcolumnID := V_ViewcolumnID + 1;

--公司ID
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'Org_ID','公司ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,3,'1','1','0',100,'Left','1','1',3);
V_ViewcolumnID := V_ViewcolumnID + 1;

commit;

V_ViewID := V_ViewID + 1;
V_FunctionID := V_FunctionID + 1;
--006关联特征频率分组视图配置信息
--插入视图基本信息
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(V_ViewID,'V_JCCS_GLTZPL', '监测数据管理->特征频率管理 关联特征频率查询视图');
commit;

--插入查询列表视图
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(V_FunctionID,V_ViewID,'特征频率管理','FeatureFreqMM','Group_ID','wgv_JCCS_RealatedFeatureFreqGroupList','0');

--插入视图的列信息
--主键
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'Group_ID','主键','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,1,'1','1','0',10,'Left','1','1',1);
V_ViewcolumnID := V_ViewcolumnID + 1;

--分组名称
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'GroupName_TX','分组名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,2,'1','1','1',100,'Left','0','0',2);
V_ViewcolumnID := V_ViewcolumnID + 1;

--设备ID
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'MObject_ID','设备ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,3,'1','1','0',100,'Left','1','1',3);
V_ViewcolumnID := V_ViewcolumnID + 1;

commit;

V_ViewID := V_ViewID + 1;
V_FunctionID := V_FunctionID + 1;
--005特征频率视图配置信息
--插入视图基本信息
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx)
values(V_ViewID,'V_JCCS_TZPL', '监测数据管理->特征频率管理 特征频率查询视图');
commit;

--插入查询列表视图
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(V_FunctionID,V_ViewID,'特征频率管理','FeatureFreqMM','FeatureFreq_ID','wgv_JCCS_FeatureFreqList','0');

--插入视图的列信息
--主键
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'FeatureFreq_ID','主键','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,1,'1','1','0',10,'Left','1','1',1);
V_ViewcolumnID := V_ViewcolumnID + 1;

--特征频率名称
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'Name_TX','特征频率名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,2,'1','1','1',200,'Left','0','0',2);
V_ViewcolumnID := V_ViewcolumnID + 1;

--特征频率值
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'FeatureFreqValue_NR','特征频率值','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,3,'1','1','1',200,'Left','0','0',3);
V_ViewcolumnID := V_ViewcolumnID + 1;

--单位
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'Unit_NR','单位','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,4,'1','1','1',200,'Left','0','0',4);
V_ViewcolumnID := V_ViewcolumnID + 1;

--分组ID
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'Group_ID','分组ID','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,5,'1','1','0',100,'Left','1','1',5);
V_ViewcolumnID := V_ViewcolumnID + 1;

commit;

--004未关联的特征频率分组视图配置信息

V_ViewID := V_ViewID + 1;
V_FunctionID := V_FunctionID + 1;
V_Sortno_Nr := 1;

--插入视图基本信息
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx,EntityType_CD)
values(V_ViewID,'DataQueryPackage.pr_QueryNonRelatedFeatureGroup', '监测数据管理->特征频率管理 特征频率分组关联视图','P');
commit;

Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_OrgID','公司ID','Int',1,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_MobjectID','设备ID','Int',2,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_PageSize','分页大小','Int',3,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_PageIndex','页码','Int',4,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_Count','记录总数','Int',5,'Output');

--插入查询列表视图
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn,Datarange_CD)
values(V_FunctionID,V_ViewID,'未关联特征频率分组','FeatureFreq_REL','Group_ID','wgv_JCCS_NonRelatedFreqGroupList','0','');

--插入视图的列信息
--主键
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'Group_ID','主键','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','0',10,'Left','1','1',1);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_Sortno_Nr := V_Sortno_Nr + 1;

--分组名称
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'GroupName_TX','分组名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,V_Sortno_Nr,'1','1','1',200,'Left','0','0',2);
V_ViewcolumnID := V_ViewcolumnID + 1;
V_ViewcolumnID := V_ViewcolumnID + 1;

commit;

/*---------------------短信统计查询 短信统计查询列表视图(开始)------------------------*/
V_ViewID := V_ViewID + 1;
V_FunctionID := V_FunctionID + 1;
--001历史摘要数据查询视图配置信息
--插入视图基本信息
Insert Into Bs_Viewinfo(View_Id,Viewname_Tx,Viewdesc_Tx,EntityType_CD)
values(V_ViewID,'SMSQueryPackage.Pr_QuerySMSHistory','短信统计查询视图','P');
commit;

Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_AppactionCD','模块CD','String',1,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_PostID','岗位ID','Int',2,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_OrgID','公司ID','Int',3,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_MobjectID','设备ID','Int',4,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_MobjectCD','设备编码','String',5,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_MobjectName','设备名称','String',6,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_PointID','测点ID','Int',7,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_MinPartitionID','起始分区','Int',8,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_MaxPartitionID','结束分区','Int',9,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_IncludeSubMob','是否包含子设备','Int',10,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_AlmLevel','报警等级','Int',11,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_PageSize','分页大小','Int',14,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_PageIndex','页码','Int',15,'Input');
Insert Into BS_ViewParameter(View_ID,ParameterName_TX,ParameterDesc_TX,DataType_TX,SortNo_NR,ParamDirection_TX)
values(V_ViewID,'P_Count','记录总数','Int',16,'Output');

--插入查询列表视图
Insert Into Bs_Viewfunction(Function_Id,View_Id,Name_Tx,Appaction_Cd,Datakey_Cd,Listname_Tx,Datarange_Yn)
values(V_FunctionID,V_ViewID,'短信统计查询','SMSHistoryMM','SMSHistory_ID','wgv_DXTJ_SMSHistory','0');

--插入视图的列信息
Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'SMSHistory_ID','主键','Int');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,1,'1','1','0',5,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'mobname_tx','设备名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,2,'1','1','1',100,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'mob_cd','设备编码','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,3,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'pntname_tx','测点名称','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,4,'1','1','1',100,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'smscontent_tx','短信内容','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,5,'1','1','1',100,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'name_tx','接收人','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,6,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'sendtime_tm','发送时间','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,7,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'status_tx','发送状态','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,8,'1','1','1',60,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'almname_tx','报警等级','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,9,'1','1','1',40,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

Insert Into Bs_Viewcolumn(Viewcolumn_Id,View_Id,Columnname_Tx,Columndesc_Tx,Datatype_Tx)
values(V_ViewcolumnID,V_ViewID,'memo_tx','备注','String');
Insert Into Bs_Viewfunccolumns(Function_Id,Viewcolumn_Id,Sortno_Nr,Syscondition_Yn,Condition_Yn,Show_Yn,Width_Nr,Align_Tx,Sort_Yn,Asc_Yn,Ranksortno_Nr)
values(V_FunctionID,V_ViewcolumnID,10,'1','1','1',30,'Left','0','0',0);
V_ViewcolumnID := V_ViewcolumnID + 1;

commit;
/*---------------------短信统计查询 短信统计查询列表视图(结束)------------------------*/

End;
/
