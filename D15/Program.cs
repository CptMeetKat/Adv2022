

List<AocMachine> machines = new List<AocMachine>();


machines.Add(new D15P1("input"));
// machines.Add(new D15P1("input2"));


machines.Add(new D15P2("input"));
// machines.Add(new D15P2("input2"));


foreach (var m in machines)
   m.run();  

//1337958778 is too low