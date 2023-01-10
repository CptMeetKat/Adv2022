
public class D14P2 : AocMachine 
{

   Dictionary<Coordinate, int> map = new Dictionary<Coordinate, int>();
   Coordinate sandGenerator = new Coordinate(500, 0);

   int floor = -1;

   public D14P2(string filename) : base(filename)
   {
   }   

   public override void run()
   {
      readData(filename);
      generateMap();
      initFloorLevel();
      // printMapData();
      generateFloor();
      generateFallingSand();

      countSand();
      printMap(490, 0, 50);
      displayResults();
   }

   private void generateFloor()
   {
      drawLine(new Coordinate(0, floor), new Coordinate(1000, floor));
   }

   private void initFloorLevel()
   {
      //Higher Y is deeper
      int lowestFloor = 0;
      foreach (var k in map)
      {
         if(k.Value == 1)
            lowestFloor = Math.Max(lowestFloor, k.Key.y);
      }
      floor = lowestFloor + 2;
   }

   private void countSand()
   {
      int totSand = 0;
      foreach (var k in map)
      {
         if(k.Value == 2)
            totSand++;
      }
      result = totSand;
   }

   private void printMapData()
   {
      foreach (var k in map)
      {
         Console.WriteLine("{0} {1} {2}" ,k.Key.x, k.Key.y, k.Value);  
      }
   }

   private void generateMap()
   {
      Coordinate start = new Coordinate();
      Coordinate last = new Coordinate();
      foreach(string line in data)
      {
         string[] atoms = line.Split("->");
         for (int i = 1; i < atoms.Length; i++)
         {
            string[] pair = atoms[i].Trim().Split(',');
            start.x = int.Parse(pair[0]);
            start.y = int.Parse(pair[1]);            

            pair = atoms[i-1].Trim().Split(',');
            last.x = int.Parse(pair[0]);
            last.y = int.Parse(pair[1]);            

            drawLine(start, last);
         }
      }

      


   }

   public void printMap(int startx, int starty, int squareSize = 20)
   {
      Coordinate c = new Coordinate();
      string s = "";
      for (int y = starty; y < squareSize + starty; y++)
      {
         c.y = y;
         for (int x = startx; x < squareSize + startx; x++)
         {
            c.x = x;
            // Console.WriteLine("{0} {1}", c.x, c.y);
            if(map.ContainsKey(c))
            {
               if(map[c] == 1)
                  s += '#';
               else if(map[c] == 2)
                  s += 'o';                           
            }
            else
               s += '.';
         }
         s += '\n';
      }

      Console.WriteLine(s);

   }
   

   public void generateFallingSand()
   {
      int maxAbyss = 500;
      int freefalls = 0;

      while(freefalls < maxAbyss)
      {
         Coordinate sand = new Coordinate(sandGenerator.x, sandGenerator.y);

         freefalls = 0;
         bool isSandDone = false;
         while(!isSandDone && freefalls < maxAbyss)
         {
            isSandDone = moveSand(sand);

            if(isSandDone) 
            {
               if(! map.ContainsKey(sand) ) 
                  map.Add(sand, 2);
            }
            else
               freefalls++;

            if(sandGenerator.Equals(sand))
               freefalls = int.MaxValue; //End Early

         }
      }
   }

   private bool moveSand(Coordinate sand)
   {
      //return true if sand stops
      //returns false if sand has not stopped

      Coordinate cursor = new Coordinate(sand);
      
      cursor.y++;
      if(!  map.ContainsKey(cursor)  )
      {
         sand.y = cursor.y;
         return false;
      }

      cursor.x--;
      if(!  map.ContainsKey(cursor)  )
      {
         sand.y = cursor.y;
         sand.x = cursor.x;
         return false;
      }

      cursor.x += 2;
      if(!  map.ContainsKey(cursor)  )
      {
         sand.y = cursor.y;
         sand.x = cursor.x;
         return false;
      }

      // cursor.x -= 1;
      // if(!  map.ContainsKey(cursor)  )
      // {
      //    sand.y = cursor.y;
      // }

      

      return true;
   }

   private void drawLine(Coordinate start, Coordinate last)
   {
      if(start.x == last.x)
         drawVerticalLine(start, last);
      else
         drawHorizontalLine(start, last);
   }

   private void drawVerticalLine(Coordinate c1, Coordinate c2)
   {
      int x = c1.x;
      int y_start = Math.Min(c1.y, c2.y);
      int y_end = Math.Max(c1.y, c2.y);

      for (int y = y_start; y <= y_end; y++)
      {
         Coordinate point = new Coordinate(x, y);

         if(!map.ContainsKey(point))
            map.Add(point, 1);
      }
      
      //0 is void
      //1 is value for block
      //2 is sand
   }

   private void drawHorizontalLine(Coordinate c1, Coordinate c2)
   {
      int y = c1.y;
      
      int x_start = Math.Min(c1.x, c2.x);
      int x_end = Math.Max(c1.x, c2.x);

      for (int x = x_start; x <= x_end; x++)
      {
         Coordinate point = new Coordinate(x, y);

         if(!map.ContainsKey(point))
            map.Add(point, 1);
      }
   }

   public override void displayResults()
   {
      Console.WriteLine("results: {0}", result);
   }
}

