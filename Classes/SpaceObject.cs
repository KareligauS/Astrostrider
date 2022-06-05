using System;
using System.Globalization;
using System.Windows.Data;
using System.ComponentModel;

namespace Astrostrider.Classes
{
    public class SpaceObject : BaseSpaceClass
    {
        #region "Fields"

        private SpaceObjectType _type;
        private int _distance;
        private bool _isCreatedByUser;
        private string _imageSource;

        #endregion

        #region "Properties"

        public SpaceObjectType Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged("Type");
            }
        }
        public int Distance
        {
            get { return _distance; }
            set
            {
                _distance = value;
                OnPropertyChanged("Distance");
            }
        }
        public bool IsCreatedByUser
        {
            get { return _isCreatedByUser; }
            set
            {
                _isCreatedByUser = value;
                OnPropertyChanged("IsCreatedByUser");
            }
        }
        public string ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }

        #endregion

        #region "Constructors"

        public SpaceObject() { }
        public SpaceObject(string name) : base(name) { }
        public SpaceObject(string name, int distance, SpaceObjectType type) : base(name)
        {
            Distance = distance;
            Type = type;
        }

        #endregion

        #region "Methods"

        public static SpaceObjectType GetSpaceObjectTypeByString(string type)
        {
            switch (type)
            {
                case "Star":
                    return SpaceObjectType.Star;
                case "Planet":
                    return SpaceObjectType.Planet;
                case "Satellite":
                    return SpaceObjectType.Satellite;
                default:
                    return SpaceObjectType.Unknown;
            }
        }

        public static SpaceObjectType GetSpaceObjectTypeByDescription(string description)
        {
            switch (description)
            {
                case "Звезда":
                    return SpaceObjectType.Star;
                case "Планета":
                    return SpaceObjectType.Planet;
                case "Спутник":
                    return SpaceObjectType.Satellite;
                default:
                    return SpaceObjectType.Unknown;
            }
        }

        public override string ToString()
        {
            string expression = string.Empty;
            expression += Name != null ? $"{Name}\n" : "";
            expression += Type != 0 ? $"Тип: {Type}\n" : "Тип: Неизвестен\n";
            expression += Distance != 0 ? $"Расстояние до объекта от Земли: {Distance}м.\n" : "";
            expression += Description != null ? $"{Description}\n" : "";
            return expression;
        }

        #endregion
    }

    public enum SpaceObjectType
    {
        [Description("Звезда")]
        Star = 1,
        [Description("Планета")]
        Planet = 2,
        [Description("Спутник")]
        Satellite = 3,
        [Description("Неизвестный Тип")]
        Unknown = 4
    }

    public class SpaceObjectGroupKeyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (SpaceObjectType)value == 0 ? SpaceObjectType.Unknown.ToDescription() : EnumExtensions.ToDescription((Enum)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

