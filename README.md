FarContextMenu
==============

Show Context menu for files from within FAR file manager

This program is a helper for the Far file manager. By configuring the the Edit Associations in FAR you
can get it to show the Explorer context-menu.

To do so:
  1. Put this binary (FarContextMenu.exe) in a folder which is on your path
  2. In FAR go to the Commands | File Associations menu
  3. Add an entry with the mask *.* and next to the key you want to use enter FarContextMenu.exe F "!\!.!"
  4. To set it up to open the context menu for the directory which the file is in enter FarContextMenu.exe D "!\"

Note. There appear to be some limitations in the context menu class (written by Jpmon1 and kindly released to the community
      here http://www.codeproject.com/Articles/22012/Explorer-Shell-Context-Menu) as not all the context-menu
      options seem to appear. Of course this could be my misunderstanding and not using it correctly.
