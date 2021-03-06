IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SMSQueryPackage].[Pr_QuerySMSHistory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [SMSQueryPackage].[Pr_QuerySMSHistory]
GO

IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'SMSQueryPackage')
DROP SCHEMA [SMSQueryPackage]
GO

/****** 对象:  Schema [CommonPackage]    脚本日期: 12/01/2014 14:32:50 ******/
CREATE SCHEMA [SMSQueryPackage] AUTHORIZATION [dbo]
GO

-- 短信统计查询 
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
-- @P_PageSize 分页大小
-- @P_PageIndex 页码
-- @P_Cursor 输出参数：返回短信统计列表
create procedure SMSQueryPackage.Pr_QuerySMSHistory(
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
            @P_PageSize		int,
			@P_PageIndex int,
			@P_Count int output)
as begin
    declare @V_count int;
    declare @V_DataRangeCD varchar(50);
    declare @V_Rangeidlist varchar(100);
    declare @V_TableBaseName varchar(50);
    declare @V_SMSHistory varchar(50);
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
    
     -- 根据条件，组织短信统计查询的SQL语句
    set @V_TableBaseName =  'Zx_SMSHistory';
    set @V_SMSHistory =  @V_TableBaseName;
    set @V_Sql =  
      ' select m.mobjectname_tx as mobname_tx, m.mobject_cd as mob_cd, p.pointname_tx as pntname_tx, ' + 
               @V_SMSHistory + '.SMShistory_id as SMShistory_id, ' + @V_SMSHistory + '.smscontent_tx as smscontent_tx, ' + @V_SMSHistory + '.sendtime_tm as sendtime_tm, ' +
               @V_SMSHistory + '.status_yn as status_yn, ' + @V_SMSHistory + '.memo_tx as memo_tx, ' + @V_SMSHistory + '.partition_id as partition_id,' + @V_SMSHistory + '.alm_id as alm_id,' + @V_SMSHistory + '.appuser_id ' +
               ' from ' + @V_SMSHistory + 
               ' left join pnt_point p ' + 
               ' left join ( select mob.*, bp.dept_id from mob_mobject mob ' +  
               ' left join bs_post bp on mob.djowner_id = bp.post_id where mob.active_yn = ''1'' ) m ' + 
               ' left join mob_mobjectstructure ms ' + 
               ' on m.mobject_id = ms.mobject_id ' + 
               ' on p.mobject_id = m.mobject_id ' + 
               ' on ' + @V_SMSHistory + '.point_id = p.point_id ' + 
               ' where p.pointname_tx is not null and ' + @V_SMSHistory + '.partition_id <= ' + DBDiffPackage.IntToString(@P_MaxPartitionID) + ' and ' + @V_SMSHistory + '.partition_id >= ' + DBDiffPackage.IntToString(@P_MinPartitionID)+ 
               ' and (' + @V_SMSHistory + '.status_yn = ''1'' or ( ' + @V_SMSHistory + '.status_yn = ''0'' and ' + @V_SMSHistory + '.sendcount_nr >= 3 ))';
    
    
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
      set @V_Sql =  @V_Sql + ' and ' + @V_SMSHistory + '.point_id = ' + DBDiffPackage.IntToString(@P_PointID);
    end;
    
    -- 根据设备编码查询
    if DBDiffPackage.GetLength(@P_MobjectCD) > 0 begin
      set @V_Sql =  @V_Sql + ' and m.mobject_cd = ''' + @P_MobjectCD + '''';
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
    
    --绑定报警等级
    set @V_Sql = 
		' select sms.*, ha.almlevel_id ' + 
		' from ( ' + @V_Sql + ' ) sms ' +
		' left join zx_history_alm ha ' +
		' on sms.alm_id = ha.alm_id ';
     
    -- 根据报警级别查询
    if @P_AlmLevel <> -1 begin
      set @V_Sql =  @V_Sql + ' where ' + ' ha.almlevel_id = ' + DBDiffPackage.IntToString(@P_AlmLevel);
    end;
	
	--绑定报警等级名称
	set @V_Sql = 
		' select sms1.*, al.name_tx as almname_tx ' + 
		' from ( ' + @V_Sql + ' ) sms1 ' +
		' left join zx_history_alm ha ' +
		' left join zx_almlevel al' +
		' on ha.almlevel_id = al.refalmlevel_id ' +
		' on sms1.alm_id = ha.alm_id ';
	
	-- 绑定用户信息  
	set @V_Sql = 
		' select sms2.*, au.name_tx as name_tx ' + 
		' from ( ' + @V_Sql + ' ) sms2 ' +
		' left join bs_appuser au ' +
		' on sms2.appuser_id = au.appuser_id ';
		
	--绑定发送状态名称
	set @V_Sql = 
		' select sms3.*, case sms3.status_yn when ''0'' then ''失败'' when ''1'' then ''成功'' end as status_tx ' + 
		' from ( ' + @V_Sql + ' ) sms3 '; 
      
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
	
	-- 排序条件：按照设备编码、设备名称、测点名称升序排列，再按照发送时间降序排列
	if @P_PageSize = -1 begin
		-- 查询所有数据
		set @V_Sql = @V_Sql + ' order by mob_cd, mobname_tx, pntname_tx asc, sendtime_tm desc';
	end else begin
		-- 查询指定页数据
		set @V_Sql = DBDiffPackage.GetPagingSQL(@V_Sql, @V_PageIndex, @P_PageSize, 'mob_cd, mobname_tx, pntname_tx asc, sendtime_tm desc');
	end;
    exec (@V_Sql);
    return;
end;
GO