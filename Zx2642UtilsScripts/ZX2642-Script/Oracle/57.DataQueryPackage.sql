create or replace package DataQueryPackage is

  Type T_Cursor Is Ref Cursor;

-- 在线系统历史数据摘要信息查询 
-- P_AppactionCD 权限
-- P_PostID 用户岗位
-- P_OrgID 公司ID
-- P_MobjectID 设备ID
-- P_MobjectCD 设备编码
-- P_MobjectName 设备名称
-- P_PointID 测点编号
-- P_MinPartitionID输入参数： 最小分区编号
-- P_MaxPartitionID输入参数： 最大分区编号
-- P_IncludeSubMob 是否包含子设备(1:包含，0:不包含)
-- P_AlmLevel 报警等级
-- P_DataType 数据类型
-- P_SignalTypeID 信号类型
-- P_PageSize 分页大小
-- P_PageIndex 页码
-- P_Cursor 输出参数：返回历史摘要数据列表
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


-- 在线系统历史数据摘要查询中的测点列表查询 
-- P_AppactionCD 权限
-- P_PostID 用户岗位
-- P_OrgID 公司ID
-- P_MobjectID 设备ID
-- P_PointName 测点名称
-- P_IncludeSubMob 是否包含子设备(1:包含，0:不包含)
-- P_PageSize 分页大小
-- P_PageIndex 页码
-- P_Cursor 输出参数：返回历史摘要数据列表
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
  --功能说明：根据条件查询未绑定的特征频率分组   by 董建林  2010 10-01
  --参数1：P_OrgID 公司
  --参数2：P_MobjectID 设备
  --参数6：p_pageSize 分页大小
  --参数7：p_pageIndex 页码
  --参数8：P_Cursor 查询结果
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

