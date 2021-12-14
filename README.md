# Курс "Базы данных"

## Сервисы для проектирования

* [DrawSQL](https://drawsql.app/)
* [DbDesigner](https://www.dbdesigner.net)
* [DbDesign](https://dbdesign.online/)
* [PonyORM Editor](https://editor.ponyorm.com/)

## Многофункциональные сервисы

* [Flowchart Maker & Online Diagram Software](rt.draw.io)
* [Miro](https://miro.com/)
* [Lucidchart](https://lucidchart.com/)

## Программы для проектирования

* [StarUML](https://staruml.io)
* [Visio](https://www.microsoft.com/ru-ru/microsoft-365/visio/flowchart-software)

## Программа для работа с БД

* [DataGrip](https://www.jetbrains.com/datagrip/)

## Задание 1

Вам поручено разработать онлайн-аукцион. Он позволяет продавцам продавать свои товары с помощью аукциона. Покупатели делают ставки. Выигрывает последняя самая высокая ставка. После закрытия аукциона победитель оплачивает товар с помощью кредитной карты. Продавец отвечает за доставку товара покупателю.

* Предложите список функциональных требований для проекта.
* Определите роли пользователей и действия для каждой роли.
* Определите объекты, о которых будут храниться данные.
* Определите связи между объектами для хранения данных.
* Нарисуйте схему объектной модели (используя любые обозначения, которые вам удобны).

### Соглашение о приеме работ

* В папке с названием задания, создается папка с фамилией студента, в которой он работает
* Текстовы файлы оформляются в формате .md . Если нет возможности - pdf, word, etc.
* Изображения лежат рядом, в .md указываем ссылку
* Срок сдачи - до 00:00 11.09.2021. 
* Комиты можно писать на русском языке

## Задание 2

**Задача 1.** Нарисуйте E/R диаграмму для библиотечной системы на основе следующих требований:
 * В библиотеки храняться экземпляры книг. Каждая копия (экземпляр) имеет свой уникальный номер копии, положение на полке и может быть однозначно идентифицирована с помощью номера копии вместе с ISBN. 
 * Каждая книга имеет уникальный номер ISBN, год, название, автора и количество страниц. 
 * Книги издаются издательствами. У издателя есть имя и адрес. 
 * В библиотечной системе книгам присвоена одна категория или несколько. Категории образуют иерархию, поэтому категория может быть просто другой подчиненой категорией (подкатегория). Категория имеет только имя и никаких других свойств. 
 * Каждому читателю присвается уникальный номер. У читателя есть Фамилия, Имя, адрес и день рождения. Читатель может взять один или несколько экземпляров книг. При взятии книги записывается запланированая дата возврата.  

 **Задача 2.** Смоделируйте следующие отношения в E/R.
  *  Квартира расположена в доме на улице в городе в стране
  *  Две команды играют друг против друга в футбол под руководством арбитра
  *  У каждого человека (мужчины и женщины) есть отец и мать

 **Задача 3.** Смоделируйте E/R-модель в виде E/R диаграммы

 ### Соглашение о приеме работ

* В папке с названием задания, создается папка с фамилией студента, в которой он работает
* Текстовы файлы оформляются в формате .md
* Изображения лежат рядом, в .md указываем ссылку
* Срок сдачи - до 23:59 18.09.2021. 
* Комиты можно писать на русском языке

 ## Задание 3

1. Почему любое отношение в реляционной схеме имеет по крайней мере один ключ?
2. Переведите все диаграммы ER из предыдущей домашней работы в реляционные схемы.
3. Переведите приведенные диаграммы ER в реляционные схемы.
    3.1. https://imgur.com/w2iDI1o
    3.2. https://imgur.com/oFBM5pp

 ### Соглашение о приеме работ

* В папке с названием задания, создается папка с фамилией студента, в которой он работает
* Текстовы файлы оформляются в формате .md
* Изображения лежат рядом, в .md указываем ссылку
* Срок сдачи - до 23:59 25.09.2021. 
* Комиты можно писать на русском языке

## Задание 4

### Задача 1
Возьмите библиотечную систему, схему которой сделали на предыдущем задании

Reader( ID, LastName, FirstName, Address, BirthDate) 
Book ( ISBN, Title, Author, PagesNum, PubYear, PubName) 
Publisher ( PubName, PubAdress) 
Category ( CategoryName, ParentCat) 
Copy ( ISBN, CopyNumber, ShelfPosition) 

Borrowing ( ReaderNr, ISBN, CopyNumber, ReturnDate) 
BookCat ( ISBN, CategoryName )  

Напишите SQL-запросы (или выражения реляционной алгебры) для следующих вопросов:
а) Какие фамилии читателей в Москве?
б) Какие книги (author, title) брал Иван Иванов?
в) Какие книги (ISBN) из категории "Горы" не относятся к категории "Путешествия"? Подкатегории не обязательно принимать во внимание!
г) Какие читатели (LastName, FirstName) вернули копию книгу?
д) Какие читатели (LastName, FirstName) брали хотя бы одну книгу (не копию), которую брал также Иван Иванов (не включайте Ивана Иванова в результат)?

### Задача 2

Возьмите схему для Поездов, которую выполняли в предыдущем задании. 

City ( Name, Region ) 
Station ( Name, #Tracks, CityName, Region ) 
Train ( TrainNr, Length, StartStationName, EndStationName ) 
 ( FromStation, ToStation, TrainNr, Departure, Arrival) 

Предположим, что отношение "Connection" уже содержит транзитивное замыкание. Когда поезд 101 отправляется из Москвы в Санкт-Петербург через Тверь, содержит кортежи для связи Москва->Тверь, Тверь-Санкт-Петербург и Москва->Санкт-Петербург. Сформулируйте следующие запросы в реляционной алгебре:

а) Найдите все прямые рейсы из Москвы в Тверь.
б) Найдите все многосегментные маршруты, имеющие точно однодневный трансфер из Москвы в Санкт-Петербург (первое отправление и прибытие в конечную точку должны быть в одну и ту же дату). Вы можете применить функцию DAY () к атрибутам Departure и Arrival, чтобы определить дату.
в) Что изменится в выражениях для а) и б), если отношение "Connection" не содержит дополнительных кортежей для транзитивного замыкания, поэтому многосегментный маршрут Москва-> Тверь-> Санкт-Петербург содержит только кортежи Москва-> Тверь и Тверь-Санкт-Петербург?

