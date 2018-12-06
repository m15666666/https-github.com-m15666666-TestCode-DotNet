IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DataQueryPackage].[Pr_QueryHistorySummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [DataQueryPackage].[Pr_QueryHistorySummary]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DataQueryPackage].[Pr_QueryPointList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [DataQueryPackage].[Pr_QueryPointList]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DataQueryPackage].[pr_QueryNonRelatedFeatureGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [DataQueryPackage].[pr_QueryNonRelatedFeatureGroup]
GO

IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'DataQueryPackage')
DROP SCHEMA [DataQueryPackage]
GO


/****** 对象:  Schema [CommonPackage]    脚本日期: 04/01/2011 10:25:27 ******/
CREATE SCHEMA [DataQueryPackage] AUTHORIZATION [dbo]
GO

-- 在线系统历史数据摘要信息查询 
-- @P_AppactionCD 权限
-- @P_PostID 用户岗位
-- @P_OrgID 公司ID
-- @P_MobjectID 设备ID
-- @P_MobjectCD 设备编码
-- @P_MobjectName 设备名称
-- @P_PointID 测点编号
-- @P_MinPartitionID输入参数： 最小分区编号
-- @P_MaxPartitionID输入参数： 最大分区编号
-- @P_IncludeSubMob 是否包含子设备(1:包含，0:不包含)
-- @P_AlmLevel 报警等级
-- @P_DataType 数据类型
-- @P_SignalTypeID 信号类型
-- @P_PageSize 分页大小
-- @P_PageIndex 页码
-- @P_Cursor 输出参数：返回历史摘要数据列表
create procedure DataQueryPackage.Pr_QueryHistorySummary(
            @P_AppactionCD		varchar(60),
            @P_PostID		int,
            @P_OrgID		int,
            @P_MobjectID		int,
            @P_MobjectCD		varchar(60),
            @P_MobjectName		varchar(100),
            @P_PointID		int,
            @P_MinPartitionID		bigint,
            @P_MaxPartitionID		bigint,
            @P_IncludeSubMob		int,
            @P_AlmLevel		int,
            @P_DataType		int,
            @P_SignalTypeID		int,
            @P_PageSize		int,
			@P_PageIndex int,
			@P_Count int output)
