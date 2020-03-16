using System;
using System.Collections.Generic;
using System.Linq;
using AnalysisData.Constants;
using AnalysisData.SampleData;
using Moons.Common20;
using AnalysisData.Dto;
using TrendData = AnalysisData.Dto.TrendDataDto;
using TimeWaveData_1D = AnalysisData.Dto.TimeWaveDataDto;

namespace SampleServer.Upload2DB
{
    /// <summary>
    /// �����ɼ������ϴ����ݿ���
    /// </summary>
    public class NormalSampleUploader : UploaderBase
    {
        /// <summary>
        /// �ϴ�����
        /// </summary>
        /// <param name="datas">List[TrendData]</param>
        public void UploadData( List<TrendData> datas )
        {
            if( CollectionUtils.IsNullOrEmptyG( datas ) )
            {
                return;
            }

            try
            {
                UploadDatas( datas );
            }
            catch( Exception ex )
            {
                TraceUtils.Error("UploadData error.", ex );
            }
        }

        #region �Զ��庯��

        /// <summary>
        /// �ϴ�����
        /// </summary>
        /// <param name="trendDatas">TrendData���󼯺�</param>
        private void UploadDatas( IEnumerable<TrendData> trendDatas )
        {
            //var save2DBDatas = new List<TrendData>();
            var sampleServerContext = Config.SampleServerContext;
            foreach ( TrendData trendData in trendDatas )
            {
                //// �������ȼ�IDͳһ
                trendData.AlmLevelID = Config.GetRefAlmLevelIDByAlmLevelID( trendData.AlmLevelID );

                // ����������
                if( trendData.DataUsageID == DataUsageID.Monitor )
                {
                    sampleServerContext.SendMonitorData(trendData);
                    //Config.SetPointMornitorData2Cache( trendData.PointID, trendData );
                    continue;
                }

                // ���ͬ��ID
                trendData.SyncID = Config.GetSyncIDBySyncUniqueID(trendData.SyncUniqueID);

                sampleServerContext.SendSave2DBData(trendData);

                //save2DBDatas.Add( trendData );
            }

            //if( CollectionUtils.IsNullOrEmptyG( save2DBDatas ) )
            //{
            //    return;
            //}

            //var uploadHelper = new UploadHelper
            //                       { Uploader = this, EntitiesWrap = EntitiesWrap };

            //uploadHelper.InitDatas( save2DBDatas );

            //if( uploadHelper.HasData2Save )
            //{
            //    uploadHelper.InsertDatas();

            //    EntitiesWrap.SaveChanges();
            //}

            //EntitiesWrap.Clear();
        }

        #endregion
    }

