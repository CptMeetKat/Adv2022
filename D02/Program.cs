

List<AocMachine> machines = new List<AocMachine>();
machines.Add(new D02P1("input"));
machines.Add(new D02P2("input"));


foreach (var m in machines)
   m.run();  