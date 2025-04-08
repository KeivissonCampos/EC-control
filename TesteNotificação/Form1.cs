using OfficeOpenXml;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace TesteNotificação
{
    public partial class Form1 : Form
    {
        private string caminhoArquivo = @"C:\Users\keivisson21\Documents\EC\Relatorios.xlsx"; // Caminho do arquivo

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
                int totalColunas = worksheet.Dimension.Columns;

                DataTable dt = new DataTable();
                DateTime hoje = DateTime.Today;
                bool encontrouData = false;

                // Criando colunas no DataTable
                for (int col = 1; col <= totalColunas; col++)
                {
                    dt.Columns.Add(worksheet.Cells[1, col].Text.Trim()); // Usa o cabeçalho como nome da coluna
                }

                // Lendo os dados e preenchendo o DataTable
                for (int i = 2; i <= totalLinhas; i++) // Começa da linha 2 (ignorando cabeçalho)
                {
                    DataRow row = dt.NewRow();
                    string descricao = worksheet.Cells[i, 2].Text.Trim(); // Coluna 2 (Descrição)
                    string dataTexto = worksheet.Cells[i, 4].Text.Trim(); // Coluna 4 (Data)

                    // Preenchendo a linha com os dados da planilha
                    for (int col = 1; col <= totalColunas; col++)
                    {
                        row[col - 1] = worksheet.Cells[i, col].Text.Trim();
                    }

                    dt.Rows.Add(row);

                    // Verifica se a data da planilha é igual à data atual
                    if (DateTime.TryParse(dataTexto, out DateTime dataCelula) && dataCelula.Date == hoje)
                    {
                        encontrouData = true;

                        DialogResult resultado = MessageBox.Show(
                            $"Vencimento encontrado: {descricao}\nDeseja remover essa data da planilha?",
                            "Notificação de Vencimento",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information
                        );

                        if (resultado == DialogResult.Yes)
                        {
                            worksheet.Cells[i, 4].Value = ""; // Apaga a data
                            worksheet.Cells[i, 4].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.None;

                            package.Save(); // Salva as alterações no arquivo

                            // Atualiza o DataTable removendo a data
                            dt.Rows[i - 2][3] = ""; // Como DataTable começa do índice 0, usamos (i - 2)
                        }
                    }
                }

                // Exibir os dados no DataGridView
                dgvDados.DataSource = dt;

                if (!encontrouData)
                {
                    MessageBox.Show("Nenhuma data correspondente foi encontrada para hoje.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
