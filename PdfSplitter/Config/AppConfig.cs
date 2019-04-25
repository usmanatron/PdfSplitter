using System;
using System.IO;

namespace PdfSplitter.Config
{
  public class AppConfig
  {
    public string InputFilePath;
    public int PageBlockSize;
    public bool PageBlockSpecified;

    private string WorkingDirectory => Path.GetDirectoryName(InputFilePath);
    public string InputFileName => Path.GetFileNameWithoutExtension(InputFilePath);

    public string OutputFolder => Path.Combine(WorkingDirectory, $"output_{DateTime.Now:yyyyMMddHHmmss}");
  }
}
