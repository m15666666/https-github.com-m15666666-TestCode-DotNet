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


/****** ����:  Schema [CommonPackage]    �ű�����: 04/01/2011 10:25:27 ******/
CREATE SCHEMA [DataQueryPackage] AUTHORIZATION [dbo]
GO

-- ����ϵͳ��ʷ����ժҪ��Ϣ��ѯ 
-- @P_AppactionCD Ȩ��
-- @P_PostID �û���λ
-- @P_OrgID ��˾ID
-- @P_MobjectID �豸ID
-- @P_MobjectCD �豸����
-- @P_MobjectName �豸����
-- @P_PointID �����
-- @P_MinPartitionID��������� ��С�������
-- @P_MaxPartitionID��������� ���������
-- @P_IncludeSubMob �Ƿ�������豸(1:������0:������)
-- @P_AlmLevel �����ȼ�
-- @P_DataType ��������
-- @P_SignalTypeID �ź�����
-- @P_PageSize ��ҳ��С
-- @P_PageIndex ҳ��
-- @P_Cursor ���������������ʷժҪ�����б�
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
    
    -- �ж��û���λ�Ƿ���Ȩ�޲�ѯ
    set @V_count =  0;
    select @V_count = count(*)  from Bs_Postappaction bp where bp.Post_ID = @P_PostID and bp.Appaction_Cd = @P_AppactionCD;
    if @V_count = 0 begin
		exec DBDiffPackage.Pr_DebugPrint '�û�û�в���Ȩ��';
		return;
    end;
    
    -- ��ȡ�û���λ��Ӧ�Ĳ���
    select @V_DeptID = dept_id  from bs_post where post_id = @P_PostID;
    if @V_DeptID is null begin
		exec DBDiffPackage.Pr_DebugPrint '�û���λ���ڲ��Ų���ȷ';
		return;
    end;
    
    set @V_PageIndex =  @P_PageIndex;
    
    -- ������������֯��ʷ���ݲ�ѯ��SQL���
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
    
    -- ���ݹ�˾��ѯ
    if @P_OrgID <> -1 begin
	  set @V_Sql =  @V_Sql + ' and m.org_id = ' + DBDiffPackage.IntToString(@P_OrgID);
    end;
    
    -- �����豸��ѯ�������豸�����豸��ѯ
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
    
    -- ���ݲ���ѯ
    if @P_PointID <> -1 begin
      set @V_Sql =  @V_Sql + ' and ' + @V_Summary + '.point_id = ' + DBDiffPackage.IntToString(@P_PointID);
    end;
    
    -- �����豸�����ѯ
    if DBDiffPackage.GetLength(@P_MobjectCD) > 0 begin
      set @V_Sql =  @V_Sql + ' and m.mobject_cd = ''' + @P_MobjectCD + '''';
    end;
    
    -- ���ݱ��������ѯ
    if @P_AlmLevel <> -1 begin
      set @V_Sql =  @V_Sql + ' and ' + @V_Summary + '.almlevel_id = ' + DBDiffPackage.IntToString(@P_AlmLevel);
    end;
    
    -- �����������Ͳ�ѯ
    if @P_DataType <> -1 begin
      set @V_Sql =  @V_Sql + ' and ' + @V_Summary + '.dattype_nr = ' + DBDiffPackage.IntToString(@P_DataType);
    end;
    
    -- ������Ϣ�����Ͳ�ѯ
    if @P_SignalTypeID <> -1 begin
      set @V_Sql =  @V_Sql + ' and ' + @V_Summary + '.sigtype_nr = ' + DBDiffPackage.IntToString(@P_SignalTypeID);
    end;
    
    -- �����豸���Ʋ�ѯ
    if DBDiffPackage.GetLength(@P_MobjectName) > 0 begin
      set @V_Sql =  @V_Sql + ' and m.mobjectname_tx like ''%' + @P_MobjectName + '%''';
    end;
    
    -- �����û���λ�����ݷ�Χ��ѯ
    select @V_DataRangeCD = DataRange_CD  from Bs_Postappaction bp where bp.Post_ID = @P_PostID and bp.Appaction_Cd = @P_AppactionCD;
    if @V_DataRangeCD = 'PE' begin --����
        -- �ж��豸�ֹ���Ϊ�գ��ҷֹ���λ���û���λһ��
        set @V_Sql =  @V_Sql + ' and m.djowner_id is not null and m.djowner_id = ' + DBDiffPackage.IntToString(@P_PostID);
    end else if @V_DataRangeCD = 'DE' begin -- ����
        -- �ж��豸�ֹ���Ϊ�գ��ҷֹ���λ���ڲ������û���λ���ڲ���һ�£����߷ֹ���λ���ڲ������û��������ݷ�Χ��
        select @V_Rangeidlist = Rangeidlist_TX  from Bs_Postappaction bp where bp.Post_ID = @P_PostID and bp.Appaction_Cd = @P_AppactionCD;
        if @V_Rangeidlist is null or DBDiffPackage.GetLength(@V_Rangeidlist) = 0 begin
           -- �ֹ���λ���ڲ������û���λ���ڲ���һ��
           set @V_Sql =  @V_Sql + ' and m.dept_id is not null and m.dept_id = ' + DBDiffPackage.IntToString(@V_DeptID);
        end else begin
           -- �ֹ���λ���ڲ������û��������ݷ�Χ��
           set @V_count =  Commonpackage.F_SplitCount(@V_Rangeidlist, ',');
           if @V_count = 1 begin
             set @V_Sql =  @V_Sql + ' and m.dept_id is not null and m.dept_id = ' + @V_Rangeidlist;
           end else begin
             set @V_Sql =  @V_Sql + ' and m.dept_id is not null and m.dept_id in (' + @V_Rangeidlist + ')';
           end;
        end;
    end;
    
    -- ������ֵ���͡��������͡��ź����͡����̵�λ�������ȼ���������Ϣ
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
    
	-- ���ؼ�¼����
	if @P_PageIndex = -1 begin
		set @V_SqlCount = 'select count(*) from (' + @V_Sql + ') V';
		exec ('declare _ScalarCuror cursor for ' + @V_SqlCount);
		open _ScalarCuror;
		fetch next from _ScalarCuror into @P_Count;
		close _ScalarCuror;
		deallocate _ScalarCuror;
		return;
	end;
	
	-- ���������������豸���롢�豸���ơ���������������У��ٰ��ղ���ʱ�併������
	if @P_PageSize = -1 begin
		-- ��ѯ��������
		set @V_Sql = @V_Sql + ' order by mob_cd, mobname_tx, pntname_tx asc, sampletime_dt desc';
	end else begin
		-- ��ѯָ��ҳ����
		set @V_Sql = DBDiffPackage.GetPagingSQL(@V_Sql, @V_PageIndex, @P_PageSize, 'mob_cd, mobname_tx, pntname_tx asc, sampletime_dt desc');
	end;
    exec (@V_Sql);
    return;
