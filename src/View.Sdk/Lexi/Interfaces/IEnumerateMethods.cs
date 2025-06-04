namespace View.Sdk.Lexi.Interfaces
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for enumerate methods.
    /// </summary>
    public interface IEnumerateMethods
    {
        /// <summary>
        /// Enumerate a collection.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="query">Query.</param>
        /// <param name="token">Token.</param>
        /// <returns>Enumeration result.</returns>
        public Task<EnumerationResult<SourceDocument>> Enumerate(Guid collectionGuid, EnumerationQuery query, CancellationToken token = default);
    }
}