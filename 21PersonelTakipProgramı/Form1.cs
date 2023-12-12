using System.Data.OleDb;

namespace _21PersonelTakipProgramı
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Veri tabanı dosya yolu ve provider nesnesinin belirlenmesi
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=personel.accdb");

        //Formlar arası veri aktarımında kullanılacak değişkenler
        public static string tcno, adi, soyadi, yetki;

        private void button1_Click(object sender, EventArgs e)
        {
            if (hak != 0)
            {
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    if (radioButton1.Checked == true)
                    {
                        if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text && kayitokuma["yetki"].ToString() == "Yönetici")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            adi = kayitokuma.GetValue(1).ToString();
                            soyadi = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();
                            this.Hide();
                            Form2 frm2 = new Form2();
                            frm2.Show();
                            break;
                        }
                    }

                    if (radioButton2.Checked == true)
                    {
                        if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text && kayitokuma["yetki"].ToString() == "Kullanıcı")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            adi = kayitokuma.GetValue(1).ToString();
                            soyadi = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();
                            this.Hide();
                            Form3 frm3 = new Form3();
                            frm3.Show();
                            break;
                        }
                    }
                }
                if (durum == false)
                    hak--;
                baglantim.Close();
            }
            label5.Text = Convert.ToString(hak);
            if (hak == 0)
            {
                button1.Enabled = false;
                MessageBox.Show("Giriş hakkı kalmadı!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        //Yalnızca bu formda geçerli olacak değişkenler(yerel)
        int hak = 3; bool durum = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Kullanıcı Girişi";
            this.AcceptButton = button1;
            this.CancelButton = button2;
            label5.Text = Convert.ToString(hak);
            radioButton1.Checked = true;

            //form açıldığında ekranın ortasında açılsın.
            this.StartPosition = FormStartPosition.CenterScreen;

            //Simge durumuna küçült ve tam ekran tuşlarını kaldırdık.
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;

        }
    }
}