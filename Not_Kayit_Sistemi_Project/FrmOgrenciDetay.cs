using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Not_Kayit_Sistemi_Project
{
    public partial class FrmOgrenciDetay : Form
    {
        public FrmOgrenciDetay()
        {
            InitializeComponent();
        }
        public string numara;
  
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-K8SFS2P\fatma;Initial Catalog=DbNotKayıt;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        {
            
            lblNumara.Text = numara;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLDERS Where OGRNUMARA = @p1", baglanti);
            komut.Parameters.AddWithValue("@p1", numara);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[2].ToString() + " " + dr[3].ToString();
                lblSınav1.Text = dr[4].ToString();
                lblSınav2.Text = dr[5].ToString();
                lblSınav3.Text = dr[6].ToString();
                lblOrtalama.Text = dr[7].ToString();
                lblDurum.Text = dr[8].ToString();
            }
            baglanti.Close();

            if (lblDurum.Text == "True")
            {
                lblDurum.Text = "Geçti";
            }
            else
            {
                lblDurum.Text = "Kaldı";
            }
           
        }
    }
}
