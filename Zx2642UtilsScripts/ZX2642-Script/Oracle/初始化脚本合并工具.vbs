' ��Ҫ�ϲ��ű��ļ���
Dim   paths 
paths = Array( "01.CreateTable.sql", "04.CreateTriggers.sql", "11.CreateView.sql", "21.InitSysData.sql", "23.InitAppConfig.sql", "24.InitDDType.sql", "25.InitQueryViewConfig.sql", "26.InitDBStructDesc.sql", "29.InitModule.sql", "30.Authorization.sql", "52.ZXSamplePackage.sql", "56.CompressDataPackage.sql", "57.DataQueryPackage.sql","57.SMSQueryPackage.sql", "58.HistoryDataPackage.sql", "59.InitPartition.sql", "71.CreateView.sql", "99.SystemAppAction.sql" ) 

' �ϲ��ű������·��
dim target
target = "�Զ��ϲ�2642��ʼ���ű�.txt"

' ----------------------------------------------------------------------------------------------------
' �ϲ���д���ļ�
Sub ConcatenateTextFiles( paths, target )
	set fso = createobject( "scripting.filesystemobject" )
	set output = fso.createtextfile( target )

	for each path in paths
		dim text
		set file = fso.opentextfile( path )
		text = file.readall
		file.close

		output.writeline text
	next

	output.close

End Sub

call ConcatenateTextFiles( paths, target )
' ----------------------------------------------------------------------------------------------------

msgbox "�ɹ��ϲ�����鿴--�Զ��ϲ��ű�.txt--�������sqlserver���ݿ������ alter database �������� COLLATE Chinese_PRC_CI_AS" 