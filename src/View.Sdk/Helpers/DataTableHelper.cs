namespace View.Sdk.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Dynamic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Data table helper.
    /// </summary>
    public static class DataTableHelper
    {
        /// <summary>
        /// Retrieve Boolean value.
        /// </summary>
        /// <param name="row">DataRow.</param>
        /// <param name="columnName"></param>
        /// <returns>Boolean value.</returns>
        public static bool GetBooleanValue(DataRow row, string columnName)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));
            if (String.IsNullOrEmpty(columnName)) throw new ArgumentNullException(nameof(columnName));

            object value = row[columnName];
            if (value == null || value == DBNull.Value) return false; 
            if (value is int intValue) return intValue >= 0;
            if (value is string stringValue)
            {
                if (Boolean.TryParse(stringValue, out bool boolResult)) return boolResult;
                if (Int32.TryParse(stringValue, out int intResult)) return intResult >= 0;
            }

            return false;
        }
        /// <summary>
        /// Retrieve nullable Boolean value.
        /// </summary>
        /// <param name="row">DataRow.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>Boolean value if valid, null if unknown or invalid.</returns>
        public static bool? GetNullableBooleanValue(DataRow row, string columnName)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));
            if (String.IsNullOrEmpty(columnName)) throw new ArgumentNullException(nameof(columnName));
            if (!row.Table.Columns.Contains(columnName)) return null;

            try
            {
                object value = row[columnName];
                if (value == null || value == DBNull.Value) return null;
                if (value is int intValue) return intValue >= 0;
                if (value is string stringValue)
                {
                    if (string.IsNullOrWhiteSpace(stringValue)) return null;
                    if (Boolean.TryParse(stringValue, out bool boolResult)) return boolResult;
                    if (Int32.TryParse(stringValue, out int intResult)) return intResult >= 0;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

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
        /// Retrieve long value.  Returns 0 if the cell is null.
        /// </summary>
        /// <param name="row">DataRow.</param>
        /// <param name="columnName"></param>
        /// <returns>Long.</returns>
        public static long GetInt64Value(DataRow row, string columnName)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));
            if (String.IsNullOrEmpty(columnName)) throw new ArgumentNullException(nameof(columnName));
            return row[columnName] != null ? Convert.ToInt64(row[columnName]) : 0;
        }

        /// <summary>
        /// Retrieve nullable integer value.
        /// </summary>
        /// <param name="row">DataRow.</param>
        /// <param name="columnName"></param>
        /// <returns>Integer.</returns>
        public static long? GetNullableInt64Value(DataRow row, string columnName)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));
            if (String.IsNullOrEmpty(columnName)) throw new ArgumentNullException(nameof(columnName));
            return row[columnName] != null ? Convert.ToInt64(row[columnName]) : null;
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

        /// <summary>
        /// Convert a data table to a dynamic list.
        /// </summary>
        /// <param name="dt">Data table.</param>
        /// <returns>List of dynamic.</returns>
        public static List<dynamic> DataTableToListDynamic(DataTable dt)
        {
            List<dynamic> ret = new List<dynamic>();
            if (dt == null || dt.Rows.Count < 1) return ret;

            foreach (DataRow curr in dt.Rows)
            {
                dynamic dyn = new ExpandoObject();
                foreach (DataColumn col in dt.Columns)
                {
                    var dic = (IDictionary<string, object>)dyn;
                    dic[col.ColumnName] = curr[col];
                }
                ret.Add(dyn);
            }

            return ret;
        }

        /// <summary>
        /// Convert a data table to a dynamic.
        /// </summary>
        /// <param name="dt">Data table.</param>
        /// <returns>Dynamic.</returns>
        public static dynamic DataTableToDynamic(DataTable dt)
        {
            dynamic ret = new ExpandoObject();
            if (dt == null || dt.Rows.Count < 1) return ret;

            foreach (DataRow curr in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    var dic = (IDictionary<string, object>)ret;
                    dic[col.ColumnName] = curr[col];
                }

                return ret;
            }

            return ret;
        }

        /// <summary>
        /// Convert a data table to a list of dictionaries.
        /// </summary>
        /// <param name="dt">Data table.</param>
        /// <returns>List of dictionaries.</returns>
        public static List<Dictionary<string, object>> DataTableToListDictionary(DataTable dt)
        {
            List<Dictionary<string, object>> ret = new List<Dictionary<string, object>>();
            if (dt == null || dt.Rows.Count < 1) return ret;

            foreach (DataRow curr in dt.Rows)
            {
                Dictionary<string, object> currDict = new Dictionary<string, object>();

                foreach (DataColumn col in dt.Columns)
                {
                    currDict.Add(col.ColumnName, curr[col]);
                }

                ret.Add(currDict);
            }

            return ret;
        }

        /// <summary>
        /// Convert a data table to a dictionary.
        /// </summary>
        /// <param name="dt">Data table.</param>
        /// <returns>Dictionary.</returns>
        public static Dictionary<string, object> DataTableToDictionary(DataTable dt)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>();
            if (dt == null || dt.Rows.Count < 1) return ret;

            foreach (DataRow curr in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    ret.Add(col.ColumnName, curr[col]);
                }

                return ret;
            }

            return ret;
        }

        /// <summary>
        /// Convert an object to a dictionary.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>Dictionary.</returns>
        public static Dictionary<string, object> ObjectToDictionary(object obj)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>();

            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                string propName = prop.Name;
                var val = obj.GetType().GetProperty(propName).GetValue(obj, null);
                if (val != null)
                {
                    ret.Add(propName, val);
                }
            }

            return ret;
        }

        /// <summary>
        /// Check if an object is a dictionary.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>True if dictionary.</returns>
        public static bool IsDictionary(object obj)
        {
            if (obj == null) return false;
            return obj is IDictionary &&
                   obj.GetType().IsGenericType &&
                   obj.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>));
        }
    }
}
