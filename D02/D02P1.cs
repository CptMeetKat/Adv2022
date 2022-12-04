
public class D02P1 : AocMachine 
{
 
   Dictionary<string, int> shapeScores = new Dictionary<string, int>() {
            { "X", 1 },
            { "Y", 2 },
            { "Z", 3 }
        };


   Dictionary<string, int> positions = new Dictionary<string, int>() {
      {"X", 0}, //ROCK
      {"Y", 1}, //Paper
      {"Z", 2}, //Scissors
      {"A", 0}, //Rock
      {"B", 1}, //Paper
      {"C", 2}, //Scissors
   };        

   List<List<int>> p2OutComes = new List<List<int>>() {
       new List<int>(){3,6,0},
       new List<int>(){0,3,6},   
       new List<int>(){6,0,3}    
   };             

      //P2 scores               
       //          P2
       //
       //        X Y Z
       //      A 3 6 0
       // P1   B 0 3 6
       //      C 6 0 3
   

   public D02P1(string filename) : base(filename){}   

   public override void run()
   {
      readData(filename);     
      findTournamentScore();
      displayResults();
   }

   private void findTournamentScore()
   {
      int p2Score = 0;
      foreach(string game in data)
      {
         string[] atoms = game.Split(' ');
         string player1 = atoms[0];
         string player2 = atoms[1];

         p2Score += getGameOutcome(player1, player2) + shapeScores[player2];
      }

      result = p2Score;
   }

   public int getGameOutcome(string p1, string p2)
   {
      return p2OutComes[positions[p1]][positions[p2]];
   }

   public override void displayResults()
   {
      Console.WriteLine("results: {0}", result);
   }
}

