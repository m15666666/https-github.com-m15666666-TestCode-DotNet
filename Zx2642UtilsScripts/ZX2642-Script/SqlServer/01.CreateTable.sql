--==============================================================
--Table: AlmStand_CommonSetting                           
--==============================================================
CREATE TABLE AlmStand_CommonSetting 
(
    CommonSetting_ID int NOT NULL,
    AlmType_ID tinyint NULL,
    LowLimit1_NR float NULL,
    HighLimit1_NR float NULL,
    LowLimit2_NR float NULL,
    HighLimit2_NR float NULL,
    LowLimit3_NR float NULL,
    HighLimit3_NR float NULL,
    LowLimit4_NR float NULL,
    HighLimit4_NR float NULL,
    Description_TX varchar(200) NULL,
    CONSTRAINT PK_AlmStand_CommonSetting PRIMARY KEY (CommonSetting_ID)
);


--==============================================================
--Table: AlmStand_PntCommon                           
--==============================================================
CREATE TABLE AlmStand_PntCommon 
(
    Point_ID int NOT NULL,
    FeatureValue_ID int NOT NULL,
    CommonSetting_ID int NOT NULL,
    CONSTRAINT PK_AlmStand_PntCommon PRIMARY KEY (Point_ID, FeatureValue_ID)
);


--==============================================================
--Table: AlmStand_SixFreqBnd                           
--==============================================================
CREATE TABLE AlmStand_SixFreqBnd 
(
    Point_ID int NOT NULL,
    SixFreqBndType_ID tinyint NOT NULL,
    LowAlmLevel_ID tinyint NULL,
    HighAlmLevel_ID tinyint NULL,
    FreqBnd_NR int NULL,
    LowFreq1_NR float NULL,
    HighFreq1_NR float NULL,
    LowLimit1_NR float NULL,
    HighLimit1_NR float NULL,
    LowFreq2_NR float NULL,
    HighFreq2_NR float NULL,
    LowLimit2_NR float NULL,
    HighLimit2_NR float NULL,
    LowFreq3_NR float NULL,
    HighFreq3_NR float NULL,
    LowLimit3_NR float NULL,
    HighLimit3_NR float NULL,
    LowFreq4_NR float NULL,
    HighFreq4_NR float NULL,
    LowLimit4_NR float NULL,
    HighLimit4_NR float NULL,
    LowFreq5_NR float NULL,
    HighFreq5_NR float NULL,
    LowLimit5_NR float NULL,
    HighLimit5_NR float NULL,
    LowFreq6_NR float NULL,
    HighFreq6_NR float NULL,
    LowLimit6_NR float NULL,
    HighLimit6_NR float NULL,
    Description_TX varchar(200) NULL,
    CONSTRAINT PK_AlmStand_SixFreqBnd PRIMARY KEY (Point_ID, SixFreqBndType_ID)
);


--==============================================================
--Table: Analysis_BarDiagram                           
--==============================================================
CREATE TABLE Analysis_BarDiagram 
(
    BarDiagram_ID int NOT NULL,
    AppUser_ID int NULL,
    CONSTRAINT PK_Analysis_BarDiagram PRIMARY KEY (BarDiagram_ID)
);


--==============================================================
--Table: Analysis_BarGroup                           
--==============================================================
CREATE TABLE Analysis_BarGroup 
(
    BarGroup_ID int NOT NULL,
    BarDiagram_ID int NOT NULL,
    GroupName_TX varchar(100) NULL,
    SortNo_NR int NULL,
    CONSTRAINT PK_Analysis_BarGroup PRIMARY KEY (BarGroup_ID)
);


--==============================================================
--Table: Analysis_BarPoint                           
--==============================================================
CREATE TABLE Analysis_BarPoint 
(
    BarPoint_ID int NOT NULL,
    BarGroup_ID int NOT NULL,
    Point_ID int NOT NULL,
    PointName_TX varchar(100) NULL,
    Low_NR float DEFAULT 0 NULL,
    High_NR float NULL,
    SortNo_NR int NULL,
    CONSTRAINT PK_Analysis_BarPoint PRIMARY KEY (BarPoint_ID)
);


