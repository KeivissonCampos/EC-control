using EC_Control;
using OfficeOpenXml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System;
using System.Linq;
using System.Reflection.Emit;

private List<ECInfo> PesquisarEC(string codigoEC, string assuntoEC, string dataEC, string pasta, bool exibirWord, bool exibirExcel, bool exibirRelatorio)
{
    List<ECInfo> resultados = new List<ECInfo>();
    DirectoryInfo dir = new DirectoryInfo(pasta);

    if (!dir.Exists)
    {
        MessageBox.Show("A pasta configurada não existe. Verifique o caminho e tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return resultados;
    }

    // Obtém arquivos Excel
    FileInfo[] arquivos = exibirExcel ? dir.GetFiles("*.xlsx").Where(f => !f.Name.Equals("Relatorios.xlsx", StringComparison.OrdinalIgnoreCase)).ToArray() : new FileInfo[0];

    // Obtém arquivos Word das pastas NEW e WIP
    string[] pastasAdicionais = { "C:\\Users\\keivisson21\\Downloads\\EC\\NEW", "C:\\Users\\keivisson21\\Downloads\\EC\\WIP" };
    List<FileInfo> arquivosWord = new List<FileInfo>();

    label4.Text = exibirWord.ToString();
    label6.Text = exibirExcel.ToString();
    label8.Text = exibirRelatorio.ToString();

    if (exibirWord)
    {
        foreach (var pastaAdicional in pastasAdicionais)
        {
            DirectoryInfo dirWord = new DirectoryInfo(pastaAdicional);
            if (dirWord.Exists)
            {
                arquivosWord.AddRange(dirWord.GetFiles("*.docx"));
            }
        }
    }

    // Processa arquivos Excel
    if (exibirExcel)
    {
        foreach (var arquivo in arquivos)
        {
            try
            {
                using (var package = new ExcelPackage(new FileInfo(arquivo.FullName)))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    int linhas = worksheet.Dimension.Rows;

                    for (int i = InitialCell; i <= linhas; i++)
                    {
                        string ecCompleto = worksheet.Cells[i, cellEC].Text.Trim();
                        string comentarios = worksheet.Cells[i, cellDescric].Text;
                        string dataReuniao = worksheet.Cells[DataL, DataC].Text;

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

                        if (!exibirRelatorio || (exibirRelatorio && dataReuniao == DateTime.Today.ToString("dd/MM/yyyy")))
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
            catch { }
        }
    }

    // Processa arquivos Word
    if (exibirWord && arquivosWord.Count > 0)
    {
        foreach (var arquivoWord in arquivosWord)
        {
            string nomeArquivo = Path.GetFileNameWithoutExtension(arquivoWord.Name);
            string[] partes = nomeArquivo.Split(new string[] { " - " }, StringSplitOptions.None);

            string codigoECWord = partes.Length > 0 ? partes[0].Trim() : "Desconhecido";
            string assuntoWord = partes.Length > 1 ? partes[1].Trim() : "Sem assunto";

            resultados.Add(new ECInfo
            {
                CodigoEC = codigoECWord,
                Assunto = assuntoWord,
                Comentarios = "", // Sem comentários para arquivos Word
                DataReuniao = "",  // Sem data definida
                Arquivo = arquivoWord.Name,
            });
        }
    }
    else if (exibirWord)
    {
        MessageBox.Show("Nenhum arquivo Word encontrado nas pastas configuradas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    return resultados;
}