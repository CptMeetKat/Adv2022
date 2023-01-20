public class D16P1 : AocMachine 
{
   public D16P1(string filename) : base(filename) { }   

   Dictionary<string, int> map = new Dictionary<string, int>();
   List<Valve> valves = new List<Valve>();

   int maxMinutes = 30;
   int startId = 0;

   public override void run()
   {
      readData(filename);
      parseData();

      // test();
      // printValves();
      setStartId("AA");
      findLargestPath(0);

      displayResults();
   }

   private void setStartId(string id)
   {
      for (int i = 0; i < valves.Count; i++)
      {
         if(valves[i].id == id)
         {
            startId = i;
            break;
         }
      }
   }

   private void test()
   {
      int[] scores = new int[]{10};

      int remain = calcRemainingScore(scores, 29);
      Console.WriteLine("out {0}", remain);
   }

   private List<int> getOrderedScores(HashSet<int>? visited = null)
   {
      List<int> scores = new List<int>();
      for (int i = 0; i < valves.Count; i++)
      {
         if( visited == null || ! visited.Contains(i)  )
            scores.Add(valves[i].flowrate);
      }

      scores.Sort((x, y) => {

         if(x < y) return 1;
         else if( x > y) return -1;
         return 0;
         });
      
      return scores;
   }

   private int calcUpperbound(int elapsed, int score, HashSet<int>? visited = null)
   {
      int[] orderedScores = getOrderedScores(visited).ToArray();
      int upperbound = calcRemainingScore(orderedScores, elapsed) + score;
       
      return upperbound;
   }

   private int calcRemainingScore(int[] scores, int minutesElapsed)
   {
      int upperbound = 0;
      int next = 0;
      for (int i = maxMinutes - minutesElapsed; i > 1; i-=2)
      {
         if(next >= scores.Length)
            break;
         upperbound += scores[next++] * i;
      }

      return upperbound;
   }


   private void pruneCandidates(List<Candidate> candidates, int best)
   {
      int prevCount = candidates.Count;
      for (int i = 0; i < candidates.Count; i++)
      {
         if (candidates[i].upperbound <= best)
            candidates.RemoveAt(i--);
      }
      int postCount = candidates.Count;
      Console.WriteLine("Pruning: {0} -> {1}", prevCount, postCount);
   }


   private Candidate pop(List<Candidate> candidates)
   {
      int last = candidates.Count-1;
      Candidate c = candidates[last];
      candidates.RemoveAt(last);
      return c;
   }
   
   private void findLargestPath(int target)
   {
      int best = 0;
      int upperbound = calcUpperbound(0, 0);
      Candidate? bestCandidate = new Candidate();
      
      List<Candidate> candidates = new List<Candidate>();
      candidates.Add(new Candidate(  startId, 0, 1, upperbound) );
      candidates[0].path.Add(valves[0].id);

      while (candidates.Count > 0)
      {
         Candidate c = pop(candidates);
         if(c.minute == maxMinutes) //Node has reached its lifetime
         {
            if(c.score + c.carry >= best)
               bestCandidate = c;
            best = Math.Max(best, c.score+c.carry);
            // pruneCandidates(candidates, best);
         }
         else
         {
            exploreNeighbours(candidates, c, best);
            if(! c.visited.Contains(c.id)) // Activate self
               exploreSelf(candidates, c, best);
         }
      }

      bestCandidate.score = bestCandidate.score - bestCandidate.carry;

      bestCandidate.printPath();
      result = best;
      result = bestCandidate.score;
   }

   private void exploreNeighbours(List<Candidate> candidates, Candidate c, int best)
   {
      foreach (string adjacent in valves[c.id].tunnels)
      {
         int id = map[adjacent];
         int upperbound = calcUpperbound(c.minute+1, c.score + c.carry, c.visited);

         if(upperbound >= best) //Ignore if better already exists
         {
            Candidate temp = new Candidate( id, c.score+c.carry, c.minute+1, upperbound, c.visited);
            temp.copyPath(c.path);
            // temp.path.Add(   c.id.ToString()  );
            temp.path.Add(   valves[id].id  );
            candidates.Add( temp  );
         }
      }

   }

   private void exploreSelf(List<Candidate> candidates, Candidate c, int best)
   {
      HashSet<int> nextVisited = c.copyVisited();
      nextVisited.Add(c.id);
      int carry = ((maxMinutes - c.minute) * valves[c.id].flowrate);
      // carry = Math.Max(0, carry); //minute cant be 0 or else need this

      int upperbound = calcUpperbound(c.minute+1, c.score + carry, nextVisited);
      if(upperbound >= best)
      {
         Candidate temp = new Candidate( c.id, c.score + c.carry, c.minute+1, upperbound, nextVisited, carry);
         
         temp.copyPath(c.path);
         temp.path.Add(   valves[c.id].id+"+"  );
         candidates.Add(  temp  );
      }
   }

   private void printValves()
   {
      for (int i = 0; i < valves.Count; i++)
      {
         Valve v = valves[i];
         Console.Write("{0} {1} {2} ", i, v.id, v.flowrate);
         v.tunnels.ForEach(x => Console.Write(x + " "));
         Console.WriteLine();  
      }
   }

   private void parseData()
   {
      for(int i = 0; i < data.Count; i++)
      {
         string s = data[i];
         char[] deliminators = new char[] {' ','=', ';', ','};
         string [] atoms = s.Split(deliminators);

         string valveId = atoms[1];
         int flowrate = int.Parse(atoms[5]);
         List<string> tunnels = new List<string>();
         
         int j = 11;
         while(j < atoms.Length)
         {
            tunnels.Add(atoms[j]);
            j+=2;
         }

         valves.Add(  new Valve(valveId, flowrate, tunnels)  );
      }

      for (int i = 0; i < valves.Count; i++)
         map.Add(valves[i].id, i);
   }

   public override void displayResults()
   {
      Console.WriteLine("results: {0}", result);
   }

}


