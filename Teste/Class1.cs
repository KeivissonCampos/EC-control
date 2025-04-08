using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using OfficeOpenXml; // Biblioteca EPPlus
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Reflection.Emit;

namespace EC_Control
{
    public partial class Form1 : Form
    {
        private bool ordenacaoAscendente = true;
        string pasta;
        int InitialCell;
        int DataL;
        int DataC;
        int cellEC;
        int cellAssunto;
        int cellDescric;
        string codigoEC;
        string assunto;
        string comentario;
        string dataReuniao;

        public Form1()
        {
            InitializeComponent();
            this.AcceptButton = btnPesquisar;
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // Define a licença da EPPlus
        }

        private void btnPesquisar_Click_1(object sender, EventArgs e)
        {
            Config();
            string codigoEC = txtCodigoEC.Text.Trim();
            string assuntoEC = txtAssunto.Text.Trim();
            string dataReuniao = dtpDataReuniao.Value.ToString("dd/MM/yyyy"); // Converte a data para o formato correto
            string pastaPlanilhas = pasta; // Caminho das planilhas

            // Verifica se pelo menos um critério de pesquisa foi informado
            if (string.IsNullOrEmpty(codigoEC) && string.IsNullOrEmpty(assuntoEC) && !checkBox1.Checked)
            {
                MessageBox.Show("Digite um código, um assunto ou selecione uma data para pesquisar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<ECInfo> resultados = PesquisarEC(codigoEC, assuntoEC, dataReuniao, pastaPlanilhas);

            if (resultados.Count > 0)
            {
                dgvResultados.DataSource = resultados; // Exibe os resultados no DataGridView
                dgvResultados.Columns[0].Width = 80;
                dgvResultados.Columns[1].Width = 80;
                dgvResultados.Columns[2].Width = 230;
                dgvResultados.Columns[3].Width = 500;
                dgvResultados.Columns[4].Width = 150;

                dgvResultados.CurrentCell = dgvResultados.Rows[0].Cells[0]; // Define o foco na primeira célula
                dgvResultados.Focus(); // Garante que o DataGridView receba o foco

                dgvResultados.DataSource = ((List<ECInfo>)dgvResultados.DataSource)
                    .OrderByDescending(ec => DateTime.TryParse(ec.DataReuniao, out DateTime data) ? data : DateTime.MaxValue)
                    .ToList();
            }
            else
            {
                MessageBox.Show("Nenhuma EC encontrada com os critérios informados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private List<ECInfo> PesquisarEC(string codigoEC, string assuntoEC, string dataEC, string pasta)
        {
            List<ECInfo> resultados = new List<ECInfo>();
            DirectoryInfo dir = new DirectoryInfo(pasta);

            if (!dir.Exists)
            {
                MessageBox.Show("A pasta configurada não existe. Verifique o caminho e tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return resultados;
            }
            FileInfo[] arquivos;

            if (checkBox2.Checked)
            {
                // Obtém a lista de arquivos .xlsx, excluindo inicialmente "Relatorios.xlsx"
                arquivos = dir.GetFiles("*.xlsx").Where(f => !f.Name.Equals("Relatorios.xlsx", StringComparison.OrdinalIgnoreCase)).ToArray();
            }

            // Se checkBox2 estiver marcado, adiciona o "Relatorios.xlsx" na lista
            if (checkBox2.Checked)
            {
                string caminhoRelatorio = Path.Combine(pasta, "Relatorios.xlsx");
                if (File.Exists(caminhoRelatorio))
                {
                    var listaArquivos = arquivos.ToList();
                    listaArquivos.Add(new FileInfo(caminhoRelatorio));
                    arquivos = listaArquivos.ToArray();
                }
            }

            if (arquivos.Length == 0)
            {
                MessageBox.Show("Nenhum arquivo Excel encontrado na pasta configurada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return resultados;
            }

            foreach (var arquivo in arquivos)
            {
                string tempFile = Path.Combine(Path.GetTempPath(), Path.GetFileName(arquivo.FullName));

                try
                {
                    // Copia o arquivo para um local temporário antes de abrir
                    File.Copy(arquivo.FullName, tempFile, true);

                    using (var package = new ExcelPackage(new FileInfo(arquivo.FullName)))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        int linhas = worksheet.Dimension.Rows;

                        for (int i = InitialCell; i <= linhas; i++)
                        {
                            string ecCompleto = worksheet.Cells[i, cellEC].Text.Trim();
                            string comentarios = worksheet.Cells[i, cellDescric].Text;
                            string dataReuniao = worksheet.Cells[DataL, DataC].Text;

                            // Se a célula da data for "comentário", então ler a célula 4 (coluna 3 no índice baseado em 1)
                            if (dataReuniao.Equals("Data", StringComparison.OrdinalIgnoreCase))
                            {
                                dataReuniao = worksheet.Cells[i, 3].Text.Trim();
                            }

                            string codigooEC = "";
                            string assunto = "";

                            int primeiroHifen = ecCompleto.IndexOf("-");
                            int segundoHifen = ecCompleto.IndexOf("-", primeiroHifen + 1);

                            if (primeiroHifen != -1 && segundoHifen != -1)
                            {
                                codigooEC = ecCompleto.Substring(0, segundoHifen).Trim();
                                assunto = ecCompleto.Substring(segundoHifen + 1).Trim();
                            }
                            else
                            {
                                codigooEC = ecCompleto;
                                assunto = "";
                            }

                            bool codigoEncontrado = !string.IsNullOrEmpty(txtCodigoEC.Text.Trim()) &&
                                !string.IsNullOrWhiteSpace(codigooEC) &&
                                codigooEC.IndexOf(txtCodigoEC.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0;
                            bool assuntoEncontrado = !string.IsNullOrEmpty(txtAssunto.Text.Trim()) &&
                                !string.IsNullOrWhiteSpace(assunto) &&
                                assunto.IndexOf(txtAssunto.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0;

                            bool dataEncontrada = false;

                            if (checkBox1.Checked)
                            {
                                if (DateTime.TryParse(dataReuniao, out DateTime dataReuniaoDate) &&
                                    DateTime.TryParse(dtpDataReuniao.Value.ToString("dd/MM/yyyy"), out DateTime dataECDate))
                                {
                                    dataEncontrada = dataReuniaoDate.Date == dataECDate.Date;
                                }
                            }
                            else
                            {
                                dataEncontrada = true;
                            }

                            if ((!string.IsNullOrEmpty(txtCodigoEC.Text.Trim()) && codigoEncontrado) ||
                                (!string.IsNullOrEmpty(txtAssunto.Text.Trim()) && assuntoEncontrado) ||
                                (checkBox1.Checked && !string.IsNullOrEmpty(dtpDataReuniao.Text) && dataEncontrada))
                            {
                                resultados.Add(new ECInfo
                                {
                                    CodigoEC = codigooEC,
                                    Assunto = assunto,
                                    Comentarios = comentarios,
                                    DataReuniao = dataReuniao,
                                    Arquivo = arquivo.Name,
                                });
                            }
                        }
                    }
                }
                catch (IOException)
                {
                    // MessageBox.Show($"O arquivo '{arquivo.Name}' está em uso e não pôde ser acessado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Tenta excluir o arquivo temporário após a leitura
                    try
                    {
                        if (File.Exists(tempFile))
                        {
                            File.Delete(tempFile);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir o arquivo temporário: {ex.Message}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            return resultados;
        }



        private void txtCodigoEC_TextChanged(object sender, EventArgs e)
        {
            txtCodigoEC.Text = txtCodigoEC.Text.ToUpper();
            txtCodigoEC.SelectionStart = txtCodigoEC.Text.Length; // Mantém o cursor no final do texto
        }

        private void Config()
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
                        string pastaSalva = linhas[0];  // Caminho da pasta
                        string cellInicio = linhas[1];  // Configuração adicional
                        string dataLinha = linhas[2];  // Nome do usuário
                        string dataColuna = linhas[3];  // Nome do usuário
                        string codEC = linhas[4];  // Configuração adicional
                        string assunto = linhas[5];  // Nome do usuário
                        string descric = linhas[6];  // Nome do usuário

                        pasta = pastaSalva;
                        InitialCell = int.Parse(cellInicio);
                        DataL = int.Parse(dataLinha);
                        DataC = int.Parse(dataColuna);
                        cellEC = int.Parse(codEC);
                        cellAssunto = int.Parse(assunto);
                        cellDescric = int.Parse(descric);

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

        private void configurarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigForm configForm = new ConfigForm();
            configForm.ShowDialog(); // Exibe a tela de configuração
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dtpDataReuniao.Enabled = true;
            }
            else
            {
                dtpDataReuniao.Enabled = false;
            }
        }

        private void dgvResultados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            codigoEC = dgvResultados.Rows[e.RowIndex].Cells["CodigoEC"].Value.ToString();
            assunto = dgvResultados.Rows[e.RowIndex].Cells["Assunto"].Value.ToString();
            comentario = dgvResultados.Rows[e.RowIndex].Cells["Comentarios"].Value.ToString();
            dataReuniao = dgvResultados.Rows[e.RowIndex].Cells["DataReuniao"].Value.ToString();

            AtualizarRichTextBox(codigoEC, assunto, comentario, dataReuniao);

        }

        private void dgvResultados_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            codigoEC = dgvResultados.Rows[e.RowIndex].Cells["CodigoEC"].Value.ToString();
            assunto = dgvResultados.Rows[e.RowIndex].Cells["Assunto"].Value.ToString();
            comentario = dgvResultados.Rows[e.RowIndex].Cells["Comentarios"].Value.ToString();
            dataReuniao = dgvResultados.Rows[e.RowIndex].Cells["DataReuniao"].Value.ToString();

            AtualizarRichTextBox(codigoEC, assunto, comentario, dataReuniao);

        }

        private void AtualizarRichTextBox(string codigoEC, string assunto, string comentario, string dataReuniao)
        {
            // Exibir comentário detalhado em MessageBox
            //FontStyle.Bold, FontStyle.Regular, FontStyle.Italic
            richTextBox1.Clear();

            // 📌 EC em negrito
            richTextBox1.SelectionFont = new Font("Arial", 10, FontStyle.Bold);
            richTextBox1.AppendText($"📌 EC: {codigoEC}\n\n");

            // 📖 Assunto em negrito
            richTextBox1.SelectionFont = new Font("Arial", 9, FontStyle.Bold);
            richTextBox1.AppendText("📖 Assunto:\n\n");

            // Texto normal após o título
            richTextBox1.SelectionFont = new Font("Arial", 9, FontStyle.Regular);
            richTextBox1.AppendText($"{assunto}\n\n");

            // 💬 Comentário em negrito
            richTextBox1.SelectionFont = new Font("Arial", 9, FontStyle.Bold);
            richTextBox1.AppendText("💬 Comentário:\n\n");

            // Texto normal após o título
            richTextBox1.SelectionFont = new Font("Arial", 9, FontStyle.Regular);
            richTextBox1.AppendText($"{comentario}\n\n");

            // 📅 Data em negrito
            richTextBox1.SelectionFont = new Font("Arial", 9, FontStyle.Bold);
            richTextBox1.AppendText("📅 Data:\n\n");

            // Texto normal após o título
            richTextBox1.SelectionFont = new Font("Arial", 9, FontStyle.Regular);
            richTextBox1.AppendText($"{dataReuniao}\n\n");

            // Linha separadora
            richTextBox1.AppendText("\n");

            labelEC.Text = codigoEC;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(codigoEC))
            {
                Form2 form2 = new Form2(codigoEC, assunto, comentario); // Passa o dado para o Form2
                form2.RelatorioSalvo += AtualizarTela;

                form2.ShowDialog(); // Abre o segundo formulário
            }
            else
            {
                MessageBox.Show("Associe o relatório do dia a uma EC", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void AtualizarTela()
        {
            btnPesquisar.PerformClick(); // Simula um clique no botão "Pesquisar"
        }

        private void dgvResultados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) // Verifica se o clique foi no cabeçalho
            {
                string nomeColuna = dgvResultados.Columns[e.ColumnIndex].Name;

                if (nomeColuna == "DataReuniao")
                {
                    if (ordenacaoAscendente)
                    {
                        dgvResultados.DataSource = ((List<ECInfo>)dgvResultados.DataSource)
                            .OrderBy(ec => DateTime.TryParse(ec.DataReuniao, out DateTime data) ? data : DateTime.MaxValue)
                            .ToList();
                    }
                    else
                    {
                        dgvResultados.DataSource = ((List<ECInfo>)dgvResultados.DataSource)
                            .OrderByDescending(ec => DateTime.TryParse(ec.DataReuniao, out DateTime data) ? data : DateTime.MaxValue)
                            .ToList();
                    }
                }
                else if (nomeColuna == "CodigoEC")
                {
                    if (ordenacaoAscendente)
                    {
                        dgvResultados.DataSource = ((List<ECInfo>)dgvResultados.DataSource)
                            .OrderBy(ec => ec.CodigoEC)
                            .ToList();
                    }
                    else
                    {
                        dgvResultados.DataSource = ((List<ECInfo>)dgvResultados.DataSource)
                            .OrderByDescending(ec => ec.CodigoEC)
                            .ToList();
                    }
                }

                ordenacaoAscendente = !ordenacaoAscendente; // Alterna o estado de ordenação
            }
        }

        void AbrirPasta(string nomePasta)
        {
            string configPastas = Path.Combine(Application.StartupPath, "configPastas.txt");
            string pasta = "";

            if (File.Exists(configPastas))
            {
                try
                {
                    string[] linhasArq = File.ReadAllLines(configPastas);

                    foreach (string linha in linhasArq)
                    {
                        string[] partes = linha.Split(',');

                        if (partes.Length >= 2) // Verifica se há pelo menos Nome e Caminho
                        {
                            string nomeBotao = partes[0].Trim();  // Nome do botão
                            string caminhoPasta = partes[1].Trim(); // Caminho da pasta

                            // Preenchendo os TextBoxes conforme o nome do botão
                            if (nomeBotao == nomePasta)
                            {
                                pasta = caminhoPasta;
                            }
                        }
                    }

                    try
                    {
                        Process.Start("explorer.exe", pasta);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao abrir a pasta: " + ex.Message);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao ler o arquivo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void ecsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirPasta("ECs");
        }

        private void qualidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirPasta("Qualidade");
        }

        private void projetosMecanicosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirPasta("ProjetosM");
        }

        private void projetosEletrônicosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirPasta("ProjetosE");
        }

        private void relatóriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirPasta("Relatorios");
        }
    }
    // Classe para armazenar os dados das ECs e exibir no DataGridView
    public class ECInfo
    {
        public string DataReuniao { get; set; }
        public string CodigoEC { get; set; }
        public string Assunto { get; set; }
        public string Comentarios { get; set; }
        public string Arquivo { get; set; } // Nome do arquivo onde a EC foi encontrada
    }
}

