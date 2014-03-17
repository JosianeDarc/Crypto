﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSKCrypto
{
    /*!
     * Klasa z metodami do szyfrowania z przykładu 2a. Na podstawie klucza (ciągu odpowiednich liczb) generowany jest tekst.
     * */
    public static class MacierzoweA
    {
        //!
        //! Zamiana klucza w postaci tekstu na tablicę liczb
        //! \param[in] key  Klucz w postaci tekstu do sparsowania
        //!
        private static int[] parseKey(String key)
        {
            // 3-1-4-2
            string[] temp = key.Split('-');
            int[] keyNumbers = new int[temp.Length];

            for (int i = 0; i < temp.Length; i++)
            {
                try
                {
                    keyNumbers[i] = Convert.ToInt32(temp[i]);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            return keyNumbers;
        }

        //!
        //! Metoda do szyfrowania tekstu na podstawie klucza
        //! \param[in] key      Klucz w postaci tekstu, który zostanie sparsowany
        //! \param[in] value    Tekst do zaszyfrowania
        //!
        public static String Encrypt(String key, String value)
        {
            //int[] keyNumbers = new int[] { 3, 1, 4, 2 };
            int[] keyNumbers;
            if((keyNumbers = parseKey(key)) == null) {
                return "";
            }
            //int tabsCount = value.Length / keyLength;
            int keyLength = keyNumbers.Length;
            int tabsCount = (value.Length + keyLength - 1) / keyLength;
            char[,] tabs = new char[tabsCount, keyLength];

            for (int i = 0; i <= tabsCount; i++)
            {
                for (int j = 0; j < keyLength; j++)
                {
                    if (i * keyLength + j >= value.Length)
                        break;
                    tabs[i,j] = value[i*keyLength + j];
                }
            }

            char[] result = new char[value.Length];

            for (int i = 0; i < tabsCount; i++)
            {
                for (int j = 0; j < keyLength; j++)
                {
                    if (i * keyLength + j >= value.Length)
                        break;
                    if (tabs[i, keyNumbers[j] - 1] == '\0')
                    {
                        int tempj = j+1;
                        while (tabs[i, keyNumbers[tempj] - 1] == '\0' && tempj<keyLength)
                            tempj++;
                        result[i * keyLength + j] = tabs[i, keyNumbers[tempj] - 1];
                    }
                    else
                        result[i * keyLength + j] = tabs[i, keyNumbers[j]-1];
                }
            }

            return new String(result);
        }

        //!
        //! Metoda do odkodowania tekstu na podstawie znanego klucza
        //! \param[in] key      Klucz na podstawie którego zostanie odszyfrowany tekst
        //! \param[in] value    Zaszyfrowany tekst do odkodowania
        //!
        public static string Decrypyt(string key, string value)
        {
            int[] keyNumbers;
            if ((keyNumbers = parseKey(key)) == null)
            {
                return "";
            }
            int keyLength = keyNumbers.Length;
            int tabsCount = (value.Length + keyLength - 1) / keyLength;
            char[,] tabs = new char[tabsCount, keyLength];

            for (int i = 0; i < tabsCount; i++)
            {
                for (int j = 0; j < keyLength; j++)
                {
                    if (i * keyLength + j >= value.Length)
                        break;
                    if (i == tabsCount - 1 && keyNumbers[j] - 1 >= value.Length - i * keyLength)
                    {
                        int tempj = j + 1;
                        while (keyNumbers[j] - 1 > value.Length - i * keyLength && tempj < keyLength)
                            tempj++;
                        try
                        {
                            tabs[i, keyNumbers[tempj] - 1] = value[i * keyLength + j];
                        }
                        catch (Exception) { }
                    }
                    else
                        tabs[i, keyNumbers[j] - 1] = value[i * keyLength + j];
                    //YCPRGTROHAYPAOS
                    //3 0 - tabs[3,2] = A
                    //3 1 - tabs[3,0] = O
                    //3 2 - tabs[3,3] = S
                    //3 3 - tabs[3,1] = out
                }
            }

            char[] result = new char[value.Length];

            for (int i = 0; i < tabsCount; i++)
            {
                for (int j = 0; j < keyLength; j++)
                {
                    try
                    {
                        result[i * keyLength + j] = tabs[i, j];
                    }
                    catch (Exception) { }
                }
            }

            return new String(result);
        }
    }
}
