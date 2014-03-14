using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSKCrypto
{
    public static class MacierzoweB
    {

        public static int[] parseKey(String key)
        {
            int[] result = new int[key.Length];
            int counter = 0;

            for (char k = 'A'; k < 'Z'; k++)
            {
                for (int i = 0; i < key.Length; i++)
                {
                    if (key[i] == k)
                    {
                        result[i] = counter;
                        counter++;
                    }
                }
            }
            return result;
        }

        public static string Encrypt(String key, String value)
        {
            int[] keyNumbers = parseKey(key);
            //value = value.Replace(" ", string.Empty);
            int keyLength = keyNumbers.Length;
            int tabsCount = (value.Length + keyLength - 1) / keyLength;
            char[,] tabs = new char[tabsCount, keyLength];

            for (int i = 0; i <= tabsCount; i++)
            {
                for (int j = 0; j < keyLength; j++)
                {
                    if (i * keyLength + j >= value.Length)
                        break;
                    tabs[i, j] = value[i * keyLength + j];
                }
            }

            char[] result = new char[2*value.Length + keyLength];
            int counter = 0;
            // 0 1 2 3  4 5 6 7 8 9 10 
            // 0 9 6 10 2 7 5 3 8 1 4
            for (int i = 0; i < keyLength; i++)
            {
                int keyCounter = -1;
                for (int k = 0; k < keyLength; k++)
                {
                    if (keyNumbers[k] == i)
                    {
                        keyCounter = k;
                    }
                }
                    for (int j = 0; j < tabsCount; j++)
                    {
                        try
                        {
                            if (tabs[j, keyCounter] == '\0')
                            {
                                //result[counter++] = ' ';
                                break;
                            }
                            result[counter++] = tabs[j, keyCounter];
                        }
                        catch (Exception) { }
                    }
                //result[counter++] = ' ';
            }

            return new String(result);
        }

        public static string Decrypt(String key, String value)
        {
            int[] keyNumbers = parseKey(key);
            //value = value.Replace(" ", string.Empty);
            int keyLength = keyNumbers.Length;
            int tabsCount = (value.Length + keyLength - 1) / keyLength;
            char[,] tabs = new char[tabsCount, keyLength];
            int lastLineCount = value.Length - (tabsCount - 1) * keyLength;

            char[] result = new char[2 * value.Length + keyLength];
            int counter = 0;

            for (int i = 0; i < keyLength; i++)
            {
                int keyCounter = -1;
                for (int k = 0; k < keyLength; k++)
                {
                    if (keyNumbers[k] == i)
                    {
                        keyCounter = k;
                        break;
                    }
                }
                for (int j = 0; j < tabsCount; j++)
                {
                    if (j == tabsCount - 1 && keyCounter >= lastLineCount)
                    {
                        break;
                    }
                    //if (i * keyLength + j >= value.Length)
                        //break;
                    try
                    {
                        tabs[j, keyCounter] = value[counter];
                        counter++;
                    }
                    catch (Exception) { }
                }

            }
            counter = 0;

            for (int i = 0; i < tabsCount; i++)
            {
                for (int j = 0; j < keyLength; j++)
                {
                    if (tabs[i, j] == '\0')
                        break;
                    result[counter++] = tabs[i, j];
                }
            }

                return new String(result);
        }
    }
}