--==============================================================
--Table: Analysis_FeatureFreq                           
--==============================================================
CREATE TABLE Analysis_FeatureFreq 
(
    FeatureFreq_ID int NOT NULL,
    Group_ID int NOT NULL,
    FeatureFreqValue_NR float NOT NULL,
    Name_TX varchar(100) NOT NULL,
    Unit_NR int NULL,
    CONSTRAINT PK_Analysis_FeatureFreq PRIMARY KEY (FeatureFreq_ID)
);


--==============================================================
--Table: Analysis_FeatureFreqGroup                           
--==============================================================
CREATE TABLE Analysis_FeatureFreqGroup 
(
    Group_ID int NOT NULL,
    GroupName_TX varchar(100) NOT NULL,
    Org_ID int NULL,
    CONSTRAINT PK_Analysis_FeatureFreqGroup PRIMARY KEY (Group_ID)
);


--==============================================================
--Table: Analysis_MObjPicture                           
--==============================================================
CREATE TABLE Analysis_MObjPicture 
(
    MObject_ID int NOT NULL,
    Picture_GR image NULL,
    FileName_TX varchar(200) NULL,
    FileSuffix_TX varchar(4) NULL,
    CONSTRAINT PK_Analysis_MObjPicture PRIMARY KEY (MObject_ID)
);


--==============================================================
--Table: Analysis_MObjPosition                           
--==============================================================
CREATE TABLE Analysis_MObjPosition 
(
    MObjPosition_ID int NOT NULL,
    Mobject_ID int NOT NULL,
    PosX_NR float NOT NULL,
    PosY_NR float NOT NULL,
    TagX_NR float NOT NULL,
    TagY_NR float NOT NULL,
    CONSTRAINT PK_Analysis_MObjPosition PRIMARY KEY (MObjPosition_ID)
);


--==============================================================
--Table: Analysis_OrgPicture                           
--==============================================================
CREATE TABLE Analysis_OrgPicture 
(
    Org_ID int NOT NULL,
    Picture_GR image NULL,
    FileName_TX varchar(200) NULL,
    FileSuffix_TX varchar(4) NULL,
    CONSTRAINT PK_Analysis_OrgPicture PRIMARY KEY (Org_ID)
);


--==============================================================
--Table: Analysis_PntPosition                           
--==============================================================
CREATE TABLE Analysis_PntPosition 
(
    PntPosition_ID int NOT NULL,
    Point_ID int NOT NULL,
    Mobject_ID int NOT NULL,
    PosX_NR float NOT NULL,
    PosY_NR float NOT NULL,
    TagX_NR float NOT NULL,
    TagY_NR float NOT NULL,
    CONSTRAINT PK_Analysis_PntPosition PRIMARY KEY (PntPosition_ID)
);


--==============================================================
--Table: MOb_FeatreFreqGroup                           
--==============================================================
CREATE TABLE MOb_FeatreFreqGroup 
(
    Group_ID int NOT NULL,
    Mobject_ID int NOT NULL,
    CONSTRAINT PK_MOb_FeatreFreqGroup PRIMARY KEY (Group_ID, Mobject_ID)
);


--==============================================================
--Table: Pnt_DataVar                           
--==============================================================
CREATE TABLE Pnt_DataVar 
(
    Var_ID int NOT NULL,
    VarName_TX varchar(50) NOT NULL,
    VarDesc_TX varchar(300) NULL,
    VarScale_NR float DEFAULT 1 NOT NULL,
    VarOffset_NR float DEFAULT 0 NOT NULL,
    CONSTRAINT PK_Pnt_DataVar PRIMARY KEY (Var_ID)
);


--==============================================================
--Table: Pnt_PntDataVar                           
--==============================================================
CREATE TABLE Pnt_PntDataVar 
(
    Point_ID int NOT NULL,
    Var_ID int NOT NULL,
    CONSTRAINT PK_Pnt_PntDataVar PRIMARY KEY (Point_ID, Var_ID)
);


--==============================================================
--Table: Pnt_Point                           
--==============================================================
CREATE TABLE Pnt_Point 
(
    Point_ID int NOT NULL,
    Mobject_ID int NOT NULL,
    PointName_TX varchar(200) NOT NULL,
    DatType_ID smallint NOT NULL,
    SigType_ID smallint NOT NULL,
    PntDim_NR smallint DEFAULT 1 NOT NULL,
    PntDirect_NR smallint NOT NULL,
    Rotation_NR smallint DEFAULT 0 NOT NULL,
    EngUnit_ID int NOT NULL,
    FeatureValue_ID int NOT NULL,
    StoreType_NR smallint DEFAULT 0 NOT NULL,
    SortNo_NR int NULL,
    Desc_TX varchar(300) NULL,
    CONSTRAINT PK_Pnt_Point PRIMARY KEY (Point_ID)
);


