

List<AocMachine> machines = new List<AocMachine>();

machines.Add(new D10P1("input"));
machines.Add(new D10P1("input3"));


machines.Add(new D10P2("input"));
machines.Add(new D10P2("input3"));



foreach (var m in machines)
   m.run();  