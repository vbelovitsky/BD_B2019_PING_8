## Кирюхин Андрей, задание 5
### Задача 1

Возьмите реляционную схему для библиотеки сделаную в задании 3.1: 

* Reader( <ins>ID</ins>, LastName, FirstName, Address, BirthDate)  <br>
* Book ( <ins>ISBN</ins>, Title, Author, PagesNum, PubYear, PubName)  <br>
* Publisher ( <ins>PubName</ins>, PubAdress)  <br>
* Category ( <ins>CategoryName</ins>, ParentCat)  <br>
* Copy ( <ins>ISBN, CopyNumber</ins>,, ShelfPosition)  <br>

* Borrowing ( <ins>ReaderNr, ISBN, CopyNumber</ins>, ReturnDate)  <br>
* BookCat ( <ins>ISBN, CategoryName</ins> )  

Напишите SQL-запросы:

* Показать все названия книг вместе с именами издателей.
```sql
SELECT Title, PubName FROM Book;
```
* В какой книге наибольшее количество страниц?
```sql
SELECT Title FROM Book WHERE MAX(PagesNum) = PagesNum; 
```
* Какие авторы написали более 5 книг?
```sql
SELECT Author FROM Book 
GROUP BY Author HAVING count(*) > 5;
```
* В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?
```sql
SELECT * FROM Book 
  WHERE PagesNum > 2 * AVG(PagesNum);
```
* Какие категории содержат подкатегории?
```sql
SELECT DISTINCT cat.CategoryName FROM Category cat
INNER JOIN Category cat2
 ON cat.CategoryName = cat2.ParentCat;
```
* У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?
```sql
SELECT Author, count(*) AS BooksCount 
    FROM Book
    GROUP BY Author 
    ORDER BY BooksCount
    DESC LIMIT 1;
```
* Какие читатели забронировали   все книги (не копии), написанные "Марком Твеном"?
```sql
SELECT reader.ID FROM Reader reader
    JOIN Borrowing borr ON reader.ID = borr.ReaderNr 
        JOIN Book book ON book.ISBN = borr.ISBN
    WHERE book.Author = 'Марк Твен'
        GROUP BY reader.ID HAVING COUNT(*) = (
	    SELECT COUNT(*) FROM Book book2 
                WHERE book2.Author = 'Марк Твен'
        );
```
* Какие книги имеют более одной копии? 
```sql
 SELECT ISBN FROM Copy 
  GROUP BY ISBN 
  HAVING COUNT(*) > 1;
```
* ТОП 10 самых старых книг
```sql
SELECT * FROM Book
    ORDER BY PubYear
    LIMIT 10;
```
* Перечислите все категории в категории “Спорт” (с любым уровнем вложености).
```sql
WITH RECURSIVE _category AS
(
  SELECT * FROM Category
    WHERE ParentCat = 'Спорт' UNION ALL
  SELECT category.CategoryName, category.ParentCat FROM Category
    JOIN _category 
        ON ParentCat = _category.CategoryName
)

SELECT DISTINCT CategoryName FROM _category;
```
### Задача 2

Напишите SQL-запросы для следующих действий:

* Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.
```sql
INSERT INTO Borrowing (ReaderNr, ISBN, CopyNumber, ReturnDate)
SELECT ID, "123456", 4, Null FROM Reader reader
WHERE reader.FirstName = 'Василий' AND reader.LastName = 'Петров';
```
* Удалить все книги, год публикации которых превышает 2000 год.
```sql
DELETE FROM Copy
    WHERE ISBN IN (SELECT ISBN FROM Book 
    WHERE PubYear > 2000);
                    
DELETE FROM Borrowing
    WHERE ISBN IN (SELECT ISBN FROM Book 
    WHERE PubYear > 2000);
                    
DELETE FROM BookCat
    WHERE ISBN IN (SELECT ISBN FROM Book 
    WHERE PubYear > 2000);

DELETE FROM Book WHERE PubYear > 2000;
```
* Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).
```sql
UPDATE Borrowing
SET ReturnDate = ReturnDate + day(30)
WHERE ISBN in (SELECT ISBN FROM BookCat WHERE CategoryName = "Базы данных");
    AND ReturnDate >= Date("01.01.2016")

```

### Задача 3

Рассмотрим следующую реляционную схему:

* Student( MatrNr, Name, Semester ) 
* Check( MatrNr, LectNr, ProfNr, Note ) 
* Lecture( LectNr, Title, Credit, ProfNr ) 
* Professor( ProfNr, Name, Room )

Опишите на русском языке результаты следующих запросов:

1.
```sql
SELECT s.Name, s.MatrNr FROM Student s 
  WHERE NOT EXISTS ( 
    SELECT * FROM Check c WHERE c.MatrNr = s.MatrNr AND c.Note >= 4.0 ) ; 
```
Возвращает имя и номер студента, у которого все оценки меньше 4.

2.
```sql
( SELECT p.ProfNr, p.Name, sum(lec.Credit) 
FROM Professor p, Lecture lec 
WHERE p.ProfNr = lec.ProfNr
GROUP BY p.ProfNr, p.Name)
UNION
( SELECT p.ProfNr, p.Name, 0 
FROM Professor p
WHERE NOT EXISTS ( 
  SELECT * FROM Lecture lec WHERE lec.ProfNr = p.ProfNr )); 
```
Возвращает номер студента, имя, сумму кредитов у профессоров. А если лекций у профессора нет, то кредитов - ноль. 

3.
```sql
SELECT s.Name, p.Note
  FROM Student s, Lecture lec, Check c
  WHERE s.MatrNr = c.MatrNr AND lec.LectNr = c.LectNr AND c.Note >= 4 
    AND c.Note >= ALL ( 
      SELECT c1.Note FROM Check c1 WHERE c1.MatrNr = c.MatrNr ) 
```
Возвращает имя и оценки студентов, у которых макс балл >= 4.
