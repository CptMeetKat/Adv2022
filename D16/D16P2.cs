using Part2;

public class D16P2 : AocMachine 
{
   public D16P2(string filename) : base(filename) { }   

   Dictionary<string, int> map = new Dictionary<string, int>();
   List<Valve> valves = new List<Valve>();

   int maxMinutes = 26;
   int startId = 0;
   int totalExplorers = 2;
   string targetId = "AA";

   public override void run()
   {
      readData(filename);
      parseData();

      // test();
      // printValves();
      setStartId(targetId);
      findLargestPath();
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
         for (int j = 0; j < 2; j++)
         {
            
            if(next >= scores.Length)
               break;
            upperbound += scores[next++] * i;
         }
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
   
   private int[] getInitalPositions()
   {
      int[] positions = new int[totalExplorers];
      for (int i = 0; i < positions.Length; i++)
         positions[i] = startId;

      return positions;
   }

   private void findLargestPath()
   {
      Candidate? bestCandidate = new Candidate();

      int best = 0; ////Useing day 1 to determine best value to start with
                     //Can change to start at new position
      int upperbound = calcUpperbound(0, 0);
      int[] initialPositions = getInitalPositions();
      
      Console.WriteLine("upper {0}:" , upperbound);

      List<Candidate> candidates = new List<Candidate>();

      candidates.Add(new Candidate(  initialPositions, 0, 1, upperbound) );
      
      // candidates[0].path1.Add( valves[initialPositions[0]].id );
      // candidates[0].path2.Add( valves[initialPositions[1]].id );
      int tryDiffApproach = 0;

      while (candidates.Count > 0)
      {
         if(tryDiffApproach % 1000000 == 0)
         {
            tryDiffApproach = 0;
            pruneCandidates(candidates, best);
            candidates.Sort(); //Sort affects depth first traversal
            Console.WriteLine("SORT: {0} {1}",candidates[0].upperbound, candidates[candidates.Count - 1].upperbound);
         }
         tryDiffApproach++;

         Candidate c = pop(candidates);

         if(c.score > best)
            bestCandidate = c;
         best = Math.Max(best, c.score);

         if(c.minute < maxMinutes && c.upperbound > best)
            explore(candidates, c, best);
      }

      if(bestCandidate.minute < maxMinutes)
         bestCandidate.score = bestCandidate.score + bestCandidate.carry;

      Console.WriteLine("Carry: {0}", bestCandidate.carry);

      bestCandidate.printPath(0);
      bestCandidate.printPath(1);

      result = bestCandidate.score;
   }



   private void explore(List<Candidate> candidates, Candidate c, int best)
   {
      Console.WriteLine("cand: {0} {1} {2} {3} {4}", c.minute, c.upperbound, c.score, candidates.Count, best);
      foreach(int o1 in IterateTunnels(c.ids[0]))
      {
         foreach(int o2 in IterateTunnels(c.ids[1]))
         {
            bool visited = false;
            Candidate temp = new Candidate();
            temp.ids.Add(o1); temp.ids.Add(o2);
            temp.score = c.score + c.carry;
            temp.minute = c.minute+1;

            // temp.copyPath(c.path1, 0);
            // temp.copyPath(c.path2, 1);

            HashSet<int> nextVisited = c.copyVisited();
            for(int i = 0; i < temp.ids.Count; i++)
            {
               int id = temp.ids[i];
               if(id < 0)
               {
                  id = transfordID(id);
                  if(!nextVisited.Contains(id) || valves[id].flowrate == 0)
                  {
                        nextVisited.Add(id);
                        temp.carry += ((maxMinutes - c.minute) * valves[id].flowrate);
                        temp.ids[i] = id;

                        // addToPath(temp, i, valves[id].id+"+");   
                  }
                  else
                     visited = true;
               }
               else
               {
                  // addToPath(temp, i, valves[id].id+"");
               }
            }

            if(!visited)
            {
               temp.upperbound = calcUpperbound(temp.minute, temp.score + temp.carry, nextVisited);
               temp.visited = nextVisited;
               if(best < temp.upperbound)
                  candidates.Add(temp);
            }
         }   
      }
   }

   private int transfordID(int id)
   {
      return Math.Abs(id+1);
   }

   private void addToPath(Candidate c, int path, string id)
   {
      if(path == 0)
         c.path1.Add(id);
      else
         c.path2.Add(id);
   }

   private int[] generateOptions(int id)
   {  
      List<string> tunnels = valves[id].tunnels;

      int[] options = new int[tunnels.Count + 1];

      int next = 0;
      for (int i = 0; i < tunnels.Count; i++)
         options[next++] = map[  tunnels[i]  ];

      options[next++] = (id * -1) - 1;       

      return options;
   }


   private IEnumerable<int> IterateTunnels(int id)
   {
      foreach (string item in valves[id].tunnels)
      {
         yield return map[item];
      }

      yield return (id * -1) - 1;

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


