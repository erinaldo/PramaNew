using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.ComponentModel;

namespace Prama
{
    public class NumericGridCell : DataGridViewTextBoxCell
    {
        public NumericGridCell() : base() { }

        private NumberFormatInfo NumberFormat
        {
            get
            {
                return ((NumericGridColumn)OwningColumn).NumberFormat;
            }
        }

        public override Type ValueType
        {
            get { return typeof(decimal?); }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                object defaultValue = base.DefaultNewRowValue;
                if (defaultValue is decimal)
                    return defaultValue;
                else
                    return null;
            }
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            Control ctl = DataGridView.EditingControl;
            ctl.KeyPress += new KeyPressEventHandler(NumericCell_KeyPress);
        }

        private void NumericCell_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataGridViewCell currentCell = ((IDataGridViewEditingControl)sender).EditingControlDataGridView.CurrentCell;
            if (!(currentCell is NumericGridCell)) return;

            DataGridViewTextBoxEditingControl ctl = (DataGridViewTextBoxEditingControl)sender;
            if (char.IsDigit(e.KeyChar))
            {
                int separatorPosition = ctl.Text.IndexOf(NumberFormat.NumberDecimalSeparator);
                if (separatorPosition >= 0 && ctl.SelectionStart > separatorPosition)
                {
                    int currentDecimals = ctl.Text.Length - separatorPosition
                        - NumberFormat.NumberDecimalSeparator.Length - ctl.SelectionLength;
                    if (currentDecimals >= NumberFormat.NumberDecimalDigits)
                        e.Handled = true;
                }
            }
            else if (e.KeyChar.ToString().Equals(NumberFormat.NumberDecimalSeparator))
            {
                e.Handled = ctl.Text.IndexOf(NumberFormat.NumberDecimalSeparator) >= 0;
            }
            else
            {
                e.Handled = !char.IsControl(e.KeyChar);
            }
        }

        public override DataGridViewCellStyle GetInheritedStyle(DataGridViewCellStyle inheritedCellStyle, int rowIndex, bool includeColors)
        {
            DataGridViewCellStyle style = base.GetInheritedStyle(inheritedCellStyle, rowIndex, includeColors);
            style.FormatProvider = NumberFormat;
            return style;
        }

        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            try
            {

                if (value == null)
                    return null;
                else
                   value = Convert.ToDecimal(value.ToString());
                   return ((decimal)value).ToString(cellStyle.FormatProvider);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace, "Error!",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }

    } 
}
