## SQL

### Задание 1
- а

```sql
SELECT
    LastName
FROM
    Reader
WHERE
    CONTAINS([Address], 'Москва');
```


- б
```sql
SELECT
    bk.Title,
    bk.Author
FROM
    Book AS bk
    INNER JOIN [Copy] AS cpy ON bk.ISBN = cpy.ISBN
    INNER JOIN Borrowing AS br ON cpy.CopyNumber = br.CopyNumber
    INNER JOIN Reader AS rd ON br.ReaderNr = rd.ID
WHERE
    rd.FirstName = 'Иван'
    AND rd.LastName = 'Иванов';
 ```


- в
```sql
SELECT
    DISTINCT bc.ISBN
FROM
    BookCat AS bc
WHERE
    bc.CategoryName = 'Горы'
EXCEPT
SELECT
    DISTINCT bc.ISBN
FROM
    BookCat AS bc
WHERE
    bc.CategoryName = 'Путешествия';
```


- г
```sql
SELECT
    DISTINCT rd.FirstName,
    rd.LastName
FROM
    Reader AS rd
    INNER JOIN Borrowing AS br ON br.ReaderNr = rd.ID
WHERE
    br.ReturnDate < GETDATE();
```


- д
```sql
SELECT
    DISTINCT rd.FirstName,
    rd.LastName
FROM
    Reader AS rd
    INNER JOIN Borrowing AS br ON br.ReaderNr = rd.ID
    INNER JOIN [Copy] AS cpy ON cpy.CopyNumber = br.CopyNumber
WHERE
    cpy.ISBN IN (
        SELECT
            DISTINCT cpy.ISBN
        FROM
            [Copy] AS cpy
            INNER JOIN Borrowing AS br ON cpy.CopyNumber = br.CopyNumber
            INNER JOIN Reader AS rd ON br.ReaderNr = rd.ID
        WHERE
            Reader.FirstName = 'Иван'
            AND Reader.LastName = 'Иванов'
    )
    AND (
        rd.FirstName <> 'Иван'
        OR rd.FirstName <> 'Иванов'
    );
```

### Задание 2

- а
```sql
SELECT
    *
FROM
    Connection AS c1
    INNER JOIN station AS s1 ON s1.Name = с.FromStation
    INNER JOIN station AS s2 ON s2.Name = с.ToStation
WHERE
    s1.CityName = 'Москва'
    AND s2.CityName = 'Тверь' 
    -- если есть записи Москва->Спб, Спб->Тверь то и Москва->Тверь
    -- и тогда время отправки из Москва->Спб = времени отправки Москва->Тверь
    -- иначе рейс прямой
    AND NOT EXISTS (
        SELECT
            1
        FROM
            Connection AS c2
        WHERE
            c1.Departure = c2.Departure
            AND c1.TrainNr = c2.TrainNr
    );
```


- б
```sql
SELECT
    *
FROM
    Connection AS c1
    INNER JOIN station AS s1 ON s1.Name = с.FromStation
    INNER JOIN station AS s2 ON s2.Name = с.ToStation
WHERE
    s1.CityName = 'Москва'
    AND s2.CityName = 'Санкт-Петербург'
    AND DAY(c1.Arrival) = DAY(c2.Departure)
    AND EXISTS (
        SELECT
            1
        FROM
            Connection AS c2
        WHERE
            c1.Departure = c2.Departure
            AND c1.TrainNr = c2.TrainNr
    );
```

### Задание 3


