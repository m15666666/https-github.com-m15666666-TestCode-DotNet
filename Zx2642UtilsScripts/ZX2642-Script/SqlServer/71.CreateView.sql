/*==============================================================*/
/* Name: @V_JCCS_CDGL                                                                                                            */
/* Author: 顾允聪    DateTime：2010-7-22                                                                      */
/* Function: 测点列表                                                                                                   */
/*==============================================================*/
if exists (select * from sysobjects where TYPE = 'V' and Name = 'V_JCCS_CDGL')
  drop view V_JCCS_CDGL;
  go
create view V_JCCS_CDGL as
select 
   pspc.*,
   ZXSamplePackage.F_GetPntDim(pspc.pntdim_nr) As pntdim_tx,
   ZXSamplePackage.F_GetPntDirect(pspc.pntdirect_nr) As pntdirect_tx,
   ZXSamplePackage.F_GetPntRotation(pspc.rotation_nr) As rotation_tx,
   ZXSamplePackage.F_GetStoreType(pspc.storetype_nr) As storetype_tx,
   ZXSamplePackage.F_GetChannelType(pspc.channeltype_nr) As channeltype_tx,
   m.mobjectname_tx as mobjectname_tx,
   dt.onlinename_tx as datatype_tx,
   st.name_tx as signaltype_tx,
   eu.namec_tx as engunit_tx,
   fv.name_tx as featurevalue_tx,
   s.name_tx as stationname_tx,
   '' as varname_tx 
from (select 
         p.point_id as point_id,
         p.mobject_id as mobject_id,
         p.pointname_tx as pointname_tx,
         p.desc_tx as desc_tx,
         p.dattype_id as dattype_id,
         p.sigtype_id as sigtype_id,
         p.pntdim_nr as pntdim_nr,
         p.pntdirect_nr as pntdirect_nr,
         p.rotation_nr as rotation_nr,
         p.engunit_id as engunit_id,
         p.featurevalue_id as featurevalue_id,
         p.storetype_nr as storetype_nr,
         p.sortno_nr as sortno_nr,
         pc1.station1_id as station_id,
         pc1.channeltype1_nr as channeltype_nr,
         pc1.stationchannel1_id as stationchannel1_id,
         pc1.channelnumber1_nr as channelnumber1_nr,
         pc2.stationchannel2_id as stationchannel2_id,
         pc2.channelnumber2_nr as channelnumber2_nr
      from pnt_point p 
           left join 
                (select 
                    ssc1.station_id as station1_id,
                    ssc1.channeltype_nr as channeltype1_nr,
                    spc1.stationchannel_id as stationchannel1_id,
                    ssc1.channelnumber_nr as channelnumber1_nr,
                    spc1.point_id as point1_id,
                    spc1.chnno_nr as chnno1_nr 
                 from sample_pntchannel spc1 left join sample_stationchannel ssc1 on spc1.stationchannel_id = ssc1.stationchannel_id 
                 where spc1.chnno_nr = 1) pc1 on p.point_id = pc1.point1_id
           left join 
                (select 
                    ssc2.station_id as station2_id,
                    ssc2.channeltype_nr as channeltype2_nr,
                    spc2.stationchannel_id as stationchannel2_id,
                    ssc2.channelnumber_nr as channelnumber2_nr,
                    spc2.point_id as point2_id,
                    spc2.chnno_nr as chnno2_nr 
                 from sample_pntchannel spc2 left join sample_stationchannel ssc2 on spc2.stationchannel_id = ssc2.stationchannel_id 
                 where spc2.chnno_nr = 2) pc2 on p.point_id = pc2.point2_id
           ) pspc 
    left join mob_mobject m on pspc.mobject_id = m.mobject_id 
  left join z_datatype dt on pspc.dattype_id = dt.datatype_id 
  left join z_signtype st on pspc.sigtype_id = st.signtype_id 
  left join z_engunit eu on pspc.engunit_id = eu.engunit_id 
  left join z_featurevalue fv on pspc.featurevalue_id = fv.featurevalue_id 
  left join sample_station s on pspc.station_id = s.station_id ;
GO

/*==============================================================*/
/* Name: @V_JCCS_CDFZGL                                                                                                            */
/* Author: 顾允聪    DateTime：2012-5-21                                                                      */
/* Function: 测点分组列表                                                                                                   */
/*==============================================================*/
if exists (select * from sysobjects where TYPE = 'V' and Name = 'V_JCCS_CDFZGL')
  drop view V_JCCS_CDFZGL;
  go
create view V_JCCS_CDFZGL as
select 
	DBDiffPackage.IntToString(l.PointGroup_ID) + ',' + DBDiffPackage.IntToString(r.PointGroup_ID) as Group_ID, 
	l.Mobject_ID as Mobject_ID, 
	lp.stationname_tx as StationName_TX, 
	l.GroupNo_NR as GroupNo_NR, 
	l.PointGroup_ID as LeftPointGroup_ID, 
	l.Point_ID as LeftPoint_ID, 
	lp.pointname_tx as LeftPointName_TX, 
	lp.channelnumber1_nr as LeftChannelNumber_NR, 
	l.Level_NR as LeftLevel_NR, 
	l.TopClearance_NR as LeftTopClearance_NR, 
	r.PointGroup_ID as RightPointGroup_ID, 
	r.Point_ID as RightPoint_ID, 
	rp.pointname_tx as RightPointName_TX, 
	rp.channelnumber1_nr as RightChannelNumber_NR, 
	r.Level_NR as RightLevel_NR, 
	r.TopClearance_NR as RightTopClearance_NR 
from 
	( Pnt_PointGroup l left join V_JCCS_CDGL lp on l.Point_ID = lp.point_id ) 
inner join 
	( Pnt_PointGroup r left join V_JCCS_CDGL rp on r.Point_ID = rp.point_id ) 
on l.Mobject_ID = r.Mobject_ID and l.GroupNo_NR = r.GroupNo_NR 
where l.Point_ID <> r.Point_ID and l.Level_NR < r.Level_NR;
GO
