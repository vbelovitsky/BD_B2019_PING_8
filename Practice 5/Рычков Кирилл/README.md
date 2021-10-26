# Практическое задание №4

## Рычков Кирилл, БПИ198

## Задание 1

* Показать все названия книг вместе с именами издателей.
```sql
   SELECT Title, PubName FROM Book;
```
* В какой книге наибольшее количество страниц?
```sql
   SELECT * FROM Book WHERE PagesNum = (SELECT max(PagesNum) FROM Book);
```
* Какие авторы написали более 5 книг?
```sql
   SELECT Author FROM Book GROUP BY Author HAVING count(*) > 5;
```
* В каких книгах более чем в два раза больше страниц, чем среднее количество страниц для всех книг?  
```sql
   SELECT * FROM Book WHERE PagesNum > (SELECT avg(PagesNum) * 2 FROM Book);
```
* Какие категории содержат подкатегории?
```sql
   SELECT c1.CategoryName FROM Category c1
   WHERE exists (SELECT * FROM Category c2 WHERE c2.ParentCat = c1.CategoryName);
```
* У какого автора (предположим, что имена авторов уникальны) написано максимальное количество книг?
```sql
   SELECT Author, count(*) as CountOfBooks FROM Book
   GROUP BY AuthorORDER BY CountOfBooks DESC LIMIT 1;
```
* Какие читатели забронировали все книги (не копии), написанные "Марком Твеном"?
```sql
   SELECT r.ID FROM Reader r
   JOIN Borrowing br ON r.ID = br.ReaderNr
   JOIN Book b ON b.ISBN = br.ISBN
   WHERE b.Author = 'Марк Твен'
   GROUP BY r.ID HAVING COUNT(*) = (SELECT COUNT(*) FROM Book b WHERE b.Author = 'Марк Твен');
```
* Какие книги имеют более одной копии?
```sql
 SELECT b.* FROM Book b INNER JOIN Copy c GROUP BY b.ISBN HAVING count(*) > 1;
 ```
* ТОП 10 самых старых книг
```sql
   SELECT b.* FROM Book b 
   WHERE (SELECT count(*) FROM Book b2 WHERE b2.PubYear <= b.PubYear) < 10 ORDER BY b.PubYear;
 ```
* Перечислите все категории в категории “Спорт” (с любым уровнем вложености).
```sql
   WITH RECURSIVE recur AS(
   SELECT * FROM Category WHERE ParentCat = 'Спорт' UNION ALL
   SELECT category.CategoryName, category.ParentCat FROM Category
   JOIN recur ON ParentCat = recur.CategoryName)
   SELECT DISTINCT CategoryName FROM recur;
```

## Задание 2

* Добавьте запись о бронировании читателем ‘Василеем Петровым’ книги с ISBN 123456 и номером копии 4.
```sql
INSERT INTO Borrowing (ReaderNr, ISBN, CopyNumber, ReturnDate) SELECT ID, "123456", 4, NULL FROM Reader reader
WHERE reader.FirstName = 'Василий' AND reader.LastName = 'Петров';
```
* Удалить все книги, год публикации которых превышает 2000 год.
```sql
DELETE FROM Book where PubYear > 2000;
```
* Измените дату возврата для всех книг категории "Базы данных", начиная с 01.01.2016, чтобы они были в заимствовании на 30 дней дольше (предположим, что в SQL можно добавлять числа к датам).
```sql
UPDATE Borrowing b
SET ReturnDate = ReturnDate + day(30)
WHERE ISBN in (SELECT ISBN FROM BookCat WHERE CategoryName = "Базы данных")
AND ReturnDate >= Date("01.01.2016");
```

## Задание 3
1) Все студенты, которые получили оценки меньше чем 4

Будет выведено -  Имя и ID студентов

2) Сумма кредитов для каждого профессора, если у профессора нет лекции, то сумма кредитов равна 0

Будет выведено -  ID, имя и сумму кредитов у профессоров

3) Максимальный балл >= 4 каждого студента по всем лекциям, которые они поситили

Будет выведено - Имя и оценки студентов



