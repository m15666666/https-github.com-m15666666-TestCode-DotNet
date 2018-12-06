--��������Ϣ
Delete From BS_RangePartitionMeta Where TableBaseName_TX = 'ZX_History_Summary';
Insert Into BS_RangePartitionMeta (TableBaseName_TX,PartitionField_TX,Category_CD,ConstraintName_TX)
values('ZX_History_Summary','Partition_ID','����ժҪ����','ZXSummary');
Delete From BS_RangePartitionMeta Where TableBaseName_TX = 'ZX_History_Waveform';
Insert Into BS_RangePartitionMeta (TableBaseName_TX,PartitionField_TX,Category_CD,ConstraintName_TX)
values('ZX_History_Waveform','Partition_ID','���߲�������','ZXWaveform');
Delete From BS_RangePartitionMeta Where TableBaseName_TX = 'ZX_History_FeatureValue';
Insert Into BS_RangePartitionMeta (TableBaseName_TX,PartitionField_TX,Category_CD,ConstraintName_TX)
values('ZX_History_FeatureValue','Partition_ID','����ָ������','ZXFeatvalue');
Go



--��������ش�������,TableBaseNameList_TX�洢���ﴦ�����ر��ԡ�,���ָ���������ǰ�ӱ��ں�(M/D/Y,��/��/��)
Delete From BS_TransPartionTable Where TransKey_CD = 'ZX_HistoryData';
Insert Into BS_TransPartionTable(TransKey_CD,TableBaseNameList_TX,ViewBaseNameList_TX,PartitionType_CD,PartitionType_NR,MoveAfterDays,MoveType)
values('ZX_HistoryData','ZX_History_Summary,ZX_History_Waveform,ZX_History_FeatureValue','','M',1,'5,5,5','1');
Go

declare @V_emsg varchar(4000);
begin
   exec PartitionPackage.InitTableStruct @V_emsg;
end;
Go


