using System;

namespace UnitTest.Common.Patterns.Structural.Composite.Mocks
{

    public class AppConfig
        : ConfigSection
    {
        public enum AppSettings
        {
            SensorID,
            SensorTag,
            SensorSensitivity,
            AppSectionA,
            AppSectionB,
        }

        public AppConfig()
        {
            // build default configuration:
            this.SensorID = new AppValueA(1);
            this.SensorTag = new AppValue(AppSettings.SensorTag, "Motion sensor");
            this.SensorSensitivity = new ConfigValue<double>(AppSettings.SensorSensitivity, 99.9);
        }

        public AppValueA SensorID 
        {
            get {return this[AppSettings.SensorID] as AppValueA; }
            set { this[AppSettings.SensorID] = value;}
        }
        public AppValue SensorTag 
        {
            get {return this[AppSettings.SensorTag] as AppValue; }
            set { this[AppSettings.SensorTag] = value;}
        }
        public ConfigValue<double> SensorSensitivity 
        {
            get {return this[AppSettings.SensorSensitivity] as ConfigValue<double>; }
            set { this[AppSettings.SensorSensitivity] = value;}
        }

        public AppSectionA SectionA => this[AppSettings.AppSectionA] as AppSectionA;
        public AppSectionB SectionB => this[AppSettings.AppSectionB] as AppSectionB;
    }

    public class AppValueA
        :ConfigValue<int>
    {
        public AppValueA(int value) 
            : base(AppConfig.AppSettings.SensorID,value)
        {
        }
    }

    public class AppValue
        : ConfigValue<string>
    {
        public AppValue(Enum valuekey, string value) 
            : base(valuekey,value)
        {
        }
    }

    public class AppSectionA
        : ConfigSection
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

    public class AppSectionB
        : ConfigSection
    {
        public AppSectionB() 
            : base(AppConfig.AppSettings.AppSectionB)
        {
        }
    }
}
