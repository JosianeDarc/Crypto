using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSKCrypto
{
    /*!
     * Klasa z metodami do szyfrowania z przykładu 2c. Na podstawie klucza (tekstu) generowany jest tekst.
     * */
    class MacierzoweC
    {
        //!
        //! Zamiana klucza w postaci tekstu na tablicę liczb
        //! \param[in] key  Klucz w postaci tekstu do sparsowania
        //! \return         Tablica liczb całkowitych - jako klucz
        public static int[] parseKey(String key)
        {
            int[] result = new int[key.Length];
            int counter = 0;
            key = key.ToUpper();

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
        //! \return             Zaszyfrowany tekst
        public static string Encrypt(String key, String value)
        {
            int[] keyNumbers = parseKey(key);
            //value = value.Replace(" ", String.Empty).Replace("\t", String.Empty).Replace("\n", String.Empty).Replace("\r", String.Empty);
            int keyLength = keyNumbers.Length;
            int cycleLength = ((1 + keyLength) / 2) * keyLength;
            int tabsCount = ((value.Length + cycleLength - 1) / cycleLength)*keyLength;
            char[,] tabs = new char[tabsCount, keyLength];
            int counter = 0; int keyCounter = -1;


            for (int i = 0; counter < value.Length; i++)
            {
                if (counter >= value.Length)
                    break;
                keyCounter = 0;
                for (int k = 0; k < keyLength; k++)
                {
                    if (keyNumbers[k] == i%keyLength)
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
        //! \return             Odszyfrowany tekst
        public static string Decrypt(String key, String value)
        {
            int[] keyNumbers = parseKey(key);
            //value = value.Replace(" ", String.Empty).Replace("\t", String.Empty).Replace("\n", String.Empty).Replace("\r", String.Empty);
            int keyLength = keyNumbers.Length;
            int[] linesSizes = new int[keyLength];
            for (int i = 0; i < keyLength; i++)
            {
                for (int k = 0; k < keyLength; k++)
                {
                    if (keyNumbers[k] == i)
                    {
                        linesSizes[i] = k + 1;
                        break;
                    }
                }
            }
            int cycleLength = 0;
            for (int i = 0; i < keyLength; i++)
            {
                cycleLength += i + 1;
            }
            int tabsCount = 0;
            int lastLineDif = value.Length % cycleLength;

            tabsCount = calculateTabSize(keyNumbers, value);
            if (tabsCount == -1)
                return "";

            char[,] tabs = new char[tabsCount, keyLength];
            char[] result = new char[value.Length + keyLength];
            int counter = 0;

            for (int i = 0; i < keyLength; i++)
            {
                if (lastLineDif > linesSizes[i])
                    lastLineDif -= linesSizes[i];
                else
                    break;
            }

            for (int i = 0; i < keyLength; i++)
            {
                int keyCounter = -1;
                for (int k = 0; k < keyLength; k++)
                {
                    if (keyNumbers[k] == i%keyLength)
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
                        if (j == keyNumbers[keyCounter] && j != tabsCount - 1)
                        {
                            tabs[j, keyCounter] = value[counter++]; //blad
                            continue;
                        }
                        for (int c = 0; c < keyCounter; c++)
                        {
                            if (j%keyLength == keyNumbers[c])
                            {
                                check = false;
                                break;
                            }
                        }
                        if(check) 
                        {
                            if (j == tabsCount - 1)
                            {
                                //JEŚLI OSTATNIA LINIA 
                                if (keyCounter < lastLineDif)
                                    tabs[j, keyCounter] = value[counter++];
                                else
                                    continue;

                            }
                            else
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
            char[] result2 = new char[counter];

            for (int i = 0; i < counter; i++)
            {
                result2[i] = result[i];
            }

            return new String(result2);
        }

        //!
        //! Metoda do obliczania ilości potrzebnych tablic
        //! \param[in] keyNumber    Klucz w postaci tablicy liczb całkowitych
        //! \param[in] value        Tekst do odszyfrowania
        //! \return                 Minimalna ilość tablic
        private static int calculateTabSize(int[] keyNumbers, string value)
        {
            int keyLength = keyNumbers.Length;
            int[] linesSizes = new int[keyLength];
            int cycleLength = 0;
            for (int i = 0; i < keyLength; i++)
            {
                cycleLength += i + 1;
            }
            int tabsCount = (value.Length / cycleLength) * keyLength;

            for (int i = 0; i < keyLength; i++)
            {
                for (int k = 0; k < keyLength; k++)
                {
                    if (keyNumbers[k] == i)
                    {
                        linesSizes[i] = k + 1;
                        break;
                    }
                }
            }

            int temp = value.Length / cycleLength;
            int leftChars = value.Length - cycleLength * temp - 1;
            for (int i = 0; i < keyLength; i++)
            {
                if (linesSizes[i] <= leftChars)
                {
                    tabsCount++;
                    leftChars -= linesSizes[i];
                }
                else
                {
                    return tabsCount+1;
                }
            }
            return -1;
        }
    }
}
