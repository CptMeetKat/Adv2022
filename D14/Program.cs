

List<AocMachine> machines = new List<AocMachine>();


machines.Add(new D14P1("input"));
// machines.Add(new D14P1("input2"));



// machines.Add(new D14P2("input2"));
machines.Add(new D14P2("input"));


foreach (var m in machines)
   m.run();  
