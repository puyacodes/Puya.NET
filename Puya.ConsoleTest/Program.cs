using Puya.Collections;
using Puya.Extensions;
using Puya.Logging;
using Puya.Service;
using Puya.Sms;
using System;

namespace Puya.ConsoleTest
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Birth { get; set; }
    }
    internal class Program
    {
        static void test_logger(ILogger logger)
        {
            logger.Info("test", "hello");
            
            logger.Debug("BeginJob", "this is a message", () => new { a = 10, b = true, c = "test" });
            logger.Log(new Logging.Log
            {
                LogType = Logging.LogType.Info,
                AppId = 12,
                User = "user7637",
                BrowserName = "Edge",
                BrowserVersion = "12.0",
                Category = "Signing",
                ContentType = "application/json",
                Cookies = "",
                Data = new { id = 123, name = "ali", age = 34 },
                Form = "",
                Headers = "x-debug: 0, x-log: normal",
                Ip = "127.0.0.1",
                Message = "request data",
                OperationResult = OperationResult.Success,
                Method = "POST",
                Url = "https://www.mywebsite.com/api/user/10",
                Referrer = "https://www.google.com",
                MemberName = "test_console_logger",
                File = "C:\\projects\\Puya.ConsoleTest\\Program.cs",
                Line = 18,
                StackTrace = Environment.StackTrace
            });
            
            logger.Error("an unexpected situation happened. please check logs!", new { size = 300, code = "iusyhdiluy87214" });
        }
        static void test_smslogger()
        {
            var smslogger = new SmsLoggerJsonFile(new LogProviderBase());

            smslogger.Log(new SmsLog
            {
                Provider = "asanak",
                LineNo = "10002000",
                Category = "otp-login",
                MobileNo = "09123456789",
                Response = "ok",
                Status = "Success",
                RefCode = "100",
                Success = true,
                Data = new { templateCode = "otp-login" },
            });
            smslogger.Log(new SmsLog
            {
                Provider = "asanak",
                LineNo = "10002000",
                Category = "otp-login",
                MobileNo = "09123456789",
                Response = "ok",
                Status = "Success",
                RefCode = "100",
                Success = true,
                Data = new { templateCode = "otp-login" },
            });
            smslogger.Log(new SmsLog
            {
                Provider = "asanak",
                LineNo = "10002000",
                Category = "otp-login",
                MobileNo = "09123456789",
                Response = "ok",
                Status = "Success",
                RefCode = "100",
                Success = true,
                Data = new { templateCode = "otp-login" },
            });
            Console.WriteLine("done");
        }
        static void test_to()
        {
            var d = new DynamicModel();

            d.Add("name", "ali");
            d.Add("age", 34);

            var p = d.To<Person>();

            p.Print();
        }
        static void Main(string[] args)
        {
            //test_logger(new ConsoleLogger());
            //test_logger(new DebugLogger());
            //test_logger(new FreeConsoleLogger());
            //test_smslogger();
            test_to();

            //Console.WriteLine(new { name = "ali", age = 34 }.SafeSerialize(false));
            Console.ReadKey();
        }
    }
}
