namespace WindowsFormsUserControl.Validators
{
    public static class DrfoValidator
    {
        private const int DrfoLength = 10;
        private static readonly int[] Weights = new int[] { -1, 5, 7, 9, 4, 6, 10, 5, 7 };

        public static bool ValidateDrfo(string drfo)
        {
            if (string.IsNullOrEmpty(drfo))
            {
                return false;
            }
            if (drfo.Length != DrfoLength || !drfo.All(char.IsDigit))
            {
                return false;
            }
            int[] digits = drfo.Select(x => x - '0').ToArray();
            int weightedSum = 0;
            for (int i = 0; i < Weights.Length; i++)
            {
                weightedSum += Weights[i] * digits[i];
            }
            // Calculate the control digit according to specification
            int controlDigit = (weightedSum % 11) % 10;
            // Check if the control digit matches the last digit
            return controlDigit == digits[9];
        }
    }
}
