## SQL

### Задание 1
### а)

```sql
SELECT
    LastName
FROM
    Reader
WHERE
    CONTAINS([Address], 'Москва');
```


### б)
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


### в)
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


### г)
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


### д)
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

### а)
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


### б)
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

### в)

а) сделать было бы еще проще, т.к. не нужно проверять, что рейс прямой
(я тут считаю что рейс прямой даже если он является частью многосегментного маршрута)

б) можно группировать по номеру поезда и дню отправки \ прибытия (если считать, что поезд не соверщает более 1 маршрута в день),
найти минимальное время отправки и прибытия и проверить, что города отправки и прибытия совпадают. 
(иначе нужно проверять весь маршрут что довольно неудобно - нужно делать перебор)

### Задание 3

Пусть 

E1 = {A1, A2, ...}
E2 = {B1, B2, ...}

Тогда

Inner Join(A, B, сondition) = project[A_1, ..., A_n, B_1, ..., B_m](select(cartesian(A, B), сondition))

Outer Join(A, B, condition) = union(
    Inner Join(A, B, сondition),
    project[A_1, ..., A_n, NULL, ..., NULL](A - project[A_1, ..., A_n](Inner Join(A, B))),
    project[NULL, ..., NULL, B_1, ..., B_m](B - project[B_1, ..., B_m](Inner Join(A, B)))
)
