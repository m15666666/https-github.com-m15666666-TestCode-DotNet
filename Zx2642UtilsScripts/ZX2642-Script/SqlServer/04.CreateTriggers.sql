if exists (select * from sysobjects where TYPE = 'TR' and Name = 'Trigger_ZX_History_Summary')
  drop TRIGGER Trigger_ZX_History_Summary;
  go
CREATE TRIGGER Trigger_ZX_History_Summary 
ON ZX_History_Summary
For INSERT 
   AS 
      Insert Into ZX_History_DataMapping(History_ID, Partition_ID)
  Select History_ID, Partition_ID
	From inserted;
Go

if exists (select * from sysobjects where TYPE = 'TR' and Name = 'Trigger_ZX_History_Alm')
  drop TRIGGER Trigger_ZX_History_Alm;
  go
CREATE TRIGGER Trigger_ZX_History_Alm 
ON ZX_History_Alm
For INSERT 
   AS 
      Insert Into ZX_SMSNewAlm(Alm_ID)
  Select Alm_ID
	From inserted;
Go

