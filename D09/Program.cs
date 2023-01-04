

List<AocMachine> machines = new List<AocMachine>();

machines.Add(new D09P1("input"));
// machines.Add(new D09P1("input2"));


machines.Add(new D09P2("input"));
// machines.Add(new D09P2("input2"));
// machines.Add(new D09P2("input3"));



foreach (var m in machines)
   m.run();  