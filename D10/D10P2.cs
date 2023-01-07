public class D10P2 : AocMachine 
{
   List<Worker> processes = new List<Worker>();
   int registerX = 1; //starts at 1
   int cycle = 0;
   int[] keyCycles = new int[] {40, 80, 120, 160, 200, 240};

   public D10P2(string filename) : base(filename)
   {
   }   

   public override void run()
   {
      readData(filename);
      findInterestingSignals();
      displayResults();
   }

   private void findInterestingSignals()
   {
      string output = "";
      foreach (var d in data)
         processes.Add(  new Worker(d) );

      int offset = 0;
      while (processes.Count > 0)
      {
         if(keyCycles.Contains(cycle))
         {
            offset+= 40;
            output += "\n";
         }

         if(registerX - 1 == cycle - offset ||
            registerX  == cycle - offset||
            registerX + 1 == cycle - offset)
            output += "#";
         else
            output += ".";
            
         tick();
         cycle++;
      }


      Console.WriteLine(output);
      result = -1;
   }

   private void tick()
   {
      Worker w = processes[0];
      w.cycle++;
      
      if(w.instruction == "noop" && w.cycle >= 1)
         processes.RemoveAt(0);
      else if (w.cycle >= 2) //We assume instruction is addx
      {
         string[] atoms = w.instruction.Split(' ');
         int xModifier = int.Parse(atoms[1]); 
         registerX += xModifier;

         processes.RemoveAt(0);
      }
   }

   public override void displayResults()
   {
      Console.WriteLine("results: {0}", result);
   }
}
