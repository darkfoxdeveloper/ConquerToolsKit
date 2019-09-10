using System.Globalization;
using System.IO;

namespace ConquerToolsKit
{
    /*
     * LevExp = 4D2
     * Silent = 2537
     * MapDestination = 2537
     * ItemType = 2537
     * Monster = 2537
     * UserHelpInfo = 2537
     * 
     * 
     */
    public class ConquerTools
    {
        public string SelectedFile { get; set; }
        public void ItemtypeDecrypt(string filename, string filenameOutput)
        {
            DatCrypto dc = new DatCrypto(DatCrypto.EncryptionKey.COMMON);
            byte[] output = dc.Decrypt(File.ReadAllBytes(filename));
            File.WriteAllBytes(filenameOutput, output);
        }
        public void ItemtypeEncrypt(string filename, string filenameOutput)
        {
            DatCrypto dc = new DatCrypto(DatCrypto.EncryptionKey.COMMON);
            byte[] output = dc.Encrypt(File.ReadAllBytes(filename));
            File.WriteAllBytes(filenameOutput, output);
        }
        public void AutoDetectionEncrypt(string filename, string filenameOutput)
        {
            DatCrypto dc = new DatCrypto(filename);
            byte[] output = dc.Encrypt(File.ReadAllBytes(filename));
            File.WriteAllBytes(filenameOutput, output);
        }
        public void AutoDetectionDecrypt(string filename, string filenameOutput)
        {
            DatCrypto dc = new DatCrypto(filename);
            byte[] output = dc.Decrypt(File.ReadAllBytes(filename));
            File.WriteAllBytes(filenameOutput, output);
        }
    }
    
    /// <summary>
    /// Conquer .dat Files Encrypt/Decrypt
    /// </summary>
    public class DatCrypto
    {
        byte[] key;
        public EncryptionKey DetectedEncryptionKey { get; set; }
        /// <summary>
        /// Auto Detect key based in filename
        /// </summary>
        public DatCrypto(string filename)
        {
            DetectedEncryptionKey = FindEncryptionKeyByFilename(filename);
            Init(DetectedEncryptionKey);
        }
        public DatCrypto(EncryptionKey seed)
        {
            Init(seed);
        }
        public void Init(EncryptionKey seed)
        {
            int fixedSeed = 0;
            if (seed == EncryptionKey.AUTO)
            {
                int.TryParse((FindEncryptionKeyByFilename("")).ToString(), NumberStyles.HexNumber, null, out fixedSeed);
            }
            else
            {
                int.TryParse(((int)seed).ToString(), NumberStyles.HexNumber, null, out fixedSeed);
            }
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

        private EncryptionKey FindEncryptionKeyByFilename(string filename)
        {
            EncryptionKey _encryptionKey = EncryptionKey.AUTO;
            string name = Path.GetFileNameWithoutExtension(filename);
            switch (name)
            {
                case "levexp":
                    {
                        _encryptionKey = EncryptionKey.ALTERNATIVE;
                        break;
                    }
                case "MapDestination":
                case "silent":
                case "itemtype":
                case "Monster":
                case "UserHelpInfo":
                    {
                        _encryptionKey = EncryptionKey.COMMON;
                        break;
                    }
            }
            return _encryptionKey;
        }

        public enum EncryptionKey
        {
            AUTO = 0,
            COMMON = 2537,
            ALTERNATIVE = 1234,
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
