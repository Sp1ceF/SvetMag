using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityTest
{
    public partial class ProductsForm : Form
    {
        List<Tovar> Products;
        BindingSource bSource = new BindingSource();
        public ProductsForm()
        {
            InitializeComponent();
            using(SvetEntities context = new SvetEntities())
            {
                Products = context.Tovar.ToList();
            }

            dataGridView1.DataSource = Products;
        }
    }
}
