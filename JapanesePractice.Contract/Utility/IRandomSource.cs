namespace JapanesePractice.Contract.Utility
{
    /// <summary>
    /// Represents a source of randomness.
    /// </summary>
    public interface IRandomSource
    {
        /// <summary>
        /// Returns a non-negative random integer that is less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random number to be generated. <paramref name="maxValue"/> must be greater than or equal to 0.
        /// </param>
        /// <returns>
        /// A 32-bit signed integer that is greater than or equal to 0, and less than <paramref name="maxValue"/>; that is, the range of return values ordinarily includes 0 but not <paramref name="maxValue"/>.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1716:IdentifiersShouldNotMatchKeywords",
            MessageId = "Next",
            Justification = "Naming convention comes from System.Random.")]
        int Next(int maxValue);
    }
}
