namespace Part2;

public class D11P2 : AocMachine 
{

   List<Monkey> monkeys = new List<Monkey>();

   public D11P2(string filename) : base(filename)
   {
   }   

   public override void run()
   {
      readData(filename);
      populateMonkeys();
      chaseMonkeys();
      displayResults();
   }


   private void populateMonkeys()
   {
      int i = 0;
      while(i < data.Count)
      {
         string[] atoms = data[i++].Split(" ");
         if(atoms[0] == "Monkey")
         {
            Monkey monkey = new Monkey();
            parseStartingItems(monkey, data[i++]);
            parseOperation(monkey, data[i++]);
            parseTest(monkey, data[i++]);
            parseTrueTest(monkey, data[i++]);
            parseFalseTest(monkey, data[i++]);
            monkeys.Add(monkey);
         }
      }
   }

   private void parseTrueTest(Monkey m, string line)
   {
      string[] atoms = line.Split(" ");
      int value = int.Parse(atoms[atoms.Count()-1]);
      m.trueTarget = value;
   }

   private void parseFalseTest(Monkey m, string line)
   {
      string[] atoms = line.Split(" ");
      int value = int.Parse(atoms[atoms.Count()-1]);
      m.falseTarget = value;
   }

   private void parseTest(Monkey m, string line)
   {
      string[] atoms = line.Split(" ");
      int value = int.Parse(atoms[atoms.Count() -1]);

      m.test = x => x % value == 0; //Divide 0 error      

   }

   private void parseOperation(Monkey m, string line)
   {
      string[] atoms = line.Split(":");
      atoms = atoms[1].Trim().Split(" ");

      string operation = atoms[atoms.Count() - 2];
      string modifer = atoms[atoms.Count() - 1];
      
      if(modifer == "old")
      {
         if(operation == "*")
            m.operation = x => x * x;
         else //(operation == "+") //Default
            m.operation = x => x + x;
      }
      else
      {
         int modiferValue = int.Parse(modifer);
         if(operation == "*")
            m.operation = x => x * modiferValue;
         else //(operation == "+") //Default
            m.operation = x => x + modiferValue;
      }

   }

   private void parseStartingItems(Monkey m, string line)
   {
      string[] atoms = line.Split(":");
      atoms = atoms[1].Split(",");
      foreach(string value in atoms)
      {
         m.items.Add(  int.Parse( value ) );
      }
   }

   public void chaseMonkeys()
   {
      int totalRounds = 10000;
      for (int i = 0; i < totalRounds; i++)
      {
         foreach(Monkey m in monkeys)
            m.inspect(monkeys);
      }

      monkeys.Sort(delegate(Monkey x, Monkey y)
      {
         if (x.inspections < y.inspections) return -1;
         else if (x.inspections > y.inspections) return 1;
         return 0;
      });
      monkeys.Reverse();
      printInspections();

      result = monkeys[0].inspections * monkeys[1].inspections;
   }

   private void printInspections()
   {
      foreach (Monkey m in monkeys)
         Console.Write(m.inspections + " ");
      Console.WriteLine();

   }


   public override void displayResults()
   {
      Console.WriteLine("results: {0}", result);
   }
  
}



public class Monkey
{
   public List<int> items = new List<int>();
   public Func<int, int> operation = x => x;
   public Func<int, bool> test = x => false;
   public int trueTarget = 0;
   public int falseTarget = 0;

   public int inspections = 0;

   public Monkey()
   {
   }

   public void inspect(List<Monkey> monkeys)
   {
      while(items.Count > 0)
      {
         inspections++;
         items[0] = operation(items[0]);
         if(  test(items[0])   )
            monkeys[trueTarget].items.Add(items[0]);
         else
            monkeys[falseTarget].items.Add(items[0]);

         items.RemoveAt(0);
      }
   }
}
