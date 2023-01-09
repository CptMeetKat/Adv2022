

List<AocMachine> machines = new List<AocMachine>();


machines.Add(new D12P1("input"));


machines.Add(new D12P2("input"));

foreach (var m in machines)
   m.run();  
