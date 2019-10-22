using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.Interfaces;
using Models.ViewModels.Config;
using static Models.Constants.GlobalConfigs;

namespace Logic
{
    public class ConfigLogic : IConfigLogic
    {
        public async Task<GlobalConfigViewModel> ResolveGlobalConfig()
        {
            var currentYear = DateTime.UtcNow.Year;

            var years = new HashSet<int> {2018, 2019, currentYear};

            var retVal = new GlobalConfigViewModel
            {
                Years = years,
                UpdatedYear = YearValue,
                EventFeature = EventFeature,
                EmailTestMode = EmailTestMode
            };
            
            return await Task.FromResult(retVal);
        }

        public Task SetGlobalConfig(GlobalConfigViewModel globalConfigViewModel)
        {
            UpdateGlobalConfigs(globalConfigViewModel);

            return Task.CompletedTask;
        }
    }
}