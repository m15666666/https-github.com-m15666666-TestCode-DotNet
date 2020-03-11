using System;
using System.Collections.Generic;
using System.Linq;
using AnalysisData.Constants;
using AnalysisData.SampleData;
using Moons.Common20;
using Moons.DataSample.Shared.Dto;
using TrendData = Moons.DataSample.Shared.Dto.TrendDataDto;
using TimeWaveData_1D = Moons.DataSample.Shared.Dto.TimeWaveDataDto;

namespace SampleServer.Upload2DB
{
    /// <summary>
    /// 正常采集数据上传数据库类
    /// </summary>
    public class NormalSampleUploader : UploaderBase
    {
        /// <summary>
        /// 上传数据
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

        #region 自定义函数

        /// <summary>
        /// 上传数据
        /// </summary>
        /// <param name="trendDatas">TrendData对象集合</param>
        private void UploadDatas( IEnumerable<TrendData> trendDatas )
        {
            //var save2DBDatas = new List<TrendData>();
            var sampleServerContext = Config.SampleServerContext;
            foreach ( TrendData trendData in trendDatas )
            {
                //// 将报警等级ID统一
                trendData.AlmLevelID = Config.GetRefAlmLevelIDByAlmLevelID( trendData.AlmLevelID );

                // 缓存监测数据
                if( trendData.DataUsageID == DataUsageID.Monitor )
                {
                    sampleServerContext.SendMonitorData(trendData);
                    //Config.SetPointMornitorData2Cache( trendData.PointID, trendData );
                    continue;
                }

                // 获得同步ID
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
    /// 正常采集报警上传数据库类
    /// </summary>
    public class NormalAlmUploader : UploaderBase
    {
        /// <summary>
        /// 上传报警事件对象
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
                    // 纪秀范可能把测点ID为零的报警事件传上来，这种报警事件需要过滤，抛弃掉。
                    if( almData.PointID == 0 )
                    {
                        continue;
                    }

                    // 将报警等级ID统一
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
                    //    // 酒钢模式下，针对采集器固件Bug的补救措施。
                    //    if( descTX.Contains( "非数字" ) ||
                    //        ( descTX.Contains( "温度" ) && descTX.Contains( "报警值：0。" ) ) ||
                    //        ( descTX.Contains( "电流" ) && descTX.Contains( "报警值：0。" ) )
                    //        )
                    //    {
                    //        TraceUtils.LogDebugInfo( $"忽略报警事件：{descTX}." );
                    //        continue;
                    //    }
                    //}
                    //else if( Config.IsWEPECCustomMode )
                    //{
                    //    descTX = almData.AlmTime + descTX;
                    //}

                    //// 使用存储过程写入报警
                    //var queryInfoBagSp = new QueryInfoBagSp { SpName = "ZXSamplePackage.InsertAlmRecord" };

                    //queryInfoBagSp.AddQueryParameter( "P_PartitionID", PartitionIDUtils.Time2LongID( almData.AlmTime ) );

                    //// 来源也是Int32，所以这里强制转换没有问题
                    //queryInfoBagSp.AddQueryParameter( "P_AlmID", Convert.ToInt32( almData.AlmID ) );

                    //queryInfoBagSp.AddQueryParameter( "P_FeatureValueID", almSourceID );
                    //queryInfoBagSp.AddQueryParameter( "P_PointID", pointID );

                    //queryInfoBagSp.AddQueryParameter( "P_AlmDT", almData.AlmTime );
                    //queryInfoBagSp.AddQueryParameter( "P_AlmLevelID", almLevel );
                    //queryInfoBagSp.AddQueryParameter( "P_AlmDescTX", descTX );
                    //queryInfoBagSp.AddQueryParameter( "P_MobjectID", mobjectID );
                    //queryInfoBagSp.AddQueryParameter( "P_OwnerPostID", postID );
                    //queryInfoBagSp.AddQueryParameter( "P_MobSpecID", specID );

                    //// 在线的用户指定为系统用户
                    //queryInfoBagSp.AddQueryParameter( "P_UserID", checkUserID );

                    //Context.QueryDataSp.ExecuteNonQuery( queryInfoBagSp );

                    //if( Config.IsWineSteelCustomMode )
                    //{
                    //    try
                    //    {
                    //        // 酒钢模式下，针对采集器固件Bug的补救措施。
                    //        // 上面的补救措施不知什么原因会有少量的错误报警记录入库，所以执行下面的SQL语句。
                    //        const string DeleteErrorRecordsSql =
                    //            "delete from ZT_MobjectWarning where (Content_TX like '%温度%报警值：0。%') or (Content_TX like '%电流%报警值：0。%') or (Content_TX like '%非数字%');";
                    //        Context.ExecuteSQLCommandNonQuery( DeleteErrorRecordsSql );

                    //        const string DeleteErrorRecordsSql2 =
                    //            "delete from ZX_History_Alm where (AlmDesc_TX like '%温度%报警值：0。%') or (AlmDesc_TX like '%电流%报警值：0。%') or (AlmDesc_TX like '%非数字%');";
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
        ///// 获取报警描述
        ///// </summary>
        ///// <param name="pointID">测点ID</param>
        ///// <param name="mobjectID">输出，设备ID</param>
        ///// <param name="postID">输出，岗位ID</param>
        ///// <param name="checkUserID">检查人ID</param>
        ///// <param name="specID">输出，设备专业ID</param>
        ///// <returns>报警描述</returns>
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
        //        TraceUtils.Error( string.Format( "GetAlmDescTX获取设备信息({0})出错({1})！", mobjectID, msg.ErrorMessage ) );
        //        return null;
        //    }
            
        //    if ( mobject == null )
        //    {
        //        TraceUtils.Error( string.Format( "GetAlmDescTX,设备({0})不存在", mobjectID ) );
        //        return null;
        //    }

        //    var mobjectName = mobject.MobjectName_TX;
        //    if ( Config.IsWEPECCustomMode )
        //    {
        //        var mObjectStructureService = Context.Current.Resolve<IMObjectStructureService>();
        //        var pathIds = mObjectStructureService.GetMObjectPathIDs( mobjectID, ref msg);

        //        List< Mob_MObject> mObjectsInPath = new List<Mob_MObject>();
        //        // 0 是OrgId
        //        for ( int index = 1; index < pathIds.Length; index++ )
        //        {
        //            Mob_MObject mobjectInPath = mobjectService.GetMObjectByID(pathIds[index], ref msg);

        //            var cd = mobjectInPath.Mobject_CD ?? string.Empty;
        //            if( cd.StartsWith( Config.WEPEC_Prefix_FirstParent ) )
        //            {
        //                // 找到了开始的设备节点，删除开始节点之前的节点。
        //                mObjectsInPath.Clear();
        //            }

        //            mObjectsInPath.Add( mobjectInPath );

        //            if( cd.StartsWith( Config.WEPEC_Prefix_Machine ) )
        //            {
        //                // 找到了关键的设备节点。
        //                mobject = mobjectInPath;
        //            }
        //        }

        //        mobjectName = StringUtils.Join( mObjectsInPath.Select( m => m.MobjectName_TX ), StringUtils.Slash );
        //    } // if( Config.IsWEPECCustomMode )

        //    // 输出专业ID
        //    if ( mobject.Spec_ID.HasValue )
        //    {
        //        specID = mobject.Spec_ID.Value;
        //    }

        //    // 点检方的岗位ID
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
        //        TraceUtils.Error( string.Format( "GetAlmDescTX获取测点通道信息({0})出错({1})！", point.Point_ID, msg.ErrorMessage ) );
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