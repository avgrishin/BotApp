using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BotApp
{
  public class BotWorker : BackgroundService
  {
    private readonly ILogger<BotWorker> _logger;
    private readonly BotService _botservice;

    public BotWorker(ILogger<BotWorker> logger, BotService botService)
    {
      _logger = logger;
      _botservice = botService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        await _botservice.Execute(stoppingToken);
      }
    }

  }
}
