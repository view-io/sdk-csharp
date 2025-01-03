namespace View.Sdk
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A search filter.
    /// </summary>
    public class SchemaFilter
    {
        #region Public-Members

        /// <summary>
        /// The schema element upon which to evaluate.
        /// </summary>
        [JsonPropertyOrder(1)]
        public string Property
        {
            get
            {
                return _Property;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(Property));
                _Property = value;
            }
        }

        /// <summary>
        /// The condition by which the schema element, or its value, is evaluated.
        /// </summary>
        [JsonPropertyOrder(2)]
        public SchemaCondition Condition { get; set; } = SchemaCondition.Equals;

        /// <summary>
        /// The value to be evaluated using the specified condition against the specified schema element.
        /// When using GreaterThan, GreaterThanOrEqualTo, LessThan, or LessThanOrEqualTo, the value supplied must be convertible to decimal.
        /// </summary>
        [JsonPropertyOrder(3)]
        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (Condition == SchemaCondition.GreaterThan
                    || Condition == SchemaCondition.GreaterThanOrEqualTo
                    || Condition == SchemaCondition.LessThan
                    || Condition == SchemaCondition.LessThanOrEqualTo)
                {
                    if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));

                    decimal testDecimal = 0m;
                    if (!decimal.TryParse(value, out testDecimal))
                    {
                        throw new ArgumentException("Value must be convertible to decimal when using GreaterThan, GreaterThanOrEqualTo, LessThan, or LessThanOrEqualTo.");
                    }
                }

                _Value = value;
            }
        }

        #endregion

        #region Private-Members

        private string _Property = "";
        private string _Value = "";

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public SchemaFilter()
        {

        }

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="field">Field.</param>
        /// <param name="condition">Condition.</param>
        /// <param name="value">Value.</param>
        public SchemaFilter(string field, SchemaCondition condition, string value)
        {
            if (String.IsNullOrEmpty(field)) throw new ArgumentNullException(nameof(field));

            Property = field;
            Condition = condition;
            Value = value;
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Evaluates a value against the search filter.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <returns>True if matched.</returns>
        public bool EvaluateValue(object value)
        {
            if (String.IsNullOrEmpty(Property)) throw new InvalidOperationException("Search filter field cannot be null.");

            if (Condition == SchemaCondition.Contains)
            {
                if (value == null && Value == null) return true;
                if (value == null && Value != null) return false;
                if (value != null && Value == null) return false;

                string str1 = value.ToString();
                string str2 = Value.ToString();
                return str1.Contains(str2);
            }
            else if (Condition == SchemaCondition.ContainsNot)
            {
                if (value == null && Value == null) return false;
                if (value == null && Value != null) return true;
                if (value != null && Value == null) return true;

                string str1 = value.ToString();
                string str2 = Value.ToString();
                return !str1.Contains(str2);
            }
            else if (Condition == SchemaCondition.EndsWith)
            {
                if (value == null && Value == null) return true;
                if (value == null && Value != null) return false;
                if (value != null && Value == null) return false;

                string str1 = value.ToString();
                string str2 = Value.ToString();
                return str1.EndsWith(str2);
            }
            else if (Condition == SchemaCondition.Equals)
            {
                if (value == null && Value == null) return true;
                if (value == null && Value != null) return false;
                if (value != null && Value == null) return false;

                string str1 = value.ToString();
                string str2 = Value.ToString();
                return str1.Equals(str2);
            }
            else if (Condition == SchemaCondition.GreaterThan)
            {
                if (value == null || Value == null) return false;

                if (value is DateTime)
                {
                    DateTime dt1 = Convert.ToDateTime(value);
                    DateTime dt2 = Convert.ToDateTime(Value);
                    return dt1 > dt2;
                }
                else if (value is decimal)
                {
                    decimal d1 = Convert.ToDecimal(value);
                    decimal d2 = Convert.ToDecimal(Value);
                    return d1 > d2;
                }
                else if (value is long)
                {
                    long l1 = Convert.ToInt64(value);
                    long l2 = Convert.ToInt64(Value);
                    return l1 > l2;
                }
                else if (value is int)
                {
                    int i1 = Convert.ToInt32(value);
                    int i2 = Convert.ToInt32(Value);
                    return i1 > i2;
                }

                return false;
            }
            else if (Condition == SchemaCondition.GreaterThanOrEqualTo)
            {
                if (value == null && Value == null) return true;
                if (value == null || Value == null) return false;

                if (value is DateTime)
                {
                    DateTime dt1 = Convert.ToDateTime(value);
                    DateTime dt2 = Convert.ToDateTime(Value);
                    return dt1 >= dt2;
                }
                else if (value is decimal)
                {
                    decimal d1 = Convert.ToDecimal(value);
                    decimal d2 = Convert.ToDecimal(Value);
                    return d1 >= d2;
                }
                else if (value is long)
                {
                    long l1 = Convert.ToInt64(value);
                    long l2 = Convert.ToInt64(Value);
                    return l1 >= l2;
                }
                else if (value is int)
                {
                    int i1 = Convert.ToInt32(value);
                    int i2 = Convert.ToInt32(Value);
                    return i1 >= i2;
                }

                return false;
            }
            else if (Condition == SchemaCondition.IsNotNull)
            {
                if (value == null) return false;
                return true;
            }
            else if (Condition == SchemaCondition.IsNull)
            {
                if (value != null) return false;
                return true;
            }
            else if (Condition == SchemaCondition.LessThan)
            {
                if (value == null || Value == null) return false;

                if (value is DateTime)
                {
                    DateTime dt1 = Convert.ToDateTime(value);
                    DateTime dt2 = Convert.ToDateTime(Value);
                    return dt1 < dt2;
                }
                else if (value is decimal)
                {
                    decimal d1 = Convert.ToDecimal(value);
                    decimal d2 = Convert.ToDecimal(Value);
                    return d1 < d2;
                }
                else if (value is long)
                {
                    long l1 = Convert.ToInt64(value);
                    long l2 = Convert.ToInt64(Value);
                    return l1 < l2;
                }
                else if (value is int)
                {
                    int i1 = Convert.ToInt32(value);
                    int i2 = Convert.ToInt32(Value);
                    return i1 < i2;
                }

                return false;
            }
            else if (Condition == SchemaCondition.LessThanOrEqualTo)
            {
                if (value == null && Value == null) return true;
                if (value == null || Value == null) return false;

                if (value is DateTime)
                {
                    DateTime dt1 = Convert.ToDateTime(value);
                    DateTime dt2 = Convert.ToDateTime(Value);
                    return dt1 <= dt2;
                }
                else if (value is decimal)
                {
                    decimal d1 = Convert.ToDecimal(value);
                    decimal d2 = Convert.ToDecimal(Value);
                    return d1 <= d2;
                }
                else if (value is long)
                {
                    long l1 = Convert.ToInt64(value);
                    long l2 = Convert.ToInt64(Value);
                    return l1 <= l2;
                }
                else if (value is int)
                {
                    int i1 = Convert.ToInt32(value);
                    int i2 = Convert.ToInt32(Value);
                    return i1 <= i2;
                }

                return false;
            }
            else if (Condition == SchemaCondition.NotEquals)
            {
                if (value == null && Value == null) return false;
                if (value == null && Value != null) return true;
                if (value != null && Value == null) return true;

                string str1 = value.ToString();
                string str2 = Value.ToString();
                return !str1.Equals(str2);
            }
            else if (Condition == SchemaCondition.StartsWith)
            {
                if (value == null && Value == null) return true;
                if (value == null && Value != null) return false;
                if (value != null && Value == null) return false;

                string str1 = value.ToString();
                string str2 = Value.ToString();
                return str1.StartsWith(str2);
            }
            else
            {
                throw new ArgumentException("Unknown search filter condition: " + Condition.ToString() + ".");
            }
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}
