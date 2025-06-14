using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace EC_Control
{
    public partial class ConfigurarEmail : Form
    {
        public ConfigurarEmail()
        {
            InitializeComponent();
        }

        private void ConfigurarEmail_Load(object sender, EventArgs e)
        {
            CarregarEmailsConfig();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SalvarEmailsConfig();
        }

        private void CarregarEmailsConfig()
        {
            string caminho = Path.Combine(Application.StartupPath, "emails_config.json");

            if (!File.Exists(caminho))
                return;

            string json = File.ReadAllText(caminho);
            EmailConfig config = JsonConvert.DeserializeObject<EmailConfig>(json);

            txtPara.Text = string.Join(";", config.Para);
            txtCc.Text = string.Join(";", config.Cc);
        }
        private void SalvarEmailsConfig()
        {
            List<string> para = txtPara.Text
                .Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(email => email.Trim())
                .ToList();

            List<string> cc = txtCc.Text
                .Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(email => email.Trim())
                .ToList();

            EmailConfig config = new EmailConfig
            {
                Para = para,
                Cc = cc
            };

            string caminho = Path.Combine(Application.StartupPath, "emails_config.json");

            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(caminho, json);


            MessageBox.Show("Configuração salva com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
    public class EmailConfig
    {
        public List<string> Para { get; set; }
        public List<string> Cc { get; set; }
    }
}
