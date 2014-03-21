using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSKCrypto
{
    class MacierzoweC
    {
        //!
        //! Zamiana klucza w postaci tekstu na tablicę liczb
        //! \param[in] key  Klucz w postaci tekstu do sparsowania
        //!
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

        //!
        //! Metoda do szyfrowania tekstu na podstawie klucza
        //! \param[in] key      Klucz w postaci tekstu, który zostanie sparsowany
        //! \param[in] value    Tekst do zaszyfrowania
        //!
        public static string Encrypt(String key, String value)
        {
            int[] keyNumbers = parseKey(key);
            value = value.Replace(" ", String.Empty).Replace("\t", String.Empty).Replace("\n", String.Empty).Replace("\r", String.Empty);
            int keyLength = keyNumbers.Length;
            int tabsCount = 1000;
            char[,] tabs = new char[tabsCount, keyLength];
            int counter = 0; int keyCounter = -1;


            for (int i = 0; counter < value.Length; i++)
            {
                if (counter >= value.Length)
                    break;
                keyCounter = 0;
                for (int k = 0; k < keyLength; k++)
                {
                    if (keyNumbers[k] == i)
                    {
                        keyCounter = k;
                    }
                }
                for (int j = 0; j < keyCounter+1; j++)
                {
                    try
                    {
                        tabs[i, j] = value[counter++];
                    }
                    catch (Exception) { }
                }
            }
            //(2 * a1 + (n - 1))/2 * n = x
            char[] result = new char[2 * value.Length + keyLength];
            counter = 0;
            // 0 1 2 3  4 5 6 7 8 9 10 
            // 0 9 6 10 2 7 5 3 8 1 4
            for (int i = 0; i < keyLength; i++)
            {
                if (counter >= value.Length)
                    break;
                keyCounter = -1;
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
                            continue;
                        }
                        result[counter++] = tabs[j, keyCounter];
                    }
                    catch (Exception) { }
                }
                //result[counter++] = ' ';
            }

            return new String(result);
        }

        //!
        //! Metoda do odkodowania tekstu na podstawie znanego klucza
        //! \param[in] key      Klucz na podstawie którego zostanie odszyfrowany tekst
        //! \param[in] value    Zaszyfrowany tekst do odkodowania
        //!
        public static string Decrypt(String key, String value)
        {
            int[] keyNumbers = parseKey(key);
            value = value.Replace(" ", String.Empty).Replace("\t", String.Empty).Replace("\n", String.Empty).Replace("\r", String.Empty);
            int keyLength = keyNumbers.Length;
            int tabsCount = 7;
            char[,] tabs = new char[tabsCount, keyLength];

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
                    try
                    {
                        bool check = true;
                        if (j == keyNumbers[keyCounter])
                        {
                            tabs[j, keyCounter] = value[counter++];
                            continue;
                        }
                        for (int c = 0; c < keyCounter; c++)
                        {
                            if (j == keyNumbers[c])
                            {
                                check = false;
                                break;
                            }
                        }
                        if(check) 
                        {
                            tabs[j, keyCounter] = value[counter++];
                        }
                        else {
                            continue;
                        }
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
