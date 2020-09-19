using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest.Common.Patterns.Structural.Composite.Mocks
{

    public class AppConfig
        :ConfigSection<AppConfig.AppConfigID, AppConfig.AppSettings>
    {
        public enum AppConfigID
        {
            AppConfigRoot,
        }

        public enum AppSettings
        {
            AppValueA,
            AppValueB,
            AppSectionA,
            AppSectionB,
        }

        public AppConfig() 
            : base(AppConfigID.AppConfigRoot)
        {
        }
    }

    public class AppValueA
        :ConfigValue<AppConfig.AppSettings,int>
    {
        public AppValueA(int value) 
            : base(AppConfig.AppSettings.AppValueA,value)
        {
        }
    }

    public class AppValueB
        :ConfigValue<AppConfig.AppSettings,string>
    {
        public AppValueB(string value) 
            : base(AppConfig.AppSettings.AppValueB,value)
        {
        }
    }
}
