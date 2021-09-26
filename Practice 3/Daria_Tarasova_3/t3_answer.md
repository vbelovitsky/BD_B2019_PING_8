Task 3.  
1. Every relation in a relational schema should have at least one key, because otherwise it is impossible to identify relations in a schema, usually inherited from the entities it connects.  

2.1. Diagram for the library system.  
Entities:  
1.  Book (ISBN, author, year, title, pages)  
2.  Copy (ID, ISBN, position)  
3.  Reader (ID, name, surname, date, address)  
4.  Publisher (ID, name, address)  
5.  Category (Name)  
6.  Borrow(ID, date)  
  
1:N relations:  
[1]->[2] has copy (ISBN, ID)  
[4]->[1] published (ID, ISBN)  
[5]->[5] subcategory of (Name1, NameN)  
[2]->[6] send to (ID,ID)  
[3]->[5] take a (ID, ID)  
  
N:M relations:  
[1]->[2] belongs to (Name, ISBN)  
  
2.2.  
The apartment is located in a house on a street in a city in a country.  
Entities:  
1.  Country (country_name)  
2.  City (country_name, city_name)  
3.  Street (country_name, city_name, street_name)  
4.  House (country_name, city_name, street_name, house_number)  
5.  Apartment (country_name, city_name, street_name, house_number, apartment_number)  
  
1:N relations:  
[1]->[2] lies in (country_name, city_name)  
[2]->[3] lies in (country_name, city_name, street_name)  
[3]->[4] stands on (country_name, city_name, street_name, house_number)  
[4]->[5] lies in (country_name, city_name, street_name, house_number, apartment_number)  
  
Two teams play football against each other under the guidance of the arbitrator.  
Entities:  
1.  Team (TID)  
2.  Arbitrator (AID)  
  
N:M:1 Relations:  
[1]->[1]->[2] Relation (TID, TID, AID)  
  
Every person (male and female) has a father and mother.  
Entities:  
1.  Person (Name, sex)  
1:N Relations:
[1]->[1] is father (father_name, child_name)
[1]->[1] is mother (mother_name, child_name)

2.3. E/R - model in the form of an E / R diagram:  
Entities:  
1.	Entity (entity_name, is_weak)  
2.	Relation (relation_name, is_weak)  
3.	Attribute (attribute_name, entity_name, relation_name, is_part_of_key)  

1:N Relations:  
[1]->[3] has (entity_name, attribute_name)  
[2]->[3] has (relation_name, attribute_name)  
  
N:M Relations:  
[1]->[2] in relation (entity_name, relation_name)  
  
3.1. Entities:
1. City (Region, Name)  
2. Station (Name, #Tracks)  
3. Train (TrainNr, Length)  
  
1:N relations:  
[2]->[3] Start (Name, TrainNr)  
[2]->[3] End (Name, TrainNr)  
[1]->[2] Lie in (Region, Name (station), Name (city))  
  
1:1:N relations:  
[2]->[2]->[3] Connected (Name (1st station), Name (2nd station), TrainNr, Departure, Arrival)  
  
3.2. Entities:  
1. Station (StatNr, Name)  
2. Room (StatNr, RoomNr, #Beds)  
3. Patient (PatientNr, Name, Disease)  
4. StationPersonell (PersNr, Name)  
5. Caregiver (PersNr, Name, Qualification)  
6. Doctor (PersNr, Name, Area, Rank)  
  
1:N relations:  
[2]->[3] Admission (PatientNr, RoomNr, StatNr, from, to)  
[1]->[2] Has (StatNr, RoomNr, #Beds)  
[6]->[3] Treats (PersNr, PatientNr)  
[4]->[1] Station (PersNr, StatNr)  
