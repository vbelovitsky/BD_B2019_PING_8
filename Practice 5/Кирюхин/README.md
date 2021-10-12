## Кирюхин Андрей, задание 5
### Задача 1

Возьмите реляционную схему для библиотеки сделаную в задании 3.1: 

* Reader( <ins>ID</ins>, LastName, FirstName, Address, BirthDate)  <br>
* Book ( <ins>ISBN</ins>, Title, Author, PagesNum, PubYear, PubName)  <br>
* Publisher ( <ins>PubName</ins>, PubAdress)  <br>
* Category ( <ins>CategoryName</ins>, ParentCat)  <br>
* Copy ( <ins>ISBN, CopyNumber</ins>,, ShelfPosition)  <br>

* Borrowing ( <ins>ReaderNr, ISBN, CopyNumber</ins>, ReturnDate)  <br>
* BookCat ( <ins>ISBN, CategoryName</ins> )  

Напишите SQL-запросы:

* Показать все названия книг вместе с именами издателей.
* В какой книге наибольшее количество страниц?
* Какие авторы написали более 5 книг?
* В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?
* Какие категории содержат подкатегории?
* У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?
* Какие читатели забронировали   все книги (не копии), написанные "Марком Твеном"?
* Какие книги имеют более одной копии? 
* ТОП 10 самых старых книг
* Перечислите все категории в категории “Спорт” (с любым уровнем вложености).

### Задача 2

Напишите SQL-запросы для следующих действий:

* Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.
* Удалить все книги, год публикации которых превышает 2000 год.
* Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).


### Задача 3

Рассмотрим следующую реляционную схему:

* Student( MatrNr, Name, Semester ) 
* Check( MatrNr, LectNr, ProfNr, Note ) 
* Lecture( LectNr, Title, Credit, ProfNr ) 
* Professor( ProfNr, Name, Room )

Опишите на русском языке результаты следующих запросов:

1.
```sql
SELECT s.Name, s.MatrNr FROM Student s 
  WHERE NOT EXISTS ( 
    SELECT * FROM Check c WHERE c.MatrNr = s.MatrNr AND c.Note >= 4.0 ) ; 
```

2.
```sql
( SELECT p.ProfNr, p.Name, sum(lec.Credit) 
FROM Professor p, Lecture lec 
WHERE p.ProfNr = lec.ProfNr
GROUP BY p.ProfNr, p.Name)
UNION
( SELECT p.ProfNr, p.Name, 0 
FROM Professor p
WHERE NOT EXISTS ( 
  SELECT * FROM Lecture lec WHERE lec.ProfNr = p.ProfNr )); 
```

3.
```sql
SELECT s.Name, p.Note
  FROM Student s, Lecture lec, Check c
  WHERE s.MatrNr = c.MatrNr AND lec.LectNr = c.LectNr AND c.Note >= 4 
    AND c.Note >= ALL ( 
      SELECT c1.Note FROM Check c1 WHERE c1.MatrNr = c.MatrNr ) 
```