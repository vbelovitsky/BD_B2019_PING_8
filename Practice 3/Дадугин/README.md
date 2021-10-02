# Дадугин Егор Артемович БПИ198
# Домашнее задание 2

## 1

### Почему любое отношение в реляционной схеме имеет по крайней мере один ключ?
### Потому что наличие по крайней мере одного ключа в отношении позволяет однозначно определить экземпляры сущностей, которые находятся в данном отношении.

## Реляционные схемы на основе ДЗ 2
## 1

### Entities:
### 1. Book {[ISBN: srting, name: string, author: string, numberOfPages: integer, year: integer, publisherId: integer]} 
### 2. Publisher {[Id: integer, name: string, address: string]}
### 3. Category {[Id: integer, name: string, parrentId: integer, nullable]}
### 4. BookCopy {[ISBN: string, Id: integer, shelfPosition: integer]}
### 5. Reader {[Id: integer, name: string, surname: string, address: string, dateOfBirth: dateTime]}
### 6. Rent {[Id: integer, dateOfReturn: dateTime]}

### Relations:
### 1. rents {[rentId: integer, readerId: integer]}
### 2. includes {[rentId: integer, bookCopyId: integer]}
### 3. belongsToCategory {[ISBN: string, categoryId: integer]}

## 2.1

### Entities:
### 1. Apartment {[Id: integer, houseId: integer]}
### 2. House {[Id: integer, streerId: integer]}
### 3. Street {[Id: integer, cityId: integer]}
### 4. City {[Id: integer, countryId: integer]}
### 5. Country {[Id: integer]}

## 2.2

### Entities:
### 1. Referee {[Id: integer]}
### 2. Team {[Id: integer]}
### 3. Game {[Id: integer, homeTeamId: integer, guestTeamId: integer, refereeId: integer]}

## 2.3

### Entities:
### 1. Male {[Id: integer, fatherId: integer, motherId: integer]}
### 2. Female {[Id: integer, fatherId: integer, motherId: integer]}

## 3

### Entites:
### 1. Entity {[Id: integer, name: string]}
### 2. Attribute of Entity {[Id: integer, name: string, isPartOfKey: boolean, entityId: integer]}
### 3. Relation {[Id: integer, name: string]}
### 4. Attribute of Relation {[Id: integer, name: string, relationId: integer]}

### Relations:
### 1. participates {[Id: integer, entityId: integer, relationId: integer]}

## Реляционные схемы на основе представленных ER-диаграмм

## 1

### Entities:
### 1. Station {[name: string, #Tracks: integer, cityRegion: string, cityName: string]}
### 2. Train {[trainNr: integer, length: integer, startStationName: string, endStationName: string]}
### 3. City {[region: string, name: string ]}

### Relations:
### 1. Connected {[trainNr: integer, firstStationName: string, secondStationName: string, departure: dateTime, arrival: dateTime]}

## 2

### Entities:
### 1. Station {[StatNr: integer, name: string]}
### 2. Room {[RoomNr: integer, #Beds: integer, StatNr: integer]}
### 3. Patient {[PatientNr: integer, name: string, disease: string, doctorId: integer]}
### 4. StationPersonell {[PersNr: integer, #name: string, StatNr: integer]} 
### 5. Caregiver {[PersNr: integer, qualification: string, StatNr: integer]}
### 6. Doctor {[PersNr: integer, area: string, rank: string], StatNr: integer}

### Relations:
### 1. Admissions {[from: dateTime, to: dateTime, PatientNr: integer, RoomNr: integer]}
