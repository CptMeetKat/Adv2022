using System.Text.Json;
using System.Text.Json.Nodes;

public class D13P1 : AocMachine 
{

   public D13P1(string filename) : base(filename)
   {
   }   

   public override void run()
   {
      readData(filename);
      determineListsInCorrectOrder();
      displayResults();
   }
   
   

   public void determineListsInCorrectOrder()
   {
      int loop = 1;
      for (int i = 0; i < data.Count; i+=3)
      {
         var list1 = JsonDocument.Parse(data[i]).RootElement;
         var list2 = JsonDocument.Parse(data[i+1]).RootElement;

         // JsonElement list1 = JsonDocument.Parse("[[1,2,3,[5]],[2,3,4]]").RootElement;
         // JsonElement list2 = JsonDocument.Parse("[1,4]").RootElement;         

         // JsonElement list1 = JsonDocument.Parse("[1,1,3,1,1]").RootElement;
         // JsonElement list2 = JsonDocument.Parse("[1,1,5,1,1]").RootElement;
         
         int order = isListInOrder(list1, list2);
         Console.WriteLine("PAIR: {0}", loop);
         if ( order == -1 )
         {
            Console.WriteLine("loop {0}", loop);
            result += loop;
         }
         else if(order == 1){}
         else
         {
            Console.WriteLine("??? List's were equivalent, this is weird");
         }
         
         loop++;
      }   
   }


   private int isListInOrder(JsonElement a, JsonElement b)
   {
      Console.WriteLine("A: {0} \nB: {1}", a, b);

      if(a.ValueKind.ToString() == "Array" && b.ValueKind.ToString() == "Array")
         return isArrayArrayInOrder(a,b);
      else if(a.ValueKind.ToString() == "Array" && b.ValueKind.ToString() == "Number")
         return isArrayNumberInOrder(a,b);
      else if(a.ValueKind.ToString() == "Number" && b.ValueKind.ToString() == "Array")
         return isNumberArrayInOrder(a,b);
         
         
      Console.WriteLine("This should not be reached weirdx2");
      
      return 0;
   }

   private int isArrayNumberInOrder(JsonElement a, JsonElement b)
   {
      //Im thinking check if Array is List containing List,
      if(a.GetArrayLength() <= 0)
         return -1;

      if(a[0].ValueKind.ToString() == "Array")
         return isListInOrder(a[0], b);


      if(a[0].GetInt32() < b.GetInt32())
         return -1;
      else if(a[0].GetInt32() > b.GetInt32())
         return 1;

      if(a.GetArrayLength() > 1)
         return 1;

      Console.WriteLine("xx returning 0");
      return 0;
   }

   private int isNumberArrayInOrder(JsonElement a, JsonElement b)
   {
      if(b.GetArrayLength() <= 0)
         return 1;


      if(b[0].ValueKind.ToString() == "Array")
         return isListInOrder(a, b[0]);

      if(b[0].GetInt32() < a.GetInt32())
         return 1;
      else if(b[0].GetInt32() > a.GetInt32())
         return -1;

      if(b.GetArrayLength() > 1)
         return -1;

      Console.WriteLine("zz returning 0");
      return 0;
   }

   private int isArrayArrayInOrder(JsonElement a, JsonElement b)
   {
      int i = 0;
      while(true)
      {
         //This process SHOULD determine a lists order, and no equal list exception is required

            if (i >= a.GetArrayLength() && i >= b.GetArrayLength())
            {
               return 0;
            }
            else if(i >= a.GetArrayLength()) //A runs out first
               return -1;
            else if(i >= b.GetArrayLength()) //B runs out first
               return 1;

         if(a[i].ValueKind.ToString() == "Number" && b[i].ValueKind.ToString() == "Number")
         {
            int valueA = a[i].GetInt32();
            int valueB = b[i].GetInt32();
            if(valueA < valueB)
               return -1;  
            else if(valueA > valueB)
               return 1;
         }
         else// if(a[i].ValueKind.ToString() == "Array" || b[i].ValueKind.ToString() == "Array")
         {
            int order = isListInOrder(a[i], b[i]);
            if(order != 0) 
               return order;
         }
         i++;
      }

   }


 

   public override void displayResults()
   {
      Console.WriteLine("results: {0}", result);
   }
}
