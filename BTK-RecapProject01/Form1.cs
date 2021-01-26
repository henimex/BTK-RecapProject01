using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTK_RecapProject01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListProducts();
            ListCategories();
        }

        private void ListCategories()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                cbxCategory.DataSource = context.Categories.ToList();
                cbxCategory.DisplayMember = "CategoryName";
                cbxCategory.ValueMember = "CategoryId";
            }
        }
        
        private void ListProducts()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                dgwProducts.DataSource = context.Products.ToList();
            }
        }
        
        private void ListProductsByCategory(int categoryId)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                dgwProducts.DataSource = context.Products.Where(p=>p.CategoryId==categoryId).ToList();
            }
        }

        private void ListProductsByName(string productName)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                dgwProducts.DataSource = context.Products.Where(p => p.ProductName.Contains(productName)).ToList();
            }
        }

        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(cbxCategory.SelectedValue.ToString());
            try
            {
                ListProductsByCategory(Convert.ToInt32(cbxCategory.SelectedValue));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                ListProducts();
            }
            else
            {
                ListProductsByName(txtSearch.Text);
            }

        }
    }
}
