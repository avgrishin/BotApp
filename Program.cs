using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;

namespace BotApp
{
  partial class Program
  {

    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
              services.AddHostedService<BotWorker>();
              services.AddTransient<BotService>();
              services.AddHttpClient("assetsmgr", c =>
              {
                c.BaseAddress = new System.Uri("http://assetsmgr/");
              })
              .ConfigurePrimaryHttpMessageHandler(() =>
              {
                return new HttpClientHandler()
                {
                  UseDefaultCredentials = true,
                  Proxy = new WebProxy("10.177.10.48", 8080)
                  {
                    UseDefaultCredentials = true
                  }
                };
              });
            })
            .ConfigureLogging(l =>
            {
              l.ClearProviders();
              l.AddConsole();
            })
          ;
  }

  //private static TelegramBotClient Bot;

  //  private static System.Timers.Timer aTimer;
  //  private static PIFRates[] pifRates;
  //  private static PIFYield[] pifYield;
  //  private static PIFBranch[] pifBranch;
  //  private static PIFBranch[] duBranch;
  //  private static List<PIFChart> pifChart = new List<PIFChart> { };
  //  private static List<PIFChart> duChart = new List<PIFChart> { };
  //  private static List<PIFChart> pifOnepage = new List<PIFChart> { };

  //  static async Task Main(string[] args)
  //  {

  //    aTimer = new System.Timers.Timer(1000);
  //    aTimer.Elapsed += new ElapsedEventHandler(async (s1, e1) =>
  //    {
  //      aTimer.Interval = 1000 * 60 * 60;
  //      await GetData();
  //    });
  //    aTimer.Enabled = true;
  //    //1121732006:AAHO3iI4CpYzHK-UU9Z20RE2D8SGarlAIdA
  //    //"930948388:AAGwTHmhjkb2nxXrrY1_cEucDvfsrwDstLw"
  //    //var Proxy = new WebProxy("176.222.58.82", 57463) { Credentials = new NetworkCredential("lyXUIeL0dJ", "iHiuLF4kRU") };
  //    var Proxy = new WebProxy() { Credentials = CredentialCache.DefaultCredentials };
  //    //var Proxy = new WebProxy("10.177.10.48", 8080) { Credentials = new NetworkCredential("techadmin", "Bpgjldsgjldthnf77!") };
  //    Bot = new TelegramBotClient(ConfigurationManager.AppSettings["token"], webProxy: Proxy);
  //    var me = await Bot.GetMeAsync();
  //    Console.Title = me.Username;
  //    //await Bot.SetChatTitleAsync(930948388, "УК Уралсиб бот");
  //    BotService.OnMessage += BotOnMessageReceived;
  //    BotService.OnMessageEdited += BotOnMessageReceived;
  //    BotService.OnCallbackQuery += BotOnCallbackQueryReceived;
  //    BotService.OnReceiveError += BotOnReceiveError;
  //    BotService.StartReceiving();
  //    Console.WriteLine($"Start listening for @{me.Username}");
  //    Thread.Sleep(int.MaxValue);
  //  }

  //  private static IEnumerable<IEnumerable<InlineKeyboardButton>> getInlineKbdPifParam(Param[] param, string pif, string back)
  //  {
  //    for (int i = 1; i <= (param.Count() + 1) / 2; i++)
  //      yield return param.Select(p => InlineKeyboardButton.WithCallbackData(p.Title, $"{back}_{pif}_{p.Cmd}")).Skip((i - 1) * 2).Take(2);
  //    yield return new[] { InlineKeyboardButton.WithCallbackData("Назад", back) };
  //  }
  //  private static IEnumerable<IEnumerable<InlineKeyboardButton>> getInlineKbdDuParam(string pif)
  //  {
  //    for (int i = 1; i <= (duParam.Count() + 1) / 2; i++)
  //      yield return duParam.Select(p => InlineKeyboardButton.WithCallbackData(p.Title, $"ДУ_{pif}_{p.Cmd}")).Skip((i - 1) * 2).Take(2);
  //    yield return new[] { InlineKeyboardButton.WithCallbackData("Назад", "ДУ") };
  //  }
  //  private static IEnumerable<IEnumerable<InlineKeyboardButton>> getInlineKbdDu(DUBase[] du, string back)
  //  {
  //    foreach (var q in du.Select(p => InlineKeyboardButton.WithCallbackData(p.Name, $"{back}_{back}_{p.Code}")))
  //      yield return new[] { q };
  //    yield return new[] { InlineKeyboardButton.WithCallbackData("Назад", back) };
  //  }

  //  private static IEnumerable<IEnumerable<InlineKeyboardButton>> getInlineKbdPif()
  //  {
  //    for (int i = 1; i <= (pif.Count() + 1) / 2; i++)
  //      yield return pif.Select(p => InlineKeyboardButton.WithCallbackData(p.Name, $"ПИФ_ПИФ_{p.Brief}")).Skip((i - 1) * 2).Take(2);
  //    yield return new[] { InlineKeyboardButton.WithCallbackData("Назад", "ПИФ") };
  //  }
  //  private static IEnumerable<IEnumerable<InlineKeyboardButton>> getInlineKbdUpr()
  //  {
  //    foreach (var q in upr.Select(p => InlineKeyboardButton.WithCallbackData(p.FIO, $"{p.Cmd}")))
  //      yield return new[] { q };
  //  }
  //  private static IEnumerable<IEnumerable<InlineKeyboardButton>> getInlineKbdPifCard()
  //  {
  //    yield return new[] { InlineKeyboardButton.WithCallbackData("Базовые Принципы", "ПИФ_КАР_1") };
  //    yield return new[] { InlineKeyboardButton.WithCallbackData("Создайте свой портфель", "ПИФ_КАР_2") };
  //    yield return new[] { InlineKeyboardButton.WithCallbackData("Назад", "ПИФ") };
  //  }

  //  private static IEnumerable<IEnumerable<InlineKeyboardButton>> getInlineKbdIisCard()
  //  {
  //    yield return new[] { InlineKeyboardButton.WithCallbackData("Готовые решения", "ИИС_КАР_1") };
  //    yield return new[] { InlineKeyboardButton.WithCallbackData("Базовые условия", "ИИС_КАР_2") };
  //    yield return new[] { InlineKeyboardButton.WithCallbackData("Назад", "ИИС") };
  //  }
  //  private static IEnumerable<IEnumerable<InlineKeyboardButton>> getInlineKbdIisPam()
  //  {
  //    yield return new[] { InlineKeyboardButton.WithCallbackData("Заключение Договора ИИС", "ИИС_ПАМ_ЗАК") };
  //    yield return new[] { InlineKeyboardButton.WithCallbackData("Расторжение Договора ИИС", "ИИС_ПАМ_РАС") };
  //    yield return new[] { InlineKeyboardButton.WithCallbackData("Назад", "ИИС") };
  //  }
  //  private static IEnumerable<IEnumerable<InlineKeyboardButton>> getInlineKbdIisNal()
  //  {
  //    yield return new[] { InlineKeyboardButton.WithCallbackData("Налоговый вычет", "ИИС_НАЛ_ПАМ") };
  //    yield return new[] { InlineKeyboardButton.WithCallbackData("Запрос документов", "ИИС_НАЛ_ДОК") };
  //    yield return new[] { InlineKeyboardButton.WithCallbackData("Назад", "ИИС") };
  //  }

  //  static IEnumerable<IEnumerable<InlineKeyboardButton>> InlineKbdDuDoc
  //  {
  //    get
  //    {
  //      yield return new[] { InlineKeyboardButton.WithCallbackData("Договор ДУ", "ДУ_ДОК_ФОР") };
  //      yield return new[] { InlineKeyboardButton.WithCallbackData("Динамика среднемесячной доходности", "ДУ_ДОК_ДИН") };
  //      yield return new[] { InlineKeyboardButton.WithCallbackData("Методика оценки стоимости активов", "ДУ_ДОК_МЕТ") };
  //      yield return new[] { InlineKeyboardButton.WithCallbackData("Перечень мер по конфликту интересов", "ДУ_ДОК_ПЕР") };
  //      yield return new[] { InlineKeyboardButton.WithCallbackData("Порядок определения инвестпрофиля", "ДУ_ДОК_ПОР") };
  //      yield return new[] { InlineKeyboardButton.WithCallbackData("Регламент ДУ", "ДУ_ДОК_РЕГ") };
  //      yield return new[] { InlineKeyboardButton.WithCallbackData("Реквизиты для перечисления средств", "ДУ_ДОК_РЕК") };
  //      yield return new[] { InlineKeyboardButton.WithCallbackData("Стандартные инвестпрофили", "ДУ_ДОК_СТА") };
  //      yield return new[] { InlineKeyboardButton.WithCallbackData("Назад", "ДУ") };
  //    }
  //  }
  //  static IEnumerable<IEnumerable<InlineKeyboardButton>> InlineKbdIisDoc
  //  {
  //    get
  //    {
  //      yield return new[] { InlineKeyboardButton.WithCallbackData("Договор ДУ ИИС", "ИИС_ДОК_ФОР") };
  //      yield return new[] { InlineKeyboardButton.WithCallbackData("Динамика среднемесячной доходности", "ИИС_ДОК_ДИН") };
  //      yield return new[] { InlineKeyboardButton.WithCallbackData("Методика оценки стоимости активов", "ИИС_ДОК_МЕТ") };
  //      yield return new[] { InlineKeyboardButton.WithCallbackData("Перечень мер по конфликту интересов", "ИИС_ДОК_ПЕР") };
  //      yield return new[] { InlineKeyboardButton.WithCallbackData("Порядок определения инвестпрофиля", "ИИС_ДОК_ПОР") };
  //      yield return new[] { InlineKeyboardButton.WithCallbackData("Реквизиты для перечисления средств", "ИИС_ДОК_РЕК") };
  //      yield return new[] { InlineKeyboardButton.WithCallbackData("Стандартные инвестпрофили", "ИИС_ДОК_СТА") };
  //      yield return new[] { InlineKeyboardButton.WithCallbackData("Назад", "ИИС") };
  //    }
  //  }

  //  private static InlineKeyboardButton[][] getInlineKbdProd()
  //  {
  //    return new[]
  //    {
  //      new[]
  //      {
  //        InlineKeyboardButton.WithCallbackData("ПИФ"),
  //        InlineKeyboardButton.WithCallbackData("ИИС"),
  //        InlineKeyboardButton.WithCallbackData("ДУ")
  //      },
  //      new[]
  //      {
  //        InlineKeyboardButton.WithCallbackData("Управляющие", "УПР")
  //      }
  //    };
  //  }
  //  private static async void BotOnMessageReceived(object sender, MessageEventArgs e)
  //  {
  //    var message = e.Message;

  //    if (message == null || message.Type != MessageType.Text) return;
  //    var c = await BotService.GetChatMemberAsync(chatId: e.Message.Chat.Id, userId: e.Message.From.Id);
  //    Console.WriteLine($"{DateTime.Now} {c.User.FirstName} {c.User.LastName} {c.User.Id} {e.Message.Text}");

  //    var msg = message.Text.Split(' ');
  //    if (msg.Count() > 0)
  //    {
  //      if (msg.Count() > 1)
  //      {
  //        switch (msg[0].ToLower())
  //        {
  //          case "/repmarket":
  //            {
  //              var d = msg[1];
  //              if (Regex.Match(d, "^\\d{2}\\.\\d{2}\\.\\d{4}$").Success)
  //              {
  //                var fn = @"C:\user\vs\ssis\RepMarketBot.cmd";
  //                if (File.Exists(fn))
  //                {
  //                  System.Diagnostics.Process proc = new System.Diagnostics.Process();
  //                  proc.StartInfo.FileName = fn;
  //                  proc.StartInfo.Arguments = d;
  //                  proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(fn);
  //                  proc.Start();
  //                  await BotService.SendTextMessageAsync(chatId: message.Chat.Id, text: "Отчет запущен на выполнение", replyMarkup: new InlineKeyboardMarkup(getInlineKbdProd()));
  //                }
  //                else
  //                {
  //                  await BotService.SendTextMessageAsync(chatId: message.Chat.Id, text: "Файл для выполнения не найден", replyMarkup: new InlineKeyboardMarkup(getInlineKbdProd()));
  //                }
  //              }
  //              else
  //              {
  //                await BotService.SendTextMessageAsync(chatId: message.Chat.Id, text: $"Неверный параметер {d}", replyMarkup: new InlineKeyboardMarkup(getInlineKbdProd()));
  //              }
  //            }
  //            return;

  //          case "/kkpif":
  //            {
  //              var d = msg[1];
  //              if (Regex.Match(d, "^\\d{11}$").Success)
  //              {
  //                var fn = @"C:\user\vs\ssis\KKPIF_bot.cmd";
  //                if (File.Exists(fn))
  //                {
  //                  System.Diagnostics.Process proc = new System.Diagnostics.Process();
  //                  proc.StartInfo.FileName = fn;
  //                  proc.StartInfo.Arguments = d;
  //                  proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(fn);
  //                  proc.Start();
  //                  await BotService.SendTextMessageAsync(chatId: message.Chat.Id, text: "Задание запущен на выполнение", replyMarkup: new InlineKeyboardMarkup(getInlineKbdProd()));
  //                }
  //                else
  //                {
  //                  await BotService.SendTextMessageAsync(chatId: message.Chat.Id, text: "Файл для выполнения не найден", replyMarkup: new InlineKeyboardMarkup(getInlineKbdProd()));
  //                }
  //              }
  //              else
  //              {
  //                await BotService.SendTextMessageAsync(chatId: message.Chat.Id, text: $"Неверный параметер {d}", replyMarkup: new InlineKeyboardMarkup(getInlineKbdProd()));
  //              }
  //            }
  //            return;

  //        }
  //      }
  //      else
  //      {
  //        switch (msg[0].ToLower())
  //        {
  //          case "/voznagr":
  //            {
  //              var fn = @"C:\user\vs\ssis\RepVvodDS.cmd";
  //              if (File.Exists(fn))
  //              {
  //                System.Diagnostics.Process proc = new System.Diagnostics.Process();
  //                proc.StartInfo.FileName = fn;
  //                proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(fn);
  //                proc.Start();
  //                await BotService.SendTextMessageAsync(chatId: message.Chat.Id, text: "Отчет запущен на выполнение", replyMarkup: new InlineKeyboardMarkup(getInlineKbdProd()));
  //              }
  //              else
  //              {
  //                await BotService.SendTextMessageAsync(chatId: message.Chat.Id, text: "Файл для выполнения не найден", replyMarkup: new InlineKeyboardMarkup(getInlineKbdProd()));
  //              }
  //            }
  //            return;

  //        }
  //      }
  //    }
  //    await BotService.SendTextMessageAsync(
  //      chatId: message.Chat.Id,
  //      text: "Выберите продукт",
  //      replyMarkup: new InlineKeyboardMarkup(getInlineKbdProd())
  //    );
  //  }
  //  private static async Task SendBotMsgAsync(Telegram.Bot.Types.CallbackQuery callbackQuery, string text, InlineKeyboardMarkup replyMarkup = null, ParseMode parseMode = ParseMode.Default)
  //  {
  //    if (callbackQuery.Message.Text == null)
  //    {
  //      await BotService.SendTextMessageAsync(
  //        chatId: callbackQuery.Message.Chat.Id,
  //        parseMode: parseMode,
  //        text: text,
  //        replyMarkup: replyMarkup
  //      );
  //      await BotService.DeleteMessageAsync(chatId: callbackQuery.Message.Chat.Id, messageId: callbackQuery.Message.MessageId);
  //    }
  //    else if (callbackQuery.Message.Text != text)
  //    {
  //      await BotService.EditMessageTextAsync(
  //        chatId: callbackQuery.Message.Chat.Id,
  //        messageId: callbackQuery.Message.MessageId,
  //        parseMode: parseMode,
  //        text: text,
  //        replyMarkup: replyMarkup
  //      );
  //    }
  //  }
  //  private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
  //  {
  //    var callbackQuery = callbackQueryEventArgs.CallbackQuery;
  //    try
  //    {
  //      var c = await BotService.GetChatMemberAsync(chatId: callbackQuery.Message.Chat.Id, userId: callbackQuery.From.Id);
  //      Console.WriteLine($"{DateTime.Now} {c.User.FirstName} {c.User.LastName} {c.User.Id} {callbackQuery.Data}");
  //      //await Bot.AnswerCallbackQueryAsync(callbackQueryId: callbackQuery.Id, text: $"Получено {callbackQuery.Data}");
  //      if (callbackQuery.Data == "Продукт")
  //      {
  //        await SendBotMsgAsync(
  //          callbackQuery: callbackQuery,
  //          text: "Выберите продукт",
  //          replyMarkup: new InlineKeyboardMarkup(getInlineKbdProd())
  //        );
  //      }
  //      else if (callbackQuery.Data == "ПИФ")
  //      {
  //        await SendBotMsgAsync(
  //          callbackQuery: callbackQuery,
  //          text: "Выберите параметр ПИФ",
  //          replyMarkup: new InlineKeyboardMarkup(getInlineKbdPif1())
  //        );
  //      }
  //      else if (callbackQuery.Data == "ДУ")
  //      {
  //        await SendBotMsgAsync(
  //          callbackQuery: callbackQuery,
  //          text: "Выберите параметр ДУ",
  //          replyMarkup: new InlineKeyboardMarkup(getInlineKbdDu1())
  //        );
  //      }
  //      else if (callbackQuery.Data == "ИИС")
  //      {
  //        await SendBotMsgAsync(
  //          callbackQuery: callbackQuery,
  //          text: "Выберите параметр ИИС",
  //          replyMarkup: new InlineKeyboardMarkup(getInlineKbdIis1())
  //        );
  //      }
  //      else if (callbackQuery.Data == "УПР")
  //      {
  //        await SendBotMsgAsync(
  //          callbackQuery: callbackQuery,
  //          text: "Выберите управляющего",
  //          replyMarkup: new InlineKeyboardMarkup(getInlineKbdUpr())
  //        );
  //      }
  //      else
  //      {
  //        var cd = callbackQuery.Data.Split("_");
  //        if (cd.Count() == 2)
  //        {
  //          if (cd[0] == "ПИФ")
  //          {
  //            switch (cd[1])
  //            {
  //              case "ПИФ":
  //                await SendBotMsgAsync(
  //                  callbackQuery: callbackQuery,
  //                  text: "Выберите фонд",
  //                  replyMarkup: new InlineKeyboardMarkup(getInlineKbdPif())
  //                );
  //                break;

  //              case "КАР":
  //                await SendBotMsgAsync(
  //                  callbackQuery: callbackQuery,
  //                  text: "Выберите карточку клиента",
  //                  replyMarkup: new InlineKeyboardMarkup(getInlineKbdPifCard())
  //                );
  //                break;

  //              case "ОБУ":
  //                {
  //                  var files = Directory.GetFiles(@"V:\VOL1\ASSETS\4All\uralsib_am_bot\ПИФ\ОБУ\", "*.pdf");
  //                  if (files.Length > 0)
  //                  {
  //                    var msg = await BotService.SendDocumentAsync(
  //                      chatId: callbackQuery.Message.Chat.Id,
  //                      document: new InputOnlineFile(new MemoryStream(File.ReadAllBytes(files[0])), Path.GetFileName(files[0])),
  //                      replyMarkup: new InlineKeyboardMarkup(getInlineKbdPif1())
  //                    );
  //                    await BotService.DeleteMessageAsync(chatId: callbackQuery.Message.Chat.Id, messageId: callbackQuery.Message.MessageId);
  //                  }
  //                }
  //                break;

  //              case "FAQ":
  //                {
  //                  var files = Directory.GetFiles(@"V:\VOL1\ASSETS\4All\uralsib_am_bot\ПИФ\FAQ\", "*.pdf");
  //                  if (files.Length > 0)
  //                  {
  //                    var msg = await BotService.SendDocumentAsync(
  //                      chatId: callbackQuery.Message.Chat.Id,
  //                      document: new InputOnlineFile(new MemoryStream(File.ReadAllBytes(files[0])), Path.GetFileName(files[0])),
  //                      replyMarkup: new InlineKeyboardMarkup(getInlineKbdPif1())
  //                    );
  //                    await BotService.DeleteMessageAsync(chatId: callbackQuery.Message.Chat.Id, messageId: callbackQuery.Message.MessageId);
  //                  }
  //                }
  //                break;

  //              case "Price":
  //                var str = String.Join("\n\n", pifRates.Select(p => $"{pif.FirstOrDefault(f => f.Brief == p.Brief)?.Name ?? ""}\n<i>{p.date}</i>  <b>`{p.pricePai}`</b>  <i>{p.pricePaiD}%</i>").ToArray());
  //                await SendBotMsgAsync(
  //                  callbackQuery: callbackQuery,
  //                  parseMode: ParseMode.Html,
  //                  text: str,
  //                  replyMarkup: new InlineKeyboardMarkup(getInlineKbdPif1())
  //                );
  //                break;
  //            }
  //          }
  //          else if (cd[0] == "ИИС")
  //          {
  //            switch (cd[1])
  //            {
  //              case "ИИС":
  //                await SendBotMsgAsync(
  //                  callbackQuery: callbackQuery,
  //                  text: "Выберите стратегию",
  //                  replyMarkup: new InlineKeyboardMarkup(getInlineKbdDu(iis, "ИИС"))
  //                );
  //                break;

  //              case "КАР":
  //                await SendBotMsgAsync(
  //                  callbackQuery: callbackQuery,
  //                  text: "Выберите карточку клиента",
  //                  replyMarkup: new InlineKeyboardMarkup(getInlineKbdIisCard())
  //                );
  //                break;

  //              case "ОБУ":
  //                {
  //                  var files = Directory.GetFiles(@"V:\VOL1\ASSETS\4All\uralsib_am_bot\ИИС\ОБУ", "*.pdf");
  //                  if (files.Length > 0)
  //                  {
  //                    var msg = await BotService.SendDocumentAsync(
  //                      chatId: callbackQuery.Message.Chat.Id,
  //                      document: new InputOnlineFile(new MemoryStream(File.ReadAllBytes(files[0])), Path.GetFileName(files[0])),
  //                      replyMarkup: new InlineKeyboardMarkup(getInlineKbdIis1())
  //                    );
  //                    await BotService.DeleteMessageAsync(chatId: callbackQuery.Message.Chat.Id, messageId: callbackQuery.Message.MessageId);
  //                  }
  //                }
  //                break;

  //              case "FAQ":
  //                {
  //                  var files = Directory.GetFiles(@"V:\VOL1\ASSETS\4All\uralsib_am_bot\ИИС\FAQ", "*.pdf");
  //                  if (files.Length > 0)
  //                  {
  //                    var msg = await BotService.SendDocumentAsync(
  //                      chatId: callbackQuery.Message.Chat.Id,
  //                      document: new InputOnlineFile(new MemoryStream(File.ReadAllBytes(files[0])), Path.GetFileName(files[0])),
  //                      replyMarkup: new InlineKeyboardMarkup(getInlineKbdIis1())
  //                    );
  //                    await BotService.DeleteMessageAsync(chatId: callbackQuery.Message.Chat.Id, messageId: callbackQuery.Message.MessageId);
  //                  }
  //                }
  //                break;

  //              case "ПАМ":
  //                await SendBotMsgAsync(
  //                  callbackQuery: callbackQuery,
  //                  text: "Выберите памятку ИИС",
  //                  replyMarkup: new InlineKeyboardMarkup(getInlineKbdIisPam())
  //                );
  //                break;

  //              case "НАЛ":
  //                await SendBotMsgAsync(
  //                  callbackQuery: callbackQuery,
  //                  text: "Выберите документ",
  //                  replyMarkup: new InlineKeyboardMarkup(getInlineKbdIisNal())
  //                );
  //                break;

  //              case "ДОК":
  //                await SendBotMsgAsync(
  //                  callbackQuery: callbackQuery,
  //                  text: "Выберите документ",
  //                  replyMarkup: new InlineKeyboardMarkup(InlineKbdIisDoc)
  //                );
  //                break;

  //            }
  //          }
  //          else if (cd[0] == "ДУ")
  //          {
  //            switch (cd[1])
  //            {
  //              case "ДУ":
  //                await SendBotMsgAsync(
  //                  callbackQuery: callbackQuery,
  //                  text: "Выберите стратегию",
  //                  replyMarkup: new InlineKeyboardMarkup(getInlineKbdDu(du, "ДУ"))
  //                );
  //                break;

  //              case "ДОК":
  //                await SendBotMsgAsync(
  //                  callbackQuery: callbackQuery,
  //                  text: "Выберите документ",
  //                  replyMarkup: new InlineKeyboardMarkup(InlineKbdDuDoc)
  //                );
  //                break;

  //              case "ОБУ":
  //                {
  //                  await SendBotMsgAsync(callbackQuery: callbackQuery, text: "Файл не найден", replyMarkup: new InlineKeyboardMarkup(getInlineKbdDu1()));
  //                  //var files = Directory.GetFiles("V:\\VOL1\\ASSETS\\4All\\Крупенина\\FAQ и Обучающая большая преза от АБ\\Обучающая большая преза ИИС\\", "*.pdf");
  //                  //if (files.Length > 0)
  //                  //{
  //                  //  var msg = await Bot.SendDocumentAsync(
  //                  //    chatId: callbackQuery.Message.Chat.Id,
  //                  //    document: new InputOnlineFile(new MemoryStream(File.ReadAllBytes(files[0])), Path.GetFileName(files[0])),
  //                  //    replyMarkup: new InlineKeyboardMarkup(getInlineKbdDu1())
  //                  //  );
  //                  //  await Bot.DeleteMessageAsync(chatId: callbackQuery.Message.Chat.Id, messageId: callbackQuery.Message.MessageId);
  //                  //}
  //                }
  //                break;

  //              case "FAQ":
  //                {
  //                  var files = Directory.GetFiles(@"V:\VOL1\ASSETS\4All\uralsib_am_bot\ДУ\FAQ\", "*.pdf");
  //                  if (files.Length > 0)
  //                  {
  //                    var msg = await BotService.SendDocumentAsync(
  //                      chatId: callbackQuery.Message.Chat.Id,
  //                      document: new InputOnlineFile(new MemoryStream(File.ReadAllBytes(files[0])), Path.GetFileName(files[0])),
  //                      replyMarkup: new InlineKeyboardMarkup(getInlineKbdDu1())
  //                    );
  //                    await BotService.DeleteMessageAsync(chatId: callbackQuery.Message.Chat.Id, messageId: callbackQuery.Message.MessageId);
  //                  }
  //                }
  //                break;

  //            }
  //          }
  //          else if (cd[0] == "УПР")
  //          {
  //            var vupr = upr.FirstOrDefault(p => p.Cmd == callbackQuery.Data);
  //            if (vupr != null)
  //            {
  //              var inlineKeyboard = new InlineKeyboardMarkup(getInlineKbdUpr());
  //              await SendBotMsgAsync(
  //                callbackQuery: callbackQuery,
  //                parseMode: ParseMode.Html,
  //                text: $"<i>{vupr.FIO}</i>\n{vupr.Text}",
  //                replyMarkup: inlineKeyboard
  //              );
  //            }
  //          }
  //        }
  //        else if (cd.Count() == 3)
  //        {
  //          if (cd[0] == "ПИФ")
  //          {
  //            switch (cd[1])
  //            {
  //              case "ПИФ":
  //                var vpif = pif.FirstOrDefault(p => p.Brief == cd[2]);
  //                await SendBotMsgAsync(
  //                  callbackQuery: callbackQuery,
  //                  text: vpif.Name,
  //                  replyMarkup: new InlineKeyboardMarkup(getInlineKbdPifParam(pifParam, vpif.Brief, "ПИФ_ПИФ"))
  //                );
  //                break;

  //              case "КАР":
  //                var card = int.Parse(cd[2]);
  //                var files = Directory.GetFiles(@$"V:\VOL1\ASSETS\4All\uralsib_am_bot\ПИФ\КАР\{card}", "*.pdf");
  //                if (files.Length > 0)
  //                {
  //                  var msg = await BotService.SendDocumentAsync(
  //                    chatId: callbackQuery.Message.Chat.Id,
  //                    document: new InputOnlineFile(new MemoryStream(File.ReadAllBytes(files[0])), Path.GetFileName(files[0])),
  //                    replyMarkup: new InlineKeyboardMarkup(getInlineKbdPifCard())
  //                  );
  //                  await BotService.DeleteMessageAsync(chatId: callbackQuery.Message.Chat.Id, messageId: callbackQuery.Message.MessageId);
  //                }
  //                break;
  //            }
  //          }
  //          else if (cd[0] == "ДУ")
  //          {
  //            switch (cd[1])
  //            {
  //              case "ДУ":
  //                var vdu = du.FirstOrDefault(p => p.Code == cd[2]);
  //                await SendBotMsgAsync(
  //                  callbackQuery: callbackQuery,
  //                  text: vdu.Name,
  //                  replyMarkup: new InlineKeyboardMarkup(getInlineKbdPifParam(duParam, vdu.Code, "ДУ_ДУ"))
  //                );
  //                break;

  //              case "ДОК":
  //                var files = Directory.GetFiles(@$"V:\VOL1\ASSETS\4All\uralsib_am_bot\ДУ\ДОК\{cd[2]}", $"*.pdf");
  //                if (files.Length > 0)
  //                {
  //                  var msg = await BotService.SendDocumentAsync(
  //                    chatId: callbackQuery.Message.Chat.Id,
  //                    document: new InputOnlineFile(new MemoryStream(File.ReadAllBytes(files[0])), Path.GetFileName(files[0])),
  //                    replyMarkup: new InlineKeyboardMarkup(InlineKbdDuDoc)
  //                  );
  //                  await BotService.DeleteMessageAsync(chatId: callbackQuery.Message.Chat.Id, messageId: callbackQuery.Message.MessageId);
  //                }
  //                else
  //                {
  //                  await SendBotMsgAsync(callbackQuery: callbackQuery, text: "Файл не найден", replyMarkup: new InlineKeyboardMarkup(getInlineKbdIisPam()));
  //                }
  //                break;
  //            }
  //          }

  //          else if (cd[0] == "ИИС")
  //          {
  //            switch (cd[1])
  //            {
  //              case "ИИС":
  //                var viis = iis.FirstOrDefault(p => p.Code == cd[2]);
  //                await SendBotMsgAsync(
  //                  callbackQuery: callbackQuery,
  //                  text: viis.Name,
  //                  replyMarkup: new InlineKeyboardMarkup(getInlineKbdPifParam(iisParam, viis.Code, "ИИС_ИИС"))
  //                );
  //                break;

  //              case "КАР":
  //                {
  //                  var card = int.Parse(cd[2]);
  //                  var files = Directory.GetFiles(@$"V:\VOL1\ASSETS\4All\uralsib_am_bot\ИИС\КАР\{cd[2]}", "*.pdf");
  //                  if (files.Length >= card)
  //                  {
  //                    var msg = await BotService.SendDocumentAsync(
  //                      chatId: callbackQuery.Message.Chat.Id,
  //                      document: new InputOnlineFile(new MemoryStream(File.ReadAllBytes(files[card - 1])), Path.GetFileName(files[card - 1])),
  //                      replyMarkup: new InlineKeyboardMarkup(getInlineKbdIisCard())
  //                    );
  //                    await BotService.DeleteMessageAsync(chatId: callbackQuery.Message.Chat.Id, messageId: callbackQuery.Message.MessageId);
  //                  }
  //                }
  //                break;

  //              case "ПАМ":
  //                {
  //                  var files = Directory.GetFiles(@$"V:\VOL1\ASSETS\4All\uralsib_am_bot\ИИС\ПАМ\{cd[2]}", "*.pdf");
  //                  if (files.Length > 0)
  //                  {
  //                    var msg = await BotService.SendDocumentAsync(
  //                      chatId: callbackQuery.Message.Chat.Id,
  //                      document: new InputOnlineFile(new MemoryStream(File.ReadAllBytes(files[0])), Path.GetFileName(files[0])),
  //                      replyMarkup: new InlineKeyboardMarkup(getInlineKbdIisPam())
  //                    );
  //                    await BotService.DeleteMessageAsync(chatId: callbackQuery.Message.Chat.Id, messageId: callbackQuery.Message.MessageId);
  //                  }
  //                  else
  //                  {
  //                    await SendBotMsgAsync(callbackQuery: callbackQuery, text: "Файл не найден", replyMarkup: new InlineKeyboardMarkup(getInlineKbdIisPam()));
  //                  }
  //                }
  //                break;

  //              case "НАЛ":
  //                {
  //                  var files = Directory.GetFiles(@$"V:\VOL1\ASSETS\4All\uralsib_am_bot\ИИС\НАЛ\{cd[2]}", "*.pdf");
  //                  if (files.Length > 0)
  //                  {
  //                    var msg = await BotService.SendDocumentAsync(
  //                      chatId: callbackQuery.Message.Chat.Id,
  //                      document: new InputOnlineFile(new MemoryStream(File.ReadAllBytes(files[0])), Path.GetFileName(files[0])),
  //                      replyMarkup: new InlineKeyboardMarkup(getInlineKbdIisNal())
  //                    );
  //                    await BotService.DeleteMessageAsync(chatId: callbackQuery.Message.Chat.Id, messageId: callbackQuery.Message.MessageId);
  //                  }
  //                  else
  //                  {
  //                    await SendBotMsgAsync(callbackQuery: callbackQuery, text: "Файл не найден", replyMarkup: new InlineKeyboardMarkup(getInlineKbdIisNal()));
  //                  }
  //                }
  //                break;

  //              case "ДОК":
  //                {
  //                  var files = Directory.GetFiles(@$"V:\VOL1\ASSETS\4All\uralsib_am_bot\ИИС\ДОК\{cd[2]}", "*.pdf");
  //                  if (files.Length > 0)
  //                  {
  //                    var msg = await BotService.SendDocumentAsync(
  //                      chatId: callbackQuery.Message.Chat.Id,
  //                      document: new InputOnlineFile(new MemoryStream(File.ReadAllBytes(files[0])), Path.GetFileName(files[0])),
  //                      replyMarkup: new InlineKeyboardMarkup(InlineKbdIisDoc)
  //                    );
  //                    await BotService.DeleteMessageAsync(chatId: callbackQuery.Message.Chat.Id, messageId: callbackQuery.Message.MessageId);
  //                  }
  //                  else
  //                  {
  //                    await SendBotMsgAsync(callbackQuery: callbackQuery, text: "Файл не найден", replyMarkup: new InlineKeyboardMarkup(getInlineKbdIisPam()));
  //                  }
  //                }
  //                break;
  //            }
  //          }
  //        }
  //        else if (cd.Count() == 4)
  //        {
  //          if (cd[0] == "ПИФ")
  //          {
  //            switch (cd[1])
  //            {
  //              case "ПИФ":
  //                var vpif = pif.FirstOrDefault(p => p.Brief == cd[2]);
  //                var cmd = cd[3];
  //                var inlineKeyboard = new InlineKeyboardMarkup(getInlineKbdPifParam(pifParam, vpif.Brief, "ПИФ_ПИФ"));
  //                switch (cmd)
  //                {
  //                  case "Desc1":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      text: vpif.Desc1,
  //                      parseMode: ParseMode.Html,
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Desc2":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      text: vpif.Desc2,
  //                      parseMode: ParseMode.Html,
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Strat":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      text: vpif.Strat,
  //                      parseMode: ParseMode.Html,
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Price":
  //                    var r = pifRates.FirstOrDefault(p => p.Brief == vpif.Brief);
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      parseMode: ParseMode.MarkdownV2,
  //                      text: $"*{vpif.Name}*\nДата: _{r.datep.Replace(".", "\\.")}_\nЦена пая: _{r.pricePaiP.Replace(".", "\\.")}_\nСЧА: _{r.schaP.Replace(".", "\\.")}р\\._\n\nДата: _{r.date.Replace(".", "\\.")}_\nЦена пая: _{r.pricePai.Replace(".", "\\.")}_\nСЧА: _{r.scha.Replace(".", "\\.")}р\\._\n\nИзменение цены пая: _{r.pricePaiD.ToString("N2").Replace(".", "\\.").Replace("-", "\\-")}%_\nИзменение СЧА: _{r.schaD.ToString("N2").Replace(".", "\\.").Replace("-", "\\-")}%_",
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Pref":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      text: vpif.Pref,
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Target":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      text: vpif.Target,
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Yield":
  //                    var y = pifYield.FirstOrDefault(p => p.Brief == vpif.Brief);
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      parseMode: ParseMode.MarkdownV2,
  //                      text: $"*{vpif.Name}*\nДоходность:\nС начала года: _{y.yy.ToString("N2").Replace(".", "\\.").Replace("-", "\\-")}%_\nЗа год: _{y.y1.ToString("N2").Replace(".", "\\.").Replace("-", "\\-")}%_\nЗа 3 года: _{y.y3.ToString("N2").Replace(".", "\\.").Replace("-", "\\-")}%_\nЗа 5 лет: _{y.y5.ToString("N2").Replace(".", "\\.").Replace("-", "\\-")}%_",
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Commis":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      text: vpif.Commis,
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Info":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      text: vpif.Info,
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Structure":
  //                    var b = pifBranch.FirstOrDefault(p => p.Brief == vpif.Brief);
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      parseMode: ParseMode.MarkdownV2,
  //                      text: $"*{vpif.Name}*{b.Value.Replace(".", "\\.").Replace("-", "\\-")}",
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Chart":
  //                    {
  //                      var ch = pifChart.FirstOrDefault(p => p.Brief == vpif.Brief);
  //                      InputOnlineFile iof;
  //                      if (ch != null)
  //                      {
  //                        iof = new InputOnlineFile(ch.FileId);
  //                      }
  //                      else
  //                      {
  //                        iof = new InputOnlineFile(new MemoryStream(File.ReadAllBytes($"charts\\{vpif.Brief}.png")));
  //                      }
  //                      var msg = await BotService.SendPhotoAsync(
  //                        chatId: callbackQuery.Message.Chat.Id,
  //                        photo: iof,
  //                        replyMarkup: inlineKeyboard
  //                      );
  //                      if (msg.Photo.Length > 0)
  //                      {
  //                        if (ch == null)
  //                          pifChart.Add(new PIFChart { Brief = vpif.Brief, FileId = msg.Photo[0].FileId });
  //                      }
  //                      await BotService.DeleteMessageAsync(chatId: callbackQuery.Message.Chat.Id, messageId: callbackQuery.Message.MessageId);
  //                    }
  //                    break;

  //                  case "One":
  //                    {
  //                      InputOnlineFile iof = null;
  //                      var ch = pifOnepage.FirstOrDefault(p => p.Brief == vpif.Brief);
  //                      if (ch != null)
  //                      {
  //                        iof = new InputOnlineFile(ch.FileId);
  //                      }
  //                      else
  //                      {
  //                        var files = Directory.GetFiles(@$"V:\VOL1\ASSETS\4All\uralsib_am_bot\ПИФ\ПИФ\{vpif.Brief}\ONE", "*.pdf");
  //                        if (files.Length > 0)
  //                        {
  //                          iof = new InputOnlineFile(new MemoryStream(File.ReadAllBytes(files[0])), Path.GetFileName(files[0]));
  //                        }
  //                      }
  //                      if (iof != null)
  //                      {
  //                        var msg = await BotService.SendDocumentAsync(
  //                          chatId: callbackQuery.Message.Chat.Id,
  //                          document: iof,
  //                          replyMarkup: inlineKeyboard
  //                        );
  //                        if (msg.Document != null)
  //                        {
  //                          if (ch == null)
  //                            pifOnepage.Add(new PIFChart { Brief = vpif.Brief, FileId = msg.Document.FileId });
  //                        }
  //                        await BotService.DeleteMessageAsync(chatId: callbackQuery.Message.Chat.Id, messageId: callbackQuery.Message.MessageId);
  //                      }
  //                      else
  //                        await SendBotMsgAsync(callbackQuery: callbackQuery, text: "Файл не найден", replyMarkup: inlineKeyboard);
  //                    }
  //                    break;

  //                  case "Forecast":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      text: vpif.Forecast,
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Rules":
  //                    {
  //                      var files = Directory.GetFiles(@$"V:\VOL1\ASSETS\4All\uralsib_am_bot\ПИФ\ПРА\", "*" + vpif.Name + "*.pdf");
  //                      InputOnlineFile iof = null;
  //                      if (files.Length > 0)
  //                        iof = new InputOnlineFile(new MemoryStream(File.ReadAllBytes(files[0])), Path.GetFileName(files[0]));

  //                      if (iof != null)
  //                      {
  //                        var msg = await BotService.SendDocumentAsync(
  //                          chatId: callbackQuery.Message.Chat.Id,
  //                          document: iof,
  //                          replyMarkup: inlineKeyboard
  //                        );
  //                        await BotService.DeleteMessageAsync(chatId: callbackQuery.Message.Chat.Id, messageId: callbackQuery.Message.MessageId);
  //                      }
  //                      else
  //                        await SendBotMsgAsync(callbackQuery: callbackQuery, text: "Файл не найден", replyMarkup: inlineKeyboard);
  //                    }
  //                    break;
  //                  default:
  //                    await BotService.SendTextMessageAsync(
  //                      chatId: callbackQuery.Message.Chat.Id,
  //                      text: $"Received {callbackQuery.Data}",
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;
  //                }
  //                break;

  //              case "FAQ":
  //              case "ОБУ":
  //                break;

  //            }
  //          }
  //          else if (cd[0] == "ИИС")
  //          {
  //            switch (cd[1])
  //            {
  //              case "ИИС":
  //                var viis = iis.FirstOrDefault(p => p.Code == cd[2]);
  //                var cmd = cd[3];
  //                var inlineKeyboard = new InlineKeyboardMarkup(getInlineKbdPifParam(iisParam, viis.Code, "ИИС_ИИС"));
  //                InputOnlineFile iof = null;
  //                switch (cmd)
  //                {

  //                  case "Цель":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      text: $"{viis.Name}\n{viis.Target}",
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Объекты":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      text: $"{viis.Name}\n{viis.Object}",
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Преим":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      text: $"{viis.Name}\n{viis.Pref}",
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Условия":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      parseMode: ParseMode.Html,
  //                      text: $"{viis.Name}\n{viis.Term}",
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Комиссии":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      parseMode: ParseMode.Html,
  //                      text: $"{viis.Name}\n{viis.Commis}",
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Результаты":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      text: $"{viis.Name}\n{viis.Result}",
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Структура":
  //                    var b = duBranch.FirstOrDefault(p => p.Brief == viis.Code);
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      parseMode: ParseMode.MarkdownV2,
  //                      text: $"*{viis.Name}*{b.Value.Replace(".", "\\.").Replace("-", "\\-")}",
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Состав":
  //                    break;

  //                  case "Динамика":
  //                    {
  //                      var ch = duChart.FirstOrDefault(p => p.Brief == viis.Code);
  //                      if (ch != null)
  //                      {
  //                        iof = new InputOnlineFile(ch.FileId);
  //                      }
  //                      else
  //                      {
  //                        iof = new InputOnlineFile(new MemoryStream(File.ReadAllBytes($"charts\\{viis.Code}.png")));
  //                      }
  //                      var msg = await BotService.SendPhotoAsync(
  //                        chatId: callbackQuery.Message.Chat.Id,
  //                        photo: iof,
  //                        replyMarkup: inlineKeyboard
  //                      );
  //                      if (msg.Photo.Length > 0)
  //                      {
  //                        if (ch == null)
  //                          pifChart.Add(new PIFChart { Brief = viis.Code, FileId = msg.Photo[0].FileId });
  //                      }
  //                      await BotService.DeleteMessageAsync(chatId: callbackQuery.Message.Chat.Id, messageId: callbackQuery.Message.MessageId);
  //                    }
  //                    break;

  //                  case "Управ":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      text: viis.Trader,
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "One":
  //                    {
  //                      var ch = pifOnepage.FirstOrDefault(p => p.Brief == viis.Code);
  //                      if (ch != null)
  //                      {
  //                        iof = new InputOnlineFile(ch.FileId);
  //                      }
  //                      else
  //                      {
  //                        var files = Directory.GetFiles(@$"V:\VOL1\ASSETS\4All\uralsib_am_bot\ИИС\ONE", viis.Name1 + "*.pdf");
  //                        if (files.Length > 0)
  //                          iof = new InputOnlineFile(new MemoryStream(File.ReadAllBytes(files[0])), Path.GetFileName(files[0]));
  //                      }
  //                      if (iof != null)
  //                      {
  //                        var msg = await BotService.SendDocumentAsync(
  //                          chatId: callbackQuery.Message.Chat.Id,
  //                          document: iof,
  //                          replyMarkup: inlineKeyboard
  //                        );
  //                        if (msg.Document != null)
  //                        {
  //                          if (ch == null)
  //                            pifOnepage.Add(new PIFChart { Brief = viis.Code, FileId = msg.Document.FileId });
  //                        }
  //                        await BotService.DeleteMessageAsync(chatId: callbackQuery.Message.Chat.Id, messageId: callbackQuery.Message.MessageId);
  //                      }
  //                      else
  //                      {
  //                        await SendBotMsgAsync(
  //                          callbackQuery: callbackQuery,
  //                          text: "Файл не найден",
  //                          replyMarkup: inlineKeyboard
  //                        );
  //                      }
  //                    }
  //                    break;

  //                  case "Onep":
  //                    {
  //                      var files = Directory.GetFiles(@$"V:\VOL1\ASSETS\4All\uralsib_am_bot\ИИС\ONEP", "*" + viis.Name1 + "»*.pdf");
  //                      if (files.Length > 0)
  //                        iof = new InputOnlineFile(new MemoryStream(File.ReadAllBytes(files[0])), Path.GetFileName(files[0]));

  //                      if (iof != null)
  //                      {
  //                        var msg = await BotService.SendDocumentAsync(
  //                          chatId: callbackQuery.Message.Chat.Id,
  //                          document: iof,
  //                          replyMarkup: inlineKeyboard
  //                        );
  //                        await BotService.DeleteMessageAsync(chatId: callbackQuery.Message.Chat.Id, messageId: callbackQuery.Message.MessageId);
  //                      }
  //                      else
  //                      {
  //                        await SendBotMsgAsync(
  //                          callbackQuery: callbackQuery,
  //                          text: "Файл не найден",
  //                          replyMarkup: inlineKeyboard
  //                        );
  //                      }
  //                    }
  //                    break;

  //                }
  //                break;
  //            }
  //          }
  //          else if (cd[0] == "ДУ")
  //          {
  //            switch (cd[1])
  //            {
  //              case "ДУ":
  //                var vdu = du.FirstOrDefault(p => p.Code == cd[2]);
  //                var cmd = cd[3];
  //                var inlineKeyboard = new InlineKeyboardMarkup(getInlineKbdPifParam(duParam, vdu.Code, "ДУ_ДУ"));
  //                switch (cmd)
  //                {
  //                  case "Доходность":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      parseMode: ParseMode.Html,
  //                      text: $"<i>{vdu.Name}</i>\n{vdu.Yield}",
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Риск":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      parseMode: ParseMode.Html,
  //                      text: $"<i>{vdu.Name}</i>\n{vdu.Risk}",
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Стратегия":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      parseMode: ParseMode.Html,
  //                      text: $"<i>{vdu.Name}</i>\n{vdu.Strategy}",
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Цель":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      parseMode: ParseMode.Html,
  //                      text: $"<i>{vdu.Name}</i>\n{vdu.Target}",
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Объекты":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      parseMode: ParseMode.Html,
  //                      text: $"<i>{vdu.Name}</i>\n{vdu.Object}",
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "ДляКого":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      text: vdu.ForWhoom,
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Срок":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      parseMode: ParseMode.Html,
  //                      text: $"<i>{vdu.Name}</i>\n{vdu.Srok}",
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Условия":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      parseMode: ParseMode.Html,
  //                      text: $"<i>{vdu.Name}</i>\n{vdu.Term}",
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Управ":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      text: vdu.Trader,
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Advantage":
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      parseMode: ParseMode.Html,
  //                      text: vdu.Advantage,
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Структура":
  //                    var b = duBranch.FirstOrDefault(p => p.Brief == vdu.Code);
  //                    await SendBotMsgAsync(
  //                      callbackQuery: callbackQuery,
  //                      parseMode: ParseMode.Html,
  //                      text: $"<i>{vdu.Name}</i>{b.Value}",
  //                      replyMarkup: inlineKeyboard
  //                    );
  //                    break;

  //                  case "Динамика":
  //                    {
  //                      InputOnlineFile iof = null;
  //                      var ch = duChart.FirstOrDefault(p => p.Brief == vdu.Code);
  //                      if (ch != null)
  //                      {
  //                        iof = new InputOnlineFile(ch.FileId);
  //                      }
  //                      else
  //                      {
  //                        iof = new InputOnlineFile(new MemoryStream(File.ReadAllBytes($"charts\\{vdu.Code}.png")));
  //                      }
  //                      var msg = await BotService.SendPhotoAsync(
  //                        chatId: callbackQuery.Message.Chat.Id,
  //                        photo: iof,
  //                        replyMarkup: inlineKeyboard
  //                      );
  //                      if (msg.Photo.Length > 0)
  //                      {
  //                        if (ch == null)
  //                          pifChart.Add(new PIFChart { Brief = vdu.Code, FileId = msg.Photo[0].FileId });
  //                      }
  //                      await BotService.DeleteMessageAsync(chatId: callbackQuery.Message.Chat.Id, messageId: callbackQuery.Message.MessageId);
  //                    }
  //                    break;

  //                  case "One":
  //                    {
  //                      InputOnlineFile iof = null;
  //                      var ch = pifOnepage.FirstOrDefault(p => p.Brief == vdu.Code);
  //                      if (ch != null)
  //                      {
  //                        iof = new InputOnlineFile(ch.FileId);
  //                      }
  //                      else
  //                      {
  //                        var files = Directory.GetFiles(@$"V:\VOL1\ASSETS\4All\uralsib_am_bot\ДУ\ДУ\{vdu.Brief}\ONE", "*.pdf");
  //                        if (files.Length > 0)
  //                          iof = new InputOnlineFile(new MemoryStream(File.ReadAllBytes(files[0])), Path.GetFileName(files[0]));
  //                      }
  //                      if (iof != null)
  //                      {
  //                        var msg = await BotService.SendDocumentAsync(
  //                          chatId: callbackQuery.Message.Chat.Id,
  //                          document: iof,
  //                          replyMarkup: inlineKeyboard
  //                        );
  //                        if (msg.Document != null)
  //                        {
  //                          if (ch == null)
  //                            pifOnepage.Add(new PIFChart { Brief = vdu.Code, FileId = msg.Document.FileId });
  //                        }
  //                        await BotService.DeleteMessageAsync(chatId: callbackQuery.Message.Chat.Id, messageId: callbackQuery.Message.MessageId);
  //                      }
  //                      else
  //                      {
  //                        await SendBotMsgAsync(callbackQuery: callbackQuery, text: "Файл не найден", replyMarkup: inlineKeyboard);
  //                      }
  //                    }
  //                    break;

  //                  case "Onep":
  //                    {
  //                      InputOnlineFile iof = null;
  //                      var files = Directory.GetFiles(@$"V:\VOL1\ASSETS\4All\uralsib_am_bot\ДУ\ONEP", "ДУ " + vdu.Name1 + "Обучающая.pdf");
  //                      if (files.Length > 0)
  //                        iof = new InputOnlineFile(new MemoryStream(File.ReadAllBytes(files[0])), Path.GetFileName(files[0]));

  //                      if (iof != null)
  //                      {
  //                        var msg = await BotService.SendDocumentAsync(
  //                          chatId: callbackQuery.Message.Chat.Id,
  //                          document: iof,
  //                          replyMarkup: inlineKeyboard
  //                        );
  //                        await BotService.DeleteMessageAsync(chatId: callbackQuery.Message.Chat.Id, messageId: callbackQuery.Message.MessageId);
  //                      }
  //                      else
  //                      {
  //                        await SendBotMsgAsync(callbackQuery: callbackQuery, text: "Файл не найден", replyMarkup: inlineKeyboard);
  //                      }
  //                    }
  //                    break;

  //                }
  //                break;
  //            }
  //          }
  //        }
  //      }
  //    }
  //    catch (Telegram.Bot.Exceptions.MessageIsNotModifiedException)
  //    {
  //      //await Bot.AnswerCallbackQueryAsync(callbackQueryId: callbackQuery.Id, text: "Message Is NotModified");
  //    }
  //    catch (Exception ex)
  //    {
  //    }

  //    InlineKeyboardButton[][] getInlineKbdPif1()
  //    {
  //      return new[]
  //      {
  //        new[]
  //        {
  //          InlineKeyboardButton.WithCallbackData("Список фондов", "ПИФ_ПИФ"),
  //          InlineKeyboardButton.WithCallbackData("Карточки клиента", "ПИФ_КАР")
  //        },
  //          new[]
  //        {
  //          InlineKeyboardButton.WithCallbackData("FAQ", "ПИФ_FAQ"),
  //          InlineKeyboardButton.WithCallbackData("Обучение", "ПИФ_ОБУ")
  //        },
  //        new[]
  //        {
  //          InlineKeyboardButton.WithCallbackData("Цены пая", "ПИФ_Price"),
  //          InlineKeyboardButton.WithCallbackData("Назад", "Продукт")
  //        }
  //      };
  //    }
  //    InlineKeyboardButton[][] getInlineKbdIis1()
  //    {
  //      return new[]
  //      {
  //        new[]
  //        {
  //          InlineKeyboardButton.WithCallbackData("Список стратегий", "ИИС_ИИС"),
  //          InlineKeyboardButton.WithCallbackData("Карточки клиента", "ИИС_КАР")
  //        },
  //        new[]
  //        {
  //          InlineKeyboardButton.WithCallbackData("Памятки", "ИИС_ПАМ"),
  //          InlineKeyboardButton.WithCallbackData("Налоговый вычет", "ИИС_НАЛ")
  //        },
  //        new[]
  //        {
  //          InlineKeyboardButton.WithCallbackData("FAQ", "ИИС_FAQ"),
  //          InlineKeyboardButton.WithCallbackData("Обучение", "ИИС_ОБУ")
  //        },
  //        new[]
  //        {
  //          InlineKeyboardButton.WithCallbackData("Документы ИИС", "ИИС_ДОК"),
  //          InlineKeyboardButton.WithCallbackData("Назад", "Продукт")
  //        }
  //      };
  //    }
  //    InlineKeyboardButton[][] getInlineKbdDu1()
  //    {
  //      return new[]
  //      {
  //        new[]
  //        {
  //          InlineKeyboardButton.WithCallbackData("Список стратегий", "ДУ_ДУ"),
  //          InlineKeyboardButton.WithCallbackData("Документы ДУ", "ДУ_ДОК")
  //        },
  //        new[]
  //        {
  //          InlineKeyboardButton.WithCallbackData("FAQ", "ДУ_FAQ"),
  //          InlineKeyboardButton.WithCallbackData("Обучение", "ДУ_ОБУ")
  //        },
  //        new[]
  //        {
  //          InlineKeyboardButton.WithCallbackData("Назад", "Продукт")
  //        }
  //      };
  //    }

  //  }

  //  private static void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
  //  {
  //    Console.WriteLine("Received error: {0} — {1}",
  //        receiveErrorEventArgs.ApiRequestException.ErrorCode,
  //        receiveErrorEventArgs.ApiRequestException.Message
  //    );
  //  }


  //}
}
