namespace UnitTest.dotNeat.Common.Patterns.GoF.Structural.Composite.Mocks
{
    using System;

    public class AppConfig
        : ConfigSection
    {
        public enum AppConfigRoot
        {
            Sensor,
        }

        public enum Sensor
        {
            ID,
            Tag,
            Sensitivity,
            Dimensions,
            AppSectionB,
        }

        public AppConfig() 
            : base(AppConfigRoot.Sensor)
        {
            // build default configuration:
            this.SensorID = new(1);
            this.SensorTag = new(Sensor.Tag, "Motion sensor");
            this.SensorSensitivity = new(Sensor.Sensitivity, 99.9);
            this.SensorDimentions = new() { Height = 0.01, Length = 0.02, Width = 0.03};
        }

        public SensorIdentity SensorID 
        {
            get => (SensorIdentity)this[Sensor.ID];
            set => this[Sensor.ID] = value;
        }
        public AppValue SensorTag 
        {
            get => (AppValue)this[Sensor.Tag];
            set => this[Sensor.Tag] = value;
        }
        public ConfigValue<double> SensorSensitivity 
        {
            get => (ConfigValue<double>)this[Sensor.Sensitivity];
            set => this[Sensor.Sensitivity] = value;
        }

        public Dimensions SensorDimentions //=> this[SensorAttributes.SensorDimensions] as Dimensions;
        {
            get => (Dimensions)this[Sensor.Dimensions];
            set => this[Sensor.Dimensions] = value;
        }
        public AppSectionB SectionB => (AppSectionB)this[Sensor.AppSectionB];
    }

    public class SensorIdentity
        :ConfigValue<int>
    {
        public SensorIdentity(int value) 
            : base(AppConfig.Sensor.ID,value)
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

    public class Dimensions
        : ConfigSection
    {
        public enum Dimension
        {
            Length,
            Width,
            Height,
        }

        public Dimensions()
            : this(0.0, 0.0, 0.0)
        {
        }

        public Dimensions(double height, double length, double width) 
            : base(AppConfig.Sensor.Dimensions)
        {
            this.Add(new ConfigValue<double>(Dimension.Height, height));
            this.Add(new ConfigValue<double>(Dimension.Length, length));
            this.Add(new ConfigValue<double>(Dimension.Width, width));
        }

        public double Height
        {
            get => ((ConfigValue<double>) this[Dimension.Height]).Value;
            set => ((ConfigValue<double>) this[Dimension.Height]).Value = value;
        }

        public double Length
        {
            get => ((ConfigValue<double>) this[Dimension.Length]).Value;
            set => ((ConfigValue<double>) this[Dimension.Length]).Value = value;
        }

        public double Width
        {
            get => ((ConfigValue<double>) this[Dimension.Width]).Value;
            set => ((ConfigValue<double>) this[Dimension.Width]).Value = value;
        }
    }

    public class AppSectionB
        : ConfigSection
    {
        public AppSectionB() 
            : base(AppConfig.Sensor.AppSectionB)
        {
        }
    }
}
