

/*
Not happy with the design, I want to allow a Folder to have a parent=null for root folder.
C# dosen't like unhandled nulls
Instead gave root a default value parent - not entirely sure if this makes sense or is logical design
..prefer null
*/


public class D07P2 : AocMachine 
{
   Folder root = new Folder("/");

   public D07P2(string filename) : base(filename)
   {
   }   

   public override void run()
   {
      readData(filename);
      buildSystemTree();
      findDirectoryToDelete();
      displayResults();
   }

   private void findDirectoryToDelete()
   {
      int totalDiskSpace = 70000000;
      int rootUsed = root.size;
      int totalFreeSpace = totalDiskSpace - rootUsed;
      int targetSpace = 30000000 - totalFreeSpace;

      // Console.WriteLine(totalFreeSpace);
      // Console.WriteLine(30000000 - totalFreeSpace);

      result = findSmallestDirectoryAboveValue(root, targetSpace);
   }
   
   static int findSmallestDirectoryAboveValue(Folder curr, int floor)
   {
      int bestSize = curr.size;
      for(int i = 1; i < curr.folders.Count; i++)
      {
         int candidateSize = findSmallestDirectoryAboveValue(curr.folders[i], floor);
         if( candidateSize < bestSize && candidateSize > floor)
         {
            bestSize = candidateSize;
         }
      }
      
      return bestSize;
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

