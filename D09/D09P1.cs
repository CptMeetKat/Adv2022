public class D09P1 : AocMachine 
{
   HashSet<string> visited = new HashSet<string>();

   public D09P1(string filename) : base(filename)
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
      Node head = new Node(0,0);
      Node tail = new Node(0,0);
      addVisited(tail.x, tail.y);

      foreach(var s in data)
      {
         string[] atoms = s.Split(' ');

         string direction = atoms[0];
         int steps = int.Parse(atoms[1]);

         if(direction == "U")
            stepUp(head, tail, steps);
         if(direction == "D")
            stepDown(head, tail, steps);
         if(direction == "L")
            stepLeft(head, tail, steps);
         if(direction == "R")
            stepRight(head, tail, steps);
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

   private void adjustTail(Node head, Node tail)
   {

      if(tail.calcEuclideanDistance(head) >= 2)
      {

         if(tail.x == head.x)
         {
            tail.y = tail.y + getDirection(tail.y, head.y);
         }
         else if(tail.y == head.y)
         {
            tail.x = tail.x + getDirection(tail.x, head.x);
         }
         else
         {
            tail.x = tail.x + getDirection(tail.x, head.x);
            tail.y = tail.y + getDirection(tail.y, head.y);
         }
      }

      addVisited(tail.x, tail.y);
   }

   private void addVisited(int x, int y)
   {
      visited.Add(x.ToString() + "," + y.ToString());
   }
   
   private void stepLeft(Node head, Node tail, int steps)
   {
      for(int i = 0; i < steps; i++)
      {
         head.x--;
         adjustTail(head, tail);
      }
   }

   private void stepRight(Node head, Node tail, int steps)
   {
      for(int i = 0; i < steps; i++)
      {
         head.x++;
         adjustTail(head, tail);
      }
   }

   private void stepUp(Node head, Node tail, int steps)
   {
      for(int i = 0; i < steps; i++)
      {
         head.y--;
         adjustTail(head, tail);
      }
   }

   private void stepDown(Node head, Node tail, int steps)
   {
      for(int i = 0; i < steps; i++)
      {
         head.y++;
         adjustTail(head, tail);
      }
   }
 
   public override void displayResults()
   {
      Console.WriteLine("results: {0}", result);
   }
}

