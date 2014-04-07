using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSKCrypto
{
    class DES
    {
        #region Statis tabs
        private static int[] PC1 = { 57, 49, 41, 33, 25, 17, 9,
                                 1, 58, 50, 42, 34, 26, 18,
                                 10, 2, 59, 51, 43, 35, 27,
                                 19, 11, 3, 60, 52, 44, 36,
                                 63, 55, 47, 39, 31, 23, 15,
                                 7, 62, 54, 46, 38, 30, 22,
                                 14, 6, 61, 53, 45, 37, 29,
                                 21, 13, 5, 28, 20, 12, 4};
        private static int[] PC2 = { 14, 17, 11, 24, 1, 5,
                                   3, 28, 15, 6, 21, 10,
                                   23, 19, 12, 4, 26, 8,
                                   16, 7, 27, 20, 13, 2,
                                   41, 52, 31, 37, 47, 55,
                                   30, 40, 51, 45, 33, 48,
                                   44, 49, 39, 56, 34, 53,
                                   46, 42, 50, 36, 29, 32};
        private static int[] IP = { 58, 50, 42, 34, 26, 18, 10, 2,
                                  60, 52, 44, 36, 28, 20, 12, 4,
                                  62, 54, 46, 38, 30, 22, 14, 6,
                                  64, 56, 48, 40, 32, 24, 16, 8,
                                  57, 49, 41, 33, 25, 17, 9, 1,
                                  59, 51, 43, 35, 27, 19, 11, 3,
                                  61, 53, 45, 37, 29, 21, 13, 5,
                                  63, 55, 47, 39, 31, 23, 15, 7};
        private static int[] IP1 = { 40, 8, 48, 16, 56, 24, 64, 32,
                                   39, 7, 47, 15, 55, 23, 63, 31,
                                   38, 6, 46, 14, 54, 22, 62, 30,
                                   37, 5, 45, 13, 53, 21, 61, 29,
                                   36, 4, 44, 12, 52, 20, 60, 28,
                                   35, 3, 43, 11, 51, 19, 59, 27,
                                   34, 2, 42, 10, 50, 18, 58, 26,
                                   33, 1, 41, 9, 49, 17, 57, 25};
        private static int[] E = { 32, 1, 2, 3, 4, 5,
                                 4, 5, 6, 7, 8, 9,
                                 8, 9, 10, 11, 12, 13,
                                 12, 13, 14, 15, 16, 17,
                                 16, 17, 18, 19, 20, 21,
                                 20, 21, 22, 23, 24, 25,
                                 24, 25, 26, 27, 28, 29,
                                 28, 29, 30, 31, 32, 1};
        private static int[] S1 = { 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7,
      0, 15,   7,  4,  14,  2,  13,  1,  10,  6,  12, 11,   9,  5,   3,  8,
      4,  1,  14,  8,  13,  6,   2, 11,  15, 12,   9,  7,   3, 10,   5,  0,
     15, 12,   8,  2,   4,  9,   1,  7,   5, 11,   3, 14,  10,  0,   6, 13};

        private static int[] S2 = { 15,  1,   8, 14,   6, 11,   3,  4,   9,  7,   2, 13,  12,  0,   5, 10,
      3, 13,   4,  7,  15,  2,   8, 14,  12,  0,   1, 10,   6,  9,  11,  5,
      0, 14,   7, 11,  10,  4,  13,  1,   5,  8,  12,  6,   9,  3,   2, 15,
     13,  8,  10,  1,   3, 15,   4,  2,  11,  6,   7, 12,   0,  5,  14,  9};
        private static int[] S3 = { 10,  0,   9, 14,   6,  3,  15,  5,   1, 13,  12,  7,  11,  4,   2,  8,
     13,  7,   0,  9,   3,  4,   6, 10,   2,  8,   5, 14,  12, 11,  15,  1,
     13,  6,   4,  9,   8, 15,   3,  0,  11,  1,   2, 12,   5, 10,  14,  7,
      1, 10,  13,  0,   6,  9,   8,  7,   4, 15,  14,  3,  11,  5,   2, 12};
        private static int[] S4 = { 7, 13,  14,  3,  0,  6,   9, 10,   1,  2,   8,  5,  11, 12,   4, 15,
     13,  8,  11,  5,   6, 15,   0,  3,   4,  7,   2, 12,   1, 10,  14,  9,
     10,  6,   9,  0,  12, 11,   7, 13,  15,  1,   3, 14,   5,  2,   8,  4,
      3, 15,   0,  6,  10,  1,  13,  8,   9,  4,   5, 11,  12,  7,   2, 14};
        private static int[] S5 = { 2, 12,   4,  1,   7, 10,  11,  6,   8,  5,   3, 15,  13,  0,  14,  9,
     14, 11,   2, 12,   4,  7,  13,  1,   5,  0,  15, 10,   3,  9,   8,  6,
      4,  2,   1, 11,  10, 13,   7,  8,  15,  9,  12,  5,   6,  3,   0, 14,
     11,  8,  12,  7,   1, 14,   2, 13,   6, 15,   0,  9,  10,  4,   5,  3};
        private static int[] S6 = { 12,  1,  10, 15,   9,  2,   6,  8,   0, 13,   3,  4,  14,  7,   5, 11,
     10, 15,   4,  2,   7, 12,   9,  5,   6,   1, 13, 14,   0, 11,   3,  8,
      9, 14,  15,  5,   2,  8,  12,  3,   7,  0,   4, 10,   1, 13,  11,  6,
      4,  3,   2, 12,   9,  5,  15, 10,  11, 14,   1,  7,   6,  0,   8, 13};
        private static int[] S7 = { 4, 11,   2, 14,  15,  0,   8, 13,   3, 12,   9,  7,   5, 10,   6,  1,
     13,  0,  11,  7,   4,  9,   1, 10,  14,  3,   5, 12,   2, 15,   8,  6,
      1,  4,  11, 13,  12,  3,   7, 14,  10, 15,   6,  8,   0,  5,   9,  2,
      6, 11,  13,  8,   1,  4,  10,  7,   9,  5,   0, 15,  14,  2,   3, 12};
        private static int[] S8 = { 13,  2,   8,  4,   6, 15,  11,  1,  10,  9,   3, 14,   5,  0,  12,  7,
      1, 15,  13,  8,  10,  3,   7,  4,  12,  5,   6, 11,   0, 14,   9,  2,
      7, 11,   4,  1,   9, 12,  14,  2,   0,  6,  10, 13,  15,  3,   5,  8,
      2,  1,  14,  7,   4, 10,   8, 13,  15, 12,   9,  0,  3,  5,   6, 11};

        private static int[][] S = { S1, S2, S3, S4, S5, S6, S7, S8 };

        private static int[] P = { 16, 7, 20, 21,
                                 29, 12, 28, 17,
                                 1, 15, 23, 26,
                                 5, 18, 31, 10,
                                 2, 8, 24, 14,
                                 32, 27, 3, 9,
                                 19, 13, 30, 6,
                                 22, 11, 4, 25};

        private static int[] shiftsNumber = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };
        #endregion

        public string Encrypt3DES(String key1, String key2, String text)
        {
            String result1 = EncryptText(key1, text);
            String result2 = DecryptText(key2, result1);

            return EncryptText(key1, result2);
        }

        public string Decrypt3DES(String key1, String key2, String text)
        {
            String result1 = DecryptText(key1, text);
            String result2 = EncryptText(key2, result1);

            return DecryptText(key1, result2);
        }

        public string EncryptText(String key, String text)
        {
            StringBuilder sb = new StringBuilder();
            byte[] b = Encoding.Default.GetBytes(text);
            BitArray bits;
            if (b.Length % 8 == 0)
                bits = new BitArray(b);
            else
            {
                bits = new BitArray((b.Length - b.Length % 8) * 8 + 64);
                BitArray temp = new BitArray(b);
                for (int i = 0; i < temp.Length; i++)
                {
                    bits[i] = temp[i];
                }
                for (int i = temp.Length; i < bits.Length; i+=8)
                {
                    bits[i] = bits[i + 1] = bits[i + 2] = bits[i + 3] = bits[i + 6] = bits[i + 7] = false;
                    bits[i + 4] = bits[i + 5] = true;
                }
            }
            reverseBitsInBytes(bits);
            BitArray[] tabs;
            if(b.Length % 8 == 0)
                tabs = new BitArray[b.Length/8];
            else
                tabs = new BitArray[b.Length/8 + 1];
            int counter = 0;

            for(int i=0; i<tabs.Length; i++) 
            {
                tabs[i] = new BitArray(64);
                for(int j=0; j<64; j++) 
                {
                    tabs[i][j] = bits[counter++];
                }
            }
            for (int i = 0; i < tabs.Length; i++)
            {
                sb.Append(Encrypt(key, tabs[i]));
            }

            return sb.ToString();
        }

        public string DecryptText(String key, String text)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < text.Length; i+=16 )
            {
                sb.Append(Decrypt(key, text.Substring(i, 16)));
            }

            byte[] bytes = new byte[sb.Length / 2];
            for (int i = 0, j = 0; i < bytes.Length*2; i+=2)
            {
                bytes[j++] = (byte) (hexToInt(sb[i])*16 + hexToInt(sb[i + 1]));
            }
            //BitArray bits = new BitArray(bytes);
            //reverseBitsInBytes(bits);
            //bits.CopyTo(bytes, 0);

            String result = Encoding.Default.GetString(bytes);
            StringBuilder build = new StringBuilder(result);

            for (int i = result.Length - 1; result[i] == '0'; i--)
            {
                build[i] = '\0';
            }

            return build.ToString();
            //return sb.ToString();
        }

        private int hexToInt(char h)
        {
            if (h < 'A')
                return h - 48;
            else
                return h - 55;
        }

        public string Encrypt(String key, BitArray m)
        {
            BitArray[] keys = generateKeys(permuteKeyPC1(key));

            //byte[] m = stringToHex(data);
            //BitArray m = new BitArray(stringToHex(data));
            //reverseBitsInBytes(m);
            BitArray ip = new BitArray(64);
            for (int i = 0; i < 64; i++)
            {
                ip[i] = m[IP[i]-1];
            }

            BitArray left = copyBits(ip, 0, 32);
            BitArray right = copyBits(ip, 32, 64);
            BitArray temp = new BitArray(right);


            //calculate first step, when n=1
            right = left.Xor(function(right, keys[0]));
            left = temp;

            for (int i = 1; i < 16; i++ )
            {
                temp = new BitArray(right);
                right = left.Xor(function(right, keys[i]));
                left = temp;
            }

            BitArray result = new BitArray(64);
            BitArray permutedResult = new BitArray(64);
            for (int i = 0; i < 32; i++)
            {
                result[i] = right[i];
            }
            for (int i = 32; i < 64; i++)
            {
                result[i] = left[i-32];
            }
            for (int i = 0; i < 64; i++ )
            {
                permutedResult[i] = result[IP1[i] - 1];
            }

            //foreach (bool b in permutedResult)
                //Console.Write(b==true?1:0);

            return hexToString(permutedResult);
        }

        public string Decrypt(String key, String data)
        {
            BitArray[] keys = generateKeys(permuteKeyPC1(key));

            //byte[] m = stringToHex(data);
            BitArray m = new BitArray(stringToHex(data));
            reverseBitsInBytes(m);
            BitArray ip = new BitArray(64);
            for (int i = 0; i < 64; i++)
            {
                ip[i] = m[IP[i] - 1];
            }

            BitArray right = copyBits(ip, 0, 32);
            BitArray left = copyBits(ip, 32, 64);
            BitArray temp = new BitArray(left); 


            //calculate first step, when n=1
            left = (function(left, keys[15])).Xor(right);
            right = temp;

            for (int i = 1; i < 16; i++)
            {
                temp = new BitArray(left);
                left = (function(left, keys[15-i])).Xor(right);
                right = temp;
            }

            BitArray result = new BitArray(64);
            BitArray permutedResult = new BitArray(64);
            for (int i = 0; i < 32; i++)
            {
                result[i] = left[i];
            }
            for (int i = 32; i < 64; i++)
            {
                result[i] = right[i - 32];
            }
            for (int i = 0; i < 64; i++)
            {
                permutedResult[i] = result[IP1[i] - 1];
            }

            //foreach (bool b in permutedResult)
                //Console.Write(b == true ? 1 : 0);

            return hexToString(permutedResult);
        }

        private BitArray function(BitArray right, BitArray key)
        {
            BitArray extended = new BitArray(48);
            BitArray result = new BitArray(32);
            for (int i = 0; i < 48; i++)
            {
                extended[i] = right[E[i] - 1];
            }
            extended = extended.Xor(key);
            int counter = 0;
            for (int i = 0; i < 48; i += 6)
            {
                int row = bToI(extended[i]) * 2 + bToI(extended[i + 5]) * 1;
                int column = bToI(extended[i + 1]) * 8 + bToI(extended[i + 2]) * 4 +
                    bToI(extended[i + 3]) * 2 + bToI(extended[i + 4]) * 1;
                int v = S[i / 6][row * 16 + column];
                BitArray bits = iToB(v);
                for (int j = 0; j < 4; j++)
                {
                    result[counter++] = bits[j];
                }
            }
            BitArray permutedResult = new BitArray(32);
            for (int i = 0; i < 32; i++ )
            {
                permutedResult[i] = result[P[i] - 1];
            }

            return permutedResult;
        }

        private int bToI(bool b)
        {
            return b == true ? 1 : 0;
        }

        private BitArray iToB(int v)
        {
            BitArray result = new BitArray(4);
            result[3] = (v % 2 == 1 ? true:false); v = v / 2;
            result[2] = (v % 2 == 1 ? true : false); v = v / 2;
            result[1] = (v % 2 == 1 ? true : false); v = v / 2;
            result[0] = (v % 2 == 1 ? true : false); v = v / 2;
            return result;
        }

        private BitArray copyBits(BitArray ip, int p1, int p2)
        {
            BitArray result = new BitArray(p2 - p1);
            for (int i = p1; i < p2; i++)
            {
                result[i-p1] = ip[i];
            }
            return result;
        }

        private BitArray[] generateKeys(BitArray key)
        {
            BitArray[] shifts = new BitArray[17];
            shifts[0] = new BitArray(key);

            for (int i = 1; i < 17; i++)
            {
                shifts[i] = shift(shifts[i - 1], shiftsNumber[i-1]);
            }

            /*foreach (BitArray ar in shifts)
            {
                int counter = 0;
                foreach (bool b in ar)
                {
                    //Console.Write((b == true ? 1 : 0));
                    if (counter == 27)
                    {
                        //Console.Write("\n");
                        counter = 0;
                    }
                    else
                        counter++;
                }
                //Console.WriteLine();
            }*/

            BitArray[] permutedKeys = new BitArray[16];

            for (int i = 0; i < 16; i++)
            {
                permutedKeys[i] = new BitArray(48);
                for (int j = 0; j < 48; j++)
                {
                    permutedKeys[i][j] = shifts[i+1][PC2[j] - 1];
                }
            }
            return permutedKeys;
        }

        private BitArray shift(BitArray key, int c)
        {
            BitArray result = new BitArray(key);

            for (int k = 0; k < c; k++)
            {
                bool first1 = result[0];
                bool first2 = result[28];
                for (int i = 0; i < 27; i++)
                {
                    result[i] = result[i + 1];
                    result[i + 28] = result[i + 29];
                }
                result[27] = first1;
                result[55] = first2;
            }

            return result;
        }

        private BitArray permuteKeyPC1(String key)
        {
            key = key.ToUpper();
            for (int i = 0; i < key.Length; i++)
            {
                if (key[i] > 'F' || key[i] < 'A')
                {
                    if (key[i] < '0' || key[i] > '9')
                        throw new BadKeyException();
                }
            }
            byte[] tab = stringToHex(key);
            if (tab == null)
                return null;
            BitArray bits = new BitArray(tab);
            BitArray permuted = new BitArray(56);
            
            // Reverse bits in bytes because of constructor BitArray(tab)
            reverseBitsInBytes(bits);
            for (int i = 0; i < 56; i++ )
            {
                permuted[i] = bits[PC1[i]-1];
            }
            /*foreach (bool b in permuted)
            {
                Console.Write((b == true ? 1 : 0));
            }*/
            return permuted;
        }

        private static void reverseBitsInBytes(BitArray bits)
        {
            for (int i = 0; i < bits.Length; i += 8)
            {
                for (int j = 0; j < 4; j++)
                {
                    bool temp = bits[j + i];
                    bits[j + i] = bits[i + 7 - j];
                    bits[i + 7 - j] = temp;
                }
            }
        }

        private byte[] stringToHex(String text)
        {
            //if (text.Length != 16)
                //return null;
            text = text.ToUpper();
            byte[] array = Encoding.Default.GetBytes(text);
            byte[] result = new byte[8];

            for (int i = 0; i < array.Length; i += 2)
            {
                byte b1 = array[i];
                byte b2 = array[i + 1];
                b1 = (byte) ( b1<65 ? (b1 << 4) : ((b1-55)<<4) );
                b2 = (byte) ( b2<65 ? (b2 - 48) : (b2-55) );
                byte temp = (byte) (b1 | b2);
                result[i / 2] = temp;
            }

            return result;
        }

        private String hexToString(BitArray bits)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bits.Length; i+=4)
            {
                result.Append( toChar( (bits[i]==true?1:0) * 8
                    + (bits[i+1] == true ? 1 : 0) * 4
                    + (bits[i+2] == true ? 1 : 0) * 2
                    + (bits[i+3] == true ? 1 : 0) * 1));
            }
            return result.ToString();
        }

        private char toChar(int p)
        {
            if (p < 10)
                return (char) (p + 48);
            else
                return (char) (p + 55);
        }
    }

    class BadKeyException : Exception
    {
        public BadKeyException()
        {
        }
        public BadKeyException(string message) : base(message)
        {
        }
        public BadKeyException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
