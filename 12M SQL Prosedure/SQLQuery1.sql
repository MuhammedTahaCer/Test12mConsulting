CREATE PROCEDURE GetStokEkstresi2
    @Malkodu nvarchar(50),
    @BaslangicTarihi int,
    @BitisTarihi int
AS
BEGIN
    SELECT ROW_NUMBER() OVER (ORDER BY STI.Tarih) AS SiraNo,
           CASE STI.IslemTur
               WHEN 0 THEN 'Giriþ'
               WHEN 1 THEN 'Çýkýþ'
           END AS IslemTur,
           STI.EvrakNo,
           CONVERT(VARCHAR(15), CAST(STI.Tarih - 2 AS datetime), 104) AS Tarih,
           CASE STI.IslemTur
               WHEN 0 THEN STI.Miktar
               ELSE 0
           END AS GirisMiktar,
           CASE STI.IslemTur
               WHEN 1 THEN STI.Miktar
               ELSE 0
           END AS CikisMiktar,
           (SELECT SUM(CASE STI.IslemTur
                            WHEN 0 THEN STI.Miktar
                            WHEN 1 THEN -STI.Miktar
                        END)
            FROM  STI
            WHERE STI.Malkodu = @Malkodu AND STI.Tarih BETWEEN @BaslangicTarihi AND @BitisTarihi
                  AND STI.ID <= STI.ID) AS Stok
    FROM  STI
    WHERE STI.Malkodu = @Malkodu AND STI.Tarih BETWEEN @BaslangicTarihi AND @BitisTarihi
END

Execute GetStokEkstresi2 '10081 SÝEMENS', 40000, 42789