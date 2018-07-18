using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PdfSharp.Pdf;

namespace PdfSplitter
{
  interface IOutputFileWrapper
  {
    Task AddPagesAndClose(IEnumerable<PdfPage> pages);
  }

  public class OutputFileWrapper : IOutputFileWrapper
  {
    private readonly PdfDocument document;
    private readonly string filename;

    public OutputFileWrapper(string filename)
    {
      this.document = new PdfDocument();
      this.filename = filename;
    }

    public async Task AddPagesAndClose(IEnumerable<PdfPage> pages)
    {
      foreach (var page in pages)
      {
        document.AddPage(page);
      }

      await CloseFile();
    }

    private async Task CloseFile()
    {
      /* TODO: Initially added as a hack; ensure this is no longer needed
      if (!Directory.Exists(appConfig.OutputFolder))
      {
        Directory.CreateDirectory(appConfig.OutputFolder);
      }
      */

      using (var stream = new MemoryStream())
      {
        document.Save(stream, false);
        using (var newFile = File.Create(filename))
        {
          stream.Seek(0, SeekOrigin.Begin);
          await stream.CopyToAsync(newFile).ConfigureAwait(false);
        }
      }
    }
  }
}
