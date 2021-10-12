## Задание 5

(Язык Postgre SQL)

### Задача 1

1. Показать все названия книг вместе с именами издателей.
```sql
   SELECT
       title,
       author
   FROM
       book;
```

2. В какой книге наибольшее количество страниц?
```sql
   SELECT
       *
   FROM
       book
   ORDER BY
       book.pages_num DESC
   LIMIT
       1;
```

3. Какие авторы написали более 5 книг?
```sql
   SELECT
       t.author
   FROM
       (
           SELECT
               author,
               Count(*) books_no
           FROM
               book
           GROUP BY
               author
       ) AS t
   WHERE
       t.books_no > 5;
```

4. В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?
```sql
   SELECT
       *
   FROM
       book
   WHERE
       pages_num > 2 * (
           SELECT
               avg(pages_num)
           FROM
               book
       );
```


5. Какие категории содержат подкатегории?
```sql
   SELECT
       category_name
   FROM
       category
   WHERE
       parent_cat IS NOT NULL;
```

6. У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?
```sql
   SELECT
       t.author
   FROM
       (
           SELECT
               author,
               Count(*) books_no
           FROM
               book
           GROUP BY
               author
       ) AS t
   ORDER BY
       books_no DESC
   LIMIT
       1;
```

7. Какие читатели забронировали все книги (не копии), написанные "Марком Твеном"?
```sql
   WITH mark_twain_books AS (
       SELECT
           isbn
       FROM
           book
       WHERE
           author = 'Марк Твен'
   )
   SELECT
       rd.*
   FROM
       (
           SELECT
               reader_nr,
               count(*) AS amount
           FROM
               borrowing
           WHERE
               isbn IN (
                   SELECT
                       *
                   FROM
                       mark_twain_books
               )
           GROUP BY
               reader_nr
       ) t
       JOIN reader rd ON rd.id = t.reader_nr
   WHERE
       t.amount = (
           SELECT
               count(*)
           FROM
               mark_twain_books
       );
```

8. Какие книги имеют более одной копии?
```sql
   SELECT
       bk.*
   FROM
       (
           SELECT
               isbn,
               count(*) AS amount
           FROM
               "copy"
           GROUP BY
               isbn
       ) t
       JOIN book bk ON bk.isbn = t.isbn
   WHERE
       t.amount > 1;
```

9. ТОП 10 самых старых книг
```sql
   SELECT
       *
   FROM
       book
   ORDER BY
       pub_year
   LIMIT
       10;
```

10. Перечислите все категории в категории “Спорт” (с любым уровнем вложености).
```sql
    WITH RECURSIVE search_path (category_name, parent_cat) AS (
        SELECT
            *
        FROM
            category
        WHERE
            parent_cat = 'Спорт'
        UNION
        ALL
        SELECT
            c.*
        FROM
            category c
            INNER JOIN search_path s ON c.parent_cat = s.category_name
    )
    SELECT
        *
    FROM
        search_path;
```

### Задача 2

1. Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.
```sql
   INSERT INTO
       borrowing
   VALUES
       (
           (
               SELECT
                   reader_nr
               FROM
                   reader
               WHERE
                   first_name = 'Василий'
                   AND last_name = 'Петров'
           ),
           '123456',
           4,
           NULL
       );
```

2. Удалить все книги, год публикации которых превышает 2000 год.

```sql
DELETE FROM
    book
WHERE
    extract(
        year
        FROM
            pub_year
    ) > 2020;
```

3.  Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).
```sql
UPDATE
    Borrowing
SET
    return_date = return_date + make_interval(days => 30)
FROM
    book bk
    JOIN bookcat bc ON bc.isbn = bk.isbn
WHERE
     Borrowing.isbn = bk.isbn
    AND Borrowing.return_date >= '2016-01-01'
    AND bc.category_name = 'Базы данных';
```


### Задача 3

1. Возвращает имена и номера студентов, у которых все оценки меньше 4.0 (или нет оценок)

2. Возвращает имена и номера профессоров, сумму кредитов по их лекциям (или 0 если профессор не проводил лекции)

3. Возвращает имена студентов с лучшей оценкой (одной из лучших) по своему курсу и эту оценку.
