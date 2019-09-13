using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ConquerToolsKit
{


    /// <summary>
    /// Conquer .dat Files Encrypt/Decrypt
    /// </summary>
    public class ConquerDatFile
    {
        byte[] key;
        public EncryptionKey CurrentEncryptionKey { get; set; }
        public DatFileType CurrentDatFileType { get; set; }
        public Dictionary<uint, DatFileLine> CurrentFileContent { get; set; }
        public string[] CurrentRAWFileContent { get; set; }
        public string CurrentFilename { get; set; }

        /// <summary>
        /// Auto Detect key based in filename
        /// </summary>
        public ConquerDatFile(string filename)
        {
            CurrentFilename = filename;
            FindDatTypeByFilename();
            if (FindEncryptionKeyByFileType())
            {
                Init();
            }
        }

        public ConquerDatFile(string filename, DatFileType datFileType)
        {
            CurrentFilename = filename;
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
            CurrentFileContent = new Dictionary<uint, DatFileLine>();
        }

        public DatFileConfig GetCurrentConfig()
        {
            DatFileConfig datFileConfig = ConquerToolsHelper.CTools.CurrentConfig.DatFilesConfig.Where(x => x.Key == CurrentDatFileType).FirstOrDefault().Value;
            return datFileConfig;
        }

        /// <summary>
        /// Open the file and get content
        /// </summary>
        public bool Open()
        {
            bool success = false;
            switch (CurrentDatFileType)
            {
                case DatFileType.ITEMTYPE:
                case DatFileType.MAGICTYPE:
                case DatFileType.MAGICTYPEOP:
                    {
                        byte[] content = File.ReadAllBytes(CurrentFilename);
                        string oneBigString = Encoding.ASCII.GetString(Decrypt(content));
                        string[] contentLines = oneBigString.Split('\n');
                        CurrentRAWFileContent = contentLines;

                        int maxPar = 0;
                        foreach (string currentLine in contentLines)
                        {
                            int maxParTmp = currentLine.Split(GetCurrentConfig().Separators, StringSplitOptions.RemoveEmptyEntries).Length;
                            if (maxParTmp > maxPar)
                            {
                                maxPar = maxParTmp;
                            }
                        }

                        uint nLine = 0;
                        foreach (string currentLine in contentLines)
                        {
                            int n = 0;
                            string[] lineSplit = currentLine.Split(GetCurrentConfig().Separators, StringSplitOptions.RemoveEmptyEntries);
                            DatFileLine dfline = new DatFileLine();
                            foreach (string attr in lineSplit)
                            {
                                if (!attr.Equals("\r"))
                                {
                                    string header = "#" + n;
                                    if (GetCurrentConfig().FileHeaders != null && GetCurrentConfig().FileHeaders.Length > n)
                                    {
                                        header = GetCurrentConfig().FileHeaders[n];
                                    }
                                    dfline.Add(header, attr);
                                    n++;
                                }
                            }
                            CurrentFileContent.Add(nLine, dfline);
                            nLine++;
                        }
                        if (contentLines.Length > 0) success = true;
                        break;
                    }
                case DatFileType.AUTOLOOT:
                    {
                        CO2_CORE_DLL.IO.AutoAllot aLoot = new CO2_CORE_DLL.IO.AutoAllot();
                        aLoot.LoadFromDat(CurrentFilename);
                        // TODO Finish
                        break;
                    }
                case DatFileType.LEVELEXP:
                    {
                        CO2_CORE_DLL.IO.LevelExp le = new CO2_CORE_DLL.IO.LevelExp();
                        le.LoadFromDat(CurrentFilename);
                        // TODO Finish
                        break;
                    }
            }
            return success;
        }

        /// <summary>
        /// Save the file
        /// </summary>
        public void Save()
        {
            // Force .dat extension output
            string outputFilename = Path.ChangeExtension(CurrentFilename, "dat");
            // Encrypt the content
            MemoryStream stream = new MemoryStream();
            foreach (string str in CurrentRAWFileContent)
            {
                stream.Write(Encoding.ASCII.GetBytes(str), 0, Encoding.ASCII.GetBytes(str).Length);
                stream.Write(Encoding.ASCII.GetBytes("\n"), 0, Encoding.ASCII.GetBytes("\n").Length);
            }
            // Save to file
            File.WriteAllBytes(outputFilename, Encrypt(stream.ToArray()));
            // Log Info: Original Size of file is changed. Any error on process?¿
        }

        private byte[] Decrypt(byte[] b)
        {
            for (int i = 0; i < b.Length; i++)
            {
                int num = b[i] ^ key[i % 0x80];
                int bits = i % 8;
                b[i] = (byte)((num << (8 - bits)) + (num >> bits));
            }
            return b;
        }

        private byte[] Encrypt(byte[] b)
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
            string name = Path.GetFileNameWithoutExtension(CurrentFilename).ToLower();
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
            }
            else
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

    public class DatFileLine {
        public DatFileLine()
        {
            LineAttribute = new Dictionary<string, string>();
        }
        
        public Dictionary<string, string> LineAttribute { get; set; }

        public void Add(string header, string attribute)
        {
            LineAttribute.Add(header, attribute);
        }
    }
}
