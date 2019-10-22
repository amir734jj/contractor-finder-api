using System;
using Models.ViewModels.Config;

namespace Models.Constants
{
    public static class GlobalConfigs
    {
        public static bool EventFeature { get; private set; }
        
        public static int YearValue { get; private set; } = DateTime.UtcNow.Year;

        public static bool EmailTestMode { get; private set; }

        public static void UpdateGlobalConfigs(GlobalConfigViewModel globalConfigViewModel)
        {
            YearValue = globalConfigViewModel.UpdatedYear;
            EventFeature = globalConfigViewModel.EventFeature;
            EmailTestMode = globalConfigViewModel.EmailTestMode;
        }

        public static object ToAnonymousObject()
        {
            return new
            {
                EventFeature,
                YearValue,
                EmailTestMode
            };
        }
    }
}