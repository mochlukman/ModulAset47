--Script untuk mengupdate Modul Aset
--Tanggal 2 Agustus 2022

--Script Delete
DELETE FROM ss01appmenu WHERE kdmenu like '04.02.03.%'

--Insert Menu Berita Acara
INSERT INTO SS01APPMENU(IDAPP, KDMENU, NMMENU, URMENU, IDMENU, KDLEVEL, TYPE)
VALUES ('1847B4EE-619F-45FE-9F5F-FD911B172601', '04.02.03.', 'BAST Hibah Dan Lainnya', '04.02.03. BAST Hibah Dan Lainnya', 
		'4CEE9DDF-80F8-44B9-B856-22298D296D8C', '3', 'H');
		
INSERT INTO SS01APPMENU(IDAPP, KDMENU, NMMENU, URMENU, IDMENU, URL, OLIST1, OLISTDETIL1, OLISTDETIL2, KDLEVEL, TYPE)
VALUES ('1847B4EE-619F-45FE-9F5F-FD911B172601', '04.02.03.01.', 'Penerimaan Hibah', '04.02.03.01. Penerimaan Hibah', 
		'7CFE252F-194F-45EB-B68F-7CC7F8672868','Page/PageTabular.aspx?','Usadi.Valid49.BO.BeritaHibahControl, Usadi.Valid49.Aset.MAT',
		'Usadi.Valid49.BO.BeritadetbrgLainnyaControl, Usadi.Valid49.Aset.MAT', 'Usadi.Valid49.BO.BeritadetbrgEditlainnyaControl, Usadi.Valid49.Aset.MAT',
		'4', 'D');

INSERT INTO SS01APPMENU(IDAPP, KDMENU, NMMENU, URMENU, IDMENU, URL, OLIST1, OLISTDETIL1, OLISTDETIL2, KDLEVEL, TYPE)
VALUES ('1847B4EE-619F-45FE-9F5F-FD911B172601', '04.02.03.02.', 'Tukar Menukar', '04.02.03.02. Tukar Menukar', 
		'4EF20259-FFB1-4A8C-9828-4C0186956625','Page/PageTabular.aspx?','Usadi.Valid49.BO.BeritaTukarmenukarControl, Usadi.Valid49.Aset.MAT',
		'Usadi.Valid49.BO.BeritadetbrgLainnyaControl, Usadi.Valid49.Aset.MAT', 'Usadi.Valid49.BO.BeritadetbrgEditlainnyaControl, Usadi.Valid49.Aset.MAT',
		'4', 'D');

INSERT INTO SS01APPMENU(IDAPP, KDMENU, NMMENU, URMENU, IDMENU, URL, OLIST1, OLISTDETIL1, OLISTDETIL2, KDLEVEL, TYPE)
VALUES ('1847B4EE-619F-45FE-9F5F-FD911B172601', '04.02.03.03.', 'Inventarisasi (Baru Catat)', '04.02.03.03. Inventarisasi (Baru Catat)', 
		'E93645D9-C56D-4842-B6F0-89D00A457BDF','Page/PageTabular.aspx?','Usadi.Valid49.BO.BeritaInventarisasiControl, Usadi.Valid49.Aset.MAT',
		'Usadi.Valid49.BO.BeritadetbrgLainnyaControl, Usadi.Valid49.Aset.MAT', 'Usadi.Valid49.BO.BeritadetbrgEditlainnyaControl, Usadi.Valid49.Aset.MAT',
		'4', 'D');

INSERT INTO SS01APPMENU(IDAPP, KDMENU, NMMENU, URMENU, IDMENU, URL, OLIST1, OLISTDETIL1, OLISTDETIL2, KDLEVEL, TYPE)
VALUES ('1847B4EE-619F-45FE-9F5F-FD911B172601', '04.02.03.04.', 'Kemajuan Fisik', '04.02.03.04. Kemajuan Fisik', 
		'E971FAF3-F834-4FA2-9E0A-9A2A4E4DCD4F','Page/PageTabular.aspx?','Usadi.Valid49.BO.BeritaBakfControl, Usadi.Valid49.Aset.MAT',
		'Usadi.Valid49.BO.BeritadetbrgLainnyaControl, Usadi.Valid49.Aset.MAT', 'Usadi.Valid49.BO.BeritadetbrgEditlainnyaControl, Usadi.Valid49.Aset.MAT',
		'4', 'D');