    /// <summary>
    /// �����ɼ������ϴ����ݿ���
    /// </summary>
    public class NormalAlmUploader : UploaderBase
    {
        /// <summary>
        /// �ϴ������¼�����
        /// </summary>
        /// <param name="datas">List[AlmEventData]</param>
        public void UploadAlm( List<AlmEventDataDto> datas )
        {
            if( datas.Count == 0 )
            {
                return;
            }

            try
            {
                var sampleServerContext = Config.SampleServerContext;
                foreach ( var almData in datas )
                {
                    // ���㷶���ܰѲ��IDΪ��ı����¼������������ֱ����¼���Ҫ���ˣ���������
                    if( almData.PointID == 0 )
                    {
                        continue;
                    }

                    // �������ȼ�IDͳһ
                    almData.AlmLevel = Config.GetRefAlmLevelIDByAlmLevelID( almData.AlmLevel );

                    sampleServerContext.SendAlmEvent(almData);

                    //int pointID = almData.PointID;
                    //int almSourceID = almData.AlmSourceID;
                    //var almLevel = (short)almData.AlmLevel;

                    //int mobjectID;
                    //int checkUserID;
                    //int postID;
                    //int specID;
                    //string descTX = GetAlmDescTX( pointID, out mobjectID, out postID, out checkUserID, out specID );
                    //if( descTX == null )
                    //{
                    //    continue;
                    //}
                    //descTX += "[" + almData.Description + "]";

                    //if( Config.IsWineSteelCustomMode )
                    //{
                    //    // �Ƹ�ģʽ�£���Բɼ����̼�Bug�Ĳ��ȴ�ʩ��
                    //    if( descTX.Contains( "������" ) ||
                    //        ( descTX.Contains( "�¶�" ) && descTX.Contains( "����ֵ��0��" ) ) ||
                    //        ( descTX.Contains( "����" ) && descTX.Contains( "����ֵ��0��" ) )
                    //        )
                    //    {
                    //        TraceUtils.LogDebugInfo( $"���Ա����¼���{descTX}." );
                    //        continue;
                    //    }
                    //}
                    //else if( Config.IsWEPECCustomMode )
                    //{
                    //    descTX = almData.AlmTime + descTX;
                    //}

                    //// ʹ�ô洢����д�뱨��
                    //var queryInfoBagSp = new QueryInfoBagSp { SpName = "ZXSamplePackage.InsertAlmRecord" };

                    //queryInfoBagSp.AddQueryParameter( "P_PartitionID", PartitionIDUtils.Time2LongID( almData.AlmTime ) );

                    //// ��ԴҲ��Int32����������ǿ��ת��û������
                    //queryInfoBagSp.AddQueryParameter( "P_AlmID", Convert.ToInt32( almData.AlmID ) );

                    //queryInfoBagSp.AddQueryParameter( "P_FeatureValueID", almSourceID );
                    //queryInfoBagSp.AddQueryParameter( "P_PointID", pointID );

                    //queryInfoBagSp.AddQueryParameter( "P_AlmDT", almData.AlmTime );
                    //queryInfoBagSp.AddQueryParameter( "P_AlmLevelID", almLevel );
                    //queryInfoBagSp.AddQueryParameter( "P_AlmDescTX", descTX );
                    //queryInfoBagSp.AddQueryParameter( "P_MobjectID", mobjectID );
                    //queryInfoBagSp.AddQueryParameter( "P_OwnerPostID", postID );
                    //queryInfoBagSp.AddQueryParameter( "P_MobSpecID", specID );

                    //// ���ߵ��û�ָ��Ϊϵͳ�û�
                    //queryInfoBagSp.AddQueryParameter( "P_UserID", checkUserID );

                    //Context.QueryDataSp.ExecuteNonQuery( queryInfoBagSp );

                    //if( Config.IsWineSteelCustomMode )
                    //{
                    //    try
                    //    {
                    //        // �Ƹ�ģʽ�£���Բɼ����̼�Bug�Ĳ��ȴ�ʩ��
                    //        // ����Ĳ��ȴ�ʩ��֪ʲôԭ����������Ĵ��󱨾���¼��⣬����ִ�������SQL��䡣
                    //        const string DeleteErrorRecordsSql =
                    //            "delete from ZT_MobjectWarning where (Content_TX like '%�¶�%����ֵ��0��%') or (Content_TX like '%����%����ֵ��0��%') or (Content_TX like '%������%');";
                    //        Context.ExecuteSQLCommandNonQuery( DeleteErrorRecordsSql );

                    //        const string DeleteErrorRecordsSql2 =
                    //            "delete from ZX_History_Alm where (AlmDesc_TX like '%�¶�%����ֵ��0��%') or (AlmDesc_TX like '%����%����ֵ��0��%') or (AlmDesc_TX like '%������%');";
                    //        Context.ExecuteSQLCommandNonQuery( DeleteErrorRecordsSql2 );
                    //    }
                    //    catch( Exception ex )
                    //    {
                    //        TraceUtils.Error( "IsWineSteelCustomMode,DeleteErrorRecordsSql,error.", ex );
                    //    }
                    //}
                } // foreach( AlmEventData almData in datas )
            }
            catch( Exception ex )
            {
                TraceUtils.Error("UploadAlm error.", ex );
            }
        }

        ///// <summary>
        ///// ��ȡ��������
        ///// </summary>
        ///// <param name="pointID">���ID</param>
        ///// <param name="mobjectID">������豸ID</param>
        ///// <param name="postID">�������λID</param>
        ///// <param name="checkUserID">�����ID</param>
        ///// <param name="specID">������豸רҵID</param>
        ///// <returns>��������</returns>
        //private static string GetAlmDescTX( int pointID, out int mobjectID, out int postID, out int checkUserID,
        //                                    out int specID )
        //{
        //    mobjectID = 0;
        //    postID = 0;
        //    specID = 0;
        //    checkUserID = 0;

        //    Pnt_Point point = Config.GetPnt_Point( pointID );
        //    if( point == null )
        //    {
        //        return null;
        //    }

        //    mobjectID = point.Mobject_ID;
        //    TraceUtils.LogDebugInfo( string.Format( "GetAlmDescTX mobjectID: {0}", mobjectID ) );

        //    var msg = new MessageInfo();
        //    var mobjectService = Context.Current.Resolve<IMObjectService>();


