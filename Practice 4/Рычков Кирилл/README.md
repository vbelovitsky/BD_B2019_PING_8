# Практическое задание №4

## Рычков Кирилл, БПИ198

## Задание 1
а) SELECT LastName FROM Reader WHERE Adress LIKE %Moscow%

б) SELECT Book.Author, Book.Title FROM Book JOIN Reader ON Borrowing.ReaderNr == Reader.ID
WHERE Reader.LastName == "Ivanov" AND Reader.FirstName == "Ivan"

в) SELECT DISTINCT Book.ISBN FROM Book JOIN BookCat USING(ISBN) WHERE
BookCat.CategoryName == "Mountains" EXCEPT SELECT DISTINCT Book.ISBN FROM
Book JOIN BookCat USING(ISBN) WHERE BookCat.CategoryName == "Travel"

г) SELECT Reader.LastName, Reader.FirstName FROM Reader JOIN Borrowing ON
 Borrowing.ReaderNr == Reader.ID WHERE Borrowing.ReturnDate IS NOT NULL
 
д) SELECT Reader.LastName, Reader.FirstName FROM Reader JOIN Borrowing ON 
 Reader.ID == Borrowing.ReaderNr WHERE ISBN IN (SELECT ISBN FROM Borrowing JOIN Reader ON
  Reader.ID == Borrowing.ReaderNr WHERE LastName = “Ivanov” AND FirstName == “Ivan”) 
  EXCEPT SELECT Reader.LastName, Reader.FirstName FROM Borrowing JOIN Reader ON
   Reader.ID == Borrowing,ReaderNr WHERE LastName == “Ivanov” AND FirstName == “Ivan”
   
## Задание 2