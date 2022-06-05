using System;
using System.Globalization;
using System.Windows.Data;
using System.ComponentModel;

namespace Astrostrider.Classes
{
    public class SpaceShip : BaseSpaceClass
    {
        #region "Fields"

        private int _speed;
        private SpaceShipType _type;
        private bool _isCreatedByUser;
        private string _imageSource;

        #endregion

        #region "Properties"

        public int Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;
                OnPropertyChanged("Speed");
            }
        }
        public SpaceShipType Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged("Type");
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

        public SpaceShip() { }
        public SpaceShip(string name) : base(name) { }
        public SpaceShip(string name, int speed) : base(name)
        {
            Speed = speed;
        }

        #endregion

        #region "Methods"

        public override string ToString()
        {
            string expression = string.Empty;
            expression += Name != null ? $"{Name}\n" : "";
            expression += Speed != 0 ? $"Скорость: {Speed}м/с\n" : "";
            expression += Description != null ? $"{Description}\n" : "";
            return expression;
        }

        public static SpaceShipType GetSpaceShipTypeTypeByString(string type)
        {
            switch (type)
            {
                case "Orbital":
                    return SpaceShipType.Orbital;
                case "Lunar":
                    return SpaceShipType.Lunar;
                case "CrossPlanet":
                    return SpaceShipType.CrossPlanet;
                default:
                    return SpaceShipType.Unknown;
            }
        }

        public static SpaceShipType GetSpaceShipTypeByDescription(string description)
        {
            switch (description)
            {
                case "Орбитальный Космический Корабль":
                    return SpaceShipType.Orbital;
                case "Лунный Космический Корабль":
                    return SpaceShipType.Lunar;
                case "Межпланетный Космический Корабль":
                    return SpaceShipType.CrossPlanet;
                default:
                    return SpaceShipType.Unknown;
            }
        }

        #endregion
    }

    public enum SpaceShipType
    {
        [Description("Орбитальный Космический Корабль")]
        Orbital = 1,
        [Description("Лунный Космический Корабль")]
        Lunar = 2,
        [Description("Межпланетный Космический Корабль")]
        CrossPlanet = 3,
        [Description("Неизвестный Тип")]
        Unknown = 4
    }

    public class SpaceShipGroupKeyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SpaceShipType type = (SpaceShipType)value;
            return type == 0 ? SpaceShipType.Unknown.ToDescription() : type.ToDescription();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
