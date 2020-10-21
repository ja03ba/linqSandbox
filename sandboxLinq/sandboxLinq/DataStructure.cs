using System;

namespace sandboxLinq
{
    // Tento soubor obsauje vsechny tridy a vycty, ktere budou 
    // udrzovat data o studentovi. Instance studenta budou ulozeny
    // v List<Student>, se kterym se bude manipulovat prostrednictvim LINQu.

    class Student
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
        public FootballFan Fan { get; set; }
    }

    class FootballFan
    {
        public int FootballFanSinceYear { get; set; }
        public FootballClub FavoriteClub { get; set; }
        public int VisitsThisSeason { get; set; }
        public bool IsUltraFan { get; set; }
        public SeasonTicket SeasonTicket { get; set; }
    }

    class SeasonTicket
    {
        public DateTime Expires { get; set; }
        public TicketClass Class { get; set; }
    }

    enum FootballClub
    {
        Slavia, Sparta, Plzen, Olomouc, Jablonec, Banik, Karvina, Bohemians
    }

    enum TicketClass
    {
        VIP, Standart
    }
}