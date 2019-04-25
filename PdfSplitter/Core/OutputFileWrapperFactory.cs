namespace PdfSplitter.Core
{
  public interface IOutputFileWrapperFactory
  {
    OutputFileWrapper CreateOutputFileWrapper(string filename);
  }
}
