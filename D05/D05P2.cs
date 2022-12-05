
public class D05P2 : AocMachine 
{

   List<Stack<char>> stacks;

   public D05P2(string filename) : base(filename)
   {
      readData(filename);
      stacks = new List<Stack<char>>();
      int totStacks = (data[0].Length + 1)/4;
      for(int i = 0; i < totStacks; i++)
      {
         stacks.Add(new Stack<char>());
      }
      
   }   

   public override void run()
   {
      parseStack();
      // printStack(); //Empties the stack
      processStacks();
      displayResults();
   }

   private void processStacks()
   {
      int start = 0;
      while(data[start] != ""){start++;}
      start++;


      for(int i = start; i < data.Count; i++)
      {
         string[] atoms = data[i].Split(' ');

         int itemsToMove = int.Parse(atoms[1]);
         int origin = int.Parse(atoms[3]) - 1;
         int destination = int.Parse(atoms[5]) - 1;

         string crates = "";
         for(int j = 0; j < itemsToMove; j++)
         {
            if(stacks[origin].Count > 0)
               crates += stacks[origin].Pop();
         }

         foreach (char c in crates.Reverse())
            stacks[destination].Push   (  c  );

      }

      
   }

   private void printStack()
   {
      foreach (Stack<char> s in stacks)
      {
         while(s.Count > 0)
         {
            Console.Write(s.Pop());
         }
         Console.WriteLine();
      }
   }

   private void parseStack()
   {
      for(int j = 1; j < data[0].Length; j = j + 4)
      {
         //Parse Downwards
         int down = 0;
         string crateLetters = "";
         while(  !Char.IsNumber( data[down][j]  )    )
         {
            if(data[down][j] != ' ')
               crateLetters += data[down][j];
            down++;
         }
         int stackNumber = int.Parse(  data[down][j].ToString()  );

         
         foreach(char c in crateLetters.Reverse())
         {
            stacks[stackNumber-1].Push(c);
         }
         // Console.WriteLine("crateLetters {0}", crateLetters);
      }

   }



   public override void displayResults()
   {
      string result = "";
      foreach(var s in stacks)
      {
         if(s.Count > 0)
         {
            result += s.Pop();
         }
         
      }
      Console.WriteLine("results: {0}", result);
   }
}

