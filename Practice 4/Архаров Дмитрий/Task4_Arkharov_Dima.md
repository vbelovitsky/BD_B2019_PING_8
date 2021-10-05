# Задание 4, Архаров Дмитрий

*В MarkDown есть специальное форматирование для SQL- использовал его для выполнения*

## 1.

### A) Какие фамилии читателей в Москве?

Ответ:
``` sql
SELECT lastName 
FROM reader 
WHERE address = 'Москва';
```

### Б) Какие книги (author, title) брал Иван Иванов?

Ответ:
``` sql
SELECT author, title 
FROM book 
INNER JOIN borrowing
ON borrowing.ISBN = book.ISBN
INNER JOIN reader
ON reader.ID = borrowing.readerNr
WHERE reader.firstName = 'Иван' AND reader.lastName = 'Иванов';
```

### В) Какие книги (ISBN) из категории "Горы" не относятся к категории "Путешествия"? Подкатегории не обязательно принимать во внимание!

Ответ:
``` sql
SELECT ISBN 
FROM bookCat
WHERE categoryName = 'Горы'
AND ISBN NOT IN 
(SELECT ISBN from bookCat WHERE categoryName = 'Путешествия')
```

### Г) Какие читатели (LastName, FirstName) вернули копию книгу?

Ответ:
``` sql
SELECT lastName, firstName 
FROM borrowing 
INNER JOIN reader
ON reader.ID = borrowing.readerNr 
WHERE returnDate < NOW();
```

### Д) Какие читатели (LastName, FirstName) брали хотя бы одну книгу (не копию), которую брал также Иван Иванов (не включайте Ивана Иванова в результат)?

Ответ:
``` sql
SELECT lastName, firstName 
FROM reader 
INNER JOIN borrowing
ON reader.ID = borrowing.readerNr
WHERE ISBN in 

(SELECT ISBN FROM borrowing 
JOIN reader 
ON reader.ID = borrowing.readerNr 
WHERE reader.lastName = 'Иванов' AND reader.firstName = 'Иван')

AND reader.lastName != 'Иванов' AND reader.firstName != 'Иван';
```

## 2.

### А) Найдите все прямые рейсы из Москвы в Тверь.

Ответ:
``` sql
SELECT trainNr FROM connection
JOIN station startSt ON 
startSt.name = connection.fromStation 
JOIN station endSt ON 
endSt.name = connection.toStation
WHERE startSt.cityName = 'Москва' AND endSt.cityName = 'Тверь';
```

### Б) Найдите все многосегментные маршруты, имеющие точно однодневный трансфер из Москвы в Санкт-Петербург (первое отправление и прибытие в конечную точку должны быть в одну и ту же дату). Вы можете применить функцию DAY () к атрибутам Departure и Arrival, чтобы определить дату. 

Ответ:
``` sql
SELECT trainNr FROM connection
JOIN station startSt ON 
stratSt.name = connection.fromStation 
JOIN station endSt ON 
endSt.name = connection.toStation
WHERE DAY(arrival) = DAY(departure) AND startSt.cityName = 'Москва' AND endSt.cityName = 'Санкт-Петербург';
```
*Так как у нас транзитивно замкнутые кортежи и Москва-Тверь-СанктПитебург схлопывается в Москва-СанктПетербург, запрос по сути не отличается*


### В) Что изменится в выражениях для А) и Б), если отношение "Connection" не содержит дополнительных кортежей для транзитивного замыкания, поэтому многосегментный маршрут Москва-> Тверь-> Санкт-Петербург содержит только кортежи Москва-> Тверь и Тверь-Санкт-Петербург?

Ответ:
1. Для А) пункта ничего бы не изменилось, так как мы искали только ПРЯМЫЕ рейсы из Москвы в Тверь, а значит многосегментные маршруты нам не подходят.
2. Для Б) пункта в свою очередь запрос изменился бы, пришлось бы сначала селектить те которые идут из Москвы, потом перебирать все следующие и для следующих так же пока не перебрали бы все и не выделили только те, что оканчиваются в Санкт-Петербурге, постепенно дополняя цепь Москва-> X -> Y -> Санкт-Петербург.




## 3.

### Представьте внешнее объединение (outer join) в виде выражения реляционной алгебры с использованием только базовых операций (select, project, cartesian, rename, union, minus)

Ответ:

По сути outer join можно представить через inner join вот так:

outer join = inner join UNION E1 \ E2 (когда мы делаем left outer join, для right - E1 и E2 меняются местами)

тогда остаётся определить только inner join, это можно сделать так:

inner join = PROJECT (SELECT (E1 CARTESIAN E2))

тогда итоговое представление внешнего объединения:

PROJECT (SELECT (E1 CARTESIAN E2)) UNION E1 \ E2