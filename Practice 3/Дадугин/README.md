# Дадугин Егор Артемович БПИ198
# Домашнее задание 2

## 1

### Почему любое отношение в реляционной схеме имеет по крайней мере один ключ?
### Потому что наличие по крайней мере одного ключа в отношении позволяет однозначно определить экземпляры сущностей, которые находятся в данном отношении.

## Реляционные схемы на основе ДЗ 2
## 1

### Entities:
### 1. Book {[**ISBN: srting**, name: string, author: string, numberOfPages: integer, year: integer, publisherId: integer]} 
### 2. Publisher {[**Id: integer**, name: string, address: string]}
### 3. Category {[**name: string**, parrentName: string, nullable]}
### 4. BookCopy {[**ISBN: string**, **Id: integer**, shelfPosition: integer]}
### 5. Reader {[**Id: integer**, name: string, surname: string, address: string, dateOfBirth: dateTime]}
### 6. Rent {[Id: integer, dateOfReturn: dateTime]}

### Relations:
### 1. rents {[**rentId: integer**, **readerId: integer**]}
### 2. includes {[**rentId: integer**, **ISBN: string**, **bookCopyId: integer**]}
### 3. belongsToCategory {[**ISBN: string**, **categoryName: string**]}

## 2.1

### Entities:
### 1. Apartment {[**apartmentNumber: integer** , **houseNumber: int** , **streetName: string**, **cityName: string**, **countryName: string**]]}
### 2. House {[**houseNumber: integer** , **streetName: string**, **cityName: string**, **countryName: string**]}
### 3. Street {[**streetName: string**, **cityName: string**, **countryName: string**]}
### 4. City {[**cityName: string**, **countryName: string**]}
### 5. Country {[**counrtyName: string**]}

## 2.2

### Entities:
### 1. Referee {[**Id: integer**]}
### 2. Team {[**Id: integer**]}

### Relations:
### 1. Game {[**homeTeamId: integer**, **guestTeamId: integer**, **refereeId: integer**]}

## 2.3

### Entities:
### 1. Male {[**Id: integer**, fatherId: integer, motherId: integer]}
### 2. Female {[**Id: integer**, fatherId: integer, motherId: integer]}

## 3

### Entites:
### 1. Entity {[**Id: integer**, name: string]}
### 2. Attribute of Entity {[**Id: integer**, **entityId: integer**, name: string, isPartOfKey: boolean]}
### 3. Relation {[**Id: integer**, name: string]}
### 4. Attribute of Relation {[**Id: integer**, **relationId: integer**, name: string]}

### Relations:
### 1. participates {[**Id: integer**, **entityId: integer**, **relationId: integer**]}

## Реляционные схемы на основе представленных ER-диаграмм

## 1

### Entities:
### 1. Station {[**Name: string**, **cityRegion: string**, **cityName: string**, #Tracks: integer]}
### 2. Train {[**TrainNr: integer**, Length: integer, startStationName: string, endStationName: string]}
### 3. City {[**Region: string**, **Name: string**]}

### Relations:
### 1. Connected {[**TrainNr: integer**, **startStationName: string**, endStationName: string, Departure: dateTime, Arrival: dateTime]}

## 2

### Entities:
### 1. Station {[**StatNr: integer**, Name: string]}
### 2. Room {[**RoomNr: integer**, **StatNr: integer**, #Beds: integer]}
### 3. Patient {[**PatientNr: integer**, Name: string, Disease: string, DoctorNr: integer]}
### 4. StationPersonell {[**PersNr: integer**, StatNr: integer, #Name: string]} 
### 5. Caregiver {[**PersNr: integer**, Qualification: string]}
### 6. Doctor {[**PersNr: integer**, Area: string, Rank: string]}

### Relations:
### 1. Admissions {[**PatientNr: integer**, **RoomNr: integer**, from: dateTime, to: dateTime]}
