namespace PdfSplitter.File
{
  public interface IOutputFileWrapperFactory
  {
    OutputFileWrapper CreateOutputFileWrapper(string filename);
  }
}
