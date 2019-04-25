using System.IO;
using System.Threading.Tasks;
using PdfSplitter.Core.Config;

namespace PdfSplitter.Core
{
  interface ISplitter
  {
    Task<string> Split();
  }

  class Splitter : ISplitter
  {
    private readonly AppConfig appConfig;
    private readonly IInputFileWrapper inputFileWrapper;
    private readonly IOutputFileWrapperFactory outputFileFactory;

    public Splitter(AppConfig appConfig, IInputFileWrapper inputFileWrapper, IOutputFileWrapperFactory outputFileFactory)
    {
      this.appConfig = appConfig;
      this.inputFileWrapper = inputFileWrapper;
      this.outputFileFactory = outputFileFactory;
    }

    public async Task<string> Split()
    {
      var suffix = 1;
      Directory.CreateDirectory(appConfig.OutputFolder);

      foreach (var pageSet in inputFileWrapper.GetPagesByBlock())
      {
        var outputFilename = Path.Combine(appConfig.OutputFolder, $"{appConfig.InputFileName}{suffix:0000}.pdf");
        var outputFile = outputFileFactory.CreateOutputFileWrapper(outputFilename);
        await outputFile.AddPagesAndClose(pageSet);
        suffix++;
      }

      return "Done";
    }
  }
}
