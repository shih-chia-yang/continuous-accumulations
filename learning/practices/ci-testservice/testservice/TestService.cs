﻿using System;

namespace testservice
{
    public class TestService
    {
        public TestService()
        {

        }

        public int Add(int number)
        {
            if (number == 1)
                return 3;
            throw new NotImplementedException("number not equal 1");
        }
    }
}
