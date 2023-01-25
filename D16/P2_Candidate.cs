namespace Part2;

public class Candidate : IComparable
{
   public HashSet<int> visited = new HashSet<int>();

   public int upperbound = 0;
   public int score = 0;
   public int minute = 0;
   public List<int> ids = new List<int>();
   public int carry = 0;

   public List<string> path1 = new List<string>();
   public List<string> path2 = new List<string>();

   public Candidate(int[] ids, int score, int minute, int upperbound, HashSet<int>? visited = null, int carry = 0)
   {
      this.upperbound = upperbound;
      this.score = score;
      this.minute = minute;
      this.carry = carry;

      foreach(var i in ids)
         this.ids.Add(i);

      if(visited != null)
      {  
         foreach (var v in visited)
            this.visited.Add(v);
      }
   }

   public Candidate(){}

   public void copyPath(List<string> prev, int path)
   {
      foreach (var p in prev)
      {
         if(path == 0)
            path1.Add(p);
         else
            path2.Add(p);
      }

   }

   public void printPath(int path)
   {
      if(path == 0)
      {
         foreach (var p in path1)
            Console.Write(p + " ");
      }
      else
      {
         foreach (var p in path2)
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