INSERT INTO SS01APPMENU(IDAPP, KDMENU, NMMENU, URMENU, IDMENU, URL, OLIST1, OLISTDETIL1, OLISTDETIL2, KDLEVEL, TYPE)
VALUES ('1847B4EE-619F-45FE-9F5F-FD911B172601', '04.02.03.05.', 'BOS', '04.02.03.05. BOS', 
		'D018ED91-C8EF-49CE-84A0-4F498270832D','Page/PageTabular.aspx?','Usadi.Valid49.BO.BeritaBosControl, Usadi.Valid49.Aset.MAT',
		'Usadi.Valid49.BO.BeritadetbrgLainnyaControl, Usadi.Valid49.Aset.MAT', 'Usadi.Valid49.BO.BeritadetbrgEditlainnyaControl, Usadi.Valid49.Aset.MAT',
		'4', 'D');
		
		INSERT INTO SS01APPMENU(IDAPP, KDMENU, NMMENU, URMENU, IDMENU, URL, OLIST1, OLISTDETIL1, OLISTDETIL2, KDLEVEL, TYPE)
VALUES ('1847B4EE-619F-45FE-9F5F-FD911B172601', '04.02.03.06.', 'BLUD', '04.02.03.06. BLUD', 
		'AB12E378-0D26-4EB2-AB3D-B0E4EC32E9A8','Page/PageTabular.aspx?','Usadi.Valid49.BO.BeritaBludControl, Usadi.Valid49.Aset.MAT',
		'Usadi.Valid49.BO.BeritadetbrgLainnyaControl, Usadi.Valid49.Aset.MAT', 'Usadi.Valid49.BO.BeritadetbrgEditlainnyaControl, Usadi.Valid49.Aset.MAT',
		'4', 'D');

INSERT INTO SS01APPMENU(IDAPP, KDMENU, NMMENU, URMENU, IDMENU, URL, OLIST1, OLISTDETIL1, OLISTDETIL2, KDLEVEL, TYPE)
VALUES ('1847B4EE-619F-45FE-9F5F-FD911B172601', '04.02.03.07.', 'Ketentuan Perundang Undangan', '04.02.03.07. Ketentuan Perundang Undangan', 
		'10C9F236-8E7F-41F9-9EFF-B41DD202E826','Page/PageTabular.aspx?','Usadi.Valid49.BO.BeritaUUControl, Usadi.Valid49.Aset.MAT',
		'Usadi.Valid49.BO.BeritadetbrgLainnyaControl, Usadi.Valid49.Aset.MAT', 'Usadi.Valid49.BO.BeritadetbrgEditlainnyaControl, Usadi.Valid49.Aset.MAT',
		'4', 'D');
		
		INSERT INTO SS01APPMENU(IDAPP, KDMENU, NMMENU, URMENU, IDMENU, URL, OLIST1, OLISTDETIL1, OLISTDETIL2, KDLEVEL, TYPE)
VALUES ('1847B4EE-619F-45FE-9F5F-FD911B172601', '04.02.03.08.', 'Putusan Pengadilan', '04.02.03.08. Putusan Pengadilan', 
		'A8E635BD-2EB1-452E-A188-88306406E384','Page/PageTabular.aspx?','Usadi.Valid49.BO.BeritaPutusanControl, Usadi.Valid49.Aset.MAT',
		'Usadi.Valid49.BO.BeritadetbrgLainnyaControl, Usadi.Valid49.Aset.MAT', 'Usadi.Valid49.BO.BeritadetbrgEditlainnyaControl, Usadi.Valid49.Aset.MAT',
		'4', 'D');