as begin
    declare @V_count int;
    declare @V_DataRangeCD varchar(50);
    declare @V_Rangeidlist varchar(100);
    declare @V_TableBaseName varchar(50);
    declare @V_Summary varchar(50);
    declare @V_QueryObjSql varchar(5000);
    declare @V_Sql varchar(5000);
    declare @V_ParentList varchar(200);
    declare @V_DeptID int;
    declare @V_SqlCount varchar(5000);
	declare @V_PageIndex int;
    
    -- 判断用户岗位是否有权限查询
    set @V_count =  0;
    select @V_count = count(*)  from Bs_Postappaction bp where bp.Post_ID = @P_PostID and bp.Appaction_Cd = @P_AppactionCD;
    if @V_count = 0 begin
		exec DBDiffPackage.Pr_DebugPrint '用户没有操作权限';
		return;
    end;
    
    -- 获取用户岗位对应的部门
    select @V_DeptID = dept_id  from bs_post where post_id = @P_PostID;
    if @V_DeptID is null begin
		exec DBDiffPackage.Pr_DebugPrint '用户岗位所在部门不正确';
		return;
    end;
    
    set @V_PageIndex =  @P_PageIndex;
    
    -- 根据条件，组织历史数据查询的SQL语句
    set @V_TableBaseName =  'Zx_History_Summary';
    set @V_Summary =  @V_TableBaseName + 'His';
    set @V_QueryObjSql =  Commonpackage.F_GetPartitionSQL(@P_MinPartitionID, @P_MaxPartitionID, @V_TableBaseName);
    set @V_Sql =  
      ' select m.mobjectname_tx as mobname_tx, m.mobject_cd as mob_cd, p.pointname_tx as pntname_tx, p.desc_tx as desc_tx, p.engunit_id as engunit_id, p.featurevalue_id as featurevalue_id, ' + 
               @V_Summary + '.history_id as history_id, ' + @V_Summary + '.result_tx as result_tx, ' + @V_Summary + '.almlevel_id as almlevel_id, ' + @V_Summary + '.samptime_dt as sampletime_dt, ' + 
               @V_Summary + '.dattype_nr as datatype_nr, ' + @V_Summary + '.sigtype_nr as signaltype_id, ' + @V_Summary + '.pntdim_nr as pntdim_nr, ' + @V_Summary + '.partition_id as partition_id ' + 
               ' from ' + @V_QueryObjSql + 
               ' left join pnt_point p ' + 
               ' left join ( select mob.*, bp.dept_id from mob_mobject mob ' +  
               ' left join bs_post bp on mob.djowner_id = bp.post_id where mob.active_yn = ''1'' ) m ' + 
               ' left join mob_mobjectstructure ms ' + 
               ' on m.mobject_id = ms.mobject_id ' + 
               ' on p.mobject_id = m.mobject_id ' + 
               ' on ' + @V_Summary + '.point_id = p.point_id ' + 
               ' where p.pointname_tx is not null and ' + @V_Summary + '.partition_id <= ' + DBDiffPackage.IntToString(@P_MaxPartitionID) + ' and ' + @V_Summary + '.partition_id >= ' + DBDiffPackage.IntToString(@P_MinPartitionID);
    
    -- 根据公司查询
    if @P_OrgID <> -1 begin
	  set @V_Sql =  @V_Sql + ' and m.org_id = ' + DBDiffPackage.IntToString(@P_OrgID);
    end;
    
    -- 根据设备查询、根据设备及子设备查询
    if @P_MobjectID <> -1 begin
      if @P_IncludeSubMob = 1 begin
        select @V_ParentList = parentlist_tx  from mob_mobjectstructure where mobject_id = @P_MobjectID;
        if @V_ParentList is not null and DBDiffPackage.GetLength(@V_ParentList) > 0 begin
		  set @V_Sql =  @V_Sql + ' and ms.parentlist_tx like ''' + @V_ParentList + '%''';
		end else begin
		  set @V_Sql =  @V_Sql + ' and m.mobject_id = ' + DBDiffPackage.IntToString(@P_MobjectID);
		end;
      end else begin
        set @V_Sql =  @V_Sql + ' and m.mobject_id = ' + DBDiffPackage.IntToString(@P_MobjectID);
      end;
    end;
    
    -- 根据测点查询
    if @P_PointID <> -1 begin
      set @V_Sql =  @V_Sql + ' and ' + @V_Summary + '.point_id = ' + DBDiffPackage.IntToString(@P_PointID);
    end;
    
    -- 根据设备编码查询
    if DBDiffPackage.GetLength(@P_MobjectCD) > 0 begin
      set @V_Sql =  @V_Sql + ' and m.mobject_cd = ''' + @P_MobjectCD + '''';
    end;
    
    -- 根据报警级别查询
    if @P_AlmLevel <> -1 begin
      set @V_Sql =  @V_Sql + ' and ' + @V_Summary + '.almlevel_id = ' + DBDiffPackage.IntToString(@P_AlmLevel);
    end;
    
    -- 根据数据类型查询
    if @P_DataType <> -1 begin
      set @V_Sql =  @V_Sql + ' and ' + @V_Summary + '.dattype_nr = ' + DBDiffPackage.IntToString(@P_DataType);
    end;
    
    -- 根据信息号类型查询
    if @P_SignalTypeID <> -1 begin
      set @V_Sql =  @V_Sql + ' and ' + @V_Summary + '.sigtype_nr = ' + DBDiffPackage.IntToString(@P_SignalTypeID);
    end;
    
    -- 根据设备名称查询
    if DBDiffPackage.GetLength(@P_MobjectName) > 0 begin
      set @V_Sql =  @V_Sql + ' and m.mobjectname_tx like ''%' + @P_MobjectName + '%''';
    end;
    
    -- 根据用户岗位和数据范围查询
    select @V_DataRangeCD = DataRange_CD  from Bs_Postappaction bp where bp.Post_ID = @P_PostID and bp.Appaction_Cd = @P_AppactionCD;
    if @V_DataRangeCD = 'PE' begin --个人
        -- 判断设备分工不为空，且分工岗位与用户岗位一致
        set @V_Sql =  @V_Sql + ' and m.djowner_id is not null and m.djowner_id = ' + DBDiffPackage.IntToString(@P_PostID);
    end else if @V_DataRangeCD = 'DE' begin -- 部门
        -- 判断设备分工不为空，且分工岗位所在部门与用户岗位所在部门一致，或者分工岗位所在部门在用户部门数据范围内
        select @V_Rangeidlist = Rangeidlist_TX  from Bs_Postappaction bp where bp.Post_ID = @P_PostID and bp.Appaction_Cd = @P_AppactionCD;
        if @V_Rangeidlist is null or DBDiffPackage.GetLength(@V_Rangeidlist) = 0 begin
           -- 分工岗位所在部门与用户岗位所在部门一致
           set @V_Sql =  @V_Sql + ' and m.dept_id is not null and m.dept_id = ' + DBDiffPackage.IntToString(@V_DeptID);
        end else begin
           -- 分工岗位所在部门在用户部门数据范围内
           set @V_count =  Commonpackage.F_SplitCount(@V_Rangeidlist, ',');
           if @V_count = 1 begin
             set @V_Sql =  @V_Sql + ' and m.dept_id is not null and m.dept_id = ' + @V_Rangeidlist;
           end else begin
             set @V_Sql =  @V_Sql + ' and m.dept_id is not null and m.dept_id in (' + @V_Rangeidlist + ')';
           end;
        end;
    end;
    
    -- 绑定特征值类型、数据类型、信号类型、工程单位、报警等级等名称信息
	set @V_Sql =  
		' select summary.*, '''' as measvalue_tx, '''' as engname_tx, ' + 
		' eng.namee_tx as engename_tx, eng.namec_tx as engcname_tx, ' + 
		' '''' as almname_tx, ft.name_tx as featurename_tx, ' + 
		' d.onlinename_tx as datatypename_tx, s.name_tx as signaltypename_tx ' + 
		' from ( ' + @V_Sql + ' ) summary ' + 
		' left join z_engunit eng on summary.engunit_id = eng.engunit_id ' + 
		' left join z_featurevalue ft on summary.featurevalue_id = ft.featurevalue_id ' + 
		' left join z_datatype d on summary.datatype_nr = d.datatype_id ' + 
		' left join z_signtype s on summary.signaltype_id = s.signtype_id ';   
      
	exec DBDiffPackage.Pr_DebugPrint @V_Sql;
    
	-- 返回记录总数
	if @P_PageIndex = -1 begin
		set @V_SqlCount = 'select count(*) from (' + @V_Sql + ') V';
		exec ('declare _ScalarCuror cursor for ' + @V_SqlCount);
		open _ScalarCuror;
		fetch next from _ScalarCuror into @P_Count;
		close _ScalarCuror;
		deallocate _ScalarCuror;
		return;
	end;
	
	-- 排序条件：按照设备编码、设备名称、测点名称升序排列，再按照采样时间降序排列
	if @P_PageSize = -1 begin
		-- 查询所有数据
		set @V_Sql = @V_Sql + ' order by mob_cd, mobname_tx, pntname_tx asc, sampletime_dt desc';
	end else begin
		-- 查询指定页数据
		set @V_Sql = DBDiffPackage.GetPagingSQL(@V_Sql, @V_PageIndex, @P_PageSize, 'mob_cd, mobname_tx, pntname_tx asc, sampletime_dt desc');
	end;
    exec (@V_Sql);
    return;
end;
GO

-- 在线系统历史数据摘要查询中的测点列表查询 
-- @P_AppactionCD 权限
-- @P_PostID 用户岗位
-- @P_OrgID 公司ID
-- @P_MobjectID 设备ID
-- @P_PointName 测点名称
-- @P_IncludeSubMob 是否包含子设备(1:包含，0:不包含)
-- @P_PageSize 分页大小
-- @P_PageIndex 页码
-- @P_Cursor 输出参数：返回历史摘要数据列表
create procedure DataQueryPackage.Pr_QueryPointList(
            @P_AppactionCD		varchar(60),
            @P_PostID		int,
            @P_OrgID		int,
            @P_MobjectID		int,
            @P_PointName		varchar(200),
            @P_IncludeSubMob		int,
            @P_PageSize		int,
			@P_PageIndex int,
			@P_Count int output)
as begin
    declare @V_count int;
    declare @V_DataRangeCD varchar(50);
    declare @V_Rangeidlist varchar(100);
    declare @V_Sql varchar(5000);
    declare @V_ParentList varchar(200);
    declare @V_DeptID int;
    declare @V_SqlCount varchar(5000);
	declare @V_PageIndex int;
    
    -- 判断用户岗位是否有权限查询
    set @V_count =  0;
    select @V_count = count(*)  from Bs_Postappaction bp where bp.Post_ID = @P_PostID and bp.Appaction_Cd = @P_AppactionCD;
	if @V_count = 0 begin
		exec DBDiffPackage.Pr_DebugPrint '用户没有操作权限';
		return;
	end;
    
    -- 获取用户岗位对应的部门
    select @V_DeptID = dept_id  from bs_post where post_id = @P_PostID;
	if @V_DeptID is null begin
		exec DBDiffPackage.Pr_DebugPrint '用户岗位所在部门不正确';
		return;
	end;
    
    set @V_PageIndex =  @P_PageIndex;
    
    -- 根据条件，组织历史数据查询的SQL语句
	set @V_Sql =  
		' select m.mobjectname_tx as mobname_tx, m.mobject_cd as mob_cd, p.point_id as point_id, p.pointname_tx as pntname_tx, p.desc_tx as desc_tx ' + 
		' from pnt_point p ' + 
		' left join ( select mob.*, bp.dept_id from mob_mobject mob ' +  
		' left join bs_post bp on mob.djowner_id = bp.post_id where mob.active_yn = ''1'' ) m ' + 
		' left join mob_mobjectstructure ms ' + 
		' on m.mobject_id = ms.mobject_id ' + 
		' on p.mobject_id = m.mobject_id ' + 
		' where p.pointname_tx is not null ';
    
    -- 根据公司查询
    if @P_OrgID <> -1 begin
	  set @V_Sql =  @V_Sql + ' and m.org_id = ' + DBDiffPackage.IntToString(@P_OrgID);
    end;
    
    -- 根据设备查询、根据设备及子设备查询
    if @P_MobjectID <> -1 begin
		if @P_IncludeSubMob = 1 begin
		  select @V_ParentList = parentlist_tx  from mob_mobjectstructure where mobject_id = @P_MobjectID;
		  if @V_ParentList is not null and DBDiffPackage.GetLength(@V_ParentList) > 0 begin
			set @V_Sql =  @V_Sql + ' and ms.parentlist_tx like ''' + @V_ParentList + '%''';
		  end else begin
			set @V_Sql =  @V_Sql + ' and m.mobject_id = ' + DBDiffPackage.IntToString(@P_MobjectID);
		  end;
		end else begin
		  set @V_Sql =  @V_Sql + ' and m.mobject_id = ' + DBDiffPackage.IntToString(@P_MobjectID);
		end;
	end;
    
    -- 根据测点名称查询
    if DBDiffPackage.GetLength(@P_PointName) > 0 begin
      set @V_Sql =  @V_Sql + ' and p.pointname_tx like ''%' + @P_PointName + '%''';
    end;
    
    -- 根据用户岗位和数据范围查询
    select @V_DataRangeCD = DataRange_CD  from Bs_Postappaction bp where bp.Post_ID = @P_PostID and bp.Appaction_Cd = @P_AppactionCD;
    if @V_DataRangeCD = 'PE' begin --个人
        -- 判断设备分工不为空，且分工岗位与用户岗位一致
        set @V_Sql =  @V_Sql + ' and m.djowner_id is not null and m.djowner_id = ' + DBDiffPackage.IntToString(@P_PostID);
    end else if @V_DataRangeCD = 'DE' begin -- 部门
        -- 判断设备分工不为空，且分工岗位所在部门与用户岗位所在部门一致，或者分工岗位所在部门在用户部门数据范围内
        select @V_Rangeidlist = Rangeidlist_TX  from Bs_Postappaction bp where bp.Post_ID = @P_PostID and bp.Appaction_Cd = @P_AppactionCD;
        if @V_Rangeidlist is null or DBDiffPackage.GetLength(@V_Rangeidlist) = 0 begin
           -- 分工岗位所在部门与用户岗位所在部门一致
           set @V_Sql =  @V_Sql + ' and m.dept_id is not null and m.dept_id = ' + DBDiffPackage.IntToString(@V_DeptID);
        end else begin
           -- 分工岗位所在部门在用户部门数据范围内
           set @V_count =  Commonpackage.F_SplitCount(@V_Rangeidlist, ',');
           if @V_count = 1 begin
             set @V_Sql =  @V_Sql + ' and m.dept_id is not null and m.dept_id = ' + @V_Rangeidlist;
           end else begin
             set @V_Sql =  @V_Sql + ' and m.dept_id is not null and m.dept_id in (' + @V_Rangeidlist + ')';
           end;
        end;
    end;
    
	exec DBDiffPackage.Pr_DebugPrint @V_Sql;
    
    -- 返回记录总数
	if @P_PageIndex = -1 begin
		set @V_SqlCount = 'select count(*) from (' + @V_Sql + ') V';
		exec ('declare _ScalarCuror cursor for ' + @V_SqlCount);
		open _ScalarCuror;
		fetch next from _ScalarCuror into @P_Count;
		close _ScalarCuror;
		deallocate _ScalarCuror;
		return;
	end;
	
	-- 排序条件：按照设备编码、设备名称、测点名称升序排列
	-- 查询指定页数据
	set @V_Sql = DBDiffPackage.GetPagingSQL(@V_Sql, @V_PageIndex, @P_PageSize, 'mob_cd, mobname_tx, pntname_tx asc');
    exec (@V_Sql);
    return;
