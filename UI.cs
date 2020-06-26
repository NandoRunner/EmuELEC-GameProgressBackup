using EmuELEC_GameProgressBackup.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EmuELEC_GameProgressBackup
{
    public static class UI
    {
        public static FileExtensions selectedExtension = FileExtensions.State;

        private static readonly Dictionary<FileExtensions, string> searchPatternDic = new Dictionary<FileExtensions, string>() { { FileExtensions.State, "*.state?" },
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

        public static void Showinfo(string info)
        {
            ((ToolStripStatusLabel)((StatusStrip)Application.OpenForms[0].Controls["statusStrip1"]).Items["tsInfo"]).Text = info;
        }

        public static void InitProgressBar(int max)
        {
            Application.OpenForms[0].BeginInvoke((Action)(() => ((ToolStripProgressBar)((StatusStrip)Application.OpenForms[0].Controls["statusStrip1"]).Items["tsProgressBar"]).Maximum += max));

        }

        public static void UpdateProgressBar(int value = 1)
        {
            if (((ToolStripProgressBar)((StatusStrip)Application.OpenForms[0].Controls["statusStrip1"]).Items["tsProgressBar"]).Maximum == ((ToolStripProgressBar)((StatusStrip)Application.OpenForms[0].Controls["statusStrip1"]).Items["tsProgressBar"]).Value)
                return;

            Application.OpenForms[0].BeginInvoke((Action)(() => ((ToolStripProgressBar)((StatusStrip)Application.OpenForms[0].Controls["statusStrip1"]).Items["tsProgressBar"]).Value += value));
        }
    }
}
