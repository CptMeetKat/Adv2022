public abstract class AocMachine 
{
   protected List<string> data;
   protected string filename;
   protected int result;

   public AocMachine(string _filename)
   {
      data = new List<string>();
      filename = _filename;
   }

   
   protected void readData(string filename)
   {
      string[] fileData = File.ReadAllLines(filename);
      foreach (string s in fileData)
      {
         data.Add(s);
      }
   }

   public string identify()
   {
      return this.GetType().Name + " " + filename;
   }

   public abstract void run(); 
   public abstract void displayResults();
}