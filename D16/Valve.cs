public class Valve
{
   public string id = "";
   public int flowrate = 0;
   public List<string> tunnels = new List<string>();

   public Valve(string id, int flowrate, List<string> tunnels)
   {
      this.id = id;
      this.flowrate = flowrate;
      foreach (string tunnelID in tunnels)
         this.tunnels.Add(tunnelID);
   }
   
   public Valve()
   {
   }

   public override int GetHashCode()
   {
      return id.GetHashCode();
   }

   public override bool Equals(Object? obj)
   {
      //Check for null and compare run-time types.
      if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
      {
         return false;
      }
      else {
         Valve v = (Valve) obj;
         return (flowrate == v.flowrate) && (id == v.id);
      }
   }
}