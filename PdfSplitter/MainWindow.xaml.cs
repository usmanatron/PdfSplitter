using System.Windows;
using Microsoft.Win32;

namespace PdfSplitter
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private AppConfig appConfig;
    private ISplitter splitter;

    public MainWindow(AppConfig appConfig, ISplitter splitter)
    {
      this.appConfig = appConfig;
      this.splitter = splitter;
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
      appConfig.Update(FileNameTextBox.Text, PageBlock.Value.Value);
      var message = await splitter.Split();
      Label_Message.Content = message;
    }
  }
}
