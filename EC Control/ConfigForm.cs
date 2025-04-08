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
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string pastaAta = txtPastaAta.Text.Trim();
            string pastaEC = txtPastaEC.Text.Trim();
            string nomeArqEC = txtnomeArqEC.Text.Trim();
            string pastaRLD = txtPastaRLD.Text.Trim();
            string nomeArqRLD = txtnomeArqRLD.Text.Trim();
            string cellInicio = numericUpDown1.Value.ToString();
            string dataLinha = numericUpDown2.Value.ToString();
            string dataColuna = numericUpDown3.Value.ToString();
            string codEC = numericUpDown6.Value.ToString();
            string assunto = numericUpDown5.Value.ToString();
            string descric = numericUpDown4.Value.ToString();

            string txtBoxEC = txtCaminho1.Text.Trim();
            string txtBoxQualidade = txtCaminho2.Text.Trim();
            string txtBoxProjetosM = txtCaminho3.Text.Trim();
            string txtBoxProjetosE = txtCaminho4.Text.Trim();
            string txtBoxRelatorios = txtCaminho5.Text.Trim();

            string caminhoArquivo = Path.Combine(Application.StartupPath, "config.txt");

            if (string.IsNullOrEmpty(pastaAta))
            {
                MessageBox.Show("Por favor, insira o caminho da pasta.");
                return;
            }

            try
            {
                // Grava o caminho no arquivo
                File.WriteAllLines(caminhoArquivo, new string[]
                {
                   pastaAta,
                   pastaEC,
                   nomeArqEC,
                   pastaRLD,
                   nomeArqRLD,
                   cellInicio,
                   dataLinha,
                   dataColuna,
                   codEC,
                   assunto,
                   descric
                });

                MessageBox.Show("Configuração salva com sucesso. Por favor, reinicie o programa.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Fecha a tela de configuração
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar configuração: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            List<PastaInfo> pastas = new List<PastaInfo>();

            for (int i = 1; i <= 10; i++)
            {
                TextBox txtNome = (TextBox)this.Controls.Find($"txtNome{i}", true).FirstOrDefault();
                TextBox txtCaminho = (TextBox)this.Controls.Find($"txtCaminho{i}", true).FirstOrDefault();

                if (txtNome != null && txtCaminho != null &&
                    !string.IsNullOrWhiteSpace(txtNome.Text) &&
                    !string.IsNullOrWhiteSpace(txtCaminho.Text))
                {
                    pastas.Add(new PastaInfo
                    {
                        Nome = txtNome.Text.Trim(),
                        Caminho = txtCaminho.Text.Trim()
                    });
                }
            }

            string json = JsonConvert.SerializeObject(pastas, Formatting.Indented);
            File.WriteAllText("pastas.json", json);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Fecha a tela sem salvar
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            string caminhoArquivo = Path.Combine(Application.StartupPath, "config.txt");

            if (File.Exists(caminhoArquivo))
            {
                try
                {
                    // Lê todas as linhas do arquivo
                    string[] linhas = File.ReadAllLines(caminhoArquivo);

                    // Verifica se o arquivo tem as linhas esperadas
                    if (linhas.Length >= 3)
                    {
                        int i = 0;

                        string pastaAta = linhas[i++];
                        string pastaEC = linhas[i++];
                        string nomeArqEC = linhas[i++];
                        string pastaRLD = linhas[i++];
                        string nomeArqRLD = linhas[i++];
                        string cellInicio = linhas[i++];  // Configuração adicional
                        string dataLinha = linhas[i++];  // Nome do usuário
                        string dataColuna = linhas[i++];  // Nome do usuário
                        string codEC = linhas[i++];  // Configuração adicional
                        string assunto = linhas[i++];  // Nome do usuário
                        string descric = linhas[i++];  // Nome do usuário

                        txtPastaAta.Text = pastaAta;
                        txtPastaEC.Text = pastaEC;
                        txtnomeArqEC.Text = nomeArqEC;
                        txtPastaRLD.Text = pastaRLD;
                        txtnomeArqRLD.Text = nomeArqRLD;
                        numericUpDown1.Value = int.Parse(cellInicio);
                        numericUpDown2.Value = int.Parse(dataLinha);
                        numericUpDown3.Value = int.Parse(dataColuna);
                        numericUpDown4.Value = int.Parse(descric);
                        numericUpDown5.Value = int.Parse(assunto);
                        numericUpDown6.Value = int.Parse(codEC);

                    }

                    if (!File.Exists("pastas.json"))
                        return;

                    string json = File.ReadAllText("pastas.json");
                    var pastas = JsonConvert.DeserializeObject<List<PastaInfo>>(json);

                    for (int i = 0; i < pastas.Count && i < 10; i++)
                    {
                        TextBox txtNome = (TextBox)this.Controls.Find($"txtNome{i + 1}", true).FirstOrDefault();
                        TextBox txtCaminho = (TextBox)this.Controls.Find($"txtCaminho{i + 1}", true).FirstOrDefault();

                        if (txtNome != null && txtCaminho != null)
                        {
                            txtNome.Text = pastas[i].Nome;
                            txtCaminho.Text = pastas[i].Caminho;
                        }
                    }

                }                
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao carregar configuração: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Caminho da pasta não encontrado. Por favor, configure o caminho nas configurações.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
    public class PastaInfo
    {
        public string Nome { get; set; }
        public string Caminho { get; set; }
    }
}
