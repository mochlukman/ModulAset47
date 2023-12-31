

--exec [dbo].[WSPV_REKLASDET]
--		    @UNITKEY = N'345_                                ',
--		    @KDASET = N'1.5.4.01.01.01.'

ALTER PROCEDURE [dbo].[WSPV_REKLASDET]
(@UNITKEY CHAR(36), @KDASET VARCHAR(15))
AS

-------------------------------------------------------------------------------------------------------------------------------
--DECLARE @UNITKEY CHAR(36), @KDASET VARCHAR(15)
--SET @UNITKEY = '1726_'
--SET @KDASET = '1.5.4.01.01.01.'

--IF EXISTS (SELECT NAME FROM TEMPDB..SYSOBJECTS  WHERE NAME LIKE '#_KIB%')
--   DROP TABLE #_KIB

-------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE #_KIB (IDBRG CHAR(10),UNITKEY CHAR(36),ASETKEY CHAR(15),URUTTRANS CHAR(10),NILAI MONEY,KDKON CHAR(2),KDKLAS CHAR(2),KDSTATUSASET CHAR(2))

DECLARE @ASETKEY VARCHAR(9), @KDKIB CHAR(2)
SET @ASETKEY = (REPLACE(@KDASET,'.',''))
SET @KDKIB = (SELECT J.KDKIB FROM JNSKIBASET J WHERE J.ASETKEY=LEFT(@ASETKEY,5))



IF (@KDKIB = '01')
BEGIN
	INSERT INTO #_KIB
	SELECT X.IDBRG, X.UNITKEY, X.ASETKEY, X.URUTTRANS, X.NILAI, B.KDKON, B.KDKLAS, B.KDSTATUSASET
	FROM (
	SELECT A.IDBRG, A.UNITKEY, A.ASETKEY, MAX(CAST(A.URUTTRANS AS INT)) URUTTRANS, SUM((CASE WHEN LEFT(A.KDTANS,1)=2 THEN -1*A.NILAI ELSE A.NILAI END)) AS NILAI
	FROM ASET_KIBADET A WHERE A.UNITKEY=@UNITKEY  
	AND LEFT(A.ASETKEY,5)=@ASETKEY  
	GROUP BY A.IDBRG, A.UNITKEY, A.ASETKEY ) X
	LEFT JOIN ASET_KIBADET B ON B.IDBRG = X.IDBRG AND B.URUTTRANS = X.URUTTRANS AND B.UNITKEY = X.UNITKEY AND B.ASETKEY = X.ASETKEY
END
ELSE IF (@KDKIB = '02')
BEGIN
	INSERT INTO #_KIB
	SELECT X.IDBRG, X.UNITKEY, X.ASETKEY, X.URUTTRANS, X.NILAI, B.KDKON, B.KDKLAS, B.KDSTATUSASET
	FROM (
	SELECT A.IDBRG, A.UNITKEY, A.ASETKEY, MAX(CAST(A.URUTTRANS AS INT)) URUTTRANS, SUM((CASE WHEN LEFT(A.KDTANS,1)=2 THEN -1*A.NILAI ELSE A.NILAI END)) AS NILAI
	FROM ASET_KIBBDET A WHERE A.UNITKEY=@UNITKEY 
	AND LEFT(A.ASETKEY,5)=@ASETKEY   
	GROUP BY A.IDBRG, A.UNITKEY, A.ASETKEY ) X
	LEFT JOIN ASET_KIBBDET B ON B.IDBRG = X.IDBRG AND B.URUTTRANS = X.URUTTRANS AND B.UNITKEY = X.UNITKEY AND B.ASETKEY = X.ASETKEY
