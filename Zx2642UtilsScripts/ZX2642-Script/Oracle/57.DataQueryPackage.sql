create or replace package DataQueryPackage is

  Type T_Cursor Is Ref Cursor;

-- ����ϵͳ��ʷ����ժҪ��Ϣ��ѯ 
-- P_AppactionCD Ȩ��
-- P_PostID �û���λ
-- P_OrgID ��˾ID
-- P_MobjectID �豸ID
-- P_MobjectCD �豸����
-- P_MobjectName �豸����
-- P_PointID �����
-- P_MinPartitionID��������� ��С�������
-- P_MaxPartitionID��������� ���������
-- P_IncludeSubMob �Ƿ�������豸(1:������0:������)
-- P_AlmLevel �����ȼ�
-- P_DataType ��������
-- P_SignalTypeID �ź�����
-- P_PageSize ��ҳ��С
-- P_PageIndex ҳ��
-- P_Cursor ���������������ʷժҪ�����б�
procedure Pr_QueryHistorySummary(
            P_AppactionCD       in		BS_AppAction.AppAction_CD%type,
            P_PostID            in		BS_Post.Post_ID%type,
            P_OrgID				in		BS_Org.Org_ID%type,
            P_MobjectID         in		Mob_MObject.Mobject_ID%type,
            P_MobjectCD         in		Mob_MObject.Mobject_CD%type,
            P_MobjectName       in		Mob_MObject.MobjectName_TX%type,
            P_PointID           in		Pnt_Point.Point_ID%type,
            P_MinPartitionID    in		ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    in		ZX_History_Summary.Partition_ID%type,
            P_IncludeSubMob     in		BS_MetaFieldType.Int_NR%type,
            P_AlmLevel          in		ZX_History_Summary.AlmLevel_ID%type,
            P_DataType          in		ZX_History_Summary.DatType_NR%type,
            P_SignalTypeID      in		ZX_History_Summary.SigType_NR%type,
            P_PageSize			in		BS_MetaFieldType.Int_NR%type,
			P_PageIndex			in		BS_MetaFieldType.Int_NR%type,
			P_Count				out		BS_MetaFieldType.Int_NR%type, P_Cursor            out		T_Cursor);


-- ����ϵͳ��ʷ����ժҪ��ѯ�еĲ���б��ѯ 
-- P_AppactionCD Ȩ��
-- P_PostID �û���λ
-- P_OrgID ��˾ID
-- P_MobjectID �豸ID
-- P_PointName �������
-- P_IncludeSubMob �Ƿ�������豸(1:������0:������)
-- P_PageSize ��ҳ��С
-- P_PageIndex ҳ��
-- P_Cursor ���������������ʷժҪ�����б�
procedure Pr_QueryPointList(
            P_AppactionCD       in		BS_AppAction.AppAction_CD%type,
            P_PostID            in		BS_Post.Post_ID%type,
            P_OrgID				in		BS_Org.Org_ID%type,
            P_MobjectID         in		Mob_MObject.Mobject_ID%type,
            P_PointName         in		Pnt_Point.PointName_TX%type,
            P_IncludeSubMob     in		BS_MetaFieldType.Int_NR%type,
            P_PageSize			in		BS_MetaFieldType.Int_NR%type,
			P_PageIndex			in		BS_MetaFieldType.Int_NR%type,
			P_Count				out		BS_MetaFieldType.Int_NR%type, P_Cursor            out		T_Cursor);
            
-------------------------------------------------------------------
  --����˵��������������ѯδ�󶨵�����Ƶ�ʷ���   by ������  2010 10-01
  --����1��P_OrgID ��˾
  --����2��P_MobjectID �豸
  --����6��p_pageSize ��ҳ��С
  --����7��p_pageIndex ҳ��
  --����8��P_Cursor ��ѯ���
  -------------------------------------------------------------------
  Procedure pr_QueryNonRelatedFeatureGroup(
            P_OrgID        in BS_MetaFieldType.Int_NR%type,
            P_MobjectID    in BS_MetaFieldType.Int_NR%type,
            p_pageSize     in  BS_MetaFieldType.Int_NR%type,
            p_pageIndex    in	BS_MetaFieldType.Int_NR%type,
            P_Count		out		BS_MetaFieldType.Int_NR%type, P_Cursor   out T_Cursor);            

end DataQueryPackage;

/

