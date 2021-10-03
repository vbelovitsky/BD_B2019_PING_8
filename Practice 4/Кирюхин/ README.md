## Кирюхин Андрей, задание 4
### Задача 1
Reader( ID, LastName, FirstName, Address, BirthDate)<br> Book ( ISBN, Title, Author, PagesNum, PubYear, PubName <br> Publisher ( PubName, PubAdress)<br>Category ( CategoryName, ParentCat) Copy ( ISBN, CopyNumber, ShelfPosition) <br><br>Borrowing ( ReaderNr, ISBN, CopyNumber, ReturnDate)<br> BookCat ( ISBN, CategoryName )

#### а) Какие фамилии читателей в Москве?
```sql
SELECT LastName FROM Reader WHERE Address LIKE "%Москва%"
```
#### б) Какие книги (author, title) брал Иван Иванов?
```sql
SELECT Author, Title FROM Book
INNER JOIN Borrowing
ON Book.ISBN = Borrowing.ISBN
INNER JOIN Reader
ON Reader.ID == Borrowing.ReaderNr
WHERE Reader.LastName = 'Иванов' AND Reader.FirstName = 'Иван'
```
#### в) Какие книги (ISBN) из категории "Горы" не относятся к категории "Путешествия"? Подкатегории не обязательно принимать во внимание!
```sql
SELECT bc1.ISBN FROM BookCat bc1
INNER JOIN BookCat bc2
ON bc1.ISBN = bc2.ISBN AND bc1.CategoryName = "Горы" AND bc2.CategoryName = 'Горы'
WHERE bc1.ISBN NOT IN
(SELECT BookCat.ISBN FROM BookCat where BookCat.CategoryName = "Путешествия")
```
#### г) Какие читатели (LastName, FirstName) вернули копию книгу?
```sql
SELECT DISTINCT Reader.LastName, Reader.FirstName FROM Borrowing 
INNER JOIN Reader, Copy
ON Reader.ID = Borrowing.ReaderNr
WHERE Borrowing.ReturnDate < GETDATE() AND Copy.ISBN = Borrowing.ISBN
```
#### д) Какие читатели (LastName, FirstName) брали хотя бы одну книгу (не копию), которую брал также Иван Иванов (не включайте Ивана Иванова в результат)?
```sql
SELECT DISTINCT Reader.FirstName, Reader.LastName FROM Reader
INNER JOIN Borrowing
ON Reader.ID = Borrowing.ReaderNr AND Reader.LastName != 'Иван' AND Reader.FirstName != 'Иванов'
WHERE ISBN in
(
 SELECT ISBN FROM Borrowing
 INNER JOIN Reader r2
 ON r2.ID = Borrowing.ReaderNr
 WHERE r2.FirstName = 'Иван' AND r2.LastName = 'Иванов'
)
```

### Задача 2
City ( Name, Region )<br>Station ( Name, #Tracks, CityName, Region )<br>Train ( TrainNr, Length, StartStationName, EndStationName )<br>Conntection( FromStation, ToStation, TrainNr, Departure, Arrival)

#### а) Найдите все прямые рейсы из Москвы в Тверь. 
```sql
SELECT DISTINCT TrainNr FROM Connection
WHERE Connection.CityName = 'Москва' AND Connection.CityName = 'Тверь'
```
#### б) Найдите все многосегментные маршруты, имеющие точно однодневный трансфер из Москвы в Санкт-Петербург (первое отправление и прибытие в конечную точку должны быть в одну и ту же дату). Вы можете применить функцию DAY () к атрибутам Departure и Arrival, чтобы определить дату. 
```sql
SELECT с.TrainNr FROM Сonnection с
JOIN station s1 
    ON s1.Name = с.FromStation 
JOIN station s2 
    ON s2.Name = с.ToStation 
WHERE DAY(Arrival) - DAY(Departure) = 0 AND s1.CityName = "Москва" AND s2.CityName = "Санкт-Петерберг";
```
#### в) Что изменится в выражениях для а) и б), если отношение "Connection" не содержит дополнительных кортежей для транзитивного замыкания, поэтому многосегментный маршрут Москва-> Тверь-> Санкт-Петербург содержит только кортежи Москва-> Тверь и Тверь-Санкт-Петербург?

#### в.а)
```sql
SELECT DISTINCT TrainNr FROM Connection
JOIN Station
    ON Station.Name = Connection.FromStation
JOIN Station s2
    ON s2.Name = Connection.ToStation
WHERE Station.CityName = 'Москва' AND s2.CityName = 'Тверь'
```
#### в.б)
```sql
SELECT DISTINCT TrainNr FROM Connection
JOIN Station
    ON Station.Name = Connection.FromStation 
JOIN Station s2 
    ON s2.Name = Connection.ToStation
WHERE Station.CityName = "Москва" AND s2.CityName = "Санкт-Петербург" AND Arrival - Departure = 0;
```