INSERT INTO SS01APPMENU(IDAPP, KDMENU, NMMENU, URMENU, IDMENU, URL, OLIST1, OLISTDETIL1, OLISTDETIL2, KDLEVEL, TYPE)
VALUES ('1847B4EE-619F-45FE-9F5F-FD911B172601', '04.02.03.09.', 'Divestasi', '04.02.03.09. Divestasi', 
		'E928516C-AB86-41EA-BA0B-581E4F85F3F6','Page/PageTabular.aspx?','Usadi.Valid49.BO.BeritaDivestasiControl, Usadi.Valid49.Aset.MAT',
		'Usadi.Valid49.BO.BeritadetbrgLainnyaControl, Usadi.Valid49.Aset.MAT', 'Usadi.Valid49.BO.BeritadetbrgEditlainnyaControl, Usadi.Valid49.Aset.MAT',
		'4', 'D');
		
		INSERT INTO SS01APPMENU(IDAPP, KDMENU, NMMENU, URMENU, IDMENU, URL, OLIST1, OLISTDETIL1, OLISTDETIL2, KDLEVEL, TYPE)
VALUES ('1847B4EE-619F-45FE-9F5F-FD911B172601', '04.02.03.10.', 'Pembatalan Penghapusan', '04.02.03.10. Pembatalan Penghapusan', 
		'C527E8D7-2303-4BD2-96BD-4C3BA88DD973','Page/PageTabular.aspx?','Usadi.Valid49.BO.BeritaPembatalanControl, Usadi.Valid49.Aset.MAT',
		'Usadi.Valid49.BO.BeritadetbrgLainnyaControl, Usadi.Valid49.Aset.MAT', 'Usadi.Valid49.BO.BeritadetbrgEditlainnyaControl, Usadi.Valid49.Aset.MAT',
		'4', 'D');

INSERT INTO SS01APPMENU(IDAPP, KDMENU, NMMENU, URMENU, IDMENU, URL, OLIST1, OLISTDETIL1, OLISTDETIL2, KDLEVEL, TYPE)
VALUES ('1847B4EE-619F-45FE-9F5F-FD911B172601', '04.02.03.11.', 'Perolehan/Penerimaan Lainnya', '04.02.03.11. Perolehan/Penerimaan Lainnya', 
		'11024D49-BFB6-4FF4-915D-F5B8F591064C','Page/PageTabular.aspx?','Usadi.Valid49.BO.BeritaLainnyaControl, Usadi.Valid49.Aset.MAT',
		'Usadi.Valid49.BO.BeritadetbrgLainnyaControl, Usadi.Valid49.Aset.MAT', 'Usadi.Valid49.BO.BeritadetbrgEditlainnyaControl, Usadi.Valid49.Aset.MAT',
		'4', 'D');
		
DELETE FROM ss01appmenu WHERE kdmenu = '04.05.04.'
INSERT INTO SS01APPMENU(IDAPP, KDMENU, NMMENU, URMENU, IDMENU, URL, OLIST1, OLISTDETIL1, KDLEVEL, TYPE)
VALUES ('1847B4EE-619F-45FE-9F5F-FD911B172601', '04.05.04.', 'Penerimaan BMD Internal', '04.05.04. Penerimaan BMD Internal', 
		'101F4F65-50AB-430C-9F50-4FF7460714B2','Page/PageTabular.aspx?','Usadi.Valid49.BO.PenerimaanBmdInternalControl, Usadi.Valid49.Aset.MAT',
		'Usadi.Valid49.BO.PenggunadetControl, Usadi.Valid49.Aset.MAT','4', 'D');

DELETE FROM ss01appmenu WHERE kdmenu in ('04.05.05.','04.07.05.')
INSERT INTO SS01APPMENU(IDAPP, KDMENU, NMMENU, URMENU, IDMENU, URL, OLIST1, OLISTDETIL1, KDLEVEL, TYPE)
VALUES ('1847B4EE-619F-45FE-9F5F-FD911B172601', '04.05.05.', 'Pengeluaran BMD Internal', '04.05.05. Pengeluaran BMD Internal', 
		'8E9CA981-758F-473D-8F23-7A74C87CDA31','Page/PageTabular.aspx?','Usadi.Valid49.BO.PengeluaranBmdInternalControl, Usadi.Valid49.Aset.MAT',
		'Usadi.Valid49.BO.PenggunadetControl, Usadi.Valid49.Aset.MAT','4', 'D');
	
