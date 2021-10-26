# Задание 5

## 1

### Показать все названия книг вместе с именами издателей.

```sql
SELECT Title, PubName FROM Book;
```

### В какой книге наибольшее количество страниц?

```sql
SELECT Title FROM Book 
  WHERE PagesNum = MAX(PagesNum); 
```

### Какие авторы написали более 5 книг?

```sql
SELECT Author 
FROM Book 
GROUP BY Author 
HAVING count(*) > 5;
```

### В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?

```sql
SELECT * FROM Book 
  WHERE PagesNum > (2 * AVG(PagesNum)); 
```

### Какие категории содержат подкатегории?

```sql
SELECT DISTINCT c1.CategoryName FROM Category c1
    WHERE EXISTS (SELECT * FROM Category c2 
        WHERE c1.CategoryName = c2.ParentCat);
```

### У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?

```sql
SELECT Author, count(*) AS BooksCount 
    FROM Book GROUP BY Author ORDER BY
        BooksCount DESC LIMIT 1;
```

### Какие читатели забронировали   все книги (не копии), написанные "Марком Твеном"?

```sql
SELECT r.ID FROM Reader r 
    JOIN Borrowing bor ON r.ID = bor.ReaderNr 
        JOIN Book b ON bor.ISBN = b.ISBN
WHERE b.Author = 'Марк Твен'
    GROUP BY r.ID HAVING COUNT(*) = (
	    SELECT COUNT(*) FROM Book b 
            WHERE b.Author = 'Марк Твен');
```

### Какие книги имеют более одной копии?

```sql
 SELECT ISBN, COUNT(*) as cnt FROM Copy 
  GROUP BY ISBN 
  HAVING cnt > 1;
```

### ТОП 10 самых старых книг

```sql
SELECT * FROM Book 
  ORDER BY PubYear 
  LIMIT 10;
```

### Перечислите все категории в категории “Спорт” (с любым уровнем вложености).

```sql
WITH RECURSIVE current_category AS
(
  SELECT * FROM Category
    WHERE ParentCat = 'Спорт'
        UNION ALL
  SELECT subcategory.* FROM Category
    JOIN current_category
      ON ParentCat = current_category.CategoryName
)
SELECT DISTINCT CategoryName FROM current_category;
```

## 2

### Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.

```sql
INSERT INTO Borrowing (ReaderNr, ISBN, CopyNumber, ReturnDate)
  SELECT ID, "123456", 4, Null FROM Reader rdr
  WHERE rdr.FirstName = "Василий" AND rdr.LastName = "Петров";
```

### Удалить все книги, год публикации которых превышает 2000 год.

```sql
DELETE FROM Copy
WHERE ISBN IN (SELECT ISBN FROM Book WHERE PubYear > 2000);
                    
DELETE FROM Borrowing
WHERE ISBN IN (SELECT ISBN FROM Book WHERE PubYear > 2000);
                    
DELETE FROM BookCat
WHERE ISBN IN (SELECT ISBN FROM Book WHERE PubYear > 2000);
DELETE FROM Book WHERE PubYear > 2000;
```

### Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).

```sql
UPDATE Borrowing
SET ReturnDate = ReturnDate + day(30)
WHERE ReturnDate >= Date("01.01.2016")
    AND ISBN in 
        (SELECT ISBN FROM BookCat WHERE CategoryName = "Базы данных");
```


## 3

### 1
```sql
SELECT s.Name, s.MatrNr FROM Student s 
  WHERE NOT EXISTS ( 
    SELECT * FROM Check c WHERE c.MatrNr = s.MatrNr AND c.Note >= 4.0 ) ; 
```

Выбрать имена и номера студентов, у которых все оценки меньше 4.0

### 2
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

Выбрать имена, номера и суммы всех кредитов для каждого профессора (если лекций нет, то кредитов 0)

### 3
```sql
SELECT s.Name, p.Note
  FROM Student s, Lecture lec, Check c
  WHERE s.MatrNr = c.MatrNr AND lec.LectNr = c.LectNr AND c.Note >= 4 
    AND c.Note >= ALL ( 
      SELECT c1.Note FROM Check c1 WHERE c1.MatrNr = c.MatrNr ) 
```

Выбрать имена и оценки студентов, у которых максимальный балл больше или равен 4
