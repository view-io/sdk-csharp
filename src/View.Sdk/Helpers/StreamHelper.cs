namespace View.Sdk.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Stream helpers.
    /// </summary>
    public static class StreamHelper
    {
        /// <summary>
        /// Read a stream fully.
        /// </summary>
        /// <param name="stream">Stream.</param>
        /// <returns>Bytes.</returns>
        public static byte[] ReadFully(Stream stream)
        {
            if (stream == null || !stream.CanRead) return Array.Empty<byte>();

            using var memoryStream = new MemoryStream();
            const int bufferSize = 65536; // Default buffer size used by StreamReader
            var buffer = new byte[bufferSize];
            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                memoryStream.Write(buffer, 0, bytesRead);

            return memoryStream.ToArray();
        }
    }
}
