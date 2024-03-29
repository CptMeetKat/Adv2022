namespace Part1;

public class Candidate : IComparable
{
   public HashSet<int> visited = new HashSet<int>();

   public int upperbound = 0;
   public int score = 0;
   public int minute = 0;
   public int id = 0;
   public int carry = 0;

   public List<string> path = new List<string>();

   public Candidate(int id, int score, int minute, int upperbound, HashSet<int>? visited = null, int carry = 0)
   {
      this.upperbound = upperbound;
      this.score = score;
      this.minute = minute;
      this.id = id;
      this.carry = carry;

      if(visited != null)
      {  
         foreach (var v in visited)
            this.visited.Add(v);
      }
   }

   public Candidate(){}

   public void copyPath(List<string> prev)
   {
      foreach (var p in prev)
         path.Add(p);

   }

   public void printPath()
   {
      foreach (var p in path)
      {
         Console.Write(p + " ");
      }
      Console.WriteLine();
   }

    public int CompareTo(Object? obj)
    {
        if (obj == null) return 1;

         Candidate candidate = (Candidate) obj;

        if(upperbound < candidate.upperbound) return -1;
        else if (upperbound > candidate.upperbound) return 1;
        return 0;
    }


    public HashSet<int> copyVisited()
    {
      HashSet<int> copy = new HashSet<int>();
      foreach (var v in visited)
      {
         copy.Add(v);
      }
      return copy;
    }

}