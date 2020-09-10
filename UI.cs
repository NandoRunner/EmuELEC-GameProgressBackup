using EmuELEC_GameProgressBackup.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EmuELEC_GameProgressBackup
{
    public static class UI
    {
        public static FileExtensions selectedExtension = FileExtensions.State;

        public static readonly Dictionary<FileExtensions, string> searchPatternDic = new Dictionary<FileExtensions, string>() { { FileExtensions.State, "*.state????" },
            { FileExtensions.Srm, "*.srm" },
            { FileExtensions.All, string.Empty }};


        public static string GetSearchPattern()
        {
            return searchPatternDic[selectedExtension];
        }

        public static string GetExtensionName()
        {
            return selectedExtension == FileExtensions.All ? string.Empty : searchPatternDic[selectedExtension].Substring(1);
        }




    }
}
