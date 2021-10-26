## Задание 5

### Задача 1

Возьмите реляционную схему для библиотеки сделаную в задании 3.1:

- Reader( <ins>ID</ins>, LastName, FirstName, Address, BirthDate) <br>
- Book ( <ins>ISBN</ins>, Title, Author, PagesNum, PubYear, PubName) <br>
- Publisher ( <ins>PubName</ins>, PubAdress) <br>
- Category ( <ins>CategoryName</ins>, ParentCat) <br>
- Copy ( <ins>ISBN, CopyNumber</ins>, ShelfPosition) <br>

- Borrowing ( <ins>ReaderNr, ISBN, CopyNumber</ins>, ReturnDate) <br>
- BookCat ( <ins>ISBN, CategoryName</ins> )

Напишите SQL-запросы:

- Показать все названия книг вместе с именами издателей.

```sql
 SELECT Title, PubName FROM Book;
```

- В какой книге наибольшее количество страниц?

```sql
SELECT * FROM Book
   WHERE PagesNum = (SELECT MAX(PagesNum) FROM Book);
```

- Какие авторы написали более 5 книг?

```sql
SELECT Author FROM Book
GROUP BY Author
HAVING COUNT(*)>5;
```

- В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?

```sql
SELECT * FROM Book
   WHERE PagesNum > 2*(SELECT AVG(PagesNum) FROM Book);
```

- Какие категории содержат подкатегории?

```sql
SELECT DISTINCT * FROM Category
   WHERE ParentCat IS NOT NULL;
```

- У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?

```sql
SELECT Author, MAX(BooksCount)
   FROM (SELECT Author,COUNT(Author) BooksCount
      FROM Book
      GROUP BY Author);
```

- Какие читатели забронировали все книги (не копии), написанные "Марком Твеном"?

```sql
SELECT * FROM Reader R
JOIN (SELECT ReaderNr FROM Borrowing
         WHERE ISBN IN (SELECT ISBN FROM Book
            WHERE Author = "Марком Твеном")
            GROUP BY ReaderNr
            HAVING COUNT(*) = (SELECT COUNT(ISBN) FROM Book
               WHERE Author = "Марком Твеном")) RN
ON R.ID = RN.ReaderNr;
```

- Какие книги имеют более одной копии?

```sql
SELECT * FROM Book
   WHERE ISBN IN (SELECT ISBN FROM Copy
GROUP BY ISBN
HAVING COUNT(*)>1);
```

- ТОП 10 самых старых книг

```sql
SELECT TOP 10 * FROM Book
ORDER BY PubYear;
```

- Перечислите все категории в категории “Спорт” (с любым уровнем вложености).

```sql
WITH RECURSIVE myrecursive AS(
   SELECT * FROM Category
      WHERE ParentCat = "Спорт"
   UNION ALL
   SELECT category.CategoryName, category.ParentCat FROM Category
   JOIN myrecursive
   ON ParentCat = myrecursive.CategoryName)
SELECT DISTINCT CategoryName FROM myrecursive;
```

### Задача 2

Напишите SQL-запросы для следующих действий:

- Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.

```sql
INSERT INTO Borrowing
(ReaderNr, ISBN, CopyNumber, ReturnDate)
VALUES
(SELECT ID, "123456", 4, NULL FROM Reader
WHERE LastName = 'Петров'
AND FirstName = 'Василий');
```

- Удалить все книги, год публикации которых превышает 2000 год.

```sql
DELETE FROM Book where PubYear > 2000;
```

- Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).

```sql
UPDATE Borrowing b
SET ReturnDate = ReturnDate + day(30)
WHERE ISBN IN
(SELECT ISBN FROM BookCat
WHERE ReturnDate >= Date("01.01.2016")
AND CategoryName = "Базы данных");
```

### Задача 3

Рассмотрим следующую реляционную схему:

- Student( MatrNr, Name, Semester )
- Check( MatrNr, LectNr, ProfNr, Note )
- Lecture( LectNr, Title, Credit, ProfNr )
- Professor( ProfNr, Name, Room )

Опишите на русском языке результаты следующих запросов:

1.

```sql
SELECT s.Name, s.MatrNr FROM Student s
  WHERE NOT EXISTS (
    SELECT * FROM Check c WHERE c.MatrNr = s.MatrNr AND c.Note >= 4.0 ) ;
```

Имена и идентификаторы студентов которые получили по какой то проверки, на какой то лекции, у кого то профисор оценка меньше 4.

---

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

Идентификатор и имя профисора и сумма кредитов у его лекции, если у профисора нету лекции, то сумма кредитов это 0.

---

3.

```sql
SELECT s.Name, p.Note
  FROM Student s, Lecture lec, Check c
  WHERE s.MatrNr = c.MatrNr AND lec.LectNr = c.LectNr AND c.Note >= 4
    AND c.Note >= ALL (
      SELECT c1.Note FROM Check c1 WHERE c1.MatrNr = c.MatrNr )
```

Имена студентов и их Максимальная оценка по всем лекциям, если у студента нет оценки >= 4, то он не будет вывден.
