## Задание 5

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

```sql

SELECT Title, PubNAme FROM Book;

```

* В какой книге наибольшее количество страниц?
```sql

SELECT MAX(PagesNum) FROM Book;

```
* Какие авторы написали более 5 книг?

```sql

SELECT Author FROM Book GROUP BY Author HAVING COUNT(*) > 5;

```

* В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?

```sql

SELECT * FROM Book WHERE PagesNum > (SELECT AVG(PagesNum) FROM Book);

```

* Какие категории содержат подкатегории?

```sql

SELECT * FROM Category WHERE CategoryName NOT IN 
(SELECT ParentCat from Category);

```

* У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?

```sql

SELECT * FROM 
(SELECT Author, COUNT(*) as NumBooks FROM Book GROUP BY Author) an
WHERE an.NumBooks = (SELECT MAX(NumBooks) FROM 
(SELECT Author, COUNT(*) as NumBooks FROM Book GROUP BY Author) ans);

```

* Какие читатели забронировали   все книги (не копии), написанные "Марком Твеном"?

```sql
SELECT ReaderNr
(SELECT ReaderNr, COUNT(*) AS NumBook FROM
(SELECT * FROM 
(SELECT DISTINCT ReaderNr, ISBN from Borrowing) BorrowingUnique
JOIN Reader
ON BorrowingUnique.ReaderNr = Reader.ID
JOIN (SELECT * FROM Book WHERE Book.Author = 'Марк Твен') BookTwen 
ON BorrowingUnique.ISBN = BookTwen.ISBN) j
GROUP BY ReaderNr) ReaderToNum
WHERE ReaderToNum.NumBook = (SELECT COUNT(*) 
FROM Book WHERE Book.Author = 'Марк Твен');

```

* Какие книги имеют более одной копии? 

```sql
    SELECT * FROM Copy GROUP BY ISBN HAVING COUNT(*) > 1
```

* ТОП 10 самых старых книг

```sql
    SELECT * Book ORDER BY PubYear DESC LIMIT 10;
```

* Перечислите все категории в категории “Спорт” (с любым уровнем вложености).

### Задача 2

Напишите SQL-запросы для следующих действий:

* Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.

```sql
    INSERT INTO Borrowing 
    (ReaderNr, ISBN, CopyNumber) 
    values 
    (SELECT ID FROM Reader 
    WHERE LastName = 'Петров' AND FirstName = 'Василий' LIMIT 10, 
    123456, 4);
```

* Удалить все книги, год публикации которых превышает 2000 год.

```sql

    DELETE FROM Book WHERE PubYear > 2000;

```

* Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).

Надо исправить!!!
```sql

    UPDATE Borriwng
    SET ReturnDate = ReturnDate + 30
    WHERE ReturnDate > '01.01.2016' 
    AND EXISTS (SELECT * FROM Book WHERE Book.ISBN = Borrowing.ISBN AND
    Book.CategoryName = 'Базы данных');

```


### Задача 3

Рассмотрим следующую реляционную схему:

* Student( MatrNr, Name, Semester ) 
* Check( MatrNr, LectNr, ProfNr, Note ) 
* Lecture( LectNr, Title, Credit, ProfNr ) 
* Professor( ProfNr, Name, Room )

Опишите на русском языке результаты следующих запросов:

1.

Выбрать студентов, корорые не получали оценки выше 4.0

```sql
SELECT s.Name, s.MatrNr FROM Student s 
  WHERE NOT EXISTS ( 
    SELECT * FROM Check c WHERE c.MatrNr = s.MatrNr AND c.Note >= 4.0 ) ; 
```

2.

Каждому профессору поставить в соответствии сумму кредитов по лекциям, по корорым он преподает.

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

В каждой проверке вывести имя студетов и оценку, которая не менее 4 и явяется макисальной по этой проверке.

```sql
SELECT s.Name, p.Note
  FROM Student s, Lecture lec, Check c
  WHERE s.MatrNr = c.MatrNr AND lec.LectNr = c.LectNr AND c.Note >= 4 
    AND c.Note >= ALL ( 
      SELECT c1.Note FROM Check c1 WHERE c1.MatrNr = c.MatrNr ) 
```