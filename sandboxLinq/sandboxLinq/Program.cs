using System;

namespace sandboxLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            FilterData filterData = new FilterData();
            filterData.CreateNewStudentList(30);

            filterData.GetStudentsWithName("Svoboda");
            filterData.CreateStudentsGroupByClub();
            filterData.GetStudentsWithDiscount();
            filterData.GetVIPSeasonTicketFans();
            filterData.GetTeamWithMostUltraFans();
        }
    }
}
