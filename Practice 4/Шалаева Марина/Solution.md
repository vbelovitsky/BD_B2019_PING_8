# Задание 4. Шалаева Марина

## Задача 1

Reader (***ID***, LastName, FirstName, Address, BirthDate)

Book (***ISBN***, Title, Author, PagesNum, PubYear, PubName)

Publisher (***PubName***, PubAdress)

Category (***CategoryName***, ParentCat)

Copy (***ISBN, CopyNumber***, ShelfPosition)

Borrowing (***ReaderNr, ISBN, CopyNumber***, ReturnDate)

BookCat (***ISBN, CategoryName***)

### а) Какие фамилии читателей в Москве?

``` sql
SELECT r.LastName
FROM Reader r
WHERE r.Address = 'Москва';
```

### б) Какие книги (author, title) брал Иван Иванов?

``` sql
SELECT b.Author, b.Title
FROM Reader r JOIN Borrowing borrow
ON r.ID = borrow.ReaderNr
BorrowReader JOIN Book b
ON borrow.ISBN = b.ISBN
WHERE r.FirstName = 'Иван' AND r.LastName = 'Иванов';
```

### в) Какие книги (ISBN) из категории "Горы" не относятся к категории "Путешествия"? Подкатегории не обязательно принимать во внимание!

``` sql
SELECT bcat.ISBN
FROM BookCat bcat
WHERE bcat.CategoryName = 'Горы' AND
bcat.CategoryName NOT IN 
SELECT DISTINCT bc.ISBN
FROM BookCat bc
WHERE bc.CategoryName != 'Путешествия';
```

### г) Какие читатели (LastName, FirstName) брали книги, которые были возвращены?

``` sql
SELECT r.FirstName, r.LastName
FROM Borrowing borrow JOIN Reader r
ON borrow.ReaderNr = r.ID
WHERE borrow.ReturnDate IS NOT NULL;
```

### д) Какие читатели (LastName, FirstName) брали хотя бы одну книгу, которую брал также Иван Иванов (не включайте Ивана Иванова в результат)?

``` sql
WITH IvanIvanov AS (SELECT ID
FROM Reader r
WHERE r.Lastname = 'Иванов' AND r.FirstName = 'Иван'),

IvanIvanovBooks AS (SELECT ISBN
FROM Borrowing borrow
WHERE ReaderNr = IvanIvanov),

ReaderNumbers AS (SELECT DISTINCT ReaderNr
FROM Borrowing
WHERE ISBN IN IvanIvanovBooks)

SELECT r.FirstName, r.LastName
FROM Reader r JOIN ReaderNumbers rn
ON r.ID = rn.ReaderNr;
```

## Задача 2

City (***Name, Region***)

Station (***Name***, #Tracks, CityName, Region) 

Train (***TrainNr***, Length, StartStationName, EndStationName)

Connection (***FromStation***, ToStation, ***TrainNr***, Departure, Arrival)

Предположим, что отношение "Connection" уже содержит транзитивное замыкание. Когда поезд 101 отправляется из Москвы в Санкт-Петербург через Тверь, содержит кортежи для связи Москва->Тверь, Тверь-Санкт-Петербург и Москва->Санкт-Петербург. Сформулируйте следующие запросы в реляционной алгебре:

### а) Найдите все прямые рейсы из Москвы в Тверь.

``` sql
SELECT con.FromStation, con.ToStation, con.TrainNr, con.Departure, con.Arrival
FROM Connection con JOIN Station startStation
ON startStation.Name = connection.fromStation 
JOIN station endStation 
ON endStation.Name = connection.toStation
WHERE startStation.cityName = 'Москва' AND endStation.cityName = 'Тверь';
```

### б) Найдите все многосегментные маршруты, имеющие точно однодневный трансфер из Москвы в Санкт-Петербург (первое отправление и прибытие в конечную точку должны быть в одну и ту же дату). Вы можете применить функцию DAY () к атрибутам Departure и Arrival, чтобы определить дату. 

Ответ:
``` sql
SELECT trainNr
FROM Connection con
JOIN Station startStation
ON stratStation.Name = con.fromStation 
JOIN station endStation
ON endStation.name = con.toStation
WHERE DAY(arrival) = DAY(departure) AND
startStation.cityName = 'Москва' AND
endStation.cityName = 'Санкт-Петербург';
```

### в) Что изменится в выражениях для а) и б), если отношение "Connection" не содержит дополнительных кортежей для транзитивного замыкания, поэтому многосегментный маршрут Москва-> Тверь-> Санкт-Петербург содержит только кортежи Москва-> Тверь и Тверь-Санкт-Петербург?

Для пункта «а» ничего не изменится, поскольку нам нужны были только прямые рейсы => многосегментные маршруты нас не интересуют.

Для пункта «б» запрос бы изменился: нужно было бы сначала выбрать те рейсы, которые идут из Москвы, затем просмотерть все следующие и выбрать только те, что заканчивают свой путь в Санкт-Петербурге. Таким образом мы бы постепенно дополняли цепь, в которой начальной точной была бы Москва, а конечной - Санкт-Петербург.

## Задача 3

### Представьте внешнее объединение (outer join) в виде выражения реляционной алгебры с использованием только базовых операций (select, project, cartesian, rename, union, minus)

inner join = Projection (Selection (E1 Cartesian E2))

![рисунок1](Рисунок1.jpg)

left outer join = inner join Union E1 \ E2

![рисунок2](Рисунок2.jpg)

right outer join = inner join Union E2 \ E1

![рисунок3](Рисунок2.jpg)
