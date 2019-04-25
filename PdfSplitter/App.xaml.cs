using System.Windows;
using Ninject;
using Ninject.Extensions.Factory;
using PdfSplitter.File;

namespace PdfSplitter
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    private IKernel kernel;

    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);
      ConfigureKernel();
      ComposeObjects();
      Current.MainWindow.Show();
    }

    private void ConfigureKernel()
    {
      this.kernel = new StandardKernel();
      kernel.Bind<IInputFileWrapper>().To<InputFileWrapper>();
      kernel.Bind<ISplitter>().To<Splitter>();
      kernel.Bind<AppConfig>().ToSelf().InSingletonScope();
      kernel.Bind<IOutputFileWrapper>().To<OutputFileWrapper>();
      kernel.Bind<IOutputFileWrapperFactory>().ToFactory();
      kernel.Bind<INumberChunker>().To<NumberChunker>();
    }

    private void ComposeObjects()
    {
      Current.MainWindow = kernel.Get<MainWindow>();
    }
  }
}
