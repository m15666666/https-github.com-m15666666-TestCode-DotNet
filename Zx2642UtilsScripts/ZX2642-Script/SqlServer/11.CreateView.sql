/*==============================================================*/
/* Name: @V_SCZ_GZZGL                                                                                                            */
/* Author: 顾允聪    DateTime：2010-8-12                                                                      */
/* Function: 采集工作站列表                                                                                                   */
/*==============================================================*/
if exists (select * from sysobjects where TYPE = 'V' and Name = 'V_SCZ_GZZGL')
  drop view V_SCZ_GZZGL;
  go
create view V_SCZ_GZZGL as
select 
   s.station_id as station_id,
   s.org_id as org_id,
   o.orgname_tx as orgname_tx,
   s.name_tx as stationname_tx,
   s.stationsn_tx as stationsn_tx,
   s.stationtype_tx as stationtype_tx,
   s.ip_tx as ip_tx,
   '' as queryinterval_tx,
   '' as waveinterval_tx,
   '' as staticinterval_tx,
   s.stationparam_tx as stationparam_tx 
from sample_station s 
    left join bs_org o on s.org_id = o.org_id ;
GO

/*==============================================================*/
/* Name: @V_SCZ_FWQGL                                                                                                            */
/* Author: 顾允聪    DateTime：2010-8-25                                                                      */
/* Function: 采集服务器列表                                                                                                   */
/*==============================================================*/
if exists (select * from sysobjects where TYPE = 'V' and Name = 'V_SCZ_FWQGL')
  drop view V_SCZ_FWQGL;
  go
create view V_SCZ_FWQGL as
select 
   svr.server_id as server_id,
   svr.name_tx as servername_tx,
   svr.url_tx as serverurl_tx,
   svr.ip_tx as serverip_tx 
from sample_server svr ;
GO

/*==============================================================*/
/* Name: @V_SCZ_CJQGL                                                                                                            */
/* Author: 顾允聪    DateTime：2010-8-25                                                                      */
/* Function: 数据采集器列表                                                                                                   */
/*==============================================================*/
if exists (select * from sysobjects where TYPE = 'V' and Name = 'V_SCZ_CJQGL')
  drop view V_SCZ_CJQGL;
  go
create view V_SCZ_CJQGL as
select 
   dau.serverdau_id as dau_id,
   dau.name_tx as dauname_tx,
   dau.url_tx as dauurl_tx,
   dau.ip_tx as dauip_tx,
   dau.server_id as server_id,
   svr.name_tx as servername_tx,
   svr.url_tx as serverurl_tx,
   svr.ip_tx as serverip_tx 
from sample_serverdau dau 
    left join sample_server svr on dau.server_id = svr.server_id ;
GO

/*==============================================================*/
/* Name: @V_SCZ_CJQGZZGL                                                                                                            */
/* Author: 顾允聪    DateTime：2010-8-25                                                                      */
/* Function: 数据采集器工作站列表                                                                                                   */
/*==============================================================*/
if exists (select * from sysobjects where TYPE = 'V' and Name = 'V_SCZ_CJQGZZGL')
  drop view V_SCZ_CJQGZZGL;
  go
create view V_SCZ_CJQGZZGL as
select 
   ds.station_id as station_id,
   ds.serverdau_id as dau_id,
   dau.name_tx as dauname_tx,
   dau.url_tx as dauurl_tx,
   dau.server_id as server_id,
   svr.name_tx as servername_tx,
   svr.url_tx as serverurl_tx,
   s.org_id as org_id,
   o.orgname_tx as orgname_tx,
   s.name_tx as stationname_tx,
   s.stationsn_tx as stationsn_tx,
   s.stationtype_tx as stationtype_tx,
   s.ip_tx as ip_tx,
   '' as queryinterval_tx,
   '' as waveinterval_tx,
   '' as staticinterval_tx,
   s.stationparam_tx as stationparam_tx 
from sample_daustation ds 
  left join sample_serverdau dau 
    left join sample_server svr on dau.server_id = svr.server_id 
  on ds.serverdau_id = dau.serverdau_id 
  left join sample_station s 
    left join bs_org o on s.org_id = o.org_id 
  on ds.station_id = s.station_id ;
GO

/*==============================================================*/
/* Name: @V_SCZ_GZZXZ                                                                                                            */
/* Author: 顾允聪    DateTime：2010-8-30                                                                      */
/* Function: 数据采集器选择采集工作站列表                                                                                                   */
/*==============================================================*/
if exists (select * from sysobjects where TYPE = 'V' and Name = 'v_scz_gzzxz')
  drop view v_scz_gzzxz;
  go
create view v_scz_gzzxz as
select
   s.station_id as station_id,
   s.org_id as org_id,
   o.orgname_tx as orgname_tx,
   s.name_tx as stationname_tx,
   s.stationsn_tx as stationsn_tx,
   s.stationtype_tx as stationtype_tx,
   s.ip_tx as ip_tx,
   '' as queryinterval_tx,
   '' as waveinterval_tx,
   '' as staticinterval_tx,
   s.stationparam_tx as stationparam_tx,
   '' as dau_id,
   '' as dauname_tx 
