
' 需要合并脚本的集合
Dim   paths 
paths = Array( "999.DBUpdate.sql", "04.CreateTriggers.sql", "11.CreateView.sql", "24.InitDDType.sql", "25.InitQueryViewConfig.sql", "26.InitDBStructDesc.sql", "29.InitModule.sql", "30.Authorization.sql", "52.ZXSamplePackage.sql", "56.CompressDataPackage.sql", "57.DataQueryPackage.sql","57.SMSQueryPackage.sql", "58.HistoryDataPackage.sql", "71.CreateView.sql", "99.SystemAppaction.sql" ) 

' 合并脚本的输出路径
dim target
target = "自动合并2642升级脚本.txt"

' ----------------------------------------------------------------------------------------------------
' 合并、写入文件
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
