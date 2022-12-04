
public class D02P2 : AocMachine 
{
 
   Dictionary<string, int> gameOutcomes = new Dictionary<string, int>() {
            { "X", 0 },
            { "Y", 3 },
            { "Z", 6 }
        };


   Dictionary<string, int> positions = new Dictionary<string, int>() {
      {"X", 0}, // Lose
      {"Y", 1}, // Draw
      {"Z", 2}, // Win
      {"A", 0}, // Rock
      {"B", 1}, // Paper
      {"C", 2}, // Scissors
   };        


   List<List<int>> p1ToOutcome = new List<List<int>>() {
       new List<int>(){3,1,2},
       new List<int>(){1,2,3},   
       new List<int>(){2,3,1}    
   };             
   //Rock 1
   //Paper 2
   //Sciccors 3

   public D02P2(string filename) : base(filename){}   

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
         string outcome = atoms[1];

         p2Score += gameOutcomes[outcome] + getP2Choice(player1, outcome);
      }

      result = p2Score;
   }

   public int getP2Choice(string p1, string outcome)
   {
      return p1ToOutcome[positions[p1]][positions[outcome]];
   }

   public override void displayResults()
   {
      Console.WriteLine("results: {0}", result);
   }
}

