using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using OfficeOpenXml; // Biblioteca EPPlus
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace EC_Control
{
    public partial class Form1 : Form
    {
        private bool ordenacaoAscendente = true;
        string pastaAtaEC;
        string pastaAllEC;
        string pastaRLDiario;
        string nomeDoArqEC;
        string nomeArqRLDiario;
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
        string pesquisa_CodigoEC = "";
        string pesquisa_AssuntoEC = "";
        string pesquisa_DataReuniaoInicio = "";
        string pesquisa_DataReuniaoFim = "";
        bool btNovaAba = false;

        List<string> pastasParaVerificar = new List<string>();
        Dictionary<string, Dictionary<string, string>> dataAtaEC = new Dictionary<string, Dictionary<string, string>>();
        Dictionary<string, string> AtaEC = new Dictionary<string, string>();

        public Form1()
        {
            InitializeComponent();
            this.AcceptButton = btnPesquisar;
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // Define a licença da EPPlus
        }

        private async void btnPesquisar_Click_1(object sender, EventArgs e)
        {
            verificaNotificação();

            progressBar1.Visible = true;
            progressBar1.Value = 0;

            pesquisa_CodigoEC = btNovaAba ? codigoEC : txtCodigoEC.Text.Trim();
            pesquisa_AssuntoEC = btNovaAba ? "" : txtAssunto.Text.Trim();
            pesquisa_DataReuniaoInicio = dtpDataReuniaoInicio.Value.ToString("dd/MM/yyyy"); // Converte a data para o formato correto
            pesquisa_DataReuniaoFim = dtpDataReuniaoFim.Value.ToString("dd/MM/yyyy"); // Converte a data para o formato correto
            string pastaPlanilhas = pastaAtaEC; // Caminho das planilhas
            bool exibirWord = btNovaAba ? true : checkBox_Word.Checked;
            bool exibirExcel = btNovaAba ? true: checkBox_excel.Checked;
            bool exibirRelatorio = btNovaAba ? true : checkBox_relatorio.Checked;

            // Verifica se pelo menos um critério de pesquisa foi informado
            if (string.IsNullOrEmpty(pesquisa_CodigoEC) && string.IsNullOrEmpty(pesquisa_AssuntoEC) && !checkBox1.Checked)
            {
                btNovaAba = false;
                progressBar1.Visible = false;
                dgvResultados.DataSource = null;  // Remove a fonte de dados
                dgvResultados.Rows.Clear();       // Remove todas as linhas
                dgvResultados.Columns.Clear();    // Remove todas as colunas (se necessário)
                MessageBox.Show("Digite um código, um assunto ou selecione uma data para pesquisar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var progress = new Progress<int>(valor => progressBar1.Value = valor);

            List<ECInfo> resultados = await Task.Run(() => PesquisarEC(pastaPlanilhas, exibirWord, exibirExcel, exibirRelatorio, progress));

            progressBar1.Visible = false;
            progressBar1.Value = 100;

            if (btNovaAba)
            {
                btNovaAba = false;
                NovaAba novaAba = new NovaAba(resultados, codigoEC);
                novaAba.Show();
            }
            else
            {
                if (resultados.Count > 0)
                {
                    dgvResultados.DataSource = resultados; // Exibe os resultados no DataGridView
                    dgvResultados.Columns[0].Width = 80;
                    dgvResultados.Columns[1].Width = 80;
                    dgvResultados.Columns[2].Width = 350;
                    dgvResultados.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgvResultados.Columns[4].Width = 100;

                    dgvResultados.CurrentCell = dgvResultados.Rows[0].Cells[0]; // Define o foco na primeira célula
                    dgvResultados.Focus(); // Garante que o DataGridView receba o foco

                    dgvResultados.DataSource = ((List<ECInfo>)dgvResultados.DataSource)
                        .OrderByDescending(ec => DateTime.TryParse(ec.Data, out DateTime data) ? data : DateTime.MaxValue)
                        .ToList();
                }
                else
                {
                    dgvResultados.DataSource = null;  // Remove a fonte de dados
                    dgvResultados.Rows.Clear();       // Remove todas as linhas
                    dgvResultados.Columns.Clear();    // Remove todas as colunas (se necessário)
                    MessageBox.Show("Nenhuma EC encontrada com os critérios informados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private List<ECInfo> PesquisarEC(string pasta, bool exibirWord, bool exibirExcel, bool exibirRelatorio, IProgress<int> progress)
        {
            List<ECInfo> resultados = new List<ECInfo>();
            List<string> codigosECPasta = new List<string>();
            DirectoryInfo dir = new DirectoryInfo(pasta);

            if (!dir.Exists)
            {
                MessageBox.Show("A pasta configurada não existe. Verifique o caminho e tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return resultados;
            }
            FileInfo[] arquivos = new FileInfo[0]; ;

            if (exibirExcel)
            {
                // Obtém a lista de arquivos .xlsx, excluindo inicialmente "Relatorios.xlsx"
                arquivos = dir.GetFiles("*.xlsx").Where(f => !f.Name.Equals(nomeArqRLDiario, StringComparison.OrdinalIgnoreCase)).ToArray();
            }

            progress?.Report(20);
            // Se checkBox2 estiver marcado, adiciona o "Relatorios.xlsx" na lista
            if (exibirRelatorio)
            {
                string caminhoRelatorio = Path.Combine(pastaRLDiario, nomeArqRLDiario);
                if (File.Exists(caminhoRelatorio))
                {
                    var listaArquivos = arquivos.ToList();
                    listaArquivos.Add(new FileInfo(caminhoRelatorio));
                    arquivos = listaArquivos.ToArray();
                }
            }
            progress?.Report(40);

            string caminhoPasta = pastaAllEC; // Defina o caminho da pasta
            List<string> arquivosEncontrados = new List<string>();


                if (Directory.Exists(caminhoPasta))
                {
                    foreach (string pastas in pastasParaVerificar)
                    {
                        if (Directory.Exists(pasta))
                        {
                            // Pega todos os arquivos da pasta e subpastas
                            string[] arquivoss = Directory.GetFiles(pastas, "*", SearchOption.AllDirectories);
                            arquivosEncontrados.AddRange(arquivoss);
                        }
                    }

                    foreach (string arqVerificados in arquivosEncontrados)
                    {
                        string nomeArquivo = Path.GetFileNameWithoutExtension(arqVerificados);

                        // Expressão regular para capturar "EC-25_001"
                        Match match = Regex.Match(nomeArquivo, @"EC-\d+_\d+");

                        if (match.Success)
                        {
                            codigosECPasta.Add(match.Value);
                        }
                    }
                }
            progress?.Report(60);

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

                            bool codigoEncontrado = !string.IsNullOrEmpty(pesquisa_CodigoEC.Trim()) &&
                                !string.IsNullOrWhiteSpace(codigooEC) &&
                                codigooEC.IndexOf(pesquisa_CodigoEC.Trim(), StringComparison.OrdinalIgnoreCase) >= 0;
                            bool assuntoEncontrado = !string.IsNullOrEmpty(pesquisa_AssuntoEC.Trim()) &&
                                !string.IsNullOrWhiteSpace(assunto) &&
                                assunto.IndexOf(pesquisa_AssuntoEC.Trim(), StringComparison.OrdinalIgnoreCase) >= 0;
                            bool comentarioEncontrado = !string.IsNullOrEmpty(pesquisa_AssuntoEC.Trim()) &&
                                !string.IsNullOrWhiteSpace(comentarios) &&
                                comentarios.IndexOf(pesquisa_AssuntoEC.Trim(), StringComparison.OrdinalIgnoreCase) >= 0;

                            bool dataEncontrada = false;

                            if (checkBox1.Checked)
                            {
                                if (DateTime.TryParse(dataReuniao, out DateTime dataReuniaoDate) &&
                                    DateTime.TryParse(pesquisa_DataReuniaoInicio, out DateTime dataInicio) &&
                                    DateTime.TryParse(pesquisa_DataReuniaoFim, out DateTime dataFim))
                                {
                                    dataEncontrada = dataReuniaoDate.Date >= dataInicio.Date && dataReuniaoDate.Date <= dataFim.Date;
                                }
                            }
                            else
                            {
                                dataEncontrada = true;
                            }

                            bool todosOsCriteriosAtendidos =
                                (!string.IsNullOrEmpty(pesquisa_CodigoEC.Trim()) ? codigoEncontrado : true) &&
                                ((!string.IsNullOrEmpty(pesquisa_AssuntoEC.Trim()) ? assuntoEncontrado : true) ||
                                (!string.IsNullOrEmpty(pesquisa_AssuntoEC.Trim()) ? comentarioEncontrado : true)) &&
                                (checkBox1.Checked && !string.IsNullOrEmpty(pesquisa_DataReuniaoInicio) &&
                                 !string.IsNullOrEmpty(pesquisa_DataReuniaoFim) ? dataEncontrada : true);

                            if ((todosOsCriteriosAtendidos) && (ecCompleto != "") )
                            {
                                if (pastasParaVerificar.Count > 0)
                                {
                                    if (codigosECPasta.Contains(codigooEC)) // <=== COMPARAÇÃO ADICIONADA
                                    {
                                        resultados.Add(new ECInfo
                                        {
                                            CodigoEC = codigooEC,
                                            Assunto = assunto,
                                            Comentarios = comentarios,
                                            Data = dataReuniao,
                                            Arquivo = arquivo.Name,
                                        });
                                    }
                                }
                                else
                                {
                                    resultados.Add(new ECInfo
                                    {
                                        CodigoEC = codigooEC,
                                        Assunto = assunto,
                                        Comentarios = comentarios,
                                        Data = dataReuniao,
                                        Arquivo = arquivo.Name,
                                    });
                                }

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
            progress?.Report(80);
            if (exibirWord)
            {
                string caminhoNovaPasta = pastaAllEC;
                string nomeArquivoNovo = nomeDoArqEC;
                string caminhoArquivoOriginal = Path.Combine(caminhoNovaPasta, nomeArquivoNovo);
                string caminhoArquivoTemporario = Path.Combine(Path.GetTempPath(), $"temp_{nomeArquivoNovo}");

                if (File.Exists(caminhoArquivoOriginal))
                {
                    try
                    {
                        File.Copy(caminhoArquivoOriginal, caminhoArquivoTemporario, true);

                        using (FileStream fs = new FileStream(caminhoArquivoTemporario, FileMode.Open, FileAccess.Read))
                        {
                            HSSFWorkbook workbook = new HSSFWorkbook(fs);
                            ISheet worksheet = workbook.GetSheetAt(0);

                            int totalLinhas = worksheet.PhysicalNumberOfRows;

                            for (int i = 0; i < totalLinhas; i++)
                            {
                                IRow row = worksheet.GetRow(i);
                                if (row == null) continue;

                                string numeroEC = row.GetCell(0) != null ? row.GetCell(0).ToString().Trim() : "";
                                string descricaoEC = row.GetCell(2) != null ? row.GetCell(2).ToString().Trim() : "";
                                string ComentariosEC = row.GetCell(3) != null ? row.GetCell(3).ToString().Trim() : "";

                                if (row.GetCell(1) != null)
                                {
                                    ICell cell = row.GetCell(1);
                                    DateTime dataAberturaDate;

                                    if (cell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(cell))
                                    {
                                        dataAberturaDate = cell.DateCellValue;
                                    }
                                    else
                                    {
                                        string dataAbertura = cell.ToString().Trim();
                                        if (!DateTime.TryParseExact(dataAbertura, new[] { "dd/MM/yyyy", "MM/dd/yyyy", "yyyy-MM-dd" },
                                                                    CultureInfo.InvariantCulture, DateTimeStyles.None, out dataAberturaDate))
                                        {
                                            continue;
                                        }
                                    }

                                    if (!string.IsNullOrEmpty(numeroEC))
                                    {
                                        string codigoECFormatado = $"EC-{dataAberturaDate:yy}_{int.Parse(numeroEC):D3}";

                                        bool codigoEncontrado = !string.IsNullOrEmpty(pesquisa_CodigoEC.Trim()) &&
                                            codigoECFormatado.IndexOf(pesquisa_CodigoEC.Trim(), StringComparison.OrdinalIgnoreCase) >= 0;

                                        bool assuntoEncontrado = !string.IsNullOrEmpty(pesquisa_AssuntoEC.Trim()) &&
                                            descricaoEC.IndexOf(pesquisa_AssuntoEC.Trim(), StringComparison.OrdinalIgnoreCase) >= 0;

                                        bool comentarioEncontrado = !string.IsNullOrEmpty(pesquisa_AssuntoEC.Trim()) &&
                                            ComentariosEC.IndexOf(pesquisa_AssuntoEC.Trim(), StringComparison.OrdinalIgnoreCase) >= 0;

                                        bool dataEncontrada = false;

                                        if (checkBox1.Checked &&
                                            DateTime.TryParse(pesquisa_DataReuniaoInicio, out DateTime dataInicio) &&
                                            DateTime.TryParse(pesquisa_DataReuniaoFim, out DateTime dataFim))
                                        {
                                            dataEncontrada = dataAberturaDate.Date >= dataInicio.Date && dataAberturaDate.Date <= dataFim.Date;
                                        }
                                        else
                                        {
                                            dataEncontrada = true;
                                        }

                                        bool todosOsCriteriosAtendidos =
                                            (!string.IsNullOrEmpty(pesquisa_CodigoEC.Trim()) ? codigoEncontrado : true) &&
                                            ((!string.IsNullOrEmpty(pesquisa_AssuntoEC.Trim()) ? assuntoEncontrado : true) ||
                                            (!string.IsNullOrEmpty(pesquisa_AssuntoEC.Trim()) ? comentarioEncontrado : true)) &&
                                            (checkBox1.Checked && !string.IsNullOrEmpty(pesquisa_DataReuniaoInicio) &&
                                             !string.IsNullOrEmpty(pesquisa_DataReuniaoFim) ? dataEncontrada : true);

                                        if (todosOsCriteriosAtendidos)
                                        {
                                            if (pastasParaVerificar.Count > 0)
                                            {
                                                if (codigosECPasta.Contains(codigoECFormatado)) // <=== COMPARAÇÃO ADICIONADA
                                                {
                                                    resultados.Add(new ECInfo
                                                    {
                                                        CodigoEC = codigoECFormatado,
                                                        Assunto = descricaoEC,
                                                        Comentarios = ComentariosEC,
                                                        Data = dataAberturaDate.ToString("dd/MM/yyyy"),
                                                        Arquivo = nomeArquivoNovo,
                                                    });
                                                }
                                            }
                                            else
                                            {
                                                resultados.Add(new ECInfo
                                                {
                                                    CodigoEC = codigoECFormatado,
                                                    Assunto = descricaoEC,
                                                    Comentarios = ComentariosEC,
                                                    Data = dataAberturaDate.ToString("dd/MM/yyyy"),
                                                    Arquivo = nomeArquivoNovo,
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (IOException)
                    {
                        MessageBox.Show($"O arquivo '{nomeArquivoNovo}' está em uso e não pôde ser acessado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        try
                        {
                            if (File.Exists(caminhoArquivoTemporario))
                            {
                                File.Delete(caminhoArquivoTemporario);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erro ao excluir o arquivo temporário: {ex.Message}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }

            return resultados;
        }

        private void txtCodigoEC_TextChanged(object sender, EventArgs e)
        {
            int cursorPosition = txtCodigoEC.SelectionStart; // Salva a posição do cursor

            txtCodigoEC.Text = txtCodigoEC.Text.ToUpper();
            txtCodigoEC.SelectionStart = txtCodigoEC.Text.Length; // Mantém o cursor no final do texto
            txtCodigoEC.SelectionStart = Math.Min(cursorPosition, txtCodigoEC.Text.Length);
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

                        pastaAtaEC = pastaAta;
                        pastaAllEC = pastaEC;
                        nomeDoArqEC = nomeArqEC;
                        pastaRLDiario = pastaRLD;
                        nomeArqRLDiario = nomeArqRLD;
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

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dtpDataReuniaoInicio.Enabled = true;
                dtpDataReuniaoFim.Enabled = true;
            }
            else
            {
                dtpDataReuniaoInicio.Enabled = false;
                dtpDataReuniaoFim.Enabled = false;
            }
        }

        private void dgvResultados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            codigoEC = dgvResultados.Rows[e.RowIndex].Cells["CodigoEC"].Value.ToString();
            assunto = dgvResultados.Rows[e.RowIndex].Cells["Assunto"].Value.ToString();
            comentario = dgvResultados.Rows[e.RowIndex].Cells["Comentarios"].Value.ToString();
            dataReuniao = dgvResultados.Rows[e.RowIndex].Cells["Data"].Value.ToString();

           AtualizarRichTextBox(codigoEC, assunto, comentario, dataReuniao);

            pastaStatus(false);

        }

        private void dgvResultados_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            codigoEC = dgvResultados.Rows[e.RowIndex].Cells["CodigoEC"].Value.ToString();
            assunto = dgvResultados.Rows[e.RowIndex].Cells["Assunto"].Value.ToString();
            comentario = dgvResultados.Rows[e.RowIndex].Cells["Comentarios"].Value.ToString();
            dataReuniao = dgvResultados.Rows[e.RowIndex].Cells["Data"].Value.ToString();

            AtualizarRichTextBox(codigoEC, assunto, comentario, dataReuniao);

            pastaStatus(false);

        }

        private void AtualizarRichTextBox(string codigoEC, string assunto, string comentario, string dataReuniao)
        {
            // Exibir comentário detalhado em MessageBox
            //FontStyle.Bold, FontStyle.Regular, FontStyle.Italic
            richTextBox1.Clear();

            // 📌 EC em negrito
            richTextBox1.SelectionFont = new Font("Arial", 10, FontStyle.Bold);
            richTextBox1.AppendText($"📌 {codigoEC}\n\n");

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
            Config();
            verificaNotificação();
            CarregarPastas();
            CentralizarProgressBar();
        }

        private void CarregarPastas()
        {
            if (!File.Exists("pastas.json"))
                return;

            string json = File.ReadAllText("pastas.json");
            var pastas = JsonConvert.DeserializeObject<List<PastaInfo>>(json);

            for (int i = 0; i < pastas.Count && i < 10; i++)
            {
                AdicionarSubItemPasta(pastas[i].Nome, pastas[i].Caminho);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

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

                if (nomeColuna == "Data")
                {
                    if (ordenacaoAscendente)
                    {
                        dgvResultados.DataSource = ((List<ECInfo>)dgvResultados.DataSource)
                            .OrderBy(ec => DateTime.TryParse(ec.Data, out DateTime data) ? data : DateTime.MaxValue)
                            .ToList();
                    }
                    else
                    {
                        dgvResultados.DataSource = ((List<ECInfo>)dgvResultados.DataSource)
                            .OrderByDescending(ec => DateTime.TryParse(ec.Data, out DateTime data) ? data : DateTime.MaxValue)
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

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void AdicionarSubItemPasta(string nomeOpcao, string caminho)
        {
            // Cria o novo subitem
            ToolStripMenuItem novoSubItem = new ToolStripMenuItem(nomeOpcao);

            // Evento de clique para abrir a pasta
            novoSubItem.Click += (sender, e) =>
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = caminho,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao abrir a pasta: " + ex.Message);
                }
            };

            // Adiciona o subitem ao item "Pastas"
            menuPastas.DropDownItems.Add(novoSubItem);
        }

        private void btn_notificacao_Click(object sender, EventArgs e)
        {
            string caminhoArquivo = pastaRLDiario + @"\" + nomeArqRLDiario; // Caminho do arquivo
            int limiteCaracteres = 100;

            if (!File.Exists(caminhoArquivo))
            {
                MessageBox.Show("O arquivo de planilha não foi encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FileInfo arquivo = new FileInfo(caminhoArquivo);
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(arquivo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int totalLinhas = worksheet.Dimension.Rows;
                DateTime hoje = DateTime.Today;
                bool encontrouData = false;

                for (int i = 2; i <= totalLinhas; i++) // Começa da linha 2 (ignorando cabeçalho)
                {
                    string N_EC = worksheet.Cells[i, 1].Text.Trim(); // Coluna 1 (EC)
                    string descricao = worksheet.Cells[i, 2].Text.Trim(); // Coluna 2 (Descrição)
                    string dataTexto = worksheet.Cells[i, 4].Text.Trim(); // Coluna 4 (Data)

                    // Verifica se a célula contém uma data válida e se ela é igual à data de hoje
                    if (DateTime.TryParse(dataTexto, out DateTime dataCelula) && dataCelula.Date <= hoje)
                    {
                        encontrouData = true;

                        string descricaoLimitada = descricao.Length > limiteCaracteres
                        ? descricao.Substring(0, limiteCaracteres) + "..."
                        : descricao;

                        DialogResult resultado = MessageBox.Show(
                            $"Notificação encontrada: \n\n{N_EC}\n\n" +
                            $"{descricaoLimitada}\n\n" +
                            "Deseja remover essa notificação da planilha?",
                            "Notificação",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information
                        );

                        if (resultado == DialogResult.Yes)
                        {
                            worksheet.Cells[i, 4].Value = ""; // Remove a data da célula
                            worksheet.Cells[i, 4].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.None;

                            package.Save(); // Salva as alterações no arquivo
                        }
                    }
                }

                if (!encontrouData)
                {
                    MessageBox.Show("Nenhuma data correspondente foi encontrada para hoje.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            verificaNotificação();
        }

        void verificaNotificação()
        {
            string caminhoArquivo = pastaRLDiario + @"\" + nomeArqRLDiario; // Caminho do arquivo

            if (!File.Exists(caminhoArquivo))
            {
                return;
            }

            FileInfo arquivo = new FileInfo(caminhoArquivo);
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(arquivo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int totalLinhas = worksheet.Dimension.Rows;
                DateTime hoje = DateTime.Today;
                bool encontrouData = false;

                for (int i = 2; i <= totalLinhas; i++) // Começa da linha 2 (ignorando cabeçalho)
                {
                    string N_EC = worksheet.Cells[i, 1].Text.Trim(); // Coluna 1 (EC)
                    string descricao = worksheet.Cells[i, 2].Text.Trim(); // Coluna 2 (Descrição)
                    string dataTexto = worksheet.Cells[i, 4].Text.Trim(); // Coluna 4 (Data)

                    // Verifica se a célula contém uma data válida e se ela é igual à data de hoje
                    if (DateTime.TryParse(dataTexto, out DateTime dataCelula) && dataCelula.Date <= hoje)
                    {
                        encontrouData = true;
                        btn_notificacao.BackColor = Color.FromArgb(255, 223, 79); // Amarelo vibrante
                        btn_notificacao.FlatAppearance.BorderColor = Color.FromArgb(204, 163, 0); // Amarelo escuro para contraste
                        btn_notificacao.FlatAppearance.BorderSize = 2;
                    }
                }

                if (!encontrouData)
                {
                    btn_notificacao.BackColor = SystemColors.Window;
                }
            }
        }

        private void dgvResultados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            txtCodigoEC.Text = dgvResultados.Rows[e.RowIndex].Cells["CodigoEC"].Value.ToString();
            txtAssunto.Text = "";
            checkBox1.Checked = false;
            btnPesquisar_Click_1(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            pastaStatus(true);
        }

        void pastaStatus(bool action)
        {
            string pastaBase = pastaAllEC; // Defina o diretório inicial
            string trechoNomeArquivo = codigoEC; // Parte do nome do arquivo que deseja encontrar
            string extensao = "*.docx"; // Tipo de arquivo do Word

            try
            {
                // Busca recursiva nos subdiretórios
                var arquivos = Directory.GetFiles(pastaBase, extensao, SearchOption.AllDirectories)
                                        .Where(f => Path.GetFileName(f).Contains(trechoNomeArquivo))
                                        .ToList();


                if (!action)
                {
                    if (arquivos.Count > 0) // Usa .Count para listas
                    {
                        string caminhoCompleto = arquivos[0]; // Primeiro arquivo encontrado
                        string caminhoPasta = Path.GetDirectoryName(caminhoCompleto); // Obtém o caminho da pasta

                        // Obtém as duas últimas pastas do caminho
                        DirectoryInfo dirInfo = new DirectoryInfo(caminhoPasta);
                        string ultimaPasta = dirInfo.Name; // Nome da última pasta
                        string penultimaPasta = dirInfo.Parent != null ? dirInfo.Parent.Name : ""; // Nome da penúltima pasta

                        if (penultimaPasta == "EC_CELL")
                        {
                            if (ultimaPasta == "0_NEW")
                            {
                                labelLocalEC.Text = ultimaPasta;
                                labeldir.Text = "Nova EC (recém criada, ainda não iniciada)";
                                labeldir.ForeColor = Color.Blue;
                            }
                            else if (ultimaPasta == "1_QUEUE") // Corrigido para verificar a última pasta
                            {
                                labelLocalEC.Text = ultimaPasta;
                                labeldir.Text = "Em espera (registrada, mas será tratada no futuro)";
                                labeldir.ForeColor = Color.Purple;
                            }
                            else if (ultimaPasta == "3- WIP - FEEDBACK") // Corrigido para verificar a última pasta
                            {
                                labelLocalEC.Text = ultimaPasta;
                                labeldir.Text = "Com pendência (finalizada, mas com pendência)";
                                labeldir.ForeColor = Color.DarkGoldenrod;
                            }
                            else if (ultimaPasta == "4_FEEDBACK") // Corrigido para verificar a última pasta
                            {
                                labelLocalEC.Text = ultimaPasta;
                                labeldir.Text = "Aguardando avaliação (finalizada, mas aguardando avaliação CAD)";
                                labeldir.ForeColor = Color.Orange;
                            }
                        }
                        else if (penultimaPasta == "2_WIP")
                        {
                            labelLocalEC.Text = penultimaPasta;
                            labeldir.Text = "Em andamento";
                            labeldir.ForeColor = Color.Green;
                        }
                        else if (penultimaPasta == "5_CLOSED")
                        {
                            labelLocalEC.Text = penultimaPasta;
                            labeldir.Text = "Finalizada";
                            labeldir.ForeColor = Color.Red;
                        }
                        else
                        {
                            labelLocalEC.Text = "Não encontrada";
                            labelEC.Text = codigoEC;
                            labeldir.Text = "Não encontrada";
                            labeldir.ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        labelLocalEC.Text = "Não encontrada";
                        labelEC.Text = codigoEC;
                        labeldir.Text = "Não encontrada";
                        labeldir.ForeColor = Color.Black;
                    }
                }
                else
                {
                    if (arquivos.Count > 0)
                    {
                        string arquivoEncontrado = arquivos.First(); // Pega o primeiro encontrado
                        Process.Start(new ProcessStartInfo(arquivoEncontrado) { UseShellExecute = true });
                    }
                    else
                    {
                        MessageBox.Show("Arquivo não encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!action)
                {
                    labeldir.Text = "Erro ao buscar o arquivo";
                }
                else
                {
                    MessageBox.Show("Erro ao buscar o arquivo: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }

        private void checkBox_Andamento_Click(object sender, EventArgs e)
        {
            if (checkBox_Andamento.Checked)
            {
                if (!pastasParaVerificar.Contains(Path.Combine(pastaAllEC, "2_WIP"), StringComparer.OrdinalIgnoreCase))
                {
                    pastasParaVerificar.Add(Path.Combine(pastaAllEC, "2_WIP"));
                }  
            }
            else
            {
                pastasParaVerificar.Remove(Path.Combine(pastaAllEC, "2_WIP"));
                //checkBox_Word.Checked = false;
            }

            
            btnPesquisar_Click_1(sender, e);
        }

        private void checkBox_NovaEC_Click(object sender, EventArgs e)
        {
            if (checkBox_NovaEC.Checked)
            {
                if (!pastasParaVerificar.Contains(Path.Combine(pastaAllEC, "0_NEW"), StringComparer.OrdinalIgnoreCase))
                {
                    pastasParaVerificar.Add(Path.Combine(pastaAllEC, "0_NEW"));
                }
            }
            else
            {
                pastasParaVerificar.Remove(Path.Combine(pastaAllEC, "0_NEW"));
            }

            btnPesquisar_Click_1(sender, e);
        }

        private void checkBox_ComPendencia_Click(object sender, EventArgs e)
        {
            if (checkBox_ComPendencia.Checked)
            {
                if (!pastasParaVerificar.Contains(Path.Combine(pastaAllEC, "3- WIP - FEEDBACK"), StringComparer.OrdinalIgnoreCase))
                {
                    pastasParaVerificar.Add(Path.Combine(pastaAllEC, "3- WIP - FEEDBACK"));
                }
            }
            else
            {
                pastasParaVerificar.Remove(Path.Combine(pastaAllEC, "3- WIP - FEEDBACK"));
            }

            btnPesquisar_Click_1(sender, e);
        }

        private void checkBox_Word_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox_Word_Click(object sender, EventArgs e)
        {
            if(checkBox_Word.Checked)
            {
                checkBox_Andamento.Enabled = true;
                checkBox_NovaEC.Enabled = true;
                checkBox_Fila.Enabled = true;
                checkBox_Finalizado.Enabled = true;
                checkBox_AguardandoAvaliacao.Enabled = true;
                checkBox_ComPendencia.Enabled = true;

            }
            else
            {
                checkBox_Andamento.Enabled = false;
                checkBox_NovaEC.Enabled = false;
                checkBox_Fila.Enabled = false;
                checkBox_Finalizado.Enabled = false;
                checkBox_AguardandoAvaliacao.Enabled = false;
                checkBox_ComPendencia.Enabled = false;
            }
            btnPesquisar_Click_1(sender, e);
        }

        private void checkBox_Fila_Click(object sender, EventArgs e)
        {
            if (checkBox_Fila.Checked)
            {
                if (!pastasParaVerificar.Contains(Path.Combine(pastaAllEC, "1_QUEUE"), StringComparer.OrdinalIgnoreCase))
                {
                    pastasParaVerificar.Add(Path.Combine(pastaAllEC, "1_QUEUE"));
                }
            }
            else
            {
                pastasParaVerificar.Remove(Path.Combine(pastaAllEC, "1_QUEUE"));
            }

            btnPesquisar_Click_1(sender, e);
        }

        private void checkBox_Finalizado_Click(object sender, EventArgs e)
        {
            if (checkBox_Finalizado.Checked)
            {
                if (!pastasParaVerificar.Contains(Path.Combine(pastaAllEC, "5_CLOSED"), StringComparer.OrdinalIgnoreCase))
                {
                    pastasParaVerificar.Add(Path.Combine(pastaAllEC, "5_CLOSED"));
                }
            }
            else
            {
                pastasParaVerificar.Remove(Path.Combine(pastaAllEC, "5_CLOSED"));
            }

            btnPesquisar_Click_1(sender, e);
        }

        private void checkBox_AguardandoAvaliacao_Click(object sender, EventArgs e)
        {
            if (checkBox_AguardandoAvaliacao.Checked)
            {
                if (!pastasParaVerificar.Contains(Path.Combine(pastaAllEC, "4_FEEDBACK"), StringComparer.OrdinalIgnoreCase))
                {
                    pastasParaVerificar.Add(Path.Combine(pastaAllEC, "4_FEEDBACK"));
                }
            }
            else
            {
                pastasParaVerificar.Remove(Path.Combine(pastaAllEC, "4_FEEDBACK"));
            }

            btnPesquisar_Click_1(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {

            
        }
        private void CentralizarProgressBar()
        {
            // Centraliza o ProgressBar dentro do DataGridView
            progressBar1.Left = dgvResultados.Left + (dgvResultados.Width - progressBar1.Width) / 2;
            progressBar1.Top = dgvResultados.Top + (dgvResultados.Height - progressBar1.Height) / 2;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            CentralizarProgressBar();
        }

        private void criarRelatórioDoDiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(codigoEC))
            {
                Form2 form2 = new Form2(codigoEC, assunto, comentario); // Passa o dado para o Form2
                form2.RelatorioSalvo += AtualizarTela;

                form2.Show(); // Abre o segundo formulário
            }
            else
            {
                MessageBox.Show("Associe o relatório do dia a uma EC", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void excluirRelatórioDoDiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string caminhoArquivo = Path.Combine(pastaRLDiario, nomeArqRLDiario); // Caminho do arquivo Excel
                FileInfo arquivoExcel = new FileInfo(caminhoArquivo);

                // Confirmação antes da exclusão
                DialogResult confirmacao = MessageBox.Show("Tem certeza que deseja excluir esta linha?", "Confirmação",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacao == DialogResult.No)
                    return; // Sai do método sem excluir

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                using (ExcelPackage package = new ExcelPackage(arquivoExcel))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Seleciona a primeira planilha

                    int totalLinhas = worksheet.Dimension.Rows;
                    bool linhaEncontrada = false;

                    for (int linha = 2; linha <= totalLinhas; linha++) // Começa na segunda linha (evita cabeçalho)
                    {
                        string col1 = worksheet.Cells[linha, 1].Text; // EC
                        string col2 = worksheet.Cells[linha, 2].Text; // Comentário
                        string col3 = worksheet.Cells[linha, 3].Text; // Dia

                        string codigoEC_RL = codigoEC + " - " + assunto;

                        if (col1 == codigoEC_RL && col2 == comentario && col3 == dataReuniao)
                        {
                            worksheet.DeleteRow(linha); // Remove a linha
                            linhaEncontrada = true;
                            break; // Para a busca após encontrar a linha
                        }
                    }

                    if (linhaEncontrada)
                    {
                        package.Save(); // Salva as alterações no arquivo
                        MessageBox.Show("Linha excluída com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("A linha especificada não foi encontrada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (IOException)
            {
                MessageBox.Show("O arquivo do Excel está aberto! Feche-o antes de tentar excluir a linha.", "Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao excluir a linha, verifique se o documento está aberto: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btnPesquisar_Click_1(sender, e); // Atualiza a interface após a exclusão
            verificaNotificação();
        }

        private void criarAtaDeReuniãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            criarAtaDeReuniãoToolStripMenuItem.Checked = !criarAtaDeReuniãoToolStripMenuItem.Checked;

            if (criarAtaDeReuniãoToolStripMenuItem.Checked)
            {
                panel11.Visible = true;
                panel10.Visible = true;
            }
            else
            {
                panel11.Visible = false;
                panel10.Visible = false;
                btnPesquisar_Click_1(sender, e);
            }
        }

        private void salvaEmXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Salvar como",
                FileName = "Relatório " + codigoEC
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Resultados");
                            int colCount = dgvResultados.ColumnCount;
                            int rowCount = dgvResultados.RowCount;

                            // Adiciona cabeçalhos formatados
                            for (int i = 0; i < colCount; i++)
                            {
                                var cell = worksheet.Cell(1, i + 1);
                                cell.Value = dgvResultados.Columns[i].HeaderText;
                                cell.Style.Font.Bold = true;
                                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                cell.Style.Fill.BackgroundColor = XLColor.LightGray; // Cor de fundo do cabeçalho
                            }

                            // Adiciona os dados
                            for (int i = 0; i < rowCount; i++)
                            {
                                for (int j = 0; j < colCount; j++)
                                {
                                    var cell = worksheet.Cell(i + 2, j + 1);
                                    cell.Value = dgvResultados.Rows[i].Cells[j].Value?.ToString();
                                    cell.Style.Alignment.WrapText = true; // Habilita quebra de linha
                                }
                            }

                            // Ajusta o tamanho das colunas para caber o conteúdo
                            worksheet.Columns().AdjustToContents();

                            // Salva o arquivo
                            workbook.SaveAs(sfd.FileName);
                            MessageBox.Show("Dados exportados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao salvar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            btNovaAba = true;
            btnPesquisar_Click_1(sender, e);
        }

        private void configuraçõesDePastaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigForm configForm = new ConfigForm();
            configForm.ShowDialog(); // Exibe a tela de configuração
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string data = dateTimePicker2.Value.ToShortDateString();

            if (dgvResultados.DataSource != null)
            {

                if (!dataAtaEC.ContainsKey(data))
                {
                    dataAtaEC[data] = new Dictionary<string, string>();
                }

                // Recupera o dicionário da data específica
                AtaEC = dataAtaEC[data];

                // Verifica se a EC (chave) já existe
                if (!AtaEC.ContainsKey(codigoEC))
                {
                    AtaEC.Add(codigoEC, assunto); // Adiciona EC e descrição
                    btnPesquisar_Click_1(sender, e); // Executa a ação que você quiser após adicionar
                }
                else
                {
                    MessageBox.Show("Essa EC já existe na Ata desse dia.");
                }
            }
        }

        private void abrirEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string data = dateTimePicker1.Value.ToShortDateString();
            string nameExcel = DateTime.Parse(data).ToString("yyyy-MM-dd");

            if (!dataAtaEC.ContainsKey(data))
            {
                dataAtaEC[data] = new Dictionary<string, string>();
            }

            AtaEC = dataAtaEC[data];

            Ata Ata = new Ata(AtaEC, data, nameExcel);
            Ata.Show();
        }

        private void dgvResultados_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (criarAtaDeReuniãoToolStripMenuItem.Checked)
            {
                foreach (DataGridViewRow row in dgvResultados.Rows)
                {
                    string chave = row.Cells["CodigoEC"].Value?.ToString();
                    if (!string.IsNullOrEmpty(chave) && AtaEC.ContainsKey(chave))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {

            string data = dateTimePicker2.Value.ToShortDateString();

            if (dgvResultados.DataSource != null)
            {
                if (!dataAtaEC.ContainsKey(data))
                {
                    dataAtaEC[data] = new Dictionary<string, string>();
                }

                // Recupera o dicionário da data específica
                AtaEC = dataAtaEC[data];

                // Verifica se a EC (chave) já existe
                if (AtaEC.ContainsKey(codigoEC))
                {
                    AtaEC.Remove(codigoEC); // Adiciona EC e descrição
                    btnPesquisar_Click_1(sender, e); // Executa a ação que você quiser após adicionar
                }
                else
                {
                    MessageBox.Show("Essa EC não existe na Ata desse dia.");
                }
            }
        }

        private void configEmail_Click(object sender, EventArgs e)
        {
            ConfigurarEmail cfgEmail = new ConfigurarEmail();
            cfgEmail.ShowDialog();
        }
    }

    // Classe para armazenar os dados das ECs e exibir no DataGridView
    public class ECInfo
    {
        public string Data { get; set; }
        public string CodigoEC { get; set; }
        public string Assunto { get; set; }
        public string Comentarios { get; set; }
        public string Arquivo { get; set; } // Nome do arquivo onde a EC foi encontrada
    }
}