INSERT INTO SS01APPMENU(IDAPP, KDMENU, NMMENU, URMENU, IDMENU, URL, OLIST1, OLISTDETIL1, KDLEVEL, TYPE)
VALUES ('1847B4EE-619F-45FE-9F5F-FD911B172601', '04.07.05.', 'Persediaan Ke Aset Tetap', '04.07.05.Persediaan Ke Aset Tetap', 
		'1FDFF1D6-89FA-43C5-8C62-587CA5689037','Page/PageTabular.aspx?','Usadi.Valid49.BO.ReklasPersediaanToTetapControl, Usadi.Valid49.Aset.MAT',
		'Usadi.Valid49.BO.ReklasdetControl, Usadi.Valid49.Aset.MAT','3', 'D');
		--New Menu

--Script Delete
DELETE FROM ss01appmenu WHERE kdmenu = '04.15.02.'
INSERT INTO SS01APPMENU(IDAPP, KDMENU, NMMENU, URMENU, IDMENU, URL, OLIST1, OLISTDETIL1, KDLEVEL, TYPE)
VALUES ('1847B4EE-619F-45FE-9F5F-FD911B172601', '04.15.02.', 'Koreksi Pencatatan Ganda', '04.15.02.Koreksi Pencatatan Ganda', 
		'592D37CC-7DDC-4CB9-8128-8FC0D9DCC804','Page/PageTabular.aspx?','Usadi.Valid49.BO.KoreksiGandaControl, Usadi.Valid49.Aset.MAT',
		'Usadi.Valid49.BO.KoreksidetControl, Usadi.Valid49.Aset.MAT','4', 'D');

DELETE FROM ss01appmenu WHERE kdmenu = '04.15.03.'
INSERT INTO SS01APPMENU(IDAPP, KDMENU, NMMENU, URMENU, IDMENU, URL, OLIST1, OLISTDETIL1, KDLEVEL, TYPE)
VALUES ('1847B4EE-619F-45FE-9F5F-FD911B172601', '04.15.03.', 'Koreksi Spesifikasi', '04.15.03.Koreksi Spesifikasi', 
		'037F761E-D37D-4EB1-9F8B-04ADE1E1802C','Page/PageTabular.aspx?','Usadi.Valid49.BO.KoreksiSpesifikasiControl, Usadi.Valid49.Aset.MAT',
		'Usadi.Valid49.BO.KoreksidetControl, Usadi.Valid49.Aset.MAT','4', 'D');

DELETE FROM ss01appmenu WHERE kdmenu = '04.15.04.'
INSERT INTO SS01APPMENU(IDAPP, KDMENU, NMMENU, URMENU, IDMENU, URL, OLIST1, OLISTDETIL1, KDLEVEL, TYPE)
VALUES ('1847B4EE-619F-45FE-9F5F-FD911B172601', '04.15.04.', 'Koreksi Lainnya', '04.15.04.Koreksi Lainnya', 
		'9DA696B2-3FE6-4939-8F9C-C9FD2E3606C7','Page/PageTabular.aspx?','Usadi.Valid49.BO.KoreksiLainnyaControl, Usadi.Valid49.Aset.MAT',
		'Usadi.Valid49.BO.KoreksidetControl, Usadi.Valid49.Aset.MAT','4', 'D');

DELETE FROM ss01appmenu WHERE kdmenu = '04.10.02.'
INSERT INTO SS01APPMENU(IDAPP, KDMENU, NMMENU, URMENU, IDMENU, URL, OLIST1, OLISTDETIL1, KDLEVEL, TYPE)
VALUES ('1847B4EE-619F-45FE-9F5F-FD911B172601', '04.10.02.', 'Pinjam Pakai', '04.10.02.Pinjam Pakai', 
		'81A1316C-7D63-4D2B-90F7-403CE6698614','Page/PageTabular.aspx?','Usadi.Valid49.BO.PinjamPakaiControl, Usadi.Valid49.Aset.MAT',
		'Usadi.Valid49.BO.KemitraandetControl, Usadi.Valid49.Aset.MAT','3', 'D');
		
