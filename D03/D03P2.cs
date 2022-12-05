
public class D03P2 : AocMachine 
{
   public D03P2(string filename) : base(filename){}   

   public override void run()
   {
      readData(filename);     
      findTotalPriority();
      displayResults();
   }

   private void findTotalPriority()
   {
      int totalPriority = 0;
      for(int i = 0; i < data.Count - 2; i = i + 3)
      {
         string s1 = data[i];
         string s2 = data[i+1];
         string s3 = data[i+2];

         char common = findCommonLetter(   findCommonLetter(s1, s2), s3    )[0];
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

   private string findCommonLetter(string s1, string s2)
   {
      HashSet<char> A = new HashSet<char>(s1);
      A.IntersectWith(s2);
      
      return String.Join("", A);
   }


   public override void displayResults()
   {
      Console.WriteLine("results: {0}", result);
   }
}

