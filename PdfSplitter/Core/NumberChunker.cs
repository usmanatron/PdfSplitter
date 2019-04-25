using System;
using System.Collections.Generic;
using PdfSplitter.Core.Config;

namespace PdfSplitter.Core
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
        Console.WriteLine($"Remaining pages: {runningTotal}");
        var chunkSize = GetChunkSize();

        var chunk = Math.Min(runningTotal, chunkSize);
        yield return chunk;
        runningTotal = runningTotal - chunk;
      }
    }

    private int GetChunkSize()
    {
      if (appConfig.PageBlockSpecified)
      {
        return appConfig.PageBlockSize;
      }
      else
      {
        throw new NotImplementedException("Interactive coming soon");
      }
    }
  }
}
