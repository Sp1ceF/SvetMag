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
    public partial class MainForm : Form
    {
        public Users loggedUser;
        public MainForm()
        {
            InitializeComponent();
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm form = new LoginForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                loggedUser = form.user;
                label3.Text = $"Аккаунт: {loggedUser.Login} \nУровень доступа: {loggedUser.Authority}";
            }
        }

        private void продукцияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductsForm productsForm = new ProductsForm();
            productsForm.Show();
        }
    }
}
