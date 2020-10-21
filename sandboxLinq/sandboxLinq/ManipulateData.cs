using System;
using System.Linq;
using System.Collections.Generic;

namespace sandboxLinq
{
    // trida urcena pro generovani nahodnych dat
    class GenerateData
    {
        // pole obsahujici jmena a prijmeni, v metode CreateList jsou pouzita pro vygenerovani nahodnych osob
        private string[] Names { get; }
        private string[] Surnames { get; }
        private Random Rnd { get; }

        public GenerateData()
        {
            Names = new string[] { "Roman", "Jiří", "Jan", "Ondřej", "Petr", "Pavel", "Jaroslav", "Martin", 
                "Tomáš", "Miroslav", "Lukáš", "František", "Radek", "Daniel", "Josef", "Marek", "Matyáš", "Patrik" };
            Surnames = new string[] { "Novák", "Svoboda", "Novotný", "Dvořák", "Černý", "Procházka", 
                "Kučera", "Veselý", "Bartoň", "Horák", "Němec", "Pospíšil", "Král", "Hájek", "Beneš", "Pokorný", 
                "Vrzal", "Bauer", "Malý" };
            Rnd = new Random();
        }

        // vygeneruje List<Student> o zadanem poctu lidi
        public List<Student> CreateList(int NumberOfPeople)
        {
            List<Student> students = new List<Student>();

            for (int x = 0; x < NumberOfPeople; x++)
            {
                int age = Rnd.Next(13, 26);

                students.Add(
                    new Student
                    {
                        Name = Names[Rnd.Next(Names.Length)],
                        Surname = Surnames[Rnd.Next(Surnames.Length)],
                        Age = age,
                        BirthDate = new DateTime(DateTime.Now.Year - age, Rnd.Next(1, 13), Rnd.Next(1, 29)),
                        Fan = new FootballFan
                        {
                            FootballFanSinceYear = DateTime.Now.Year - Rnd.Next(0, age - 5),
                            FavoriteClub = (FootballClub)Rnd.Next(Enum.GetNames(typeof(FootballClub)).Length),
                            IsUltraFan = Rnd.Next(0, 2) == 0,
                            VisitsThisSeason = Rnd.Next(0, 50),
                            SeasonTicket = new SeasonTicket
                            {
                                Expires = new DateTime(DateTime.Now.Year + Rnd.Next(-3, 4), 8, 22),
                                Class = (TicketClass)Rnd.Next(Enum.GetNames(typeof(TicketClass)).Length),
                            }
                        }
                    }
                );
            }

            return students;
        }
    }

    // trida urcena pro uchovavani metod, ktere vyuyivaji LINQ
    class FilterData
    {
        private GenerateData generateData;
        private List<Student> students;

        public FilterData()
        {
            generateData = new GenerateData();
        }

        // vytvori novy list studentu
        public void CreateNewStudentList(int NumberOfPeople)
        {
            students = generateData.CreateList(NumberOfPeople);
        }

        // metody obsahujici LINQ dotazy
        // nalezne prvnich 5 lidi se zadanym prijmenim, vysledni lide jsou serazeni vzestupne podle krestniho jmena
        public void GetStudentsWithName(string surname)
        {
            var query = from student in students
                        where student.Surname == surname
                        let fullname = student.Name + " " + student.Surname
                        orderby student.Name ascending
                        select new { FullName = fullname, student.BirthDate };

            Console.WriteLine("\n=== Lidé s příjmením {0} ===", surname);

            foreach (var student in query.Take(5))
            {
                Console.WriteLine("{0}, narozen {1}", student.FullName, student.BirthDate.ToString("dd/MMMM yyyy"));
            }
        }

        // rozradi fanousky do skupin podle toho, kteremu tymu fandi a ziska jejich pocty v jednotlivych skupinach
        public void CreateStudentsGroupByClub()
        {
            var query = from student in students
                        select new { FullName = student.Name + " " + student.Surname, FavClub = student.Fan.FavoriteClub } 
                        into editedStudent         
                        group editedStudent by editedStudent.FavClub
                        into fanGroups
                        select new { students = fanGroups, fanGroups.Key, Count = fanGroups.Count() };

            foreach(var fanGroup in query)
            {
                Console.WriteLine("\n=== Tým: {0}, počet fanoušků: {1} ===", fanGroup.Key, fanGroup.Count);

                foreach(var student in fanGroup.students)
                {
                    Console.WriteLine(student.FullName);
                }
            }
        }

        // nalezne studenty, kteri jsou plnoleti a maji dostatecnou navstevnost pro slevu na pivo
        public void GetStudentsWithDiscount()
        {
            var query = from student in students
                        where student.Age >= 18 && student.Fan.VisitsThisSeason > 40
                        select new { FullName = student.Name + " " + student.Surname, student.Age, student.Fan.VisitsThisSeason };

            Console.WriteLine("\n=== Na slevu mají nárok ===");
            foreach(var student in query)
            {
                Console.WriteLine("{0} ({1}), počet návšěv: {2}", student.FullName, student.Age, student.VisitsThisSeason);
            }
        }

        // zjisti fanousky s platnou pernamentkou, ktera je na vsechny zapasy (ti kteri maji VIP)
        public void GetVIPSeasonTicketFans()
        {
            var query = from student in students
                        where student.Fan.SeasonTicket.Expires.Year >= 2020 &&
                        student.Fan.SeasonTicket.Class == TicketClass.VIP
                        select new { FullName = student.Name + " " + student.Surname, 
                            FavClub = student.Fan.FavoriteClub, TicketExpYear = student.Fan.SeasonTicket.Expires.Year };

            Console.WriteLine("\n=== Lidé s platnou VIP pernamentkou ===");

            foreach (var student in query)
            {
                Console.WriteLine("{0}, {1}, vyprší v roce {2}", student.FullName, student.FavClub, student.TicketExpYear);
            }
        }

        // zjisti, jaky tym ma nejvice ultra fanousku + vypise vsecny ultra fanousky s jejich klubem pro kontrolu
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

            Console.WriteLine("\n=== Kluby s ultra fanoušky ===");
            foreach (var group in fanGroups)
            {
                Console.WriteLine("{0}, počet: {1}", group.Key, group.Count());
            }
        }
    }
}