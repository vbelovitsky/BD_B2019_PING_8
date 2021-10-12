## Задание 5

### Задача 1

1. ```sql
   SELECT
       TITLE,
       AUTHOR
   FROM
       BOOK;
   ```

2. ```sql
   SELECT
       *
   FROM
       book
   ORDER BY
       book.pages_num DESC
   LIMIT
       1;
   ```

3. ```sql
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
       t.books_no > 1;
   ```

4. ```sql
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

````

5. ```sql
   SELECT
       category_name
   FROM
       category
   WHERE
       parent_cat IS NOT NULL;
````

6. ```sql
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

````

7. ```sql
   WITH mk_books AS (
       SELECT
           isbn
       FROM
           book
       WHERE
           author = 'Mark Twain'
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
                       mk_books
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
               mk_books
       );
````

8. ```sql
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

````

9. ```sql
   SELECT
       *
   FROM
       book
   ORDER BY
       pub_Year
   LIMIT
       10;
````

10. ```sql
    WITH RECURSIVE search_path (category_name, parent_cat) AS (
        SELECT
            *
        FROM
            category
        WHERE
            parent_cat = 'Sport'
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

````

### Задача 2

1. ```sql
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
````

2.

````sql
DELETE FROM
    book
WHERE
    extract(
        year
        FROM
            pub_year
    ) > 2020;
   ```

3.  ```sql
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

````
