# Задание 1.

 Ф) Какие фамилии читателей в Москве?
 ```sql
 select LastName from Reader where Reader.Address = 'Moscow';
 ```
 Б) Какие книги (author, title) брал Иван Иванов?   

 ```sql
 select distinct Book.Author, Book.Title
 from Borrowing inner join Book on Borrowing.ISBN = Book.ISBN
 where Borrowing.ReaderNr = (select Reader.ID from Reader where Reader.LastName = 'Иванов' and Reader.FirstName = 'Иван');
 ```

 В) Какие книги (ISBN) из категории "Горы" не относятся к категории "Путешествия"? Подкатегории не обязательно принимать во внимание!
 ```sql
 select distinct BookCat.ISBN from BookCat where BookCat.CategoryName = 'Горы' except select distinct BookCat.ISBN from BookCat where BookCat.CategoryName = 'Путешествия';
 ```

 Г) Какие читатели (LastName, FirstName) вернули копию книгу?
 ```sql
 select distinct Reader.LastName, Reader.FirstName from Reader
 where Reader.ID in (select Borrowing.ReaderNr from Borrowing where Borrowing.ReturnDate < datetime());
 ```

 Д) Какие читатели (LastName, FirstName) брали хотя бы одну книгу (не копию), которую брал также Иван Иванов (не включайте Ивана Иванова в результат)?
 ```sql
 select distinct Reader.LastName, Reader.FirstName from Borrowing left join Reader on Borrowing.ReaderNr = Reader.ID
 where Borrowing.ISBN in (
     select distinct Borrowing.ISBN from Borrowing
     where Borrowing.ReaderNr in (select Reader.ID from Reader where Reader.LastName = 'Иванов' and Reader.FirstName = 'Иван')) 
 and not (FirstName = 'Иван' and LastName = 'Иванов');
 ```

 # Задание 2

 Ф) Найдите все прямые рейсы из Москвы в Тверь.

 ```sql
 select distinct TrainNr from Connection
 join station stat1 on stat1.Name = connection.FromStation join station stat2 on stat2.Name = connection.ToStation
 where stat1.CityName = 'Moscow' and stat2.CityName = 'Tver';
 ```

 Б) Найдите все многосегментные маршруты, имеющие точно однодневный трансфер из Москвы в Санкт-Петербург (первое отправление и прибытие в конечную точку должны быть в одну и ту же дату). Вы можете применить функцию DAY () к атрибутам Departure и Arrival, чтобы определить дату.
 ```sql
 select Connection.TrainNr from Connection 
 join Station as statfrom on Connection.FromStation = statfrom.Name join Station as statTo on Connection.ToStation = statTo.Name
 where statfrom.CityName = 'Москва' and statTo.CityName = 'Санкт-Петербург' and day(Connection.Departure) = day(Connection.Arrival)
 and Connection.TrainNr in (select TrainNr from Connection group by TrainNr having count(TrainNr) > 1)
 ```

 В) Что изменится в выражениях для а) и б), если отношение "Connection" не содержит дополнительных кортежей для транзитивного замыкания, поэтому многосегментный маршрут Москва-> Тверь-> Санкт-Петербург содержит только кортежи Москва-> Тверь и Тверь-Санкт-Петербург?

 а) stays same

 б) Изменится на:
 ```sql
 select Con1.TrainNr from Train join Connection as Con1 on Train.TrainNr = Con1.TrainNr
 join Connection as Con2 on Train.TrainNr = Con2.TrainNr
 join Station as St1 on Con1.FromStation = Train.StartStationName
 join Station as St2 on Con2.FromStation = Train.EndStationName
 where St1.CityName = 'Москва' and St2.CityName = 'Санкт-Петербург' and day(Con1.Departure) = day(Con2.Arrival)
 and Con1.TrainNr in (select TrainNr from Connection group by TrainNr having count(TrainNr) > 1)
 ```
