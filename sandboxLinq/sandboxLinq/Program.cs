using System;

namespace sandboxLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            FilterData filterData = new FilterData(1000);

            filterData.GetStudentsWithName("Bartoň");
            //filterData.CreateStudentsGroupByClub();
            //filterData.GetStudentsWithDiscount();
            //filterData.GetVIPSeasonTicketFans();
            //filterData.GetTeamWithMostUltraFans();
        }
    }
}
