# Практическое задание №4

## Рычков Кирилл, БПИ198

## Задание 1

```sql
а) SELECT LastName FROM Reader WHERE Adress LIKE Moscow;
```

```sql
б) SELECT Author, Title FROM Book 
JOIN Reader ON Borrowing.ReaderNr == Reader.ID
WHERE Reader.LastName == "Ivanov" AND Reader.FirstName == "Ivan";
```
```sql
в) SELECT DISTINCT ISBN FROM Book 
JOIN BookCat WHERE CategoryName == "Mountains"
EXCEPT SELECT DISTINCT ISBN FROM Book
JOIN BookCat WHERE CategoryName == "Travel";
```
```sql
г) SELECT LastName, FirstName FROM Reader JOIN Borrowing ON
 Borrowing.ReaderNr == Reader.ID WHERE Borrowing.ReturnDate IS NOT NULL;
 ```
 ```sql
д) SELECT LastName, FirstName FROM Reader JOIN Borrowing ON 
 Reader.ID == Borrowing.ReaderNr WHERE ISBN IN (SELECT ISBN FROM Borrowing JOIN Reader ON
  Reader.ID == Borrowing.ReaderNr WHERE LastName = “Ivanov” AND FirstName == “Ivan”) 
  EXCEPT SELECT LastName, FirstName FROM Borrowing JOIN Reader ON
   Reader.ID == Borrowing,ReaderNr WHERE LastName == “Ivanov” AND FirstName == “Ivan”;
   ```
## Задание 2

 ```sql
а) SELECT DISTINCT TrainNr FROM Connection
JOIN station S1 ON S1.Name = connection.FromStation 
JOIN station S2 ON S2.Name = connection.ToStation
WHERE S1.CityName = 'Moscow' AND S2.CityName = 'Tver';
```

 ```sql
б) SELECT DISTINCT TrainNr FROM Connection
JOIN station S1 ON S1.Name = connection.FromStation
JOIN station S2 ON S2.Name = connection.ToStation
WHERE Arrival = Departure AND S1.CityName = 'Moscow' AND S2.CityName = 'St. Petersburg';
```

