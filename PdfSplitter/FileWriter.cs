using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace PdfSplitter
{
  interface IFileWriter
  {
    void SetupOutputFolder();
    void WriteFile(string fileName, Stream content);
  }
  class FileWriter : IFileWriter
  {
    private readonly AppConfig appConfig;

    private string outputFolder => Path.Combine(appConfig.WorkingDirectory, "output");
    public FileWriter(AppConfig appConfig)
    {
      this.appConfig = appConfig;
      
    }

    public void SetupOutputFolder()
    {
      if (Directory.Exists(outputFolder))
      {
        Directory.Delete(outputFolder, true);
      }

      Directory.CreateDirectory(outputFolder);
    }

    public void WriteFile(string fileName, Stream content)
    {
      var newFile = File.Create(Path.Combine(outputFolder, fileName));
      content.Seek(0, SeekOrigin.Begin);
      content.CopyTo(newFile);
      newFile.Close();
    }
  }
}
