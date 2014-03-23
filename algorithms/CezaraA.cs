using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSKCrypto
{
    /*!
     * Klasa z metodami do szyfrowania z przykładu 3a. Na podstawie klucza (tekstu) generowany jest tekst.
     * */
    class CezaraA
    {
        //!
        //! Metoda do zaszyfrowania pojedynczego znaku
        //! \param[in] code         Klucz używany przy szyfrowaniu
        //! \param[in] ch           Znak do zaszyfrowania
        //! \return                 Zaszyfrowany znak zgodnie z podanym kluczem
        static char Encrypt(char ch, int code)
        {
            if (!char.IsLetter(ch))
            {
                return ch;
            }
            char offset = char.IsUpper(ch) ? 'A' : 'a';
            return (char)(((ch + code - offset) % 26) + offset);
        }
        
        //!
        //! Metoda do zaszyfrowania tekstu o zadanym kluczu
        //! \param[in] key          Klucz używany przy szyfrowaniu
        //! \param[in] value        Tekst do zaszyfrowania
        //! \return                 Zaszyfrowany tekst zgodnie z podanym kluczem
        public static string Encrypt(int key, string value)
        {
            return new string(value.ToCharArray().Select(ch => Encrypt(ch, key)).ToArray());
        }

        //!
        //! Metoda do odszyfrowywania zakodowanego tekstu o określonym kluczu
        //! \param[in] key          Klucz do odszyfrowania
        //! \param[in] value        Zaszyfrowany tekst
        //! \return                 Odszyfrowany tekst zgodnie z podanym kluczem
        public static string Decrypt(int key, string value)
        {
            return Encrypt(26 - key, value);
        }
    }
}