from sample_station s
    left join bs_org o on s.org_id = o.org_id;
GO


 /*==============================================================*/
/* Name: @V_AnalysisBarGroup                                                                                  */
/* Author: 熊立  DateTime：2010-8-10                                                                            */
/* Function:                                                                                        */
/*==============================================================*/     
if exists (select * from sysobjects where TYPE = 'V' and Name = 'V_AnalysisBarGroup')
  drop view V_AnalysisBarGroup;
  go
create view V_AnalysisBarGroup as
select * from Analysis_BarGroup ;
GO
    
/*==============================================================*/
/* Name: @V_jcxx_zxmobject                         */
/* Author: 董建林    DateTime：2010-8-16            */
/* Function: （在线）设备视图                   */
/*==============================================================*/
if exists (select * from sysobjects where TYPE = 'V' and Name = 'v_jcxx_zxmobject')
  drop view v_jcxx_zxmobject;
  go
create view v_jcxx_zxmobject as
select m.mobject_id,
       m.org_id,
       m.mobject_cd,
       m.mobjectname_tx,       
       m.spec_id,
       m.ggxh_tx,
       m.repairtype_id,
       m.property_id,
       m.djowner_id,
       m.jxdept_id,
       m.sbms_tx,
       m.jscs_tx,
       m.zjl_nr,
       m.zjldw_tx,
       m.status_id,
       m.zzcj_tx,
       DBDiffPackage.ToDateString(m.tyrq_dt,'yyyy-MM-dd') as tyrq_dt,
       m.adduser_tx,
       DBDiffPackage.ToDateString(m.add_dt,'yyyy-MM-dd') as add_dt,
       m.active_yn,
       m.sortno_nr,
       m.area_id,       
       d.name_tx as spec_tx, 
       s.name_tx as repairtype_tx,
       p.AUserName_TX as djowner_tx,       
       o.deptname_tx as jxdept_tx,
       DBDiffPackage.IntToString(m.djowner_id) + '||||' + p.POSTNAME_TX as djowner_idtx,
       DBDiffPackage.IntToString(m.jxdept_id) + '||||' + o.DEPTNAME_TX as jxdept_idtx,
       b.name_tx as area_tx,
       a.name_tx as status_tx,       
       r.parent_id,
       r.parentlist_tx,
       j.mobject_cd as parentmobject_cd,
       j.mobjectname_tx as parentmobjectname_tx,
       e.name_tx as propertyname_tx
  from mob_mobject m 
  left join bs_ddlist d on m.spec_id = d.dd_id
  left join bs_ddlist s on m.repairtype_id = s.dd_id
  left join v_post p on p.post_id = m.djowner_id
  left join v_dept o on o.dept_id = m.jxdept_id
  left join bs_ddlist b on b.dd_id = m.area_id
  left join bs_ddlist a on a.dd_id = m.status_id
  left join mob_mobjectstructure r on r.mobject_id = m.mobject_id
  left join mob_mobject j on j.mobject_id = r.parent_id
  left join bs_ddlist e on m.property_id = e.dd_id
  where m.active_yn = '1';
GO



/*==============================================================*/
/* Name: @v_jcxx_zxmobject                         */
/* Author:*/
/* Function: （在线）测点位置视图                  */
/*==============================================================*/
if exists (select * from sysobjects where TYPE = 'V' and Name = 'v_pntposition')
  drop view v_pntposition;
  go
create view v_pntposition as  
select
       pp.mobject_id,
       p.point_id,
       p.featurevalue_id,
       p.sigtype_id,
       p.pointname_tx,
       u.namee_tx as engunitname_tx,
       pp.posx_nr,
       pp.posy_nr,
       pp.tagx_nr,
       pp.tagy_nr
    from analysis_pntposition pp inner join pnt_point p
on pp.point_id = p.point_id left join z_engunit u on p.engunit_id = u.engunit_id;
GO

/*==============================================================*/
/* Name: @V_MObjPosition                                                                                                        */
/* Author: 董建林    DateTime：2010-8-23                                                                      */
/* Function: 设备位置列表                                                                                                   */
/*==============================================================*/
if exists (select * from sysobjects where TYPE = 'V' and Name = 'V_MObjPosition')
  drop view V_MObjPosition;
  go
create view V_MObjPosition as
select 
       m.MOBJECTNAME_TX,
       m.MOBJECT_ID,
       ms.parent_id,
       m.ORG_ID,
       mp.posx_nr,
       mp.posy_nr,
       mp.tagx_nr,
       mp.tagy_nr
    from analysis_mobjposition mp inner join v_mobject m
on mp.mobject_id = m.mobject_id left join mob_mobjectstructure ms on m.MOBJECT_ID = ms.mobject_id;
GO