END
ELSE IF (@KDKIB = '03')
BEGIN
	INSERT INTO #_KIB
	SELECT X.IDBRG, X.UNITKEY, X.ASETKEY, X.URUTTRANS, X.NILAI, B.KDKON, B.KDKLAS, B.KDSTATUSASET
	FROM (
	SELECT A.IDBRG, A.UNITKEY, A.ASETKEY, MAX(CAST(A.URUTTRANS AS INT)) URUTTRANS, SUM((CASE WHEN LEFT(A.KDTANS,1)=2 THEN -1*A.NILAI ELSE A.NILAI END)) AS NILAI
	FROM ASET_KIBCDET A WHERE A.UNITKEY=@UNITKEY 
	AND LEFT(A.ASETKEY,5)=@ASETKEY   
	GROUP BY A.IDBRG, A.UNITKEY, A.ASETKEY ) X
	LEFT JOIN ASET_KIBCDET B ON B.IDBRG = X.IDBRG AND B.URUTTRANS = X.URUTTRANS AND B.UNITKEY = X.UNITKEY AND B.ASETKEY = X.ASETKEY
END
ELSE IF (@KDKIB = '04')
BEGIN
	INSERT INTO #_KIB
	SELECT X.IDBRG, X.UNITKEY, X.ASETKEY, X.URUTTRANS, X.NILAI, B.KDKON, B.KDKLAS, B.KDSTATUSASET
	FROM (
	SELECT A.IDBRG, A.UNITKEY, A.ASETKEY, MAX(CAST(A.URUTTRANS AS INT)) URUTTRANS, SUM((CASE WHEN LEFT(A.KDTANS,1)=2 THEN -1*A.NILAI ELSE A.NILAI END)) AS NILAI
	FROM ASET_KIBDDET A WHERE A.UNITKEY=@UNITKEY 
	AND LEFT(A.ASETKEY,5)=@ASETKEY   
	GROUP BY A.IDBRG, A.UNITKEY, A.ASETKEY ) X
	LEFT JOIN ASET_KIBDDET B ON B.IDBRG = X.IDBRG AND B.URUTTRANS = X.URUTTRANS AND B.UNITKEY = X.UNITKEY AND B.ASETKEY = X.ASETKEY
END
ELSE IF (@KDKIB = '05')
BEGIN
	INSERT INTO #_KIB
	SELECT X.IDBRG, X.UNITKEY, X.ASETKEY, X.URUTTRANS, X.NILAI, B.KDKON, B.KDKLAS, B.KDSTATUSASET
	FROM (
	SELECT A.IDBRG, A.UNITKEY, A.ASETKEY, MAX(CAST(A.URUTTRANS AS INT)) URUTTRANS, SUM((CASE WHEN LEFT(A.KDTANS,1)=2 THEN -1*A.NILAI ELSE A.NILAI END)) AS NILAI
	FROM ASET_KIBEDET A WHERE A.UNITKEY=@UNITKEY 
	AND LEFT(A.ASETKEY,5)=@ASETKEY   
	GROUP BY A.IDBRG, A.UNITKEY, A.ASETKEY ) X
	LEFT JOIN ASET_KIBEDET B ON B.IDBRG = X.IDBRG AND B.URUTTRANS = X.URUTTRANS AND B.UNITKEY = X.UNITKEY AND B.ASETKEY = X.ASETKEY
END
ELSE IF (@KDKIB = '07')
BEGIN
	INSERT INTO #_KIB
	SELECT X.IDBRG, X.UNITKEY, X.ASETKEY, X.URUTTRANS, X.NILAI, B.KDKON, B.KDKLAS, B.KDSTATUSASET
	FROM (
	SELECT A.IDBRG, A.UNITKEY, A.ASETKEY, MAX(CAST(A.URUTTRANS AS INT)) URUTTRANS, SUM((CASE WHEN LEFT(A.KDTANS,1)=2 THEN -1*A.NILAI ELSE A.NILAI END)) AS NILAI
	FROM ASET_KIBGDET A WHERE A.UNITKEY=@UNITKEY 
	AND LEFT(A.ASETKEY,5)=@ASETKEY  
	GROUP BY A.IDBRG, A.UNITKEY, A.ASETKEY ) X
	LEFT JOIN ASET_KIBGDET B ON B.IDBRG = X.IDBRG AND B.URUTTRANS = X.URUTTRANS AND B.UNITKEY = X.UNITKEY AND B.ASETKEY = X.ASETKEY
