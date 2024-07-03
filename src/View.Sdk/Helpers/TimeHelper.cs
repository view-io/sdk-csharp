namespace View.Sdk.Helpers
{
    using System;

    /// <summary>
    /// Time helper.
    /// </summary>
    public static class TimeHelper
    {
        /// <summary>
        /// Determine the total number of milliseconds between a start and end time.
        /// </summary>
        /// <param name="start">Start time.</param>
        /// <param name="end">End time.</param>
        /// <returns>Milliseconds.</returns>
        public static double TotalMsBetween(DateTime start, DateTime end)
        {
            try
            {
                start = start.ToUniversalTime();
                end = end.ToUniversalTime();
                TimeSpan total = end - start;
                return total.TotalMilliseconds;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
