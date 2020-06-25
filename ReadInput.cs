using EmuELEC_GameProgressBackup.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmuELEC_GameProgressBackup
{
    public enum myExtensions
    {
        state,
        srm,
        all
    }

    public static class ReadInput
    {
        public static TreeNode tn;
        public static myExtensions ext = myExtensions.state;
        public static int found;
        public static string[] path_nocheck = { "downloaded_images", "logos", "images", "image", "downloaded_videos", "videos", "video" };

        #region Recursive approach
        public static async Task ListDirectory(object path)
        {
            DateTime dt = DateTime.Now;
            found = 0;

            var rootDirectoryInfo = new DirectoryInfo((string)path);
            tn = CreateDirectoryNode(rootDirectoryInfo);
            Showinfo($"{found} files found - Reading Input completed in {DateTime.Now.Subtract(dt).TotalSeconds} seconds");
        }

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);

            Showinfo($"{found} files found - Reading '{directoryInfo.FullName}' ...");

            foreach (var directory in directoryInfo.GetDirectories())
            {
                if (directory.Name == "downloaded_images" || directory.Name == "downloaded_videos" || directory.Name == "images" || directory.Name == "image" || directory.Name == "video" || directory.Name == "videos")
                    continue;

                var aux = CreateDirectoryNode(directory);
                if (aux.Nodes.Count != 0)
                {
                    directoryNode.Nodes.Add(CreateDirectoryNode(directory));
                }
            }

            dynamic files;

            if (ext == myExtensions.srm || ext == myExtensions.all)
            {
                files = directoryInfo.GetFiles("*.srm", SearchOption.TopDirectoryOnly);
                foreach (var file in files)
                {
                    found++;
                    Showinfo($"{found} files found - Reading '{directoryInfo.FullName}' ...");
                    Console.WriteLine($"{found}: {file.FullName}");
                    directoryNode.Nodes.Add(new TreeNode(file.Name));
                }
            }
            if (ext == myExtensions.state || ext == myExtensions.all)
            {
                files = directoryInfo.GetFiles("*.state?", SearchOption.TopDirectoryOnly);
                foreach (var file in files)
                {
                    found++;
                    Showinfo($"{found} files found - Reading '{directoryInfo.FullName}' ...");
                    directoryNode.Nodes.Add(new TreeNode(file.Name));
                }
            }
            return directoryNode;
        }
        #endregion

        #region Non-recursive approach
        public static void ListDirectory(TreeView treeView, string path)
        {
            DateTime dt = DateTime.Now;
            found = 0;
            treeView.Nodes.Clear();

            var searchPattern = string.Empty;

            if (ext == myExtensions.state)
            {
                searchPattern = "*.state?";
            }
            else if (ext == myExtensions.srm)
            {
                searchPattern = "*.srm";
            }

            var stack = new Stack<TreeNode>();
            var rootDirectory = new DirectoryInfo(path);
            var node = new TreeNode(rootDirectory.Name) { Tag = rootDirectory };
            stack.Push(node);

            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                var directoryInfo = (DirectoryInfo)currentNode.Tag;
                foreach (var directory in directoryInfo.GetDirectories())
                {
                    if (path_nocheck.Contains(directory.Name)) continue;

                    Showinfo($"{found} {searchPattern.Substring(1)} files found - Reading '{directory.FullName}' ...");

                    var files = directory.GetFiles(searchPattern, SearchOption.AllDirectories);
                    if (files.Length == 0) continue;

                    found += directory.GetFiles(searchPattern, SearchOption.TopDirectoryOnly).Length;
                    Showinfo($"{found} {searchPattern.Substring(1)} files found - Reading '{directory.FullName}' ...");

                    var childDirectoryNode = new TreeNode(directory.Name) { Tag = directory };
                    currentNode.Nodes.Add(childDirectoryNode);
                    stack.Push(childDirectoryNode);
                    
                }
                foreach (var file in directoryInfo.GetFiles(searchPattern))
                {
                    currentNode.Nodes.Add(new TreeNode(file.Name));
                }
            }

            treeView.SafeInvoke(c => c.Nodes.Add(node));
            Showinfo($"{found} {searchPattern.Substring(1)} files found - Reading input completed in {DateTime.Now.Subtract(dt).TotalSeconds.ToString("###.#")} seconds");
        }
        #endregion

        private static void Showinfo(string info)
        {
            ((ToolStripStatusLabel)((StatusStrip)Application.OpenForms[0].Controls["statusStrip1"]).Items["tsInfo"]).Text = info;
        }
    }
}
