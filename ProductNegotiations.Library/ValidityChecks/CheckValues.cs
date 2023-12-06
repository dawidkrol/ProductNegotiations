namespace ProductNegotiations.Library.ValidityChecks
{
    public class CheckValues
    {
        /// <summary>
        /// Max attempts to negotiate price
        /// </summary>
        public int? MaxAttempts = null;

        /// <summary>
        /// How many times can the price be lower than the default price
        /// </summary>
        public int? MaxTimesLowerPrice = null;
    }
}