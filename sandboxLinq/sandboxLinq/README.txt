
------ Zadání ------

Dohodli jsme se, že vytvořím třídu "Student". V ní deklaruji několik vlastností, které budou svými hodnotami reprezentovat některé věci týkající se reálného 
studenta (jméno, věk, datum narození, atd.) Dále jsem měl deklarovat třídu "FootballFan", která by opět obsahovala několik vlastností, s hodnotami 
reprezentujícími fotbalového fanouška a odkaz na její instanci by byl uložen ve třídě "Student". Zároveň jste říkal, abych použil i výčty (např. v třídě "FootballFan" 
deklarovat vlastnost, která by se jmenovala "oblíbený klub" a jako typ měla výčet "fotbalový klub"). Poté jste zmiňoval, že skládání tříd je v reálném programu 
velmi hluboké a komplkikované. Proto jste mi doporučil, abych deklaroval jetě třetí třídu. Odkaz na instanci této třídy by pak měl být uložen v třídě "FootballFan".
Nakonec jsem dostal za úkol vytvořit několik instancí třídy "Student" a odkazy na tyto instance vložit do List<Student>. S tímto listem jsem měl následně pracovat pomocí LINQu
a vyhledávat v něm požadované věci (např. všichni lidé, kteří fandí určitému týmu).

------ Provedení mého programu ------

Nejdříve jsem potřeboval vytvořit třídy s výčty a vlastnostmi, které budou obsahovat data týkající se studenta. =>
V třídě "Student" jsem deklaroval => 
		- vlastnosti, které mají různé typy a svými hodnotami reprezentují věci týkající se studenta ("Name" - string, "Age" - int, "BirthDate" - DateTime, atd.) 
		- vlastnost obsahující odkaz na instanci třídy "FootballFan"
V třídě "FootballFan" jsem deklaroval => 
		- vlastnosti, které mají různé typy a svými hodnotami reprezentují věci týkající se fotbalového fanouška ("VisitsThisSeason" - int, atd.)
		- výčet "FootballClub", který obsahuje různé kluby ("Slavia", "Sparta", atd.)
		- vlastnost "FavoriteClub" typu "FootballClub", která reprezentuje oblíbený klub
		- vlastnost obsahující odkaz na instanci třídy "SeasonTicket". 
V třídě "SeasonTicket" jsem deklaroval => 
		- vlastnost "Expire" typu DateTime
		- výčet "TicketClass", který obsahuje třídy pernamentek ("Normal", "VIP")
		- vlastnost "Class" typu "TicketClass", která reprezentuje třídu pernamentky. 

Dále jsem potřeboval vytvořit několik instancí třídy "Student", tyto instance naplnit daty a odkazy na instance vložit do List<Student>. Za tímto účelem jsem deklaroval třídu 
"GenerateData". V ní je metoda "CreateList", která má 2 přetížení a návratový typ "List<Student>". =>
1. CreateList(int numberOfPeople) =>
		- jako parametr přijímá, kolik má být vloženo lidí do List<Student> (využije se v podmínce cyklu for)
		- v cyklu for se vždy vytvoří nová instance třídy "Student", její vlastnosti se vyplní náhodnými hodnotami pomocí třídy "System.Random" a následně je odkaz na instanci
		  vložen do List<Student> => rozsahy náhodných hodnot jsem volil tak, aby mi dávaly pro danou vlastnost smysl (např. věk je v rozmezí 13 - 25)
2. CreateList() =>
		- vloží do List<Student> instance třídy "Student", které mají vlastnosti vyplněny předem definovanými hodnotami
==> Pro vytvoření přetížení metody "CreateList(int numberOfPeople)" jsem se rozhodl, protože mi přišlo zajímavější pracovat pomocí LINQu s listem studentů, který uchovává 
    více instancí, čehož bych manuálním vkládáním do listu těžko dosáhnul.

Nakonec jsem se rozhodl deklarovat třídu "FilterData", jejímž účelem je filtrovat data z List<Student> pomocí LINQu. =>
		- nová instance třídy "FilterData" je vytvořena v metodě "Main()"
		- třída obsahuje 2 konstruktory instancí, které mají za úkol obstarat vytvoření List<Student> => "FilterData()", který volá metodu "GenerateData.CreateList()" a 
		  "FilterData(int numberOfPeople)", který volá metodu "GenerateData.CreateList(int numberOfPeople)"
		- v třídě je 5 metod, které využívají LINQ pro filtraci podle nějakých smyšlených požadavku (viz. níže), 
		- v dotazech jazyka LINQ jsem se pokusil využít co nejvíce různých klíčových slov a metod, abych pochopil jejich fungování a použití

1. metoda "GetStudentsWithName(string surname)"
		- nalezne studenty se zadaným příjmením (parametr metody "surname")
		- výslední lidé jsou seřazeni vzestupně podle křestního jména
		- v projektoru select se spojí "Student.Name" a "Student.Surname" do "FullName"
		- vypíše "FullName", "BirthDate" pro prvních 5 studentů, kteří splňují podmínku 
2. metoda "CreateStudentsGroupByClub()"
		- v projektoru select se spojí "Student.Name" a "Student.Surname" do "FullName"
		- rozřadí fanoušky do skupin podle toho, kterému týmu fandí 
		- vypíše tým s početem jeho fanoušků a níže uvede všechny příslušníky daného týmu ("FullName")
3. metoda "GetStudentsWithDiscount()"
		- nalezne studenty, kteří jsou plnoletí (věk je roven nebo vyšší jak 18) a mají dostatečnou návštěvnost (návštěvnost je vyšší jak určité číslo)
		- v projektoru select se spojí "Student.Name" a "Student.Surname" do "FullName"
		- vypíše "FullName", "Age", "VisitsThisSeason" pro jednotlivé studenty, kteří splňují požadavky
4. metoda "GetVIPSeasonTicketFans()"
		- zjistí fanoušky s platnou pernamentkou (datum expirace pernamentky je vyšší nebo rovno 2020) 
		- pernamentka musí být třídy VIP
		- v projektoru select se spojí "Student.Name" a "Student.Surname" do "FullName"
		- vypíše "FullName", "FavoriteClub", "Expires.Year" pro jednotlivé studenty, kteří splňují požadavky 
5. metoda "GetTeamWithMostUltraFans()"
		- nalezne všechny ultra fanoušky a rozdělí je do skupin podle týmů, kterým fandí => výsledek převede na list
		- zjistí nejvyšší počet ultra fanoušků v skupině ze všech skupin z listu
		- nalezne skupinu, které odpovídá nejvyšší počet ultra fanoušků
		- vypíše tým (klíč) skupiny s nejvíce ultra fanoušky a jejich počet 