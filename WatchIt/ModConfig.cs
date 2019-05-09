﻿namespace WatchIt
{
    [ConfigurationPath("WatchItConfig.xml")]
    public class ModConfig
    {
        public bool ConfigUpdated { get; set; }
        public bool ShowOnOffButton { get; set; } = true;
        public bool ShowDragIcon { get; set; } = true;
        public float OnOffButtonPositionX { get; set; }
        public float OnOffButtonPositionY { get; set; }
        public bool Visible { get; set; } = true;
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public bool VerticalLayout { get; set; } = true;
        public bool DoubleRibbonLayout { get; set; } = true;
        public float RefreshInterval { get; set; } = 5.0f;
        public int ShowNumericalDigits { get; set; } = 1;
        public int NumericalDigitsAnchor { get; set; } = 1;
        public float Opacity { get; set; } = 1.0f;
        public float OpacityWhenHover { get; set; } = 1.0f;
        public bool ElectricityAvailability { get; set; } = true;
        public bool WaterAvailability { get; set; } = true;
        public bool SewageAvailability { get; set; } = true;
        public bool GarbageAvailability { get; set; } = true;
        public bool ElementarySchoolAvailability { get; set; } = true;
        public bool HighSchoolAvailability { get; set; } = true;
        public bool UniversityAvailability { get; set; } = true;
        public bool HealthcareAvailability { get; set; } = true;
        public bool CrematoriumAvailability { get; set; } = true;
        public bool FireDepartmentAvailability { get; set; } = true;
        public bool PoliceDepartmentAvailability { get; set; } = true;
        public bool JailAvailability { get; set; } = true;
        public bool HeatingAvailability { get; set; } = true;
        public bool LandfillUsage { get; set; } = true;
        public bool CemeteryUsage { get; set; } = true;
        public bool TrafficFlow { get; set; } = true;
        public bool GroundPollution { get; set; } = true;
        public bool DrinkingWaterPollution { get; set; } = true;
        public bool NoisePollution { get; set; } = true;
        public bool FireHazard { get; set; } = true;
        public bool CrimeRate { get; set; } = true;
        public bool UnemploymentRate { get; set; } = true;
        public bool HealthAverage { get; set; } = true;
        public bool CityAttractiveness { get; set; } = true;
        public bool Happiness { get; set; } = true;
        public bool ShowGameLimitsButton { get; set; } = true;
        public bool ShowCityStatisticsButton { get; set; } = true;

        private static ModConfig instance;

        public static ModConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Configuration<ModConfig>.Load();
                }

                return instance;
            }
        }

        public void Save()
        {
            Configuration<ModConfig>.Save();
            ConfigUpdated = true;
        }
    }
}