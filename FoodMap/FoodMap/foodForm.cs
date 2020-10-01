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
    public partial class FoodForm : Form
    {
        public FoodForm()
        {
            InitializeComponent();
            //this.RefreshData();
        }

        FoodMapEntities dbContext = new FoodMapEntities();


        public void RefreshData(FoodMapEntities DB)
        {
            this.bindingSource1.DataSource = DB.Foods.ToList();
            this.dataGridView1.DataSource = this.bindingSource1;
        }

        public void btnCreate_Click(object sender, EventArgs e)
        {
            createForm createForm = new createForm();
            createForm.Show();
            
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            //this.bindingSource1.DataSource = this.dbContext.Foods.ToList();
            //this.dataGridView1.DataSource = this.bindingSource1;
            this.RefreshData(this.dbContext);
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            try
            {
                byte[] bytes = ((Food)this.bindingSource1.Current).Picture;
                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                this.pictureBox1.Image = Image.FromStream(ms);
            }
            catch
            {

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.DataSource == null)
            {
                MessageBox.Show("請先選取資料");
            }
            else
            {
                this.RefreshData(this.dbContext);
                var dataIndex = this.dataGridView1.SelectedCells[0].RowIndex;
                var data = (Food)this.dataGridView1.Rows[dataIndex].DataBoundItem;
                this.dbContext.Foods.Remove(data);
                dbContext.SaveChanges();
                this.RefreshData(this.dbContext);
            }            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.DataSource == null)
            {
                MessageBox.Show("請先選取資料");
            }
            else
            {
                var dataIndex = this.dataGridView1.SelectedCells[0].RowIndex;
                var data = (Food)this.dataGridView1.Rows[dataIndex].DataBoundItem;
                //updateForm updateForm = (updateForm)Application.OpenForms["updateForm"];
                updateForm updateForm = new updateForm();
                updateForm.getData(data);
                updateForm.Show();
            }            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            var f = (from food in dbContext.Foods.AsEnumerable()
                     where (food.FoodName.Contains(txtSearch.Text))
                     || (food.District.Contains(txtSearch.Text))
                     || (food.City.Contains(txtSearch.Text))
                     select food).FirstOrDefault();
            if (f == null)
            {
                MessageBox.Show("查無資料");
            }
            else
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream(f.Picture);
                pictureBox1.Image = Image.FromStream(ms);


                var searchfood = from sf in dbContext.Foods.AsEnumerable()
                                 where (sf.FoodName.Contains(txtSearch.Text)) 
                                 || (sf.District.Contains(txtSearch.Text))
                                 || (sf.City.Contains(txtSearch.Text))
                                 select sf;

                this.dataGridView1.DataSource = null;
                //this.dataGridView1.DataSource = searchfood.ToList();
                this.bindingSource1.DataSource = searchfood.ToList();
                this.dataGridView1.DataSource = this.bindingSource1;

                var dataIndex = this.dataGridView1.SelectedCells[0].RowIndex;
                var data = (Food)this.dataGridView1.Rows[dataIndex].DataBoundItem;
            }
            
                        
        }
    }
}