        //    Mob_MObject mobject = mobjectService.GetMObjectByID( mobjectID, ref msg );
        //    if( msg.IsError )
        //    {
        //        TraceUtils.Error( string.Format( "GetAlmDescTX��ȡ�豸��Ϣ({0})����({1})��", mobjectID, msg.ErrorMessage ) );
        //        return null;
        //    }
            
        //    if ( mobject == null )
        //    {
        //        TraceUtils.Error( string.Format( "GetAlmDescTX,�豸({0})������", mobjectID ) );
        //        return null;
        //    }

        //    var mobjectName = mobject.MobjectName_TX;
        //    if ( Config.IsWEPECCustomMode )
        //    {
        //        var mObjectStructureService = Context.Current.Resolve<IMObjectStructureService>();
        //        var pathIds = mObjectStructureService.GetMObjectPathIDs( mobjectID, ref msg);

        //        List< Mob_MObject> mObjectsInPath = new List<Mob_MObject>();
        //        // 0 ��OrgId
        //        for ( int index = 1; index < pathIds.Length; index++ )
        //        {
        //            Mob_MObject mobjectInPath = mobjectService.GetMObjectByID(pathIds[index], ref msg);

        //            var cd = mobjectInPath.Mobject_CD ?? string.Empty;
        //            if( cd.StartsWith( Config.WEPEC_Prefix_FirstParent ) )
        //            {
        //                // �ҵ��˿�ʼ���豸�ڵ㣬ɾ����ʼ�ڵ�֮ǰ�Ľڵ㡣
        //                mObjectsInPath.Clear();
        //            }

        //            mObjectsInPath.Add( mobjectInPath );

        //            if( cd.StartsWith( Config.WEPEC_Prefix_Machine ) )
        //            {
        //                // �ҵ��˹ؼ����豸�ڵ㡣
        //                mobject = mobjectInPath;
        //            }
        //        }

        //        mobjectName = StringUtils.Join( mObjectsInPath.Select( m => m.MobjectName_TX ), StringUtils.Slash );
        //    } // if( Config.IsWEPECCustomMode )

        //    // ���רҵID
        //    if ( mobject.Spec_ID.HasValue )
        //    {
        //        specID = mobject.Spec_ID.Value;
        //    }

        //    // ��췽�ĸ�λID
        //    int? djOwnerID = mobject.DJOwner_ID;
        //    TraceUtils.LogDebugInfo( string.Format( "GetAlmDescTX mobject.DJOwner_ID: {0}", mobject.DJOwner_ID ) );
        //    if( djOwnerID.HasValue )
        //    {
        //        postID = djOwnerID.Value;
        //        var postService = Context.Current.Resolve<IPostService>();
        //        BS_AppuserPost userPost = postService.GetAppUserPostByID( postID, DBConst.ABRole_A, ref msg );
        //        if( userPost != null )
        //        {
        //            //departmentID = post.Dept_ID;
        //            checkUserID = userPost.AppUser_ID;
        //        }
        //    }

        //    if( Config.IsWEPECCustomMode )
        //    {
        //        var appUserService = Context.Current.Resolve<IAppUserService>();
        //        var checkUser = appUserService.GetAppUserByID( checkUserID, ref msg );
        //        if( checkUser != null )
        //        {
        //            mobjectName = string.Format(" {0}/{1}, {2}", checkUser.Name_TX, checkUser.Account_TX, mobjectName);
        //        }
        //    } // if( Config.IsWEPECCustomMode )

        //    string pointString = string.Format( "{0}/{1}", mobjectName, point.PointName_TX );

        //    var pntChannelService = Context.Current.Resolve<IPntChannelService>();
        //    StationChannel channel = pntChannelService.GetPntChnlDetailByPointIDAndChnNo( point.Point_ID, 1, ref msg );
        //    if( msg.IsError )
        //    {
        //        TraceUtils.Error( string.Format( "GetAlmDescTX��ȡ���ͨ����Ϣ({0})����({1})��", point.Point_ID, msg.ErrorMessage ) );
        //        return null;
        //    }
        //    if( channel != null )
        //    {
        //        channel.XmlToParams();
        //        pointString += string.Format( "-{0}-{1}", channel.StationName_TX, channel.ChannelNumber_NR );
        //        string channelIdentifier = channel.ChannelIdentifier_TX;
        //        if( !string.IsNullOrWhiteSpace( channelIdentifier ) &&
        //            channelIdentifier != "000000000000000000000000" )
        //        {
        //            pointString +=
        //                string.Format( "({0})", int.Parse( channelIdentifier, NumberStyles.HexNumber ).ToString() );
        //        }
        //    }

        //    return pointString;
        //}
    }
}