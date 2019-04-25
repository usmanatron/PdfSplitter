using System;
using System.IO;

namespace PdfSplitter
{
  public class AppConfig
  {
    public void Update(string inputFilePath, int pageBlockSize)
    {
      InputFilePath = inputFilePath;
      PageBlockSize = pageBlockSize;
    }

    public string InputFilePath { get; private set; }
    public int PageBlockSize { get; private set; }

    private string WorkingDirectory => Path.GetDirectoryName(InputFilePath);
    public string InputFileName => Path.GetFileNameWithoutExtension(InputFilePath);
    public string OutputFolder => Path.Combine(WorkingDirectory, $"{InputFileName}_Split_{DateTime.Now:yyyyMMddHHmmss}");
  }
}
