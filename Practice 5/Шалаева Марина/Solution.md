# Задача 1

Reader (***ID***, LastName, FirstName, Address, BirthDate)

Book (***ISBN***, Title, Author, PagesNum, PubYear, PubName)

Publisher (***PubName***, PubAdress)

Category (***CategoryName***, ParentCat)

Copy (***ISBN, CopyNumber***, ShelfPosition)

Borrowing (***ReaderNr, ISBN, CopyNumber***, ReturnDate)

BookCat (***ISBN, CategoryName***)

### Показать все названия книг вместе с именами издателей.

``` sql
SELECT b.Title, b.Author
FROM Book b;
```

### В какой книге наибольшее количество страниц?

``` sql
SELECT b.ISBN
FROM Book b
ORDER BY b.PagesNum DESC
LIMIT 1
```

### Какие авторы написали более 5 книг?

``` sql
SELECT b.PubName, COUNT(b.ISBN)
AS counter
FROM Book b
ORDER BY b.PubName DESC
WHERE counter > 5
```

### В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?

``` sql
SELECT b.ISBN
FROM Book b
WHERE b.PagesNum > 2 * AVG(b.PagesNum)
```

### Какие категории содержат подкатегории?

``` sql
SELECT DISTINCT cat.ParentCat
FROM Category cat
WHERE cat.ParentCat IS NOT NULL
```

### У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?

``` sql
SELECT b.PubName, COUNT(ISBN)
AS counter
FROM Book b
ORDER BY counter DESC
LIMIT 1
```

### Какие читатели забронировали все книги (не копии), написанные "Марком Твеном"?

``` sql
SELECT b.ISBN, b.Title
FROM Book b
JOIN Borrowing borrow 
ON b.ISBN = borrow.ISBN
WHERE b.PubName = 'Марк Твен'
AND borrow.ReturnDate > NOW()
```

### Какие книги имеют более одной копии?

``` sql
SELECT c.ISBN, COUNT(c.CopyNumber)
AS counter
FROM Copy c
WHERE counter > 1
```

### ТОП 10 самых старых книг

``` sql
SELECT b.ISBN, b.PubYear
FROM Book b
ORDER BY b.PubYear ASC
LIMIT 10
```

### Перечислите все категории в категории “Спорт” (с любым уровнем вложености).

``` sql
WITH RECURSIVE Sport
AS (SELECT cat.CategoryName
FROM Category cat
WHERE cat.ParentCat = 'Спорт'
UNION ALL
SELECT cat1.CategoryName
FROM Category cat1
WHERE cat1.ParentCat = cat)
SELECT *
FROM Sport;
```

# Задача 2

### Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.

``` sql
INSERT INTO Borrowing borrow
VALUES
(SELECT r.ID
FROM Reader r
WHERE r.LastName = 'Петров' 
AND r.FirstName = 'Василий'),
123456,
4, 
NOW() + 14;
```

### Удалить все книги, год публикации которых превышает 2000 год.

``` sql
DELETE FROM Book b
JOIN Copy c 
ON b.ISBN = c.ISBN
WHERE b.PubYear > 2000;
```

### Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).

``` sql
UPDATE Borrowing borrow
SET borrow.ReturnDate = borrow.ReturnDate + 30
WHERE borrow.ReturnDate >= 01.01.2016
AND borrow.ISBN IN
(SELECT ISBN
FROM BookCat bc
WHERE bc.CategoryName = 'Базы Данных');
```

# Задача 3

Student (MatrNr, Name, Semester)

Check (MatrNr, LectNr, ProfNr, Note)

Lecture (LectNr, Title, Credit, ProfNr)

Professor (ProfNr, Name, Room)

Опишите на русском языке результаты следующих запросов:

1) Вывести студентов, работы которых были проверены и получили оценку ниже 4.

``` sql
SELECT s.Name, s.MatrNr FROM Student s 
WHERE NOT EXISTS
(SELECT * FROM Check c
WHERE c.MatrNr = s.MatrNr
AND c.Note >= 4.0); 
```

2) Вывести профессоров и сумму кредитов (весов?) их лекций. Если лекций у профессора нет, то вместо суммы выводим 0.

``` sql
(SELECT p.ProfNr, p.Name, sum(lec.Credit) 
FROM Professor p, Lecture lec 
WHERE p.ProfNr = lec.ProfNr
GROUP BY p.ProfNr, p.Name)
UNION
(SELECT p.ProfNr, p.Name, 0 
FROM Professor p
WHERE NOT EXISTS
(SELECT * FROM Lecture lec
WHERE lec.ProfNr = p.ProfNr)); 
```

3) Вывести имена студентов и их наивысшие оценки в случае, если они больше 4. 

``` sql
SELECT s.Name, p.Note
FROM Student s, Lecture lec, Check c
WHERE s.MatrNr = c.MatrNr
AND lec.LectNr = c.LectNr AND c.Note >= 4 
AND c.Note >= ALL
(SELECT c1.Note
FROM Check c1
WHERE c1.MatrNr = c.MatrNr); 
```
