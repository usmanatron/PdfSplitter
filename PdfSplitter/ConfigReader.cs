using System;
using System.IO;
using System.Linq;

namespace PdfSplitter
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
      if (inputArguments.Count() != 2)
      {
        throw new ArgumentException($"Unexpected number of arguments (expected 2, got {inputArguments.Count()}");
      }

      var inputFilePath = inputArguments[0];
      if (!Path.IsPathRooted(inputFilePath))
      {
        inputFilePath = Path.Combine(Environment.CurrentDirectory, inputFilePath);
      }

      appConfig.InputFilePath = inputFilePath;
      appConfig.PageBlockSize = int.Parse(inputArguments[1]);
    }
  }
}
