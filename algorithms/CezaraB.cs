using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSKCrypto
{
    class CezaraB
    {
        static char Encrypt(char ch, int code)
        {
            int k0, k1;
            k0 = 3; k1 = 5;
            if (!char.IsLetter(ch))
            {
                return ch;
            }
            char offset = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch - offset)*k1 + k0) % 26) + offset);
        }

        //!
        //! Metoda do zaszyfrowania tekstu o zadanym kluczu
        //! \param[in] rail         Klucz używany przy szyfrowaniu
        //! \param[in] plainText    Tekst do zaszyfrowania
        //! \return                 Zaszyfrowany tekst zgodnie z podanym kluczem
        public static string Encrypt(int key, string value)
        {
            return new string(value.ToCharArray().Select(ch => Encrypt(ch, key)).ToArray());
        }

        //!
        //! Metoda do odszyfrowywania zakodowanego tekstu o określonym kluczu
        //! \param[in] rail         Klucz do odszyfrowania
        //! \param[in] cipherText   Zaszyfrowany tekst
        //! \return                 Odszyfrowany tekst zgodnie z podanym kluczem
        public static string Decrypt(int key, string value)
        {
            StringBuilder result = new StringBuilder();
            foreach (char ch in value.ToCharArray())
            {
                int k0, k1;
                k0 = 3; k1 = 5;
                if (!char.IsLetter(ch))
                {
                    result.Append(ch);
                }
                else
                {
                    char offset = char.IsUpper(ch) ? 'A' : 'a';
                    result.Append((char)(((ch - offset + (26 - k0)) * Math.Pow(k1, 11) % 26) + offset));
                }
            }
            return result.ToString();
        }
    }
}
