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
    public partial class FormRegisterUser : Form
    {
        public FormRegisterUser()
        {
            InitializeComponent();
        }

        private void FormRegisterUser_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'usersDataSet.users' table. You can move, or remove it, as needed.
            this.usersTableAdapter.Fill(this.usersDataSet.users);
        }


        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Tem a certeza que pretende eliminar o registro atual?","Apagar registo",MessageBoxButtons.YesNo,MessageBoxIcon.Question) 
                ==DialogResult.Yes)
            {
                this.Validate();
                this.usersBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.usersDataSet);
                MessageBox.Show("Registo eliminado com sucesso!", "Apagar utilizador",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.usersTableAdapter.Fill(this.usersDataSet.users);
            }
        }

        private void textBoxUsername_TextChanged(object sender, EventArgs e)
        {
            this.usersTableAdapter.FillByQUser(this.usersDataSet.users,
                textBoxUsername.Text);
        }

        private void usersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.usersBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.usersDataSet);
                MessageBox.Show("Registro salvo com sucesso!", "Gravar utilizador",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível gravar! Erro: " + ex.Message, "Erro ao gravar",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
