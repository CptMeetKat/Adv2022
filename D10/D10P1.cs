public class D10P1 : AocMachine 
{
   List<Worker> processes = new List<Worker>();
   int registerX = 1; //starts at 1
   int cycle = 1;
   int[] keyCycles = new int[] {20, 60, 100, 140, 180, 220};


   public D10P1(string filename) : base(filename)
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
      int accumalatedSignal = 0;

      foreach (var d in data)
         processes.Add(  new Worker(d) );

      while (processes.Count > 0)
      {
         if(keyCycles.Contains(cycle))
            accumalatedSignal = accumalatedSignal + (registerX * cycle);
         tick();
         cycle++;
      }

      result = accumalatedSignal;
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