-- 在线系统历史数据摘要信息查询 
-- P_AppactionCD 权限
-- P_PostID 用户岗位
-- P_OrgID 公司ID
-- P_MobjectID 设备ID
-- P_MobjectCD 设备编码
-- P_MobjectName 设备名称
-- P_PointID 测点编号
-- P_MinPartitionID输入参数： 最小分区编号
-- P_MaxPartitionID输入参数： 最大分区编号
-- P_IncludeSubMob 是否包含子设备(1:包含，0:不包含)
-- P_AlmLevel 报警等级
-- P_DataType 数据类型
-- P_SignalTypeID 信号类型
-- P_PageSize 分页大小
-- P_PageIndex 页码
-- P_Cursor 输出参数：返回历史摘要数据列表
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
    -- 判断用户岗位是否有权限查询
    V_count := 0;
    select count(*) into V_count from Bs_Postappaction bp where bp.Post_ID = P_PostID and bp.Appaction_Cd = P_AppactionCD;
    if V_count = 0 then
     DBDiffPackage.Pr_DebugPrint('用户没有操作权限');
       open P_Cursor for select * from dual where 1 <> 1;
       return;
    end if;
    
    -- 获取用户岗位对应的部门
    select dept_id into V_DeptID from bs_post where post_id = P_PostID;
    if V_DeptID is null then
    DBDiffPackage.Pr_DebugPrint('用户岗位所在部门不正确');
      open P_Cursor for select * from dual where 1 <> 1;
      return;
    end if;
    
    V_PageIndex := P_PageIndex;
    
    -- 根据条件，组织历史数据查询的SQL语句
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
    
    -- 根据公司查询
    if P_OrgID <> -1 then
	  V_Sql := V_Sql || ' and m.org_id = ' || DBDiffPackage.IntToString(P_OrgID);
    end if;
    
    -- 根据设备查询、根据设备及子设备查询
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
    
    -- 根据测点查询
    if P_PointID <> -1 then
      V_Sql := V_Sql || ' and ' || V_Summary || '.point_id = ' || DBDiffPackage.IntToString(P_PointID);
    end if;
    
    -- 根据设备编码查询
    if DBDiffPackage.GetLength(P_MobjectCD) > 0 then
      V_Sql := V_Sql || ' and m.mobject_cd = ''' || P_MobjectCD || '''';
    end if;
    
    -- 根据报警级别查询
    if P_AlmLevel <> -1 then
      V_Sql := V_Sql || ' and ' || V_Summary || '.almlevel_id = ' || DBDiffPackage.IntToString(P_AlmLevel);
    end if;
    
    -- 根据数据类型查询
    if P_DataType <> -1 then
      V_Sql := V_Sql || ' and ' || V_Summary || '.dattype_nr = ' || DBDiffPackage.IntToString(P_DataType);
    end if;
    
    -- 根据信息号类型查询
    if P_SignalTypeID <> -1 then
      V_Sql := V_Sql || ' and ' || V_Summary || '.sigtype_nr = ' || DBDiffPackage.IntToString(P_SignalTypeID);
    end if;
    
    -- 根据设备名称查询
    if DBDiffPackage.GetLength(P_MobjectName) > 0 then
      V_Sql := V_Sql || ' and m.mobjectname_tx like ''%' || P_MobjectName || '%''';
    end if;
    
    -- 根据用户岗位和数据范围查询
    select DataRange_CD into V_DataRangeCD from Bs_Postappaction bp where bp.Post_ID = P_PostID and bp.Appaction_Cd = P_AppactionCD;
    if V_DataRangeCD = 'PE' then --个人
        -- 判断设备分工不为空，且分工岗位与用户岗位一致
        V_Sql := V_Sql || ' and m.djowner_id is not null and m.djowner_id = ' || DBDiffPackage.IntToString(P_PostID);
    elsif V_DataRangeCD = 'DE' then -- 部门
        -- 判断设备分工不为空，且分工岗位所在部门与用户岗位所在部门一致，或者分工岗位所在部门在用户部门数据范围内
        select Rangeidlist_TX into V_Rangeidlist from Bs_Postappaction bp where bp.Post_ID = P_PostID and bp.Appaction_Cd = P_AppactionCD;
        if V_Rangeidlist is null or DBDiffPackage.GetLength(V_Rangeidlist) = 0 then
           -- 分工岗位所在部门与用户岗位所在部门一致
           V_Sql := V_Sql || ' and m.dept_id is not null and m.dept_id = ' || DBDiffPackage.IntToString(V_DeptID);
        else
           -- 分工岗位所在部门在用户部门数据范围内
           V_count := Commonpackage.F_SplitCount(V_Rangeidlist, ',');
           if V_count = 1 then
             V_Sql := V_Sql || ' and m.dept_id is not null and m.dept_id = ' || V_Rangeidlist;
           else
             V_Sql := V_Sql || ' and m.dept_id is not null and m.dept_id in (' || V_Rangeidlist || ')';
           end if;
        end if;
    end if;
    
    -- 绑定特征值类型、数据类型、信号类型、工程单位、报警等级等名称信息
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
    
    -- 查询P_PageIndex为-1的情况下，返回总记录数
    if P_PageIndex = -1 then
		V_SqlCount := 'select count(*) from (' || V_Sql || ') V';
		execute immediate V_SqlCount into P_Count;
		open P_Cursor for select * from dual where 1 <> 1;
		return;
	end if;
	
    -- 排序条件：按照设备编码、设备名称、测点名称升序排列，再按照采样时间降序排列
	if P_PageSize = -1 then
		-- 查询所有数据
		V_Sql := V_Sql || ' order by mob_cd, mobname_tx, pntname_tx asc, sampletime_dt desc';
	else
		-- 查询指定页、指定行数的数据
		V_Sql := DBDiffPackage.GetPagingSQL(V_Sql, V_PageIndex, P_PageSize, 'mob_cd, mobname_tx, pntname_tx asc, sampletime_dt desc');
	end if;
    open P_Cursor for V_Sql;
    return;
end;

