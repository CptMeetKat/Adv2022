namespace Part2;

using System.Numerics;

public class D11P2 : AocMachine 
{

   List<Monkey> monkeys = new List<Monkey>();
   new BigInteger result;

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
      int cap = 1;
      while(i < data.Count)
      {
         string[] atoms = data[i++].Split(" ");
         if(atoms[0] == "Monkey")
         {
            Monkey monkey = new Monkey();
            parseStartingItems(monkey, data[i++]);
            parseOperation(monkey, data[i++]);
            cap = cap * parseTest(monkey, data[i++]);
            parseTrueTest(monkey, data[i++]);
            parseFalseTest(monkey, data[i++]);
            monkeys.Add(monkey);
         }
      }

      foreach (var m in monkeys)
         m.cap = cap;  
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

   private int parseTest(Monkey m, string line)
   {
      string[] atoms = line.Split(" ");
      ulong value = ulong.Parse(atoms[atoms.Count() -1]);

      m.test = x => x % value == 0; //Divide 0 error      

      return (int)value;

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
         ulong modiferValue = ulong.Parse(modifer);
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
         m.items.Add(  ulong.Parse( value ) );
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

      // printInspections();
      monkeys.Sort(delegate(Monkey x, Monkey y)
      {
         if (x.inspections < y.inspections) return -1;
         else if (x.inspections > y.inspections) return 1;
         return 0;
      });
      monkeys.Reverse();

      result = BigInteger.Multiply(monkeys[0].inspections, monkeys[1].inspections);
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
   public int cap = -1;
   public List<ulong> items = new List<ulong>();
   public Func<ulong, ulong> operation = x => x;
   public Func<ulong, bool> test = x => false;
   public int trueTarget = 0;
   public int falseTarget = 0;

   public int inspections = 0;

   public Monkey()
   {
   }

   private ulong operate(int value)
   {
      ulong result = operation(  (ulong) value);
      result = result % (ulong)cap;

      
      return result;
   }

   public void inspect(List<Monkey> monkeys)
   {
      
      while(items.Count > 0)
      {
         // Console.WriteLine(items[0]);
         inspections++;
         
         items[0] = operate( (int) items[0]);
         // items[0] = items[0] / 3;

         if(  test(items[0])   )
            monkeys[trueTarget].items.Add(items[0]);
         else
            monkeys[falseTarget].items.Add(items[0]);

         items.RemoveAt(0);

      }



   }
}