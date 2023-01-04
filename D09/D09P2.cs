//Can probably make the size of tail more generic to improve code quality

public class D09P2 : AocMachine 
{
   HashSet<string> visited = new HashSet<string>();

   public D09P2(string filename) : base(filename)
   {
   }   

   public override void run()
   {
      readData(filename);
      traverseRope();
      displayResults();
   }

   private void traverseRope()
   {
      // Node head = new Node(0,0);
      Node[] body = new Node[10];
      for (int i = 0; i < body.Length; i++)
         body[i] = new Node();
      
      addVisited(0,0);

      foreach(var s in data)
      {
         string[] atoms = s.Split(' ');

         string direction = atoms[0];
         int steps = int.Parse(atoms[1]);

         if(direction == "U")
            stepUp(body, steps);
         if(direction == "D")
            stepDown(body, steps);
         if(direction == "L")
            stepLeft(body, steps);
         if(direction == "R")
            stepRight(body, steps);
      }

      result = visited.Count;
   }
    
   private int getDirection(int a, int b)
   {
      if(a < b)
         return 1;
      if(a > b)
         return -1;
      
      return 0;
   }

   private void adjustTail(Node[] body)
   {
      for (int i = 1; i < body.Length; i++)
      {
         Node t = body[i];
         Node front = body[i-1];
         

         if(t.calcEuclideanDistance(front) >= 2)
         {

            if(t.x == front.x)
            {
               t.y = t.y + getDirection(t.y, front.y);
            }
            else if(t.y == front.y)
            {
               t.x = t.x + getDirection(t.x, front.x);
            }
            else
            {
               t.x = t.x + getDirection(t.x, front.x);
               t.y = t.y + getDirection(t.y, front.y);
            }
         }
         

      }
      addVisited(body[9].x, body[9].y);

      
   }

   private void addVisited(int x, int y)
   {
      visited.Add(x.ToString() + "," + y.ToString());
   }
   
   private void stepLeft(Node[] body, int steps)
   {
      for(int i = 0; i < steps; i++)
      {
         body[0].x--;
         adjustTail(body);
      }
   }

   private void stepRight(Node[] body, int steps)
   {
      for(int i = 0; i < steps; i++)
      {
         body[0].x++;
         adjustTail(body);
      }
   }

   private void stepUp(Node[] body, int steps)
   {
      for(int i = 0; i < steps; i++)
      {
         body[0].y--;
         adjustTail(body);
      }
   }

   private void stepDown(Node[] body, int steps)
   {
      for(int i = 0; i < steps; i++)
      {
         body[0].y++;
         adjustTail(body);
      }
   }
 
   public override void displayResults()
   {
      Console.WriteLine("results: {0}", result);
   }
}

