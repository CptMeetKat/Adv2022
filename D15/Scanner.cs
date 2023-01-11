
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

}
