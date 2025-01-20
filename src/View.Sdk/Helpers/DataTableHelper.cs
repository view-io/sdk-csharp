namespace View.Sdk.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Data table helper.
    /// </summary>
    public static class DataTableHelper
    {
        /// <summary>
        /// Retrieve string value.
        /// </summary>
        /// <param name="row">DataRow.</param>
        /// <param name="columnName"></param>
        /// <returns>String.</returns>
        public static string GetStringValue(DataRow row, string columnName)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));
            if (String.IsNullOrEmpty(columnName)) throw new ArgumentNullException(nameof(columnName));
            return row[columnName] != null ? row[columnName].ToString() : null;
        }

        /// <summary>
        /// Retrieve integer value.  Returns 0 if the cell is null.
        /// </summary>
        /// <param name="row">DataRow.</param>
        /// <param name="columnName"></param>
        /// <returns>Integer.</returns>
        public static int GetInt32Value(DataRow row, string columnName)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));
            if (String.IsNullOrEmpty(columnName)) throw new ArgumentNullException(nameof(columnName));
            return row[columnName] != null ? Convert.ToInt32(row[columnName]) : 0;
        }

        /// <summary>
        /// Retrieve nullable integer value.
        /// </summary>
        /// <param name="row">DataRow.</param>
        /// <param name="columnName"></param>
        /// <returns>Integer.</returns>
        public static int? GetNullableInt32Value(DataRow row, string columnName)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));
            if (String.IsNullOrEmpty(columnName)) throw new ArgumentNullException(nameof(columnName));
            return row[columnName] != null ? Convert.ToInt32(row[columnName]) : null;
        }

        /// <summary>
        /// Retrieve GUID value.  Returns the default GUID if the cell is null.
        /// </summary>
        /// <param name="row">DataRow.</param>
        /// <param name="columnName"></param>
        /// <returns>GUID.</returns>
        public static Guid GetGuidValue(DataRow row, string columnName)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));
            if (String.IsNullOrEmpty(columnName)) throw new ArgumentNullException(nameof(columnName));
            return row[columnName] != null ? Guid.Parse(row[columnName].ToString()) : default(Guid);
        }

        /// <summary>
        /// Retrieve nullable GUID value.
        /// </summary>
        /// <param name="row">DataRow.</param>
        /// <param name="columnName"></param>
        /// <returns>GUID.</returns>
        public static Guid? GetNullableGuidValue(DataRow row, string columnName)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));
            if (String.IsNullOrEmpty(columnName)) throw new ArgumentNullException(nameof(columnName));
            return row[columnName] != null ? Guid.Parse(row[columnName].ToString()) : null;
        }

        /// <summary>
        /// Retrieve enum value.
        /// </summary>
        /// <param name="row">DataRow.</param>
        /// <param name="columnName"></param>
        /// <returns>Enum.</returns>
        public static T GetEnumValue<T>(DataRow row, string columnName) where T : Enum
        {
            if (row == null) throw new ArgumentNullException(nameof(row));
            if (String.IsNullOrEmpty(columnName)) throw new ArgumentNullException(nameof(columnName));
            return (T)(Enum.Parse(typeof(T), GetStringValue(row, columnName)));
        }

        /// <summary>
        /// Retrieve DateTime value.  Returns the default DateTime if the cell is null.
        /// </summary>
        /// <param name="row">DataRow.</param>
        /// <param name="columnName"></param>
        /// <returns>DateTime.</returns>
        public static DateTime GetDateTimeValue(DataRow row, string columnName)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));
            if (String.IsNullOrEmpty(columnName)) throw new ArgumentNullException(nameof(columnName));
            return row[columnName] != null ? DateTime.Parse(row[columnName].ToString()) : default(DateTime);
        }

        /// <summary>
        /// Retrieve nullable DateTime value.
        /// </summary>
        /// <param name="row">DataRow.</param>
        /// <param name="columnName"></param>
        /// <returns>Nullable DateTime.</returns>
        public static DateTime? GetNullableDateTimeValue(DataRow row, string columnName)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));
            if (String.IsNullOrEmpty(columnName)) throw new ArgumentNullException(nameof(columnName));
            return row[columnName] != null ? DateTime.Parse(row[columnName].ToString()) : null;
        }

        /// <summary>
        /// Retrieve decimal value.  Returns 0 if the cell is null.
        /// </summary>
        /// <param name="row">DataRow.</param>
        /// <param name="columnName"></param>
        /// <returns>Decimal.</returns>
        public static decimal GetDecimalValue(DataRow row, string columnName)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));
            if (String.IsNullOrEmpty(columnName)) throw new ArgumentNullException(nameof(columnName));
            return row[columnName] != null ? Convert.ToDecimal(row[columnName].ToString()) : 0m;
        }

        /// <summary>
        /// Retrieve nullable decimal value.
        /// </summary>
        /// <param name="row">DataRow.</param>
        /// <param name="columnName"></param>
        /// <returns>Nullable decimal.</returns>
        public static decimal? GetNullableDecimalValue(DataRow row, string columnName)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));
            if (String.IsNullOrEmpty(columnName)) throw new ArgumentNullException(nameof(columnName));
            return row[columnName] != null ? Convert.ToDecimal(row[columnName].ToString()) : null;
        }

    }
}
