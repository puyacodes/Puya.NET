﻿using Puya.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Puya.Mail
{
    public abstract class FakeMailManager<TMailConfig> : IMailManager where TMailConfig: class, IMailConfig, new()
    {
        private IMailConfig _config;
        public IMailConfig Config
        {
            get
            {
                if (_config == null)
                    _config = new TMailConfig();

                return _config;
            }
            set
            {
                if (value is TMailConfig)
                {
                    _config = value;
                }
                else
                {
                    throw new ApplicationException("Invalid mail config type");
                }
            }
        }
        public TMailConfig StrongConfig
        {
            get { return Config as TMailConfig; }
        }
        protected abstract void LogInternal(string data);
        protected virtual void Log(string data, [CallerMemberName] string memberName = "")
        {
            var _data = "Method: " + memberName + "\r\n" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fffff") + "\r\n" + data + "\r\n" + new string('-', 50) + "\r\n";

            LogInternal(_data);
        }
        public virtual bool Send(string to, string subject, string body, bool isHtml = false)
        {
            var data = $"To: {to}\r\nSubject: {subject}\r\nBody:{body}";

            Log(data);

            return true;
        }

        public bool Send(string to, string subject, string body, bool isHtml, IEnumerable<string> cc, IEnumerable<string> bcc)
        {
            var data = $"To: {to}\r\nSubject: {subject}\r\nIsHtml:{isHtml}\r\nCC: {cc?.Join(",")}\r\nBCC: {bcc?.Join(",")}\r\nBody:{body}";

            Log(data);

            return true;
        }

        public virtual Task<bool> SendAsync(string to, string subject, string body, bool isHtml = false)
        {
            return Task.Run(() =>
            {
                var data = $"To: {to}\r\nSubject: {subject}\r\nBody:{body}";

                Log(data);

                return true;
            });
        }

        public virtual Task<bool> SendAsync(string to, string subject, string body, bool isHtml, IEnumerable<string> cc, IEnumerable<string> bcc)
        {
            return Task.Run(() =>
            {
                var data = $"To: {to}\r\nSubject: {subject}\r\nIsHtml:{isHtml}\r\nCC: {cc?.Join(",")}\r\nBCC: {bcc?.Join(",")}\r\nBody:{body}";

                Log(data);

                return true;
            });
        }
    }

    public abstract class FakeMailManager : FakeMailManager<FakeMailConfig>
    {
    }
}
