using System;
using System.IO;

namespace PdfSplitter.Config
{
  interface IConfigReader
  {
    void ReadConfig(string fileName, int pageBlock);
  }

  class ConfigReader : IConfigReader
  {
    private readonly AppConfig appConfig;
    public ConfigReader(AppConfig appConfig)
    {
      this.appConfig = appConfig;
    }

    public void ReadConfig(string fileName, int pageBlock)
    {
      appConfig.InputFilePath = GetFullPath(fileName);

      appConfig.PageBlockSpecified = true;
      appConfig.PageBlockSize = pageBlock;
    }

    private string GetFullPath(string filePath)
    {
      return Path.IsPathRooted(filePath)
        ? filePath
        : Path.Combine(Environment.CurrentDirectory, filePath);
    }
  }
}
