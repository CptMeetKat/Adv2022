
public class Node
{
   public int x = 0;
   public int y = 0;
   
   public Node(int x, int y)
   {
      this.x = x;
      this.y = y;
   }

   public Node()
   {
   }

   public int calcEuclideanDistance(Node target)
   {
      return (int)(Math.Sqrt(          Math.Pow(this.x - target.x, 2) + Math.Pow(this.y - target.y, 2)     ));

      // return (Math.Sqrt(          Math.Pow(0 - 0, 2) + Math.Pow(1 - 1, 2)     ));
   }

}