using System.Globalization;
using System.IO;

namespace ConquerToolsKit
{
    public class ConquerTools
    {
        public void ItemtypeDecrypt(string filename, string filenameOutput)
        {
            DatCrypto dc = new DatCrypto(DatCrypto.EncryptionKey.Common);
            byte[] output = dc.Decrypt(File.ReadAllBytes(filename));
            File.WriteAllBytes(filenameOutput, output);
        }
        public void ItemtypeEncrypt(string filename, string filenameOutput)
        {
            DatCrypto dc = new DatCrypto(DatCrypto.EncryptionKey.Common);
            byte[] output = dc.Encrypt(File.ReadAllBytes(filename));
            File.WriteAllBytes(filenameOutput, output);
        }
    }
    
    /// <summary>
    /// Conquer .dat Files Encrypt/Decrypt
    /// </summary>
    public class DatCrypto
    {
        byte[] key;
        public DatCrypto(EncryptionKey seed)
        {
            int fixedSeed = 0;
            int.TryParse(((int)seed).ToString(), NumberStyles.HexNumber, null, out fixedSeed);
            key = new byte[0x80];
            MSRandom r = new MSRandom(fixedSeed);
            for (int i = 0; i < key.Length; i++)
            {
                key[i] = (byte)(r.Next() % 0x100);
            }
        }
        public byte[] Decrypt(byte[] b)
        {
            for (int i = 0; i < b.Length; i++)
            {
                int num = b[i] ^ key[i % 0x80];
                int bits = i % 8;
                b[i] = (byte)((num << (8 - bits)) + (num >> bits));
            }
            return b;
        }
        public byte[] Encrypt(byte[] b)
        {
            for (int i = 0; i < b.Length; i++)
            {
                int bits = i % 8;
                int num = (byte)((b[i] >> (8 - bits)) + (b[i] << bits));
                b[i] = (byte)(num ^ key[i % 0x80]);
            }
            return b;
        }

        public enum EncryptionKey
        {
            Common = 2537,
            Alternative = 1234,
        }
    }


    /// <summary>
    /// Generate a Random Seed
    /// </summary>
    public class MSRandom
    {
        public long Seed;
        public MSRandom(int seed)
        {
            Seed = seed;
        }
        public int Next()
        {
            return (int)(((Seed = Seed * 214013L + 2531011L) >> 16) & 0x7fff);
        }
    }
}
