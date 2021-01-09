using System;

namespace testservice
{
    public class testservice
    {
        public testservice()
        {

        }

        public int Add(int number)
        {
            if (number == 1)
                return 2;
            throw new NotImplementedException("number not equal 1");
        }
    }
}
