# Дадугин Егор Артемович БПИ198
# Домашнее задание 5

## Задача 1

### Показать все названия книг вместе с именами издателей.
``` sql
select title, publisher_name
from books
```

### В какой книге наибольшее количество страниц?
``` sql
select *
from books
where page_count = (select MAX(page_count) from books)
```

### Какие авторы написали более 5 книг?
``` sql
select author
from books
group by author
having count(author) >= 5
```

### В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?
``` sql
select *
from books
where page_count >= 2*(select avg(page_count) from books)
```

### Какие категории содержат подкатегории?
``` sql
select distinct parent_name from categories
where parent_name is not null
```

### У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?
``` sql
select author from books
group by author
order by count(author) desc
limit 1
```

### Какие читатели забронировали все книги (не копии), написанные "Марком Твеном"?
``` sql
select reader_number from bookings
join books on bookings.isbn = books.isbn
where books.author = 'Марк Твен'
group by reader_number having count(*) =
                              (select count(*) from books b
                                  where b.author = 'Марк Твен')
```

### Какие книги имеют более одной копии?
``` sql
select isbn from copies
group by isbn
having count(isbn) > 1
```

### ТОП 10 самых старых книг
``` sql
select * from books
order by year
limit 10
```

### Перечислите все категории в категории “Спорт” (с любым уровнем вложености).
``` sql
with recursive cat_r as(
    select name, parent_name from categories
    where name = 'Sport'
    union
    select cat.name, cat.parent_name from categories cat
    join cat_r on cat.parent_name = cat_r.name
) select cat_r.name from cat_r
```

если нулевой уровень вложенности не учитывается, то добавляем в конец

``` sql
except (select cat_e.name from categories cat_e
    where cat_e.name = 'Sport')
```

## Задача 2

### Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.
``` sql
insert into bookings(reader_number, copy_number, isbn, return_date, created_at, updated_at)
values((select number from readers where first_name = 'Василий' and last_name = 'Петров'),
       4, 123456, NOW(), null, null)
```

### Удалить все книги, год публикации которых превышает 2000 год.
``` sql
delete from books
where year > 2000

delete from bookings
where isbn in(select isbn from books
    where year > 200)

delete from book_categories
where isbn in(select isbn from books
    where year > 200)
```
### Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).
``` sql
update bookings
set return_date = return_date + 30
where isbn in
      (select bc.isbn
      from book_categories bc
          where bc.category_name = 'Базы данных')
and return_date > '2016-01-01'
```


## Задача 3

## 1.
Выбираем имена и фамилии студентов, у которых  все оценки(Note) по всем предметам меньше 4

## 2.
Выбираем номера, имена профессоров и сумму кредитов за все их лекции, если лекции они не ведут то сумма кредитов = 0

## 3.
Выбираем имена студентов и их максимальные оценки по всем предметам, большие либо равные 4
