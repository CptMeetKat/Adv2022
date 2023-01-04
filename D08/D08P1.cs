
public class D08P1 : AocMachine 
{
   HashSet<string> visible = new HashSet<string>();

   public D08P1(string filename) : base(filename)
   {
   }   

   public override void run()
   {
      readData(filename);
      findVisibleTrees();
      // printVisibleTrees();
      displayResults();
   }

   private void printVisibleTrees()
   {
      foreach (var key in visible)
      {
         Console.WriteLine("{0}", key);
         
      }
   }
   
   private bool isVisibleDown(int x, int y)
   {
      int treeHeight = int.Parse(data[y][x].ToString());
      for(int i = 0; i < y; i++)
      {
         if(  int.Parse(data[i][x].ToString()  )  >= treeHeight)
            return false;
      }

      return true;
   }
   
   private bool isVisibleUp(int x, int y)
   {
      int treeHeight = int.Parse(data[y][x].ToString());

      for(int i = data.Count-1; i > y; i--)
      {
         if(  int.Parse(data[i][x].ToString()  )  >= treeHeight)
            return false;         
      }

      return true;
   }


   private bool isVisibleRight(int x, int y)
   {
      int treeHeight = int.Parse(data[y][x].ToString());

      for(int i = 0; i < x; i++)
      {
         if(int.Parse(data[y][i].ToString()) >= treeHeight)
            return false;
      }

      return true;
   }
   private bool isVisibleLeft(int x, int y)
   {
      int treeHeight = int.Parse(data[y][x].ToString());

      for(int i = data[0].Length-1; i > x; i--)
      {
         if(int.Parse(data[y][i].ToString()) >= treeHeight)
            return false;
      }
      return true;
   }

   private void findVisibleTrees()
   {
      for (int y = 0; y < data.Count; y++)
      {
         for (int x = 0; x < data[0].Length; x++)
         {
            if(isVisibleLeft(x, y) || 
               isVisibleRight(x, y) ||
               isVisibleDown(x, y) ||
               isVisibleUp(x, y) )
               {
                  result++;
               }
            
         }
         
      }

      // result = visible.Count;
   }

   public override void displayResults()
   {
      Console.WriteLine("results: {0}", result);
   }
}

