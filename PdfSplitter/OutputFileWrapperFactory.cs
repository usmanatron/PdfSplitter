namespace PdfSplitter
{
  public interface IOutputFileWrapperFactory
  {
    OutputFileWrapper CreateOutputFileWrapper(string filename);
  }
}
