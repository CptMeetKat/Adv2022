
public class Coordinate
{
   public int x = -1;
   public int y = -1;
   public Coordinate()
   {
   }

   public Coordinate(int x, int y)
   {
      this.x = x;
      this.y = y;
   }

   public Coordinate(Coordinate c)
   {
      setCoordinate(c);
   }

   public override int GetHashCode()
   {
      return (x * y) * 13;
   }

   public override bool Equals(Object? obj)
   {
      //Check for null and compare run-time types.
      if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
      {
         return false;
      }
      else {
         Coordinate c = (Coordinate) obj;
         return (x == c.x) && (y == c.y);
      }
   }

   public void setCoordinate(Coordinate c)
   {
      this.x = c.x;
      this.y = c.y;
   }

}