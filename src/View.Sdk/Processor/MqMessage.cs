namespace View.Sdk.Processor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Message queue message.
    /// </summary>
    public class MqMessage
    {
        #region Public-Members

        /// <summary>
        /// Message version.
        /// </summary>
        public MqMessageVersionEnum Version { get; set; } = MqMessageVersionEnum.v1_0;

        /// <summary>
        /// Type detection request.
        /// </summary>
        public MqMessageTypeEnum Type { get; set; } = MqMessageTypeEnum.TypeDetectionRequest;

        /// <summary>
        /// Data.
        /// </summary>
        public byte[] Data { get; set; } = null;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Message queue message.
        /// </summary>
        public MqMessage()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
