using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Data.SqlClient;


namespace WebCamFromLib
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Bitmap bitmap;
        FilterInfoCollection FilterInfoCollection;
        VideoCaptureDevice VideoCaptureDevice;
        private void Button1_Click(object sender, EventArgs e)
        {
            
            VideoCaptureDevice = new VideoCaptureDevice(FilterInfoCollection[cboCamera.SelectedIndex].MonikerString);
            VideoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            VideoCaptureDevice.Start();
            pic.Visible = true;
            Image shalom = new Bitmap(@"C:\Users\lucas.huhtala\Pictures\Shalomander.jpg");
            this.BackgroundImage = shalom;

            SqlConnection con = new SqlConnection(@"Data source=CND8263QPM\LULLESQL;Initial Catalog=18IT_Test; Integrated Security=True;");

            con.Open();
            try
            {
                if (textBox1.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("Select Texts From Texts", con);
                    SqlDataReader da = cmd.ExecuteReader();

                    while (da.Read())
                    {
                        textBox1.Text = da.GetValue(0).ToString();
                    }
                    con.Close();
                }
            }

            catch

            {
                button1.Text = "Dont you dare press this button";
                textBox1.Text = "Dont press the button";
            }

            MessageBox.Show("shalom");
        }
        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs EventArgs )
        {
            pic.Image = bitmap;
            bitmap = (Bitmap)EventArgs.Frame.Clone();

            Bitmap current = (Bitmap)bitmap.Clone();


            string filepath = @"C:\Webcamtest\" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".png";
            current.Save(filepath);

            current.Dispose();

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void BackGroundShalom(object sender, System.EventArgs e)
        {
            

        }
        private void Form1_Load(object sender, EventArgs e)
        {

            textBox1.Text = "Dont press the button";

            FilterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo FilterInfo in FilterInfoCollection)
            cboCamera.Items.Add(FilterInfo.Name);
            cboCamera.SelectedIndex = 0;




        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
