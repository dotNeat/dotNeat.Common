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

    public class AppValue
        : ConfigValue<Enum,string>
    {
        public AppValue(Enum valuekey, string value) 
            : base(valuekey,value)
        {
        }
    }

    public class AppSectionA
        : ConfigSection<AppConfig.AppSettings, AppSectionA.SectionKeys>
    {
        public enum SectionKeys
        {
            AppValue1,
            AppValue2,
        }
        public AppSectionA() 
            : base(AppConfig.AppSettings.AppSectionA)
        {
        }

    }
}
