# Практическое задание 5

> Абдельсалам Шади Мазен, БПИ198

## Задание 1

- Показать все названия книг вместе с именами издателей. 
```sql
SELECT title, publisher_name FROM books;
```

- В какой книге наибольшее количество страниц?
```sql
SELECT ISBN, title FROM books
ORDER BY page_count DESC
FETCH FIRST 1 rows only;
```

- Какие авторы написали более 5 книг?
```sql
SELECT DISTINCT author FROM books
GROUP BY author
HAVING count(*) > 5;
```

- В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?
```sql
SELECT ISBN, title FROM books
WHERE books.page_count > 2 * (
	SELECT avg(books.page_count) FROM books
);
```

- Какие категории содержат подкатегории?
```sql
SELECT DISTINCT cat2.name  FROM categories cat1
JOIN categories cat2 ON cat1.parent_name = cat2.name;
```

- У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?
```sql
SELECT DISTINCT author, count(*) as book_count FROM books
GROUP BY author
ORDER BY book_count DESC
LIMIT 1;
```

- Какие читатели забронировали все книги (не копии), написанные "Марком Твеном"?

- Какие книги имеют более одной копии?
```sql
SELECT ISBN, title FROM (
    SELECT books.ISBN, title, count(*) as book_count
    FROM books
             JOIN copies ON books.isbn = copies.isbn
    GROUP BY books.ISBN
) AS c WHERE book_count > 1;
```

- ТОП 10 самых старых книг
```sql
SELECT ISBN, title, year FROM Books
ORDER BY year
LIMIT 10;
```

- Перечислите все категории в категории “Спорт” (с любым уровнем вложености).
```sql
WITH RECURSIVE r AS (
	SELECT cat1.name, cat1.parent_name FROM categories cat1
	WHERE cat1.name = 'Sport'
	UNION
	SELECT cat2.name, cat2.parent_name FROM categories cat2
	JOIN r ON cat2.parent_name = r.name
)

SELECT name FROM r;
```

## Задание 2

- Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.
```sql
INSERT INTO bookings (reader_number, ISBN, copy_number, return_date)
SELECT number, 123456, 4, now() FROM readers
WHERE readers.first_name = 'Василий' AND readers.last_name = 'Петров';
```

- Удалить все книги, год публикации которых превышает 2000 год.
```sql
DELETE FROM books WHERE books.year > 2000;
```

- Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).
```sql
UPDATE bookings
SET return_date = return_date + 30
WHERE bookings.return_date >= '2016-01-01'::date
AND EXISTS (
    SELECT * FROM books
    JOIN book_categories bc on books.isbn = bc.isbn
    WHERE books.ISBN = bookings.ISBN AND bc.category_name = 'Databases'
);
```

## Задание 3

1. Все студенты, которые не получали оценок выше или равной 4
```sql
SELECT s.Name, s.MatrNr FROM Student s 
  WHERE NOT EXISTS ( 
    SELECT * FROM Check c WHERE c.MatrNr = s.MatrNr AND c.Note >= 4.0 ) ; 
```

2. Суммарное количество кредитов для каждого профессора, если он не проводил лекций, то сумма кредитов считается равной 0
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

3. Максимальные оценки каждого студента по всем посещенным ими лекциям
```sql
SELECT s.Name, p.Note
  FROM Student s, Lecture lec, Check c
  WHERE s.MatrNr = c.MatrNr AND lec.LectNr = c.LectNr AND c.Note >= 4 
    AND c.Note >= ALL ( 
      SELECT c1.Note FROM Check c1 WHERE c1.MatrNr = c.MatrNr ) 
```