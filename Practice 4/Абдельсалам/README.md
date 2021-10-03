# Практическое задание 4

> Абдельсалам Шади Мазен, БПИ198

## Задание 1

Имеем следующую библиотечную систему:<br>
Сущности:
- Reader( ID, LastName, FirstName, Address, BirthDate) 
- Book ( ISBN, Title, Author, PagesNum, PubYear, PubName) 
- Publisher ( PubName, PubAdress) 
- Category ( CategoryName, ParentCat) 
- Copy ( ISBN, CopyNumber, ShelfPosition) 

Отношения:
- Borrowing ( ReaderNr, ISBN, CopyNumber, ReturnDate)
- BookCat ( ISBN, CategoryName )

### а) Какие фамилии читателей в Москве?
```sql
SELECT LastName from Reader WHERE ((Address LIKE '%Moscow%') or (Address LIKE '%Москва%'));
```

### б) Какие книги (author, title) брал Иван Иванов?
```sql
SELECT Author, Title FROM Book 
JOIN Borrowing ON Borrowing.ISBN = Book.ISBN
JOIN Reader ON Reader.ID = Borrowing.ReaderNr
WHERE Reader.FirstName = 'Иван' AND Reader.LastName = 'Иванов';
```

### в) Какие книги (ISBN) из категории "Горы" не относятся к категории "Путешествия"? Подкатегории не обязательно принимать во внимание!

```sql
SELECT ISBN FROM Book
JOIN BookCat ON BookCat.ISBN = Book.ISBN
WHERE BookCat.CategoryName = 'Горы' AND Book.CategoryName != 'Путешествия';
```

### г) Какие читатели (LastName, FirstName) брали книги, которые были возвращены?
```sql
SELECT LastName, FirstName FROM Reader
JOIN Borrowing ON Borrowing.ReaderNr = Reader.ID
WHERE Borrowing.ReturnDate IS NOT NULL;
-- иное условие
WHERE Borrowing.ReturnDate < GETDATE();
```

### д) Какие читатели (LastName, FirstName) брали хотя бы одну книгу, которую брал также Иван Иванов (не включайте Ивана Иванова в результат)?
```sql
SELECT LastName, FirstName FROM Reader
JOIN Borrowing ON Borrowing.ReaderNr = Reader.ID AND Reader.FirstName != 'Иван' AND READER.SecondName != 'Иванов'
WHERE ISBN IN (
	SELECT ISBN FROM Borrowing
	JOIN Reader ON Reader.ID = Borrowing.ReaderNr
	WHERE FirstName = 'Иван' AND LastName = 'Иванов'
);
```

## Задание 2
Имеем следующую схему для поездов:<br>
Сущности:
- City ( Name, Region ) 
- Station ( Name, #Tracks, CityName, Region ) 
- Train ( TrainNr, Length, StartStationName, EndStationName ) 

Отношения:
- Connection ( FromStation, ToStation, TrainNr, Departure, Arrival) 

### а) Найдите все прямые рейсы из Москвы в Тверь.
```sql
SELECT DISTINCT TrainNr FROM Connection
JOIN Station s1 ON Station.Name = Connection.FromStation
JOIN Station s2 ON Station.Name = Connection.ToStation 
WHERE s1.CityName = 'Москва' AND s2.CityName = 'Тверь'
EXCEPT 
SELECT TrainNr FROM Connection
JOIN Station s1 ON Station.Name = Connection.FromStation
JOIN Station s2 ON Station.Name = Connection.ToStation 
WHERE s1.CityName != 'Москва' OR s2.CityName != 'Тверь';
```
Для нахождения прямых рейсов необходимо исключить из найденных рейсов из Москвы в Тверь непрямые, то есть те, которые либо начинаются в Москве, либо заканчиваются в Твери (из найденных изначально).

### б) Найдите все многосегментные маршруты, имеющие точно однодневный трансфер из Москвы в Санкт-Петербург (первое отправление и прибытие в конечную точку должны быть в одну и ту же дату). Вы можете применить функцию DAY () к атрибутам Departure и Arrival, чтобы определить дату.

```sql
SELECT DISTINCT TrainNr from Connection
JOIN Station s1 ON Station.Name = Connection.FromStation
JOIN Station s2 ON Station.Name = Connection.ToStation 
WHERE DAY(Connection.Arrival) - DAY(Connection.Departure) AND s1.CityName = 'Москва' AND s2.CityName = 'Санкт-Петербург'
EXCEPT 
SELECT TrainNr FROM Connection
JOIN Station s1 ON Station.Name = Connection.FromStation
JOIN Station s2 ON Station.Name = Connection.ToStation 
WHERE s1.CityName != 'Москва' OR s2.CityName != 'Санкт-Петербург';
```

### в) Что изменится в выражениях для а) и б), если отношение "Connection" не содержит дополнительных кортежей для транзитивного замыкания, поэтому многосегментный маршрут Москва-> Тверь-> Санкт-Петербург содержит только кортежи Москва-> Тверь и Тверь-Санкт-Петербург?

а) Без транзитивных замыканий все изначально найденные рейсы будут прямыми
```sql
SELECT TrainNr FROM Connection
JOIN Station s1 ON Station.Name = Connection.FromStation
JOIN Station s2 ON Station.Name = Connection.ToStation 
WHERE s1.CityName = 'Москва' AND s2.CityName = 'Тверь'
```
б) Аналогично и здесь, однако для учитывания многосегментности необходимо рассмотреть естественное соединение Connection
```sql
SELECT DISTINCT TrainNr from Connection conn1
JOIN Connection conn2 ON conn1.ToStation = conn2.FromStation AND conn1.TrainNr = conn2.TrainNr
JOIN Station s1 ON Station.Name = Connection.FromStation
JOIN Station s2 ON Station.Name = Connection.ToStation 
WHERE DAY(Connection.Arrival) - DAY(Connection.Departure) AND s1.CityName = 'Москва' AND s2.CityName = 'Санкт-Петербург'
```

## Задание 3
Представьте внешнее объединение (outer join ) в виде выражения реляционной алгебры с использованием только базовых операций (select, project, cartesian, rename, union, minus)

Пусть есть две сущность A(A1,...,An) и B(Bn,...,Bk). Тогда полное внешнее объединение (Outer Join) - объединение внутреннего объединения (Inner Join) A и B и всех строк для A и B, не вошедниш во внутрненнее объединение   

```
Inner Join(A,B, Condition) = project[A1,..., An, B1,...,Bk](select(cartesian(A,B), Condition)) 

Outer Join(A,B,Condition) = select(union(
	Inner Join(A,B, Condition),
	project[A1,...An](minus(A project[A1,...,An](Inner Join(A,B,Condition)))),
	project[B1,...Bk](minus(B, project[B1,...,Bk](Inner Join(A,B,Condition)))),
), Condition)

```