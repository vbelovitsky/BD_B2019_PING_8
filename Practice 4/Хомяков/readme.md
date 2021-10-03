## SQL
### Задача 1.
-- a)
```
SELECT
    LastName
FROM
    Reader
WHERE
    Address = 'Москва';
``` -- б)
```
SELECT
    Book.Title,
    Book.Author
FROM
    Book
    INNER JOIN [Copy] AS cpy ON Book.ISBN = cpy.ISBN
    INNER JOIN Borrowing ON cop.CopyNumber = Borrowing.CopyNumber
    INNER JOIN Reader ON Borrowing.ReaderNr = Reader.ID
WHERE
    Reader.FirstName = 'Иван'
    AND Reader.LastName = 'Иванов'
``` -- в)
-- г)
-- д)