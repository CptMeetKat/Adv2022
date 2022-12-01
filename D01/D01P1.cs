
public class D01P1 : AocMachine 
{
 
   public D01P1(string filename) : base(filename){  }   

   
   public override void run()
   {
      readData(filename);     
      findHighestCalories();
      displayResults();
   }

   private void findHighestCalories()
   {
      int highestCalories = 0;

      for (int i = 0; i < data.Count; i++)
      {
         int currentElf = 0;
         while(i < data.Count && data[i] != "")
         {
            currentElf += Int32.Parse(data[i++]);
         }
         
         highestCalories = Math.Max(highestCalories, currentElf);
      }

      result = highestCalories;
   }

   public override void displayResults()
   {
      Console.WriteLine("results: {0}", result);
   }
}