--==============================================================
--Table: Pnt_PointGroup                           
--==============================================================
CREATE TABLE Pnt_PointGroup 
(
    PointGroup_ID int NOT NULL,
    Point_ID int NOT NULL,
    Mobject_ID int NOT NULL,
    GroupNo_NR int NOT NULL,
    Level_NR int NOT NULL,
    TopClearance_NR float NULL,
    CONSTRAINT PK_Pnt_PointGroup PRIMARY KEY (PointGroup_ID)
);


--==============================================================
--Table: Sample_DAUStation                           
--==============================================================
CREATE TABLE Sample_DAUStation 
(
    Station_ID int NOT NULL,
    ServerDAU_ID int NOT NULL,
    CONSTRAINT PK_Sample_DAUStation PRIMARY KEY (Station_ID)
);


--==============================================================
--Table: Sample_PntChannel                           
--==============================================================
CREATE TABLE Sample_PntChannel 
(
    StationChannel_ID int NOT NULL,
    Point_ID int NOT NULL,
    ChnNo_NR tinyint NULL,
    CONSTRAINT PK_Sample_PntChannel PRIMARY KEY (StationChannel_ID)
);


--==============================================================
--Table: Sample_Server                           
--==============================================================
CREATE TABLE Sample_Server 
(
    Server_ID int NOT NULL,
    Name_TX varchar(60) NOT NULL,
    URL_TX varchar(100) NOT NULL,
    ServerParam_TX varchar(2000) NULL,
    IP_TX varchar(100) NULL,
    CONSTRAINT PK_Sample_Server PRIMARY KEY (Server_ID)
);


--==============================================================
--Table: Sample_ServerDAU                           
--==============================================================
CREATE TABLE Sample_ServerDAU 
(
    ServerDAU_ID int NOT NULL,
    Server_ID int NOT NULL,
    Name_TX varchar(60) NOT NULL,
    URL_TX varchar(100) NOT NULL,
    ServerDAUParam_TX varchar(2000) NULL,
    IP_TX varchar(100) NULL,
    CONSTRAINT PK_Sample_ServerDAU PRIMARY KEY (ServerDAU_ID)
);


--==============================================================
--Table: Sample_Station                           
--==============================================================
CREATE TABLE Sample_Station 
(
    Station_ID int NOT NULL,
    Org_ID int DEFAULT 0 NULL,
    Name_TX varchar(60) NULL,
    StationSN_TX varchar(32) NULL,
    StationType_TX varchar(100) NULL,
    IP_TX varchar(100) NULL,
    StationParam_TX varchar(2000) NULL,
    CONSTRAINT PK_Sample_Station PRIMARY KEY (Station_ID)
);


--==============================================================
--Table: Sample_StationChannel                           
--==============================================================
CREATE TABLE Sample_StationChannel 
(
    StationChannel_ID int NOT NULL,
    Station_ID int NOT NULL,
    ChannelType_NR int NULL,
    ChannelNumber_NR int NULL,
    StationChannelParam_TX varchar(2000) NULL,
    CONSTRAINT PK_Sample_StationChannel PRIMARY KEY (StationChannel_ID)
);


--==============================================================
--Table: ZX_AlmLevel                           
--==============================================================
CREATE TABLE ZX_AlmLevel 
(
    AlmLevel_ID int NOT NULL,
    Name_TX varchar(40) NULL,
    RefAlmLevel_ID int NOT NULL,
    CONSTRAINT PK_ZX_AlmLevel PRIMARY KEY (AlmLevel_ID)
);


--==============================================================
--Table: ZX_Bearing                           
--==============================================================
CREATE TABLE ZX_Bearing 
(
    Bearing_ID int NOT NULL,
    Facotry_TX varchar(100) NULL,
    Model_TX varchar(100) NULL,
    RollerCount_NR int NULL,
    FTF_NR float NOT NULL,
    BSF_NR float NULL,
    BBPFO_NR float NULL,
    BPFI_NR float NULL,
    IsInlay_YN char(1) NULL,
    Description_TX varchar(200) NULL,
    CONSTRAINT PK_ZX_Bearing PRIMARY KEY (Bearing_ID)
);


