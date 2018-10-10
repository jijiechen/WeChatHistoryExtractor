using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using OpenQA.Selenium.Remote;

namespace WeChatHistoryExtractor
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.Title = "WeChat History Extractor";
            AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) =>
            {
                var ex = eventArgs.ExceptionObject as Exception;
                if (ex != null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine(ex);   
                    Console.ResetColor();
                }

                Environment.Exit(-1);
            };
            
            
            var driver = InitDriver();
            var extractor = new Extractor(driver);

            var messages = extractor.ExtractMessage();
            
            var fileName = OutputMessagesToFile(messages);
            Console.WriteLine($"{messages.Count} messages wrote to {fileName}");
        }

        static string OutputMessagesToFile(List<ChatMessage> messages)
        {
            var random = Guid.NewGuid().ToString("N").Substring(4, 8);
            var fileName = Path.Combine(Directory.GetCurrentDirectory(), "messages", $"chat-{random}.json");
            var dir = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            
            var json = JsonConvert.SerializeObject(messages);
            File.WriteAllText(fileName, json, Encoding.UTF8);
            return fileName;
        }

        static MacDriver InitDriver()
        {
            var capabilities = new DesiredCapabilities(new Dictionary<string, object>()
            {
                {"app", "Dock"},
                {"platformName", "Mac"},
                {"deviceName", "Mac"},
                {"newCommandTimeout", "1000"},
                {"cookies", defaultCookies()}
            });

            var localServerUri = new Uri("http://127.0.0.1:4723/wd/hub");
            return new MacDriver(localServerUri, capabilities);
        }


        static Dictionary<string, string> defaultCookies()
        {
            var defaultGlobalDiagnosticsDirectory = Directory.GetCurrentDirectory();
            var defaultLoopDelay_sec = 0.100;
            var defaultCommandDelay_sec = 0.100;
            var defaultImplicitTimeout_sec = 5.000;
            var defaultMouseSpeed = 5;
            return new Dictionary<string, string>
            {
                {"loop_delay", defaultLoopDelay_sec.ToString()},
                {"command_delay", defaultCommandDelay_sec.ToString()},
                {"implicit_timeout", defaultImplicitTimeout_sec.ToString()},
                {"mouse_speed", defaultMouseSpeed.ToString()},
                {"screen_shot_on_error", "true"},
                {"global_diagnostics_directory", defaultGlobalDiagnosticsDirectory}
            };
        }
    }
}
