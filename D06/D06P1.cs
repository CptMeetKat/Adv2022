
public class D06P1 : AocMachine 
{
   Dictionary<char, int> repeats = new Dictionary<char, int>();

   public D06P1(string filename) : base(filename)
   {
   }   

   public override void run()
   {
      readData(filename);
      findNonRepeatingSubstring(4);
      displayResults();

      findNonRepeatingSubstring(14);
      displayResults();
   }

   private void initMap(string initial)
   {
      foreach(char s in initial)
      {
         if(repeats.ContainsKey(s))
            repeats[s]++;
         else
            repeats.Add(s, 1);
      }
   }

   private void findNonRepeatingSubstring(int substringSize)
   {
      initMap(data[0].Substring( 0,  substringSize ));

      for(int i = substringSize; i < data[0].Length; i++)
      {
         char tail = data[0][i];
         char head = data[0][i-substringSize];

         if(repeats[head] > 1)
            repeats[head]--;
         else
            repeats.Remove(head);

         if(repeats.ContainsKey(tail))
            repeats[tail]++;
         else
            repeats.Add(tail, 1);


         if (! containsDupes() )
         {
            result = i + 1;
            break;
         }
      }

      repeats.Clear();
   }

   private bool containsDupes()
   {
      foreach(var pair in repeats)
      {
         if(pair.Value > 1)
         {
            return true;
         }
      }

      return false;
   }


   private void printMap()
   {
      foreach(var t in repeats)
      {
         Console.Write("({0} {1}) ", t.Key, t.Value);
      }
      Console.WriteLine();
   }



   public override void displayResults()
   {
      Console.WriteLine("results: {0}", result);
   }
}

