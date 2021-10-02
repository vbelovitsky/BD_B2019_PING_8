# Задача 1

a) SELECT LastName FROM Reader WHERE CONTAINS(Address, 'Moscow')

б) SELECT book.Author, book.Title FROM Borrowing brw 
    JOIN Book book ON book.ISBN = brw.ISBN 
    JOIN Reader rd ON rd.ID = brw.ReaderNr  
    WHERE rd.Lastname='Иванов' AND rd.FirstName='Иван'
      
в) SELECT b.ISBN FROM Book b 
    JOIN BookCat bc ON b.ISBN = bc.ISBN 
    WHERE bc.CategoryName = 'Горы' 
    AND bc.CategoryName != 'Путешествия'
    
г) SELECT rd.LastName, rd.FirstName FROM Reader rd 
    JOIN Borrowing brw ON brw.ReaderNr = rd.ID
    Where brw.ReturnDate <= GETDATE()
    
д) 


# Задача 2

а) SELECT * FROM Connection
    WHERE FromStation = 'Москва'
    AND ToStation = 'Тверь'
 
