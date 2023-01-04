
public class D08P2 : AocMachine 
{
   HashSet<string> visible = new HashSet<string>();

   public D08P2(string filename) : base(filename)  { }   

   public override void run()
   {
      readData(filename);
      findBestScenicScore();
      displayResults();
   }

   private void findBestScenicScore()
   {
      for (int y = 0; y < data.Count; y++)
      {
         int scenicScore = 0;
         for (int x = 0; x < data[0].Length; x++)
         {
            scenicScore = calcScenicScore(x,y);
            result = Math.Max(result, scenicScore);
         }
      }
   }

   private int calcScenicScore(int x, int y)
   {
      return calcScenicLeft(x,y) * calcScenicRight(x,y) * calcScenicUp(x,y) * calcScenicDown(x,y);
   }

   private int calcScenicLeft(int x, int y)
   {
      int scenicScore = 0;

      int candidateHeight = int.Parse(data[y][x].ToString());
      for (int i = x-1; i >= 0; i--)
      {
         scenicScore++;
         
         int value = int.Parse(data[y][i].ToString());
         if( value >= candidateHeight )
            break;
      }

      return scenicScore;
   }

   private int calcScenicRight(int x, int y)
   {
      int scenicScore = 0;
      int candidateHeight = int.Parse(data[y][x].ToString());

      for (int i = x+1; i < data[0].Length; i++)
      {
         scenicScore++;
         
         int value = int.Parse(data[y][i].ToString());
         if( value >= candidateHeight )
            break;
      }

      return scenicScore;
   }

   private int calcScenicUp(int x, int y)
   {
      int scenicScore = 0;
      int candidateHeight = int.Parse(data[y][x].ToString());

      for (int i = y-1; i >= 0; i--)
      {
         scenicScore++;
         
         int value = int.Parse(data[i][x].ToString());
         if( value >= candidateHeight )
            break;
      }
      // Console.WriteLine("{0} {1}: {2}", x, y, scenicScore);

      return scenicScore;
   }

   private int calcScenicDown(int x, int y)
   {
      int scenicScore = 0;
      int candidateHeight = int.Parse(data[y][x].ToString());

      for (int i = y+1; i < data.Count; i++)
      {
         scenicScore++;
         
         int value = int.Parse(data[i][x].ToString());
         if( value >= candidateHeight )
            break;
      }

      return scenicScore;
   }


   public override void displayResults()
   {
      Console.WriteLine("results: {0}", result);
   }
}