-- 在线系统历史数据摘要查询中的测点列表查询 
-- P_AppactionCD 权限
-- P_PostID 用户岗位
-- P_OrgID 公司ID
-- P_MobjectID 设备ID
-- P_PointName 测点名称
-- P_IncludeSubMob 是否包含子设备(1:包含，0:不包含)
-- P_PageSize 分页大小
-- P_PageIndex 页码
-- P_Cursor 输出参数：返回历史摘要数据列表
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
    -- 判断用户岗位是否有权限查询
    V_count := 0;
    select count(*) into V_count from Bs_Postappaction bp where bp.Post_ID = P_PostID and bp.Appaction_Cd = P_AppactionCD;
    if V_count = 0 then
     DBDiffPackage.Pr_DebugPrint('用户没有操作权限');
       open P_Cursor for select * from dual where 1 <> 1;
       return;
    end if;
    
    -- 获取用户岗位对应的部门
    select dept_id into V_DeptID from bs_post where post_id = P_PostID;
    if V_DeptID is null then
      DBDiffPackage.Pr_DebugPrint('用户岗位所在部门不正确');
      open P_Cursor for select * from dual where 1 <> 1;
      return;
    end if;
    
    V_PageIndex := P_PageIndex;
    
    -- 根据条件，组织历史数据查询的SQL语句
    V_Sql := 
      ' select m.mobjectname_tx as mobname_tx, m.mobject_cd as mob_cd, p.point_id as point_id, p.pointname_tx as pntname_tx, p.desc_tx as desc_tx ' || 
               ' from pnt_point p ' || 
               ' left join ( select mob.*, bp.dept_id from mob_mobject mob ' ||  
               ' left join bs_post bp on mob.djowner_id = bp.post_id where mob.active_yn = ''1'' ) m ' || 
               ' left join mob_mobjectstructure ms ' || 
               ' on m.mobject_id = ms.mobject_id ' || 
               ' on p.mobject_id = m.mobject_id ' || 
               ' where p.pointname_tx is not null ';
    
    -- 根据公司查询
    if P_OrgID <> -1 then
	  V_Sql := V_Sql || ' and m.org_id = ' || DBDiffPackage.IntToString(P_OrgID);
    end if;
    
    -- 根据设备查询、根据设备及子设备查询
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
    
    -- 根据测点名称查询
    if DBDiffPackage.GetLength(P_PointName) > 0 then
      V_Sql := V_Sql || ' and p.pointname_tx like ''%' || P_PointName || '%''';
    end if;
    
    -- 根据用户岗位和数据范围查询
    select DataRange_CD into V_DataRangeCD from Bs_Postappaction bp where bp.Post_ID = P_PostID and bp.Appaction_Cd = P_AppactionCD;
    if V_DataRangeCD = 'PE' then --个人
        -- 判断设备分工不为空，且分工岗位与用户岗位一致
        V_Sql := V_Sql || ' and m.djowner_id is not null and m.djowner_id = ' || DBDiffPackage.IntToString(P_PostID);
    elsif V_DataRangeCD = 'DE' then -- 部门
        -- 判断设备分工不为空，且分工岗位所在部门与用户岗位所在部门一致，或者分工岗位所在部门在用户部门数据范围内
        select Rangeidlist_TX into V_Rangeidlist from Bs_Postappaction bp where bp.Post_ID = P_PostID and bp.Appaction_Cd = P_AppactionCD;
        if V_Rangeidlist is null or DBDiffPackage.GetLength(V_Rangeidlist) = 0 then
           -- 分工岗位所在部门与用户岗位所在部门一致
           V_Sql := V_Sql || ' and m.dept_id is not null and m.dept_id = ' || DBDiffPackage.IntToString(V_DeptID);
        else
           -- 分工岗位所在部门在用户部门数据范围内
           V_count := Commonpackage.F_SplitCount(V_Rangeidlist, ',');
           if V_count = 1 then
             V_Sql := V_Sql || ' and m.dept_id is not null and m.dept_id = ' || V_Rangeidlist;
           else
             V_Sql := V_Sql || ' and m.dept_id is not null and m.dept_id in (' || V_Rangeidlist || ')';
           end if;
        end if;
    end if;
    
    DBDiffPackage.Pr_DebugPrint(V_Sql);
    
    -- 查询P_PageIndex为-1的情况下，返回总记录数
    if P_PageIndex = -1 then
		V_SqlCount := 'select count(*) from (' || V_Sql || ') V';
		execute immediate V_SqlCount into P_Count;
		open P_Cursor for select * from dual where 1 <> 1;
		return;
	end if;
    
    -- 查询指定页、指定行数的数据
    -- 排序条件：按照设备编码、设备名称、测点名称升序排列
	V_Sql := DBDiffPackage.GetPagingSQL(V_Sql, V_PageIndex, P_PageSize, 'mob_cd, mobname_tx, pntname_tx asc');    
    open P_Cursor for V_Sql;
    return;
end;

-------------------------------------------------------------------
  --功能说明：根据条件查询未绑定的特征频率分组   by 董建林  2010 10-01
  --参数1：P_OrgID 公司
  --参数2：P_MobjectID 设备
  --参数6：p_pageSize 分页大小
  --参数7：p_pageIndex 页码
  --参数8：P_Cursor 查询结果
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
  
	-- 查询P_PageIndex为-1的情况下，返回总记录数
	if p_pageIndex = -1 then
		V_SqlCount := 'select count(*) from (' || V_Sql || ') V';
		execute immediate V_SqlCount into P_Count;
		open P_Cursor for select * from dual where 1 <> 1;
		return;
	end if;
	
	-- 查询指定页、指定行数的数据
    -- 排序条件：按照特征频率分组升序排列
	V_Sql := DBDiffPackage.GetPagingSQL(V_Sql, V_pageIndex, p_pageSize, 'Group_ID');
	DBDiffPackage.Pr_DebugPrint(V_Sql);
	open P_Cursor for V_Sql;
  end;

end DataQueryPackage;

/