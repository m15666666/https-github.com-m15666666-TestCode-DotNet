/*==============================================================*/
/* DBMS name:      ORACLE Version 9i2                           */
/* Created on:     2011-3-23 14:58:36                           */
/*==============================================================*/
create or replace trigger Trigger_ZX_History_Summary
  after insert on ZX_History_Summary  
  for each row
declare
  -- local variables here
begin  
  Insert Into ZX_History_DataMapping(History_ID, Partition_ID)
  Values(:new.History_ID, :new.Partition_ID);
end Trigger_ZX_History_Summary;
/

create or replace trigger Trigger_ZX_History_Alm
  after insert on ZX_History_Alm  
  for each row
declare
  -- local variables here
begin  
  Insert Into ZX_SMSNewAlm(Alm_ID)
  Values(:new.Alm_ID);
end Trigger_ZX_History_Alm;
/
