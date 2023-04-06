using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuideLoadPicDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
           /* openFileDialog1.ShowDialog();
            string file = openFileDialog1.FileName;
            if (string.IsNullOrEmpty(file))
                return;
            Image myImage = Image.FromFile(file);
            pictureBox1.Image = myImage;*/
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // chuyenr file anh thanh binary 

            MemoryStream stream = new MemoryStream();
            pictureBox1.Image.Save(stream,ImageFormat.Jpeg);// luu stream va 1 cai dinh dang 

            // khoi tap doi tuong 
            // luu vao db 
            testPicDBEntities db = new testPicDBEntities();
            TestAnh anh = new TestAnh();
            anh.id = 3;
            anh.ten = "ngon";
            // tra ve 1 mang byte 
            anh.image = stream.ToArray();
            db.TestAnhs.Add(anh);
            db.SaveChanges();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            // load anh tu DB len picture
            testPicDBEntities db = new testPicDBEntities();
            TestAnh ta = db.TestAnhs.Where(p => p.ten.Equals("ngon")).FirstOrDefault();
            if (ta == null)
                return;
            // chuyen thanh 1 mang byte 
            MemoryStream memoryStream = new MemoryStream(ta.image.ToArray()); 
            // khoi tao 1 doi tuogn anh 
            Image img = Image.FromStream(memoryStream);
            // gan lai
            pictureBox2.Image = img;
        }

        private void btnCapnhat_Click(object sender, EventArgs e)
        {
            testPicDBEntities db = new testPicDBEntities();
            dataGridView1.DataSource = (from u in db.TestAnhs
                                       select new
                                       {
                                           u.ten,
                                           u.id,
                                           u.image
                                       }).ToList();
        }
    }
}
