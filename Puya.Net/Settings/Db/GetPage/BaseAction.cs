using Puya.Service;
using Puya.ServiceModel;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Puya.Settings.Service.Models;

namespace Puya.Settings.Service.Db
{
	public abstract partial class TapDbSettingsGetPageBaseAction:
        TapBaseServiceAction<TapDbSettingsBase, TapDbSettingsBaseConfig, TapDbSettingsGetPageRequest, TapDbSettingsGetPageResponse>
    {
        public TapDbSettingsGetPageBaseAction(TapDbSettingsBase owner) : base(owner)
        {
        }
    }
}
