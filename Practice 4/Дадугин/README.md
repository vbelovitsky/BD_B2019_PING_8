# Дадугин Егор Артемович БПИ198
# Домашнее задание 4

## Задача 1

### А)
``` sql
select LastName
from Reader
where contains(Address, 'Москва')
```

### Б)
``` sql
select b.Author, b.Title
from Reader r
join Borrowing bor on r.Id = bor.ReaderNr
join Book b on bor.ISBN = b.ISBN
where r.FirstName = 'Иван' and r.LastName = 'Иванов' 
```

### В
``` sql
select ISBN
from BookCat bc1
where bc1.CategoryName = 'Горы' and ISBN not in (
select ISBN 
from BookCat bc2
where bc2.CategoryName = 'Путешествия' )
```

### Г)
``` sql
select r.LastName, r.FirstName
from Reader r
join Borrowing bor on r.ID = bor.ReaderNr
where bor.ReturnDate < NOW()
```

### Д)
``` sql
select LastName, FirstName
from Reader
join Borrowing on ID = ReaderNr
where ISBN in (
select ISBN
from Reader r
join Borrowing b on r.ID = b.ReaderNr
where r.FirstName =  'Иван' and r.LastName = 'Иванов'
) and FirstName != 'Иван' and LastName != 'Иванов'
```

## Задача 2

### А)
``` sql
select distinct TrainNr
from Connection
join Station s1 on s1.Name = StartStationName
join Station s2 on s2.Name = EndStationName
where s1.CityName = 'Москва'  and s2.CityName = 'Тверь'
```

### Б)
``` sql
select distinct TrainNr
from Connection
join Station s1 on s1.Name = StartStationName
join Station s2 on s2.Name = EndStationName
where s1.CityName = 'Москва' and s2.CityName = 'Санкт-Петербург' and Day(Departure) = Day(Arrival)
```
### В)
Для пункта А ничего не изменится. Для пункта Б придется объединять таблицы по конечной и начальной станции, что бы схлопнуть путь со всеми остановками и найти поезд, у которого конечная станция Санкт-Петербург


## Задача 3

Сначала выразим join:

join(A, B)= select(cartesian(A, B))

Теперь с помощью выраженного join выразим outer join:

outer_join(A, B) = union(join(A, B), (project(minus(A, project(join(A, B))))), (project(minus(B, project(join(A, B))))))