create or replace package body DataQueryPackage is  

-- ����ϵͳ��ʷ����ժҪ��Ϣ��ѯ 
-- P_AppactionCD Ȩ��
-- P_PostID �û���λ
-- P_OrgID ��˾ID
-- P_MobjectID �豸ID
-- P_MobjectCD �豸����
-- P_MobjectName �豸����
-- P_PointID �����
-- P_MinPartitionID��������� ��С�������
-- P_MaxPartitionID��������� ���������
-- P_IncludeSubMob �Ƿ�������豸(1:������0:������)
-- P_AlmLevel �����ȼ�
-- P_DataType ��������
-- P_SignalTypeID �ź�����
-- P_PageSize ��ҳ��С
-- P_PageIndex ҳ��
-- P_Cursor ���������������ʷժҪ�����б�
procedure Pr_QueryHistorySummary(
            P_AppactionCD       in		BS_AppAction.AppAction_CD%type,
            P_PostID            in		BS_Post.Post_ID%type,
            P_OrgID				in		BS_Org.Org_ID%type,
            P_MobjectID         in		Mob_MObject.Mobject_ID%type,
            P_MobjectCD         in		Mob_MObject.Mobject_CD%type,
            P_MobjectName       in		Mob_MObject.MobjectName_TX%type,
            P_PointID           in		Pnt_Point.Point_ID%type,
            P_MinPartitionID    in		ZX_History_Summary.Partition_ID%type,
            P_MaxPartitionID    in		ZX_History_Summary.Partition_ID%type,
            P_IncludeSubMob     in		BS_MetaFieldType.Int_NR%type,
            P_AlmLevel          in		ZX_History_Summary.AlmLevel_ID%type,
            P_DataType          in		ZX_History_Summary.DatType_NR%type,
            P_SignalTypeID      in		ZX_History_Summary.SigType_NR%type,
            P_PageSize			in		BS_MetaFieldType.Int_NR%type,
			P_PageIndex			in		BS_MetaFieldType.Int_NR%type,
			P_Count				out		BS_MetaFieldType.Int_NR%type, P_Cursor            out		T_Cursor)
as
    V_count         BS_MetaFieldType.Int_NR%type;
    V_DataRangeCD   BS_MetaFieldType.Fifty_TX%type;
    V_Rangeidlist   BS_MetaFieldType.OneHundred_TX%type;
    V_TableBaseName BS_MetaFieldType.Fifty_TX%type;
    V_Summary		BS_MetaFieldType.Fifty_TX%type;
    V_QueryObjSql   BS_MetaFieldType.FiveKilo_TX%type;
    V_Sql           BS_MetaFieldType.FiveKilo_TX%type;
    V_ParentList    BS_MetaFieldType.TwoHundred_TX%type;
    V_DeptID        BS_MetaFieldType.Int_NR%type;
    V_SqlCount		BS_MetaFieldType.FiveKilo_TX%type;
	V_PageIndex		BS_MetaFieldType.Int_NR%type;
