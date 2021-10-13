# Дадугин Егор Артемович БПИ198
# Домашнее задание 4

## Задача 1

### А)
select LastName
from Reader
where contains(Address, 'Москва')

### Б)
select Author, Title
from Reader, Borrowing
where ID = ReaderNr and FirstName = 'Иван' and LastName = 'Иванов' 

### В)
select ISBN
from Book, BookCat
where Book.ISBN = BookCat.ISBN and CategoryName = 'Горы' and ISBN not in (
select ISBN 
from Book b, BookCat bc
where b.ISBN = bc.ISBN and CategoryName != 'Путешествия' )

### Г)
select LastName, FirstName
from Reader, Borrowing
where ID = ReaderNr and ReturnDate is not null

### Д)
select LastName, FirstName
from Reader, Borrowing
where ID = ReaderNr and ISBN in (
select ISBN
from Reader r, Borrowing b
where r.ID = b.ReaderNr and r.FirstName =  'Иван' and r.LastName = 'Иванов'
) and FirstName != 'Иван' and LastName != 'Иванов'

## Задача 2

### А)
select distinct TrainNr
from Connection
join Station s1 on s1.Name = StartStationName
join Station s2 on s2.Name = EndStationName
where s1.CityName = 'Москва'  and s2.CityName = 'Тверь'

### Б)
select distinct TrainNr
from Connection
join Station s1 on s1.Name = StartStationName
join Station s2 on s2.Name = EndStationName
where s1.CityName = 'Москва' and s2.CityName = 'Санкт-Петербург' and Day(Departure) = Day(Arrival)
