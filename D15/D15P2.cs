
public class D15P2 : AocMachine 
{
   List<Scanner> scanners = new List<Scanner>();
   HashSet<Coordinate> beacons = new HashSet<Coordinate>();

   new ulong result = 0;

   public D15P2(string filename) : base(filename) { }   

   public override void run()
   {
      readData(filename);
      parseData();
      findFirstAvailablePosition(new Coordinate(0, 0), new Coordinate(4000000, 4000000));
      
      // findFirstAvailablePosition(new Coordinate(0, 0), new Coordinate(20, 20));
      // findDistressBeacon(new Coordinate(0, 0), new Coordinate(20, 20));

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

   public void findFirstAvailablePosition(Coordinate first, Coordinate last)
   {
      ulong tuningFreq = 0;
      bool isDone = false;

      for (int i = 0; i < scanners.Count && !isDone; i++)
      {
         Coordinate c = new Coordinate();
         foreach (Coordinate item in scanners[i].trace(c))
         {
            if ( positionAvailable(c) && c.inRange(first,last))
            {
               // Console.WriteLine("FOUND! {0} {1}", c.x, c.y);
               tuningFreq =  (  (ulong) item.x * (ulong) 4000000  ) + (ulong)item.y;
               isDone = true;
               break;
            } 
         }
      }

      result = tuningFreq;
   }

   private bool positionAvailable(Coordinate position )
   {
      foreach (Scanner s in scanners)
      {
         if(position.calcEuclideanDistance(s.position) <= s.distanceToBeacon || beacons.Contains(position))
            return false;
      }

      return true;
   }


   //Brute Force //too slow
   // public void findDistressBeacon(Coordinate first, Coordinate last)
   // {
   //    ulong tuningFreq = 0;
   //    Coordinate it = new Coordinate();
   //    for (int y = first.y; y <= last.y; y++)
   //    {
   //       it.y = y;

   //       for (int x = first.x; x <= last.x; x++)
   //       {
   //          it.x = x;
   //          bool found = true;
   //          foreach (Scanner s in scanners)
   //          {
   //             if(it.calcEuclideanDistance(s.position) <= s.distanceToBeacon || beacons.Contains(it))
   //             {
   //                found = false;
   //                break;
   //             }
   //          }
   //          if(found)
   //          {
   //             Console.WriteLine("FOUND old: {0} {1}", x, y);
   //             tuningFreq = ((ulong)x * (ulong)4000000) + (ulong)y;
   //          }
   //       }
   //    }

   //    result = tuningFreq;
   // }

   public override void displayResults()
   {
      Console.WriteLine("results: {0}", result);
   }
}
