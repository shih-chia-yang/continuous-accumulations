namespace TestService
{
    using System;

    /// <summary>
    /// test git hook pre-commit
    /// </summary>
    public class TestService
    {
        public int Add(int number)
        {
            if (number == 1)
            {
                return 2;
            }

            throw new NotImplementedException("number not equal 1");
        }

        public int Minus(int number)
        {
            if (number == 2)
            {
                return 1;
            }

            throw new NotImplementedException("number not equal 2");
        }
    }
}