begin 
    -- �ж��û���λ�Ƿ���Ȩ�޲�ѯ
    V_count := 0;
    select count(*) into V_count from Bs_Postappaction bp where bp.Post_ID = P_PostID and bp.Appaction_Cd = P_AppactionCD;
    if V_count = 0 then
     DBDiffPackage.Pr_DebugPrint('�û�û�в���Ȩ��');
       open P_Cursor for select * from dual where 1 <> 1;
       return;
    end if;
    
    -- ��ȡ�û���λ��Ӧ�Ĳ���
    select dept_id into V_DeptID from bs_post where post_id = P_PostID;
    if V_DeptID is null then
    DBDiffPackage.Pr_DebugPrint('�û���λ���ڲ��Ų���ȷ');
      open P_Cursor for select * from dual where 1 <> 1;
      return;
    end if;
    
    V_PageIndex := P_PageIndex;
    
    -- ������������֯��ʷ���ݲ�ѯ��SQL���
    V_TableBaseName := 'Zx_History_Summary';
    V_Summary := V_TableBaseName || 'His';
    V_QueryObjSql := Commonpackage.F_GetPartitionSQL(P_MinPartitionID, P_MaxPartitionID, V_TableBaseName);
    V_Sql := 
      ' select m.mobjectname_tx as mobname_tx, m.mobject_cd as mob_cd, p.pointname_tx as pntname_tx, p.desc_tx as desc_tx, p.engunit_id as engunit_id, p.featurevalue_id as featurevalue_id, ' || 
               V_Summary || '.history_id as history_id, ' || V_Summary || '.result_tx as result_tx, ' || V_Summary || '.almlevel_id as almlevel_id, ' || V_Summary || '.samptime_dt as sampletime_dt, ' || 
               V_Summary || '.dattype_nr as datatype_nr, ' || V_Summary || '.sigtype_nr as signaltype_id, ' || V_Summary || '.pntdim_nr as pntdim_nr, ' || V_Summary || '.partition_id as partition_id ' || 
               ' from ' || V_QueryObjSql || 
               ' left join pnt_point p ' || 
               ' left join ( select mob.*, bp.dept_id from mob_mobject mob ' ||  
               ' left join bs_post bp on mob.djowner_id = bp.post_id where mob.active_yn = ''1'' ) m ' || 
               ' left join mob_mobjectstructure ms ' || 
               ' on m.mobject_id = ms.mobject_id ' || 
               ' on p.mobject_id = m.mobject_id ' || 
               ' on ' || V_Summary || '.point_id = p.point_id ' || 
               ' where p.pointname_tx is not null and ' || V_Summary || '.partition_id <= ' || DBDiffPackage.IntToString(P_MaxPartitionID) || ' and ' || V_Summary || '.partition_id >= ' || DBDiffPackage.IntToString(P_MinPartitionID);
    
    -- ���ݹ�˾��ѯ
    if P_OrgID <> -1 then
	  V_Sql := V_Sql || ' and m.org_id = ' || DBDiffPackage.IntToString(P_OrgID);
    end if;
    
    -- �����豸��ѯ�������豸�����豸��ѯ
    if P_MobjectID <> -1 then
      if P_IncludeSubMob = 1 then
        select parentlist_tx into V_ParentList from mob_mobjectstructure where mobject_id = P_MobjectID;
        if V_ParentList is not null and DBDiffPackage.GetLength(V_ParentList) > 0 then
		  V_Sql := V_Sql || ' and ms.parentlist_tx like ''' || V_ParentList || '%''';
		else
		  V_Sql := V_Sql || ' and m.mobject_id = ' || DBDiffPackage.IntToString(P_MobjectID);
		end if;
      else
        V_Sql := V_Sql || ' and m.mobject_id = ' || DBDiffPackage.IntToString(P_MobjectID);
      end if;
    end if;
    
    -- ���ݲ���ѯ
    if P_PointID <> -1 then
      V_Sql := V_Sql || ' and ' || V_Summary || '.point_id = ' || DBDiffPackage.IntToString(P_PointID);
    end if;
    
    -- �����豸�����ѯ
    if DBDiffPackage.GetLength(P_MobjectCD) > 0 then
      V_Sql := V_Sql || ' and m.mobject_cd = ''' || P_MobjectCD || '''';
    end if;
    
    -- ���ݱ��������ѯ
    if P_AlmLevel <> -1 then
      V_Sql := V_Sql || ' and ' || V_Summary || '.almlevel_id = ' || DBDiffPackage.IntToString(P_AlmLevel);
    end if;
    
    -- �����������Ͳ�ѯ
    if P_DataType <> -1 then
      V_Sql := V_Sql || ' and ' || V_Summary || '.dattype_nr = ' || DBDiffPackage.IntToString(P_DataType);
    end if;
    
    -- ������Ϣ�����Ͳ�ѯ
    if P_SignalTypeID <> -1 then
      V_Sql := V_Sql || ' and ' || V_Summary || '.sigtype_nr = ' || DBDiffPackage.IntToString(P_SignalTypeID);
    end if;
    
    -- �����豸���Ʋ�ѯ
    if DBDiffPackage.GetLength(P_MobjectName) > 0 then
      V_Sql := V_Sql || ' and m.mobjectname_tx like ''%' || P_MobjectName || '%''';
    end if;
    
    -- �����û���λ�����ݷ�Χ��ѯ
    select DataRange_CD into V_DataRangeCD from Bs_Postappaction bp where bp.Post_ID = P_PostID and bp.Appaction_Cd = P_AppactionCD;
    if V_DataRangeCD = 'PE' then --����
        -- �ж��豸�ֹ���Ϊ�գ��ҷֹ���λ���û���λһ��
        V_Sql := V_Sql || ' and m.djowner_id is not null and m.djowner_id = ' || DBDiffPackage.IntToString(P_PostID);
    elsif V_DataRangeCD = 'DE' then -- ����
        -- �ж��豸�ֹ���Ϊ�գ��ҷֹ���λ���ڲ������û���λ���ڲ���һ�£����߷ֹ���λ���ڲ������û��������ݷ�Χ��
        select Rangeidlist_TX into V_Rangeidlist from Bs_Postappaction bp where bp.Post_ID = P_PostID and bp.Appaction_Cd = P_AppactionCD;
        if V_Rangeidlist is null or DBDiffPackage.GetLength(V_Rangeidlist) = 0 then
           -- �ֹ���λ���ڲ������û���λ���ڲ���һ��
           V_Sql := V_Sql || ' and m.dept_id is not null and m.dept_id = ' || DBDiffPackage.IntToString(V_DeptID);
        else
           -- �ֹ���λ���ڲ������û��������ݷ�Χ��
           V_count := Commonpackage.F_SplitCount(V_Rangeidlist, ',');
           if V_count = 1 then
             V_Sql := V_Sql || ' and m.dept_id is not null and m.dept_id = ' || V_Rangeidlist;
           else
             V_Sql := V_Sql || ' and m.dept_id is not null and m.dept_id in (' || V_Rangeidlist || ')';
           end if;
        end if;
    end if;
    
    -- ������ֵ���͡��������͡��ź����͡����̵�λ�������ȼ���������Ϣ
     V_Sql := 
       ' select summary.*, '''' as measvalue_tx, '''' as engname_tx, ' || 
       ' eng.namee_tx as engename_tx, eng.namec_tx as engcname_tx, ' || 
       ' '''' as almname_tx, ft.name_tx as featurename_tx, ' || 
       ' d.onlinename_tx as datatypename_tx, s.name_tx as signaltypename_tx ' || 
       ' from ( ' || V_Sql || ' ) summary ' || 
       ' left join z_engunit eng on summary.engunit_id = eng.engunit_id ' || 
       ' left join z_featurevalue ft on summary.featurevalue_id = ft.featurevalue_id ' || 
       ' left join z_datatype d on summary.datatype_nr = d.datatype_id ' || 
       ' left join z_signtype s on summary.signaltype_id = s.signtype_id ';   
      
    DBDiffPackage.Pr_DebugPrint(V_Sql);
    
    -- ��ѯP_PageIndexΪ-1������£������ܼ�¼��
    if P_PageIndex = -1 then
		V_SqlCount := 'select count(*) from (' || V_Sql || ') V';
		execute immediate V_SqlCount into P_Count;
		open P_Cursor for select * from dual where 1 <> 1;
		return;
	end if;
	
    -- ���������������豸���롢�豸���ơ���������������У��ٰ��ղ���ʱ�併������
	if P_PageSize = -1 then
		-- ��ѯ��������
		V_Sql := V_Sql || ' order by mob_cd, mobname_tx, pntname_tx asc, sampletime_dt desc';
	else
		-- ��ѯָ��ҳ��ָ������������
		V_Sql := DBDiffPackage.GetPagingSQL(V_Sql, V_PageIndex, P_PageSize, 'mob_cd, mobname_tx, pntname_tx asc, sampletime_dt desc');
	end if;
    open P_Cursor for V_Sql;
    return;
end;

-- ����ϵͳ��ʷ����ժҪ��ѯ�еĲ���б��ѯ 
-- P_AppactionCD Ȩ��
-- P_PostID �û���λ
-- P_OrgID ��˾ID
-- P_MobjectID �豸ID
-- P_PointName �������
-- P_IncludeSubMob �Ƿ�������豸(1:������0:������)
-- P_PageSize ��ҳ��С
-- P_PageIndex ҳ��
-- P_Cursor ���������������ʷժҪ�����б�
procedure Pr_QueryPointList(
            P_AppactionCD       in		BS_AppAction.AppAction_CD%type,
            P_PostID            in		BS_Post.Post_ID%type,
            P_OrgID				in		BS_Org.Org_ID%type,
            P_MobjectID         in		Mob_MObject.Mobject_ID%type,
            P_PointName         in		Pnt_Point.PointName_TX%type,
            P_IncludeSubMob     in		BS_MetaFieldType.Int_NR%type,
            P_PageSize			in		BS_MetaFieldType.Int_NR%type,
			P_PageIndex			in		BS_MetaFieldType.Int_NR%type,
			P_Count				out		BS_MetaFieldType.Int_NR%type, P_Cursor            out		T_Cursor)
as
    V_count         BS_MetaFieldType.Int_NR%type;
    V_DataRangeCD   BS_MetaFieldType.Fifty_TX%type;
    V_Rangeidlist   BS_MetaFieldType.OneHundred_TX%type;
    V_Sql           BS_MetaFieldType.FiveKilo_TX%type;
    V_ParentList    BS_MetaFieldType.TwoHundred_TX%type;
    V_DeptID        BS_MetaFieldType.Int_NR%type;
    V_SqlCount		BS_MetaFieldType.FiveKilo_TX%type;
	V_PageIndex		BS_MetaFieldType.Int_NR%type;
begin 
    -- �ж��û���λ�Ƿ���Ȩ�޲�ѯ
    V_count := 0;
    select count(*) into V_count from Bs_Postappaction bp where bp.Post_ID = P_PostID and bp.Appaction_Cd = P_AppactionCD;
    if V_count = 0 then
     DBDiffPackage.Pr_DebugPrint('�û�û�в���Ȩ��');
       open P_Cursor for select * from dual where 1 <> 1;
       return;
    end if;
    
    -- ��ȡ�û���λ��Ӧ�Ĳ���
    select dept_id into V_DeptID from bs_post where post_id = P_PostID;
    if V_DeptID is null then
      DBDiffPackage.Pr_DebugPrint('�û���λ���ڲ��Ų���ȷ');
      open P_Cursor for select * from dual where 1 <> 1;
      return;
    end if;
    
    V_PageIndex := P_PageIndex;
    
    -- ������������֯��ʷ���ݲ�ѯ��SQL���
    V_Sql := 
      ' select m.mobjectname_tx as mobname_tx, m.mobject_cd as mob_cd, p.point_id as point_id, p.pointname_tx as pntname_tx, p.desc_tx as desc_tx ' || 
               ' from pnt_point p ' || 
               ' left join ( select mob.*, bp.dept_id from mob_mobject mob ' ||  
               ' left join bs_post bp on mob.djowner_id = bp.post_id where mob.active_yn = ''1'' ) m ' || 
               ' left join mob_mobjectstructure ms ' || 
               ' on m.mobject_id = ms.mobject_id ' || 
               ' on p.mobject_id = m.mobject_id ' || 
               ' where p.pointname_tx is not null ';
    
    -- ���ݹ�˾��ѯ
    if P_OrgID <> -1 then
	  V_Sql := V_Sql || ' and m.org_id = ' || DBDiffPackage.IntToString(P_OrgID);
    end if;
    
    -- �����豸��ѯ�������豸�����豸��ѯ
    if P_MobjectID <> -1 then
		if P_IncludeSubMob = 1 then
		  select parentlist_tx into V_ParentList from mob_mobjectstructure where mobject_id = P_MobjectID;
		  if V_ParentList is not null and DBDiffPackage.GetLength(V_ParentList) > 0 then
			V_Sql := V_Sql || ' and ms.parentlist_tx like ''' || V_ParentList || '%''';
		  else
			V_Sql := V_Sql || ' and m.mobject_id = ' || DBDiffPackage.IntToString(P_MobjectID);
		  end if;
		else
		  V_Sql := V_Sql || ' and m.mobject_id = ' || DBDiffPackage.IntToString(P_MobjectID);
		end if;
	end if;
    
    -- ���ݲ�����Ʋ�ѯ
    if DBDiffPackage.GetLength(P_PointName) > 0 then
      V_Sql := V_Sql || ' and p.pointname_tx like ''%' || P_PointName || '%''';
    end if;
    
    -- �����û���λ�����ݷ�Χ��ѯ
    select DataRange_CD into V_DataRangeCD from Bs_Postappaction bp where bp.Post_ID = P_PostID and bp.Appaction_Cd = P_AppactionCD;
    if V_DataRangeCD = 'PE' then --����
        -- �ж��豸�ֹ���Ϊ�գ��ҷֹ���λ���û���λһ��
        V_Sql := V_Sql || ' and m.djowner_id is not null and m.djowner_id = ' || DBDiffPackage.IntToString(P_PostID);
    elsif V_DataRangeCD = 'DE' then -- ����
        -- �ж��豸�ֹ���Ϊ�գ��ҷֹ���λ���ڲ������û���λ���ڲ���һ�£����߷ֹ���λ���ڲ������û��������ݷ�Χ��
        select Rangeidlist_TX into V_Rangeidlist from Bs_Postappaction bp where bp.Post_ID = P_PostID and bp.Appaction_Cd = P_AppactionCD;
        if V_Rangeidlist is null or DBDiffPackage.GetLength(V_Rangeidlist) = 0 then
           -- �ֹ���λ���ڲ������û���λ���ڲ���һ��
           V_Sql := V_Sql || ' and m.dept_id is not null and m.dept_id = ' || DBDiffPackage.IntToString(V_DeptID);
        else
           -- �ֹ���λ���ڲ������û��������ݷ�Χ��
           V_count := Commonpackage.F_SplitCount(V_Rangeidlist, ',');
           if V_count = 1 then
             V_Sql := V_Sql || ' and m.dept_id is not null and m.dept_id = ' || V_Rangeidlist;
           else
             V_Sql := V_Sql || ' and m.dept_id is not null and m.dept_id in (' || V_Rangeidlist || ')';
           end if;
        end if;
    end if;
    
    DBDiffPackage.Pr_DebugPrint(V_Sql);
    
    -- ��ѯP_PageIndexΪ-1������£������ܼ�¼��
    if P_PageIndex = -1 then
		V_SqlCount := 'select count(*) from (' || V_Sql || ') V';
		execute immediate V_SqlCount into P_Count;
		open P_Cursor for select * from dual where 1 <> 1;
		return;
	end if;
    
    -- ��ѯָ��ҳ��ָ������������
    -- ���������������豸���롢�豸���ơ����������������
	V_Sql := DBDiffPackage.GetPagingSQL(V_Sql, V_PageIndex, P_PageSize, 'mob_cd, mobname_tx, pntname_tx asc');    
    open P_Cursor for V_Sql;
    return;
