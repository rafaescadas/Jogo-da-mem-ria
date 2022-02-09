using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace JogoDaMemoria2
{
    public partial class FormJogo : Form
    {
        public FormJogo()
        {
            InitializeComponent();
        }

        private void FormJogo_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'usersDataSet.games' table. You can move, or remove it, as needed.
            this.gamesTableAdapter.Fill(this.usersDataSet.games);
            AtualizarFoto();
        }

        private void gamesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.gamesBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.usersDataSet);
                MessageBox.Show("Registro salvo com sucesso!", "Gravar jogo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível gravar! Erro: " + ex.Message, "Erro ao gravar",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem a certeza que pretende eliminar o registro atual?", "Apagar registo", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                this.Validate();
                this.gamesBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.usersDataSet);
                MessageBox.Show("Registo eliminado com sucesso!", "Apagar utilizador",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.gamesTableAdapter.Fill(this.usersDataSet.games);
            }
        }

        private void textBoxNome_TextChanged(object sender, EventArgs e)
        {
            this.gamesTableAdapter.FillByQGames(this.usersDataSet.games,
                textBoxNome.Text);
        }

        private void gamesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string FilePath = (string)gamesDataGridView.Rows[e.RowIndex].Cells[4].Value;
            System.Diagnostics.Process.Start(FilePath);
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Hide();
        }

        private void buttonFoto_Click(object sender, EventArgs e)
        {
            string nFoto, nCaminho;
            string pathP = Application.StartupPath.ToString() + "\\Fotos\\";
            if (!Directory.Exists(pathP))
                Directory.CreateDirectory(pathP);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                nCaminho = openFileDialog1.FileName;
                if (File.Exists(nCaminho))
                {
                    nFoto = Path.GetFileName(nCaminho);
                    File.Copy(nCaminho, pathP + nFoto, true);
                    fotoTextBox.Text = nFoto;
                    pictureBox1.Image = Image.FromFile(pathP + nFoto);
                }
                else
                    pictureBox1.Image = null;
            }
        }

        private void AtualizarFoto()
        {
            string nFoto;
            string pathP = Application.StartupPath.ToString() + "\\Fotos\\";
            if (!Directory.Exists(pathP))
                Directory.CreateDirectory(pathP);
            nFoto = fotoTextBox.Text;
            if (File.Exists(pathP + nFoto))
                pictureBox1.Image = Image.FromFile(pathP + nFoto);
            else
                pictureBox1.Image = null;
        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            AtualizarFoto();
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            AtualizarFoto();
        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            AtualizarFoto();
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            AtualizarFoto();
        }
    }
}
