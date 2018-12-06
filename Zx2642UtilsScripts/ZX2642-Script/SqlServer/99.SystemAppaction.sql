--恢复客户自定义菜单名称
Declare 
    @menukey_tx varchar(100),
    @custommenukey_ref varchar(100),
    @jsblock_tx  varchar(200),
    @Tvisible_yn varchar(100),
    @sortno_nr varchar(100),
    @customname_tx varchar(100);
declare	memu_Cursor Cursor For
    Select s.menukey_tx,s.custommenukey_ref,s.customname_tx,jsblock_tx, visible_yn,sortno_nr from BS_MAINMENUTEMP s;
Open memu_Cursor;
While ( 1=1 )
Begin
Fetch memu_Cursor Into @menukey_tx,@custommenukey_ref,@customname_tx,@jsblock_tx,@Tvisible_yn,@sortno_nr
if @@Fetch_Status  = 0  --没有错误
   begin
		 update BS_MAINMENU set custommenukey_ref =@custommenukey_ref , customname_tx = @customname_tx,jsblock_tx = @jsblock_tx ,visible_yn =@Tvisible_yn,sortno_nr=@sortno_nr where menukey_tx = @menukey_tx;
   end 
else--结束
	Break;
End
Close memu_Cursor;
Deallocate memu_Cursor;



--恢复授权
Declare 
    @appaction_cd varchar(100),
    @visible_yn varchar(100);
declare	memu_Cursor Cursor For
    Select appaction_cd,visible_yn from BS_TempAppAction s;
Open memu_Cursor;
While ( 1=1 )
Begin
Fetch memu_Cursor Into @appaction_cd,@visible_yn;
if @@Fetch_Status  = 0  --没有错误
   begin
		update BS_AppAction set visible_yn = @visible_yn  where appaction_cd = @appaction_cd;
   end 
else--结束
	Break;
End
Close memu_Cursor;
Deallocate memu_Cursor;


--恢复查询列表名称
Declare 
    @View_ID int,
    @V_ViewcolumnID int,
    @ColumnName_TX varchar(100),
    @Columndesc_Tx varchar(100);
declare	memu_Cursor Cursor For
    Select View_ID,ColumnName_TX,Columndesc_Tx ,Viewcolumn_ID from BS_ViewColumnTemp s;
Open memu_Cursor;
While ( 1=1 )
Begin
Fetch memu_Cursor Into @View_ID,@ColumnName_TX,@Columndesc_Tx,@V_ViewcolumnID;
if @@Fetch_Status  = 0  --没有错误
   begin
	    update BS_ViewColumn set Columndesc_Tx = @Columndesc_Tx  where   View_ID =  @View_ID and Columnname_Tx = @ColumnName_TX;
   end 
else--结束
	Break;
End
Close memu_Cursor;
Deallocate memu_Cursor;


--恢复编辑界面控件名称
Declare 
    @KEY_TX varchar(100),
    @REMARK_TX varchar(100),
    @Tip_TX varchar(2000),
    @VALUE_TX varchar(100);
declare	memu_Cursor Cursor For
    Select KEY_TX,VALUE_TX,REMARK_TX,Tip_TX from BS_Resource s;
Open memu_Cursor;
While ( 1=1 )
Begin
Fetch memu_Cursor Into @KEY_TX,@VALUE_TX,@REMARK_TX,@Tip_TX;
if @@Fetch_Status  = 0  --没有错误
   begin
	      update BS_Resource set VALUE_TX = @VALUE_TX, REMARK_TX = @REMARK_TX,Tip_TX = @Tip_TX  where KEY_TX = @KEY_TX;
   end 
else--结束
	Break;
End
Close memu_Cursor;
Deallocate memu_Cursor;


--从系统配置恢复通信监控地址

