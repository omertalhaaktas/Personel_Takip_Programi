using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
//güvenli parolayı oluşturmak için ekliyoruz.Regex kütüphanesi.
using System.Text.RegularExpressions;
//giriş çıkış işlemleri için input output kütüphanesini ekliyoruz.
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics.CodeAnalysis;

namespace _21PersonelTakipProgramı
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=personel.accdb");
        private void kullanicilari_goster()
        {
            try
            {
                baglantim.Open();
                //veri tabanındaki sütun isimlerini uygulamada değiştirmek isteyebiliriz türkçe karakter olmadığı için bu değiştirmeyi bu şekilde yapıyoruz.
                OleDbDataAdapter kullanicilari_listele = new OleDbDataAdapter("select tcno AS[TC KİMLİK NO],ad AS[ADI],soyad AS[SOYADI],yetki AS[YETKİ],kullaniciadi AS[KULLANICI ADI],parola AS[PAROLA] from kullanicilar Order By ad ASC", baglantim);
                //bellekte bir alan oluşturuyoruz.
                DataSet dshafiza = new DataSet();
                //oluşturduğumuz alanı sorgumuzun sonuçlarıyla dolduruyoruz.
                kullanicilari_listele.Fill(dshafiza);
                //sorgunun sonucunda gelen ilk tabloyu yazdırıyoruz.
                dataGridView1.DataSource = dshafiza.Tables[0];
                baglantim.Close();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
            }
        }
        private void personelleri_goster()
        {
            try
            {
                baglantim.Open();
                //veri tabanındaki sütun isimlerini uygulamada değiştirmek isteyebiliriz türkçe karakter olmadığı için bu değiştirmeyi bu şekilde yapıyoruz.
                OleDbDataAdapter personelleri_listele = new OleDbDataAdapter("select tcno AS[TC KİMLİK NO],ad AS[ADI],soyad AS[SOYADI],cinsiyet AS[CİNSİYETİ], mezuniyet AS[MEZUNİYETİ],dogumtarihi AS[DOĞUM TARİHİ], gorevi AS[GÖREVİ], gorevyeri AS[GÖREV YERİ],maasi AS[MAAŞI] from personeller Order By ad ASC", baglantim);
                //bellekte bir alan oluşturuyoruz.
                DataSet dshafiza = new DataSet();
                //oluşturduğumuz alanı sorgumuzun sonuçlarıyla dolduruyoruz.
                personelleri_listele.Fill(dshafiza);
                //sorgunun sonucunda gelen ilk tabloyu yazdırıyoruz.
                dataGridView2.DataSource = dshafiza.Tables[0];
                baglantim.Close();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
            }
        }
        private void topPage1_temizle()
        {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); textBox5.Clear(); textBox6.Clear();
        }
        private void topPage2_temizle()
        {
            pictureBox2.Image = null; maskedTextBox1.Clear(); maskedTextBox2.Clear(); maskedTextBox3.Clear(); maskedTextBox4.Clear(); comboBox1.SelectedIndex = -1; comboBox2.SelectedIndex = -1; comboBox3.SelectedIndex = -1;
        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //form2 ayarları
            pictureBox1.Height = 150;
            pictureBox1.Width = 150;

            //resmi pictureboxa göre ayarla
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Form1.tcno + ".jpg");
            }
            catch (Exception)
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\resimyok.jpg");
            }
            //kullanıcı işlemleri sekmesi ayarları
            this.Text = "YÖNETİCİ İŞLEMLERİ";
            label11.ForeColor = Color.DarkRed;
            label11.Text = Form1.adi + " " + Form1.soyadi;
            textBox1.MaxLength = 11;
            textBox4.MaxLength = 8;
            textBox5.MaxLength = 10;
            textBox6.MaxLength = 10;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;


            //kullanıcı adı yerinde 11 karakter olmalı diye bir ipucu baloncuğu çıkıyor.
            toolTip1.SetToolTip(this.textBox1, "TC Kimlik No 11 Karakter Olmalı!");
            radioButton1.Checked = true;

            //küçük harf de yazılsa otomatik olarak büyük harfe çeviriyoruz.
            textBox2.CharacterCasing = CharacterCasing.Upper;
            textBox3.CharacterCasing = CharacterCasing.Upper;

            kullanicilari_goster();

            //personel işlemleri sekmesi ayarları
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Width = 100;
            pictureBox2.Height = 100;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D;

            //kullanıcıyı bizim istediğimiz şablonda veri girmeye zorluyoruz.
            //11 adet rakam girmek zorunlu. 0 zorunlu rakam demek.
            maskedTextBox1.Mask = "00000000000";

            //iki adet harf girmek zorunlu diğerleri ise opsiyonel
            maskedTextBox2.Mask = "LL????????????????????";
            maskedTextBox3.Mask = "LL????????????????????";

            //4 adet rakam girmek zorunda
            maskedTextBox4.Mask = "0000";

            //doldurmak zorunlu
            maskedTextBox4.Text = "0";

            //bu kutuya girilen harfleri otomatik olarak büyütecek
            maskedTextBox2.Text.ToUpper();
            maskedTextBox3.Text.ToUpper();

            comboBox1.Items.Add("İlköğretim");
            comboBox1.Items.Add("Ortaöğretim");
            comboBox1.Items.Add("Lise");
            comboBox1.Items.Add("Üniversite");

            comboBox2.Items.Add("Yönetici");
            comboBox2.Items.Add("Memur");
            comboBox2.Items.Add("Şoför");
            comboBox2.Items.Add("İşçi");

            comboBox3.Items.Add("ARGE");
            comboBox3.Items.Add("Bilgi İşlem");
            comboBox3.Items.Add("Muhasebe");
            comboBox3.Items.Add("Üretim");
            comboBox3.Items.Add("Paketleme");
            comboBox3.Items.Add("Nakliye");

            DateTime zaman = DateTime.Now;
            int yil = int.Parse(zaman.ToString("yyyy"));
            int ay = int.Parse(zaman.ToString("MM"));
            int gun = int.Parse(zaman.ToString("dd"));

            //50 yaşından büyükler çalışamasın
            dateTimePicker1.MinDate = new DateTime(1960, 1, 1);

            //18 yaşından küçükler çalışamasın
            dateTimePicker1.MaxDate = new DateTime(yil - 18, ay, gun);

            //kısa tarih görünsün
            dateTimePicker1.Format = DateTimePickerFormat.Short;

            radioButton3.Checked = true;

            personelleri_goster();
            label22.Text = "";
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //tc kimlik no 11 karakterin altında olmasın
            if (textBox1.Text.Length < 11)
            {
                errorProvider1.SetError(textBox1, "TC Kimlik No 11 karakter olmalı!");
            }
            else
                errorProvider1.Clear();
        }
        //textbox1 de imleç yanıp sönmekteyken her karaktere bastığınızda key press olayı tetiklenir.
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //klavyedeki her karakterin bir ascii karakteri vardır. klavyeden girilen tuş 48 ve 57 arasındaysa veya 8 ascii karakterine sahipse(backspace) bu tuşlara basılabilir.
            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8)
            {
                //bu tuşlara basılmasına izin veriyoruz
                e.Handled = false;
            }
            else
                //aksi durumda basılamaz
                e.Handled = true;

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //yalnızca harf, boşluk ve backspace girebilecek
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
                e.Handled = true;


        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //yalnızca harf, boşluk ve backspace girebilecek
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Length != 8)
                errorProvider1.SetError(textBox4, "Kullanıcı adı 8 karakter olmalı!");
            else
                errorProvider1.Clear();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //harf, sayı ve backspace e izin vericez.birinci koşul harf mi?, ikinci koşul backspace e basılmışsa?, üçüncü koşul sayı mı?
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsDigit(e.KeyChar) == true)
            {
                //kısıtlamayı kapat
                e.Handled = false;
            }
            //kısıtlamayı aç
            else e.Handled = true;
        }

        int parola_skoru = 0;
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string parola_seviyesi = "";
            int kucuk_harf_skoru = 0, buyuk_harf_skoru = 0, sembol_skoru = 0, rakam_skoru = 0;
            string sifre = textBox5.Text;

            //Regex kütüphanesi ingilizce karakterleri baz aldığından, Türkçe karakterlerde sorun yaşamamak için şifre string ifadesindeki Türkçe karakterleri İngilizce karakterlere dönüştürmemiz gerekiyor.
            string duzeltilmis_sifre = "";
            duzeltilmis_sifre = sifre;
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('İ', 'I');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ı', 'i');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ç', 'C');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ç', 'c');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ş', 'S');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ş', 's');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ğ', 'G');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ğ', 'g');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ü', 'U');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ü', 'u');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ö', 'O');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ö', 'o');

            if (sifre != duzeltilmis_sifre)
            {
                sifre = duzeltilmis_sifre;
                textBox5.Text = sifre;
                MessageBox.Show("Paroladaki Türkçe karakterler İngilizce karakterlere dönüştürülmüştür!");
            }
            //Tüm şifredeki karakter sayısından küçük harfleri çıkardığımızda küçük harflerin karakter sayısını buluyoruz.
            int az_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[a-z]", "").Length;

            //1 küçük harf 10 puan, 2 ve üzeri 20 puan olacağı için math min kütüphanesiyle 2 mi küçük azkaraktersayisi mi diyip hangisi küçükse onu alıyoruz ve 10 ile çarpıyoruz.
            kucuk_harf_skoru = Math.Min(2, az_karakter_sayisi) * 10;

            //1 büyük harf 10 puan, 2 ve üzeri 20 puan olacağı için math min kütüphanesiyle 2 mi küçük AZkaraktersayisi mi diyip hangisi küçükse onu alıyoruz ve 10 ile çarpıyoruz.
            int AZ_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[A-Z]", "").Length;
            buyuk_harf_skoru = Math.Min(2, AZ_karakter_sayisi) * 10;

            //Bir rakam 10 puan, iki ve üzeri 20 puan
            int rakam_sayisi = sifre.Length - Regex.Replace(sifre, "[0-9]", "").Length;
            rakam_skoru = Math.Min(2, rakam_sayisi) * 10;

            //Bir sembol 10 puan, iki ve üzeri 20 puan.Geriye başka koşul kalmadığı için regex kütüphanesini kullanmamıza gerek yok
            int sembol_sayisi = sifre.Length - az_karakter_sayisi - AZ_karakter_sayisi - rakam_sayisi;
            sembol_skoru = Math.Min(2, sembol_sayisi) * 10;

            parola_skoru = kucuk_harf_skoru + buyuk_harf_skoru + rakam_skoru + sembol_skoru;

            if (sifre.Length == 9)
                parola_skoru += 10;
            else if (sifre.Length == 10)
                parola_skoru += 20;

            if (kucuk_harf_skoru == 0 || buyuk_harf_skoru == 0 || rakam_skoru == 0 || sembol_skoru == 0)
                label22.Text = "Büyük harf, küçük harf, rakam ve sembol mutlaka kullanmalısın!";
            if (kucuk_harf_skoru != 0 && buyuk_harf_skoru != 0 && rakam_skoru != 0 && sembol_skoru != 0)
                label22.Text = "";

            if (parola_skoru < 70)
                parola_seviyesi = "Kabul Edilemez!";
            else if (parola_skoru == 70 || parola_skoru == 80)
                parola_seviyesi = "Güçlü";
            else if (parola_skoru == 90 || parola_skoru == 100)
                parola_seviyesi = "Çok Güçlü";

            label9.Text = "%" + Convert.ToString(parola_skoru);
            label10.Text = parola_seviyesi;
            progressBar1.Value = parola_skoru;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text != textBox5.Text)
                errorProvider1.SetError(textBox6, "Parola tekrarı eşleşmiyor!");
            else
                errorProvider1.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string yetki = "";
            bool kayitkontrol = false;

            baglantim.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar where tcno='" + textBox1.Text + "'", baglantim);
            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
            while (kayitokuma.Read())
            {
                kayitkontrol = true;
                break;
            }
            baglantim.Close();

            if (kayitkontrol == false)
            {
                //Tc Kimlik no kontrolü
                if (textBox1.Text.Length < 11 || textBox1.Text == "")
                {
                    label1.ForeColor = Color.Red;
                }
                else
                    label1.ForeColor = Color.Black;

                //adı veri kontrolü
                if (textBox2.Text.Length < 2 || textBox2.Text == "")
                {
                    label2.ForeColor = Color.Red;
                }
                else
                    label2.ForeColor = Color.Black;

                //soyadı veri kontrolü
                if (textBox3.Text.Length < 2 || textBox3.Text == "")
                {
                    label3.ForeColor = Color.Red;
                }
                else
                    label3.ForeColor = Color.Black;

                //kullanıcı adı veri kontrolü
                if (textBox4.Text.Length != 8 || textBox4.Text == "")
                {
                    label5.ForeColor = Color.Red;
                }
                else
                    label5.ForeColor = Color.Black;

                //parola veri kontrolü
                if (textBox5.Text == "" || parola_skoru < 70)
                {
                    label6.ForeColor = Color.Red;
                }
                else
                    label6.ForeColor = Color.Black;

                //parola tekrar veri kontrolü
                if (textBox6.Text == "" || textBox5.Text != textBox6.Text)
                {
                    label7.ForeColor = Color.Red;
                }
                else
                    label7.ForeColor = Color.Black;

                if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" && textBox3.Text.Length > 1 && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox5.Text == textBox6.Text && parola_skoru >= 70)
                {
                    if (radioButton1.Checked == true)
                        yetki = "Yönetici";
                    else if (radioButton2.Checked == true)
                        yetki = "Kullanıcı";
                    try
                    {
                        baglantim.Open();
                        OleDbCommand eklekomutu = new OleDbCommand("insert into kullanicilar values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + yetki + "','" + textBox4.Text + "','" + textBox5.Text + "')", baglantim);

                        //ekle komutunun sonuçlarını access tablomuza işliyoruz.
                        eklekomutu.ExecuteNonQuery();
                        baglantim.Close();
                        MessageBox.Show("Yeni kullanıcı kaydı oluşturuldu!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        topPage1_temizle();
                        kullanicilari_goster();
                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message);
                        baglantim.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Kırmızı ile işaretlenmiş alanları kontrol ediniz!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Girilen TC Kimlik Numarası daha önceden kayıtlıdır!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ara tuşuna bastığımızda girdiğimiz tc numarasıyla eşleşen personelin bilgilerini getirecek
            bool kayit_arama_durumu = false;
            if (textBox1.Text.Length == 11)
            {
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar where tcno='" + textBox1.Text + "'", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();

                //sorgunun sonucunda herhangi bir eşleşme varsa kayitokuma.read komutu true dönecek
                while (kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    textBox2.Text = kayitokuma.GetValue(1).ToString();
                    textBox3.Text = kayitokuma.GetValue(2).ToString();
                    if (kayitokuma.GetValue(3).ToString() == "Yönetici")
                        radioButton1.Checked = true;
                    else
                        radioButton2.Checked = true;
                    textBox4.Text = kayitokuma.GetValue(4).ToString();
                    textBox5.Text = kayitokuma.GetValue(5).ToString();
                    textBox6.Text = kayitokuma.GetValue(5).ToString();
                    break;
                }
                if (kayit_arama_durumu == false)
                    MessageBox.Show("Aranan kayıt bulunamadı!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                baglantim.Close();
            }
            else
            {
                MessageBox.Show("Lütfen 11 haneli bir TC Kimlik No giriniz!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                topPage1_temizle();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string yetki = "";

            //Tc Kimlik no kontrolü
            if (textBox1.Text.Length < 11 || textBox1.Text == "")
            {
                label1.ForeColor = Color.Red;
            }
            else
                label1.ForeColor = Color.Black;

            //adı veri kontrolü
            if (textBox2.Text.Length < 2 || textBox2.Text == "")
            {
                label2.ForeColor = Color.Red;
            }
            else
                label2.ForeColor = Color.Black;

            //soyadı veri kontrolü
            if (textBox3.Text.Length < 2 || textBox3.Text == "")
            {
                label3.ForeColor = Color.Red;
            }
            else
                label3.ForeColor = Color.Black;

            //kullanıcı adı veri kontrolü
            if (textBox4.Text.Length != 8 || textBox4.Text == "")
            {
                label5.ForeColor = Color.Red;
            }
            else
                label5.ForeColor = Color.Black;

            //parola veri kontrolü
            if (textBox5.Text == "" || parola_skoru < 70)
            {
                label6.ForeColor = Color.Red;
            }
            else
                label6.ForeColor = Color.Black;

            //parola tekrar veri kontrolü
            if (textBox6.Text == "" || textBox5.Text != textBox6.Text)
            {
                label7.ForeColor = Color.Red;
            }
            else
                label7.ForeColor = Color.Black;

            if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" && textBox3.Text.Length > 1 && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox5.Text == textBox6.Text && parola_skoru >= 70)
            {
                if (radioButton1.Checked == true)
                    yetki = "Yönetici";
                else if (radioButton2.Checked == true)
                    yetki = "Kullanıcı";
                try
                {
                    baglantim.Open();
                    OleDbCommand guncellekomutu = new OleDbCommand("update kullanicilar set ad='" + textBox2.Text + "',soyad='" + textBox3.Text + "',yetki='" + yetki + "',kullaniciadi='" + textBox4.Text + "',parola='" + textBox5.Text + "' where tcno='" + textBox2.Text + "'", baglantim);

                    //güncelle komutunun sonuçlarını access tablomuza işliyoruz.
                    guncellekomutu.ExecuteNonQuery();
                    baglantim.Close();
                    MessageBox.Show("Kullanıcı bilgileri güncellendi!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    topPage1_temizle();
                    kullanicilari_goster();
                }
                catch (Exception hatamsj)
                {
                    MessageBox.Show(hatamsj.Message, "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglantim.Close();
                }
            }
            else
            {
                MessageBox.Show("Kırmızı ile işaretlenmiş alanları kontrol ediniz!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 11)
            {
                bool kayit_arama_durumu = false;
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar where tcno='" + textBox1.Text + "'", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    OleDbCommand deletesorgu = new OleDbCommand("delete from kullanicilar where tcno='" + textBox1.Text + "'", baglantim);
                    deletesorgu.ExecuteNonQuery();
                    MessageBox.Show("Kullanıcı kaydı silindi!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglantim.Close();
                    kullanicilari_goster();
                    topPage1_temizle();
                    break;
                }
                if (kayit_arama_durumu == false)
                {
                    MessageBox.Show("Silinecek kayıt bulunamadı!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                baglantim.Close();
                topPage1_temizle();
            }
            else
            {
                MessageBox.Show("Lütfen 11 karakterden oluşan bir TC Kimlik No giriniz!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            topPage1_temizle();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //openfiledialog nesnesinin bütün özelliklerini barındıran resimsec isimli bir openfiledialog nesnesi oluşturuyoruz
            OpenFileDialog resimsec = new OpenFileDialog();
            resimsec.Title = "Personel resmi seçiniz.";
            resimsec.Filter = "JPG Dosyalar(*.jpg) | *.jpg";
            if (resimsec.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox2.Image = new Bitmap(resimsec.OpenFile());
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string cinsiyet = "";

            //daha önceden eklenmiş böyle bir kullanıcı var mı?
            bool kayitkontrol = false;

            baglantim.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select * from personeller where tcno='" + maskedTextBox1.Text + "'", baglantim);
            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
            while (kayitokuma.Read() == true)
            {
                kayitkontrol = true;
                break;
            }
            baglantim.Close();

            if (kayitkontrol == false)
            {
                if (pictureBox2.Image == null)
                    button6.ForeColor = Color.Red;
                else
                    button6.ForeColor = Color.Black;

                if (maskedTextBox1.MaskCompleted == false)
                    label13.ForeColor = Color.Red;
                else
                    label13.ForeColor = Color.Black;

                if (maskedTextBox2.MaskCompleted == false)
                    label14.ForeColor = Color.Red;
                else
                    label14.ForeColor = Color.Black;

                if (maskedTextBox3.MaskCompleted == false)
                    label15.ForeColor = Color.Red;
                else
                    label15.ForeColor = Color.Black;

                if (comboBox1.Text == "")
                    label17.ForeColor = Color.Red;
                else
                    label17.ForeColor = Color.Black;

                if (comboBox2.Text == "")
                    label19.ForeColor = Color.Red;
                else
                    label19.ForeColor = Color.Black;

                if (comboBox3.Text == "")
                    label20.ForeColor = Color.Red;
                else
                    label20.ForeColor = Color.Black;

                if (maskedTextBox4.MaskCompleted == false)
                    label21.ForeColor = Color.Red;
                else
                    label21.ForeColor = Color.Black;

                if (int.Parse(maskedTextBox4.Text) < 1000)
                    label21.ForeColor = Color.Red;
                else
                    label21.ForeColor = Color.Black;

                if (pictureBox2.Image != null && maskedTextBox1.MaskCompleted != false && maskedTextBox2.MaskCompleted != false && maskedTextBox3.MaskCompleted != false && comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && maskedTextBox4.MaskCompleted != false)
                {
                    if (radioButton3.Checked == true)
                        cinsiyet = "Bay";
                    else if (radioButton4.Checked == true)
                        cinsiyet = "Bayan";
                    try
                    {
                        baglantim.Open();
                        OleDbCommand eklemekomutu = new OleDbCommand("insert into personeller values('" + maskedTextBox1.Text + "','" + maskedTextBox2.Text + "','" + maskedTextBox3.Text + "','" + cinsiyet + "','" + comboBox1.Text + "','" + dateTimePicker1.Text + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + maskedTextBox4.Text + "')", baglantim);
                        eklemekomutu.ExecuteNonQuery();
                        baglantim.Close();

                        //başına ünlem koyarsak false değeri dönerse demek.
                        if (!Directory.Exists(Application.StartupPath + "\\personelresimler"))
                            Directory.CreateDirectory(Application.StartupPath + "\\personelresimler");
                        pictureBox2.Image.Save(Application.StartupPath + "\\personelresimler\\" + maskedTextBox1.Text + ".jpg");
                        MessageBox.Show("Yeni personel kaydı oluşturuldu", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        personelleri_goster();
                        topPage2_temizle();
                        maskedTextBox4.Text = "0";
                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message, "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        baglantim.Close();
                    }
                }
                else
                    MessageBox.Show("Yazı rengi kırmızı olan alanları yeniden gözden geçiriniz!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Girilen TC Kimlik Numarası daha önceden kayıtlıdır!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bool kayit_arama_durumu = false;
            if (maskedTextBox1.Text.Length == 11)
            {
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from personeller where tcno='" + maskedTextBox1.Text + "'", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    try
                    {
                        pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\" + kayitokuma.GetValue(0).ToString() + ".jpg");
                    }
                    catch
                    {
                        pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\resimyok.jpg");
                    }
                    maskedTextBox2.Text = kayitokuma.GetValue(1).ToString();
                    maskedTextBox3.Text = kayitokuma.GetValue(2).ToString();

                    if (kayitokuma.GetValue(3).ToString() == "Bay")
                        radioButton3.Checked = true;
                    else
                        radioButton4.Checked = true;

                    comboBox1.Text = kayitokuma.GetValue(4).ToString();
                    dateTimePicker1.Text = kayitokuma.GetValue(5).ToString();
                    comboBox2.Text = kayitokuma.GetValue(6).ToString();
                    comboBox3.Text = kayitokuma.GetValue(7).ToString();
                    maskedTextBox4.Text = kayitokuma.GetValue(8).ToString();
                    break;
                }
                if (kayit_arama_durumu == false)
                    MessageBox.Show("Aranan kayıt bulunamadı!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                baglantim.Close();
            }
            else
                MessageBox.Show("11 haneli TC No giriniz!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string cinsiyet = "";


            if (pictureBox2.Image == null)
                button6.ForeColor = Color.Red;
            else
                button6.ForeColor = Color.Black;

            if (maskedTextBox1.MaskCompleted == false)
                label13.ForeColor = Color.Red;
            else
                label13.ForeColor = Color.Black;

            if (maskedTextBox2.MaskCompleted == false)
                label14.ForeColor = Color.Red;
            else
                label14.ForeColor = Color.Black;

            if (maskedTextBox3.MaskCompleted == false)
                label15.ForeColor = Color.Red;
            else
                label15.ForeColor = Color.Black;

            if (comboBox1.Text == "")
                label17.ForeColor = Color.Red;
            else
                label17.ForeColor = Color.Black;

            if (comboBox2.Text == "")
                label19.ForeColor = Color.Red;
            else
                label19.ForeColor = Color.Black;

            if (comboBox3.Text == "")
                label20.ForeColor = Color.Red;
            else
                label20.ForeColor = Color.Black;

            if (maskedTextBox4.MaskCompleted == false)
                label21.ForeColor = Color.Red;
            else
                label21.ForeColor = Color.Black;

            if (int.Parse(maskedTextBox4.Text) < 1000)
                label21.ForeColor = Color.Red;
            else
                label21.ForeColor = Color.Black;

            if (pictureBox2.Image != null && maskedTextBox1.MaskCompleted != false && maskedTextBox2.MaskCompleted != false && maskedTextBox3.MaskCompleted != false && comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && maskedTextBox4.MaskCompleted != false)
            {
                if (radioButton3.Checked == true)
                    cinsiyet = "Bay";
                else if (radioButton4.Checked == true)
                    cinsiyet = "Bayan";
                try
                {
                    baglantim.Open();
                    OleDbCommand guncellekomutu = new OleDbCommand("update personeller set ad='" + maskedTextBox2.Text + "',soyad='" + maskedTextBox3.Text + "',cinsiyet='" + cinsiyet + "',mezuniyet='" + comboBox1.Text + "',dogumtarihi='" + dateTimePicker1.Text + "',gorevi='" + comboBox2.Text + "',gorevyeri='" + comboBox3.Text + "',maasi='" + maskedTextBox4.Text + "'where tcno='" + maskedTextBox1.Text + "'", baglantim);
                    guncellekomutu.ExecuteNonQuery();
                    baglantim.Close();
                    personelleri_goster();
                    topPage2_temizle();
                    maskedTextBox4.Text = "0";
                }
                catch (Exception hatamsj)
                {
                    MessageBox.Show(hatamsj.Message, "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglantim.Close();
                }
            }
            else
                MessageBox.Show("Yazı rengi kırmızı olan alanları yeniden gözden geçiriniz!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if(maskedTextBox1.MaskCompleted==true)
            {
                bool kayit_arama_durumu = false;
                baglantim.Open();
                OleDbCommand aramasorgusu = new OleDbCommand("select * from personeller where tcno='"+maskedTextBox1.Text+"'",baglantim);
                OleDbDataReader kayitokuma = aramasorgusu.ExecuteReader();
                while(kayitokuma.Read())
                {
                    kayit_arama_durumu=true;
                    OleDbCommand deletesorgu = new OleDbCommand("delete from personeller where tcno='"+maskedTextBox1.Text+"'",baglantim);
                    deletesorgu.ExecuteNonQuery();
                    MessageBox.Show("Personel silindi!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                }
                if(kayit_arama_durumu==false) 
                    MessageBox.Show("Silinecek kayıt bulunamadı!","SKY Personel Takip Programı",MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
                personelleri_goster();
                topPage2_temizle();
                maskedTextBox4.Text = "0";
            }
            else
                MessageBox.Show("Lütfen 11 karakterden oluşan bir TC Kimlik No giriniz!","SKY Personel Takip Programı",MessageBoxButtons.OK,MessageBoxIcon.Error);
            topPage2_temizle();
            maskedTextBox4.Text = "0";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            topPage2_temizle();
        }
    }
}
