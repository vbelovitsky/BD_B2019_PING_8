## Задача 1

* а) Какие фамилии читателей в Москве?
```sql
SELECT LastName FROM Reader
WHERE Address = 'Москва';
```

* б) Какие книги (author, title) брал Иван Иванов?
```sql
SELECT Book.Author, Book.Title FROM Book
JOIN Reader ON Borrowing.ReaderNr = Reader.ID
WHERE Reader.FirstName = 'Иван' AND Reader.LastName = 'Иванов';
```

* в) Какие книги (ISBN) из категории "Горы" не относятся к категории "Путешествия"? Подкатегории не обязательно принимать во внимание!
```sql
SELECT DISTINCT ISBN FROM BookCategory
WHERE BookCategory.CategoryName = 'Mountains'
EXCEPT
SELECT DISTINCT ISBN FROM BookCategory
WHERE BookCategory.CategoryName = 'Travel';
```

* г) Какие читатели (LastName, FirstName) вернули копию книги?
```sql
SELECT DISTINCT Reader.FirstName, Reader.LastName FROM Reader
JOIN Borrowing ON Reader.ID = Borrowing.ReaderNr
WHERE Borrowing.ReturnDate IS NOT NULL;
```

* д) Какие читатели (LastName, FirstName) брали хотя бы одну книгу (не копию), которую брал также Иван Иванов (не включайте Ивана Иванова в результат)?
```sql
SELECT Reader.LastName, Reader.FirstName FROM Reader
JOIN Borrowing ON Reader.ID = Borrowing.ReaderNr
WHERE ISBN IN (
SELECT ISBN FROM Borrowing
JOIN Reader ON Reader.ID = Borrowing.ReaderNr
WHERE FirstName = 'Иван' AND LastName = 'Иванов'
)
EXCEPT
SELECT Reader.LastName, Reader.FirstName FROM Borrowing
JOIN Reader ON Reader.ID = Borrowing.ReaderNr
WHERE FirstName = 'Иван' AND LastName = 'Иванов';
```

## Задача 2

City(Name, Region)
Station(Name, #Tracks, CityName, Region)
Train(TrainNr, Length, StartStationName, EndStationName)
Connection(FromStation, ToStation, TrainNr, Departure, Arrival)

* а) 
```sql
SELECT  TrainNr FROM Connection
    JOIN Station sfrom ON sfrom.Name = Connection.FromStation
    JOIN Station sto ON sto.Name = Connection.ToStation
WHERE s1.CityName = 'Москва' AND s2.CityName = 'Тверь'
    EXCEPT
SELECT TrainNr FROM Connection
    JOIN Station sfrom ON sfrom.Name = Connection.FromStation
    JOIN Station sto ON sto.Name = Connection.ToStation
WHERE s1.CityName != 'Москва' OR s2.CityName != 'Тверь';
```

* б)
```sql
SELECT C1.TrainNr, C1.Departure, C2.Arrival
FROM Connection AS C1
    JOIN Connection AS C2 ON C1.TrainNr = C2.TrainNr
WHERE DAY (C1.Departure) = DAY (C2.Arrival) 
    AND C1.FromStation = 'Москва'
    AND С2.ToStation = 'Санкт-Петербург';
```

* в) 
- Можно из всех connection'ов взять те записи, в которых FromStation = Москва и ToStation = Тверь.Т.е. ничего не поменяется.

## Задача 3

Пусть есть 2 сущности: A(A_1, ..., A_n) и B(B_1, ..., B_m)

Outer Join можно представить как Inner Join вместе с значениями из A, которые не попали в Inner Join и значениями из B, которые тоже не попали в Inner Join.

То есть для того чтобы выразить Outer Join, сначала выразим Inner Join, а через него уже нужное:
```
Inner Join(A, B, сondition) = project[A_1, ..., A_n, B_1, ..., B_m](select(cartesian(A, B), сondition))
```
```
Outer Join(A, B) = union(
Inner Join(A, B, сondition),
project[A_1, ..., A_n, NULL, ..., NULL](A - project[A_1, ..., A_n](Inner Join(A, B))),
project[NULL, ..., NULL, B_1, ..., B_m](B - project[B_1, ..., B_m](Inner Join(A, B)))
)
```