create or replace package SMSQueryPackage is

  Type T_Cursor Is Ref Cursor;

-- 短信统计查询 
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
-- P_PageSize 分页大小
-- P_PageIndex 页码
-- P_Cursor 输出参数：返回短信统计列表
procedure Pr_QuerySMSHistory(
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
            P_PageSize			in		BS_MetaFieldType.Int_NR%type,
			P_PageIndex			in		BS_MetaFieldType.Int_NR%type,
			P_Count				out		BS_MetaFieldType.Int_NR%type, P_Cursor            out		T_Cursor);

end SMSQueryPackage;

/

create or replace package body SMSQueryPackage is  

	-- 短信统计查询 
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
	-- P_PageSize 分页大小
	-- P_PageIndex 页码
	-- P_Cursor 输出参数：返回历史摘要数据列表
	procedure Pr_QuerySMSHistory(
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
				P_PageSize			in		BS_MetaFieldType.Int_NR%type,
				P_PageIndex			in		BS_MetaFieldType.Int_NR%type,
				P_Count				out		BS_MetaFieldType.Int_NR%type, P_Cursor            out		T_Cursor)
	as
		V_count         BS_MetaFieldType.Int_NR%type;
		V_DataRangeCD   BS_MetaFieldType.Fifty_TX%type;
		V_Rangeidlist   BS_MetaFieldType.OneHundred_TX%type;
		V_TableBaseName BS_MetaFieldType.Fifty_TX%type;
		V_SMSHistory	BS_MetaFieldType.Fifty_TX%type;
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
		V_TableBaseName := 'Zx_SMSHistory';
		V_SMSHistory := V_TableBaseName;
		V_Sql := 
		   ' select m.mobjectname_tx as mobname_tx, m.mobject_cd as mob_cd, p.pointname_tx as pntname_tx, ' || 
               V_SMSHistory || '.SMShistory_id as SMShistory_id, ' || V_SMSHistory || '.smscontent_tx as smscontent_tx, ' || V_SMSHistory || '.sendtime_tm as sendtime_tm, ' ||
               V_SMSHistory || '.status_yn as status_yn, ' || V_SMSHistory || '.memo_tx as memo_tx, ' || V_SMSHistory || '.partition_id as partition_id,' || V_SMSHistory || '.alm_id as alm_id,' || V_SMSHistory || '.appuser_id ' ||
               ' from ' || V_SMSHistory || 
               ' left join pnt_point p ' || 
               ' left join ( select mob.*, bp.dept_id from mob_mobject mob ' ||  
               ' left join bs_post bp on mob.djowner_id = bp.post_id where mob.active_yn = ''1'' ) m ' || 
               ' left join mob_mobjectstructure ms ' || 
               ' on m.mobject_id = ms.mobject_id ' || 
               ' on p.mobject_id = m.mobject_id ' || 
               ' on ' || V_SMSHistory || '.point_id = p.point_id ' || 
               ' where p.pointname_tx is not null and ' || V_SMSHistory || '.partition_id <= ' || DBDiffPackage.IntToString(P_MaxPartitionID) || ' and ' || V_SMSHistory || '.partition_id >= ' || DBDiffPackage.IntToString(P_MinPartitionID)|| 
               ' and (' || V_SMSHistory || '.status_yn = ''1'' or ( ' || V_SMSHistory || '.status_yn = ''0'' and ' || V_SMSHistory || '.sendcount_nr >= 3 ))';
    
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
		  V_Sql := V_Sql || ' and ' || V_SMSHistory || '.point_id = ' || DBDiffPackage.IntToString(P_PointID);
		end if;
    
		-- 根据设备编码查询
		if DBDiffPackage.GetLength(P_MobjectCD) > 0 then
		  V_Sql := V_Sql || ' and m.mobject_cd = ''' || P_MobjectCD || '''';
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
    
		--绑定报警等级
		V_Sql := 
			' select sms.*, ha.almlevel_id ' ||
			' from ( ' || V_Sql || ' ) sms ' ||
			' left join zx_history_alm ha ' ||
			' on sms.alm_id = ha.alm_id ';
     
		-- 根据报警级别查询
		if P_AlmLevel <> -1 then
		   V_Sql :=  V_Sql || ' where ' || ' ha.almlevel_id = ' || DBDiffPackage.IntToString(P_AlmLevel);
		end if;
	
		--绑定报警等级名称
		V_Sql := 
			' select sms1.*, al.name_tx as almname_tx ' || 
			' from ( ' || V_Sql || ' ) sms1 ' ||
			' left join zx_history_alm ha ' ||
			' left join zx_almlevel al' ||
			' on ha.almlevel_id = al.refalmlevel_id ' ||
			' on sms1.alm_id = ha.alm_id ';
	
		-- 绑定用户信息  
		V_Sql := 
			' select sms2.*, au.name_tx as name_tx ' || 
			' from ( ' || V_Sql || ' ) sms2 ' ||
			' left join bs_appuser au ' ||
			' on sms2.appuser_id = au.appuser_id ';
		
		--绑定发送状态名称
		V_Sql := 
			' select sms3.*, case sms3.status_yn when ''0'' then ''失败'' when ''1'' then ''成功'' end as status_tx ' || 
			' from ( ' || V_Sql || ' ) sms3 '; 
      
		DBDiffPackage.Pr_DebugPrint(V_Sql);
    
		-- 查询P_PageIndex为-1的情况下，返回总记录数
		if P_PageIndex = -1 then
			V_SqlCount := 'select count(*) from (' || V_Sql || ') V';
			execute immediate V_SqlCount into P_Count;
			open P_Cursor for select * from dual where 1 <> 1;
			return;
		end if;
	
		-- 排序条件：按照设备编码、设备名称、测点名称升序排列，再按照发送时间降序排列
		if P_PageSize = -1 then
			-- 查询所有数据
			V_Sql := V_Sql || ' order by mob_cd, mobname_tx, pntname_tx asc, sendtime_tm desc';
		else
			-- 查询指定页、指定行数的数据
			V_Sql := DBDiffPackage.GetPagingSQL(V_Sql, V_PageIndex, P_PageSize, 'mob_cd, mobname_tx, pntname_tx asc, sendtime_tm desc');
		end if;
		open P_Cursor for V_Sql;
		return;
	end;

end SMSQueryPackage;

/