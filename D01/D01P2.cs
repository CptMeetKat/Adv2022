
public class D01P2 : AocMachine 
{
 
   public D01P2(string filename) : base(filename){  }   

   
   public override void run()
   {
      readData(filename);     
      findTop3HighestCalories();
      displayResults();
   }

   private void findTop3HighestCalories()
   {
      List<int> highestCalories = new List<int>();

      for (int i = 0; i < data.Count; i++)
      {
         int currentElf = 0;
         while(i < data.Count && data[i] != "")
         {
            currentElf += Int32.Parse(data[i++]);
         }   
         highestCalories.Add(currentElf);
      }

      highestCalories.Sort();
      highestCalories.Reverse();
      result = highestCalories[0] + highestCalories[1] + highestCalories[2];
   }

   public override void displayResults()
   {
      Console.WriteLine("results: {0}", result);
   }
}

