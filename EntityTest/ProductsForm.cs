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
        SortableBindingList<Tovar> Products;
        SortableBindingList<Tovar> FilteredBindingList;
        public ProductsForm()
        {
            InitializeComponent();
            using (SvetEntities context = new SvetEntities())
            {
                Products = new SortableBindingList<Tovar>(context.Tovar.ToList());
            }
            comboBox1.SelectedIndex = 0;
            dataGridView1.DataSource = Products;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            switch(comboBox1.SelectedItem)
            {
                case "Имя":
                    FilteredBindingList = new SortableBindingList<Tovar>(Products.Where(x => x.Name.Contains(textBox1.Text)).ToList());
                    break;
                case "Описание":
                    FilteredBindingList = new SortableBindingList<Tovar>(Products.Where(x => x.Description.Contains(textBox1.Text)).ToList());
                    break;
                case "Поставщик":
                    FilteredBindingList = new SortableBindingList<Tovar>(Products.Where(x => x.Provider.Contains(textBox1.Text)).ToList());
                    break;
                default:
                    MessageBox.Show("Выберите тип фильтрации");
                    break;
            }
            if (FilteredBindingList != null)
            {
                dataGridView1.DataSource = FilteredBindingList;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FilteredBindingList.Clear();
            dataGridView1.DataSource = Products;
        }
    }
    
}
