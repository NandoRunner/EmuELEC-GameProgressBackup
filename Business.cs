using FAndradeTI.Util.FileSystem;
using FAndradeTI.Util.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EmuELEC_GameProgressBackup
{
    public static class Business
    {
        public static int found;
        public static int foundDir;
        public readonly static string[] ignoreFolderList = { "downloaded_images", "logos", "images", "image", "downloaded_videos", "videos", "video" };

        public static List<string> fileList = new List<string>();
        public static List<string> dirList = new List<string>();

        public static void BackupFiles(string selectedNode, string path)
        {
            DateTime dt = DateTime.Now;

            StatusStripControl.InitStatusStrip(string.Empty, fileList.Count);

            //todo: criar caminhos relativos?
            var i = 1;

            var newLIst = fileList.ToList();

            if (!string.IsNullOrEmpty(selectedNode)) newLIst.RemoveAll(u => !u.Contains(selectedNode));

            newLIst.Sort();

            foreach (var file in newLIst)
            {
                StatusStripControl.UpdateProgressBar();
                StatusStripControl.UpdateLabel($"{i++}/{found} - {file}");
                FS.CopyFileIfNewer(file, path);
            }

            StatusStripControl.UpdateLabel($"{found} {UI.GetExtensionName()} files backup completed in {DateTime.Now.Subtract(dt).TotalSeconds.ToString("#.#")} seconds");

            found = 0;
            if (string.IsNullOrEmpty(selectedNode)) fileList.Clear();
        }

        #region Non-recursive approach
        public static void ListDirectory(TreeView treeView, string path)
        {
            DateTime dt = DateTime.Now;
            found = 0;
            foundDir = 0;
            fileList.Clear();
            dirList.Clear();

            try
            {
                var stack = new Stack<TreeNode>();
                var rootDirectory = new DirectoryInfo(path);
                var node = new TreeNode(rootDirectory.Name) { Tag = rootDirectory };
                stack.Push(node);

                StatusStripControl.UpdateLabel($"Preparing to read input folder ...");

                while (stack.Count > 0)
                {
                    var currentNode = stack.Pop();
                    var directoryInfo = (DirectoryInfo)currentNode.Tag;

                    var query = directoryInfo.GetFiles(UI.GetSearchPattern(), SearchOption.TopDirectoryOnly).OrderBy(file => file.Name);

                    found += query.Count();
                    StatusStripControl.UpdateLabel($"{found} {UI.GetExtensionName()} files found - Reading '{directoryInfo.FullName}' ...");


                    foreach (var directory in directoryInfo.GetDirectories().OrderBy(dir => dir.Name))
                    {
                        if (ignoreFolderList.Contains(directory.Name)) continue;

                        if (directory.GetFiles(UI.GetSearchPattern(), SearchOption.AllDirectories).Length == 0) continue;

                        var childDirectoryNode = new TreeNode(directory.Name) { Tag = directory };
                        currentNode.Nodes.Add(childDirectoryNode);
                        stack.Push(childDirectoryNode);
                    }

                    foreach (var file in query)
                    {
                        currentNode.Nodes.Add(file.DirectoryName, file.Name);
                        fileList.Add(file.FullName);

                        if (!dirList.Contains(file.DirectoryName))
                        {
                            dirList.Add(file.DirectoryName);
                            foundDir++;
                        }
                    }

                }

                treeView.SafeInvoke(c => c.Nodes.Add(node));

                StatusStripControl.UpdateLabel($"{found} {UI.GetExtensionName()} files found - Reading input completed in {DateTime.Now.Subtract(dt).TotalSeconds.ToString("###.#")} seconds");
            }
            catch (Exception ex)
            {
                StatusStripControl.UpdateLabel($"Error: {ex.Message} - Try again");
            }
        }
        #endregion

        public static void DeleteFiles(TreeView treeView, int recentFiles)
        {
            //todo: \\roms not working

            DateTime dt = DateTime.Now;

            StatusStripControl.InitStatusStrip(string.Empty, fileList.Count);

            dirList.Sort();

            var deletedFiles = 0;

            foreach (var dir in dirList)
            {
                var parentNode = treeView.SafeInvoke(c => c.Nodes.Find(dir, true));

                if (parentNode == null) continue;

                var dic = new Dictionary<string, int>();

                var myDir = new DirectoryInfo(dir);

                var list = myDir.GetFiles(UI.GetSearchPattern(), SearchOption.TopDirectoryOnly);

                var query = list.OrderByDescending(file => file.CreationTime);


                var i = 0;
                foreach (var file in query)
                {
                    i++;
                    var pureName = file.Name.Replace(file.Extension, string.Empty);

                    if (!dic.ContainsKey(pureName))
                    {
                        dic.Add(pureName, 1);
                        continue;
                    }

                    if (dic[pureName]++ >= recentFiles)
                    {
                        var result = parentNode.OfType<TreeNode>()
                            .FirstOrDefault(node => node.Text.Equals(file.Name));
                        treeView.SafeInvoke(c => c.Nodes.Remove(result));
                        fileList.Remove(file.FullName);
                        file.Delete();
                        deletedFiles++;
                    }
                }

                StatusStripControl.UpdateProgressBar();
                StatusStripControl.UpdateLabel($"{i++}/{foundDir} - {dir}");
            }

            StatusStripControl.UpdateLabel($"{deletedFiles} {UI.GetExtensionName()} files deleted in {DateTime.Now.Subtract(dt).TotalSeconds.ToString("#.#")} seconds");

        }

    }
}
