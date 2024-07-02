namespace View.Sdk
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A node of data from a parsed document.
    /// </summary>
    public class DataNode
    {
        #region Public-Members

        /// <summary>
        /// The key.
        /// </summary>
        [JsonPropertyOrder(1)]
        public string Key { get; set; } = null;

        /// <summary>
        /// The DataType associated with the key-value pair.
        /// </summary>
        [JsonPropertyOrder(2)]
        public DataTypeEnum Type { get; set; } = DataTypeEnum.String;

        /// <summary>
        /// The data associated with the key.
        /// </summary>
        [JsonPropertyOrder(3)]
        public object Data { get; set; } = null;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiates the DataNode.
        /// </summary>
        public DataNode()
        {

        }

        /// <summary>
        /// Instantiates the DataNode.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="data">The data associated with the key.</param>
        /// <param name="type">The DataType associated with the key-value pair.</param>
        public DataNode(string key, object data, DataTypeEnum type)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            Key = key;
            Data = data;
            Type = type;
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Retrieve the DataType of the supplied value.
        /// </summary>
        /// <param name="val">The object to evaluate.</param>
        /// <returns>DataType.</returns>
        public static DataTypeEnum TypeFromValue(object val)
        {
            if (val == null) return DataTypeEnum.Null;

            decimal testDecimal;
            int testInt;
            long testLong;
            bool testBool;

            if (val.ToString().Contains("."))
            {
                if (decimal.TryParse(val.ToString(), out testDecimal))
                {
                    return DataTypeEnum.Decimal;
                }
            }

            if (int.TryParse(val.ToString(), out testInt))
            {
                return DataTypeEnum.Integer;
            }

            if (long.TryParse(val.ToString(), out testLong))
            {
                return DataTypeEnum.Long;
            }

            if (bool.TryParse(val.ToString(), out testBool))
            {
                return DataTypeEnum.Boolean;
            }

            return DataTypeEnum.String;
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