end;
GO

-- ����ϵͳ��ʷ����ժҪ��ѯ�еĲ���б��ѯ 
-- @P_AppactionCD Ȩ��
-- @P_PostID �û���λ
-- @P_OrgID ��˾ID
-- @P_MobjectID �豸ID
-- @P_PointName �������
-- @P_IncludeSubMob �Ƿ�������豸(1:������0:������)
-- @P_PageSize ��ҳ��С
-- @P_PageIndex ҳ��
-- @P_Cursor ���������������ʷժҪ�����б�
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
    
    -- �ж��û���λ�Ƿ���Ȩ�޲�ѯ
    set @V_count =  0;
    select @V_count = count(*)  from Bs_Postappaction bp where bp.Post_ID = @P_PostID and bp.Appaction_Cd = @P_AppactionCD;
	if @V_count = 0 begin
		exec DBDiffPackage.Pr_DebugPrint '�û�û�в���Ȩ��';
		return;
	end;
    
    -- ��ȡ�û���λ��Ӧ�Ĳ���
    select @V_DeptID = dept_id  from bs_post where post_id = @P_PostID;
	if @V_DeptID is null begin
		exec DBDiffPackage.Pr_DebugPrint '�û���λ���ڲ��Ų���ȷ';
		return;
	end;
    
    set @V_PageIndex =  @P_PageIndex;
    
    -- ������������֯��ʷ���ݲ�ѯ��SQL���
	set @V_Sql =  
		' select m.mobjectname_tx as mobname_tx, m.mobject_cd as mob_cd, p.point_id as point_id, p.pointname_tx as pntname_tx, p.desc_tx as desc_tx ' + 
		' from pnt_point p ' + 
		' left join ( select mob.*, bp.dept_id from mob_mobject mob ' +  
		' left join bs_post bp on mob.djowner_id = bp.post_id where mob.active_yn = ''1'' ) m ' + 
		' left join mob_mobjectstructure ms ' + 
		' on m.mobject_id = ms.mobject_id ' + 
		' on p.mobject_id = m.mobject_id ' + 
		' where p.pointname_tx is not null ';
    
    -- ���ݹ�˾��ѯ
    if @P_OrgID <> -1 begin
	  set @V_Sql =  @V_Sql + ' and m.org_id = ' + DBDiffPackage.IntToString(@P_OrgID);
    end;
    
    -- �����豸��ѯ�������豸�����豸��ѯ
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
    
    -- ���ݲ�����Ʋ�ѯ
    if DBDiffPackage.GetLength(@P_PointName) > 0 begin
      set @V_Sql =  @V_Sql + ' and p.pointname_tx like ''%' + @P_PointName + '%''';
    end;
    
    -- �����û���λ�����ݷ�Χ��ѯ
    select @V_DataRangeCD = DataRange_CD  from Bs_Postappaction bp where bp.Post_ID = @P_PostID and bp.Appaction_Cd = @P_AppactionCD;
    if @V_DataRangeCD = 'PE' begin --����
        -- �ж��豸�ֹ���Ϊ�գ��ҷֹ���λ���û���λһ��
        set @V_Sql =  @V_Sql + ' and m.djowner_id is not null and m.djowner_id = ' + DBDiffPackage.IntToString(@P_PostID);
    end else if @V_DataRangeCD = 'DE' begin -- ����
        -- �ж��豸�ֹ���Ϊ�գ��ҷֹ���λ���ڲ������û���λ���ڲ���һ�£����߷ֹ���λ���ڲ������û��������ݷ�Χ��
        select @V_Rangeidlist = Rangeidlist_TX  from Bs_Postappaction bp where bp.Post_ID = @P_PostID and bp.Appaction_Cd = @P_AppactionCD;
        if @V_Rangeidlist is null or DBDiffPackage.GetLength(@V_Rangeidlist) = 0 begin
           -- �ֹ���λ���ڲ������û���λ���ڲ���һ��
           set @V_Sql =  @V_Sql + ' and m.dept_id is not null and m.dept_id = ' + DBDiffPackage.IntToString(@V_DeptID);
        end else begin
           -- �ֹ���λ���ڲ������û��������ݷ�Χ��
           set @V_count =  Commonpackage.F_SplitCount(@V_Rangeidlist, ',');
           if @V_count = 1 begin
             set @V_Sql =  @V_Sql + ' and m.dept_id is not null and m.dept_id = ' + @V_Rangeidlist;
           end else begin
             set @V_Sql =  @V_Sql + ' and m.dept_id is not null and m.dept_id in (' + @V_Rangeidlist + ')';
           end;
        end;
    end;
    
	exec DBDiffPackage.Pr_DebugPrint @V_Sql;
    
    -- ���ؼ�¼����
	if @P_PageIndex = -1 begin
		set @V_SqlCount = 'select count(*) from (' + @V_Sql + ') V';
		exec ('declare _ScalarCuror cursor for ' + @V_SqlCount);
		open _ScalarCuror;
		fetch next from _ScalarCuror into @P_Count;
		close _ScalarCuror;
		deallocate _ScalarCuror;
		return;
	end;
	
	-- ���������������豸���롢�豸���ơ����������������
	-- ��ѯָ��ҳ����
	set @V_Sql = DBDiffPackage.GetPagingSQL(@V_Sql, @V_PageIndex, @P_PageSize, 'mob_cd, mobname_tx, pntname_tx asc');
    exec (@V_Sql);
    return;
end;
GO

-------------------------------------------------------------------
  --����˵��������������ѯδ�󶨵�����Ƶ�ʷ���   by ������  2010 10-01
  --����1��@P_OrgID ��˾
  --����2��@P_MobjectID �豸
  --����6��@P_pageSize ��ҳ��С
  --����7��@P_pageIndex ҳ��
  --����8��@P_Cursor ��ѯ���
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
	
	-- ���ؼ�¼����
	if @P_pageIndex = -1 begin
		set @V_SqlCount =  'select Count(*) From (' + @V_Sql + ') V';
		exec ('declare _ScalarCuror cursor for ' + @V_SqlCount);
		open _ScalarCuror;
		fetch next from _ScalarCuror into @P_Count;
		close _ScalarCuror;
		deallocate _ScalarCuror;
		return;
	end;

	-- ����������������
	-- ��ѯָ��ҳ����
	set @V_Sql = DBDiffPackage.GetPagingSQL(@V_Sql, @V_PageIndex, @P_PageSize, 'Group_ID');
	exec (@V_Sql);
	return;
end;
GO