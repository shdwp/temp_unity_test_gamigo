namespace UnityTechTest.Scripts.Utils
{
    /// <summary>
    /// Collection of various number utils
    /// </summary>
    public static class NumberUtils
    {
        /// <summary>
        /// Check if all digits in the number are unique
        /// </summary>
        /// <param name="number">number</param>
        /// <returns>true or false</returns>
        public static bool AreDigitsUnique(uint number)
        {
            // much faster than a collection and alloc-free
            var digitsMask = 0;

            do
            {
                var mod = (int)(number % 10);
                if ((digitsMask & (1 << mod)) != 0)
                {
                    return false;
                }

                digitsMask |= 1 << mod;
            } while ((number /= 10) > 0);

            return true;
        }
    }
}