end;

-------------------------------------------------------------------
  --����˵��������������ѯδ�󶨵�����Ƶ�ʷ���   by ������  2010 10-01
  --����1��P_OrgID ��˾
  --����2��P_MobjectID �豸
  --����6��p_pageSize ��ҳ��С
  --����7��p_pageIndex ҳ��
  --����8��P_Cursor ��ѯ���
  -------------------------------------------------------------------
  Procedure pr_QueryNonRelatedFeatureGroup(
            P_OrgID        in BS_MetaFieldType.Int_NR%type,
            P_MobjectID    in BS_MetaFieldType.Int_NR%type,
            p_pageSize     in  BS_MetaFieldType.Int_NR%type,
            p_pageIndex    in	BS_MetaFieldType.Int_NR%type,
            P_Count			out	BS_MetaFieldType.Int_NR%type, P_Cursor   out T_Cursor)

  as
  V_Sql BS_MetaFieldType.TwoKilo_TX%type;
  V_SqlCount BS_MetaFieldType.TwoKilo_TX%type;  
  V_PageIndex BS_MetaFieldType.Int_NR%type;
  begin
  V_PageIndex := p_pageIndex;
  V_Sql := 'select g.group_id, g.groupname_tx from analysis_featurefreqgroup g where g.org_id =' || DBDiffPackage.IntToString(P_OrgID) || 
					' and not exists( select 1 from mob_featrefreqgroup mf where mf.mobject_id =' || DBDiffPackage.IntToString(P_MobjectID) || '  and mf.group_id = g.group_id)';
  
	-- ��ѯP_PageIndexΪ-1������£������ܼ�¼��
	if p_pageIndex = -1 then
		V_SqlCount := 'select count(*) from (' || V_Sql || ') V';
		execute immediate V_SqlCount into P_Count;
		open P_Cursor for select * from dual where 1 <> 1;
		return;
	end if;
	
	-- ��ѯָ��ҳ��ָ������������
    -- ������������������Ƶ�ʷ�����������
	V_Sql := DBDiffPackage.GetPagingSQL(V_Sql, V_pageIndex, p_pageSize, 'Group_ID');
	DBDiffPackage.Pr_DebugPrint(V_Sql);
	open P_Cursor for V_Sql;
  end;

end DataQueryPackage;

/