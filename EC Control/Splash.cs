using System;
using System.Windows.Forms;

namespace EC_Control
{
    public partial class Splash : Form
    {
        int cont = 0;

        public Splash()
        {
            InitializeComponent();

            // Inicializa o Timer já ativado
            timer1.Interval = 50; // velocidade da animação
            timer1.Enabled = true;

            // Configurações visuais para fundo transparente
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.StartPosition = FormStartPosition.CenterScreen;
            //this.BackColor = System.Drawing.Color.White;
            //this.TransparencyKey = System.Drawing.Color.White;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (cont < 100)
            {
                cont += 2;
                // Aqui você pode animar uma barra de progresso, por exemplo
                // progressBar1.Value = cont;
            }
            else
            {
                timer1.Stop();
                this.Hide(); // Oculta o splash

                // Abre o formulário principal
                Form1 form2 = new Form1();
                form2.ShowDialog();

                this.Close(); // Encerra o splash após fechar o principal
            }
        }
    }
}
