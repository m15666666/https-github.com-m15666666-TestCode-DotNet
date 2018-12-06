IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SMSQueryPackage].[Pr_QuerySMSHistory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [SMSQueryPackage].[Pr_QuerySMSHistory]
GO

IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'SMSQueryPackage')
DROP SCHEMA [SMSQueryPackage]
GO

/****** ����:  Schema [CommonPackage]    �ű�����: 12/01/2014 14:32:50 ******/
CREATE SCHEMA [SMSQueryPackage] AUTHORIZATION [dbo]
GO

-- ����ͳ�Ʋ�ѯ 
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
-- @P_PageSize ��ҳ��С
-- @P_PageIndex ҳ��
-- @P_Cursor ������������ض���ͳ���б�
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
    
     -- ������������֯����ͳ�Ʋ�ѯ��SQL���
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
      set @V_Sql =  @V_Sql + ' and ' + @V_SMSHistory + '.point_id = ' + DBDiffPackage.IntToString(@P_PointID);
    end;
    
    -- �����豸�����ѯ
    if DBDiffPackage.GetLength(@P_MobjectCD) > 0 begin
      set @V_Sql =  @V_Sql + ' and m.mobject_cd = ''' + @P_MobjectCD + '''';
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
    
    --�󶨱����ȼ�
    set @V_Sql = 
		' select sms.*, ha.almlevel_id ' + 
		' from ( ' + @V_Sql + ' ) sms ' +
		' left join zx_history_alm ha ' +
		' on sms.alm_id = ha.alm_id ';
     
    -- ���ݱ��������ѯ
    if @P_AlmLevel <> -1 begin
      set @V_Sql =  @V_Sql + ' where ' + ' ha.almlevel_id = ' + DBDiffPackage.IntToString(@P_AlmLevel);
    end;
	
	--�󶨱����ȼ�����
	set @V_Sql = 
		' select sms1.*, al.name_tx as almname_tx ' + 
		' from ( ' + @V_Sql + ' ) sms1 ' +
		' left join zx_history_alm ha ' +
		' left join zx_almlevel al' +
		' on ha.almlevel_id = al.refalmlevel_id ' +
		' on sms1.alm_id = ha.alm_id ';
	
	-- ���û���Ϣ  
	set @V_Sql = 
		' select sms2.*, au.name_tx as name_tx ' + 
		' from ( ' + @V_Sql + ' ) sms2 ' +
		' left join bs_appuser au ' +
		' on sms2.appuser_id = au.appuser_id ';
		
	--�󶨷���״̬����
	set @V_Sql = 
		' select sms3.*, case sms3.status_yn when ''0'' then ''ʧ��'' when ''1'' then ''�ɹ�'' end as status_tx ' + 
		' from ( ' + @V_Sql + ' ) sms3 '; 
      
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
	
	-- ���������������豸���롢�豸���ơ���������������У��ٰ��շ���ʱ�併������
	if @P_PageSize = -1 begin
		-- ��ѯ��������
		set @V_Sql = @V_Sql + ' order by mob_cd, mobname_tx, pntname_tx asc, sendtime_tm desc';
	end else begin
		-- ��ѯָ��ҳ����
		set @V_Sql = DBDiffPackage.GetPagingSQL(@V_Sql, @V_PageIndex, @P_PageSize, 'mob_cd, mobname_tx, pntname_tx asc, sendtime_tm desc');
	end;
    exec (@V_Sql);
    return;
end;
GO