using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JogoDaMemoria2
{
    public partial class FormLogin : Form
    {

        static public string user;
        static public string pass;

        public FormLogin()
        {
            InitializeComponent();
        }

        private void usersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.usersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.usersDataSet);

        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'usersDataSet.users' table. You can move, or remove it, as needed.
            this.usersTableAdapter.Fill(this.usersDataSet.users);
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            user = textBoxUser.Text;
            pass = textBoxPass.Text;
            int res = this.usersTableAdapter.FillByLogin(this.usersDataSet.users,user, pass);
            if(res==1)
            {
                Form1 frm = new Form1();
                frm.Show();
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("Wrong username/password");
                textBoxUser.Text = "";
                textBoxPass.Text = "";
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBoxPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
                buttonConfirm_Click(sender, e);
        }

        private void linkLabelNewUser_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormNewUser frm = new FormNewUser();
            frm.ShowDialog();
        }
    }
}