--==============================================================
--Table: ZX_History_Alm                           
--==============================================================
CREATE TABLE ZX_History_Alm 
(
    Partition_ID bigint NOT NULL,
    Alm_ID bigint NOT NULL,
    FeatureValue_ID int NULL,
    AlmLevel_ID smallint NULL,
    Alm_DT datetime NULL,
    MObject_ID int NULL,
    Point_ID int NULL,
    AlmDesc_TX varchar(1000) NULL,
    DealWithStatus_CD char(2) NULL,
    Memo_TX varchar(1000) NULL,
    OwnerDept_ID int NULL,
    OwnerUser_ID int NULL,
    DealWithUser_ID int NULL,
    DealWith_DT datetime NULL,
    DealWithRel_CD varchar(60) NULL,
    DealWithType_NR int NULL,
    DealWithResult_TX varchar(200) NULL,
    DealWithFromRel_CD varchar(60) NULL,
    CONSTRAINT PK_ZX_History_Alm PRIMARY KEY (Alm_ID)
);


--==============================================================
--Table: ZX_History_DataMapping                           
--==============================================================
CREATE TABLE ZX_History_DataMapping 
(
    Partition_ID bigint NOT NULL,
    History_ID bigint NOT NULL,
    CONSTRAINT PK_ZX_History_DataMapping PRIMARY KEY (History_ID)
);


--==============================================================
--Table: ZX_History_FeatureValue                           
--==============================================================
CREATE TABLE ZX_History_FeatureValue 
(
    FeatureValuePK_ID bigint NOT NULL,
    History_ID bigint NOT NULL,
    ChnNo_NR tinyint NOT NULL,
    FeatureValueType_ID int DEFAULT 0 NOT NULL,
    FeatureValue_ID int NOT NULL,
    Partition_ID bigint NOT NULL,
    FeatureValue_NR float NOT NULL,
    SigType_NR int NOT NULL,
    EngUnit_ID int NOT NULL,
    CONSTRAINT PK_ZX_History_FeatureValue PRIMARY KEY (FeatureValuePK_ID)
);


--==============================================================
--Table: ZX_History_Summary                           
--==============================================================
CREATE TABLE ZX_History_Summary 
(
    Partition_ID bigint NOT NULL,
    History_ID bigint NOT NULL,
    Point_ID int NOT NULL,
    Compress_ID tinyint NOT NULL,
    CompressType_ID int NOT NULL,
    PntDim_NR tinyint NOT NULL,
    DatType_NR tinyint NOT NULL,
    SigType_NR int NOT NULL,
    SampTime_DT datetime NOT NULL,
    SampTimeGMT_DT datetime NOT NULL,
    DatLen_NR int NOT NULL,
    RotSpeed_NR int NOT NULL,
    MinFreq_NR float DEFAULT 0 NOT NULL,
    SampleFreq_NR float NOT NULL,
    SampMod_NR tinyint NOT NULL,
    Result_TX varchar(100) NULL,
    AlmLevel_ID int NOT NULL,
    DataGroup_NR int NOT NULL,
    Alm_ID bigint NOT NULL,
    EngUnit_ID int DEFAULT 0 NOT NULL,
    Synch_NR bigint NOT NULL,
    CONSTRAINT PK_ZX_History_Summary PRIMARY KEY (History_ID)
);


--==============================================================
--Table: ZX_History_Waveform                           
--==============================================================
CREATE TABLE ZX_History_Waveform 
(
    Waveform_ID bigint NOT NULL,
    History_ID bigint NOT NULL,
    ChnNo_NR tinyint NOT NULL,
    Partition_ID bigint NOT NULL,
    WaveformType_ID int DEFAULT 0 NOT NULL,
    SigType_NR int NOT NULL,
    DatLen_NR int NOT NULL,
    RotSpeed_NR int NOT NULL,
    MinFreq_NR float DEFAULT 0 NOT NULL,
    SampleFreq_NR float NOT NULL,
    SampMod_NR tinyint DEFAULT 0 NOT NULL,
    EngUnit_ID int NOT NULL,
    Demod_YN char(1) DEFAULT '0' NOT NULL,
    DMinFreq_NR int DEFAULT 0 NOT NULL,
    DMaxFreq_NR int DEFAULT 0 NOT NULL,
    Wave_GR image NOT NULL,
    Compress_NR tinyint NOT NULL,
    WaveScale_NR float NOT NULL,
    CONSTRAINT PK_ZX_History_Waveform PRIMARY KEY (Waveform_ID)
);


