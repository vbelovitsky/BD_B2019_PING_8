# Homework4
## Задача 1
Имеем 
- Reader( ID, LastName, FirstName, Address, BirthDate) 
- Book ( ISBN, Title, Author, PagesNum, PubYear, PubName) 
- Publisher ( PubName, PubAdress) 
- Category ( CategoryName, ParentCat) 
- Copy ( ISBN, CopyNumber, ShelfPosition)
- Borrowing ( ReaderNr, ISBN, CopyNumber, ReturnDate) 
- BookCat ( ISBN, CategoryName )

### Какие фамилии читателей в Москве?
``` sql
SELECT LastName FROM Reader WHERE Address = 'Moscow';
```
### Какие книги (author, title) брал Иван Иванов?
``` sql
SELECT Author, Title FROM Book INNER JOIN Borrowing
ON borrowing.ISBN = book.ISBN
INNer JOIN Reader
ON reader.ID = borrowing.ReaderNr
WHERE reader.FirstName = 'Ivan' AND reader.LastName = 'Ivanov';
```
### Какие книги (ISBN) из категории "Горы" не относятся к категории "Путешествия"?
``` sql
SELECT * FROM BookCat
WHERE CategoryName = 'Mountain'
AND ISBN NOT IN 
(SELECT ISBN from BookCat WHERE CategoryName = 'Travel')
```
### Какие читатели (LastName, FirstName) вернули копию книгу?
``` sql
SELECT DISTINCT reader.FirstName, reader.LastName FROM borrowing INNER JOIN Reader
ON reader.ID = borrowing.ReaderNr WHERE ReturnDate < NOW();
```
### Какие читатели (LastName, FirstName) брали хотя бы одну книгу (не копию), которую брал также Иван Иванов (не включайте Ивана Иванова в результат)?
``` sql
SELECT DISTINCT FirstName, LastName FROM Reader INNER JOIN Borrowing
ON reader.ID = borrowing.ReaderNr
WHERE ISBN in (SELECT ISBN FROM Borrowing JOIN Reader ON reader.ID = borrowing.ReaderNr WHERE reader.FirstName = 'Ivan' AND reader.LastName = 'Ivanov')
AND reader.LastName != 'Ivan' AND reader.FirstName != 'Ivanov';
```
## Задача 2
Имеем:
- City ( Name, Region ) 
- Station ( Name, #Tracks, CityName, Region ) 
- Train ( TrainNr, Length, StartStationName, EndStationName ) 
- Connection ( FromStation, ToStation, TrainNr, Departure, Arrival)
### Найдите все прямые рейсы из Москвы в Тверь.
``` sql
SELECT DISTINCT TrainNr FROM Connection
JOIN station s1 ON s1.Name = connection.FromStation JOIN station s2 ON s2.Name = connection.ToStation
WHERE s1.CityName = 'Moscow' AND s2.CityName = 'Tver';
```
### Найдите все многосегментные маршруты, имеющие точно однодневный трансфер из Москвы в Санкт-Петербург (первое отправление и прибытие в конечную точку должны быть в одну и ту же дату). Вы можете применить функцию DAY () к атрибутам Departure и Arrival, чтобы определить дату. 
``` sql
SELECT DISTINCT TrainNr FROM connection
JOIN station s1 ON s1.Name = connection.FromStation JOIN station s2 ON s2.Name = connection.ToStation
WHERE (Arrival - Departure) = 0 AND s1.CityName = 'Moscow' AND s2.CityName = 'Petersburg';
```
### Что изменится в выражениях для а) и б), если отношение "Connection" не содержит дополнительных кортежей для транзитивного замыкания, поэтому многосегментный маршрут Москва-> Тверь-> Санкт-Петербург содержит только кортежи Москва-> Тверь и Тверь-Санкт-Петербург?
- а) Тогда надо было бы делать полный перебор, путём умножения Train x Connection и отбирать записи с подходящими городами
- б) Тоже надо было бы делать полный перебор, далее логика бы тож самое

## Задача 3
### Представьте внешнее объединение (outer join ) в виде выражения реляционной алгебры с использованием только базовых операций (select, project, cartesian, rename, union, minus)

Рассмотрим следующие сущности: A(A1, ..., An) и B(B1, ..., Bm)

Outer Join мы можем представить как Inner Join вместе с значениями из A, которые не попали в Inner Join и аналогичными значениями из B

Теперь вызарим Inner Join, а потом через него выразим Outer Join:

Inner Join(A, B, Condition) = project[A1,..., An, B1,..., Bm]( select( cartesian(A, B), Condition))


```
Outer Join(A, B) =  union(
        Inner Join(A, B, Condition),
        project[A1, ..., An, NULL, ..., NULL](A - project[A1, ..., An](Inner Join(A, B))),
        project[NULL, ..., NULL, B1, ..., Bm](B - project[B1, ..., Bm](Inner Join(A, B)))
    )
```