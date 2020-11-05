using System;
using System.Collections.Generic;

namespace sandboxLinq
{
    class GenerateData
    {
        // pole obsahujici jmena a prijmeni, v metode CreateList(int) jsou pouzita pro vygenerovani nahodnych osob
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

        // vygeneruje List<Student> o zadanem poctu lidi s nahodnymi instancemi tridy Student
        public List<Student> CreateList(int numberOfPeople)
        {
            List<Student> students = new List<Student>();

            for (int x = 0; x < numberOfPeople; x++)
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
                            FavoriteClub = (FootballFan.FootballClub)Rnd.Next(Enum.GetNames(typeof(FootballFan.FootballClub)).Length),
                            IsUltraFan = Rnd.Next(0, 2) == 0,
                            VisitsThisSeason = Rnd.Next(0, 50),
                            SeasonTicket = new SeasonTicket
                            {
                                Expires = new DateTime(DateTime.Now.Year + Rnd.Next(-3, 4), 8, 22),
                                Class = (SeasonTicket.TicketClass)Rnd.Next(Enum.GetNames(typeof(SeasonTicket.TicketClass)).Length),
                            }
                        }
                    }
                );
            }

            return students;
        }

        // vygeneruje List<Student> s instancemi tridy Student, ktere maji predem definovane hodnoty
        public List<Student> CreateList()
        {
            List<Student> students = new List<Student>();

            //student1
            students.Add(
                new Student
                {
                    Name = "Jan",
                    Surname = "Bartoň",
                    Age = 17,
                    BirthDate = new DateTime(2003, 4, 13),
                    Fan = new FootballFan
                    {
                        FootballFanSinceYear = 2013,
                        FavoriteClub = FootballFan.FootballClub.Slavia,
                        IsUltraFan = false,
                        VisitsThisSeason = 6,
                        SeasonTicket = new SeasonTicket
                        {
                            Expires = new DateTime(2020, 8, 22),
                            Class = SeasonTicket.TicketClass.Standart,
                        }
                    }
                }
            );
            //student2
            students.Add(
                new Student
                {
                    Name = "Petr",
                    Surname = "Novák",
                    Age = 19,
                    BirthDate = new DateTime(2001, 10, 23),
                    Fan = new FootballFan
                    {
                        FootballFanSinceYear = 2009,
                        FavoriteClub = FootballFan.FootballClub.Slavia,
                        IsUltraFan = true,
                        VisitsThisSeason = 43,
                        SeasonTicket = new SeasonTicket
                        {
                            Expires = new DateTime(2022, 8, 22),
                            Class = SeasonTicket.TicketClass.VIP,
                        }
                    }
                }
            );
            //student3
            students.Add(
                new Student
                {
                    Name = "Tomáš",
                    Surname = "Bartoň",
                    Age = 15,
                    BirthDate = new DateTime(2005, 5, 4),
                    Fan = new FootballFan
                    {
                        FootballFanSinceYear = 2013,
                        FavoriteClub = FootballFan.FootballClub.Sparta,
                        IsUltraFan = false,
                        VisitsThisSeason = 0,
                        SeasonTicket = new SeasonTicket
                        {
                            Expires = new DateTime(2019, 8, 22),
                            Class = SeasonTicket.TicketClass.VIP,
                        }
                    }
                }
            );

            return students;
        }
    }
}