--==============================================================
--Table: ZX_SMS                           
--==============================================================
CREATE TABLE ZX_SMS 
(
    SMS_ID int NOT NULL,
    Alm_ID bigint NULL,
    Point_ID int NULL,
    Mobject_ID int NULL,
    AppUser_ID int NULL,
    SendCount_NR int NULL,
    SendTime_TM datetime NULL,
    SMSContent_TX varchar(200) NULL,
    CONSTRAINT PK_ZX_SMSFailure PRIMARY KEY (SMS_ID)
);


--==============================================================
--Table: ZX_SMSConfig                           
--==============================================================
CREATE TABLE ZX_SMSConfig 
(
	SMSConfig_ID int NOT NULL,
    Mobject_ID int NOT NULL,
    AppUser_ID int NOT NULL,
	AlmLevel_ID int NULL,
    CONSTRAINT PK_ZX_SMSConfig PRIMARY KEY (SMSConfig_ID)
);


--==============================================================
--Table: ZX_SMSHistory                           
--==============================================================
CREATE TABLE ZX_SMSHistory 
(
    SMSHistory_ID int NOT NULL,
    Partition_ID bigint NOT NULL,
    Alm_ID bigint NOT NULL,
    Point_ID int NOT NULL,
    Mobject_ID int NOT NULL,
    AppUser_ID int NOT NULL,
    SendCount_NR int NULL,
    Status_YN char(1) NULL,
    SendTime_TM datetime NULL,
    SMSContent_TX varchar(200) NULL,
	Memo_TX varchar(2000) NULL,
    CONSTRAINT PK_ZX_SMSHistory PRIMARY KEY (SMSHistory_ID)
);


--==============================================================
--Table: ZX_SMSNewAlm                           
--==============================================================
CREATE TABLE ZX_SMSNewAlm 
(
    Alm_ID bigint NOT NULL,
    CONSTRAINT PK_ZX_SMS PRIMARY KEY (Alm_ID)
);

--==============================================================
--Index: FK_PntDataVar_Pnt_FK                           
--==============================================================
CREATE UNIQUE INDEX FK_PntDataVar_Pnt_FK ON Pnt_PntDataVar
(
  Point_ID ASC
);


--==============================================================
--Index: FK_PntDataVar_Var_FK                           
--==============================================================
CREATE UNIQUE INDEX FK_PntDataVar_Var_FK ON Pnt_PntDataVar
(
  Var_ID ASC
);


--==============================================================
--Index: FK_PointGroup_Mobject_FK                           
--==============================================================
CREATE INDEX FK_PointGroup_Mobject_FK ON Pnt_PointGroup
(
  Mobject_ID ASC
);


--==============================================================
--Index: FK_PointGroup_Point_FK                           
--==============================================================
CREATE INDEX FK_PointGroup_Point_FK ON Pnt_PointGroup
(
  Point_ID ASC
);


--==============================================================
--Index: I_PntChannel_PointID                           
--==============================================================
CREATE INDEX I_PntChannel_PointID ON Sample_PntChannel
(
  Point_ID ASC
);


--==============================================================
--Index: I_ZX_FeatureValue_HistoryID                           
--==============================================================
CREATE INDEX I_ZX_FeatureValue_HistoryID ON ZX_History_FeatureValue
(
  History_ID ASC
);


--==============================================================
--Index: I_ZX_Summary_PartitionID                           
--==============================================================
CREATE INDEX I_ZX_Summary_PartitionID ON ZX_History_Summary
(
  Partition_ID ASC
);


--==============================================================
--Index: I_ZX_Summary_PointID                           
--==============================================================
CREATE INDEX I_ZX_Summary_PointID ON ZX_History_Summary
(
  Point_ID ASC
);


