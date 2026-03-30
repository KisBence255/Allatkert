using System.Text;

const string AllatokTxt = "..\\..\\..\\data\\allatok.txt";

List<Reszleg> reszlegek = [];
List<Gondozo> gondozok = [];
List<Allat> allatok = [];


#region beolvasas
using StreamReader sr = new(AllatokTxt,Encoding.UTF8);

_= sr.ReadLine();
while (!sr.EndOfStream)
{
    string[] t = sr.ReadLine().Split(';');

    Reszleg? reszleg = reszlegek.SingleOrDefault(r => r.Id == int.Parse(t[4]));
    if(reszleg is null)
    {
        reszleg = new()
        {
            Id = int.Parse(t[4]),
            Nev =   t[5],
            Meret = int.Parse(t[6]),
        };
        reszlegek.Add(reszleg);
    }

    Gondozo? gondozo = gondozok.SingleOrDefault(g=> g.Id == int.Parse(t[7]));
    if(gondozo is null)
    {
        gondozo = new()
        {
            Id = int.Parse(t[7]),
            Nev = t[8],
            szakterulet = t[9],
        };
        gondozok.Add(gondozo);
    }

    allatok.Add(new()
    {
        Id = int.Parse(t[0]),
        Nev = t[1] == "NULL" ? null : t[1],
        Faj = t[2],
        Eletkor = float.Parse(t[3].Replace('.',',')),
        Gondozo = gondozo,
        Reszleg = reszleg
    });
}

#endregion




Console.WriteLine($"állatok száma: {allatok.Count()}");
Console.WriteLine($"gondozok száma: {gondozok.Count()}");
Console.WriteLine($"reszlegek száma: {reszlegek.Count()}");




//dolgozo legidosebb allata

//Console.Write("dolgzoo neve:");
//string dNev = Console.ReadLine();

//var idosallat = allatok
//    .Where(a => a.Gondozo.Nev.Contains(dNev))
//    .MaxBy(a => a.Eletkor);


//Console.WriteLine($"Legidosebb allata: {idosallat}");



//-oroszlanok atlagos eletkora

var oroszlanokkor = allatok.Where(a => a.Faj == "Oroszlán").Average(a=>a.Eletkor);
Console.WriteLine("Oroszlánok átlagos életkora: "+oroszlanokkor);



//-nevvel rendelkezo allatok szama
var nevallatok = allatok.Where(a => a.Nev != null).Count();

Console.WriteLine("Nevvel rendelkező állatok száma: "+nevallatok);

//- 2 évesnél idosebb lakok a majom-szigeten
var majomsziget = allatok.Where(a => a.Reszleg.Nev == "Majom-sziget" && a.Eletkor > 2).Count();

Console.WriteLine("2 évesnél idősebb majom szigeten lakok:" + majomsziget);


//- allatok szama gondozonkent

var gondozonkent = allatok.GroupBy(a => a.Gondozo.Nev)
    .Select(g => new
    {
        GondozoNev = g.Key,
        AllatokSzama = g.Count()
    });




foreach (var item in gondozonkent)
{
    Console.WriteLine($" \n Gondozó: {item.GondozoNev}, Állatok száma: {item.AllatokSzama}");
}



//- nagy anna altal gondozott allatfajok szama

var nagyanna = allatok.Where(a => a.Gondozo.Nev == "Nagy Anna").Select(a => a.Faj).Distinct().Count();

Console.WriteLine("Nagy anna altal gonzott fajok szama:"+nagyanna);


//- 2000 nm alatti alapteruletu reszlegen ello allatfajok

var reszleg2000 = allatok.Where(a => a.Reszleg.Meret < 2000).Select(a => a.Faj).Distinct();

Console.WriteLine("2000 nm alatti reszlegen elo allatfajok: ");
foreach (var item in reszleg2000)
{
    Console.WriteLine("\t" + item);
}

//- reszlegek nepsurusege
var nepsuruseg = allatok.GroupBy(a => a.Reszleg.Nev)
    .Select(g => new
    {
        ReszlegNev = g.Key,
        Nepsuruseg = g.Count() / (float)reszlegek.Single(r => r.Nev == g.Key).Meret
    });
foreach (var item in nepsuruseg)
{
    Console.WriteLine($"Részleg: {item.ReszlegNev}, Népsűrűség: {item.Nepsuruseg:0.00} állat/nm");
}

//- vizi allatokkal foglalkozo gondozo allatainak reszlegnevei

Console.WriteLine("Vízi állatok gondozójának részlegei:");
var vizigondozo = allatok
   .Where(a => a.Gondozo.szakterulet == "Vízi állatok")
   .Select(a => a.Reszleg.Nev)
   .Distinct();

foreach (var r in vizigondozo)
    Console.WriteLine($"\t{r}");


//- <input> allatfaj gondozojanak neve

Console.Write("Állatfaj neve:");
var fajnev = Console.ReadLine();

var gondozoNeve = allatok
    .Where(a => a.Faj.Contains(fajnev))
    .Select(a => a.Gondozo.Nev)
    .FirstOrDefault();
Console.WriteLine(gondozoNeve);


