namespace Utils
{
    public static class NumberUtils
    {
        public static bool AreDigitsUnique(uint val)
        {
            // much faster than a collection and alloc-free
            var digitsMask = 0;

            do
            {
                var mod = (int)(val % 10);
                if ((digitsMask & (1 << mod)) != 0)
                {
                    return false;
                }

                digitsMask |= 1 << mod;
            } while ((val /= 10) > 0);

            return true;
        }
    }
}