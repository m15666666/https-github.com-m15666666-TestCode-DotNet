create or replace package SMSQueryPackage is

  Type T_Cursor Is Ref Cursor;

-- ����ͳ�Ʋ�ѯ 
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
-- P_PageSize ��ҳ��С
-- P_PageIndex ҳ��
-- P_Cursor ������������ض���ͳ���б�
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

	-- ����ͳ�Ʋ�ѯ 
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
	-- P_PageSize ��ҳ��С
	-- P_PageIndex ҳ��
	-- P_Cursor ���������������ʷժҪ�����б�
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
		  V_Sql := V_Sql || ' and ' || V_SMSHistory || '.point_id = ' || DBDiffPackage.IntToString(P_PointID);
		end if;
    
		-- �����豸�����ѯ
		if DBDiffPackage.GetLength(P_MobjectCD) > 0 then
		  V_Sql := V_Sql || ' and m.mobject_cd = ''' || P_MobjectCD || '''';
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
    
		--�󶨱����ȼ�
		V_Sql := 
			' select sms.*, ha.almlevel_id ' ||
			' from ( ' || V_Sql || ' ) sms ' ||
			' left join zx_history_alm ha ' ||
			' on sms.alm_id = ha.alm_id ';
     
		-- ���ݱ��������ѯ
		if P_AlmLevel <> -1 then
		   V_Sql :=  V_Sql || ' where ' || ' ha.almlevel_id = ' || DBDiffPackage.IntToString(P_AlmLevel);
		end if;
	
		--�󶨱����ȼ�����
		V_Sql := 
			' select sms1.*, al.name_tx as almname_tx ' || 
			' from ( ' || V_Sql || ' ) sms1 ' ||
			' left join zx_history_alm ha ' ||
			' left join zx_almlevel al' ||
			' on ha.almlevel_id = al.refalmlevel_id ' ||
			' on sms1.alm_id = ha.alm_id ';
	
		-- ���û���Ϣ  
		V_Sql := 
			' select sms2.*, au.name_tx as name_tx ' || 
			' from ( ' || V_Sql || ' ) sms2 ' ||
			' left join bs_appuser au ' ||
			' on sms2.appuser_id = au.appuser_id ';
		
		--�󶨷���״̬����
		V_Sql := 
			' select sms3.*, case sms3.status_yn when ''0'' then ''ʧ��'' when ''1'' then ''�ɹ�'' end as status_tx ' || 
			' from ( ' || V_Sql || ' ) sms3 '; 
      
		DBDiffPackage.Pr_DebugPrint(V_Sql);
    
		-- ��ѯP_PageIndexΪ-1������£������ܼ�¼��
		if P_PageIndex = -1 then
			V_SqlCount := 'select count(*) from (' || V_Sql || ') V';
			execute immediate V_SqlCount into P_Count;
			open P_Cursor for select * from dual where 1 <> 1;
			return;
		end if;
	
		-- ���������������豸���롢�豸���ơ���������������У��ٰ��շ���ʱ�併������
		if P_PageSize = -1 then
			-- ��ѯ��������
			V_Sql := V_Sql || ' order by mob_cd, mobname_tx, pntname_tx asc, sendtime_tm desc';
		else
			-- ��ѯָ��ҳ��ָ������������
			V_Sql := DBDiffPackage.GetPagingSQL(V_Sql, V_PageIndex, P_PageSize, 'mob_cd, mobname_tx, pntname_tx asc, sendtime_tm desc');
		end if;
		open P_Cursor for V_Sql;
		return;
	end;

end SMSQueryPackage;

/