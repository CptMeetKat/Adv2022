
public class D03P1 : AocMachine 
{
   public D03P1(string filename) : base(filename){}   

   public override void run()
   {
      readData(filename);     
      findTotalPriority();
      displayResults();
   }

   private void findTotalPriority()
   {
      int totalPriority = 0;
      foreach (var rucksack in data)
      {
         string s1 = rucksack.Substring(0, rucksack.Length/2);
         string s2 = rucksack.Substring(rucksack.Length/2);

         char common = findCommonLetter(s1, s2);
         totalPriority += getLetterValue(common);
      }

      result = totalPriority;
   }

   private int getLetterValue(char common)
   {
      int code = (int)common;
      int score = 0;
      if(code <= 90)
         score = code - 38; // upper
      else
         score = code - 96; // lower

      return score;
   }

   private char findCommonLetter(string s1, string s2)
   {
      HashSet<char> A = new HashSet<char>(s1);
      A.IntersectWith(s2);
      
      return String.Join("", A)[0];
   }


   public override void displayResults()
   {
      Console.WriteLine("results: {0}", result);
   }
}

