using System.Collections.Generic;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSplitter.Core.Config;

namespace PdfSplitter.Core
{
  interface IInputFileWrapper
  {
    IEnumerable<List<PdfPage>> GetPagesByBlock();
  }

  class InputFileWrapper : IInputFileWrapper
  {
    private readonly AppConfig appConfig;
    private readonly INumberChunker numberChunker;
    private readonly PdfDocument document;

    public int NumberOfPagesInDocument => document.PageCount;

    public InputFileWrapper(AppConfig appConfig, INumberChunker numberChunker)
    {
      this.appConfig = appConfig;
      this.numberChunker = numberChunker;
      document = PdfReader.Open(appConfig.InputFilePath, PdfDocumentOpenMode.Import);
    }

    public IEnumerable<List<PdfPage>> GetPagesByBlock()
    {
      var cumulativePageStart = 0;

      foreach (var numberOfPages in numberChunker.ChunkNumber(NumberOfPagesInDocument))
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