END
ELSE IF (@KDKIB = '09')
BEGIN
	INSERT INTO #_KIB
	SELECT X.IDBRG, X.UNITKEY, X.ASETKEY, X.URUTTRANS, X.NILAI, B.KDKON, B.KDKLAS, B.KDSTATUSASET
	FROM (
	SELECT A.IDBRG, A.UNITKEY, A.ASETKEY, MAX(CAST(A.URUTTRANS AS INT)) URUTTRANS, SUM((CASE WHEN LEFT(A.KDTANS,1)=2 THEN -1*A.NILAI ELSE A.NILAI END)) AS NILAI
	FROM ASET_KIBCDET A WHERE A.UNITKEY=@UNITKEY 
	AND LEFT(A.ASETKEY,9)=@ASETKEY  
	GROUP BY A.IDBRG, A.UNITKEY, A.ASETKEY ) X
	LEFT JOIN ASET_KIBCDET B ON B.IDBRG = X.IDBRG AND B.URUTTRANS = X.URUTTRANS AND B.UNITKEY = X.UNITKEY AND B.ASETKEY = X.ASETKEY
	UNION
	SELECT X.IDBRG, X.UNITKEY, X.ASETKEY, X.URUTTRANS, X.NILAI, B.KDKON, B.KDKLAS, B.KDSTATUSASET
	FROM (
	SELECT A.IDBRG, A.UNITKEY, A.ASETKEY, MAX(CAST(A.URUTTRANS AS INT)) URUTTRANS, SUM((CASE WHEN LEFT(A.KDTANS,1)=2 THEN -1*A.NILAI ELSE A.NILAI END)) AS NILAI
	FROM ASET_KIBDDET A WHERE A.UNITKEY=@UNITKEY 
	AND LEFT(A.ASETKEY,9)=@ASETKEY  
	GROUP BY A.IDBRG, A.UNITKEY, A.ASETKEY ) X
	LEFT JOIN ASET_KIBDDET B ON B.IDBRG = X.IDBRG AND B.URUTTRANS = X.URUTTRANS AND B.UNITKEY = X.UNITKEY AND B.ASETKEY = X.ASETKEY
	UNION
	SELECT X.IDBRG, X.UNITKEY, X.ASETKEY, X.URUTTRANS, X.NILAI, B.KDKON, B.KDKLAS, B.KDSTATUSASET
	FROM (
	SELECT A.IDBRG, A.UNITKEY, A.ASETKEY, MAX(CAST(A.URUTTRANS AS INT)) URUTTRANS, SUM((CASE WHEN LEFT(A.KDTANS,1)=2 THEN -1*A.NILAI ELSE A.NILAI END)) AS NILAI
	FROM ASET_KIBEDET A WHERE A.UNITKEY=@UNITKEY 
	AND LEFT(A.ASETKEY,9)=@ASETKEY  
	GROUP BY A.IDBRG, A.UNITKEY, A.ASETKEY ) X
	LEFT JOIN ASET_KIBEDET B ON B.IDBRG = X.IDBRG AND B.URUTTRANS = X.URUTTRANS AND B.UNITKEY = X.UNITKEY AND B.ASETKEY = X.ASETKEY
END



