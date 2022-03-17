using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Not_Kayit_Sistemi_Project
{
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-K8SFS2P\fatma;Initial Catalog=DbNotKayıt;Integrated Security=True");
        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbNotKayıtDataSet.TBLDERS' table. You can move, or remove it, as needed.
            this.tBLDERSTableAdapter.Fill(this.dbNotKayıtDataSet.TBLDERS);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Insert into TBLDERS (OGRNUMARA,OGRAD,OGRSOYAD) values (@p1,@p2,@p3)", baglanti);
            komut.Parameters.AddWithValue("@p1", mskNumara.Text);
            komut.Parameters.AddWithValue("@p2", txtAd.Text);
            komut.Parameters.AddWithValue("@p3", TxtSoyad.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci sisteme eklendi");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayıtDataSet.TBLDERS);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            mskNumara.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtSınav1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtSınav2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            TxtSınav3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }
        public string durum;
        private void button2_Click(object sender, EventArgs e)
        {
            double ortalama, s1, s2, s3;
            
            s1 = Convert.ToDouble(TxtSınav1.Text);
            s2 = Convert.ToDouble(TxtSınav2.Text);
            s3 = Convert.ToDouble(TxtSınav3.Text);
            ortalama = (s1 + s2 + s3) / 3;
            lblSinifOrtalamasi.Text = ortalama.ToString();
            if(ortalama>=50)
            {
                durum = "true";
            }
            else
            {
                durum = "false";
            }
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update TBLDERS set OGRS1 = @p1,OGRS2=@p2,OGRS3=@p3,ORTALAMA=@p4,DURUM=@p5 WHERE OGRNUMARA=@P6",baglanti);
            komut.Parameters.AddWithValue("@p1", TxtSınav1.Text);
            komut.Parameters.AddWithValue("@p2", TxtSınav2.Text);
            komut.Parameters.AddWithValue("@p3", TxtSınav3.Text);
            komut.Parameters.AddWithValue("@p4", decimal.Parse(lblSinifOrtalamasi.Text));
            komut.Parameters.AddWithValue("@p5", durum);
            komut.Parameters.AddWithValue("@p6", mskNumara.Text);
            komut.ExecuteNonQuery();
           
            MessageBox.Show("Öğrenci Notları Güncellendi");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayıtDataSet.TBLDERS);
            SqlCommand komut2 = new SqlCommand("SELECT COUNT (*) FROM TBLDERS WHERE DURUM = 'True'", baglanti);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                lblGecenOgrenci.Text = Convert.ToInt16(dr2[0]).ToString();
            }
            SqlCommand komut3 = new SqlCommand("SELECT COUNT (*) FROM TBLDERS WHERE DURUM = 'False'", baglanti);
            baglanti.Close();
            baglanti.Open();
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                LblKalanOgrenci.Text = Convert.ToInt16(dr3[0]).ToString();
            }
            baglanti.Close();

        }
    }
}
