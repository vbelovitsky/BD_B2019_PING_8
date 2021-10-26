## Задание 4

### Задача 1

Возьмите библиотечную систему, схему которой сделали на предыдущем задании

Reader( ID, LastName, FirstName, Address, BirthDate)</br>
Book ( ISBN, Title, Author, PagesNum, PubYear, PubName)</br>Publisher ( PubName, PubAdress)</br>
Category ( CategoryName, ParentCat)</br>
Copy ( ISBN, CopyNumber, ShelfPosition)</br>

Borrowing ( ReaderNr, ISBN, CopyNumber, ReturnDate)</br>
BookCat ( ISBN, CategoryName )

Напишите SQL-запросы (или выражения реляционной алгебры) для следующих вопросов:</br>
а) Какие фамилии читателей в Москве?

```sql
SELECT LastName FROM Reader
WHERE Address LIKE '%Москва%'
```

б) Какие книги (author, title) брал Иван Иванов?

```sql
SELECT Author, Title FROM Book WHERE ISBN IN
 (SELECT ISBN FROM Borrowing WHERE ReaderNr IN
  (SELECT ID FROM Reader WHERE FirstName = "Иван" AND LastName = "Иванов"))
```

Or

```sql
SELECT Author, Title FROM Reader AS r
 WHERE FirstName = "Иван" AND LastName = "Иванов"
INNER JOIN Borrowing AS bo
 ON r.ID = bo.ReaderNr
INNER JOIN Book AS b
 ON bo.ISBN = b.ISBN
```

в) Какие книги (ISBN) из категории "Горы" не относятся к категории "Путешествия"? Подкатегории не обязательно принимать во внимание!

```sql
SELECT ISBN FROM BookCat AS bc
 WHERE CategoryName = "Горы"
LEFT JOIN BookCat AS lbc
 ON bc.ISBN = lbs.ISBN AND
  lbs.CategoryName != "Путешествия"
```

г) Какие читатели (LastName, FirstName) вернули копию книгу?

```sql
SELECT LastName, FirstName FROM Reader
 WHERE ID IN (SELECT DISTINCT ReaderNr FROM Borrowing
  WHERE DATE(ReturnDate) <= DATE(NOW()))
```

д) Какие читатели (LastName, FirstName) брали хотя бы одну книгу (не копию), которую брал также Иван Иванов (не включайте Ивана Иванова в результат)?

```sql
SELECT LastName, FirstName FROM Reader
 WHERE ID IN (SELECT ReaderNr FROM Borrowing WHERE ISBN
  IN (SELECT DISTINCT ISBN FROM Borrowing WHERE ReaderNr
   IN (SELECT ID FROM Reader WHERE FirstName = "Иван"
    AND LastName = "Иванов")))

```

### Задача 2

Возьмите схему для Поездов, которую выполняли в предыдущем задании.

City ( Name, Region )</br>
Station ( Name, #Tracks, CityName, Region )</br>
Train ( TrainNr, Length, StartStationName, EndStationName )</br>Connection ( FromStation, ToStation, TrainNr, Departure, Arrival)</br>

Предположим, что отношение "Connection" уже содержит транзитивное замыкание. Когда поезд 101 отправляется из Москвы в Санкт-Петербург через Тверь, содержит кортежи для связи Москва->Тверь, Тверь-Санкт-Петербург и Москва->Санкт-Петербург. Сформулируйте следующие запросы в реляционной алгебре:

а) Найдите все прямые рейсы из Москвы в Тверь.

```sql
SELECT DISTINCT  TrainNr FROM Connection
INNER JOIN Station fromSt
 ON fromSt.Name = FromStation
INNER JOIN Station toSt
 ON toSt.Name = ToStation
WHERE fromSt.CityName = "Москва"
 AND toSt.CityName = "Тверь"
EXCEPT
SELECT DISTINCT TrainNr FROM Connection
INNER JOIN Station fromSt
 ON fromSt.Name = FromStation
INNER JOIN Station toSt
 ON toSt.Name = ToStation
WHERE fromSt.CityName != 'Москва'
 OR toSt.CityName != 'Тверь'
```

б) Найдите все многосегментные маршруты, имеющие точно однодневный трансфер из Москвы в Санкт-Петербург (первое отправление и прибытие в конечную точку должны быть в одну и ту же дату). Вы можете применить функцию DAY () к атрибутам Departure и Arrival, чтобы определить дату.

```sql
SELECT DISTINCT TrainNr FROM Connection
INNER JOIN Station fromSt
 ON fromSt.Name = Connection.FromStation
INNER JOIN Station toSt
 ON toSt.Name = Connection.ToStation
WHERE fromSt.CityName = 'Москва'
 AND toSt.CityName = 'Санкт-Петербург'
 AND day(Arrival) = day(Departure)
INTERSECT
SELECT DISTINCT TrainNr FROM Connection
INNER JOIN Station fromSt
 ON fromSt.Name = Connection.FromStation
INNER JOIN Station toSt
 ON toSt.Name = Connection.ToStation
WHERE fromSt.CityName != 'Москва'
 OR toSt.CityName != 'Санкт-Петербург'
```

в) Что изменится в выражениях для а) и б), если отношение "Connection" не содержит дополнительных кортежей для транзитивного замыкания, поэтому многосегментный маршрут Москва-> Тверь-> Санкт-Петербург содержит только кортежи Москва-> Тверь и Тверь-Санкт-Петербург?</br>
а)

```sql
SELECT DISTINCT  TrainNr FROM Connection
INNER JOIN Station fromSt
 ON fromSt.Name = FromStation
INNER JOIN Station toSt
 ON toSt.Name = ToStation
WHERE fromSt.CityName = "Москва"
 AND toSt.CityName = "Тверь"
```

б)

```sql
SELECT DISTINCT connPart1.TrainNr FROM Connection connPart1
INNER JOIN Connection connPart2
 ON connPart2.FromStation = connPart1.ToStation
  AND connPart2.TrainNr = connPart1.TrainNr
INNER JOIN Station fromSt
 ON fromSt.Name = connPart1.FromStation
INNER JOIN Station toSt
 ON toSt.Name = connPart2.ToStation
WHERE day(connPart1.Arrival) = day(connPart2.Departure)
 AND fromSt.CityName = 'Москва'
 AND toSt.CityName = 'Санкт-Петербург'
```

### Задача 3

Представьте внешнее объединение (outer join ) в виде выражения реляционной алгебры с использованием только базовых операций (select, project, cartesian, rename, union, minus)

Пусть</br>
R1 = {A1, A2, ...}</br>
R2 = {B1, B2, ...}

Тогда

Inner Join(A, B, stipulation) = project[A_1, ..., A_n, B_1, ..., B_m](select(cartesian(A, B), stipulation))

Outer Join(A, B, condition) = union( Inner Join(A, B, stipulation), project[A_1, ..., A_n, NULL, ..., NULL](A - project[A_1, ..., A_n](Inner Join(A, B))), project[NULL, ..., NULL, B_1, ..., B_m](B - project[B_1, ..., B_m](Inner Join(A, B))))
