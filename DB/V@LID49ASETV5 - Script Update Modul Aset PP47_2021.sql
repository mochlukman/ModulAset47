--Script untuk mengupdate Modul Aset
--Tanggal 2 Agustus 2022
		
-- Begin Store Procedure HY_LOOKUP_REK
if exists ( select *
            from  sys.objects
            where object_id = object_id(N'HY_LOOKUP_REK')
                    and type in ( N'P', N'PC' ) 
) 
begin
  drop procedure HY_LOOKUP_REK
end
go

/*
declare @unitkey nvarchar(36)
  ,@kdtahap nvarchar(2)
  ,@kdkegunit nvarchar(36)
  ,@noba nvarchar(50)
  ,@idxdask nvarchar(36)

set @unitkey = '1023_'
set @kdtahap ='2'
set @kdkegunit = '5021_'

exec HY_LOOKUP_REK @unitkey, @kdtahap, @kdkegunit, @noba
--*/
create procedure HY_LOOKUP_REK (
  @unitkey nvarchar(36)
  ,@kdtahap nvarchar(2)
  ,@kdkegunit nvarchar(36)
  ,@noba nvarchar(50)
) as
begin

/*
declare @unitkey nvarchar(36)
  ,@kdtahap nvarchar(2)
  ,@kdkegunit nvarchar(36)
  ,@noba nvarchar(50)

set @unitkey = '1023_'
set @kdtahap ='2'
set @kdkegunit = '5021_'
--*/

  declare @thang int, @idxdask nvarchar(36)

  set @thang = (select rtrim(CONFIGVAL) from PEMDA where CONFIGID = 'cur_thang')


  set @idxdask = (select top 1 A.IDXDASK from skdask A 
    where A.UNITKEY = @unitkey
    and A.KDTAHAP = @kdtahap
    and A.IDXDASK in 
      (
         select IDXDASK from DASKR A
         where A.UNITKEY = @unitkey and A.THANG = @thang and A.IDXKODE = 2 and A.KDKEGUNIT=@kdkegunit 
         and TGLVALID is not null and NOSAH <> ''
      )
    order by A.TGLDASK desc
  )

  select A.UNITKEY, D.KDUNIT,D.NMUNIT, A.IDXKODE, A.KDKEGUNIT, A.MTGKEY, B.KDPER, B.NMPER, A.NILAI,A.THANG
  from DASKR A
  left join MATANGR B on A.MTGKEY=B.MTGKEY
  left join SKDASK C on A.IDXDASK=C.IDXDASK and A.UNITKEY = C.UNITKEY and c.THANG = a.THANG and a.IDXKODE = c.IDXKODE
  left join DAFTUNIT D on A.UNITKEY=D.UNITKEY and d.UNITKEY = c.UNITKEY
  where A.UNITKEY= @unitkey
  and C.KDTAHAP=@kdtahap
  and A.KDKEGUNIT= @kdkegunit
  and A.MTGKEY not in(select MTGKEY from ASET_BERITADETR where UNITKEY=@unitkey and NOBA=@noba)
  --and left(B.KDPER,4) in ('5.1.','5.2.')
  and A.THANG = (select rtrim(CONFIGVAL) from PEMDA where CONFIGID = 'cur_thang')
  and C.IDXDASK = @idxdask

  /*
  select distinct  left(KDPER,4) as KDPER from MATANGR
  --*/
end
go
		
-- End Store Procedure HY_LOOKUP_REK

		

--Insert JTRANS
delete from ASET_BERITA where KDTANS in ('119','120','121','122','123','124','125','126','127','128','129','130','131','132','302','314','315','999')
delete from ASET_PENGGUNA where KDTANS in ('119','120','121','122','123','124','125','126','127','128','129','130','131','132','302','314','315','999')
delete from ASET_REKLAS where KDTANS in ('119','120','121','122','123','124','125','126','127','128','129','130','131','132','302','314','315','999')
delete from ASET_KEMITRAAN where KDTANS in ('119','120','121','122','123','124','125','126','127','128','129','130','131','132','302','314','315','999')
delete from ASET_KOREKSI where KDTANS in ('119','120','121','122','123','124','125','126','127','128','129','130','131','132','302','314','315','999')
delete from JTRANS where KDTANS in ('119','120','121','122','123','124','125','126','127','128','129','130','131','132','302','314','315','999')
go

INSERT INTO JTRANS(  KDTANS, NMTRANS, KET, JENIS, DATECREATE, DATEUPDATE)
VALUES ('119', 'BOS', 'BAST', '1', GETDATE(), GETDATE());

INSERT INTO JTRANS(  KDTANS, NMTRANS, KET, JENIS, DATECREATE, DATEUPDATE)
VALUES ('120', 'BLUD', 'BAST', '1', GETDATE(), GETDATE());

INSERT INTO JTRANS(  KDTANS, NMTRANS, KET, JENIS, DATECREATE, DATEUPDATE)
VALUES ('121', 'Ketentuan Perundang-undangan', 'BAST', '1', GETDATE(), GETDATE());

INSERT INTO JTRANS(  KDTANS, NMTRANS, KET, JENIS, DATECREATE, DATEUPDATE)
VALUES ('122', 'Putusan Pengadilan', 'BAST', '1', GETDATE(), GETDATE());

INSERT INTO JTRANS(  KDTANS, NMTRANS, KET, JENIS, DATECREATE, DATEUPDATE)
VALUES ('123', 'Divestasi', 'BAST', '1', GETDATE(), GETDATE());

INSERT INTO JTRANS(  KDTANS, NMTRANS, KET, JENIS, DATECREATE, DATEUPDATE)
VALUES ('124', 'Pembatalan Penghapusan', 'BAST', '1', GETDATE(), GETDATE());

