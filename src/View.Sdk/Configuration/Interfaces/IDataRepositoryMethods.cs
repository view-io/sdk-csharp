namespace View.Sdk.Configuration.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for data repository methods.
    /// </summary>
    public interface IDataRepositoryMethods
    {
        /// <summary>
        /// Create data repository.
        /// </summary>
        /// <param name="repository">Data repository.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Data repository.</returns>
        public Task<DataRepository> Create(DataRepository repository, CancellationToken token = default);

        /// <summary>
        /// Retrieve a data repository.
        /// </summary>
        /// <param name="repositoryGuid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Data repository.</returns>
        public Task<DataRepository> Retrieve(string repositoryGuid, CancellationToken token = default);

        /// <summary>
        /// Retrieve data repositories.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of data repository.</returns>
        public Task<List<DataRepository>> RetrieveMany(CancellationToken token = default);

        /// <summary>
        /// Update data repository.
        /// </summary>
        /// <param name="repository">Data repository.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Data repository.</returns>
        public Task<DataRepository> Update(DataRepository repository, CancellationToken token = default);

        /// <summary>
        /// Delete data repository.
        /// </summary>
        /// <param name="repositoryGuid">Data repository GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public Task<bool> Delete(string repositoryGuid, CancellationToken token = default);

        /// <summary>
        /// Data repository with pagination support.
        /// </summary>
        /// <param name="maxKeys">Maximum number of keys to return in the response. Default is 5.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>EnumerationResult containing Data repository.</returns>
        public Task<EnumerationResult<DataRepository>> Enumerate(int maxKeys = 5, CancellationToken token = default);
    }
}