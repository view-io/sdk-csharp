namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for tenant methods.
    /// </summary>
    public interface ITenantMethods
    {
        /// <summary>
        /// Read a tenant.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Tenant.</returns>
        public Task<TenantMetadata> Retrieve(CancellationToken token = default);

        /// <summary>
        /// Retrieve multiple tenants.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of tenants.</returns>
        public Task<List<TenantMetadata>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Create a new tenant.
        /// </summary>
        /// <param name="tenant">Tenant to create.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Tenant setup result.</returns>
        public Task<TenantMetadata> Create(TenantMetadata tenant, CancellationToken token = default);

        /// <summary>
        /// Delete a tenant.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Update a tenant.
        /// </summary>
        /// <param name="tenant">Tenant.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Tenant.</returns>
        public Task<TenantMetadata> Update(TenantMetadata tenant, CancellationToken token = default);

        /// <summary>
        /// Enumerate tenants.
        /// </summary>
        /// <param name="maxKeys">Maximum number of keys to return.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result.</returns>
        public Task<EnumerationResult<TenantMetadata>> Enumerate(int maxKeys = 5, CancellationToken token = default);
    }
}
