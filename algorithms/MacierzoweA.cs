using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSKCrypto
{
    class MacierzoweA
    {
        String key { get; set; }


        public String Encrypt(String value)
        {
            int keyLength = 4;
            int[] keyNumbers = new int[] { 3, 1, 4, 2 };
            int tabsCount = value.Length / keyLength;
            char[,] tabs = new char[tabsCount, keyLength];

            for (int i = 0; i <= tabsCount; i++)
            {
                for (int j = 0; j < keyLength; j++)
                {
                    tabs[i,j] = value[i*keyLength + j];
                }
            }

            char[] result = new char[value.Length];

            for (int i = 0; i < tabsCount; i++)
            {
                for (int j = 0; j < keyLength; j++)
                {
                        result[i * tabsCount + j] = tabs[i, keyNumbers[j]-1];
                }
            }

            return new String(result);
        }
    }
}
