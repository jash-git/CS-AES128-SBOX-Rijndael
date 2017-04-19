using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography;
using System.IO;  

namespace CS_BYTES_AES_test
{
    class AESEncryption
    {
        //默认密钥向量
        //36649AF26DDE0C3A0F1E2D3C4B5A6978
        private static byte[] _key1 = { 0x36, 0x64, 0x9A, 0xF2, 0x6D, 0xDE, 0x0C, 0x3A, 0x0F, 0x1E, 0x2D, 0x3C, 0x4B, 0x5A, 0x69, 0x78 };

        /// <summary>  
        /// AES加密算法  
        /// </summary>  
        /// <param name="plainText">明文字符串</param>  
        /// <param name="strKey">密钥</param>  
        /// <returns>返回加密后的密文字节数组</returns>  
        public static byte[] AESEncrypt(byte[] inputByteArray)
        {
            //分组加密算法  
            SymmetricAlgorithm des = Rijndael.Create();
            //byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);//得到需要加密的字节数组      
            //设置密钥及密钥向量  
            des.Key = _key1;//Encoding.UTF8.GetBytes(strKey);
            des.IV = _key1;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            byte[] cipherBytes = ms.ToArray();//得到加密后的字节数组  
            cs.Close();
            ms.Close();
            return cipherBytes;
        }

        /// <summary>  
        /// AES解密  
        /// </summary>  
        /// <param name="cipherText">密文字节数组</param>  
        /// <param name="strKey">密钥</param>  
        /// <returns>返回解密后的字符串</returns>  
        public static byte[] AESDecrypt(byte[] cipherText)
        {
            SymmetricAlgorithm des = Rijndael.Create();
            des.Key = _key1;//Encoding.UTF8.GetBytes(strKey);
            des.IV = _key1;
            byte[] decryptBytes = new byte[cipherText.Length];
            MemoryStream ms = new MemoryStream(cipherText);
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read);
            cs.Read(decryptBytes, 0, decryptBytes.Length);
            cs.Close();
            ms.Close();
            return decryptBytes;
        }
    }  
    class Program
    {
        // 
        //
        static void pause()
        {
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
        public static string ToHexString(byte[] bytes,String flag=",") // 0xae00cf => "AE00CF "
        {

            string hexString = string.Empty;

            if (bytes != null)
            {

                StringBuilder strB = new StringBuilder();



                for (int i = 0; i < bytes.Length; i++)
                {

                    strB.Append(bytes[i].ToString("X2"));
                    strB.Append(flag);
                }

                hexString = strB.ToString();

            }

            return hexString;

        }
        static void Main(string[] args)
        {
            /*
            byte[] inputByteArray = { 0xAA, 0x55, 0xBB, 0x66, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            byte[] outputByteArray = AESEncryption.AESEncrypt(inputByteArray);
            byte[] outputByteArray1 = AESEncryption.AESDecrypt(outputByteArray);
            */

            //*
            Aes.KeySize keysize;
            keysize = Aes.KeySize.Bits128;
            byte[] inputByteArray = { 0xAA, 0x55, 0xBB, 0x66, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            byte[] _key1 = { 0x36, 0x64, 0x9A, 0xF2, 0x6D, 0xDE, 0x0C, 0x3A, 0x0F, 0x1E, 0x2D, 0x3C, 0x4B, 0x5A, 0x69, 0x78 };
            Aes a = new Aes(keysize, _key1);
            byte[] outputByteArray = new byte[16];
            a.Cipher(inputByteArray, outputByteArray);
            byte[] outputByteArray1 = new byte[16];
            a.InvCipher(outputByteArray, outputByteArray1);
            //*/
            
            string hex2 = ToHexString(outputByteArray);
            string hex1 = ToHexString(outputByteArray1);
            Console.WriteLine("input - " + hex1);
            Console.WriteLine("output - " + hex2);
            
            pause();
        }
    }
}
