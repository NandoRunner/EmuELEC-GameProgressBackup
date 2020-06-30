using FAndradeTecInfo.Utils;
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
        public readonly static string[] ignoreFolderList = { "downloaded_images", "logos", "images", "image", "downloaded_videos", "videos", "video" };

        public static List<string> fileList = new List<string>();

        public static void BackupFiles(TreeView treeView, string path)
        {
            DateTime dt = DateTime.Now;

            //todo: criar destino se não existir?
            //todo: verificar se destino é local?
            //todo: criar caminhos relativos?
            var i = 1;

            fileList.Sort();

            foreach (var file in fileList)
            {
                UI.Showinfo($"{i++}/{found} - {file}");
                MyFS.CopyFile(file, path);
            }

            UI.Showinfo($"{found} {UI.GetExtensionName()} files Backup completed in {DateTime.Now.Subtract(dt).TotalSeconds.ToString("###.#")} seconds");

            found = 0;
            fileList.Clear();
        }

        #region Non-recursive approach
        public static void ListDirectory(TreeView treeView, string path)
        {
            DateTime dt = DateTime.Now;
            found = 0;
            fileList.Clear();

            var stack = new Stack<TreeNode>();
            var rootDirectory = new DirectoryInfo(path);
            var node = new TreeNode(rootDirectory.Name) { Tag = rootDirectory };
            stack.Push(node);

            UI.Showinfo($"Preparing to read input folder ...");


            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                var directoryInfo = (DirectoryInfo)currentNode.Tag;
                                
                foreach (var directory in directoryInfo.GetDirectories())
                {
                    UI.UpdateProgressBar();
                    if (ignoreFolderList.Contains(directory.Name)) continue;

                    UI.Showinfo($"{found} {UI.GetExtensionName()} files found - Reading '{directory.FullName}' ...");

                    if (directory.GetFiles(UI.GetSearchPattern(), SearchOption.AllDirectories).Length == 0) continue;

                    found += directory.GetFiles(UI.GetSearchPattern(), SearchOption.TopDirectoryOnly).Length;
                    
                    UI.Showinfo($"{found} {UI.GetExtensionName()} files found - Reading '{directory.FullName}' ...");

                    var childDirectoryNode = new TreeNode(directory.Name) { Tag = directory };
                    currentNode.Nodes.Add(childDirectoryNode);
                    stack.Push(childDirectoryNode);

                }
                foreach (var file in directoryInfo.GetFiles(UI.GetSearchPattern()))
                {
                    currentNode.Nodes.Add(new TreeNode(file.Name));
                    fileList.Add(file.FullName);
                }
            }

            treeView.SafeInvoke(c => c.Nodes.Add(node));
            
            UI.Showinfo($"{found} {UI.GetExtensionName()} files found - Reading input completed in {DateTime.Now.Subtract(dt).TotalSeconds.ToString("###.#")} seconds");
        }
        #endregion
    }
}
