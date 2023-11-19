
/****** Object:  StoredProcedure [dbo].[WSP_VALBERITA]    Script Date: 07/09/2023 07:20:41 ******/
DROP PROCEDURE [dbo].[WSP_VALBERITA]
GO

/****** Object:  StoredProcedure [dbo].[WSP_VALBERITA]    Script Date: 07/09/2023 07:20:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[WSP_VALBERITA]
(@UNITKEY CHAR(36), @NOBA VARCHAR(50), @TGLBA DATETIME, @TGLVALID DATETIME, @KDTANS CHAR(3))
AS

--------------------------------------------------------------------------------------------------------------------------------------------
--DECLARE @UNITKEY CHAR(36), @NOBA VARCHAR(50), @TGLBA DATETIME, @TGLVALID DATETIME, @KDTANS CHAR(3)
--SET @UNITKEY = '353_'
--SET @NOBA = 'SK NOMOR 14/HUK/2020'
--SET @TGLBA = '2021-01-05'
--SET @TGLVALID = '2021-01-05'
--SET @KDTANS = '102'

--if exists (select name from tempdb..sysobjects  where name like '#ASET_BERITADETBRG%')
--  drop table #ASET_BERITADETBRG

--------------------------------------------------------------------------------------------------------------------------------------------
SET XACT_ABORT ON
BEGIN TRANSACTION

DECLARE @MAXIDBRG INT, @IDBRG VARCHAR(10), @MAXNOREG INT, @NOREG VARCHAR(10), @TAHUN CHAR(4), @ASETKEY CHAR(15), @KDKIB CHAR(2), @KDBRGPO VARCHAR(50), @URUTBRG INT

DECLARE @COUNTER INT

IF (@KDTANS = '111') -- TRANSAKSI BAKF(KDP) INSERT KIBFDET
BEGIN
	--DECLARE @COUNTER INT
	
	SET @COUNTER = 1
	WHILE @COUNTER <= (SELECT COUNT(*) FROM ASET_BERITADETBRG WHERE UNITKEY=@UNITKEY AND NOBA=@NOBA)
	BEGIN
		SET @URUTBRG	= (SELECT URUTBRG FROM ASET_BERITADETBRG WHERE UNITKEY=@UNITKEY AND NOBA=@NOBA AND URUTBRG=@COUNTER)
		SET @ASETKEY	= (SELECT ASETKEY FROM ASET_BERITADETBRG WHERE UNITKEY=@UNITKEY AND NOBA=@NOBA AND URUTBRG=@COUNTER)
		SET @TAHUN		= (SELECT TAHUN FROM ASET_BERITADETBRG WHERE UNITKEY=@UNITKEY AND NOBA=@NOBA AND URUTBRG=@COUNTER)
		SET @MAXIDBRG	= (SELECT ISNULL(MAX(CAST(IDBRG AS INT)),0) FROM ASET_KIB) 
		SET @IDBRG		= @MAXIDBRG+1
		SET @MAXNOREG	= (SELECT ISNULL(MAX(CAST((CASE WHEN LEFT(NOREG,1) IN ('0','1','2','3','4','5','6','7','8','9') THEN NOREG ELSE 
						  RTRIM(CAST((ASCII(LEFT(NOREG,1))-55) AS CHAR(2)))+RIGHT(RTRIM(CAST(NOREG AS CHAR(10))),3) 
		             	  END) AS INT)),0) FROM ASET_KIB
						  WHERE UNITKEY = @UNITKEY AND ASETKEY = @ASETKEY AND TAHUN = @TAHUN)
		SET @NOREG		= (SELECT dbo.fn_GetNoreg(@MAXNOREG+1))
		SET @KDBRGPO	= (SELECT '06.'+LTRIM(RTRIM(KDASET))+@NOREG FROM DAFTASET WHERE ASETKEY = @ASETKEY)
		
		INSERT INTO ASET_KIB(IDBRG,UNITKEY,ASETKEY,TAHUN,NOREG,DOKPEROLEHAN,TGLPEROLEHAN,URUTBRG,KDKIB,STATUSPENGGUNA)			
		SELECT @IDBRG, @UNITKEY, @ASETKEY, @TAHUN, @NOREG, @NOBA, @TGLBA, @URUTBRG, '06', '1'
		
		INSERT INTO ASET_KIBFDET(IDBRG,URUTTRANS,UNITKEY,ASETKEY,TAHUN,NOREG,KDTANS,NODOKUMEN,TGLDOKUMEN,IDTERMIN,JUMLAH,NILAI,PROSENFISIK,PROSENBIAYA,KDLOKPO,KDBRGPO,KET,THANG)
		SELECT @IDBRG, 1, @UNITKEY, @ASETKEY, @TAHUN, @NOREG, @KDTANS, @NOBA, @TGLBA, 1, A.JUMLAH, A.NILAI, A.PROSENFISIK, A.PROSENBIAYA, 
		A.KDPEMILIK+'.'+LEFT(D.KDUNIT,8)+RTRIM(RIGHT(A.TAHUN,2)) KDLOKPO, @KDBRGPO, AB.URAIBA, YEAR(@TGLBA)
		FROM ASET_BERITADETBRG A
		LEFT JOIN ASET_BERITA AB ON AB.UNITKEY = A.UNITKEY AND AB.NOBA = A.NOBA
		LEFT JOIN DAFTUNIT D ON D.UNITKEY = A.UNITKEY
		WHERE A.UNITKEY=@UNITKEY AND A.NOBA=@NOBA AND A.URUTBRG=@COUNTER
			
		INSERT INTO ASET_KIBSPESIFIKASI(IDBRG,URUTHIST,UNITKEY,ASETKEY,TAHUN,NOREG,KDKIB,KDPEMILIK,ASALUSUL,KET,KDHAK,ALAMAT,KDSATUAN,BERTINGKAT,BETON,LUASLT,KDFISIK,NODOKKDP,TGDOKKDP,TGMULAI,NOKDTANAH)
		SELECT @IDBRG, 1, @UNITKEY, @ASETKEY, @TAHUN, @NOREG, '06', A.KDPEMILIK, A.ASALUSUL, A.KET, A.KDHAK, A.ALAMAT, A.KDSATUAN, A.BERTINGKAT, A.BETON, A.LUASLT, 
		A.KDFISIK, A.NODOKKDP, A.TGDOKKDP, A.TGMULAI, A.NOKDTANAH
		FROM ASET_BERITADETBRG A
		WHERE A.UNITKEY=@UNITKEY AND A.NOBA=@NOBA AND A.URUTBRG=@COUNTER
		
		SET @COUNTER = @COUNTER+1
	END
END

ELSE -- TRANSAKSI  BAKF dan Persediaan 

BEGIN
	DECLARE @I INT, @J INT, @JMLBRG INT,@count INT
	
	SELECT IDENTITY(INT,1,1) AS ROWNUMB, A.JUMLAH,A.URUTBRG,A.ASETKEY,A.TAHUN, J.KDKIB
	INTO #ASET_BERITADETBRG
	  FROM ASET_BERITADETBRG A
	INNER JOIN DAFTASET D ON D.ASETKEY = A.ASETKEY
	INNER JOIN JNSKIBASET J ON J.K_BRG = LEFT(D.KDASET,9)
	WHERE A.UNITKEY=@UNITKEY AND A.NOBA=@NOBA
	
	SET @count = ISNULL((SELECT COUNT(*) FROM #ASET_BERITADETBRG),0)
	SET @I = 1
	WHILE @I <= @count--(SELECT SUM(JUMLAH) FROM ASET_BERITADETBRG WHERE UNITKEY=@UNITKEY AND NOBA=@NOBA)
	BEGIN
		SET @JMLBRG		= (SELECT JUMLAH FROM #ASET_BERITADETBRG WHERE ROWNUMB = @I)
		SET @URUTBRG	= (SELECT URUTBRG FROM #ASET_BERITADETBRG WHERE ROWNUMB = @I)
		SET @ASETKEY	= (SELECT ASETKEY FROM #ASET_BERITADETBRG WHERE ROWNUMB = @I)
		SET @TAHUN		= (SELECT TAHUN FROM #ASET_BERITADETBRG WHERE ROWNUMB = @I)
		SET @KDKIB		= (SELECT KDKIB FROM #ASET_BERITADETBRG WHERE ROWNUMB = @I)
			
			
		IF (@KDTANS IN('102','103','107') AND @TAHUN <> YEAR(@TGLBA)) -- TGLPEROLEHAN UNTUK TRANSAKSI HIBAH, TUKAR MENUKAR, INVENTARISASI
		BEGIN
			SET @TGLBA = (SELECT CAST(@TAHUN+'/'+RTRIM(MONTH(@TGLBA))+'/'+RTRIM(DAY(@TGLBA)) AS DATETIME))
		END	
						
		SET @J = 1
		WHILE @J <= @JMLBRG
		BEGIN
			SET @MAXNOREG	= (SELECT ISNULL(MAX(CAST((CASE WHEN LEFT(NOREG,1) IN ('0','1','2','3','4','5','6','7','8','9') THEN NOREG ELSE 
							  RTRIM(CAST((ASCII(LEFT(NOREG,1))-55) AS CHAR(2)))+RIGHT(RTRIM(CAST(NOREG AS CHAR(10))),5)
							  END) AS INT)),0) FROM ASET_KIB
							  WHERE UNITKEY = @UNITKEY AND ASETKEY = @ASETKEY AND TAHUN = @TAHUN)
						  
			SET @MAXIDBRG	= (SELECT ISNULL(MAX(CAST(IDBRG AS INT)),0) FROM ASET_KIB) 
			SET @IDBRG		= @MAXIDBRG+1
			SET @NOREG		= (SELECT dbo.fn_GetNoreg(@MAXNOREG+1))
			SET @KDBRGPO	= (SELECT '00.'+LTRIM(RTRIM(KDASET))+@NOREG FROM DAFTASET WHERE ASETKEY = @ASETKEY)

			INSERT INTO ASET_KIB(IDBRG,UNITKEY,ASETKEY,TAHUN,NOREG,DOKPEROLEHAN,TGLPEROLEHAN,URUTBRG,KDKIB,STATUSPENGGUNA)			
			SELECT @IDBRG, @UNITKEY, @ASETKEY, @TAHUN, @NOREG, @NOBA, @TGLBA, @URUTBRG, @KDKIB, '0'

			IF (@KDKIB='00')
			BEGIN

			INSERT INTO ASET_KIBHDET(IDBRG,URUTTRANS,UNITKEY,ASETKEY,TAHUN,NOREG,KDTANS,NODOKUMEN,TGLDOKUMEN,NILAI,UMEKO,KDKIB,KDKON,KDSTATUSASET,KDLOKPO,KDBRGPO,KET,THANG)
			SELECT @IDBRG, 1, @UNITKEY, @ASETKEY, @TAHUN, @NOREG, @KDTANS, @NOBA, @TGLBA, A.NILAI,0,'00',A.KDKON, '10', 
			A.KDPEMILIK+'.'+LEFT(D.KDUNIT,8)+RTRIM(RIGHT(A.TAHUN,2)) KDLOKPO, @KDBRGPO, AB.URAIBA, YEAR(@TGLBA)
			FROM ASET_BERITADETBRG A
			LEFT JOIN ASET_BERITA AB ON AB.UNITKEY = A.UNITKEY AND AB.NOBA = A.NOBA
			LEFT JOIN DAFTUNIT D ON D.UNITKEY = A.UNITKEY
			WHERE A.UNITKEY=@UNITKEY AND A.NOBA=@NOBA AND A.URUTBRG=@count
			

			INSERT INTO ASET_KIBSPESIFIKASI(IDBRG,URUTHIST,UNITKEY,ASETKEY,TAHUN,NOREG,KDKIB,KDPEMILIK,ASALUSUL,KET,KDHAK,ALAMAT,KDSATUAN,BERTINGKAT,BETON,LUASLT,KDFISIK,NODOKKDP,TGDOKKDP,TGMULAI,NOKDTANAH)
			SELECT @IDBRG, 1, @UNITKEY, @ASETKEY, @TAHUN, @NOREG, '00', A.KDPEMILIK, A.ASALUSUL, A.KET, A.KDHAK, A.ALAMAT, A.KDSATUAN, A.BERTINGKAT, A.BETON, A.LUASLT, 
			A.KDFISIK, A.NODOKKDP, A.TGDOKKDP, A.TGMULAI, A.NOKDTANAH
			FROM ASET_BERITADETBRG A
			WHERE A.UNITKEY=@UNITKEY AND A.NOBA=@NOBA AND A.URUTBRG=@count

			END
		
		
			SET @J = @J+1
		END
		SET @I = @I + 1
	END
END
COMMIT TRANSACTION

UPDATE ASET_BERITA SET TGLVALID = @TGLVALID WHERE UNITKEY=@UNITKEY AND NOBA=@NOBA

GO


