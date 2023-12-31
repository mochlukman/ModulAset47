
GO
/****** Object:  StoredProcedure [dbo].[WSPX_HAPUSSK]    Script Date: 02/11/2023 16:38:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[WSPX_HAPUSSK]
(@UNITKEY CHAR(36), @NOSKHAPUS VARCHAR(50), @TGLSKHAPUS DATETIME, @KDTANS CHAR(3))
AS

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--DECLARE @UNITKEY CHAR(36), @NOSKHAPUS VARCHAR(50), @TGLSKHAPUS DATETIME
--SET @UNITKEY = '136_'
--SET @NOSKHAPUS = '34567'
--SET @TGLSKHAPUS = '2021-10-24'

--IF EXISTS (SELECT NAME FROM TEMPDB..SYSOBJECTS  WHERE NAME LIKE '#KIBHAPUSSK%')
--   DROP TABLE #KIBHAPUSSK

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

SELECT IDENTITY(INT,1,1) NOURUT, A.UNITKEY, A.NOSKHAPUS, AH.TGLSKHAPUS, A.KDTANS, AH.KET AS URAITRANS, A.ASETKEY,(CASE WHEN @KDTANS='201' THEN B.ASETKEY2 ELSE A.ASETKEY END) ASETKEY2, A.TAHUN, A.NOREG, A.IDBRG, A.NILAI, DA.UMEKO, J.KDKIB
INTO #KIBHAPUSSK
FROM ASET_HAPUSSKDET A
LEFT JOIN ASET_HAPUSSK AH ON AH.UNITKEY = A.UNITKEY AND AH.NOSKHAPUS = A.NOSKHAPUS
LEFT JOIN DAFTASET DA ON DA.ASETKEY = A.ASETKEY
LEFT JOIN JNSKIBASET J ON J.K_BRG = LEFT(DA.KDASET,9)
LEFT JOIN ASET_REKLASDET AS B ON A.UNITKEY = B.UNITKEY AND B.ASETKEY = A.ASETKEY AND B.TAHUN = A.TAHUN AND B.NOREG = A.NOREG AND B.IDBRG = A.IDBRG	
WHERE A.UNITKEY = @UNITKEY AND A.NOSKHAPUS = @NOSKHAPUS

--SELECT * FROM #KIBHAPUSSK 
--RETURN

SET XACT_ABORT ON
BEGIN TRANSACTION

DECLARE @NROW AS INT, @I INT, @KDKIB CHAR(2), @MAXURUTTRANS INT
SET @NROW = (SELECT MAX(NOURUT) FROM #KIBHAPUSSK)
SET @I = 1

WHILE @I <= @NROW
BEGIN
	SET @KDKIB = (SELECT K.KDKIB FROM #KIBHAPUSSK K WHERE K.NOURUT = @I)
	
	IF (@KDKIB = '01')
	BEGIN
		SET @MAXURUTTRANS	= (SELECT MAX(CAST(A.URUTTRANS AS INT)) FROM ASET_KIBADET A 
							   INNER JOIN #KIBHAPUSSK K ON K.IDBRG=A.IDBRG 
							   WHERE K.NOURUT = @I)
							  
		INSERT INTO ASET_KIBADET(IDBRG,URUTTRANS,UNITKEY,ASETKEY,TAHUN,NOREG,KDTANS,NODOKUMEN,TGLDOKUMEN,NILAI,UMEKO,KDKIB,KET,KDKON,KDKLAS,KDSENSUS,KDSTATUSASET,KDLOKPO,KDBRGPO,THANG)
		SELECT K.IDBRG, @MAXURUTTRANS+1, K.UNITKEY, K.ASETKEY2, A.TAHUN, A.NOREG, K.KDTANS, K.NOSKHAPUS, K.TGLSKHAPUS, K.NILAI, CASE WHEN K.KDTANS = '206' THEN 0 ELSE K.UMEKO END, 
		A.KDKIB, K.URAITRANS, A.KDKON, A.KDKLAS, A.KDSENSUS, A.KDSTATUSASET, A.KDLOKPO, A.KDBRGPO, YEAR(@TGLSKHAPUS)
		FROM #KIBHAPUSSK K
		INNER JOIN ASET_KIBADET A ON A.IDBRG = K.IDBRG AND A.URUTTRANS = @MAXURUTTRANS
		WHERE K.NOURUT = @I
	END
	ELSE IF (@KDKIB = '02')
	BEGIN
		SET @MAXURUTTRANS	= (SELECT MAX(CAST(A.URUTTRANS AS INT)) FROM ASET_KIBBDET A 
							   INNER JOIN #KIBHAPUSSK K ON K.IDBRG=A.IDBRG 
							   WHERE K.NOURUT = @I)
							  
		INSERT INTO ASET_KIBBDET(IDBRG,URUTTRANS,UNITKEY,ASETKEY,TAHUN,NOREG,KDTANS,NODOKUMEN,TGLDOKUMEN,NILAI,UMEKO,KDKIB,KET,KDKON,KDKLAS,KDSENSUS,KDSTATUSASET,KDLOKPO,KDBRGPO,THANG)
		SELECT K.IDBRG, @MAXURUTTRANS+1, K.UNITKEY, K.ASETKEY2, A.TAHUN, A.NOREG, K.KDTANS, K.NOSKHAPUS, K.TGLSKHAPUS, K.NILAI, CASE WHEN K.KDTANS = '206' THEN 0 ELSE K.UMEKO END, 
		A.KDKIB, K.URAITRANS, A.KDKON, A.KDKLAS, A.KDSENSUS, A.KDSTATUSASET, A.KDLOKPO, A.KDBRGPO, YEAR(@TGLSKHAPUS)
		FROM #KIBHAPUSSK K
		INNER JOIN ASET_KIBBDET A ON A.IDBRG = K.IDBRG AND A.URUTTRANS = @MAXURUTTRANS
		WHERE K.NOURUT = @I
	END
	ELSE IF (@KDKIB = '03')
	BEGIN
		SET @MAXURUTTRANS	= (SELECT MAX(CAST(A.URUTTRANS AS INT)) FROM ASET_KIBCDET A 
							   INNER JOIN #KIBHAPUSSK K ON K.IDBRG=A.IDBRG 
							   WHERE K.NOURUT = @I)
							  
		INSERT INTO ASET_KIBCDET(IDBRG,URUTTRANS,UNITKEY,ASETKEY,TAHUN,NOREG,KDTANS,NODOKUMEN,TGLDOKUMEN,NILAI,UMEKO,KDKIB,KET,KDKON,KDKLAS,KDSENSUS,KDSTATUSASET,KDLOKPO,KDBRGPO,THANG)
		SELECT K.IDBRG, @MAXURUTTRANS+1, K.UNITKEY, K.ASETKEY2, A.TAHUN, A.NOREG, K.KDTANS, K.NOSKHAPUS, K.TGLSKHAPUS, K.NILAI, CASE WHEN K.KDTANS = '206' THEN 0 ELSE K.UMEKO END, 
		A.KDKIB, K.URAITRANS, A.KDKON, A.KDKLAS, A.KDSENSUS, A.KDSTATUSASET, A.KDLOKPO, A.KDBRGPO, YEAR(@TGLSKHAPUS)
		FROM #KIBHAPUSSK K
		INNER JOIN ASET_KIBCDET A ON A.IDBRG = K.IDBRG AND A.URUTTRANS = @MAXURUTTRANS
		WHERE K.NOURUT = @I
	END
	ELSE IF (@KDKIB = '04')
	BEGIN
		SET @MAXURUTTRANS	= (SELECT MAX(CAST(A.URUTTRANS AS INT)) FROM ASET_KIBDDET A 
							   INNER JOIN #KIBHAPUSSK K ON K.IDBRG=A.IDBRG 
							   WHERE K.NOURUT = @I)
							  
		INSERT INTO ASET_KIBDDET(IDBRG,URUTTRANS,UNITKEY,ASETKEY,TAHUN,NOREG,KDTANS,NODOKUMEN,TGLDOKUMEN,NILAI,UMEKO,KDKIB,KET,KDKON,KDKLAS,KDSENSUS,KDSTATUSASET,KDLOKPO,KDBRGPO,THANG)
		SELECT K.IDBRG, @MAXURUTTRANS+1, K.UNITKEY, K.ASETKEY2, A.TAHUN, A.NOREG, K.KDTANS, K.NOSKHAPUS, K.TGLSKHAPUS, K.NILAI, CASE WHEN K.KDTANS = '206' THEN 0 ELSE K.UMEKO END, 
		A.KDKIB, K.URAITRANS, A.KDKON, A.KDKLAS, A.KDSENSUS, A.KDSTATUSASET, A.KDLOKPO, A.KDBRGPO, YEAR(@TGLSKHAPUS)
		FROM #KIBHAPUSSK K
		INNER JOIN ASET_KIBDDET A ON A.IDBRG = K.IDBRG AND A.URUTTRANS = @MAXURUTTRANS
		WHERE K.NOURUT = @I
	END
	ELSE IF (@KDKIB = '05')
	BEGIN
		SET @MAXURUTTRANS	= (SELECT MAX(CAST(A.URUTTRANS AS INT)) FROM ASET_KIBEDET A 
							   INNER JOIN #KIBHAPUSSK K ON K.IDBRG=A.IDBRG 
							   WHERE K.NOURUT = @I)
							  
		INSERT INTO ASET_KIBEDET(IDBRG,URUTTRANS,UNITKEY,ASETKEY,TAHUN,NOREG,KDTANS,NODOKUMEN,TGLDOKUMEN,NILAI,UMEKO,KDKIB,KET,KDKON,KDKLAS,KDSENSUS,KDSTATUSASET,KDLOKPO,KDBRGPO,THANG)
		SELECT K.IDBRG, @MAXURUTTRANS+1, K.UNITKEY, K.ASETKEY2, A.TAHUN, A.NOREG, K.KDTANS, K.NOSKHAPUS, K.TGLSKHAPUS, K.NILAI, CASE WHEN K.KDTANS = '206' THEN 0 ELSE K.UMEKO END, 
		A.KDKIB, K.URAITRANS, A.KDKON, A.KDKLAS, A.KDSENSUS, A.KDSTATUSASET, A.KDLOKPO, A.KDBRGPO, YEAR(@TGLSKHAPUS)
		FROM #KIBHAPUSSK K
		INNER JOIN ASET_KIBEDET A ON A.IDBRG = K.IDBRG AND A.URUTTRANS = @MAXURUTTRANS
		WHERE K.NOURUT = @I
	END
	ELSE IF (@KDKIB = '07')
	BEGIN
		SET @MAXURUTTRANS	= (SELECT MAX(CAST(A.URUTTRANS AS INT)) FROM ASET_KIBGDET A 
							   INNER JOIN #KIBHAPUSSK K ON K.IDBRG=A.IDBRG 
							   WHERE K.NOURUT = @I)
							  
		INSERT INTO ASET_KIBGDET(IDBRG,URUTTRANS,UNITKEY,ASETKEY,TAHUN,NOREG,KDTANS,NODOKUMEN,TGLDOKUMEN,NILAI,UMEKO,KDKIB,KET,KDKON,KDKLAS,KDSENSUS,KDSTATUSASET,KDLOKPO,KDBRGPO,THANG)
		SELECT K.IDBRG, @MAXURUTTRANS+1, K.UNITKEY, K.ASETKEY2, A.TAHUN, A.NOREG, K.KDTANS, K.NOSKHAPUS, K.TGLSKHAPUS, K.NILAI, CASE WHEN K.KDTANS = '206' THEN 0 ELSE K.UMEKO END, 
		A.KDKIB, K.URAITRANS, A.KDKON, A.KDKLAS, A.KDSENSUS, A.KDSTATUSASET, A.KDLOKPO, A.KDBRGPO, YEAR(@TGLSKHAPUS)
		FROM #KIBHAPUSSK K
		INNER JOIN ASET_KIBGDET A ON A.IDBRG = K.IDBRG AND A.URUTTRANS = @MAXURUTTRANS
		WHERE K.NOURUT = @I
	END
	
	SET @I = @I + 1
END

COMMIT TRANSACTION

-- UPDATE TGLVALID
UPDATE ASET_HAPUSSK
SET TGLVALID = @TGLSKHAPUS
WHERE UNITKEY = @UNITKEY AND NOSKHAPUS = @NOSKHAPUS
