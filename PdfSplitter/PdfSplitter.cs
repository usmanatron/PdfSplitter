using System.IO;
using PdfSharp.Pdf;

namespace PdfSplitter
{
  interface IPdfSplitter
  {
    void Split();
  }

  class PdfSplitter : IPdfSplitter
  {
    private readonly IInputFileWrapper inputFileWrapper;
    private readonly IFileWriter fileWriter;

    public PdfSplitter(IInputFileWrapper inputFileWrapper, IFileWriter fileWriter)
    {
      this.inputFileWrapper = inputFileWrapper;
      this.fileWriter = fileWriter;
    }

    public void Split()
    {
      var suffix = 1;
      fileWriter.SetupOutputFolder();

      foreach (var pageSet in inputFileWrapper.GetPagesByBlock())
      {
        //var newDocument = inputFileWrapper.GetPagelessClone();
        var newDocument = new PdfDocument();

        foreach (var page in pageSet)
        {
          newDocument.AddPage(page);
        }

        using (var stream = new MemoryStream())
        {
          newDocument.Save(stream, false);
          fileWriter.WriteFile($"output{suffix}.pdf", stream);
        }

        suffix++;
      }
    }
  }
}