INSERT INTO JTRANS(  KDTANS, NMTRANS, KET, JENIS, DATECREATE, DATEUPDATE)
VALUES ('125', 'Perolehan/Penerimaan Lainnya', 'BAST', '1', GETDATE(), GETDATE());

INSERT INTO JTRANS(KDTANS, NMTRANS, KET, JENIS, DATECREATE, DATEUPDATE)
VALUES ('126', 'Penerimaan BMD Internal', 'BAST BMD Internal', '1', GETDATE(), GETDATE());

INSERT INTO JTRANS(KDTANS, NMTRANS, KET, JENIS, DATECREATE, DATEUPDATE)
VALUES ('127', 'Pengeluaran BMD Internal', 'BAST BMD Internal', '1', GETDATE(), GETDATE());

INSERT INTO JTRANS(KDTANS, NMTRANS, KET, JENIS, DATECREATE, DATEUPDATE)
VALUES ('129', 'Reklas Persediaan Ke Aset Tetap', ' ', '1', GETDATE(), GETDATE());

INSERT INTO JTRANS(KDTANS, NMTRANS, KET, JENIS, DATECREATE, DATEUPDATE)
VALUES ('130', 'Koreksi Pencatatan Ganda', 'Selisih lebih nilai koreksi dengan nilai perolehan sebelumnya', '3', GETDATE(), GETDATE());

INSERT INTO JTRANS(KDTANS, NMTRANS, KET, JENIS, DATECREATE, DATEUPDATE)
VALUES ('131', 'Koreksi Spesifikasi', 'Selisih lebih nilai koreksi dengan nilai perolehan sebelumnya', '3', GETDATE(), GETDATE());

INSERT INTO JTRANS(KDTANS, NMTRANS, KET, JENIS, DATECREATE, DATEUPDATE)
VALUES ('132', 'Koreksi Lainnya', 'Selisih lebih nilai koreksi dengan nilai perolehan sebelumnya', '3', GETDATE(), GETDATE());

INSERT INTO JTRANS(KDTANS, NMTRANS, KET, JENIS, DATECREATE, DATEUPDATE)
VALUES ('302', 'Pinjam Pakai', 'Pemanfaatan', '2', GETDATE(), GETDATE());

INSERT INTO JTRANS(KDTANS, NMTRANS, KET, JENIS, DATECREATE, DATEUPDATE)
VALUES ('314', 'Pemindahtanganan', '', '3', GETDATE(), GETDATE());

INSERT INTO JTRANS(KDTANS, NMTRANS, KET, JENIS, DATECREATE, DATEUPDATE)
VALUES ('315', 'Pemusnahan', '', '3', GETDATE(), GETDATE());

INSERT INTO JTRANS(KDTANS, NMTRANS, KET, JENIS, DATECREATE, DATEUPDATE)
VALUES ('999', 'Sebab Lain', '', '3', GETDATE(), GETDATE());


--Insert JBUKTI
delete from JBUKTI where KDBUKTI in ('09','10','11','12','13')

INSERT [dbo].[JBUKTI] ([KDBUKTI], [NMBUKTI], [KET], [DATECREATE], [DATEUPDATE]) 
VALUES (N'09', N'BAST UU', N'Ketentuan Perundang-undangan', GETDATE(), GETDATE())

INSERT [dbo].[JBUKTI] ([KDBUKTI], [NMBUKTI], [KET], [DATECREATE], [DATEUPDATE]) 
VALUES (N'10', N'BAST Putusan', N'Putusan Pengadilan', GETDATE(), GETDATE())

INSERT [dbo].[JBUKTI] ([KDBUKTI], [NMBUKTI], [KET], [DATECREATE], [DATEUPDATE]) 
VALUES (N'11', N'BAST Divestasi', N'Divestasi', GETDATE(), GETDATE())

INSERT [dbo].[JBUKTI] ([KDBUKTI], [NMBUKTI], [KET], [DATECREATE], [DATEUPDATE]) 
VALUES (N'12', N'BAST Pembatalan', N'Pembatalan Penghapusan', GETDATE(), GETDATE())

INSERT [dbo].[JBUKTI] ([KDBUKTI], [NMBUKTI], [KET], [DATECREATE], [DATEUPDATE]) 
VALUES (N'13', N'BAST Lainnya', N'Perolehan/Penerimaan Lainnya', GETDATE(), GETDATE())
	


--ASET_KIBSPESIFIKASI

ALTER TABLE [dbo].[ASET_KIBSPESIFIKASI] DROP COLUMN UTARA 
GO
ALTER TABLE [dbo].[ASET_KIBSPESIFIKASI] DROP COLUMN TIMUR 
GO
ALTER TABLE [dbo].[ASET_KIBSPESIFIKASI] DROP COLUMN SELATAN 
GO
ALTER TABLE [dbo].[ASET_KIBSPESIFIKASI] DROP COLUMN BARAT 
GO
ALTER TABLE [dbo].[ASET_KIBSPESIFIKASI] DROP COLUMN LOKASI 
GO

--USE [V@LID49ASETV5]
--GO
ALTER TABLE [dbo].[ASET_KIBSPESIFIKASI] ADD UTARA nvarchar(200)
GO
ALTER TABLE [dbo].[ASET_KIBSPESIFIKASI] ADD TIMUR nvarchar(200)
GO
ALTER TABLE [dbo].[ASET_KIBSPESIFIKASI] ADD SELATAN nvarchar(200)
GO
ALTER TABLE [dbo].[ASET_KIBSPESIFIKASI] ADD BARAT nvarchar(200)
GO
ALTER TABLE [dbo].[ASET_KIBSPESIFIKASI] ADD LOKASI nvarchar(200)
GO
