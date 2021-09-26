### Task 3.
# Почему любое отношение в реляционной схеме имеет по крайней мере один ключ?
В любом отношении должен существовать по крайней мере один потенциальный ключ, потому что иначе невозможно определить отношения в схеме, обычно наследуемые от сущностей, которые она связывает

# Переведите все диаграммы ER из предыдущей домашней работы в реляционные схемы.

# 2.1. Диаграмма для библиотечной системы
Entities:
- Publisher (ID, name, address)
- Category (ID, name)
- Rent (ID, startDate, endDate)
- Book (ISBN, year, name, author, pageCount)
- Copy Book (ID, position)
- Reader (ID, firstName, lastName, birthdayDate, address)

1:N отношения:
Publisher -> Book - Publish (ID, ISBN)
Book-> Category - Assign (ISBN, ID)
Copy Book -> Rent - send to (ID,ID)
Reader -> Copy - Get (ISBN, ID)
N:M отношения:
Book ->Copy Book - Belongs (Name, ISBN)

# 2.2.
Диаграмма для квартира-дома-страна-...
Entities:
- Country (countryName)
- City (countryName, cityName)
- Street (countryName, cityName, streetName)
- House (countryName, cityName, streetName, houseNumber)
- Flat (countryName, cityName, streetName, houseNumber, flatNumber)

1:N отношения:
House -> Flat - Contains
Country -> City - Contains
City -> Street - Contains
Street -> House - Contains

футбольные команды
Entities:
- Team
- Arbitrator

N:1 Relations:
Team -> Arbitrator - Judges

семья
Entities:
- Person (full name, sex)

1:N Relations:
Person ->Person isFather (fatherName, childName)
Person ->Person isMother (motherName, childName)

# 3.1.
Entities:
- Station (Name, #Tracks)
- Train (TrainNr, Length)
- City (Region, Name)

1:N relations:
Station -> Train - End (Name, TrainNr)
City ->Station Lie in (Region, StationName, cityName)
Station -> Train - Start (Name, TrainNr)

1:1:N relations:
Station -> Station ->Train - Connected (stationName1, stationName2, TrainNr, Departure, Arrival)

# 3.2.
Entities:
- Patient (PatientNr, Name, Disease)
- StationPersonell (PersNr, Name)
- Caregiver (PersNr, Name, Qualification)
- Doctor (PersNr, Name, Area, Rank)
- Station (StatNr, Name)
- Room (StatNr, RoomNr, #Beds)

1:N relations:
Doctor ->Patient Cures (PersNr, PatientNr)
StationPersonell -> Station - Station (PersNr, StatNr)
Room ->Patient - Admission (PatientNr, RoomNr, StatNr, from, to)
Station ->Room Contains (StatNr, RoomNr, Beds)
