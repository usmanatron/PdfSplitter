using System.Reflection;
using System.Threading.Tasks;
using Ninject;
using Ninject.Extensions.Factory;
using PdfSplitter.Config;

[assembly: AssemblyVersion("0.1.0.0")]
namespace PdfSplitter
{
  internal class Program
  {
    private static void Main(string[] inputArguments)
    {
      var kernel = BuildKernel();

      var configReader = kernel.Get<IConfigReader>();
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
      kernel.Bind<IConfigReader>().To<ConfigReader>();
      kernel.Bind<IOutputFileWrapper>().To<OutputFileWrapper>();
      kernel.Bind<IOutputFileWrapperFactory>().ToFactory();
      kernel.Bind<INumberChunker>().To<NumberChunker>();

      return kernel;
    }
  }
}
