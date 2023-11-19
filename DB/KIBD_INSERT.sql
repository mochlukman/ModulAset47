USE [V@LID49ASETV5]
GO
/****** Object:  StoredProcedure [dbo].[KIBD_INSERT]    Script Date: 07/10/2022 21:46:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[KIBD_INSERT]
(@UNITKEY CHAR(36), @ASETKEY CHAR(15), @TAHUN CHAR(5), @KDPEMILIK CHAR(2), @ASALUSUL VARCHAR(512), @PENGGUNA VARCHAR(254), @KET VARCHAR(4000), 
@KDKON CHAR(2), @KDHAK VARCHAR(2), @ALAMAT VARCHAR(512), @KDSATUAN CHAR(2), @KONSTRUKSI VARCHAR(50), @PANJANG VARCHAR(50), @LEBAR VARCHAR(50), 
@LUAS VARCHAR(50), @NODOKJIJ VARCHAR(30), @TGDOKJIJ DATETIME, @NOKDTANAH VARCHAR(30), 
@NOBA VARCHAR(50), @TGLBA DATETIME, @KDTANS CHAR(3), @JUMLAH INT, @NILAI VARCHAR(50), @NILINSERT CHAR(1), 
@LOKASI VARCHAR(200))
AS

--------------------------------------------------------------------------------------------------------------------------------------------
-- GET UMEKO
DECLARE @UMEKO DECIMAL
SET @UMEKO = (SELECT ISNULL(UMEKO,0) FROM DAFTASET WHERE ASETKEY = @ASETKEY)

-- SET NOBA
IF (@KDTANS = '000')
BEGIN	
	SET @NOBA = '000/SA/'+RTRIM(YEAR(@TGLBA))+'.'+RTRIM(MONTH(@TGLBA))+'.'+RTRIM(DAY(@TGLBA))
END

-- VALIDASI JUMLAH
IF (@JUMLAH < 1)
BEGIN
	SET @JUMLAH = 1
END

-- PILIHAN NILAI TOTAL ATAU SATUAN
SET @NILAI = REPLACE(@NILAI,',','.')

IF(@NILINSERT = '1')
BEGIN
	SET @NILAI = CAST(@NILAI AS MONEY) / @JUMLAH
END
ELSE IF(@NILINSERT = '2')
BEGIN
	SET @NILAI = @NILAI
END

-- KONVERSI KE DECIMAL
SET @PANJANG	= REPLACE(@PANJANG,',','.')
SET @LEBAR		= REPLACE(@LEBAR,',','.')
SET @LUAS		= REPLACE(@LUAS,',','.')

-- GET KDKLAS
DECLARE @KDKLAS CHAR(2), @NKLAS MONEY
SET @NKLAS = (SELECT NKLAS FROM DAFTASET WHERE ASETKEY = @ASETKEY)

IF (@NILAI >= @NKLAS)
BEGIN
	SET @KDKLAS = '01'
END
ELSE
BEGIN
	SET @KDKLAS = '02'		
END

-- GET KDLOKPO, KDBRGPO
DECLARE @KDPEMDA CHAR(6), @KDLOKSATKER CHAR(3), @KDLOKPO VARCHAR(50), @KDBRGPO VARCHAR(50)
SET @KDPEMDA		= (SELECT CONFIGVAL FROM PEMDA WHERE CONFIGID='pemda_kd')
SET @KDLOKSATKER	= (SELECT CASE WHEN LEN(KDUNIT) <= 8 THEN '00.' ELSE SUBSTRING(KDUNIT,9,3) END FROM DAFTUNIT WHERE UNITKEY = @UNITKEY)
SET @KDLOKPO		= (SELECT @KDPEMILIK+'.'+@KDPEMDA+LEFT(KDUNIT,8)+RTRIM(RIGHT(@TAHUN,2))+'.'+@KDLOKSATKER FROM DAFTUNIT WHERE UNITKEY = @UNITKEY)

-- GET MAXIMUM NOMOR REGISTER DAN IDBRG
DECLARE @MAXIDBRG INT, @IDBRG VARCHAR(10), @MAXNOREG INT, @NOREG VARCHAR(10)
SET @MAXNOREG = (SELECT ISNULL(MAX(CAST((CASE WHEN LEFT(NOREG,1) IN ('0','1','2','3','4','5','6','7','8','9') THEN NOREG ELSE 
							RTRIM(CAST((ASCII(LEFT(NOREG,1))-55) AS CHAR(2)))+RIGHT(RTRIM(CAST(NOREG AS CHAR(10))),5)
							END) AS INT)),0) FROM ASET_KIBDDET
					WHERE UNITKEY = @UNITKEY AND ASETKEY = @ASETKEY AND TAHUN = @TAHUN)
SET @MAXIDBRG = (SELECT ISNULL(MAX(CAST(IDBRG AS INT)),0) FROM ASET_KIB) 

--------------------------------------------------------------------------------------------------------------------------------------------
SET XACT_ABORT ON
BEGIN TRANSACTION

DECLARE @I INT
SET @I = 1

WHILE @I <= @JUMLAH
BEGIN
	SET @IDBRG = @MAXIDBRG+@I
	SET @NOREG = (SELECT dbo.fn_GetNoreg(@MAXNOREG+@I))
	SET @KDBRGPO = (SELECT '04'+'.'+LTRIM(RTRIM(KDASET))+@NOREG FROM DAFTASET WHERE ASETKEY = @ASETKEY)
	
	INSERT INTO ASET_KIB(IDBRG,UNITKEY,ASETKEY,TAHUN,NOREG,DOKPEROLEHAN,TGLPEROLEHAN,URUTBRG,KDKIB,STATUSPENGGUNA)
	SELECT @IDBRG, @UNITKEY, @ASETKEY, @TAHUN, @NOREG, @NOBA, @TGLBA, 1, '04', '1'
	
	INSERT INTO ASET_KIBDDET(IDBRG,URUTTRANS,UNITKEY,ASETKEY,TAHUN,NOREG,KDTANS,NODOKUMEN,TGLDOKUMEN,NILAI,UMEKO,KDKIB,
	KET,KDKON,KDKLAS,KDSENSUS,KDSTATUSASET,KDLOKPO,KDBRGPO,THANG)
	SELECT @IDBRG, 1 URUTTRANS, @UNITKEY, @ASETKEY, @TAHUN, @NOREG, @KDTANS, @NOBA, @TGLBA, @NILAI, @UMEKO, '04' KDKIB, 
	@KET, @KDKON, @KDKLAS, NULL KDSENSUS, '01' KDSTATUSASET, @KDLOKPO, @KDBRGPO, YEAR(@TGLBA) THANG
	
	INSERT INTO ASET_KIBSPESIFIKASI(IDBRG,URUTHIST,UNITKEY,ASETKEY,TAHUN,NOREG,KDKIB,KDPEMILIK,ASALUSUL,PENGGUNA,KET,KDHAK,
		ALAMAT,KDSATUAN,KONSTRUKSI,PANJANG,LEBAR,LUAS,NODOKJIJ,TGDOKJIJ,NOKDTANAH, LOKASI)
	SELECT @IDBRG, 1, @UNITKEY, @ASETKEY, @TAHUN, @NOREG, '04', @KDPEMILIK, @ASALUSUL, @PENGGUNA, @KET, @KDHAK, 
		@ALAMAT, @KDSATUAN, @KONSTRUKSI, @PANJANG, @LEBAR, @LUAS, @NODOKJIJ, @TGDOKJIJ, @NOKDTANAH, @LOKASI

	SET @I = @I + 1
END

COMMIT TRANSACTION