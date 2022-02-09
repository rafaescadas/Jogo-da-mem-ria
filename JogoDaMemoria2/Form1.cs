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
    public partial class Form1 : Form
    {
        // Use this Random object to choose random icons for the squares
        Random random = new Random();

        // Each of these letters is an interesting icon
        // in the Webdings font,
        // and each icon appears twice in this list
        List<string> icons = new List<string>()
        {
                "!", "!", "N", "N", ",", ",", "k", "k",
                "b", "b", "v", "v", "w", "w", "z", "z"
        };

        Label firstClicked = null;
        Label secondClicked = null;
        int score; 
          
        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        private void AssignIconsToSquares()
        {            
            foreach(Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if(iconLabel!=null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }

            }
        }

        private void label_Click(object sender, EventArgs e)
        {

            if(timer1.Enabled==true)
            {
                return;
            }

            Label clickedLabel = sender as Label;
           
            if(clickedLabel!=null)
            {
                if(clickedLabel.ForeColor==Color.Black)
                {
                    return;
                }
                if(firstClicked==null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }

                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black; 
                CheckForWinner();

                if (firstClicked.Text==secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    score = int.Parse(textBoxScore.Text);
                    score += 20;
                    textBoxScore.Text = score.ToString();
                    return;
                }
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinner()
        {            
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            MessageBox.Show("Parabéns!! Conseguiste passar o nível.");
            score = int.Parse(textBoxScore.Text);
            score += 150;
            textBoxScore.Text = score.ToString();    
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void inToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLogin frm = new FormLogin();
            frm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.usersTableAdapter.Fill(this.usersDataSet.users);
            this.usersTableAdapter.FillByLogin(this.usersDataSet.users, FormLogin.user, FormLogin.pass);
            userToolStripMenuItem.Text = "User: " + FormLogin.user;
            if (FormLogin.user == "admin")
            {
                userRegisterToolStripMenuItem.Enabled = true;
                jogosToolStripMenuItem.Enabled = true;
            }               
        }

        private void userRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRegisterUser frm = new FormRegisterUser();
            frm.ShowDialog();
        }

        private void outToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveandLogOut();
        }

        private void SaveandLogOut()
        {
            try
            {
                this.Validate();
                this.usersBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.usersDataSet);
                
            }           
            catch(Exception ex)
            {
                MessageBox.Show("Não foi possível gravar! Erro: " + ex.Message, "Erro ao gravar",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }

        private void jogosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormJogo frm = new FormJogo();
            frm.ShowDialog();
        }
    }
}
