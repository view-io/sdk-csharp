namespace View.Sdk
{
    using System;

    /// <summary>
    /// Object statistics.
    /// </summary>
    public class ObjectStatistics
    {
        #region Public-Members

        /// <summary>
        /// Number of objects.
        /// </summary>
        public int Objects
        {
            get
            {
                return _Objects;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(Objects));
                _Objects = value;
            }
        }

        /// <summary>
        /// Number of bytes.
        /// </summary>
        public long Bytes
        {
            get
            {
                return _Bytes;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(Bytes));
                _Bytes = value;
            }
        }

        #endregion

        #region Private-Members

        private int _Objects = 0;
        private long _Bytes = 0;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public ObjectStatistics()
        {

        }

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="objects">Number of objects.</param>
        /// <param name="bytes">Number of bytes.</param>
        public ObjectStatistics(int objects, long bytes)
        {
            if (objects < 0) throw new ArgumentOutOfRangeException(nameof(objects));
            if (bytes < 0) throw new ArgumentOutOfRangeException(nameof(bytes));

            Objects = objects;
            Bytes = bytes;
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
