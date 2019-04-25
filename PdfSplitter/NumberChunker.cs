using System;
using System.Collections.Generic;

namespace PdfSplitter
{
  public interface INumberChunker
  {
    IEnumerable<int> ChunkNumber(int total);
  }

  public class NumberChunker : INumberChunker
  {
    private readonly AppConfig appConfig;
    public NumberChunker(AppConfig appConfig)
    {
      this.appConfig = appConfig;
    }

    public IEnumerable<int> ChunkNumber(int total)
    {
      var runningTotal = total;

      while (runningTotal > 0)
      {
        var chunk = Math.Min(runningTotal, appConfig.PageBlockSize);
        yield return chunk;
        runningTotal = runningTotal - chunk;
      }
    }
  }
}
