using System;

namespace UnitTest.Common.Patterns.Structural.Composite.Mocks
{

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
            this.SensorID = new SensorIdentity(1);
            this.SensorTag = new AppValue(Sensor.Tag, "Motion sensor");
            this.SensorSensitivity = new ConfigValue<double>(Sensor.Sensitivity, 99.9);
            this.SensorDimentions = new Dimensions() { Height = 0.01, Length = 0.02, Width = 0.03};
        }

        public SensorIdentity SensorID 
        {
            get {return this[Sensor.ID] as SensorIdentity; }
            set { this[Sensor.ID] = value;}
        }
        public AppValue SensorTag 
        {
            get {return this[Sensor.Tag] as AppValue; }
            set { this[Sensor.Tag] = value;}
        }
        public ConfigValue<double> SensorSensitivity 
        {
            get {return this[Sensor.Sensitivity] as ConfigValue<double>; }
            set { this[Sensor.Sensitivity] = value;}
        }

        public Dimensions SensorDimentions //=> this[SensorAttributes.SensorDimensions] as Dimensions;
        {
            get {return this[Sensor.Dimensions] as Dimensions; }
            set { this[Sensor.Dimensions] = value;}
        }
        public AppSectionB SectionB => this[Sensor.AppSectionB] as AppSectionB;
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
            get { return ((ConfigValue<double>) this[Dimension.Height]).Value; }
            set { ((ConfigValue<double>) this[Dimension.Height]).Value = value; }
        }

        public double Length
        {
            get { return ((ConfigValue<double>) this[Dimension.Length]).Value; }
            set { ((ConfigValue<double>) this[Dimension.Length]).Value = value; }
        }

        public double Width
        {
            get { return ((ConfigValue<double>) this[Dimension.Width]).Value; }
            set { ((ConfigValue<double>) this[Dimension.Width]).Value = value; }
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
