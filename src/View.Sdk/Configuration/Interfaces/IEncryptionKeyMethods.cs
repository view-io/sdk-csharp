namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for encryptionkey methods.
    /// </summary>
    public interface IEncryptionKeyMethods
    {
        /// <summary>
        /// Create an encryption key.
        /// </summary>
        /// <param name="key">Encryption key.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Encryption key.</returns>
        public Task<EncryptionKey> Create(EncryptionKey key, CancellationToken token = default);

        /// <summary>
        /// Check if an encryption key exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public Task<bool> Exists(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read an encryption key.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Encryption key.</returns>
        public Task<EncryptionKey> Retrieve(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Read encryption keys.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Encryption keys.</returns>
        public Task<List<EncryptionKey>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Update an encryption key.
        /// </summary>
        /// <param name="key">Encryption key.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Encryption key.</returns>
        public Task<EncryptionKey> Update(EncryptionKey key, CancellationToken token = default);

        /// <summary>
        /// Delete an encryption key.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(Guid guid, CancellationToken token = default);

        /// <summary>
        /// Enumerate encryption keys.
        /// </summary>
        /// <param name="maxKeys">Maximum number of keys to return.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Enumeration result containing encryption keys.</returns>
        public Task<EnumerationResult<EncryptionKey>> Enumerate(int maxKeys = 5, CancellationToken token = default);
    }
}