begin
declare
@V_XSTComURL varchar(300),@V_IP varchar(300);
select @V_IP = Value_TX   From Bs_Appconfig where appconfig_CD = 'XSTComMM' ;
set @V_XSTComURL =  'javascript:openOcxPage(''' + @V_IP + ''',''XSTCommunication'');';
update Bs_Mainmenu set JsBlock_TX = @V_XSTComURL where menukey_tx = 'XSTComMM';
end
 
 
 

--重新设置system权限
Delete From Bs_AppRoleAction Where AppRole_ID = 1;
Delete From BS_PostAppAction Where Post_ID = 1;
Insert Into Bs_AppRoleAction(AppRole_ID,AppAction_CD,DataRange_CD,ShortCut_YN) Select 1,AppAction_CD,'AL',1 From Bs_AppAction ;
Insert Into BS_PostAppAction(Post_ID,AppAction_CD,DataRange_CD,ShortCut_YN) Select 1,AppAction_CD,'AL',1 From Bs_AppAction ;


---从81.AlterDate转移到此处 edit by gy 
begin
	declare @V_Count int;
	declare @V_MaxID  int;
	
    ---更新数据字典项ObjectKey
	Set @V_Count =0 ;
	select @V_Count = Count(*) From BS_ObjectKey where  Source_CD = 'BS_DDList';    
	If @V_Count != 0 
	Begin
		Select @V_MaxID=isnull(max(DD_ID),0) + 1 From BS_DDLIST;
		update BS_ObjectKey Set KeyValue = @V_MaxID Where Source_CD = 'BS_DDList'; 
	End;		
		
	--更新公司下数据字典
	declare  
		@V_Org_ID int,
		@V_Ddtype_Cd varchar(100),
		@V_Name_Tx varchar(100),
		@V_Parent_Cd varchar(100),
		@V_Ddtier_Yn varchar(100),
		@V_DDFlag_YN varchar(100),
		@V_Flagdes_Tx varchar(100),
		@V_Isusing_Yn varchar(100),
		@V_Issystem_Yn varchar(100),
		@V_Sortno_Nr varchar(100),
		@V_Appaction_Cd varchar(100);
		 
		Declare DataRowType_Cursor Cursor For
			Select Ddtype_Cd,Name_Tx,Parent_Cd,Ddtier_Yn,DDFlag_YN,Flagdes_Tx,Isusing_Yn,Issystem_Yn,Sortno_Nr,
			Appaction_Cd   From BS_DDType Where Org_ID = -1;
		Declare DataRow_Cursor Cursor For	
			Select Org_ID  From V_Org;
			
		Set @V_count = 0;
		Open DataRowType_Cursor;
		While (1 = 1)
		Begin
			Fetch DataRowType_Cursor into @V_Ddtype_Cd,@V_Name_Tx,@V_Parent_Cd,@V_Ddtier_Yn,
			@V_DDFlag_YN,@V_Flagdes_Tx,@V_Isusing_Yn,@V_Issystem_Yn,@V_Sortno_Nr,@V_Appaction_Cd;
			If @@Fetch_Status <> 0 
				Break
			Open DataRow_Cursor;
			While (1 = 1)
			Begin
				Fetch DataRow_Cursor into @V_Org_ID;
				If @@Fetch_Status <> 0 
					Break
				select @V_count = Count(*)  From BS_DDType 
				where DDType_CD = @V_Ddtype_Cd
				and Org_ID= @V_Org_ID;
				if @V_count = 0
				  Insert Into BS_DDType
				  (DDType_CD,Org_ID,Name_TX,Parent_CD,DDTier_YN,DDFlag_YN,FlagDes_TX,
				  IsUsing_YN,IsSystem_YN,SortNo_NR,appaction_CD)
				  Values
				  (@V_Ddtype_Cd,@V_Org_ID,@V_Name_Tx,@V_Parent_Cd,@V_Ddtier_Yn,@V_DDFlag_YN,@V_Flagdes_Tx,
				  @V_Isusing_Yn,@V_Issystem_Yn,@V_Sortno_Nr,@V_Appaction_Cd);  	
			End;
			Close DataRow_Cursor;
		End;
		Close DataRowType_Cursor;
		Deallocate DataRowType_Cursor;
		Deallocate DataRow_Cursor;
		
end
go   
    