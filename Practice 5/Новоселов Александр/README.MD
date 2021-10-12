## Задача 5.1

* Показать все названия книг вместе с именами издателей.

```sql
SELECT b.Title, b.PubName FROM Book b;
```

* В какой книге наибольшее количество страниц?

```sql
SELECT * FROM Book b
WHERE b.PagesNum = (
SELECT MAX(b2.PagesNum) FROM Book b2);
```

* Какие авторы написали более 5 книг?

```sql
SELECT Author, COUNT(Author) FROM Book
GROUP BY Author
HAVING COUNT(Author) > 5;
```

* В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?

```sql
SELECT * FROM Book b
WHERE b.PagesNum > (
SELECT 2 * AVG(b2.PagesNum) FROM Book b2);
```

* Какие категории содержат подкатегории?

```sql
SELECT DISTINCT c1.CategoryName FROM Category c1
INNER JOIN Category c2 ON c1.CategoryName = c2.ParentCat;
```

* У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?

```sql
SELECT author, COUNT(author) as count FROM books
GROUP BY author
ORDER BY count DESC
LIMIT 1;
```

* Какие читатели забронировали все книги (не копии), написанные "Марком Твеном"?

```sql
SELECT r.ID, r.LastName, r.FirstName, r.Address, r.BirthDate FROM
( SELECT r.ID, r.LastName, r.FirstName, r.Address, r.BirthDate, COUNT(*) as count
FROM Reader r
JOIN Borrowing bor ON bor.ReaderNR = r.ID
JOIN Book book ON book.ISBN = bor.ISBN
WHERE Author = 'Марк Твен'
GROUP BY r.ID)
WHERE count = (
SELECT COUNT(*) FROM Book b
WHERE b.Author = 'Марк Твен'
);
```

* Какие книги имеют более одной копии?

```sql
SELECT c.ISBN FROM Copy c
GROUP BY c.ISBN
HAVING COUNT(*) > 1;
```

* ТОП 10 самых старых книг

```sql
SELECT * FROM Book b
ORDER BY b.PubYear
LIMIT 10;
```

* Перечислите все категории в категории “Спорт” (с любым уровнем вложености).

```sql
WITH RECURSIVE category AS
(
SELECT c.CategoryName, c.ParentCat FROM Category c
WHERE c.ParentCat = 'Спорт'
UNION ALL
SELECT category.CategoryName, category.ParentCat
FROM category, Category c2
WHERE category.CategoryName = c2.ParentCat
)
SELECT * FROM category;
```

## Задача 5.2

* Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.

```sql
INSERT INTO Borrowing (ReaderNr, ISBN, CopyNumber, ReturnDate)
SELECT ID, "123456", 4, Null FROM Reader r
WHERE r.FirstName = 'Василий' AND r.LastName = 'Петров' ;
```

* Удалить все книги, год публикации которых превышает 2000 год.

```sql
DELETE FROM Copy c
WHERE c.ISBN IN (SELECT ISBN FROM Book b
WHERE b.PubYear > 2000) ;

DELETE FROM Borrowing bor
WHERE bor.ISBN IN (SELECT ISBN FROM Book b
WHERE b.PubYear > 2000) ;

DELETE FROM BookCat bc
WHERE bc.ISBN IN (SELECT ISBN FROM Book b
WHERE b.PubYear > 2000) ;
DELETE FROM Book b
WHERE b.PubYear > 2000 ;
```

* Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).

```sql
UPDATE Borrowing bor
SET bor.ReturnDate = bor.ReturnDate + day(30)
WHERE bor.ISBN in ( SELECT ISBN FROM BookCat bc
WHERE bc.CategoryName = 'Базы данных' )
AND bor.ReturnDate >= Date('01.01.2016')
```

## Задача 5.3

* 1. Возвращает имя и номер для тех студентов, у которых все оценки меньше 4.0.

* 2. Возвращают номер, имя и сумма всех кредитов у профессоров. Если у профессора совсем нет лекций, то он получает 0 кредитов.

* 3. Возвращает имя и оценку студентов, имеющих наивысший балл больше или равный 4