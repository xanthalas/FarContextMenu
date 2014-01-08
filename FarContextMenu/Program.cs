/*****************************************************************************
* Name   : FarContextMenu  (FarContextMenu.exe)                              *
* Author : Copyright (C) 2014 Xanthalas                                      *
* Date   : 8th January 2014                                                  *
* Purpose: This is a helper program to be used with the FAR file manager to  *
*          display a context menu for the currently selected file.           *
******************************************************************************
* This program is free software; you can redistribute it and/or              *
* modify it under the terms of the GNU General Public License                *
* as published by the Free Software Foundation; either version 2             *
* of the License, or (at your option) any later version.                     *
*                                                                            *
* This program is distributed in the hope that it will be useful,            *
* but WITHOUT ANY WARRANTY; without even the implied warranty of             *
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the              *
* GNU General Public License for more details.                               *
*                                                                            *
* You should have received a copy of the GNU General Public License          *
* along with this program; if not, write to the Free Software                *
* Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.*
*****************************************************************************/

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Xanthalas
{
    static class Program
    {
        private static string type = string.Empty;

        /// <summary>
        /// This program is a helper for the Far file manager. By configuring the the Edit Associations in FAR you
        /// can get it to show the Explorer context-menu.
        /// 
        /// To do so:
        ///   1. Put this binary (FarContextMenu.exe) in a folder which is on your path
        ///   2. In FAR go to the Commands | File Associations menu
        ///   3. Add an entry with the mask *.* and next to the key you want to use enter FarContextMenu.exe F "!\!.!"
        ///   4. To set it up to open the context menu for the directory which the file is in enter FarContextMenu.exe D "!\"
        /// 
        /// Note. There appear to be some limitations in the context menu class (written by Jpmon1 and kindly released to the community
        ///       here http://www.codeproject.com/Articles/22012/Explorer-Shell-Context-Menu) as not all the context-menu
        ///       options seem to appear. Of course this could be my misunderstanding and not using it correctly.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Debug.WriteLine("FarContextMenu called");
            if (args.Length < 2)
            {
                MessageBox.Show(string.Format("FarContextMenu: Expected at least 2 arguments. Got {0}", args.Length));
                return;
            }

            type = args[0];
            if (type != "F" && type != "D")
            {
                MessageBox.Show(string.Format("FarContextMenu: Type must be F (for file) or D (for Directory). Got {0}", type));
                return;                
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            StringBuilder builder = new StringBuilder();

            var argNumber = 0;
            foreach (var arg in args)
            {
                if (argNumber > 0)
                {
                    builder.Append(arg);
                }
                argNumber++;
            }
            var path = builder.ToString(); 

            Debug.WriteLine("Path = " + path);
            ShowMenu(path);
        }

        public static void ShowMenu(string path)
        {
            FileInfo fi = new FileInfo(path);

            if (type == "F" && File.Exists(path))
            {
                FileInfo[] files = new FileInfo[1];
                files[0] = fi;
                ShellContextMenu fileMenu = new ShellContextMenu();

                Debug.WriteLine("Showing menu for file = " + path);
                fileMenu.ShowContextMenu(files, new System.Drawing.Point(50, 50));
            }
            else
            {
                var dir = fi.Directory.ToString();

                if (Directory.Exists(dir))
                {
                    DirectoryInfo di = new DirectoryInfo(dir);
                    DirectoryInfo[] directories = new DirectoryInfo[1];
                    directories[0] = di;
                    ShellContextMenu directoryMenu = new ShellContextMenu();

                    Debug.WriteLine("Showing menu for directory = " + dir);
                    directoryMenu.ShowContextMenu(directories, new System.Drawing.Point(50, 50));
                }
            }
        }
    }
}
