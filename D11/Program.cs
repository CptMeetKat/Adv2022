

List<AocMachine> machines = new List<AocMachine>();

machines.Add(new Part1.D11P1("input"));
machines.Add(new Part1.D11P1("input2"));



// machines.Add(new D11P2("input"));
machines.Add(new Part2.D11P2("input2"));

foreach (var m in machines)
   m.run();  