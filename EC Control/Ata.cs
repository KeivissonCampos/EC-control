using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Newtonsoft.Json;
using OfficeOpenXml;
using RtfPipe;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace EC_Control
{
    public partial class Ata : Form
    {
        string caminho = @"Z:\PROJETOS_COMPARTILHADOS\EC_CELL\Database ata EC\Redução de custos EC\R-custo-EC.xlsx";
        string para;
        string cc;
        string dataAta;
        string nameFile;


        private Dictionary<string, string> _dados;
        public Ata(Dictionary<string, string> dados, string data, string nameExcel)
        {
            InitializeComponent();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // Define a licença da EPPlus

            _dados = dados;
            dataAta = data;
            nameFile = nameExcel;

            ExibirDadosNoRichTextBox();
            PreencherComboFontes();
            PreencherComboTamanhos();
            CarregarEmailsConfig();
        }

        private void ExibirDadosNoRichTextBox()
        {
            richTextBox1.Clear();

            string caminhoExcel = caminho;
            var valoresExtras = CarregarDadosDaPlanilha(caminhoExcel);

            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.SelectionFont = new System.Drawing.Font("Calibri", 12, FontStyle.Regular);
            richTextBox1.SelectionBackColor = Color.White;
            richTextBox1.AppendText($"Prezados,\r\n\r\n            Resumo da Reunião EC do dia {dataAta}.\r\n\r\n");

            // Separar itens com e sem valor extra
            var comExtra = _dados.Where(kv => valoresExtras.ContainsKey(kv.Key)).ToList();
            var semExtra = _dados.Where(kv => !valoresExtras.ContainsKey(kv.Key)).ToList();

            // Junta os dois grupos
            var todosOrdenados = comExtra.Concat(semExtra);

            foreach (var item in todosOrdenados)
            {
                string valorExtra = valoresExtras.TryGetValue(item.Key, out var valorPlanilha)
                                    ? valorPlanilha
                                    : "";

                string key = item.Key.ToUpper();
                string value = item.Value.ToUpper();
                string extra = valorExtra.ToUpper();

                string parte1 = $"{key} - {value}";

                // Parte comum
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.SelectionFont = new System.Drawing.Font("Calibri", 12, FontStyle.Regular);
                richTextBox1.SelectionBackColor = Color.White;
                richTextBox1.AppendText(parte1);

                if (!string.IsNullOrWhiteSpace(extra))
                {
                    richTextBox1.AppendText(" - ");
                    string textoMarcado = $"({extra})";

                    // Valor extra com marcador amarelo
                    richTextBox1.SelectionStart = richTextBox1.TextLength;
                    richTextBox1.SelectionFont = new System.Drawing.Font("Calibri", 12, FontStyle.Regular);
                    richTextBox1.SelectionBackColor = Color.Yellow;
                    richTextBox1.AppendText(textoMarcado);
                }

                // Três linhas em branco
                richTextBox1.SelectionBackColor = Color.White;
                richTextBox1.AppendText(Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            }
        }


        private Dictionary<string, string> CarregarDadosDaPlanilha(string caminhoArquivo)
        {
            var dadosPlanilha = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase); // ignora maiúsculas

            try
            {
                if (!File.Exists(caminhoArquivo))
                {
                    MessageBox.Show($"Arquivo Excel não encontrado:\n{caminhoArquivo}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return dadosPlanilha;
                }

                using (var package = new ExcelPackage(new FileInfo(caminhoArquivo)))
                {
                    var worksheet = package.Workbook.Worksheets[0]; // primeira aba

                    if (worksheet.Dimension == null)
                    {
                        MessageBox.Show("A planilha está vazia.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return dadosPlanilha;
                    }

                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++) // pula o cabeçalho
                    {
                        string chave = worksheet.Cells[row, 1].Text.Trim();
                        string valor = worksheet.Cells[row, 2].Text.Trim();

                        if (!string.IsNullOrEmpty(chave) && !dadosPlanilha.ContainsKey(chave))
                        {
                            dadosPlanilha[chave] = valor;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao ler a planilha:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dadosPlanilha;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                System.Drawing.Font currentFont = richTextBox1.SelectionFont;
                FontStyle newStyle = currentFont.Style ^ FontStyle.Bold;

                try
                {
                    richTextBox1.SelectionFont = new System.Drawing.Font(currentFont.FontFamily, currentFont.Size, newStyle);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao aplicar negrito: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Selecione um texto com formatação consistente para aplicar o estilo.");
            }
        }

        private void cmbFonte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null && cmbFonte.SelectedItem != null)
            {
                try
                {
                    string fonte = cmbFonte.SelectedItem.ToString();
                    float tamanho = richTextBox1.SelectionFont.Size;
                    FontStyle estilo = richTextBox1.SelectionFont.Style;

                    richTextBox1.SelectionFont = new System.Drawing.Font(fonte, tamanho, estilo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao trocar a fonte: " + ex.Message);
                }
            }
        }
        private void cmbTamanho_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null && cmbTamanho.SelectedItem != null)
            {
                try
                {
                    string fonte = richTextBox1.SelectionFont.FontFamily.Name;
                    float tamanho = float.Parse(cmbTamanho.SelectedItem.ToString());
                    FontStyle estilo = richTextBox1.SelectionFont.Style;

                    richTextBox1.SelectionFont = new System.Drawing.Font(fonte, tamanho, estilo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao trocar o tamanho da fonte: " + ex.Message);
                }
            }
        }

        private void PreencherComboFontes()
        {
            foreach (FontFamily font in FontFamily.Families)
            {
                cmbFonte.Items.Add(font.Name);
            }

            cmbFonte.SelectedIndex = 37; // seleciona a primeira fonte por padrão
        }
        private void PreencherComboTamanhos()
        {
            int[] tamanhos = { 8, 9, 10, 11, 12, 14, 16, 18, 20, 24, 28, 32, 36, 48, 72 };

            foreach (int tamanho in tamanhos)
            {
                cmbTamanho.Items.Add(tamanho.ToString());
            }

            cmbTamanho.SelectedIndex = 4; // por exemplo, seleciona 12
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                System.Drawing.Font atual = richTextBox1.SelectionFont;
                FontStyle novoEstilo = atual.Style ^ FontStyle.Italic;

                richTextBox1.SelectionFont = new System.Drawing.Font(atual.FontFamily, atual.Size, novoEstilo);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                System.Drawing.Font atual = richTextBox1.SelectionFont;
                FontStyle novoEstilo = atual.Style ^ FontStyle.Underline;

                richTextBox1.SelectionFont = new System.Drawing.Font(atual.FontFamily, atual.Size, novoEstilo);
            }
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            try
            {
                // Cria a instância do Outlook e o e-mail
                Outlook.Application outlookApp = new Outlook.Application();
                Outlook.MailItem mailItem = outlookApp.CreateItem(Outlook.OlItemType.olMailItem);

                mailItem.Subject = $"Reunião EC dia {dataAta}";
                mailItem.To = para;
                mailItem.CC = cc;

                // Exibe o e-mail primeiro para garantir que a assinatura seja carregada
                mailItem.Display();

                // Aguarda o carregamento do editor Word (que renderiza o corpo do e-mail)
                dynamic inspector = mailItem.GetInspector;
                dynamic wordEditor = inspector.WordEditor;

                if (wordEditor != null)
                {
                    // Copia o conteúdo formatado do RichTextBox
                    RichTextBox rtb = new RichTextBox();
                    rtb.Rtf = richTextBox1.Rtf;
                    rtb.SelectAll();
                    rtb.Copy();

                    // Cola no início do corpo do e-mail sem apagar a assinatura
                    dynamic selection = wordEditor.Application.Selection;
                    selection.HomeKey(6); // Move cursor para o início do documento
                    selection.Paste();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Arquivos Excel (*.xlsx)|*.xlsx";
                saveFileDialog.Title = "Salvar Ata como Excel";

                string nomeArquivo = DateTime.Parse(dataAta).ToString("yy-MM-dd");
                saveFileDialog.FileName = $"{nomeArquivo}.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (ExcelPackage package = new ExcelPackage())
                        {
                            var worksheet = package.Workbook.Worksheets.Add("Ata");
                            worksheet.Cells["C1"].Value = dataAta;

                            string[] linhas = richTextBox1.Text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                            int linhaExcel = 2;

                            for (int i = 0; i < linhas.Length; i++)
                            {
                                string linhaAtual = linhas[i].Trim();

                                // Detecta linha de item (código + assunto)
                                if (Regex.IsMatch(linhaAtual, @"^EC-\d{2}_\d{3}\s-\s"))
                                {
                                    string descricaoCompleta = "";
                                    i++; // Avança para a próxima linha após o item

                                    // Coleta todas as linhas da descrição até outro EC-XX_XXX ou fim
                                    while (i < linhas.Length && !Regex.IsMatch(linhas[i].Trim(), @"^EC-\d{2}_\d{3}\s-\s"))
                                    {
                                        if (!string.IsNullOrWhiteSpace(linhas[i]))
                                        {
                                            descricaoCompleta += linhas[i].Trim() + Environment.NewLine;
                                        }
                                        i++;
                                    }

                                    // Remove espaços extras e retorna índice para processar o próximo item corretamente
                                    descricaoCompleta = descricaoCompleta.Trim();
                                    i--; // Volta uma posição pois o `for` também incrementa

                                    if (string.IsNullOrEmpty(descricaoCompleta))
                                    {
                                        descricaoCompleta = "DESCRIÇÃO NÃO INFORMADA";
                                    }

                                    worksheet.Cells[linhaExcel, 1].Value = linhaAtual;
                                    worksheet.Cells[linhaExcel, 2].Value = descricaoCompleta;
                                    worksheet.Cells[linhaExcel, 2].Style.WrapText = true; // <- Quebra de linha ativada
                                    linhaExcel++;
                                }
                            }

                            package.SaveAs(new FileInfo(saveFileDialog.FileName));
                            MessageBox.Show("Arquivo salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao salvar o arquivo: " + ex.Message);
                    }
                }
            }
        }
        private void CarregarEmailsConfig()
        {
            string caminho = Path.Combine(Application.StartupPath, "emails_config.json");

            if (!File.Exists(caminho))
                return;

            string json = File.ReadAllText(caminho);
            EmailConfig config = JsonConvert.DeserializeObject<EmailConfig>(json);

            para = string.Join(";", config.Para);
            cc = string.Join(";", config.Cc);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Rich Text Format (*.rtf)|*.rtf";
                saveFileDialog.Title = "Salvar rascunho";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
                    MessageBox.Show("Texto salvo com sucesso!");
                }
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Rich Text Format (*.rtf)|*.rtf";
                openFileDialog.Title = "Carregar rascunho";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.RichText);
                    MessageBox.Show("Texto carregado com sucesso!");
                }
            }
        }
    }
}