SELECT Y.*
FROM (
SELECT X.IDBRG, X.UNITKEY, X.ASETKEY, DA.KDASET, DA.NMASET, AB.TAHUN, AB.NOREG, X.NILAI, 
	ISNULL(DA.UMEKO,0) UMEKO, AB.KDPEMILIK, JM.NMPEMILIK, X.KDKON, K.NMKON, AB.ASALUSUL, AB.PENGGUNA, AB.KDSATUAN, S.NMSATUAN, 
	CASE AB.KDKIB 
	WHEN '03' THEN (case AB.BERTINGKAT WHEN 1 THEN 'Bertingkat' ELSE '' END)+(case AB.BETON WHEN 1 THEN ', Beton' ELSE '' END) 
	WHEN '04' THEN AB.KONSTRUKSI
	WHEN '05' THEN AB.SPESIFIKASI
	WHEN '06' THEN (case AB.BERTINGKAT WHEN 1 THEN 'Bertingkat' ELSE '' END)+(case AB.BETON WHEN 1 THEN ', Beton' ELSE '' END) 
	WHEN '07' THEN AB.SPESIFIKASI
	ELSE AB.MERKTYPE
	END AS SPESIFIKASI, 
	CASE AB.KDKIB 
	WHEN '01' THEN 'Luas tanah: '+CAST(ISNULL(AB.LUASTNH,0) AS VARCHAR)
	WHEN '03' THEN 'Luas tanah: '+CAST(ISNULL(AB.LUASTNH,0) AS VARCHAR)+' '+' Luas lantai: '+CAST(ISNULL(AB.LUASLT,0) AS VARCHAR)
	WHEN '04' THEN 'Panjang: '+CAST(ISNULL(AB.PANJANG,0) AS VARCHAR)+' Lebar: '+CAST(ISNULL(AB.LEBAR,0) AS VARCHAR)+' Luas: '+CAST(ISNULL(AB.LUAS,0) AS VARCHAR)
	WHEN '06' THEN 'Luas lantai: '+CAST(ISNULL(AB.LUASLT,0) AS VARCHAR)
	ELSE AB.UKURAN
	END AS UKURAN,
	AB.BAHAN, 
	CASE AB.KDKIB 
	WHEN '01' THEN AB.NOFIKAT
	WHEN '02' THEN ISNULL(AB.NOPABRIK,'')+' / '+ISNULL(AB.NORANGKA,'')+' / '+ISNULL(AB.NOPOLISI,'')+' / '+ISNULL(AB.NOBPKB,'')+' / '+ISNULL(AB.NOMESIN,'')
	WHEN '03' THEN AB.NODOKGDG
	WHEN '04' THEN AB.NODOKJIJ
	WHEN '06' THEN AB.NODOKKDP
	ELSE NULL 
	END AS NOSERTIFIKAT,
	AB.ALAMAT, AB.KET, ISNULL(ISNULL(AB.JDLPENERBIT,(AB.JUDUL)),'') AS JUDUL,X.KDKLAS, JK.URAIKLAS, X.KDSTATUSASET, AB.KDKIB, J.NMKIB
FROM #_KIB X
	LEFT JOIN ASET_KIBSPESIFIKASI AB ON AB.IDBRG = X.IDBRG AND AB.UNITKEY = X.UNITKEY AND AB.ASETKEY = X.ASETKEY
	LEFT JOIN ASET_KIB AK ON AK.IDBRG = X.IDBRG
	LEFT JOIN DAFTASET DA ON DA.ASETKEY = X.ASETKEY
	LEFT JOIN JMILIK JM ON JM.KDPEMILIK = AB.KDPEMILIK
	LEFT JOIN KONASET K ON K.KDKON = X.KDKON
	LEFT JOIN SATUAN S ON S.KDSATUAN = AB.KDSATUAN
	LEFT JOIN JKLAS JK ON JK.KDKLAS = X.KDKLAS
	LEFT JOIN JNSKIB J ON J.KDKIB = AB.KDKIB
WHERE AK.STATUSPENGGUNA = '1' AND X.NILAI > 0
) Y
-- LOOKUP BARANG YG BELUM MASUK KE REKLASDET
LEFT JOIN ASET_REKLASDET AR ON AR.IDBRG = Y.IDBRG AND AR.UNITKEY = Y.UNITKEY AND AR.ASETKEY = Y.ASETKEY
WHERE AR.NOREKLAS IS NULL
ORDER BY Y.KDASET, Y.TAHUN, Y.NOREG
