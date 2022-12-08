

/*
Not happy with the design, I want to allow a Folder to have a parent=null for root folder.
C# dosen't like unhandled nulls
Instead gave root a default value parent - not entirely sure if this makes sense or is logical design
..prefer null
*/


public class D07P1 : AocMachine 
{
   Folder root = new Folder("/");

   public D07P1(string filename) : base(filename)
   {
   }   

   public override void run()
   {
      readData(filename);
      buildSystemTree();
      getFolderSize(root);
      displayResults();
   }
   
   private int getFolderSize(Folder node)
   {
      int folderTotal = 0;
      for (int i = 1; i < node.folders.Count; i++)
      {
         folderTotal += getFolderSize( node.folders[i] );
         // Console.WriteLine("look: {0} {1}", node.name, node.folders[i].name);
      }

      foreach(lFile f in node.files)
      {
         // Console.WriteLine("look: {0} {1} {2}", node.name, f.name, folderTotal);
         folderTotal += f.size;
      }

      // Console.WriteLine("Folder Total: {0} {1}", node.name, folderTotal);

      if(folderTotal <= 100000)
         result += folderTotal;

      return folderTotal;

   }

   private void buildSystemTree()
   {
      Folder current = root;

      int i = 0;
      while (i < data.Count)
      {
         string[] atoms = data[i].Split(" ");
         // Console.WriteLine("COMMAND: {0}", data[i]);
         if(atoms[0] == "$" && atoms[1] == "cd")
         {
            if(atoms[2] == "..")
            {
               current = current.getFolder(0);
            }
            else if(atoms[2] == "/")
            {
               current = root;
            }
            else // e.g $ cd foldername1
            {
            
               Folder f = current.getFolder(current.findFolder(atoms[2]));
               current = f;
            }
            i++;
         }
         else //LS
         {
            i++;
            while(i < data.Count && data[i][0] != '$')
            {
               atoms = data[i].Split(" ");
               if(atoms[0] == "dir")
                  current.addFolder(atoms[1]);
               else
                  current.addFile(atoms[1], int.Parse(atoms[0]));
               i++;
            }
         }
      }
   }

   public override void displayResults()
   {
      Console.WriteLine("results: {0}", result);
   }
}

