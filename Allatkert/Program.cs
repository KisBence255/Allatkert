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

Console.Write("dolgzoo neve:");
string dNev = Console.ReadLine();

var idosallat = allatok
    .Where(a => a.Gondozo.Nev.Contains(dNev))
    .MaxBy(a => a.Eletkor);


Console.WriteLine($"Legidosebb alata: {idosallat}");