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
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;  //// Connection String, web.config dosyasından düzenledikten sonra code-behind tarafında çağırma



        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btn_click(object sender, EventArgs e)
        {
            string Malkodu = ""; //parametre eklerken hata vermemesi için sqlde sorgulanacak nesneyi burdan da tanımlamam gerekti.


            //// Başlangıç tarihi ve bitiş tarihini TextBox'lardan,
            //DateTime baslangicTarihi = DateTime.Parse(tarihilk.Text);
            //DateTime bitisTarihi = DateTime.Parse(tarihson.Text);

            // DateTime değerlerini INT değerlere Convert işlemi
            int baslangicTarihiInt = Convert.ToInt32(baslangicTarihi.ToOADate());
            int bitisTarihiInt = Convert.ToInt32(bitisTarihi.ToOADate());

            
            using (SqlConnection connection = new SqlConnection(connectionString))// SqlConnection nesnesi oluştur
            {
                
                string storedProcedureName = "StokEkstresiProseduru";// Stored Procedure adını belirt

                // SqlCommand nesnesi oluştur
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    
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
                        }
                    }
                }
            }

        }

        protected void btnAra_Click(object sender, EventArgs e)
        {
            string searchValue = txtMalArama.Text;

            string query = "SELECT * FROM STI INNER JOIN STK ON STI.MalKodu = STK.MalKodu WHERE STK.MalKodu LIKE @SearchValue OR STK.MalAdi LIKE @SearchValue ORDER BY Tarih ASC"; // Click event ile arama yapacak queryi burada tanımaldım

            

               using (SqlConnection connection = new SqlConnection(connectionString)) //connectionString çağırmam gerekiyor
            {
                using (SqlCommand command = new SqlCommand(query, connection)) // click ile command eylemi (bu aslında DataAdapter ve DataTable arasını yapmak için olması lazım) için çağırıyorum
                {
                    command.Parameters.AddWithValue("@SearchValue", "%" + searchValue + "%");

                    // SQL sorgusunu çalıştırın ve sonucu DataTable nesnesinde tutun
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt); // Burada hata alıyorum, prosedür ve connection doğru çalışmasına rağmen Fill işlemi başarısız oluyor.
                    }

                    gridView.DataSource = dt;
                    gridView.DataBind();
                }
            }
        }

    }
}