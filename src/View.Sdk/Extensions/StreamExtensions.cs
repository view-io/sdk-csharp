namespace View.Sdk.Extensions
{
    using System.IO;

    /// <summary>
    /// Stream extensions.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Convert stream to bytes.
        /// </summary>
        /// <param name="stream">Stream.</param>
        /// <returns>Byte array.</returns>
        public static byte[] ToBytes(this Stream stream)
        {
            if (stream.CanSeek) stream.Seek(0, SeekOrigin.Begin);

            using (MemoryStream ms = new MemoryStream())
            {
                byte[] buf = new byte[4096];
                while (true)
                {
                    int read = stream.Read(buf, 0, buf.Length);
                    if (read > 0)
                    {
                        ms.Write(buf, 0, read);
                    }
                    else
                    {
                        break;
                    }
                }

                ms.Seek(0, SeekOrigin.Begin);
                return ms.ToArray();
            }
        }
    }
}
