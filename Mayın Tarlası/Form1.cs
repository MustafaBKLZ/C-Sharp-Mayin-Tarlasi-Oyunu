using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mayın_Tarlası
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random rnd = new Random();
        List<int> mayın = new List<int>();
        int puan, dtiklanan = 0, mayinsayisi = 0, kutu = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
        }

       

        public void Oyna(string mod)
        {
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.Controls.Clear();
            mayın.Clear();
            dtiklanan = 0;
            mayinsayisi = 0;
            kutu = 0;
            puan = 0;
            label4.Text = "PUAN: 0";

            tableLayoutPanel1.ColumnCount = 10;
            tableLayoutPanel1.RowCount = 10;

            if (mod == "kolay") mayinsayisi = 10;
            else if (mod == "orta") mayinsayisi = 25;
            else if (mod == "zor") mayinsayisi = 40;

            for (int i = 0; i < tableLayoutPanel1.ColumnCount; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

                for (int x = 0; x < tableLayoutPanel1.RowCount; x++)
                {
                    if (i == 0)
                    {
                        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                    }
                    Button btn = new Button();
                    btn.BackColor = Color.Gray;
                    btn.Dock = DockStyle.Fill;
                    btn.Name = kutu.ToString();
                    tableLayoutPanel1.Controls.Add(btn, i, x);
                    kutu++;
                }
            }
            int randomsayi;
            for (int i = 0; i < mayinsayisi; i++)
            {
                do
                {
                    randomsayi = rnd.Next(1, (tableLayoutPanel1.ColumnCount * tableLayoutPanel1.RowCount) - 1);

                } while (mayın.Contains(randomsayi));

                mayın.Add(randomsayi);
            }

            ButtonClickAyarla();
        }
        private void ButtonClickAyarla()
        {
            foreach (Control ctl in this.tableLayoutPanel1.Controls)
            {
                ctl.MouseClick += new MouseEventHandler(Form1_MouseClick);
            }
        }
        void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Event(sender);
        }
        private void Event(object sender)
        {
            if (sender.GetType().Name == "Button")
            {
                Button tiklanan = (Button)sender;
                if (tiklanan.BackColor != Color.Green && tiklanan.BackColor != Color.Red)
                {
                    string isim = tiklanan.Name;
                    if (mayın.IndexOf(Convert.ToInt32(isim)) != -1) // Kaybettin
                    {
                        tiklanan.Image = Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\mayin.PNG");
                        HepsiniAc();
                        MessageBox.Show("Kaybettiniz. \nToplam Puan: " + puan);
                    }
                    else // Kazandın
                    {
                        tiklanan.BackColor = Color.Green;
                        Random rnd = new Random();
                        int tikpuan = rnd.Next(1, mayinsayisi);
                        puan += tikpuan;
                        tiklanan.Text = tikpuan.ToString();
                        label4.Text = "PUAN: " + puan.ToString();

                        if (dtiklanan == ((tableLayoutPanel1.ColumnCount * tableLayoutPanel1.RowCount) - 1) - mayinsayisi)
                        {
                            HepsiniAc();
                            MessageBox.Show("Kazandınız \nToplam Puan: " + puan);
                        }
                        else
                        {
                            dtiklanan++;
                        }
                    }
                }
            }
        }

        private void HepsiniAc()
        {
            for (int i = 0; i <= (tableLayoutPanel1.ColumnCount * tableLayoutPanel1.RowCount) - 1; i++)
            {
                Button btn = (Button)tableLayoutPanel1.Controls[i];
                if (mayın.IndexOf(Convert.ToInt32(btn.Name)) != -1)
                {
                    btn.BackColor = Color.Red;
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                    btn.BackgroundImage = Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\mayin.PNG");
                }
                else
                {
                    btn.BackColor = Color.Green;
                }
            }
        }



        private void btn_oyna_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
                Oyna("kolay");
            else if (radioButton2.Checked == true)
                Oyna("orta");
            else if (radioButton3.Checked == true)
                Oyna("zor");
            else
                MessageBox.Show("Zorluk Seçin");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
                label1.Text = "Mayın Sayısı 10";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
                label1.Text = "Mayın Sayısı 25";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
                label1.Text = "Mayın Sayısı 40";
        }
    }
}
