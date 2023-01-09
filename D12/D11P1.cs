

public class D12P1 : AocMachine 
{
   Node start = new Node();
   Node end = new Node();

   int height = -1;
   int width = -1;

   public D12P1(string filename) : base(filename)
   {
   }   

   public override void run()
   {
      readData(filename);
      init();
      climbHill();
      displayResults();
   }
   
   private void init()
   {
      setHeightWidth();
      setStartEnd();
   }

   private void setHeightWidth()
   {
      height = data.Count;
      width = data[0].Length;
   }

   private void setStartEnd()
   {
      for (int y = 0; y < height; y++)
      {
         for (int x = 0; x < width; x++)
         {
            if(data[y][x] == 'S')
            {
               start.x = x;
               start.y = y;
            }
            if(data[y][x] == 'E')
            {
               end.x = x;
               end.y = y;
            }
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

   public void climbHill()
   {
      Queue<(Node, Node)> q = new Queue<(Node, Node)>();
      Node[,] visited = initVisited();

      q.Enqueue(   ( new Node(-1, -1), start )   );
      // visited[start.y, start.x].x = 0;
      // visited[start.y, start.x].y = 0;

      while(q.Count > 0)
      {
         (Node, Node) cand = q.Dequeue();
         Node prev = cand.Item1;
         Node curr = cand.Item2;

         // Console.WriteLine("{0} {1}", curr.x, curr.y);
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


      result = calcResult(visited);
   }


   private int calcResult(Node[,] visited)
   {
      // Console.WriteLine("calcresult: ");
      int result = 0;
      Node curr = new Node(end);
      while(   curr.x != start.x || curr.y != start.y)
      { 
         // Console.WriteLine("{0} {1}", curr.x, curr.y);
         curr = visited[curr.y, curr.x];
         result++;
      }

      return result;
   }


   private void visit(Queue<(Node, Node)> q, Node curr, Node dest)
   {
      if( canVisit(curr, dest) )
         q.Enqueue(   (new Node(curr),  new Node(dest)  ) );
   }

   private bool canVisit(Node start, Node dest)
   {
      // Console.WriteLine(" {0} {1} {2} ", inRange(start), inRange(dest), validHeightDifference(start, end));
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
