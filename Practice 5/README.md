# Задание 1
Возьмите реляционную схему для библиотеки сделаную в задании 3.1:

Reader( ID, LastName, FirstName, Address, BirthDate)

Book ( ISBN, Title, Author, PagesNum, PubYear, PubName)

Publisher ( PubName, PubAdress)

Category ( CategoryName, ParentCat)

Copy ( ISBN, CopyNumber,, ShelfPosition)

Borrowing ( ReaderNr, ISBN, CopyNumber, ReturnDate)

BookCat ( ISBN, CategoryName )


Напишите SQL-запросы:

### Показать все названия книг вместе с именами издателей. 
```sql
SELECT Title, PubName FROM Book;
```
### В какой книге наибольшее количество страниц? 
```sql
SELECT * FROM Book WHERE PagesNum = (SELECT max(PagesNum) FROM Book);
```
### Какие авторы написали более 5 книг? 
```sql
SELECT Author FROM Book GROUP BY Author HAVING count(*) > 5;
```
### В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг? 
```sql
SELECT * FROM Book WHERE PagesNum > (SELECT avg(PagesNum) * 2 FROM Book);
```
### Какие категории содержат подкатегории? 
```sql
SELECT DISTINCT a.CategoryName FROM Category a 
INNER JOIN Category b 
ON a.CategoryName = b.ParentCat;
```
### У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг? 

```sql
SELECT Author FROM Book
WHERE Author = (SELECT Author FROM (SELECT Author, count(*) FROM Book
                                    GROUP BY Author 
                                    ORDER BY count DESC
                                    LIMIT 1) AS AuthorCount);
```
### Какие читатели забронировали все книги (не копии), написанные "Марком Твеном"?
```sql
SELECT r.ID, r.LastName, r.FirstName, r.Address, r.BirthDate FROM
  (SELECT r.ID, r.LastName, r.FirstName, r.Address, r.BirthDate, COUNT(*) as count
   FROM Reader r
      JOIN Borrowing bor ON bor.ReaderNR = r.ID
      JOIN Book book ON book.ISBN = bor.ISBN
    WHERE Author = "Марк Твен" GROUP BY r.ID)
WHERE count = (SELECT COUNT(*) FROM Book b WHERE b.Author = "Марк Твен");
```
### Какие книги имеют более одной копии? 
```sql
SELECT * FROM Book
  INNER JOIN (SELECT ISBN, count(*) FROM Copy GROUP BY ISBN) AS BookNumber
        ON Book.ISBN = BookNumber.ISBN
WHERE count > 1;
```

### ТОП 10 самых старых книг 
```sql
SELECT * FROM Book
ORDER BY PubYear
LIMIT 10;
```
### Перечислите все категории в категории “Спорт” (с любым уровнем вложености).
```sql
WITH RECURSIVE current_category AS
(
  SELECT * FROM Category WHERE Category.ParentCat = 'Спорт' UNION ALL
  SELECT subcategory.* FROM Category subcategory JOIN current_category ON subcategory.ParentCat = current_category.CategoryName
)
SELECT DISTINCT CategoryName FROM current_category;
```
# Задача 2
Напишите SQL-запросы для следующих действий:
### Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.
```sql
INSERT INTO Borrowing (ReaderNr, ISBN, CopyNumber, ReturnDate)
  SELECT ID, '123456', 4, null FROM Reader WHERE FirstName = 'Василий' AND LastName = 'Петров';
```

### Удалить все книги, год публикации которых превышает 2000 год.

```sql
DELETE FROM Borrowing
WHERE ISBN IN (
                    SELECT ISBN FROM Book
                    WHERE PubYear > 2000
              );
              
DELETE FROM Copy
WHERE ISBN IN (
                    SELECT ISBN FROM Book
                    WHERE PubYear > 2000
              );
              
DELETE FROM BookCat
WHERE ISBN IN (
                    SELECT ISBN FROM Book
                    WHERE PubYear > 2000
              );
              
DELETE FROM Book
WHERE PubYear > 2000;
```

### Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).

```sql
UPDATE Borrowing b, BookCat bc
SET ReturnDate = ReturnDate + 30 days
WHERE b.ReturnDate >= to_date('01 01 2016', 'DD MM YYYY') AND b.ISBN = bc.ISBN AND bc.CategoryName = 'Базы Данных';
```

# Задание 3
Рассмотрим следующую реляционную схему:

Student( MatrNr, Name, Semester )
Check( MatrNr, LectNr, ProfNr, Note )
Lecture( LectNr, Title, Credit, ProfNr )
Professor( ProfNr, Name, Room )
Опишите на русском языке результаты следующих запросов:


## 3.1 
В результате будут выбраны имена и номера зачисления всех студентов, у которых нет ни одной оценки >= 4.

## 3.2
В результате будут выбраны все профессора с их суммой кредитов, которые они получат за все лекции. Причём, если у профессора нет лекций, то он получит 0 кредитов.
