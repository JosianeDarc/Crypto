using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSKCrypto
{
    /*!
     * Klasa z metodami do szyfrowania z przykładu 4. Na podstawie klucza (tekstu) generowany jest tekst.
     * */
    class Vigenere
    {
        string key;
        static char[,] tableau = fillArray();

        //!
        //! Konstruktor do utworzenia instancji klasy
        //! \param[in] key Ustawia klucz do szyfrowania
        public Vigenere(string key)
        {
            this.key = key;
        }

        //! Wypałnia tablice używaną do zakodowania tekstu
        private static char[,] fillArray()
        {
            String letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int length = letters.Length;

            tableau = new char[length, length];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    int getLetter = j + i;

                    if (getLetter > (length - 1)) { getLetter -= length; }
                    tableau[i, j] = Convert.ToChar(letters.Substring(getLetter, 1));
                }
            }

            return tableau;
        }

        //!
        //! Metoda do szyfrowania tekstu
        //! \param[in] value    Tekst do zaszyfrowania
        //!
        public string Encrypt(string plainText)
        {
            String result = "";

            // Uppercase and trim our strings
            key = key.Trim().ToUpper();
            plainText = plainText.Trim().ToUpper();

            int keyIndex = 0;
            int keylength = key.Length;

            if ((keylength <= 0)) { return result; }

            for (int i = 0; i < plainText.Length; i++)
            {
                
                if (Char.IsLetter(plainText, i))
                {

                    keyIndex = keyIndex % keylength;
                    result += LookupLetter(key.Substring(keyIndex, 1), plainText.Substring(i, 1));
                    keyIndex++;
                }
            }
            return result;
        }

        //!
        //! Zamiania znak z tekstu na znak z tablicy (zakodowany)
        //! \param[in] character1 Znak klucza
        //! \param[in] character2 Znak tekstu do zakodowania
        private static char LookupLetter(String character1, String character2)
        {
            int letter1 = Convert.ToInt32(Convert.ToChar(character1));
            int letter2 = Convert.ToInt32(Convert.ToChar(character2));

            letter1 -= 65;
            letter2 -= 65;

            if ((letter1 >= 0) && (letter1 <= 26))
            {
                if ((letter2 >= 0) && (letter2 <= 26))
                {
                    return tableau[letter1, letter2];
                }
            }
            return ' ';
        }

        //!
        //! Metoda do odkodowania tekstu na podstawie znanego klucza
        //! \param[in] value    Zaszyfrowany tekst do odkodowania
        //!
        public string Decrypt(string cipherText)
        {
            String result = "";

            key = key.Trim().ToUpper(); ;
            cipherText = cipherText.Trim().ToUpper();

            int keyIndex = 0;
            int keylength = key.Length;

            if (keylength <= 0) { return result; }

            for (int i = 0; i < cipherText.Length; i++)
            {

                if (Char.IsLetter(cipherText, i))
                {
                    keyIndex = keyIndex % keylength;
                    int keyChar = Convert.ToInt32(Convert.ToChar(key.Substring(keyIndex, 1))) - 65;

                    for (int find = 0; find < 26; find++)
                    {
                        if (tableau[keyChar, find].ToString().CompareTo(cipherText.Substring(i, 1)) == 0)
                        {
                            result += (char)(find + 65);
                        }
                    }

                    keyIndex++;
                }
            }
            return result;
        }
    }
}
