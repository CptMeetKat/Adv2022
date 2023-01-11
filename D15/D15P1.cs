
public class D15P1 : AocMachine 
{
   List<Scanner> scanners = new List<Scanner>();
   HashSet<Coordinate> beacons = new HashSet<Coordinate>();

   public D15P1(string filename) : base(filename) { }   

   public override void run()
   {
      readData(filename);
      parseData();
      // findUnavailablePositions(10);
      findUnavailablePositions(2000000);

      // debugDistance();
      displayResults();
   }

   private void debugDistance()
   {
      foreach (var s in scanners)
         Console.WriteLine(s.distanceToBeacon);
   }

   private void parseData()
   {
      foreach (string s in data)
      {
         string [] atoms = s.Split(":");
         char[] deliminators = new char[] {' ',',', '='};

         string [] lAtoms = atoms[0].Split(deliminators);
         string [] rAtoms = atoms[1].Split(deliminators);

         int scannerX = int.Parse(  lAtoms[lAtoms.Length-4]  );
         int scannerY = int.Parse(  lAtoms[lAtoms.Length-1]  );
         int beaconX = int.Parse(   rAtoms[rAtoms.Length- 4] );
         int beaconY = int.Parse(   rAtoms[rAtoms.Length- 1] );

         // Console.WriteLine("{0} {1} {2} {3}", scannerX, scannerY, beaconX, beaconY);
         Scanner scanner = new Scanner(  new Coordinate(scannerX, scannerY),
                                   new Coordinate(beaconX, beaconY)  );
         scanners.Add( scanner );
         beacons.Add(new Coordinate(beaconX, beaconY));
      }
   }
   

   private int findFirstInspection()
   {
      int firstPosition = int.MaxValue;
      foreach (Scanner s in scanners)
      {
         firstPosition = Math.Min(firstPosition, s.position.x - s.distanceToBeacon);
      }
      return firstPosition;
   }

   private int findLastInspection()
   {
      int lastPosition = int.MinValue;
      foreach (Scanner s in scanners)
      {
         lastPosition = Math.Max(lastPosition, s.position.x + s.distanceToBeacon);
      }
      return lastPosition;
   }

   public void findUnavailablePositions(int targetRow)
   {
      int start = findFirstInspection();
      int last = findLastInspection();

      int totalUnavailable = 0;


      Coordinate it = new Coordinate();
      for (int i = start; i <= last; i++)
      {
         it.y = targetRow;
         it.x = i;

         foreach (Scanner s in scanners)
         {
            if(it.calcEuclideanDistance(s.position) <= s.distanceToBeacon && !beacons.Contains(it))
            {
               totalUnavailable++;
               break;
            }         
         }
      }

      result = totalUnavailable;
   }

   public override void displayResults()
   {
      Console.WriteLine("results: {0}", result);
   }
}
