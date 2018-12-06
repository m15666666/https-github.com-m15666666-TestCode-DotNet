

/*==============================================================*/
/* Name: V_SCZ_GZZGL                                                                                                            */
/* Author: ���ʴ�    Date��2010-8-12                                                                      */
/* Function: �ɼ�����վ�б�                                                                                                   */
/*==============================================================*/
create or replace view V_SCZ_GZZGL as
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

/*==============================================================*/
/* Name: V_SCZ_FWQGL                                                                                                            */
/* Author: ���ʴ�    Date��2010-8-25                                                                      */
/* Function: �ɼ��������б�                                                                                                   */
/*==============================================================*/
create or replace view V_SCZ_FWQGL as
select 
   svr.server_id as server_id,
   svr.name_tx as servername_tx,
   svr.url_tx as serverurl_tx,
   svr.ip_tx as serverip_tx 
from sample_server svr ;

/*==============================================================*/
/* Name: V_SCZ_CJQGL                                                                                                            */
/* Author: ���ʴ�    Date��2010-8-25                                                                      */
/* Function: ���ݲɼ����б�                                                                                                   */
/*==============================================================*/
create or replace view V_SCZ_CJQGL as
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

/*==============================================================*/
/* Name: V_SCZ_CJQGZZGL                                                                                                            */
/* Author: ���ʴ�    Date��2010-8-25                                                                      */
/* Function: ���ݲɼ�������վ�б�                                                                                                   */
/*==============================================================*/
create or replace view V_SCZ_CJQGZZGL as
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

/*==============================================================*/
/* Name: V_SCZ_GZZXZ                                                                                                            */
/* Author: ���ʴ�    Date��2010-8-30                                                                      */
/* Function: ���ݲɼ���ѡ��ɼ�����վ�б�                                                                                                   */
/*==============================================================*/
create or replace view v_scz_gzzxz as
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

/*==============================================================*/
/* Name: V_AnalysisBarGroup                                                                                  */
/* Author: ����  Date��2010-8-10                                                                            */
/* Function:                                                                                        */
/*==============================================================*/     
create or replace view V_AnalysisBarGroup as
select * from Analysis_BarGroup ;
    
/*==============================================================*/
/* Name: v_jcxx_zxmobject                         */
/* Author: ������    Date��2010-8-16            */
/* Function: �����ߣ��豸��ͼ                   */
/*==============================================================*/
create or replace view v_jcxx_zxmobject as
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
       DBDiffPackage.IntToString(m.djowner_id) || '||||' || p.POSTNAME_TX as djowner_idtx,
       DBDiffPackage.IntToString(m.jxdept_id) || '||||' || o.DEPTNAME_TX as jxdept_idtx,
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



create or replace view v_pntposition as
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

/*==============================================================*/
/* Name: V_MObjPosition                                                                                                        */
/* Author: ������    Date��2010-8-23                                                                      */
/* Function: �豸λ���б�                                                                                                   */
/*==============================================================*/
create or replace view V_MObjPosition as
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


/*==============================================================*/
/* Name: V_JCFX_ZCK                                                                                                       */
/* Author: ������   Date��2010-10-19                            */
/* Function: ��п���ͼ                                         */
/*==============================================================*/
create or replace view v_jcfx_zck as
Select b.bearing_id,
       b.facotry_tx,
       b.model_tx,
       b.rollercount_nr,
       DBDiffPackage.ToFloatString1(b.bsf_nr) as bsf_nr,
       DBDiffPackage.ToFloatString1(b.ftf_nr) as ftf_nr,
       DBDiffPackage.ToFloatString1(b.bbpfo_nr) as bbpfo_nr,
       DBDiffPackage.ToFloatString1(b.bpfi_nr) as bpfi_nr,
       DBDiffPackage.StringDecode(b.isinlay_yn,'1','��','0','��') as isinlay_yn,
       b.description_tx
from Zx_Bearing b;


/*==============================================================*/
/* Name: V_JCCS_TZPLFZ                                                                                                      */
/* Author: ������   Date��2010-10-28                            */
/* Function: ����Ƶ�ʷ�����ͼ                                         */
/*==============================================================*/
create or replace view V_JCCS_TZPLFZ as
Select g.group_id,
       g.groupname_tx,
       g.org_id
from Analysis_FeatureFreqGroup g;

/*==============================================================*/
/* Name: V_JCCS_GLTZPL                                                                                                      */
/* Author: ������   Date��2010-10-28                            */
/* Function: ����Ƶ����ͼ                                         */
/*==============================================================*/
create or replace view V_JCCS_GLTZPL as
Select g.group_id,
       g.groupname_tx,
       mf.mobject_id
from Mob_Featrefreqgroup mf left join Analysis_Featurefreqgroup g on mf.group_id = g.group_id ;

/*==============================================================*/
/* Name: V_JCCS_TZPL                                                                                                      */
/* Author: ������   Date��2010-10-28                            */
/* Function: ��������Ƶ�ʷ�����ͼ                               */
/*==============================================================*/
create or replace view V_JCCS_TZPL as
Select g.group_id,
       f.name_tx,
       f.featurefreq_id,
       f.featurefreqvalue_nr,
       DBDiffPackage.StringDecode(f.unit_nr,'1','X','0','HZ') as unit_nr
from Analysis_Featurefreq f left join Analysis_Featurefreqgroup g on f.group_id = g.group_id ;

/*==============================================================*/
/* Name: V_ZXAlm                                                                                       */
/* Author: ������   Date��2011-05-12                            */
/* Function: ���߱�����ͼ                               */
/*==============================================================*/
Create Or Replace View V_ZXAlm
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
	   
/*==============================================================*/
/* Name: @V_PointChannelStation                                 */                                                     
/* Author: ������   Date��2012-12-29                           */
/* Function: ���ͨ��������վ������ͼ                          */
/*==============================================================*/
Create Or Replace View V_PointChannelStation
As
SELECT  p.Point_ID, 
		p.StationChannel_ID,
		p.ChnNo_NR,
		c.Station_ID
		FROM Sample_PntChannel p left join Sample_StationChannel c ON p.StationChannel_ID = c.StationChannel_ID;

/*==============================================================*/
/* Name: V_JCCS_SJYBLGL                                                                                                            */
/* Author: ���ʴ�    Date��2013-6-13                                                                      */
/* Function: ����Դ���������б�                                                                                                   */
/*==============================================================*/
create or replace view V_JCCS_SJYBLGL as
select 
   dv.var_id as var_id,
   dv.varname_tx as varname_tx,
   dv.vardesc_tx as vardesc_tx,
   dv.varscale_nr as varscale_nr,
   dv.varoffset_nr as varoffset_nr 
from pnt_datavar dv;