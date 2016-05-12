using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace PdfSplitter
{
  internal class Program
  {
    private static void Main(string[] inputArguments)
    {
      var kernel = BuildKernel();

      var configReader = kernel.Get<ConfigReader>();
      configReader.ReadConfig(inputArguments);

      var pdfSplitter = kernel.Get<PdfSplitter>();
      pdfSplitter.Split();
    }

    private static StandardKernel BuildKernel()
    {
      var kernel = new StandardKernel();
      kernel.Bind<IInputFileWrapper>().To<InputFileWrapper>();
      kernel.Bind<IPdfSplitter>().To<PdfSplitter>();
      kernel.Bind<AppConfig>().ToSelf().InSingletonScope();
      kernel.Bind<ConfigReader>().To<ConfigReader>();
      kernel.Bind<IFileWriter>().To<FileWriter>();

      return kernel;
    }
  }
}
