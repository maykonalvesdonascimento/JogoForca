using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace JogoForca
{
    public partial class Form1 : Form
    {
        private static string palavra ;
        private char[] revelado;
        private int erros = 0;
        string[] palavrasaleatoria = { "Cachorro", "Gato", "Papagaio", "Coelho", "Pato", "Leopardo" };
        public Form1()
        {
            InitializeComponent();
        }

        private void txtLetra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Impede a entrada do caractere
            }
        }

        private void btnAdivinhar_Click(object sender, EventArgs e)
        {
            int modificado = 0;
            for (int i = 0; i < palavra.Length; i++) {

                String nomeTxt = "txt" + (i + 1);
                TextBox txt = this.Controls.Find(nomeTxt, true).FirstOrDefault() as TextBox;
                txt.Visible = true;
                
                if (txtLetra.Text.ToLower() == palavra[i].ToString().ToLower())
                {
                   
                    txt.Text = palavra[i].ToString();
                    revelado[i] = palavra[i];
                    modificado++;
                }
                
            }

            if (modificado == 0) {
                erros++;
                var imagem = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject((erros).ToString());
                PicForca.Image = imagem;
            }
            verificarGanhou(revelado, palavra);
            verificarPerdeu(erros);
            lblTentativas.Text = lblTentativas.Text +txtLetra.Text+",";

            txtLetra.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            Random rnd = new Random();
            palavra = palavrasaleatoria[rnd.Next(0,palavrasaleatoria.Length)];
            revelado = new char [palavra.Length]; ;

            for (int i = 0; i < palavra.Length; i++)
            {
                revelado[i] = '_';
                String nomeTxt = "txt" + (i + 1);
                TextBox txt = this.Controls.Find(nomeTxt, true).FirstOrDefault() as TextBox;
                txt.Visible = true;

            }
        }

        private void verificarGanhou(char[] revelados, string palavra) {
            string palavrarevelada="";
            for (int i = 0; i < palavra.Length; i++) {
                palavrarevelada = palavrarevelada + revelado[i].ToString();
            }
            if (palavrarevelada == palavra) {

                MessageBox.Show("Você venceu");
                btnAdivinhar.Enabled = false;
                btnReiniciar.Visible = true;

            }
        
        }

        private void verificarPerdeu(int erros) {
            if (erros == 6) {
                MessageBox.Show("Você perdeu");
                btnAdivinhar.Enabled = false;
                btnReiniciar.Visible = true;
            }
        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            sumirTodosTxt();
            PicForca.Image = null;
            btnAdivinhar.Enabled = true;
            btnReiniciar.Visible = false;
            lblTentativas.Text = "";
            erros = 0;
            Random rnd = new Random();
            palavra = palavrasaleatoria[rnd.Next(0, palavrasaleatoria.Length)];
            revelado = new char[palavra.Length]; ;

            for (int i = 0; i < palavra.Length; i++)
            {
                revelado[i] = '_';
                String nomeTxt = "txt" + (i + 1);
                TextBox txt = this.Controls.Find(nomeTxt, true).FirstOrDefault() as TextBox;
                txt.Visible = true;

            }
        }

        private void sumirTodosTxt() {
            for (int i = 1; i <= 12; i++) {
                String nomeTxt = "txt" + (i);
                TextBox txt = this.Controls.Find(nomeTxt, true).FirstOrDefault() as TextBox;
                txt.Clear();
                txt.Visible = false;
            }
        }
    }
}
