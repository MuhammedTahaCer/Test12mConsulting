using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Test12mConsulting.Scripts.WebForms
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btn_click(object sender, EventArgs e)
        {
            string Malkodu = "";


            // Başlangıç tarihi ve bitiş tarihini TextBox'lardan alın
            DateTime baslangicTarihi = DateTime.Parse(tarihilk.Text);
            DateTime bitisTarihi = DateTime.Parse(tarihson.Text);

            // DateTime değerlerini INT değerlere dönüştürün
            int baslangicTarihiInt = Convert.ToInt32(baslangicTarihi.ToOADate());
            int bitisTarihiInt = Convert.ToInt32(bitisTarihi.ToOADate());

            // Dönüştürülmüş değerleri kullanarak işlemlere devam edin...

            // Connection String'i web.config dosyasından al
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringName"].ConnectionString;

            // SqlConnection nesnesi oluştur
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Stored Procedure adını belirt
                string storedProcedureName = "StokEkstresiProseduru";

                // SqlCommand nesnesi oluştur
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    // Komut tipini belirle (Stored Procedure)
                    command.CommandType = CommandType.StoredProcedure;

                    // Parametreleri ekle
                    command.Parameters.AddWithValue("@Malkodu", Malkodu);
                    command.Parameters.AddWithValue("@BaslangicTarihi", baslangicTarihi);
                    command.Parameters.AddWithValue("@BitisTarihi", bitisTarihi);

                    // Verileri almak için SqlDataReader nesnesi oluştur
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Verileri oku ve işle
                        while (reader.Read())
                        {
                            // Sıra numarası, işlem türü, evrak no, tarih, giriş miktarı, çıkış miktarı ve stok bilgisi gibi verileri SqlDataReader nesnesinden oku
                            int siraNo = (int)reader["SiraNo"];
                            string islemTur = (int)reader["IslemTur"] == 0 ? "Giriş" : "Çıkış";
                            string evrakNo = (string)reader["EvrakNo"];
                            DateTime tarih = (DateTime)reader["Tarih"];
                            decimal girisMiktar = (decimal)reader["GirisMiktar"];
                            decimal cikisMiktar = (decimal)reader["CikisMiktar"];
                            decimal stok = (decimal)reader["Stok"];

                            // Verileri kullanarak işlem yap
                            // Örneğin, bir GridView veya bir ListBox gibi bir kontrolde verileri görüntüleyebilirsiniz
                        }
                    }
                }
            }

        }

        protected void btnAra_Click(object sender, EventArgs e)
        {
            string searchValue = txtMalArama.Text;

            // SQL sorgusunu oluştururken arama yapılacak alanı belirleyin
            string query = "SELECT * FROM STI INNER JOIN STK ON STI.MalKodu = STK.MalKodu WHERE STK.MalKodu LIKE @SearchValue OR STK.MalAdi LIKE @SearchValue ORDER BY Tarih ASC";

            

               using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SearchValue", "%" + searchValue + "%");

                    // SQL sorgusunu çalıştırın ve sonucu DataTable nesnesinde tutun
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
                    }

                    // DataTable nesnesindeki verileri gridview'e bind edin
                    gridView.DataSource = dt;
                    gridView.DataBind();
                }
            }
        }

    }
}