--==============================================================
--Index: I_ZX_WaveForm_HistoryID                           
--==============================================================
CREATE INDEX I_ZX_WaveForm_HistoryID ON ZX_History_Waveform
(
  History_ID ASC
);


--==============================================================
--Index: IAnalysis_BarGroup_DiagramID                           
--==============================================================
CREATE INDEX IAnalysis_BarGroup_DiagramID ON Analysis_BarGroup
(
  BarDiagram_ID ASC
);


--==============================================================
--Index: IAnalysis_BarPoint_GroupID                           
--==============================================================
CREATE INDEX IAnalysis_BarPoint_GroupID ON Analysis_BarPoint
(
  BarGroup_ID ASC
);


--==============================================================
--Index: IAnalysis_FeatureFreq                           
--==============================================================
CREATE INDEX IAnalysis_FeatureFreq ON Analysis_FeatureFreq
(
  FeatureFreq_ID ASC,
  Group_ID ASC
);


--==============================================================
--Index: ISample_StationChannel                           
--==============================================================
CREATE INDEX ISample_StationChannel ON Sample_StationChannel
(
  StationChannel_ID ASC,
  Station_ID ASC
);


ALTER TABLE Analysis_BarGroup
  ADD CONSTRAINT FK_ANALYSIS_FK_BAR_GR_ANALYSIS 
    FOREIGN KEY (BarDiagram_ID)
      REFERENCES Analysis_BarDiagram
        ON DELETE CASCADE;


ALTER TABLE Analysis_BarPoint
  ADD CONSTRAINT FK_ANALYSIS_FK_BAR_PO_ANALYSIS 
    FOREIGN KEY (BarGroup_ID)
      REFERENCES Analysis_BarGroup
        ON DELETE CASCADE;


ALTER TABLE Analysis_FeatureFreq
  ADD CONSTRAINT FK_ANS_FEATFR_ANS_FEATFREQGR 
    FOREIGN KEY (Group_ID)
      REFERENCES Analysis_FeatureFreqGroup
        ON DELETE CASCADE;


ALTER TABLE MOb_FeatreFreqGroup
  ADD CONSTRAINT FK_MOB_FEAT_REFERENCE_MOB_MOBJ 
    FOREIGN KEY (Mobject_ID)
      REFERENCES Mob_MObject;


ALTER TABLE MOb_FeatreFreqGroup
  ADD CONSTRAINT FK_MOB_FEATGR_ANS_FEATFREQGR 
    FOREIGN KEY (Group_ID)
      REFERENCES Analysis_FeatureFreqGroup;


ALTER TABLE Pnt_PointGroup
  ADD CONSTRAINT FK_PointGroup_Mobject 
    FOREIGN KEY (Mobject_ID)
      REFERENCES Mob_MObject
        ON DELETE CASCADE;


ALTER TABLE Pnt_PointGroup
  ADD CONSTRAINT FK_PointGroup_Point 
    FOREIGN KEY (Point_ID)
      REFERENCES Pnt_Point
        ON DELETE CASCADE;


ALTER TABLE Sample_PntChannel
  ADD CONSTRAINT FK_SAMPLE_P_FK_ONLINE_SAMPLE_S 
    FOREIGN KEY (StationChannel_ID)
      REFERENCES Sample_StationChannel;


ALTER TABLE Sample_PntChannel
  ADD CONSTRAINT FK_SAMPLE_P_REFERENCE_PNT_POIN 
    FOREIGN KEY (Point_ID)
      REFERENCES Pnt_Point;


ALTER TABLE Sample_StationChannel
  ADD CONSTRAINT FK_SAMPLE_S_FK_ONLINE_SAMPLE_S 
    FOREIGN KEY (Station_ID)
      REFERENCES Sample_Station;


ALTER TABLE ZX_History_FeatureValue
  ADD CONSTRAINT FK_ZX_HIS_FEAT_ZX_HIS_Sum 
    FOREIGN KEY (History_ID)
      REFERENCES ZX_History_Summary
        ON DELETE CASCADE;
GO


ALTER TABLE ZX_History_Waveform
  ADD CONSTRAINT FK_ZX_HIS_Wav_ZX_HIS_Sum 
    FOREIGN KEY (History_ID)
      REFERENCES ZX_History_Summary
        ON DELETE CASCADE;
		
GO