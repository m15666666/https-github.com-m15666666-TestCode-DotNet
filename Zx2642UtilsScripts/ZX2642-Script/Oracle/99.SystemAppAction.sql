


--先删除，等菜单回复好以后，去掉上面的语句
--恢复客户设置的菜单名称
begin
For V_DataRow in (Select s.menukey_tx,s.custommenukey_ref,s.customname_tx,jsblock_tx,visible_yn,sortno_nr from BS_MAINMENUTEMP s) Loop
    update BS_MAINMENU set custommenukey_ref = V_DataRow.custommenukey_ref , customname_tx = V_DataRow.customname_tx,jsblock_tx = V_DataRow.jsblock_tx ,visible_yn = V_DataRow.visible_yn,sortno_nr = V_DataRow.sortno_nr where menukey_tx = V_DataRow.menukey_tx;
end loop;
end;
/

--恢复授权
begin
For V_DataRow in (Select appaction_cd,visible_yn from BS_TempAppAction ) Loop
    update BS_AppAction set visible_yn = V_DataRow.visible_yn  where appaction_cd = V_DataRow.appaction_cd;
end loop;
end;
/

--从系统配置恢复通信监控地址
declare
V_XSTComURL varchar2(300);
V_IP varchar2(300);
begin
select Value_TX into V_IP From Bs_Appconfig where appconfig_CD = 'XSTComMM' ;
V_XSTComURL :=  'javascript:openOcxPage(''' || V_IP || ''',''XSTCommunication'');';
update Bs_Mainmenu set JsBlock_TX = V_XSTComURL where menukey_tx = 'XSTComMM';
commit;
end;
/

--恢复查询列表名称
begin
For V_DataRow in (Select s.View_ID,s.ColumnName_TX,s.Columndesc_Tx,Viewcolumn_ID from BS_ViewColumnTemp s) Loop
    update BS_ViewColumn set Columndesc_Tx = V_DataRow.Columndesc_Tx  where   View_ID =  V_DataRow.View_ID and Columnname_Tx = V_DataRow.Columnname_Tx;
end loop;
commit;
end;
/


--恢复编辑界面自定义名称
begin
For V_DataRow in (Select KEY_TX,VALUE_TX,CATEGORY_CD,REMARK_TX,Type_CD,Tip_TX from BS_ResourceTemp  ) Loop
    update BS_Resource set VALUE_TX = V_DataRow.VALUE_TX, REMARK_TX = V_DataRow.REMARK_TX ,Tip_TX = V_DataRow.Tip_TX where KEY_TX = V_DataRow.KEY_TX;
end loop;
end;
/


--重新设置system权限
Delete From Bs_AppRoleAction Where AppRole_ID = 1;
Delete From BS_PostAppAction Where Post_ID = 1;
Insert Into Bs_AppRoleAction(AppRole_ID,AppAction_CD,DataRange_CD,ShortCut_YN) Select 1,AppAction_CD,'AL',1 From Bs_AppAction ;
Insert Into BS_PostAppAction(Post_ID,AppAction_CD,DataRange_CD,ShortCut_YN) Select 1,AppAction_CD,'AL',1 From Bs_AppAction ;
commit;
/


---从81.AlterDate转移到此处 edit by gy 
declare 
V_Count int;
V_MaxID  int;
begin
	 
	---更新数据字典项ObjectKey
	V_Count :=0 ;
	select Count(*) Into V_Count From BS_ObjectKey where  Source_CD = 'BS_DDList';    
	If V_Count != 0 then
		select nvl(max(dd_id),0) + 1 into V_MaxID from BS_DDList;    
		update BS_ObjectKey Set KeyValue = V_MaxID Where Source_CD = 'BS_DDList';   
	End if;
	    
	--更新公司下数据字典
	For V_DataRowType in (Select *  From BS_DDType Where Org_ID = -1  ) Loop
		For V_DataRow in ( Select Org_ID  From V_Org  ) Loop
			V_count := 0;
			select Count(*) into V_count From BS_DDType 
			where DDType_CD = V_DataRowType.Ddtype_Cd
			and Org_ID= V_DataRow.Org_ID;
			if V_count = 0 then 
				Insert Into BS_DDType(DDType_CD,Org_ID,Name_TX,Parent_CD,
					DDTier_YN,DDFlag_YN,FlagDes_TX,
					IsUsing_YN,IsSystem_YN,SortNo_NR,appaction_CD)
				Values(V_DataRowType.Ddtype_Cd,V_DataRow.Org_ID,V_DataRowType.Name_Tx,V_DataRowType.Parent_Cd,
					V_DataRowType.Ddtier_Yn,V_DataRowType.Ddflag_Yn,V_DataRowType.Flagdes_Tx,
					V_DataRowType.Isusing_Yn,V_DataRowType.Issystem_Yn,V_DataRowType.Sortno_Nr,V_DataRowType.Appaction_Cd);  
			End if;  
		end loop;
	end loop;
	commit;

end;
/