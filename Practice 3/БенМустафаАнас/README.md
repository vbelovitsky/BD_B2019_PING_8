**Задание №1: Почему любое отношение в реляционной схеме имеет по крайней мере один ключ?**

Ключи уникальны, а уникальность ключей разрешает проблему того, что в любой реляционной схеме отношения должны иметь уникальное значение.

**Задание №3: Переведите приведенные диаграммы ER в реляционные схемы.**

__3.1 https://imgur.com/w2iDI1o__

Entities:
1. Station - (Name: string, #Tracks: int)
2. City - (Name: string, Region: string)
3. Train - (TrainNr: int, Length: int)

Relationships:
1. Start - (Name: string, TrainNr: int)
2. End - (Name: string, TrainNr: int)
3. Lie-in - (Name: string, Name: string)
4. Connected - (Name: string, Name: string, TrainNr: int, Departure: datetime, Arrival: datetime)

__3.2 https://imgur.com/oFBM5pp__

Entities:
1. Doctor - (PersNr: int, #Name: string, Area: string, Rank: string, Treats: string)
2. Patient - (PatientNr: int, Name: string, Disease: string, PersNr: int)
3. StationPersonell - (PersNr: int, #Name: string)
4. Caregiver - (PersNr: int, #Name: string, Qualification: string)
5. Station - (StatNr: int, Name: string)
6. Room - (StatNe: int, RoomNr: int, #Beds: int)

Relationships:
1. Has - (StatNr: int, RoomNr: int)
2. Works-for - (PersNr: int, StatNr: int)
3. Admission - (StatNr: int, RoomNr: int, PatientNr: int, from: string, to: string)
4. Treats - (PersNr: int, PatientNr: int)

**Задание №2: Переведите все диаграммы ER из предыдущей домашней работы в реляционные схемы.**

__пункт 2.1__

Entities:
1. Book - (ISBN - int, Year - Date, Title - string, Author - string, Number-of-pages - int)
2. Categories - (Name - string)
3. Publisher - (Name - string, Address - string)
4. Reader - (Reader-ID - int, Last-Name - string, First-Name - string, Address - string, Birthday - Date)
5. Copy - (Copy-ID - int, Position-on-the-shelf - int)

Relationships:
1. Publish - (ISBN - int, Title - string, Name - string)
2. Exist - (ISBN - int, Title - string)
3. Assign - (ISBN - int, Title - string)
4. Get - (ISBN - int, Title - string, Reader-ID - int, Date-of-returnment - Date)

__пункт 2.2__

Entities:  
1.  Country (County-Name: string)  
2.  City (County-Name: string, City-Name: string)  
3.  Street (County-Name: string, City-Name: string, Street-Name: string)  
4.  House (County-Name: string, City-Name: string, Street-Name: string, House-Number: int)  
5.  Apartment (County-Name, City-Name, Street-Name: string, House-Number: int, Apartment-Number: int)  

Relationships:
1. Stands on - (County-Name: string, City-Name: string, Street-Name: string: string, House-Number: int: int)  
2. Lies - (County-Name: string, City-Name, Street-Name: string, House-Number: int, Apartment-Number: int)


__пункт 2.3__

Entities:
1. Arbitrator - (Arbitrator-Name: string)
2. Team - (Team-Name: string)

Relationships:
1. Umpire - (Arbitrator-Name: string, Team-Name: string)

__пункт 2.4__

Entities:
1. Man - (ManName: string)
2. Woman - (WomanName: string)

Relationships:
1. IsFather - (ManName: string, ChildName: string)
2. IsMother - (WomanName: string, ChildName: string)

__пункт 2.5__

Entities:
1. Entity - (EntityName: string)
2. Relationship - (RelationshipName: string)
3. Attribute of relationship - (AttributeName: string)

Relationships:
1. Has - (EntityName: string, AttributeName: string)
2. Participate - (Function: string, Role: string, Min: int, Max: int)







