using System.Collections.Generic;
using Rowatch2.Enums;

namespace Rowatch2.Embeding
{
    public class Chart
    {
        #region Constructor

        public Chart(ChartType type, string name)
        {
            _type = type;
            _name = name;

            _values = new List<int>();
        }

        #endregion

        #region Methods

        public void AddValue(int value)
        {
            if (value > _maxValue)
            {
                _maxValue = value;
            }

            _values.Add(value);
        }

        public void Clear()
        {
            _maxValue = 0;
            _values.Clear();
        }

        #endregion

        #region Properties

        private ChartType _type;
        public ChartType Type
        {
            get { return _type; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
        }

        private List<int> _values;
        public List<int> Values
        {
            get { return _values; }
            set { _values = value; }
        }

        private int _maxValue;
        public int MaxValue
        {
            get { return _maxValue; }
        }

        #endregion

        #region Overwrites

        public override string ToString()
        {
            return _name;
        }

        #endregion
    }
}