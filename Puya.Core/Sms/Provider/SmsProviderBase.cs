using Puya.Debugging;
using Puya.Extensions;
using Puya.Service;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Puya.Sms
{
    public abstract class SmsProviderBase<TConfig, TTemplateParams> : ISmsProvider
        where TConfig : new()
    {
        #region ctor, fields and props
        TConfig _config;
        readonly ISmsLogger _logger;
        readonly IDebugger _debugger;
        readonly ILogProvider _logProvider;
        readonly ISmsTemplateCodeAdaptor<TTemplateParams> _templateCodeAdaptor;
        public SmsProviderBase(TConfig config,
                                ISmsLogger logger,
                                IDebugger debugger,
                                ILogProvider logProvider,
                                ISmsTemplateCodeAdaptor<TTemplateParams> templateCodeAdaptor)
        {
            _config = config;
            _logger = logger;
            _debugger = debugger;
            _logProvider = logProvider;
            _templateCodeAdaptor = templateCodeAdaptor;
        }
        public abstract string Type { get; }
        public virtual TConfig Config
        {
            get
            {
                if (_config == null)
                {
                    _config = new TConfig();
                }

                return _config;
            }
            set { _config = value; }
        }
        public ISmsLogger Logger
        {
            get { return _logger; }
        }
        public ILogProvider LogProvider
        {
            get { return _logProvider; }
        }
        public IDebugger Debugger
        {
            get { return _debugger; }
        }
        public ISmsTemplateCodeAdaptor<TTemplateParams> TemplateCodeAdaptor
        {
            get { return _templateCodeAdaptor; }
        }
        #endregion
        protected abstract Task<SmsSendResponse> SendInternalAsync(SmsSendRequest request, SmsLog log, CancellationToken cancellation);
        public async Task<SmsSendResponse> SendAsync(SmsSendRequest request, CancellationToken cancellation)
        {
            var response = new SmsSendResponse();
            var log = new SmsLog
            {
                Category = request.Category,
                MobileNo = request.Mobile,
                Message = request.Message,
                Provider = Type,
                Data = new { request.TemplateCode, request.Parameters, request.OtherData },
            };

            try
            {
                var sr = await SendInternalAsync(request, log, cancellation);

                if (sr != null)
                {
                    log.Response = sr.Data?.Response;
                    log.RefCode = sr.Data?.RefCode;
                    log.Error = sr.Data?.Error;

                    response.Copy(sr);
                }
                else
                {
                    response.Succeeded();
                }
            }
            catch (Exception e)
            {
                log.Error = e;

                response.Failed(e);
            }

            log.Success = response.Success;
            log.Status = response.Status;

            if (_debugger.IsDebugging)
            {
                _logProvider?.Debug(GetType().Name ,"Request", request, LogSource.Service);
                _logProvider?.Debug(GetType().Name ,"Config", Config, LogSource.Service);
                _logProvider?.Info(GetType().Name , "Log", log, LogSource.Service);
            }

            try
            {
                await Logger?.LogAsync(log, cancellation);
            }
            catch { }

            return response;
        }
    }
    public abstract class SmsProviderBase<TConfig> : SmsProviderBase<TConfig, string>
        where TConfig : new()
    {
        public SmsProviderBase(TConfig config,
                                ISmsLogger logger,
                                IDebugger debugger,
                                ILogProvider logProvider,
                                ISmsTemplateCodeAdaptor<string> templateCodeAdaptor)
            : base(config, logger, debugger, logProvider, templateCodeAdaptor)
        {
        }
    }
}
