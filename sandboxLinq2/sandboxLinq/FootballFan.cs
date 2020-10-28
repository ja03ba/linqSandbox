using System;

namespace sandboxLinq
{
    class FootballFan
    {
        public int FootballFanSinceYear { get; set; }
        public FootballClub FavoriteClub { get; set; }
        public int VisitsThisSeason { get; set; }
        public bool IsUltraFan { get; set; }
        public SeasonTicket SeasonTicket { get; set; }
        public enum FootballClub
        {
            Slavia, Sparta, Plzen, Olomouc, Jablonec, Banik, Karvina, Bohemians
        }
    }
}
