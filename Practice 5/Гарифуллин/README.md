# Гарифуллин Руслан Ильфатович Задание 5
## Задача 1
 - *Показать все названия книг вместе с именами издателей*
```sql
SELECT title, publisher_name FROM books
```
 - *В какой книге наибольшее количество страниц?*
```sql
SELECT title, page_count FROM books
WHERE page_count = (SELECT MAX(page_count) FROM books)
```
 - *Какие авторы написали более 5 книг?*
```sql
SELECT author FROM books 
GROUP BY author HAVING COUNT(author) > 5
```
 - *В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?*
```sql
SELECT title, page_count FROM books
WHERE page_count > (SELECT AVG(page_count) FROM books) * 2
```
 - *Какие категории содержат подкатегории?*
```sql
SELECT DISTINCT parent_name FROM categories WHERE parent_name IS NOT NULL
```
 - *У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?*
```sql
SELECT Author FROM 
(SELECT Author, COUNT(*) FROM books GROUP BY Author)
ORDER BY count DESC
LIMIT 1
```
 - *Какие читатели забронировали все книги (не копии), написанные "Марком Твеном"?*
```sql
SELECT last_name, first_name FROM (
    SELECT number, last_name, first_name, count(*) as count FROM (
        SELECT reader.number, reader.last_name, reader.first_name, book.isbn
        FROM readers reader
            JOIN bookings booking ON booking.reader_number = reader.number
            JOIN books book ON book.isbn = booking.isbn
        WHERE book.author = 'Марк Твен'
        GROUP BY reader.number, reader.last_name, reader.first_name, book.isbn
    ) c GROUP BY number, last_name, first_name
) d WHERE count = (SELECT COUNT(*) FROM books WHERE author = 'Марк Твен')
```
 - *Какие книги имеют более одной копии?*
```sql
SELECT book.isbn, copy.count FROM books book
INNER JOIN (SELECT isbn, count(*) FROM copies GROUP BY isbn) AS copy
    ON book.isbn = copy.isbn
WHERE count > 1
```
 - *ТОП 10 самых старых книг*
```sql
SELECT isbn, title, year FROM books
ORDER BY year
LIMIT 10
```
 - *Перечислите все категории в категории “Спорт” (с любым уровнем вложености).*
```sql
```
## Задача 2
 - *Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.*
```sql
INSERT INTO bookings (reader_number, copy_number, isbn, return_date)
    SELECT number, 4, '123456', (NOW() + 7 days) FROM readers reader
    WHERE reader.first_name = 'Василий' AND reader.last_name = 'Петров'
```
 - *Удалить все книги, год публикации которых превышает 2000 год.*
```sql
DELETE FROM bookings WHERE isbn IN
    (SELECT isbn FROM books WHERE year > 2000);
DELETE FROM books WHERE year > 2000;
```
 - *Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).*
```sql
UPDATE bookings booking, book_categories category
SET return_date = (return_date + 30 days)
WHERE booking.return_date >= to_date('01 01 2016', 'DD MM YYYY') 
    AND booking.ISBN = category.isbn 
    AND category.category_name = 'Базы Данных'
```
## Задача 3
1. Из таблицы студентов выбираются те, у кого нет ни одной оценки больше или равной 4.0.