using System;
using System.Collections.Generic;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace PdfSplitter
{
  interface IInputFileWrapper
  {
    IEnumerable<List<PdfPage>> GetPagesByBlock();
    PdfDocument GetPagelessClone();
  }

  class InputFileWrapper : IInputFileWrapper
  {
    private readonly AppConfig appConfig;
    private readonly PdfDocument document;

    public InputFileWrapper(AppConfig appConfig)
    {
      this.appConfig = appConfig;
      document = PdfReader.Open(appConfig.InputFilePath, PdfDocumentOpenMode.Import);
    }

    public PdfDocument GetPagelessClone()
    {
      PdfDocument clone = (PdfDocument) document.Clone();
      clone.Pages.PagesArray.Elements.Clear();
      return clone;
    }

    public IEnumerable<List<PdfPage>> GetPagesByBlock()
    {
      for (int startPage = 0; startPage < document.Pages.Count; startPage = startPage + appConfig.PageBlockSize)
      {
        var pages = new List<PdfPage>();

        // TODO: Explain about Math.Min below(allows for blocks which are smaller than the blockSize)
        for (int page = startPage; page < Math.Min(document.Pages.Count, startPage + appConfig.PageBlockSize); page++)
        {
          pages.Add(document.Pages[page]);
        }

        yield return pages;
      }
    }
  }
}