end;
GO

-------------------------------------------------------------------
  --功能说明：根据条件查询未绑定的特征频率分组   by 董建林  2010 10-01
  --参数1：@P_OrgID 公司
  --参数2：@P_MobjectID 设备
  --参数6：@P_pageSize 分页大小
  --参数7：@P_pageIndex 页码
  --参数8：@P_Cursor 查询结果
  -------------------------------------------------------------------
  create procedure DataQueryPackage.pr_QueryNonRelatedFeatureGroup(
            @P_OrgID int,
            @P_MobjectID int,
            @P_pageSize  int,
            @P_pageIndex int,
			@P_Count int output)

  as begin
	declare @V_Sql varchar(2000);
	declare @V_SqlCount varchar(2000);  
	declare @V_PageIndex int;
	set @V_PageIndex =  @P_pageIndex;
	set @V_Sql = 'select g.group_id, g.groupname_tx from analysis_featurefreqgroup g where g.org_id =' + DBDiffPackage.IntToString(@P_OrgID) + 
					' and not exists( select 1 from mob_featrefreqgroup mf where mf.mobject_id =' + DBDiffPackage.IntToString(@P_MobjectID) + '  and mf.group_id = g.group_id)';
	
	exec DBDiffPackage.Pr_DebugPrint @V_Sql;
	
	-- 返回记录总数
	if @P_pageIndex = -1 begin
		set @V_SqlCount =  'select Count(*) From (' + @V_Sql + ') V';
		exec ('declare _ScalarCuror cursor for ' + @V_SqlCount);
		open _ScalarCuror;
		fetch next from _ScalarCuror into @P_Count;
		close _ScalarCuror;
		deallocate _ScalarCuror;
		return;
	end;

	-- 排序条件：分组编号
	-- 查询指定页数据
	set @V_Sql = DBDiffPackage.GetPagingSQL(@V_Sql, @V_PageIndex, @P_PageSize, 'Group_ID');
	exec (@V_Sql);
	return;
end;
GO