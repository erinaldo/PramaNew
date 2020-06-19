using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.ComponentModel;

namespace Prama
{
    public class NumericGridColumn : DataGridViewColumn
    {
        private NumberFormatInfo _numberFormat;
        private string _decimalSeparator;
        private int _decimalDigits = -1;

        public NumericGridColumn() : base(new NumericGridCell()) { }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null &&
                        !value.GetType().IsAssignableFrom(typeof(NumericGridCell)))
                    throw new InvalidCastException("Debe especificar una instancia de NumericGridCell");
                base.CellTemplate = value;
            }
        }

        [Category("Appearance")]
        public string DecimalSeparator
        {
            get
            {
                if (string.IsNullOrEmpty(_decimalSeparator))
                    return CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
                else
                    return _decimalSeparator;
            }
            set
            {
                if (value.Length != 1)
                    throw new ArgumentException("El separador decimal debe ser un único caracter");
                _decimalSeparator = value;
            }
        }

        [Category("Appearance")]
        public int DecimalDigits
        {
            get
            {
                if (_decimalDigits < 0)
                    return CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalDigits;
                else
                    return _decimalDigits;
            }
            set { _decimalDigits = value; }
        }

        internal NumberFormatInfo NumberFormat
        {
            get
            {
                if (_numberFormat == null)
                    _numberFormat = new NumberFormatInfo();
                _numberFormat.NumberDecimalSeparator = DecimalSeparator;
                _numberFormat.NumberDecimalDigits = DecimalDigits;
                return _numberFormat;
            }
        }

        public override object Clone()
        {
            NumericGridColumn newColumn = (NumericGridColumn)base.Clone();
            newColumn.DecimalSeparator = DecimalSeparator;
            newColumn.DecimalDigits = DecimalDigits;
            return newColumn;
        }

    } 
}