/*==============================================================*/
/* Name: @V_JCFX_ZCK                                                                                                       */
/* Author: 董建林   DateTime：2010-10-19                            */
/* Function: 轴承库视图                                         */
/*==============================================================*/
if exists (select * from sysobjects where TYPE = 'V' and Name = 'v_jcfx_zck')
  drop view v_jcfx_zck;
  go
create view v_jcfx_zck as
Select b.bearing_id,
       b.facotry_tx,
       b.model_tx,
       b.rollercount_nr,
       DBDiffPackage.ToFloatString1(b.bsf_nr) as bsf_nr,
       DBDiffPackage.ToFloatString1(b.ftf_nr) as ftf_nr,
       DBDiffPackage.ToFloatString1(b.bbpfo_nr) as bbpfo_nr,
       DBDiffPackage.ToFloatString1(b.bpfi_nr) as bpfi_nr,
       DBDiffPackage.StringDecode(b.isinlay_yn,'1','是','0','否') as isinlay_yn,
       b.description_tx
from Zx_Bearing b;
GO


/*==============================================================*/
/* Name: @V_JCCS_TZPLFZ                                                                                                      */
/* Author: 董建林   DateTime：2010-10-28                            */
/* Function: 特征频率分组视图                                         */
/*==============================================================*/
if exists (select * from sysobjects where TYPE = 'V' and Name = 'V_JCCS_TZPLFZ')
  drop view V_JCCS_TZPLFZ;
  go
create view V_JCCS_TZPLFZ as
Select g.group_id,
       g.groupname_tx,
       g.org_id
from Analysis_FeatureFreqGroup g;
GO

/*==============================================================*/
/* Name: @V_JCCS_GLTZPL                                                                                                      */
/* Author: 董建林   DateTime：2010-10-28                            */
/* Function: 特征频率视图                                         */
/*==============================================================*/
if exists (select * from sysobjects where TYPE = 'V' and Name = 'V_JCCS_GLTZPL')
  drop view V_JCCS_GLTZPL;
  go
create view V_JCCS_GLTZPL as
Select g.group_id,
       g.groupname_tx,
       mf.mobject_id
from Mob_Featrefreqgroup mf left join Analysis_Featurefreqgroup g on mf.group_id = g.group_id ;
GO

/*==============================================================*/
/* Name: @V_JCCS_TZPL                                                                                                      */
/* Author: 董建林   DateTime：2010-10-28                            */
/* Function: 关联特征频率分组视图                               */
/*==============================================================*/
if exists (select * from sysobjects where TYPE = 'V' and Name = 'V_JCCS_TZPL')
  drop view V_JCCS_TZPL;
  go
create view V_JCCS_TZPL as
Select g.group_id,
       f.name_tx,
       f.featurefreq_id,
       f.featurefreqvalue_nr,
       DBDiffPackage.StringDecode(f.unit_nr,'1','X','0','HZ') as unit_nr
from Analysis_Featurefreq f left join Analysis_Featurefreqgroup g on f.group_id = g.group_id ;
GO

/*==============================================================*/
/* Name: @V_ZXAlm                                                                                       */
/* Author: 董建林   Date：2011-05-16                           */
/* Function: 在线报警视图                               */
/*==============================================================*/
if exists (select * from sysobjects where TYPE = 'V' and Name = 'V_ZXAlm')
  drop view V_ZXAlm;
  go
Create View V_ZXAlm
As
Select a.partition_id,
       a.alm_id,
       a.featurevalue_id,
       a.alm_dt,
       a.mobject_id,
       a.point_id,
       a.almlevel_id,
       a.almdesc_tx
       from zt_mobjectwarning w inner join zx_history_alm a on w.mobjectwarning_id = a.alm_id where w.close_yn = '0';
Go

/*==============================================================*/
/* Name: @V_PointChannelStation                                                                                       */
/* Author: 董建林   Date：2012-12-29                           */
/* Function: 测点通道和数采站关联视图                          */
/*==============================================================*/
if exists (select * from sysobjects where TYPE = 'V' and Name = 'V_PointChannelStation')
  drop view V_PointChannelStation;
  go
Create View V_PointChannelStation
As
SELECT  p.Point_ID, 
		p.StationChannel_ID,
		p.ChnNo_NR,
		c.Station_ID
		FROM Sample_PntChannel p left join Sample_StationChannel c ON p.StationChannel_ID = c.StationChannel_ID;
		
GO

/*==============================================================*/
/* Name: @V_JCCS_SJYBLGL                                                                                                            */
/* Author: 顾允聪    DateTime：2013-6-13                                                                      */
/* Function: 数据源变量管理列表                                                                                                   */
/*==============================================================*/
if exists (select * from sysobjects where TYPE = 'V' and Name = 'V_JCCS_SJYBLGL')
  drop view V_JCCS_SJYBLGL;
  go
create view V_JCCS_SJYBLGL as
select 
   dv.var_id as var_id,
   dv.varname_tx as varname_tx,
   dv.vardesc_tx as vardesc_tx,
   dv.varscale_nr as varscale_nr,
   dv.varoffset_nr as varoffset_nr 
from pnt_datavar dv;
GO