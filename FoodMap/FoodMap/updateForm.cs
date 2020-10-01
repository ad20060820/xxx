using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodMap
{
    public partial class updateForm : Form
    {
        public updateForm()
        {
            InitializeComponent();
          
        }
        FoodForm foodForm = (FoodForm)Application.OpenForms["FoodForm"];

        FoodMapEntities dbContext = new FoodMapEntities();
        //FoodForm foodForm = (FoodForm)Application.OpenForms["FoodForm"];
        //FoodForm foodForm = new FoodForm();
        private int idData = 0;
        public void getData(Food data)
        {            
            txtFoodNameUpdate.Text = data.FoodName;
            txtCityUpdate.Text = data.City;
            txtDistrictUpdate.Text = data.District;
            txtAddressUpdate.Text = data.Address;
            txtStoreNameUpdate.Text = data.StoreName;

            System.IO.MemoryStream ms = new System.IO.MemoryStream(data.Picture);
            pictureBox1.Image = Image.FromStream(ms);

            txtPhoneUpdate.Text = data.Phone;
            rtxtDescriptionUpdate.Text = data.Description;
            rtxtRemarkUpdate.Text =data.Remark;
            idData = data.FoodId;
        }

        private void btnOpenFileUpdate_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox1.Image = Image.FromFile(this.openFileDialog1.FileName);
            }
        }

        private void btnOKUpdate_Click(object sender, EventArgs e)
        {           
            if (txtFoodNameUpdate.Text == "" || txtCityUpdate.Text == "" || txtDistrictUpdate.Text == "" || txtAddressUpdate.Text == "")
            {
                MessageBox.Show("必填資料未填完");
            }
            else
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                this.pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] bytes = ms.GetBuffer();

                var f = (from food in dbContext.Foods
                        where food.FoodId == idData
                        select food).FirstOrDefault();


                f.FoodName = txtFoodNameUpdate.Text;
                f.City = txtCityUpdate.Text;
                f.District = txtDistrictUpdate.Text;
                f.Address = txtAddressUpdate.Text;
                f.StoreName = txtStoreNameUpdate.Text;
                f.Picture = bytes;
                f.Phone = txtPhoneUpdate.Text;
                f.Description = rtxtDescriptionUpdate.Text;
                f.Remark = rtxtRemarkUpdate.Text;
                
                
                //dbContext.Foods.Add(food);
                this.dbContext.SaveChanges();                                
                foodForm.RefreshData(this.dbContext);
                this.Close();
            }
        }
        //public static Bitmap ByteToImage(byte[] blob)
        //{
        //    MemoryStream mStream = new MemoryStream();
        //    byte[] pData = blob;
        //    mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
        //    Bitmap bm = new Bitmap(mStream, false);
        //    mStream.Dispose();
        //    return bm;
        //}
    }
}
