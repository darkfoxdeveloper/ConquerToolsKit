using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static ConquerToolsKit.ConquerDatFile;

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
                Dictionary<DatFileType, DatFileConfig> d = new Dictionary<DatFileType, DatFileConfig>
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

        public void SaveDat()
        {
            switch (ConquerToolsHelper.CTools.SelectedDatFile.CurrentDatFileType)
            {
                case DatFileType.ITEMTYPE:
                case DatFileType.MAGICTYPE:
                case DatFileType.MAGICTYPEOP:
                    {
                        ConquerToolsHelper.CTools.SelectedDatFile.Save();
                        break;
                    }
            }
        }

        public void AutoDetectionDecrypt(string filename, string filenameOutput)
        {
            ConquerDatFile dc = new ConquerDatFile(filename);
            SelectedDatFile = dc;
            DatFileConfig datFileConfig = ConquerToolsHelper.CTools.CurrentConfig.DatFilesConfig.Where(x => x.Key == dc.CurrentDatFileType).FirstOrDefault().Value;
            if (datFileConfig != null)
            {
                dc.Open();
            }
        }

        public void CustomDecrypt(string filename, string filenameOutput, ConquerDatFile.DatFileType datFileType)
        {
            ConquerDatFile dc = new ConquerDatFile(filename, datFileType);
            SelectedDatFile = dc;
            DatFileConfig datFileConfig = ConquerToolsHelper.CTools.CurrentConfig.DatFilesConfig.Where(x => x.Key == dc.CurrentDatFileType).FirstOrDefault().Value;
            if (datFileConfig != null)
            {
                dc.Open();
            }
        }

        public void GenerateTable(DataGridView dgContent, bool RAWMode = false)
        {
            DataTable dt = null;
            if (RAWMode)
            {
                dt = RAWTableFromStrings(ConquerToolsHelper.CTools.SelectedDatFile.CurrentRAWFileContent);
                dgContent.ReadOnly = true;
            }
            else
            {
                dt = TableFromDatFileLine();
                dgContent.ReadOnly = false;
            }
            dgContent.DataSource = dt;
        }

        public DataTable TableFromDatFileLine()
        {
            DataTable dt = new DataTable();
            
            foreach (DatFileLine currentLine in ConquerToolsHelper.CTools.SelectedDatFile.CurrentFileContent.Values)
            {
                if (currentLine.LineAttribute.Keys.Count > dt.Columns.Count)
                {
                    foreach(string kHeader in currentLine.LineAttribute.Keys)
                    {
                        if (!dt.Columns.Contains(kHeader))
                        {
                            dt.Columns.Add(new DataColumn(kHeader));
                        }
                    }
                }
                dt.NewRow();
                dt.Rows.Add(currentLine.LineAttribute.Values.ToArray());
            }
            return dt;
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
