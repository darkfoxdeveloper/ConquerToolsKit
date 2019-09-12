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
     * Some keys used in this App for decrypt dat files. Program created by DaRkFox
     * --------------
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
                            Separators = new char[] { '@', '@' },
                            FileHeaders = new string[] {
                                "ID",
                                "Name",
                                "RequiredProfession",
                                "Level",
                                "RequiredLevel",
                                "RequiredSex",
                                "RequiredForce",
                                "RequiredDexterity",
                                "RequiredHealth",
                                "RequiredSoul",
                            }
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
                    },
                    {
                        ConquerDatFile.DatFileType.LEVELEXP,
                        new DatFileConfig()
                        {
                            FileType = ConquerDatFile.DatFileType.LEVELEXP,
                            EncryptionKey = ConquerDatFile.EncryptionKey.CUSTOM,
                            Separators = new char[] { ',' }
                        }
                    },
                    {
                        ConquerDatFile.DatFileType.LEVEXP,
                        new DatFileConfig()
                        {
                            FileType = ConquerDatFile.DatFileType.LEVEXP,
                            EncryptionKey = ConquerDatFile.EncryptionKey.ALTERNATIVE,
                            Separators = null
                        }
                    },
                    {
                        ConquerDatFile.DatFileType.AUTOLOOT,
                        new DatFileConfig()
                        {
                            FileType = ConquerDatFile.DatFileType.AUTOLOOT,
                            EncryptionKey = ConquerDatFile.EncryptionKey.COMMON,
                            Separators = null
                        }
                    },
                    {
                        ConquerDatFile.DatFileType.MAPDESTINATION,
                        new DatFileConfig()
                        {
                            FileType = ConquerDatFile.DatFileType.MAPDESTINATION,
                            EncryptionKey = ConquerDatFile.EncryptionKey.COMMON,
                            Separators = null
                        }
                    }
                };
                ConquerToolsConfig c = new ConquerToolsConfig(d);
                File.WriteAllText(FileConfigName, JsonConvert.SerializeObject(c, Formatting.Indented));
                CurrentConfig = c;
            } else
            {
                CurrentConfig = JsonConvert.DeserializeObject<ConquerToolsConfig>(File.ReadAllText(FileConfigName));
            }
        }

        public void AutoDetectionEncrypt(string filename, string filenameOutput)
        {
            ConquerDatFile dc = new ConquerDatFile(filename);
            SelectedDatFile = dc;
            switch(dc.CurrentDatFileType)
            {
                case ConquerDatFile.DatFileType.ITEMTYPE:
                    {
                        break;
                    }
            }
            //byte[] output = dc.Encrypt(File.ReadAllBytes(filename));
            //File.WriteAllBytes(filenameOutput, output);
        }

        public void AutoDetectionDecrypt(string filename, string filenameOutput)
        {
            ConquerDatFile dc = new ConquerDatFile(filename);
            SelectedDatFile = dc;
            DatFileConfig datFileConfig = ConquerToolsHelper.CTools.CurrentConfig.DatFilesConfig.Where(x => x.Key == dc.CurrentDatFileType).FirstOrDefault().Value;
            if (datFileConfig != null)
            {
                if (datFileConfig.FileType == ConquerDatFile.DatFileType.LEVELEXP)
                {
                    CO2_CORE_DLL.IO.LevelExp le = new CO2_CORE_DLL.IO.LevelExp();
                    le.LoadFromDat(filename);
                    le.SaveToTxt(filenameOutput);
                } else
                {
                    if (datFileConfig.FileType == ConquerDatFile.DatFileType.AUTOLOOT)
                    {
                        CO2_CORE_DLL.IO.AutoAllot aLoot = new CO2_CORE_DLL.IO.AutoAllot();
                        aLoot.LoadFromDat(filename);
                        aLoot.SaveToTxt(filenameOutput);
                    } else
                    {
                        dc.Open();
                        //byte[] output = dc.Decrypt(File.ReadAllBytes(filename));
                        //File.WriteAllBytes(filenameOutput, output);
                    }
                }
            }
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
            DatFileConfig datFileConfig = ConquerToolsHelper.CTools.CurrentConfig.DatFilesConfig.Where(x => x.Key == dc.CurrentDatFileType).FirstOrDefault().Value;
            if (datFileConfig != null)
            {
                if (datFileConfig.FileType == ConquerDatFile.DatFileType.LEVELEXP)
                {
                    CO2_CORE_DLL.IO.LevelExp le = new CO2_CORE_DLL.IO.LevelExp();
                    le.LoadFromDat(filename);
                    le.SaveToTxt(filenameOutput);
                }
                else
                {
                    if (datFileConfig.FileType == ConquerDatFile.DatFileType.AUTOLOOT)
                    {
                        CO2_CORE_DLL.IO.AutoAllot aLoot = new CO2_CORE_DLL.IO.AutoAllot();
                        aLoot.LoadFromDat(filename);
                        aLoot.SaveToTxt(filenameOutput);
                    } else
                    {
                        byte[] output = dc.Decrypt(File.ReadAllBytes(filename));
                        File.WriteAllBytes(filenameOutput, output);
                    }
                }
            }
        }

        public void GenerateTable(string[] FileContent, DataGridView dgContent, ConquerDatFile datCrypto, bool RAWMode = false)
        {
            DataTable dt = null;
            DatFileConfig datFileConfig = ConquerToolsHelper.CTools.CurrentConfig.DatFilesConfig.Where(x => x.Key == datCrypto.CurrentDatFileType).FirstOrDefault().Value;
            if (RAWMode)
            {
                dt = RAWTableFromStrings(FileContent);
            } else
            {
                dt = TableFromStrings(FileContent, datFileConfig);
            }
            dgContent.DataSource = dt;
        }

        public DataTable TableFromStrings(string[] lines, DatFileConfig datFileConfig)
        {
            DataTable dt = new DataTable();

            int maxPar = 0;
            int maxParFails = 0;
            if (lines.Length > 0)
            {
                foreach (string currentLine in lines)
                {
                    int maxParTmp = currentLine.Split(datFileConfig.Separators, StringSplitOptions.RemoveEmptyEntries).Length;
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
                if (datFileConfig.FileHeaders != null && datFileConfig.FileHeaders.Length > i)
                {
                    dt.Columns.Add(new DataColumn(datFileConfig.FileHeaders[i]));
                }
                else
                {
                    dt.Columns.Add(new DataColumn("#" + i.ToString()));
                }
            }

            foreach (string currentLine in lines)
            {
                dt.NewRow();
                string[] lineSplit = currentLine.Split(datFileConfig.Separators, StringSplitOptions.RemoveEmptyEntries);
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
        public List<string> CurrentFileContent { get; set; }
        public string Filename { get; set; }
        /// <summary>
        /// Auto Detect key based in filename
        /// </summary>
        public ConquerDatFile(string filename)
        {
            Filename = filename;
            FindDatTypeByFilename();
            if (FindEncryptionKeyByFileType())
            {
                Init();
            }
        }

        public ConquerDatFile(DatFileType datFileType)
        {
            CurrentDatFileType = datFileType;
            if (FindEncryptionKeyByFileType())
            {
                Init();
            }
        }

        public void Init()
        {
            string k = CurrentEncryptionKey.ToString("d");
            uint.TryParse(k, NumberStyles.Number, null, out uint fixedSeed);
            key = new byte[0x80];
            CO2_CORE_DLL.MSRandom r = new CO2_CORE_DLL.MSRandom(fixedSeed);
            for (int i = 0; i < key.Length; i++)
            {
                key[i] = (byte)(r.Next() % 0x100);
            }
        }

        public void Open()
        {
            // TODO create a object with all data of dat file
            switch (CurrentDatFileType)
            {
                case DatFileType.ITEMTYPE:
                    {
                        break;
                    }
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
        
        /// <summary>
        /// Find the current dat type using the current filename
        /// </summary>
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
                case "levelexp":
                    {
                        CurrentDatFileType = DatFileType.LEVELEXP;
                        break;
                    }
                case "levexp":
                    {
                        CurrentDatFileType = DatFileType.LEVEXP;
                        break;
                    }
                case "autoallot":
                    {
                        CurrentDatFileType = DatFileType.AUTOLOOT;
                        break;
                    }
                case "mapdestination":
                    {
                        CurrentDatFileType = DatFileType.MAPDESTINATION;
                        break;
                    }
            }
        }
        
        /// <summary>
        /// Find the current encryption key for current filetype
        /// </summary>
        private bool FindEncryptionKeyByFileType()
        {
            bool Success = false;
            KeyValuePair<DatFileType, DatFileConfig> kvp = ConquerToolsHelper.CTools.CurrentConfig.DatFilesConfig.Where(x => x.Key == CurrentDatFileType).FirstOrDefault();
            if (kvp.Key != DatFileType.AUTODETECT)
            {
                CurrentEncryptionKey = kvp.Value.EncryptionKey;
                Success = true;
            } else
            {
                MessageBox.Show("Sorry, this file is not compatible with this software.", Assembly.GetCallingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return Success;
        }
        
        /// <summary>
        /// Available encryption keys for dat files. In INTEGER format.
        /// </summary>
        public enum EncryptionKey
        {
            CUSTOM = 0,
            COMMON = 9527,
            ALTERNATIVE = 1234,
        }

        /// <summary>
        /// Types of dat file
        /// </summary>
        public enum DatFileType
        {
            AUTODETECT = 0,
            ITEMTYPE,
            MONSTER,
            MAGICTYPE,
            MAGICTYPEOP,
            LEVELEXP,
            LEVEXP,
            AUTOLOOT,
            MAPDESTINATION,
        }
    }


    /// <summary>
    /// App Configuration
    /// </summary>
    public class ConquerToolsConfig
    {
        public Dictionary<ConquerDatFile.DatFileType, DatFileConfig> DatFilesConfig { get; set; }

        public DateTime CreationDate = DateTime.Now;

        public ConquerToolsConfig(Dictionary<ConquerDatFile.DatFileType, DatFileConfig> datFilesConfig)
        {
            DatFilesConfig = datFilesConfig;
        }
    }

    /// <summary>
    /// Configuration for Dat File
    /// </summary>
    public class DatFileConfig
    {
        public ConquerDatFile.DatFileType FileType { get; set; }
        public ConquerDatFile.EncryptionKey EncryptionKey { get; set; }
        public char[] Separators { get; set; }
        public string[] FileHeaders { get; set; }
    }

    /// <summary>
    /// Helper for call other classes
    /// </summary>
    public static class ConquerToolsHelper {
        public static ConquerTools CTools;
    }
}
