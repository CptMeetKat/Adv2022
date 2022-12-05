
public class D04P2 : AocMachine 
{
   public D04P2(string filename) : base(filename){}   

   public override void run()
   {
      readData(filename);     
      findTotalContainedPairs();
      displayResults();
   }


   private void findTotalContainedPairs()
   {
      int totalContainerPairs = 0;


      foreach (string pairs in data)
      {
         string[] atoms = pairs.Split(',' , '-');
         (int, int) range1 = (int.Parse(atoms[0]), int.Parse(atoms[1]));
         (int, int) range2 = (int.Parse(atoms[2]), int.Parse(atoms[3]));

         if( (   inRange(range1.Item1, range1.Item2, range2.Item1) || inRange(range1.Item1, range1.Item2, range2.Item2) )  ||
            (inRange(range2.Item1, range2.Item2, range1.Item1) || inRange(range2.Item1, range2.Item2, range1.Item2)))
         {
            totalContainerPairs++;
         }         
      }
      result = totalContainerPairs;
   }

   private bool inRange(int first, int last, int target)
   {
      return target >= first && target <= last;
   }

   public override void displayResults()
   {
      Console.WriteLine("results: {0}", result);
   }
}

