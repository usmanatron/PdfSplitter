using System.IO;

namespace PdfSplitter
{
  class AppConfig
  {
    public string InputFilePath;
    public int PageBlockSize;

    public string WorkingDirectory => Path.GetDirectoryName(InputFilePath);
  }
}
