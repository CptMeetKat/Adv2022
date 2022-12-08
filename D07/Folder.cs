class Folder
{
   public string name = "";
   public int size = 0;
   public List<lFile> files = new List<lFile>();
   public List<Folder> folders = new List<Folder>();

   public Folder(string name, Folder? parent = null)
   {
      this.name = name;
      if(parent == null)
         folders.Add(this);      
      else //Root folder is its own parent
         folders.Add(parent);
   }

   public Folder()
   {
   }


   public void addFile(string file, int size)
   {
      // Console.WriteLine("addFile: {2} {0} {1}", file, size, name);
      files.Add(new lFile(file, size));
      increaseSize(size);
   }

   public void increaseSize(int size)
   {
      this.size += size;
      // Console.WriteLine("{0} {1}",  f.name, folders[0].name);
      if(name != "/" || folders[0].name != "/")
         folders[0].increaseSize(size);

   }

   public void addFolder(string foldername)
   {
      folders.Add(   new Folder(  foldername, this  ));
   }

   public Folder getFolder(int i)
   {
      return folders[i];
   }

   public int findFolder(string foldername)
   {
      for(int i = 1; i < folders.Count; i++)
      {
         if(folders[i].name == foldername)
            return i;
      }
      return -1;
   }
}
