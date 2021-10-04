## hw4, Bakytbek uulu Nurzhigit

### task 1

a)

``` sql

select LastName from Reader where Address like '%Moscow%'

```

b) 

``` sql

select bk.Author, bk.Title from Reader r, Borrowing b, Book bk where r.FirstName = 'Ivan' and r.LastName = 'Ivanov' and r.ID = b.ReaderNr and bk.ISBN = b.ISBN and b.ReturnDate is not null

```

c)

``` sql

select ISBN from (select ISBN from BookCat where CategoryName = 'Горы') minus (select ISBN from BookCat where CategoryName = 'Путешествия')

```

d)

``` sql

select r.LastName, r.FirstName from Reader r, (select distinct ReaderNr from Bororowing where ReturnDate is not null) q where r.ID = q.ReaderNr

```
e)

``` sql

select r.LastName, r.FirstName from Reader r, (select distinct ReaderNr from Borrowing b, (select ISBN from Borowing b, (select ID from Reader where FirstName = 'Ivan' and r.LastName = 'Ivanov') q where b.ReaderNr = q.ID) q where q.ISBN = b.ISBN) allr where r.ID = allr.ID and r.LastName != 'Ivanov' and r.FirstName != 'Ivan'

```

### task 2

a)

``` sql

select * from Connection c where not exists (select * from Connection o where c.TrainNr = o.TrainNr and c.FromStation == o.FromStation and c.FromStation = 'Moscow' and c.ToStation = 'Tver' and c.ToStation != o.ToStation)

```

b) 

``` sql
(select * from Connection c where c.FromStation = 'Moscow' and c.ToStation = 'Sankt-Petersburg' and c.Depature.Day() = c.Arrival.Day())
minus
(select * from Connection c where c.Depature.Day() = c.Arrival.Day() and  not exists (select * from Connection o where c.TrainNr = o.TrainNr and c.FromStation == o.FromStation and c.FromStation = 'Moscow' and c.ToStation = 'Sankt-Petersburg' and c.ToStation != o.ToStation))

```
c) 


### task 3
tables:<br>
L(A<sub>1</sub>,.., A<sub>m</sub>, B<sub>1</sub>,.., B<sub>k</sub>)<br>
R(B<sub>1</sub>,.., B<sub>k</sub>, C<sub>1</sub>,.., C<sub>n</sub>)

P -projection, S -sigma

P<sub>all</sub> = P<sub>A<sub>1</sub>,.., A<sub>m</sub>, B<sub>1</sub>,.., B<sub>k</sub>, C<sub>1</sub>,.., C<sub>n</sub></sub>

P<sub>left</sub> = P<sub>A<sub>1</sub>,.., A<sub>m</sub>, B<sub>1</sub>,.., B<sub>k</sub></sub>

P<sub>right</sub> = P<sub>B<sub>1</sub>,.., B<sub>k</sub>, C<sub>1</sub>,.., C<sub>n</sub></sub>

S<sub>join</sub> = S<sub>L.B<sub>1</sub> = R.B<sub>1</sub> & ... & L.B<sub>k</sub> = R.B<sub>k</sub></sub>


L (outer join) R = P<sub>all</sub>(L - P<sub>left</sub>(S<sub>join</sub>(LxR))) union  P<sub>all</sub>(R - P<sub>left</sub>(S<sub>join</sub>(LxR))) union P<sub>all</sub>(S<sub>join</sub>(LxR))



