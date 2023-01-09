
public class Node
{
   public int x = -1;
   public int y = -1;
   public Node()
   {
   }

   public Node(int x, int y)
   {
      this.x = x;
      this.y = y;
   }

   public Node(Node node)
   {
        this.x = node.x;
        this.y = node.y;
   }

}