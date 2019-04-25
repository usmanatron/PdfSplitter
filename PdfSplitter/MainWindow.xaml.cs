using System.Windows;
using Microsoft.Win32;
using Ninject;
using Ninject.Extensions.Factory;
using PdfSplitter.Core;
using PdfSplitter.Core.Config;

namespace PdfSplitter
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      PageBlock.Value = 1;
    }

    private void BrowseButton_Click(object sender, RoutedEventArgs e)
    {

      var openFileDlg = new OpenFileDialog
      {
        DefaultExt = ".pdf",
        Filter = "Pdf Documents|*.pdf",
        Multiselect = false
      };

      var result = openFileDlg.ShowDialog();
      if (result == true)
      {
        FileNameTextBox.Text = openFileDlg.FileName;
      }
    }

    private async void SplitButton_Click(object sender, RoutedEventArgs e)
    {
      var kernel = BuildKernel();
      var configReader = kernel.Get<IConfigReader>();
      configReader.ReadConfig(FileNameTextBox.Text, PageBlock.Value.Value);

      var pdfSplitter = kernel.Get<ISplitter>();
      var message = await pdfSplitter.Split();
      Label_Message.Content = message;
    }

    private static StandardKernel BuildKernel()
    {
      var kernel = new StandardKernel();
      kernel.Bind<IInputFileWrapper>().To<InputFileWrapper>();
      kernel.Bind<ISplitter>().To<Splitter>();
      kernel.Bind<AppConfig>().ToSelf().InSingletonScope();
      kernel.Bind<IConfigReader>().To<ConfigReader>();
      kernel.Bind<IOutputFileWrapper>().To<OutputFileWrapper>();
      kernel.Bind<IOutputFileWrapperFactory>().ToFactory();
      kernel.Bind<INumberChunker>().To<NumberChunker>();

      return kernel;
    }
  }
}
