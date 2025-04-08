using System;
using System.IO;
using System.Windows.Forms;
using OfficeOpenXml;

namespace EC_Control
{
    public partial class Form2 : Form
    {
        public event Action RelatorioSalvo;

        string pastaSalva;
        string nomeArq;

        public Form2(string codigoEC, string assunto, string comentario)
        {
            InitializeComponent();
            labelEC.Text = codigoEC;
            labelAssunto.Text = assunto;
            labelData.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxComentario.Text))
            {
                if (string.IsNullOrWhiteSpace(pastaSalva) || string.IsNullOrWhiteSpace(nomeArq) || !Directory.Exists(pastaSalva))
                {
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.Filter = "Excel Workbook|*.xlsx";
                        sfd.Title = "Salvar como";
                        sfd.FileName = "Relatório do dia";

                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            pastaSalva = Path.GetDirectoryName(sfd.FileName);
                            nomeArq = Path.GetFileName(sfd.FileName);
                            SalvarConfiguracao();
                        }
                        else
                        {
                            MessageBox.Show("Operação cancelada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                string caminhoArquivoLido = pastaSalva + @"\" + nomeArq;

                // Configura o EPPlus para trabalhar com arquivos Excel
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                FileInfo arquivoExcel = new FileInfo(caminhoArquivoLido);

                using (ExcelPackage package = new ExcelPackage(arquivoExcel))
                {
                    ExcelWorksheet worksheet;

                    // Se o arquivo já existir, usa a planilha existente
                    if (arquivoExcel.Exists)
                    {
                        worksheet = package.Workbook.Worksheets[0]; // Primeira aba
                    }
                    else
                    {
                        // Cria uma nova planilha
                        worksheet = package.Workbook.Worksheets.Add("Resoluções");
                        worksheet.Cells[1, 1].Value = "Código EC";
                        worksheet.Cells[1, 2].Value = "Comentário";
                        worksheet.Cells[1, 3].Value = "Data";
                        worksheet.Cells[1, 4].Value = "Notificação";
                    }

                    // Encontra a próxima linha vazia
                    int linha = worksheet.Dimension?.End.Row + 1 ?? 2;

                    // Adiciona os dados
                    worksheet.Cells[linha, 1].Value = labelEC.Text + " - Relatório do dia";
                    worksheet.Cells[linha, 2].Value = textBoxComentario.Text;
                    worksheet.Cells[linha, 3].Value = DateTime.Now.ToString("dd/MM/yyyy");

                    if (checkBox1.Checked)
                    {
                        worksheet.Cells[linha, 4].Value = dateTimePicker1.Value.ToString("dd/MM/yyyy");
                    }

                    // Salva o arquivo
                    package.Save();
                }



                MessageBox.Show("Dados salvos com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RelatorioSalvo?.Invoke();

                this.Close(); // Fecha a tela sem salvar
            }
            else
            {
                MessageBox.Show("O relatório não pode ser salvo em branco", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
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
                        pastaSalva = linhas[3];  // Caminho da pasta
                        nomeArq = linhas[4];  // Caminho da pasta
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

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dateTimePicker1.Enabled = true;
            }
            else
            {
                dateTimePicker1.Enabled = false;
            }
        }

        void SalvarConfiguracao()
        {
            string caminhoArquivo = Path.Combine(Application.StartupPath, "config.txt");

            if (File.Exists(caminhoArquivo))
            {
                string[] linhas = File.ReadAllLines(caminhoArquivo);

                if (linhas.Length >= 4)
                {
                    linhas[3] = pastaSalva; // Edita a linha 3
                    linhas[4] = nomeArq; // Edita a linha 4

                    File.WriteAllLines(caminhoArquivo, linhas); // Sobrescreve o arquivo

                    //Console.WriteLine("Linhas 3 e 4 foram modificadas com sucesso!");
                }
                else
                {
                    //Console.WriteLine("O arquivo não contém linhas suficientes.");
                }
            }
            else
            {
                //Console.WriteLine("O arquivo não existe.");
            }
        }
    }
}
