using System;
using System.Linq;
using System.Collections.Generic;

namespace sandboxLinq
{
    class FilterData
    {
        private GenerateData generateData;
        private List<Student> students;

        //vola metodu GenerateData.CreateList()
        public FilterData()
        {
            generateData = new GenerateData();
            students = generateData.CreateList();
        }

        //vola metodu GenerateData.CreateList(int numberOfPeople)
        public FilterData(int numberOfPeople)
        {
            generateData = new GenerateData();
            students = generateData.CreateList(numberOfPeople);
        }

        // metody obsahujici LINQ dotazy
        public void GetStudentsWithName(string surname)
        {
            var query = from student in students
                        where student.Surname == surname
                        orderby student.Name ascending
                        select new { FullName = student.Name + " " + student.Surname, student.BirthDate };

            foreach (var student in query.Take(5))
            {
                Console.WriteLine("{0}, narozen {1}", student.FullName, student.BirthDate.ToString("dd/MMMM yyyy"));
            }
        }
        public void CreateStudentsGroupByClub()
        {
            var query = from student in students
                        select new { FullName = student.Name + " " + student.Surname, FavClub = student.Fan.FavoriteClub }
                        into editedStudent
                        group editedStudent by editedStudent.FavClub
                        into fanGroups
                        select new { students = fanGroups, fanGroups.Key, Count = fanGroups.Count() };

            foreach (var fanGroup in query)
            {
                Console.WriteLine("\n=== Tým: {0}, počet fanoušků: {1} ===", fanGroup.Key, fanGroup.Count);

                foreach (var student in fanGroup.students)
                {
                    Console.WriteLine(student.FullName);
                }
            }
        }
        public void GetStudentsWithDiscount()
        {
            var query = from student in students
                        where student.Age >= 18 && student.Fan.VisitsThisSeason > 40
                        select new { FullName = student.Name + " " + student.Surname, student.Age, student.Fan.VisitsThisSeason };

            foreach (var student in query)
            {
                Console.WriteLine("{0} ({1}), počet návšěv: {2}", student.FullName, student.Age, student.VisitsThisSeason);
            }
        }
        public void GetVIPSeasonTicketFans()
        {
            var query = from student in students
                        where student.Fan.SeasonTicket.Expires.Year >= 2020 &&
                        student.Fan.SeasonTicket.Class == SeasonTicket.TicketClass.VIP
                        select new
                        {
                            FullName = student.Name + " " + student.Surname,
                            FavClub = student.Fan.FavoriteClub,
                            TicketExpYear = student.Fan.SeasonTicket.Expires.Year
                        };

            foreach (var student in query)
            {
                Console.WriteLine("{0}, {1}, vyprší v roce {2}", student.FullName, student.FavClub, student.TicketExpYear);
            }
        }
        public void GetTeamWithMostUltraFans()
        {
            var fanGroups = (from student in students
                             where student.Fan.IsUltraFan == true
                             group student by student.Fan.FavoriteClub).ToList();

            int maxCount = (from fanGroup in fanGroups
                            select fanGroup.Count()).Max();

            var query = from fanGroup in fanGroups
                        where fanGroup.Count() == maxCount
                        select new { fanGroup.Key, Count = fanGroup.Count() };

            Console.WriteLine("\n=== Klub s nejvíce ultra fanoušky ===");
            foreach (var group in query)
            {
                Console.WriteLine("{0}, počet: {1}", group.Key, group.Count);
            }
        }
    }
}
