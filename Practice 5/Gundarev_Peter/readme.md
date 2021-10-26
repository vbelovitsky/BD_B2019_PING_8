# Задача 1

Reader( ID, LastName, FirstName, Address, BirthDate)
Book ( ISBN, Title, Author, PagesNum, PubYear, PubName)
Publisher ( PubName, PubAdress)
Category ( CategoryName, ParentCat)
Copy ( ISBN, CopyNumber,, ShelfPosition)
Borrowing ( ReaderNr, ISBN, CopyNumber, ReturnDate)
BookCat ( ISBN, CategoryName )

a) Показать все названия книг вместе с именами издателей.\
b) В какой книге наибольшее количество страниц?\
c) Какие авторы написали более 5 книг?\
d) В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?\
e) Какие категории содержат подкатегории?\
f) У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?\
g) Какие читатели забронировали все книги (не копии), написанные "Марком Твеном"?\
h) Какие книги имеют более одной копии?\
i) ТОП 10 самых старых книг\
j) Перечислите все категории в категории “Спорт” (с любым уровнем вложености).\

a)
```sql
select 
	Title, Author
from 
	Book;
```
	
b)
```sql
select 
	ISBN
from 
	Book
order 
	by PagesNum DESC
limit 1
```

c)
```sql
select
	PubName, count(ISBN) as cnt
from
	book
group
	by PubName
where
	cnt > 5
```

d)
```sql
select
	ISBN
from 
	Book
where
	PagesNum > 2 * AVG(PagesNum)
```

e)
```sql
select
	distinct ParentCat
from 
	Category
where
	ParentCat is not null
	
f)
select
	PubName
	, count(ISBN) as cnt
from
	Book
order
	by cnt desc
limit 1
```

g)
```sql
select
	ISBN, Title
from
	Book join Borrowing borr on
		Book.ISBN = borr.ISBN
where
	PubName = "Марк Твен"
	and borr.ReturnDate > NOW()
```

h)
```sql
select
	ISBN
	, count(CopyNumber) as cnt
from
	Copy
where
	cnt > 1
```

i)
```sql
select
	ISBN
	, PubYear
from
	Book
order
	by PubYear ASC
limit 10
```

j)
```sql
select
	*
from 
	Category
where
	Category = "Спорт"
	or ParentCat = "Спорт"
```

# Задача 2

Напишите SQL-запросы для следующих действий:

* Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.
* Удалить все книги, год публикации которых превышает 2000 год.
* Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).

1)
```sql
with PetrovID as (
select 
	ID
from
	Reader
where
	LastName = "Петров"
	and FirstName = "Василий")
insert
	into Borrowing
values
	PetrovID
	, 123456
	, 4
	, NOW() + 30
```

2)
```sql
delete
from Book join Copy
	on Book.ISBN = Copy.ISBN
where
	Book.PubYear > 2000
```

3)
```sql
update
	Borrowing b join BookCat cat
		on b.ISBN = cat.ISBN
set
	ReturnDate = ReturnDate + 30
where
	ReturnDate > "01.01.2016"
	and cat.Category = "Базы Данных"
```

# Задача 3

1)
Выбрать имена и номера студентов, номера которых есть в Check и которые получили оценку < 4

2)
Каждому профессору ставит в соответствие сумму lec.Credit (кредиты лекций?), и если номера профессора нет в lec, то ему сопоставляется 0

3)
Запрос выводит имена студентов и их одну наибольшую оценку или несколько, если они одинаковые (учитываются только оценки больше 4 по существующим лекциям)


