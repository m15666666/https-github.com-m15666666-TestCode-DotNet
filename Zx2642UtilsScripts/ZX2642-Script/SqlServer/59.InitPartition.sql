--分区表信息
Delete From BS_RangePartitionMeta Where TableBaseName_TX = 'ZX_History_Summary';
Insert Into BS_RangePartitionMeta (TableBaseName_TX,PartitionField_TX,Category_CD,ConstraintName_TX)
values('ZX_History_Summary','Partition_ID','在线摘要数据','ZXSummary');
Delete From BS_RangePartitionMeta Where TableBaseName_TX = 'ZX_History_Waveform';
Insert Into BS_RangePartitionMeta (TableBaseName_TX,PartitionField_TX,Category_CD,ConstraintName_TX)
values('ZX_History_Waveform','Partition_ID','在线波形数据','ZXWaveform');
Delete From BS_RangePartitionMeta Where TableBaseName_TX = 'ZX_History_FeatureValue';
Insert Into BS_RangePartitionMeta (TableBaseName_TX,PartitionField_TX,Category_CD,ConstraintName_TX)
values('ZX_History_FeatureValue','Partition_ID','在线指标数据','ZXFeatvalue');
Go



--分区表相关处理事务,TableBaseNameList_TX存储事物处理的相关表，以“,”分隔，主表在前子表在后(M/D/Y,月/日/年)
Delete From BS_TransPartionTable Where TransKey_CD = 'ZX_HistoryData';
Insert Into BS_TransPartionTable(TransKey_CD,TableBaseNameList_TX,ViewBaseNameList_TX,PartitionType_CD,PartitionType_NR,MoveAfterDays,MoveType)
values('ZX_HistoryData','ZX_History_Summary,ZX_History_Waveform,ZX_History_FeatureValue','','M',1,'5,5,5','1');
Go

declare @V_emsg varchar(4000);
begin
   exec PartitionPackage.InitTableStruct @V_emsg;
end;
Go


