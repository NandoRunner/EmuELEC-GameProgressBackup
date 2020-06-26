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

        #region Non-recursive approach
        public static void ListDirectory(TreeView treeView, string path)
        {
            DateTime dt = DateTime.Now;
            found = 0;
            treeView.Nodes.Clear();

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
                }
            }

            treeView.SafeInvoke(c => c.Nodes.Add(node));
            
            UI.Showinfo($"{found} {UI.GetExtensionName()} files found - Reading input completed in {DateTime.Now.Subtract(dt).TotalSeconds.ToString("###.#")} seconds");
        }
        #endregion
    }
}
