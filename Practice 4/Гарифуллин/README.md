# Гарифуллин Руслан Ильфатович Задание 4

## Задача 1

а) 
```sql
SELECT last_name FROM reader
WHERE address = 'Москва';
```
б) 
```sql
SELECT book.author, book.title FROM book 
INNER JOIN (
	reader INNER JOIN borrowing ON (
		reader.id = borrowing.reader_nr AND
		reader.last_name = "Иванов" AND 
		reader.first_name = "Иван"
	)
) ON borrowing.isbn = book.isbn
```
в)
```sql
SELECT DISTINCT a.isbn FROM book_cat AS a 
WHERE a.category_name = "Горы"
EXCEPT
SELECT DISTINCT a.isbn FROM book_cat AS a 
WHERE a.category_name = "Путешествия" 
```
г)
```sql
SELECT reader.last_name, reader.first_name FROM reader
WHERE NOT EXISTS (
  SELECT 1 FROM borrowing
  WHERE borrowing.reader_nr = reader.id AND borrowing.return_date > NOW()
)
```
д)
```sql
SELECT reader.last_name, reader.first_name FROM reader
INNER JOIN borrowing ON reader.id = borrowing.reader_nr
WHERE borrowing.isbn IN (
  SELECT isbn FROM borrowing 
  JOIN reader ON reader.id = borrowing.reader_nr
  WHERE reader.first_name = 'Иван' AND
    reader.last_name = 'Иванов'
) AND 
reader.first_name != 'Иван' AND 
reader.last_name != 'Иванов';
```

## Задача 2
a)
```sql
SELECT DISTINCT train_nr FROM connection AS c
INNER JOIN Station a ON a.name = c.start_station_name
INNER JOIN Station b ON b.name = c.end_station_name
WHERE a.city_name = 'Москва' AND b.city_name = 'Тверь'
```
б)
```sql
SELECT DISTINCT train_nr FROM connection AS c
INNER JOIN Station a ON a.name = c.start_station_name
INNER JOIN Station b ON b.name = c.end_station_name
WHERE a.city_name = 'Москва' AND b.city_name = 'Санкт-Петербург' AND DAY(c.departure) = DAY(c.arrival)
```
