# Задача 1

В реляционной схеме любое отношение имеет хотя бы один ключ, чтобы он мог однозначно индетифицировал кортеж (потому что одинаковых кортежей быть не может, т.к повтор приводит к возникновению ошибок) и отношения в схеме (из-за наследования от соединенных объектов). Иными словами, должен быть хотя бы один ключ, иначе было бы невозможно индетифицировать отношения в реляционной схеме.

# Задача 2

## 2.1

https://github.com/mikamurasaki2/BD_B2019_PING_8/blob/master/Practice2/Task2_1.png

* PK *key*

Entities:

Categories: { *categ_id: int*; name: string }

Book: { *ISBN: int*; title: string; author: string; year: datetime; numOfPages: int }

Publisher: { *publ_id: int*; name: string; address: string }

Copy: { *copy_id: int*; position: int }

Rent: { *rent_id: int*; dateReturn: datetime }

Reader: { *read_id: int*; firstName: string; secondName: string; birthday: datetime; address: string }


* FK **key**

Relations:

Categories: { *categ_id: int*; name: string; **parent_id: int** }

Book: { *ISBN: int*; title: string; author: string; year: datetime; numOfPages: int; **categ_id: int; publ_id: int** }

Publisher: { *publ_id: int*; name: string; address: string }

Copy: { *copy_id: int*; position: int; **ISBN: int** }

Rent: { *rent_id: int*; dateReturn: datetime; **read_id: int; copy_id: int** }

Reader: { *read_id: int*; firstName: string; secondName: string; birthday: datetime; address: string }

## 2.2

https://github.com/mikamurasaki2/BD_B2019_PING_8/blob/master/Practice2/Task2_1.png


* PK *key*

Entities:

Apartment: { *apart_id: int*; number: int }

House: { *house_id: int*; number: int }

Street: { *street_id: int*; name: string }

City: { *city_id: int*; name: string }

Country: { *country_id: int*; name: string }


* FK **key**

Relations:

Apartment: { *apart_id: int*; number: int; **house_id: int** }

House: { *house_id: int*; number: int; **street_id: int** }

Street: { *street_id: int*; name: string; **city_id: int** }

City: { *city_id: int*; name: string; **id_country** }

Country: { *country_id: int*; name: string }

## 2.3

https://github.com/mikamurasaki2/BD_B2019_PING_8/blob/master/Practice2/Task2_2.png


* PK *key*

Entities:

Arbitator: { *arb_id: int* }

Team: { *team_id: int*; name: string }


* FK **key**

Relations:

Arbitator: { *arb_id: int* }

Team: { *team_id: int*; name: string }

Judging: { *jud_id: int*; **arb_id: int; team1_id: int; team2_id: int** }

## 2.4

https://github.com/mikamurasaki2/BD_B2019_PING_8/blob/master/Practice2/Task2_3.png

* PK *key*

Entities:

Woman: { *wom_id: int*; moth_id: int; daugh_id: int }

Man: { *man_id: int*; fath_id: int; son_id: int }


* FK **key**

Relations:

Woman: { *wom_id: int*; moth_id: int; daugh_id: int;  **fath_id: int; son_id: int** }

Man: { *man_id: int*; fath_id: int; son_id: int; **moth_id: int; daugh_id: int** }

# Задача 3

## 3.1

https://imgur.com/w2iDI1o

* PK *key*

Entities:

Station: {*st_Name: string*; #Tracks: int }

City: {*city_Name: string; Region: string* }

Train: {*TrainNr: int*; Length: int }


* FK **key**

Relations:

Station: {*st_Name: string*; #Tracks: int; **city_Name: string; Region: string** }

City: {*city_Name: string; Region: string* }

Train: {*TrainNr: int*; Length: int; **start_st: string; end_st: string; conn_id: int** }

End: {**TrainNr_id: int; end_st: string** }

Start: {**TrainNr_id: int; start_st: string** }

Connection: {*conn_id: int*; Depature: datetime; Arrival: datetime; **start_st: string; end_st: string** }

Lie_in: {*st_id: int*; Region: string; Name: string }

## 3.2

https://imgur.com/oFBM5pp

* PK *key*

Entities:

StationPersonell: {*PersNr: int*; PersName: string }

Station: { *StatNr: int*; Name: string}

Doctor: { *DoctorNr: int*; #Name: string; Qualification: string}

Caregiver: { *PersNr: int*; #Name: string; Area: string; Rank: string}

Patient: { *PatientNr: int*; Name: string; Disease: string}

Room: { *RoomNr: int*; #beds: int}



* FK **key**

Relations:

StationPersonell: {*PersNr: int*; PersName: string; **StatNr: int** }

Station: { *StatNr: int*; Name: string}

Doctor: { *DoctorNr: int*; #Name: string; Qualification: string}

Caregiver: { *PersNr: int*; #Name: string; Area: string; Rank: string}

Patient: { *PatientNr: int*; Name: string; Disease: string; **DoctorNr: int; Admission_id: int**}

Room: { *ToomNr: int*; #beds: int; **SatNr: int**}

Admission: {*Admission_id: int*; RoonNr: int;  **PatientNr: int**; from: string; to: string}

Trats: {**PatientNr: int; PersNr: int**}

Works_for: {**PersNr: int; StatNr: int**}
