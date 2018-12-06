IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ZXSamplePackage].[F_GetPntDim]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [ZXSamplePackage].[F_GetPntDim]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ZXSamplePackage].[F_GetPntDirect]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [ZXSamplePackage].[F_GetPntDirect]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ZXSamplePackage].[F_GetPntRotation]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [ZXSamplePackage].[F_GetPntRotation]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ZXSamplePackage].[F_GetStoreType]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [ZXSamplePackage].[F_GetStoreType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ZXSamplePackage].[F_GetChannelType]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [ZXSamplePackage].[F_GetChannelType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ZXSamplePackage].[InsertAlmRecord]') AND type in (N'P', N'PC'))
DROP PROCEDURE [ZXSamplePackage].[InsertAlmRecord]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DataQueryPackage].[Pr_QueryHistorySummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [DataQueryPackage].[Pr_QueryHistorySummary]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DataQueryPackage].[Pr_QueryPointList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [DataQueryPackage].[Pr_QueryPointList]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DataQueryPackage].[pr_QueryNonRelatedFeatureGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [DataQueryPackage].[pr_QueryNonRelatedFeatureGroup]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CommonPackage].[F_GetPartitionTable]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [CommonPackage].[F_GetPartitionTable]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[StringDecode]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[StringDecode]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[GetWeekNo]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[GetWeekNo]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[GetDay]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[GetDay]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[GetMonth]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[GetMonth]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[GetYear]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[GetYear]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[ToDateString]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[ToDateString]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetSummaryByKey]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetSummaryByKey]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetSummaryBySyncNR]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetSummaryBySyncNR]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[ToFloatString1]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[ToFloatString1]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetSummaryListByPoint]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetSummaryListByPoint]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[ToDateTimeString]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[ToDateTimeString]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetSummaryByPointAndSpeed]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetSummaryByPointAndSpeed]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_CheckPointHistories]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_CheckPointHistories]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetWaveformByKey]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetWaveformByKey]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetFreqWaveformByKey]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetFreqWaveformByKey]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[StringToDate]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[StringToDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetFeatureValueByKey]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetFeatureValueByKey]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[StringToTime]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[StringToTime]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetFeaturesByChnNrAndFeat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetFeaturesByChnNrAndFeat]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetFeaturesByChannelNr]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetFeaturesByChannelNr]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[FormatInt]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[FormatInt]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetAllFeatTrendsByChnNSpeed]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetAllFeatTrendsByChnNSpeed]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[GetMod]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[GetMod]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetAllFeatTrendsByChnNr]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetAllFeatTrendsByChnNr]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetTrendDatasByChnNr]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetTrendDatasByChnNr]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[GetDayBegin]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[GetDayBegin]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CompressDataPackage].[Pr_DeleteZXHistoryDatas]') AND type in (N'P', N'PC'))
DROP PROCEDURE [CompressDataPackage].[Pr_DeleteZXHistoryDatas]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CompressDataPackage].[Pr_UpdateCompressID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [CompressDataPackage].[Pr_UpdateCompressID]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CompressDataPackage].[Pr_GetZXHistoryDatas]') AND type in (N'P', N'PC'))
DROP PROCEDURE [CompressDataPackage].[Pr_GetZXHistoryDatas]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[GetDayEnd]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[GetDayEnd]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[Pr_DebugPrint]') AND type in (N'P', N'PC'))
DROP PROCEDURE [DBDiffPackage].[Pr_DebugPrint]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[Pr_ExecuteCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [DBDiffPackage].[Pr_ExecuteCount]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[JCXXPackage].[Pr_SetPAByRA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [JCXXPackage].[Pr_SetPAByRA]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[JCXXPackage].[Pr_SetPAsByRA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [JCXXPackage].[Pr_SetPAsByRA]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[JCXXPackage].[Pr_SetPDByRD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [JCXXPackage].[Pr_SetPDByRD]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[JCXXPackage].[Pr_SetPDsByRD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [JCXXPackage].[Pr_SetPDsByRD]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[JCXXPackage].[Pr_SetPIByUI]') AND type in (N'P', N'PC'))
DROP PROCEDURE [JCXXPackage].[Pr_SetPIByUI]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[JCXXPackage].[Pr_SetPIsByUI]') AND type in (N'P', N'PC'))
DROP PROCEDURE [JCXXPackage].[Pr_SetPIsByUI]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[GetDayCountOfMonth]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[GetDayCountOfMonth]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[AddDays]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[AddDays]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[AddMonths]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[AddMonths]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[CreateDate]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[CreateDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_GetTrendDatasByChnNSpeed]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_GetTrendDatasByChnNSpeed]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[Pr_DeleteSummaryByKey]') AND type in (N'P', N'PC'))
DROP PROCEDURE [HistoryDataPackage].[Pr_DeleteSummaryByKey]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[XTGLPackage].[Pr_InsertAppLog]') AND type in (N'P', N'PC'))
DROP PROCEDURE [XTGLPackage].[Pr_InsertAppLog]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[XTGLPackage].[Pr_UserLogin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [XTGLPackage].[Pr_UserLogin]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[XTGLPackage].[Pr_InsertMenuBrowse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [XTGLPackage].[Pr_InsertMenuBrowse]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[GetPagingSQL]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[GetPagingSQL]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CommonPackage].[F_Split]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [CommonPackage].[F_Split]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CommonPackage].[F_SplitCount]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [CommonPackage].[F_SplitCount]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HistoryDataPackage].[F_GetPartitionIDByHistoryID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [HistoryDataPackage].[F_GetPartitionIDByHistoryID]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CommonPackage].[F_GetPartitionSQL]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [CommonPackage].[F_GetPartitionSQL]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[GetFirstDayOfMonth]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[GetFirstDayOfMonth]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[GetLastDayOfMonth]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[GetLastDayOfMonth]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[GetSubstring]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[GetSubstring]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[GetCharIndex]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[GetCharIndex]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[StringToInt64]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[StringToInt64]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[GetLength]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[GetLength]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[XTGLPackage].[Pr_GetLastObjectID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [XTGLPackage].[Pr_GetLastObjectID]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[GetSystemTime]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[GetSystemTime]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[XTGLPackage].[Pr_GetLastBigObjectID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [XTGLPackage].[Pr_GetLastBigObjectID]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DBDiffPackage].[IntToString]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [DBDiffPackage].[IntToString]
IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'XTGLPackage')
DROP SCHEMA [XTGLPackage]
GO
IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'JCXXPackage')
DROP SCHEMA [JCXXPackage]
GO
IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'HistoryDataPackage')
DROP SCHEMA [HistoryDataPackage]
GO
IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'DBDiffPackage')
DROP SCHEMA [DBDiffPackage]
GO
IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'DataQueryPackage')
DROP SCHEMA [DataQueryPackage]
GO
IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'CompressDataPackage')
DROP SCHEMA [CompressDataPackage]
GO
IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'CommonPackage')
DROP SCHEMA [CommonPackage]
GO
IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'ZXSamplePackage')
DROP SCHEMA ZXSamplePackage
GO