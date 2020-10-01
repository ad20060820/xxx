using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodMap
{
    public partial class createForm : Form
    {
        public createForm()
        {
            InitializeComponent();
        }

        FoodMapEntities dbContext = new FoodMapEntities();
        FoodForm foodForm = (FoodForm)Application.OpenForms["FoodForm"];
        //FoodForm foodform = new FoodForm();

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(this.openFileDialog1.FileName);
                this.pictureBox1.Image = Image.FromFile(this.openFileDialog1.FileName);
            }
        }

        private void btnCreateOK_Click(object sender, EventArgs e)
        {

            if (txtFoodName.Text == "" || txtCity.Text == "" || txtDistrict.Text == "" || txtAddress.Text == "")
            {
                MessageBox.Show("必填資料未填完");
            }
            else
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                this.pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] bytes = ms.GetBuffer();

                Food food = new Food
                {
                    FoodName = txtFoodName.Text,
                    City = txtCity.Text,
                    District = txtDistrict.Text,
                    Address = txtAddress.Text,
                    StoreName = txtStoreName.Text,
                    Picture = bytes,
                    Phone = txtPhone.Text,
                    Description = rtxtDescription.Text,
                    Remark = rtxtRemark.Text
                };
                dbContext.Foods.Add(food);
                dbContext.SaveChanges();                
                this.Close();
                
                foodForm.RefreshData(this.dbContext);
            }
        }
    }
}
