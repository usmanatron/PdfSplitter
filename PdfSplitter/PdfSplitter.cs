using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PdfSplitter.Config;

namespace PdfSplitter
{
  interface IPdfSplitter
  {
    void Split();
  }

  class PdfSplitter : IPdfSplitter
  {
    private readonly AppConfig appConfig;
    private readonly IInputFileWrapper inputFileWrapper;
    private readonly IOutputFileWrapperFactory outputFileFactory;

    public PdfSplitter(AppConfig appConfig, IInputFileWrapper inputFileWrapper, IOutputFileWrapperFactory outputFileFactory)
    {
      this.appConfig = appConfig;
      this.inputFileWrapper = inputFileWrapper;
      this.outputFileFactory = outputFileFactory;
    }

    public void Split()
    {
      var suffix = 1;
      CreateOutputFolder();
      var outputFileWriterTasks = new List<Task>();

      foreach (var pageSet in inputFileWrapper.GetPagesByBlock())
      {
        var outputFilename = Path.Combine(appConfig.OutputFolder, $"{appConfig.InputFileName}{suffix:0000}.pdf");
        var outputFile = outputFileFactory.CreateOutputFileWrapper(outputFilename);
        outputFileWriterTasks.Add(outputFile.AddPagesAndClose(pageSet));
        suffix++;
      }

      Task.WaitAll(outputFileWriterTasks.ToArray());
    }

    private void CreateOutputFolder()
    {
      if (Directory.Exists(appConfig.OutputFolder))
      {
        Directory.Delete(appConfig.OutputFolder, true);
      }

      Directory.CreateDirectory(appConfig.OutputFolder);
    }
  }
}
