
public class Scanner
{
   public Coordinate beacon;
   public Coordinate position;
   public int distanceToBeacon = -1;
   
   public Scanner(Coordinate position, Coordinate beacon)
   {
      this.position = new Coordinate(position);
      this.beacon = new Coordinate(beacon);
      this.distanceToBeacon = calcDistanceToBeacon();
   }

   private int calcDistanceToBeacon()
   {
      return position.calcEuclideanDistance(beacon);
   }

   public IEnumerable<Coordinate> trace(Coordinate c)
   {
      c.setCoordinate(position);
      c.x -= distanceToBeacon+1;
      foreach (Coordinate it in step(c, 1, -1, distanceToBeacon+1))
         yield return it;
      foreach (Coordinate it in step(c, 1, 1, distanceToBeacon+1))
         yield return it;
      foreach (Coordinate it in step(c, -1, 1, distanceToBeacon+1))
         yield return it;
      foreach (Coordinate it in step(c, -1, -1, distanceToBeacon+1))
         yield return it;
   }

   private IEnumerable<Coordinate> step(Coordinate c, int xMod, int yMod, int steps)
   {
      for (int i = 0; i < steps; i++)
      {
         c.x += xMod;
         c.y += yMod;
         yield return c;
      }
   }

}
