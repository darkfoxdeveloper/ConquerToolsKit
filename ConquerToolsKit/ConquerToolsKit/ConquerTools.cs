using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace ConquerToolsKit
{
    /*
     * LevExp = 4D2
     * Silent = 2537
     * MapDestination = 2537
     * ItemType = 2537
     * Monster = 2537
     * UserHelpInfo = 2537
     */
    public class ConquerTools
    {
        public DatCrypto SelectedDatFile { get; set; }

        public void ItemtypeDecrypt(string filename, string filenameOutput)
        {
            DatCrypto dc = new DatCrypto(DatCrypto.EncryptionKey.COMMON);
            SelectedDatFile = dc;
            byte[] output = dc.Decrypt(File.ReadAllBytes(filename));
            File.WriteAllBytes(filenameOutput, output);
        }
        public void ItemtypeEncrypt(string filename, string filenameOutput)
        {
            DatCrypto dc = new DatCrypto(DatCrypto.EncryptionKey.COMMON);
            SelectedDatFile = dc;
            byte[] output = dc.Encrypt(File.ReadAllBytes(filename));
            File.WriteAllBytes(filenameOutput, output);
        }
        public void AutoDetectionEncrypt(string filename, string filenameOutput)
        {
            DatCrypto dc = new DatCrypto(filename);
            SelectedDatFile = dc;
            byte[] output = dc.Encrypt(File.ReadAllBytes(filename));
            File.WriteAllBytes(filenameOutput, output);
        }
        public void AutoDetectionDecrypt(string filename, string filenameOutput)
        {
            DatCrypto dc = new DatCrypto(filename);
            SelectedDatFile = dc;
            byte[] output = dc.Decrypt(File.ReadAllBytes(filename));
            File.WriteAllBytes(filenameOutput, output);
        }
        
        public void GenerateTable(string[] FileContent, DataGridView dgContent, DatCrypto datCrypto, bool RAWMode = false)
        {
            // TODO config of dat file type from a json
            DataTable dt = null;
            if (RAWMode)
            {
                dt = RAWTableFromStrings(FileContent);
            } else
            {
                switch (datCrypto.CurrentDatFileType)
                {
                    case DatCrypto.DatFileType.ITEMTYPE:
                    case DatCrypto.DatFileType.MAGICTYPE:
                        {
                            dt = TableFromStrings(FileContent, new char[] { '@', '@' });
                            break;
                        }
                    case DatCrypto.DatFileType.MAGICTYPEOP:
                        {
                            dt = TableFromStrings(FileContent, new char[] { ',' });
                            break;
                        }
                    case DatCrypto.DatFileType.MONSTER:
                        {
                            dt = TableFromStrings(FileContent, new char[] { ' ' });
                            break;
                        }
                    default:
                        {
                            dt = TableFromStrings(FileContent, new char[] { ' ' });
                            break;
                        }
                }
            }
            dgContent.DataSource = dt;
        }

        public DataTable TableFromStrings(string[] lines, char[] separator)
        {
            DataTable dt = new DataTable();

            int maxPar = 0;
            int maxParFails = 0;
            if (lines.Length > 0)
            {
                foreach (string currentLine in lines)
                {
                    int maxParTmp = currentLine.Split(separator, System.StringSplitOptions.RemoveEmptyEntries).Length;
                    if (maxParTmp > maxPar)
                    {
                        maxPar = maxParTmp;
                        maxParFails++;
                    }
                }
            }

            if (maxParFails > 2)
            {
                MessageBox.Show("This file may not be entirely correct", Assembly.GetCallingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            for (int i = 0; i < maxPar; i++)
            {
                dt.Columns.Add(new DataColumn("#" + i.ToString()));
            }
            
            foreach (string currentLine in lines)
            {
                dt.NewRow();
                string[] lineSplit = currentLine.Split(separator, System.StringSplitOptions.RemoveEmptyEntries);
                List<string> lineColumns = new List<string>();
                foreach (string line in lineSplit)
                {
                    lineColumns.Add(line);
                }
                dt.Rows.Add(lineColumns.ToArray());
            }
            return dt;
        }
        public DataTable RAWTableFromStrings(string[] lines)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("RAW"));

            foreach (string currentLine in lines)
            {
                dt.NewRow();
                dt.Rows.Add(currentLine);
            }
            return dt;
        }
    }
    
    /// <summary>
    /// Conquer .dat Files Encrypt/Decrypt
    /// </summary>
    public class DatCrypto
    {
        byte[] key;
        public EncryptionKey CurrentEncryptionKey { get; set; }
        public DatFileType CurrentDatFileType { get; set; }
        public string Filename { get; set; }
        /// <summary>
        /// Auto Detect key based in filename
        /// </summary>
        public DatCrypto(string filename)
        {
            Filename = filename;
            FindEncryptionKeyByFilename();
            Init(CurrentEncryptionKey);
        }
        public DatCrypto(EncryptionKey seed)
        {
            Init(seed);
        }
        public void Init(EncryptionKey seed)
        {
            CurrentEncryptionKey = seed;
            int fixedSeed = 0;
            if (CurrentEncryptionKey == EncryptionKey.AUTODETECT)
            {
                int.TryParse(CurrentEncryptionKey.ToString(), NumberStyles.HexNumber, null, out fixedSeed);
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

        private void FindEncryptionKeyByFilename()
        {
            CurrentDatFileType = DatFileType.AUTODETECT;
            // TODO key of dat file from a json
            string name = Path.GetFileNameWithoutExtension(Filename);
            switch (name)
            {
                case "itemtype":
                    {
                        CurrentEncryptionKey = EncryptionKey.COMMON;
                        CurrentDatFileType = DatFileType.ITEMTYPE;
                        break;
                    }
                case "monster":
                    {
                        CurrentEncryptionKey = EncryptionKey.COMMON;
                        CurrentDatFileType = DatFileType.MONSTER;
                        break;
                    }
                case "MagicType":
                    {
                        CurrentEncryptionKey = EncryptionKey.COMMON;
                        CurrentDatFileType = DatFileType.MAGICTYPE;
                        break;
                    }
                case "magictypeop":
                    {
                        CurrentEncryptionKey = EncryptionKey.COMMON;
                        CurrentDatFileType = DatFileType.MAGICTYPEOP;
                        break;
                    }
            }
        }

        public enum EncryptionKey
        {
            AUTODETECT = 0,
            COMMON = 2537,
            ALTERNATIVE = 1234,
        }

        public enum DatFileType
        {
            AUTODETECT = 0,
            ITEMTYPE,
            MONSTER,
            MAGICTYPE,
            MAGICTYPEOP,
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
