using System;
using System.Collections.Generic;

namespace View.Sdk.Assistant
{
    /// <summary>
    /// Response containing a list of assistant configurations.
    /// </summary>
    public class AssistantConfigurationResponse
    {
        /// <summary>
        /// List of assistant configurations.
        /// </summary>
        public List<AssistantConfiguration> AssistantConfigs { get; set; }
    }

    /// <summary>
    /// Represents a single assistant configuration.
    /// </summary>
    public class AssistantConfiguration
    {
        /// <summary>
        /// GUID.
        /// </summary>
        public Guid GUID { get; set; } = Guid.Empty;

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = null;

        /// <summary>
        /// Description.
        /// </summary>
        public string Description { get; set; } = null;

        /// <summary>
        /// Created UTC.
        /// </summary>
        public DateTime CreatedUTC { get; set; } = DateTime.UtcNow;
    }
}
