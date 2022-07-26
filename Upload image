using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FilePopup2
{
    public partial class Form1 : Form
    {
        private OpenFileDialog ofd;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ofd = new OpenFileDialog();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(ofd.FileName);

                //Create a MemoryStream
                var ms = new MemoryStream(); //This is where we deposit the bytes

                //save bytes to ms
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                //to get the bytes we type
                var bytes = ms.ToArray();

                //we can now save the byte array to a db, file, or transport (stream) it



                //Decoder

                //we need to create a MemoryStream with byte array
                var imageMemoryStream = new MemoryStream(bytes);

                //now we create an image from Stream
                Image imgFromStream = Image.FromStream(imageMemoryStream);

                this.image.Image = imgFromStream;
                fileLocation.Text = ofd.FileName;
                
                //The encoder and decoder stuff is just to check if the conversion to byte array still works
                // can replace this.image.Image = imgFromStream; with this.image.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void btnPopup_Click(object sender, EventArgs e)
        {
            try
            {
                using(imagePopup form = new imagePopup())
                {
                    
                    form.ShowDialog();
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
