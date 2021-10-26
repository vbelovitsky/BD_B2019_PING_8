### Задание 1

* **Показать все названия книг вместе с именами издателей.**
```sql
SELECT Title, PubName FROM Book
```

* **В какой книге наибольшее количество страниц?**
```sql
SELECT Title FROM Book 
WHERE PagesNum = MAX(PagesNum)
```

* **Какие авторы написали более 5 книг?**
```sql
SELECT Author, COUNT(Author) FROM Book 
GROUP BY Author HAVING COUNT(Author) > 5
```

* **В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?**
```sql
SELECT * FROM Book 
WHERE PagesNum > (2 * AVG(PagesNum))
```

* **Какие категории содержат подкатегории?**
```sql
SELECT DISTINCT a.CategoryName FROM Category a
INNER JOIN Category b ON a.CategoryName = b.ParentCat
```

* **У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?**
```sql
SELECT author, 
COUNT(author) as c FROM books
GROUP BY author
ORDER BY c DESC LIMIT 1
```
* **Какие читатели забронировали все книги (не копии), написанные "Марком Твеном"?**
```sql
 SELECT number FROM (
   SELECT number, COUNT(*) AS num FROM (
    SELECT distinct * from bookings 
    JOIN readers ON bookings.reader_number = readers.number
    JOIN books ON bookings.isbn = books.isbn
    WHERE books.author = "Марк Твен"
  ) AS bk GROUP BY number
) AS cnt WHERE num = (SELECT COUNT(*) FROM books WHERE author = "Марк Твен")
```

* **Какие книги имеют более одной копии?**
```sql
SELECT copies.isbn FROM copies
GROUP BY copies.isbn 
HAVING COUNT(*) > 1
```

* **ТОП 10 самых старых книг.**
```sql
 SELECT books.isbn FROM books
 ORDER BY books.year LIMIT 10 
```

* **Перечислите все категории в категории “Спорт” (с любым уровнем вложености).**
```sql
WITH RECURSIVE Sport(cat) AS (
  SELECT categories.name FROM categories
  WHERE categories.parent_name = 'Спорт'
  UNION SELECT categories.name
  FROM categories 
  JOIN Sport 
  ON categories.parent_name = Sport.cat
) SELECT * FROM Sport
```

### Задание 3

* **Найти студентов, у которых нет работ, написанных не менее, чем на 4 ровно.**
* **Найти профессоров и сумму кредитов лекций, которые они ведут. Если профессор не ведет лекций, сумма кредитов нулевая.**
* **Найти студентов и их максимальные оценки, если они не меньше 4 ровно.**

### Задание 2
* **Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.**
Не уверен, что будет, если их будет несколько, этих Василиев
```sql
INSERT INTO bookings (reader_number, isbn, copy_number, return_date)
SELECT readers.number, "123456", 4, "01.01.2001" FROM readers
WHERE firstname = "Василий" AND lastname = "Петров"
```

* **Удалить все книги, год публикации которых превышает 2000 год.**
```sql
DELETE FROM books WHERE books.year > 2000
```

* **Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).**
```sql
UPDATE bookings SET returnData = returnData + day(30)
WHERE ReturnDate >= Date("01.01.2016") AND ISBN in (
  SELECT ISBN FROM categ WHERE categoryName = "Базы данных"
)
```