### Задача 3
Представьте внешнее объединение (outer join ) в виде выражения реляционной алгебры с использованием только базовых операций (select, project, cartesian, rename, union, minus)

### Соглашение о приеме работ

* В папке с названием задания, создается папка с фамилией студента, в которой он работает
* Текстовы файлы оформляются в формате .md
* Изображения лежат рядом, в .md указываем ссылку
* Срок сдачи - до 18:00 03.09.2021. 
* Комиты можно писать на русском языке

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

## Модуль 2

## Задание 6

Установить PostrgeSQL локально. Создать базу данных. 

Пример датасета для Oracle (для примера): http://pastebin.com/dEqPSAk3 

### Описание дата сета
У спортсмена есть олимпийское удостоверение, имя, пол, страна и дата рождения.
У каждой олимпиады есть год, сезон (летний или зимний), страна, где она проходила, и город.
Каждый спортсмен в базе данных участвует по крайней мере в одной олимпиаде. Спортсмен может участвовать в нескольких соревнованиях на одной Олимпиаде и фактически может участвовать более чем в одной Олимпиаде. Например, Майкл Армстронг участвовал в соревнованиях по плаванию, дайвингу и водному поло. Ян Торп участвовал в летних Олимпийских играх 2000 года в Сиднее и завоевал 3 золотые и 2 серебряные медали. В Афинах 2004 года он завоевал две золотые, одну серебряную и одну бронзовую медали.
У события есть название вида спорта, название события, место проведения, а также запланированное время и дата, какие спортсмены должны были участвовать в соревнованиях, в каких соревнованиях и как они разместились, и кто был победителем (победителями) события (например, какой медалью они были награждены). Вы можете предположить, что нулевые значения используются для победителей и мест размещения до тех пор, пока событие не будет проведено.
Соревнования на Олимпийских играх могут быть как индивидуальными, так и командными. Если это командное мероприятие, мы хотим знать, кто был членом каждой команды.<br/>

### Задание 

Напишие SQL запросы

* Для Олимпийских игр 2004 года сгенерируйте список (год рождения, количество игроков, количество золотых медалей), содержащий годы, в которые родились игроки, количество игроков, родившихся в каждый из этих лет, которые выиграли по крайней мере одну золотую медаль, и количество золотых медалей, завоеванных игроками, родившимися в этом году.
* Перечислите все индивидуальные (не групповые) соревнования, в которых была ничья в счете, и два или более игрока выиграли золотую медаль.
* Найдите всех игроков, которые выиграли хотя бы одну медаль (GOLD, SILVER и
BRONZE) на одной Олимпиаде. (player-name, olympic-id).
* В какой стране был наибольший процент игроков (из перечисленных в наборе данных), чьи имена начинались с гласной?
* Для Олимпийских игр 2000 года найдите 5 стран с минимальным соотношением количества групповых медалей к численности населения.

## Задание 7

С помощью любого знакомого вам фрейморка требуется сделать автоматическое наполнение БД с помощью фейковых данных, для БД которая используется в прошлом задании. Для каждой из таблиц возможно назначение рандомного количество элементов. В качестве результата в папку со своей фамилией загружаете исходный код, в файле Readme указываете описание запуска процесса наполнение и описание изменения количество экземпляров. 

Пример фейкера, как источника данных, для PHP - https://github.com/fzaninotto/Faker

## Задание 8

### Задача 1

Возьмите схему библиотечной системы из задания 2, и создайте на ее основе таблицы, лучше на основе миграций. 

