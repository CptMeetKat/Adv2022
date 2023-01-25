

List<AocMachine> machines = new List<AocMachine>();


// machines.Add(new D16P1("input"));  
// machines.Add(new D16P1("input2"));



machines.Add(new D16P2("input"));
// machines.Add(new D16P2("input2"));


foreach (var m in machines)
   m.run();  


   //2171 is too high
   //2167 is too high
   //error was starting from the wrong ID lmfao

   //if manually adding everything appears correct then the path is being overvalued



//    NV NV+ YB PS PS+ VU FX FX+ HY JM JM+ QV KZ KZ+ CK UX UX+ CZ DO DO+ OW PH PH+ KW DG DG+ DF RG RG+ DF
// 30 29  28 27 26  25 24 23  22 21 20  19 18 17  16 15 14  13 12 11  10 9  8   7  6  5   4  3  2   1
    



// NV+  5 * 29
// PS+  18 * 26
// FX+  23 * 23
// JM+  19 * 20
// KZ+  12 * 17
// UX+  13 * 14
// DO+  14 * 11
// PH+  7  * 8
// DG+  9  * 5
// RG+  4 * 2


// (5 * 29 )+(18 * 26 )+(23 * 23 )+(19 * 20 )+(12 * 17 )+(13 * 14 )+(14 * 11 )+(7  * 8 )+(9  * 5 )+(4 * 2)


//2109 is too low