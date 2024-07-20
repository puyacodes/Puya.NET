﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Puya.Service;
using Puya.Data;
using Puya.Conversion;

namespace Puya.ServiceModel
{
    public abstract class TapBaseDbReaderServiceAction<TBaseService, TConfig, TRequest, TResponse, TData> : TapBaseServiceAction<TBaseService, TConfig, TRequest, TResponse>
        where TConfig : TapBaseConfig, new()
        where TBaseService : TapBaseActionBasedService<TConfig>, IService<TConfig>
        where TRequest : TapBaseDbServiceRequest
        where TResponse : ServiceResponse<IList<TData>>, new()
    {
        public TapBaseDbReaderServiceAction(TBaseService owner): base(owner)
        { }
        private async Task DoRun(TRequest request, TResponse response, bool async, CancellationToken cancellation)
        {
            if (async)
            {
                response.Data = await Db.ExecuteReaderCommandAsync<TData>($"usp1_{Owner.Name}_{Name}", request, cancellation);
            }
            else
            {
                response.Data = Db.ExecuteReaderCommand<TData>($"usp1_{Owner.Name}_{Name}", request);
            }

            var result = SafeClrConvert.ToString(request.Result.Value);

            response.SetStatus(result);
            response.Message = SafeClrConvert.ToString(request.Message.Value);
        }
        protected override void RunInternal(TRequest request, TResponse response)
        {
            DoRun(request, response, false, CancellationToken.None).Wait();
        }
        protected override async Task RunInternalAsync(TRequest request, TResponse response, CancellationToken token)
        {
            await DoRun(request, response, true, CancellationToken.None);
        }
    }
}