### Задача 2

- Создайте модели
- Напишите или используйте готовый генератор данных для всех таблиц
- Создаете CRUD (Create, Read, Delete, Update) операции в виде REST API
  - Книг и экземпляров
  - Бронирования (В списке с бронированием нужно выводить данные по читателю и по книге)

Для Java можете использовать руководство -  https://spring.io/guides/gs/accessing-data-jpa/. 


## Задание 9

### Задача 1

DBMS выполняет следующий запрос:

```sql
SELECT * FROM emp WHERE salary = 200; 
```

Предположим, у вас есть индекс B-дерева "emp.salary". Кроме того, предположим, что таблица содержит 100 миллионов кортежей, хранящихся на 1 миллионе страниц на диске (т.е. в среднем 100 кортежей на страницу). Далее предположим, что у вас имеется бесконечное количество доступной основной памяти и что все B-дерево изначально находится в основной памяти, в то время как ни одна страница таблицы не находится в основной памяти. Предположим, что произвольный доступ к диску занимает 28 миллисекунд, в то время как последовательный доступ к диску занимает всего 0,28 миллисекунды.

* а) Определите стоимость (как общее время доступа к жесткому диску) ответа на этот запрос при использовании B-дерева для ответа на запрос. Для этого укажите формулу, которая получает в качестве параметра количество кортежей с содержимым 200.

* б) Для каких значений параметров (количество сотрудников с содержанием 200) использование B-дерева было бы выгоднее по сравнению с полным сканированием таблицы? Насколько лучше было бы сканирование полной таблицы, чем сканирование индекса, если бы все 100 миллионов сотрудников имели зарплату 200?


### Задача 2

Для каждого из следующих операторов укажите максимальный размер буфера основной памяти, который он может использовать, и минимально возможный размер буфера основной памяти, оба в зависимости от размера ввода.

* Nested-loop Join
*	(Grace) Hash Join
*	Sort Merge Join
*	Table Scan 
*	Index Scan  (Доступ к таблице с помощью B-дерева. Внимание: вам нужны два буфера: один для B-дерева и один для блоков таблицы)
Какую стратегию замены вы бы использовали?

### Задача 3

Предположим, у нас есть следующая реляционная схема:

```
Customer(Cid, Name) 
Order(Oid, Customer, Volume) 
```

Существует 1000 кортежей клиентов и 100000 кортежей заказов. Размер каждого кортежа составляет 100 байт.

Кроме того, предположим, что у нас есть следующий запрос, который запрашивает общий объем заказов Клиента(Customer) по имени “Alex”.:

```sql
SELECT sum(o.Volume)FROM Customer c, Order o
WHERE c.Cid = o.Customer AND c.Name = “Alex”; 
```

* а) Переведите этот SQL-запрос в выражение реляционной алгебры. (Подсказка: вы можете использовать функцию sum.)

* б) Объясните, как вы будете реализовывать каждый оператор, т.е. ипользуйте ключевое слово, которое определяет, какой алгоритм вы будете использовать для реализации оператора. (Например, 2-фазная внешняя сортировка (2-Phase External Sort))

* в) Для каждого оператора укажите объем основной памяти, который вы бы выделили. Почему? Сколько памяти вам нужно для обработки всего запроса?

### Задача 4

Предположим, у нас есть следующий запрос:

```sql
SELECT *FROM R, S, T
WHERE R.rid = S.sid AND S.sid = T.tid AND T.tid = R.rid 
```

* а) Укажите 3 разных плана запроса для этого запроса (join method).
* б) Для каждого данного плана в предыдущей части укажите размер каждой таблицы, чтобы каждый план был оптимальным.
* в) Возьмите один из планов из предыдущей части и предположите, что ни одна из таблиц не помещается в основную память, т.е. память составляет не более половины размера самой маленькой таблицы. В этих условиях, как вы распределяете буферы? Какова будет ваша политика замены страниц?

## Задание 10

### Задача 1

Отношение (A, B, C, D, E, G) имеет следующие функциональные зависимости:

* AB → C
* C → A 
* BC → D 
* ACD → B 
* D → EG 
* BE → C 
* CG → BD 
* CE → AG 

Постройте закрытие атрибута (Attribute Closure )(BD)+

### Задача 2

Посмотрите на отношения: Order (ProductNo, ProductName, CustomerNo, CustomerName,OrderDate,UnitPrice, Quantity, SubTotal, Tax, Total)

Ставка налога зависит от Товара (например, 20 % для книг или 30 % для предметов роскоши).
В день допускается только один заказ на продукт и клиента (несколько заказов объединяются).

* А) Определить нетривиальные функциональные зависимости в отношении
* Б) Каковы ключи-кандидаты?

### Задача 3

Рассмотрим соотношение R(A, B, C, D) со следующими функциональными зависимостями:
F = {A→D, AB→ C, AC→ B}
* А) *Каковы все ключи-кандидаты?
* Б) Преобразуйте R в 3NF, используя алгоритм синтеза.