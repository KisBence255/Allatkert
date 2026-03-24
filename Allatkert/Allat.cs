
    class Allat
    {
        public int Id { get; set; }
        public string? Nev { get; set; }
        public string Faj { get; set; }
        public float Eletkor { get; set; }
        public Reszleg Reszleg { get; set; }
        public Gondozo Gondozo { get; set; }




    public override string ToString() =>
        $"\t {Faj} {(Nev is null ? "" : $"(neve: {Nev})")},{Eletkor:0.00} éves \n" +
        $"\t Részlege: {Reszleg.Nev} ({Reszleg.Meret} nm) \n" +
        $"\t Gondozoja: {Gondozo.Nev} ({Gondozo.szakterulet})";
    
    }
