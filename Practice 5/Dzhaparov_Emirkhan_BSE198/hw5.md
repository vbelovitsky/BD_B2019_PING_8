# Джапаров Эмирхан, БПИ198

## Задание 5

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

``` sql
SELECT title, author
FROM books
```

* В какой книге наибольшее количество страниц?

``` sql
SELECT title
FROM books
ORDER BY page_count DESC
LIMIT 1;
```


* Какие авторы написали более 5 книг?
``` sql
SELECT author
FROM books
GROUP BY author
HAVING COUNT(isbn) > 5;
```

* В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?

``` sql
SELECT title
FROM books
WHERE page_count > (
    SELECT avg(page_count)
    FROM books
    );
```

* Какие категории содержат подкатегории?

``` sql
SELECT DISTINCT parent_name
FROM categories
WHERE parent_name IS NOT NULL;
```

* У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?

``` sql
SELECT author
FROM books
GROUP BY author
ORDER BY COUNT(ISBN) DESC
LIMIT 1;
```

* Какие читатели забронировали   все книги (не копии), написанные "Марком Твеном"?

``` sql
SELECT readers.number, readers.first_name
FROM readers
INNER JOIN bookings
ON readers.number = bookings.reader_number
INNER JOIN books
ON bookings.isbn = books.isbn
WHERE author LIKE '%Твен%'
GROUP BY readers.number
HAVING COUNT(books.isbn) = (
    SELECT COUNT(title)
    FROM books
    WHERE author LIKE '%Твен%'
    );
```
* Какие книги имеют более одной копии? 

``` sql
SELECT books.isbn, books.title
FROM copies
INNER JOIN books
ON copies.isbn = books.isbn
GROUP BY books.isbn
HAVING COUNT(copies.number) > 1;
```

* ТОП 10 самых старых книг

``` sql
SELECT isbn, title, year
FROM books
ORDER BY year
LIMIT 10;
```

* Перечислите все категории в категории “Спорт” (с любым уровнем вложености).

``` sql
WITH RECURSIVE sport AS
(
  SELECT name, parent_name FROM categories WHERE name = 'Sport'
  UNION ALL
  SELECT sub_cat.name, sub_cat.parent_name
  FROM categories sub_cat
  JOIN sport ON sport.name = sub_cat.parent_name
)
SELECT sport.name, sport.parent_name
FROM sport
```

### Задача 2

Напишите SQL-запросы для следующих действий:

* Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.

``` sql
INSERT INTO bookings
VALUES (
    (
    SELECT DISTINCT number
    FROM readers
    WHERE first_name = 'Василий' and last_name = 'Петров'
    ),
    4,
    123456,
    NOW() + interval '28 day');
```

* Удалить все книги, год публикации которых превышает 2000 год.
``` sql
DELETE FROM bookings
WHERE isbn IN (
    SELECT isbn
    FROM books
    WHERE year >= 2001
    );
DELETE FROM book_categories
WHERE isbn IN (
    SELECT isbn 
    FROM books 
    WHERE year >= 2001);

DELETE FROM copies
WHERE isbn IN (
    SELECT isbn 
    FROM books 
    WHERE year >= 2001);

DELETE FROM books
WHERE year >= 2001;
```

* Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).
``` sql
UPDATE bookings
SET return_date = return_date + interval '30 day'
WHERE return_date >= '01.01.2016' AND isbn IN (
    SELECT isbn
    FROM book_categories
    WHERE category_name = 'Базы данных'
    );
```

### Задача 3

Рассмотрим следующую реляционную схему:

* Student( MatrNr, Name, Semester ) 
* Check( MatrNr, LectNr, ProfNr, Note ) 
* Lecture( LectNr, Title, Credit, ProfNr ) 
* Professor( ProfNr, Name, Room )

Опишите на русском языке результаты следующих запросов:

1.
``` sql
SELECT s.Name, s.MatrNr 
FROM Student s 
WHERE NOT EXISTS ( 
    SELECT * FROM Check c WHERE c.MatrNr = s.MatrNr AND c.Note >= 4.0 ) ; 
```

Осуществляется запрос на поиск студентов, у которых в заметке (Check.Note) указано значение ниже 4, и вывод имен и номеров (Student.MatrNr) этих студентов.

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

Осуществляется запрос на вывод номера, имени каждого профессора и общего количества кредитов по всем проведенным лекциям для профессора, если он провел хотя бы одну лекцию, и 0 для последнего значения, иначе.

3.
```sql
SELECT s.Name, p.Note
  FROM Student s, Lecture lec, Check c
  WHERE s.MatrNr = c.MatrNr AND lec.LectNr = c.LectNr AND c.Note >= 4 
    AND c.Note >= ALL ( 
      SELECT c1.Note FROM Check c1 WHERE c1.MatrNr = c.MatrNr ) 
```

Осуществляется запрос на вывод наивысших значений
в заметках о проверках профессорами каждого конкретного студента, которые (значения в заметках), в добавок, не ниже 4.
