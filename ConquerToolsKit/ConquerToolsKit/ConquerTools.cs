using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
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
        public ConquerDatFile SelectedDatFile { get; set; }
        public ConquerToolsConfig CurrentConfig { get; set; }

        public ConquerTools()
        {
            Init();
        }

        public ConquerTools(ConquerDatFile selectedDatFile)
        {
            SelectedDatFile = selectedDatFile;
            Init();
        }

        public void Init()
        {
            string FileConfigName = "ctk.config.json";
            if (!File.Exists(FileConfigName))
            {
                Dictionary<ConquerDatFile.DatFileType, DatFileConfig> d = new Dictionary<ConquerDatFile.DatFileType, DatFileConfig>
                {
                    {
                        ConquerDatFile.DatFileType.ITEMTYPE,
                        new DatFileConfig()
                        {
                            FileType = ConquerDatFile.DatFileType.ITEMTYPE,
                            EncryptionKey = ConquerDatFile.EncryptionKey.COMMON,
                            Separators = new char[] { '@', '@' }
                        }
                    },
                    {
                        ConquerDatFile.DatFileType.MAGICTYPE,
                        new DatFileConfig()
                        {
                            FileType = ConquerDatFile.DatFileType.MAGICTYPE,
                            EncryptionKey = ConquerDatFile.EncryptionKey.COMMON,
                            Separators = new char[] { '@', '@' }
                        }
                    },
                    {
                        ConquerDatFile.DatFileType.MAGICTYPEOP,
                        new DatFileConfig()
                        {
                            FileType = ConquerDatFile.DatFileType.MAGICTYPEOP,
                            EncryptionKey = ConquerDatFile.EncryptionKey.COMMON,
                            Separators = new char[] { ',' }
                        }
                    },
                    {
                        ConquerDatFile.DatFileType.MONSTER,
                        new DatFileConfig()
                        {
                            FileType = ConquerDatFile.DatFileType.MONSTER,
                            EncryptionKey = ConquerDatFile.EncryptionKey.COMMON,
                            Separators = null
                        }
                    }
                };

                ConquerToolsConfig c = new ConquerToolsConfig(d);
                File.WriteAllText(FileConfigName, JsonConvert.SerializeObject(c));
                CurrentConfig = c;
            } else
            {
                CurrentConfig = JsonConvert.DeserializeObject<ConquerToolsConfig>(File.ReadAllText(FileConfigName));
            }
        }

        public void ItemtypeEncrypt(string filename, string filenameOutput)
        {
            ConquerDatFile dc = new ConquerDatFile(ConquerDatFile.DatFileType.ITEMTYPE);
            SelectedDatFile = dc;
            byte[] output = dc.Encrypt(File.ReadAllBytes(filename));
            File.WriteAllBytes(filenameOutput, output);
        }

        public void ItemtypeDecrypt(string filename, string filenameOutput)
        {
            ConquerDatFile dc = new ConquerDatFile(ConquerDatFile.DatFileType.ITEMTYPE);
            SelectedDatFile = dc;
            byte[] output = dc.Decrypt(File.ReadAllBytes(filename));
            File.WriteAllBytes(filenameOutput, output);
        }

        public void AutoDetectionEncrypt(string filename, string filenameOutput)
        {
            ConquerDatFile dc = new ConquerDatFile(filename);
            SelectedDatFile = dc;
            byte[] output = dc.Encrypt(File.ReadAllBytes(filename));
            File.WriteAllBytes(filenameOutput, output);
        }

        public void AutoDetectionDecrypt(string filename, string filenameOutput)
        {
            ConquerDatFile dc = new ConquerDatFile(filename);
            SelectedDatFile = dc;
            byte[] output = dc.Decrypt(File.ReadAllBytes(filename));
            File.WriteAllBytes(filenameOutput, output);
        }

        public void CustomEncrypt(string filename, string filenameOutput, ConquerDatFile.DatFileType datFileType)
        {
            ConquerDatFile dc = new ConquerDatFile(datFileType);
            SelectedDatFile = dc;
            byte[] output = dc.Encrypt(File.ReadAllBytes(filename));
            File.WriteAllBytes(filenameOutput, output);
        }

        public void CustomDecrypt(string filename, string filenameOutput, ConquerDatFile.DatFileType datFileType)
        {
            ConquerDatFile dc = new ConquerDatFile(datFileType);
            SelectedDatFile = dc;
            byte[] output = dc.Decrypt(File.ReadAllBytes(filename));
            File.WriteAllBytes(filenameOutput, output);
        }

        public void GenerateTable(string[] FileContent, DataGridView dgContent, ConquerDatFile datCrypto, bool RAWMode = false)
        {
            DataTable dt = null;
            if (RAWMode)
            {
                dt = RAWTableFromStrings(FileContent);
            } else
            {
                dt = TableFromStrings(FileContent, ConquerToolsHelper.CTools.CurrentConfig.DatFilesConfig.Where(x => x.Key == datCrypto.CurrentDatFileType).FirstOrDefault().Value.Separators);
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
    public class ConquerDatFile
    {
        byte[] key;
        public EncryptionKey CurrentEncryptionKey { get; set; }
        public DatFileType CurrentDatFileType { get; set; }
        public string Filename { get; set; }
        /// <summary>
        /// Auto Detect key based in filename
        /// </summary>
        public ConquerDatFile(string filename)
        {
            Filename = filename;
            FindDatTypeByFilename();
            FindEncryptionKeyByFileType();
            Init();
        }
        public ConquerDatFile(DatFileType datFileType)
        {
            CurrentDatFileType = datFileType;
            FindEncryptionKeyByFileType();
            Init();
        }
        public void Init()
        {
            string seed = Enum.Format(typeof(EncryptionKey), CurrentEncryptionKey, "d");
            int.TryParse(seed, NumberStyles.HexNumber, null, out int fixedSeed);
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

        private void FindDatTypeByFilename()
        {
            string name = Path.GetFileNameWithoutExtension(Filename).ToLower();
            switch (name)
            {
                case "itemtype":
                    {
                        CurrentDatFileType = DatFileType.ITEMTYPE;
                        break;
                    }
                case "monster":
                    {
                        CurrentDatFileType = DatFileType.MONSTER;
                        break;
                    }
                case "magictype":
                    {
                        CurrentDatFileType = DatFileType.MAGICTYPE;
                        break;
                    }
                case "magictypeop":
                    {
                        CurrentDatFileType = DatFileType.MAGICTYPEOP;
                        break;
                    }
            }
        }

        private void FindEncryptionKeyByFileType()
        {
            CurrentEncryptionKey = ConquerToolsHelper.CTools.CurrentConfig.DatFilesConfig.Where(x => x.Key == CurrentDatFileType).FirstOrDefault().Value.EncryptionKey;
        }

        public enum EncryptionKey
        {
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

    public class ConquerToolsConfig
    {
        public Dictionary<ConquerDatFile.DatFileType, DatFileConfig> DatFilesConfig { get; set; }

        public DateTime CreationDate = DateTime.Now;

        public ConquerToolsConfig(Dictionary<ConquerDatFile.DatFileType, DatFileConfig> datFilesConfig)
        {
            DatFilesConfig = datFilesConfig;
        }
    }

    public class DatFileConfig
    {
        public ConquerDatFile.DatFileType FileType { get; set; }
        public ConquerDatFile.EncryptionKey EncryptionKey { get; set; }
        public char[] Separators { get; set; }
    }

    public static class ConquerToolsHelper {
        public static ConquerTools CTools;
    }
}
