using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSKCrypto
{
    /*!
         * Klasa implementująca metody do szyfrowania i deszyfrowania tekstu za pomocą algorytmu RailFence
         * */
    public static class RailFence
    {
        //!
        //! Metoda do zaszyfrowania tekstu o zadanym kluczu
        //! \param[in] rail         Klucz używany przy szyfrowaniu
        //! \param[in] plainText    Tekst do zaszyfrowania
        //! \return                 Zaszyfrowany tekst zgodnie z podanym kluczem
        public static string Encrypt(int rail, string plainText)
        {
            if (rail <= 1)
                return plainText;
            List<string> railFence = new List<string>();
            for (int i = 0; i < rail; i++)
            {
                railFence.Add("");
            }

            int number = 0;
            int increment = 1;
            foreach (char c in plainText)
            {
                if (number + increment == rail)
                {
                    increment = -1;
                }
                else if (number + increment == -1)
                {
                    increment = 1;
                }
                railFence[number] += c;
                number += increment;
            }

            string buffer = "";
            foreach (string s in railFence)
            {
                buffer += s;
            }
            return buffer;
        }

        //!
        //! Metoda do odszyfrowywania zakodowanego tekstu o określonym kluczu
        //! \param[in] rail         Klucz do odszyfrowania
        //! \param[in] cipherText   Zaszyfrowany tekst
        //! \return                 Odszyfrowany tekst zgodnie z podanym kluczem
        public static string Decrypt(int rail, string cipherText)
        {
            if (rail <= 1)
                return cipherText;
            int cipherLength = cipherText.Length;
            List<List<int>> railFence = new List<List<int>>();
            for (int i = 0; i < rail; i++)
            {
                railFence.Add(new List<int>());
            }

            int number = 0;
            int increment = 1;
            for (int i = 0; i < cipherLength; i++)
            {
                if (number + increment == rail)
                {
                    increment = -1;
                }
                else if (number + increment == -1)
                {
                    increment = 1;
                }
                railFence[number].Add(i);
                number += increment;
            }

            int counter = 0;
            char[] buffer = new char[cipherLength];
            for (int i = 0; i < rail; i++)
            {
                for (int j = 0; j < railFence[i].Count; j++)
                {
                    buffer[railFence[i][j]] = cipherText[counter];
                    counter++;
                }
            }

            return new string(buffer);
        }
    }
}
