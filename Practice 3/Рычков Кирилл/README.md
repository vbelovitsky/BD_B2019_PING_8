# Практическое задание №3

## Рычков Кирилл, БПИ198

## Задание 1
Так как каждое отношение должно иметь уникальный идентификатор, 
поэтому в реляционной схеме как минимум есть один ключ.
   
## Задание 2.1

Category: {[Name:string]}

Book: {[ISBN: integer, Year: integer, Title: string, Author: string, Number-of-pages: integer]}

Publisher:{[Name: string, Address: string]}

Publish: {[ISBN:integer, Name:string]}

Copy: {[ISBN:integer, CopyNumber:integer, Position:integer]}

Reader: {[ReaderId:integer, FirstName:string, LastName:string,  Address:string, Birthday:date]}

Rent: {[RentId: integer, ReceiveDate: date, ReturnDate: date]}

Rents: {[ReaderId:integer, RentId:integer, ISBN:integer, CopyNumber:integer, ReceiveDate: date, ReturnDate:string]}

## Задание 2.2
Country: {[CountryId:integer]}

City: {[CountryId:integer, CityId:integer]}

Street: {[CityId:integer, StreetId:integer]}

House: {[StreetId:intege, HouseId:integer]}

Flat: {[HouseId:integer, FlatId:integer]}

## Задание 2.3

Team: {[TeamId:binary]}

Arbitrator: {[ArbitratorId:integer]}

Judges: {[TeamId:binary, ArbitratorId:integer]}


## Задание 3.1
https://imgur.com/w2iDI1o

Station: {[Name:string, #Tracks:integer]}

City: {[Region:string, Name:string]}

Train: {[TrainNr:integer, Length:integer]}

Start: {[Name:string, TrainNr:integer]}

End: {[Name:string, TrainNr:integer]}

Connected: {[Name:string, Name:string, TrainNr:integer, Departure:string, Arrival:string]}

## Задание 3.2

https://imgur.com/oFBM5pp

StationPersonell: {[PersNr:integer, #Name:string, StatNr:integer]}

Caregiver: {[PersNr:integer, #Name:string, Qualification:string]}

Doctor: {[PersNr:integer, #Name:string, Area:string, Rank:double]}

Station: {[StatNr:integer, Name:string]}

Room: {[StatNr:integer, RoomNr:integer, #Beds:integer]}

Patient: {[PatientNr:integer, Name:string, Disease:string, PersNr:integer]}

Admission: {[StatNr:integer, RoomNr:integer, PatientNr:integer, from:string, to:string]}
