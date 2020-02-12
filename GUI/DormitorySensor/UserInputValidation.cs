using System.Globalization;
using System.Windows.Controls;

namespace DormitorySensor.UserInputValidation
{
    public class SensorNameValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var name = value.ToString();
            if (string.IsNullOrWhiteSpace(name))
            {
                return new ValidationResult(false, "Sensor name cannot be empty!");
            }
            else if (name.Length < 4)
            {
                return new ValidationResult(false, "Sensor name must be minimum 4 characters!");
            }
            else if (name.Length > 32)
            {
                return new ValidationResult(false, "Sensor name must be maximum 32 characters!");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class SensorLatitudeValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var stringValue = value.ToString();
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return new ValidationResult(false, "Sensor latitude cannot be empty!");
            }

            double latitude;
            if (double.TryParse(stringValue, out latitude))
            {
                if (latitude > 90)
                {
                    return new ValidationResult(false, "Latitude must be maximum +90.00 degrees!");
                }
                else if (latitude < -90)
                {
                    return new ValidationResult(false, "Latitude must be minimum -90.00 degrees!");
                }
            }
            else
            {
                return new ValidationResult(false, "Latitude must a number");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class SensorLongtitudeValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var stringValue = value.ToString();
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return new ValidationResult(false, "Sensor longtitude cannot be empty!");
            }

            double longtitude;
            if (double.TryParse(stringValue, out longtitude))
            {
                if (longtitude > 180)
                {
                    return new ValidationResult(false, "Longtitude must be maximum +180.00 degrees!");
                }
                else if (longtitude < -180)
                {
                    return new ValidationResult(false, "Longtitude must be minimum -180.00 degrees!");
                }
            }
            else
            {
                return new ValidationResult(false, "Longtitude must a number");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class SensorMinValueValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var stringValue = value.ToString();
            if (string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(false, "Sensor minimum value cannot be empty!");
            }

            double minValue;
            if (double.TryParse(value.ToString(), out minValue))
            {
                if (minValue > 4500)
                {
                    return new ValidationResult(false, "Minimum value must be maximum +4500.00!");
                }
                else if (minValue < -50)
                {
                    return new ValidationResult(false, "Minimum value must be minimum -50.00!");
                }
            }
            else
            {
                return new ValidationResult(false, "Minimum value must an integer");
            }
            return ValidationResult.ValidResult;
        }
    }
    public class SensorMaxValueValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var stringValue = value.ToString();
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return new ValidationResult(false, "Sensor maximum value cannot be empty!");
            }

            int maxValue;
            if (int.TryParse(stringValue, out maxValue))
            {
                if (maxValue > 4500)
                {
                    return new ValidationResult(false, "Maximum value must be maximum +4500.00!");
                }
                else if (maxValue < -50)
                {
                    return new ValidationResult(false, "Maximum value must be minimum -50.00!");
                }
            }
            else
            {
                return new ValidationResult(false, "Maximum value must an integer");
            }
            return ValidationResult.ValidResult;
        }
    }
    public class SensorDescriptionValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var description = value.ToString();
            if (string.IsNullOrWhiteSpace(description))
            {
                return new ValidationResult(false, "Sensor description cannot be empty!");
            }
            return ValidationResult.ValidResult;
        }
    }
}
