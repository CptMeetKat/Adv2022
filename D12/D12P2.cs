

public class D12P2 : AocMachine 
{
   List<Node> startNodes = new List<Node>();
   Node end = new Node();

   int height = -1;
   int width = -1;

   public D12P2(string filename) : base(filename)
   {
   }   

   public override void run()
   {
      readData(filename);
      init();
      findBestHill(); 
      displayResults();
   }
   
   private void init()
   {
      setHeightWidth();
      setStart();
      setEnd();
   }

   private void findBestHill()
   {
      int best = int.MaxValue;
      foreach(Node n in startNodes)
      {
         best = Math.Min(best, climbHill(n, end));
      }
      result = best;
   }

   private void setHeightWidth()
   {
      height = data.Count;
      width = data[0].Length;
   }

   private void setStart()
   {
      for (int y = 0; y < height; y++)
      {
         for (int x = 0; x < width; x++)
         {
            char value = data[y][x];
            if(value == 'S' || value == 'a')
            {
               startNodes.Add(new Node(x, y));
            }
         }
      }
   }

   private void setEnd()
   {
      for (int y = 0; y < height; y++)
         for (int x = 0; x < width; x++)
         {
            if(data[y][x] == 'E')
            {
               end.x = x;
               end.y = y;
               return;
            }
         }
   }

   private Node[,] initVisited()
   {
      Node[,] visited = new Node[height, width];
      for (int y = 0; y < height; y++)
         for (int x = 0; x < width; x++)
            visited[y,x] = new Node(-1, -1);

      return visited;
   }

   public int climbHill(Node start, Node end)
   {
      Queue<(Node, Node)> q = new Queue<(Node, Node)>();
      Node[,] visited = initVisited();

      q.Enqueue(   ( new Node(-1, -1), start )   );

      while(q.Count > 0)
      {
         (Node, Node) cand = q.Dequeue();
         Node prev = cand.Item1;
         Node curr = cand.Item2;

         if(visited[curr.y, curr.x].x != -1) // if visited, skip
            continue;

         visited[curr.y, curr.x].x = prev.x;
         visited[curr.y, curr.x].y = prev.y;

         Node dest = new Node(curr);

         dest.x = curr.x + 1;
         visit(q, curr, dest);

         dest.x = curr.x - 1;
         visit(q, curr, dest);
         
         dest.x = curr.x;
         dest.y = curr.y + 1;
         visit(q, curr, dest);
         
         dest.y = curr.y - 1;
         visit(q, curr, dest);
      }


      return calcResult(visited, start, end);
   }


   private int calcResult(Node[,] visited, Node start, Node end)
   {
      int distance = 0;
      Node curr = new Node(end);
      while(   curr.x != start.x || curr.y != start.y)
      { 
         if(curr.x == -1) //Catch no path exception
            return int.MaxValue;

         curr = visited[curr.y, curr.x];
         distance++;
      }

      return distance;
   }


   private void visit(Queue<(Node, Node)> q, Node curr, Node dest)
   {
      if( canVisit(curr, dest) )
         q.Enqueue(   (new Node(curr),  new Node(dest)  ) );
   }

   private bool canVisit(Node start, Node dest)
   {
      if(inRange(start) && inRange(dest) && validHeightDifference(start, dest))
         return true;
      
      return false;
   }

   private bool validHeightDifference(Node start, Node dest)
   {
      int startElevation = getHeight(start);
      int destElevation = getHeight(dest);

      if(destElevation <= startElevation + 1)
         return true;

      return false;
   }

   private int getHeight(Node start)
   {
      int elevation = (int) data[start.y][start.x];

      if(elevation == 'S')
         elevation = 'a';
      else if(elevation == 'E')
         elevation = 'z';

      elevation -= 97;

      return elevation;
   }

   public override void displayResults()
   {
      Console.WriteLine("results: {0}", result);
   }

   private bool inRange(int x, int y)
   {
      if (x >= 0 && x < width &&
          y >= 0 && y < height)
         return true;

      return false;
   }

   private bool inRange(Node node)
   {
      return inRange(node.x, node.y);
   }
}

