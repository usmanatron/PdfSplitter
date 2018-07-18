using System;
using System.IO;
using System.Linq;

namespace PdfSplitter.Config
{
  interface IConfigReader
  {
    void ReadConfig(string[] inputArguments);
  }

  class ConfigReader : IConfigReader
  {
    private readonly AppConfig appConfig;
    public ConfigReader(AppConfig appConfig)
    {
      this.appConfig = appConfig;
    }

    public void ReadConfig(string[] inputArguments)
    {
      if (inputArguments.Length > 2)
      {
        throw new ArgumentException($"Unexpected number of arguments (expected 2, got {inputArguments.Count()}");
      }

      appConfig.InputFilePath = GetFullPath(inputArguments[0]);

      if (inputArguments.Length == 2)
      {
        appConfig.PageBlockSpecified = true;
        appConfig.PageBlockSize = int.Parse(inputArguments[1]);
      }
      else
      {
        appConfig.PageBlockSpecified = false;
      }
    }

    private string GetFullPath(string filePath)
    {
      return Path.IsPathRooted(filePath)
        ? filePath
        : Path.Combine(Environment.CurrentDirectory, filePath);
    }
  }
}
