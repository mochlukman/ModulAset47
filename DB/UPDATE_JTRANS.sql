



INSERT INTO JTRANS
VALUES ('230','Penyaluran Persediaan','Penyaluran Persediaan','2','2023-09-21 20:01:33.463','2023-09-21 20:01:33.463')

INSERT INTO JTRANS
VALUES ('231','Penyaluran Penerimaan (Validasi)','Penyaluran Penerimaan (Validasi)','2','2023-09-21 20:01:33.463','2023-09-21 20:01:33.463')

INSERT INTO JTRANS
VALUES ('232','Penggunaan Persediaan','Penggunaan Persediaan','2','2023-09-21 20:01:33.463','2023-09-21 20:01:33.463')

UPDATE JTRANS
SET NMTRANS='Reklas Aset Lain-lain ke Aset Tetap', KET='Reklas (Masuk ke Aset Tetap)'
WHERE KDTANS='130'

UPDATE JTRANS
SET NMTRANS='Pengembalian ke Gudang', KET='Retur Persediaan'
WHERE KDTANS='118'


---UPDATE JTRANS ASET LAIN-LAIN----
update JTRANS set NMTRANS='Rusak Berat (Keluar)', KET='Rusak Berat (Keluar dari Aset Lain-lain)' where KDTANS='216'
update JTRANS set NMTRANS='Non Operasional (Keluar)', KET='Non Operasional (Keluar dari Aset Lain-lain)' where KDTANS='217'
update JTRANS set NMTRANS='Aset Lain-lain lainnya (Keluar)', KET='Aset Lain-lain lainnya (Keluar dari Aset Lain-lain)' where KDTANS='218'

---TAMBAH JTRANS PENGHAPUSAN---
insert JTRANS (KDTANS,NMTRANS,KET,JENIS)
values ('250','Pemindahtanganan','Penghapusan','2'),
('251','Putusan Pengadilan','Penghapusan','2'),
('252','Ketentuan Perudang-undangan','Penghapusan','2'),
('253','Sebab Lain','Penghapusan','2')
