# Гарифуллин Руслан Ильфатович Задание 3

## Задача 1
> Почему любое отношение в реляционной схеме имеет по крайней мере один ключ?

Отношения между сущностями образуются при помощи уникальных ссылок, которые реализуемы в виде ключей в БД. Ключей, описывающих уникальную сущность, может быть и несколько, однако их отсутствие не позволит отношению связать сущности и отличить один экземпляр сущности от другого.

## Задача 2
**1.**

Entities:
 - **Category**: {[**id: int**, name: string, parentId: int, nullable]}
 - **Book**: {[**ISBN: string**, name: string, publicationYear: int, author: string, categoryId: int]}
 - **BookCopy**: {[**ISBN: string**, **copyId: int**, position: string, originalBookId: int]}
 - **Rent**: {[**id: int**, nullable, returnDate: date]}
 - **Reader**: {[**id: int**, lastName: string, firstName: string, address: string, birthDate: date]}
 - **Publisher**: {[**id: int**, name: string, address: string]}

Relationships:
 - **rents**: {[**readerId: int**, **rentId: int**]}
 - **includes**: {[**bookId: int**, **rentId: int**]}

**2.1.**

Entities:
 - **Flat**: {[**id: int**, houseId: int]}
 - **House**: {[**id: int**, streetId: int]}
 - **Street**: {[**id: int**, cityId: int]}
 - **City**: {[**id: int**, countryId: int]}
 - **Country**: {[**id: int**]}

**2.2.**

Entities:
 - **Referee**: {[**id: int**]}
 - **Game**: {[**id: int**, refereeId: int, firstTeamId: int, secondTeamId: int]}
 - **Team**: {[**id: int**]}

**2.3.**

Entities:
 - **Male**: {[**id: int**, fatherId: int, motherId: int]}
 - **Female**: {[**id: int**, fatherId: int, motherId: int]}

**3.**

Entities:
 - **User**: {[**id: int**]}
 - **Meeting**: {[**id: int**]}
 - **ChatMessage**: {[**id: int**, authorId: int, meetingId: int]}
 - **UploadedFile**: {[**id: int**, messageId: int]}

Relationships:
 - **participates**: {[**userId: int**, **meetingId: int**]}
 - **sent**: {[**messageId: int**, **meetingId: int**]}

## Задача 3
**1.**

Entities:
 - **Station**: {[**name: string**, tracksCount: int, cityRegion: string, cityName: string]}
 - **Train**: {[**trainNr: int**, length: int, startName: int, endName: int, arrivalTime: datetime, departureTime: datetime]}
 - **City**: {[**region: string**, **name: string**]}

Relationships:
 - **connected**: {[**trainNr: int**, **stationName: string**, arrival: datetime, departure: datetime]}

**2.**

Entities:
 - **Station**: {[**statNr: int**, name: string]}
 - **Room**: {[**roomNr: int**, bedsCount: int, statNr: int]}
 - **Patient**: {[**patientNr: int**, name: string, disease: string, doctorId: int]}
 - **Doctor**: {[**PersNr: int**, name: string, area: string, rank: string, stationId: int]}
 - **Caregiver**: {[**PersNr: int**, name: string, stationId: int]}

Relationships:
 - **Admission**: {[**patientNr: int**, **roomNr: int**, from: datetime, to: datetime]}
