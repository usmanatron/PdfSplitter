using System.Collections.Generic;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace PdfSplitter.File
{
  interface IInputFileWrapper
  {
    IEnumerable<List<PdfPage>> GetPagesByBlock();
  }

  class InputFileWrapper : IInputFileWrapper
  {
    private readonly AppConfig appConfig;
    private readonly INumberChunker numberChunker;

    public InputFileWrapper(AppConfig appConfig, INumberChunker numberChunker)
    {
      this.appConfig = appConfig;
      this.numberChunker = numberChunker;
    }

    public IEnumerable<List<PdfPage>> GetPagesByBlock()
    {
      var document = PdfReader.Open(appConfig.InputFilePath, PdfDocumentOpenMode.Import);
      var numberOfPagesInDocument = document.PageCount;

      var cumulativePageStart = 0;

      foreach (var numberOfPages in numberChunker.ChunkNumber(numberOfPagesInDocument))
      {
        var pages = new List<PdfPage>();

        for (var p = cumulativePageStart; p < cumulativePageStart + numberOfPages; p++)
        {
          pages.Add(document.Pages[p]);
        }

        cumulativePageStart = cumulativePageStart + numberOfPages;
        yield return pages;
      }
    }
  }
}
