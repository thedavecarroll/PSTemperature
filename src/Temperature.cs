namespace PSTemperature
{
    public enum TemperatureUnit
    {
        F = 0,  // Fahrenheit
        C = 1,  // Celsius
        K = 2,  // Kelvin
        R = 3   // Rankine
    }

    public class Temperature
    {
        internal static readonly string degreeSymbol = "°";

        private decimal _value;

        public decimal Value
        {
            get { return _value; }
            set
            {
                _value = GetTemperatureValue(value, Unit);
                Comment = GetStateComment();
            }
        }

        public TemperatureUnit Unit { get; set; }

        public string Comment { get; set; }

        public Temperature() { }

        public Temperature(decimal value, TemperatureUnit unit)
        {
            _value = GetTemperatureValue(value, unit);
            Unit = unit;
            Comment = GetStateComment();
        }

        public override string ToString()
        {
            string toStringOutput = (Unit == TemperatureUnit.F || Unit == TemperatureUnit.C) ?
                    $"{Value} {degreeSymbol}{Unit}" :
                    $"{Value} {Unit}";
            return toStringOutput;
        }

        public decimal ConvertTo(TemperatureUnit unit)
        {
            switch (Unit)
            {
                case TemperatureUnit.C:
                    switch (unit)
                    {
                        case TemperatureUnit.C:
                            return Value;
                        case TemperatureUnit.F:
                            return (Value * 9 / 5) + 32;
                        case TemperatureUnit.K:
                            return Value + 273.15M;
                        case TemperatureUnit.R:
                            return (Value + 273.15M) * 9 / 5;
                    }
                    break;
                case TemperatureUnit.F:
                    switch (unit)
                    {
                        case TemperatureUnit.C:
                            return (Value - 32) * 5 / 9;
                        case TemperatureUnit.F:
                            return Value;
                        case TemperatureUnit.K:
                            return (Value - 32) * 5 / 9 + 273.15M;
                        case TemperatureUnit.R:
                            return Value + 459.67M;
                    }
                    break;
                case TemperatureUnit.K:
                    switch (unit)
                    {
                        case TemperatureUnit.C:
                            return Value - 273.15M;
                        case TemperatureUnit.F:
                            return (Value - 273.15M) * 9 / 5 + 32;
                        case TemperatureUnit.K:
                            return Value;
                        case TemperatureUnit.R:
                            return Value * 9 / 5;
                    }
                    break;
                case TemperatureUnit.R:
                    switch (unit)
                    {
                        case TemperatureUnit.C:
                            return (Value - 491.67M) * 5 / 9;
                        case TemperatureUnit.F:
                            return Value - 459.67M;
                        case TemperatureUnit.K:
                            return Value * 5 / 9;
                        case TemperatureUnit.R:
                            return Value;
                    }
                    break;
            }
            return 0;
        }

        public decimal ConvertTo(TemperatureUnit unit, int Precision)
        {
            return unit switch
            {
                TemperatureUnit.C => decimal.Round(ConvertTo(unit), Precision),
                TemperatureUnit.F => decimal.Round(ConvertTo(unit), Precision),
                TemperatureUnit.K => decimal.Round(ConvertTo(unit), Precision),
                TemperatureUnit.R => decimal.Round(ConvertTo(unit), Precision),
                _ => 0,
            };
        }

        public string ConvertToString(TemperatureUnit unit)
        {
            string toStringOutput = null;
            switch (unit)
            {
                case TemperatureUnit.C:
                case TemperatureUnit.F:
                case TemperatureUnit.R:
                    toStringOutput = $"{ConvertTo(unit)} {degreeSymbol}{unit}";
                    break;
                case TemperatureUnit.K:
                    toStringOutput = $"{ConvertTo(unit)} {unit}";
                    break;
            }
            return toStringOutput;
        }

        public string ConvertToString(TemperatureUnit unit, int precision)
        {
            string convertToStringOutput = null;
            switch (unit)
            {
                case TemperatureUnit.C:
                case TemperatureUnit.F:
                case TemperatureUnit.R:
                    convertToStringOutput = $"{decimal.Round(ConvertTo(unit), precision)} {degreeSymbol}{unit}";
                    break;
                case TemperatureUnit.K:
                    convertToStringOutput = $"{decimal.Round(ConvertTo(unit), precision)} {unit}";
                    break;
            }
            return convertToStringOutput;
        }

        public static bool IsBelowAbsoluteZero(decimal value, TemperatureUnit unit)
        {
            return unit switch
            {
                TemperatureUnit.F => value < -459.67M,
                TemperatureUnit.C => value < -273.15M,
                TemperatureUnit.K => value < 0M,
                TemperatureUnit.R => value < 0M,
                _ => false,
            };
        }

        private static decimal GetTemperatureValue(decimal value, TemperatureUnit unit)
        {
            if (IsBelowAbsoluteZero(value, unit))
            {
                string InvalidTemperature = (unit == TemperatureUnit.F || unit == TemperatureUnit.C || unit == TemperatureUnit.R)
                    ? $"{value} {degreeSymbol}{unit} is below absolute zero. Please try again."
                    : $"{value} {unit} is below absolute zero. Please try again.";
                throw new System.ArgumentException(InvalidTemperature);
            }
            return value;
        }

        private string GetStateComment()
        {
            decimal celsius = ConvertTo(TemperatureUnit.C);
            return celsius switch
            {
                -273.15M => "Absolute zero",
                -195.8M => "Boiling point of liquid nitrogen",
                -78 => "Sublimation point of dry ice",
                -40 => "Intersection of Celsius and Fahrenheit scales",
                0 => "Freezing point",
                20 => "Room temperature (NIST standard)",
                37 => "Normal human body temperature (average)",
                100 => "Boiling point",
                233 => "Fahrenheit 451 - the temperature at which book paper catches fire and burns",
                5505 => "Surface of the Earth's sun",
                _ => null,
            };
        }
    }
}
