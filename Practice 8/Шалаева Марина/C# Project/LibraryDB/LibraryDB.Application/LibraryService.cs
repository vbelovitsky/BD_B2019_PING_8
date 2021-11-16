using System;
using System.Collections.Generic;
using System.Linq;
using LibraryDB.Domain;

namespace LibraryDB.Application
{
    public class LibraryService
    {
        private readonly LibraryDbContext _db;

        public LibraryService(LibraryDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Получение книги по ее
        /// идентификационному номеру (ISBN).
        /// </summary>
        /// <param name="isbn">
        /// идентификационный номер</param>
        /// <returns>возвращает книгу с данным
        /// ISBN</returns>
        public Book FindBookByIsbn(int isbn)
            => _db.Books.FirstOrDefault(o => o.Isbn == isbn)!;

        /// <summary>
        /// Поиск книги по категории.
        /// </summary>
        /// <param name="categoryName">имя
        /// категории</param>
        /// <returns>возвращает список искомых
        /// книг</returns>
        public List<Book> FindBookByCategoryName(string categoryName)
        {
            var isbns = (from bookCat in _db.BookCats
                where bookCat.CategoryName == categoryName
                select bookCat.Isbn).ToList();
            return isbns.Select(FindBookByIsbn).ToList();
        }

        public List<int> FindBookIsbnByCategoryName(string categoryName)
            => (from bookCat in _db.BookCats
                where bookCat.CategoryName == categoryName
                select bookCat.Isbn).ToList();

        /// <summary>
        /// Добавление объекта класса Book
        /// в таблицу из базы данных.
        /// </summary>
        /// <param name="book">объект
        /// класса Book</param>
        /// <returns>возвращает true,
        /// если операция прошла успешно</returns>
        private bool AddNewBook(Book book)
        {
            try
            {
                _db.Books.Add(book);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool AddNewBook(int isbn, string title, string author,
            int pagesNum, int pubYear, string pubName)
        {
            var res = _db.Books.FirstOrDefault(o => o.Isbn == isbn);
            if (res != null)
                return false;
            Book book = new()
            {
                Isbn = isbn,
                Title = title,
                Author = author,
                PagesNum = pagesNum,
                PubYear = pubYear,
                PubName = pubName
            };
            return AddNewBook(book);
        }

        public void DeleteBookByIsbn(int isbn)
        {
            var res = (from book in _db.Books
                where book.Isbn == isbn
                select book).ToList();
            if (res == null || res.Count <= 0)
                return;
            foreach (var book in res)
            {
                _db.Books.Remove(book);
            }

            _db.SaveChanges();
        }

        public void UpdateBooksIsbn(int oldIsbn, int newIsbn)
        {
            var editingBook = _db.Books.FirstOrDefault(b => b.Isbn == oldIsbn);
            var bookWithNewIsbn = _db.Books.FirstOrDefault(b => b.Isbn == newIsbn);
            if (editingBook == null || bookWithNewIsbn != null)
                return;

            editingBook.Isbn = newIsbn;
            _db.Books.Update(editingBook);

            var copies = (from copy in _db.Copies
                where copy.Isbn == oldIsbn
                select copy).ToList();
            foreach (var item in copies)
            {
                item.Isbn = newIsbn;
                _db.Copies.Update(item);
            }

            var bookCats = (from bookCat in _db.BookCats
                where bookCat.Isbn == oldIsbn
                select bookCat).ToList();
            foreach (var item in bookCats)
            {
                item.Isbn = newIsbn;
                _db.BookCats.Update(item);
            }

            var borrowings = (from borrow in _db.Borrowings
                where borrow.Isbn == oldIsbn
                select borrow).ToList();
            foreach (var item in borrowings)
            {
                item.Isbn = newIsbn;
                _db.Borrowings.Update(item);
            }

            _db.SaveChanges();
        }

        public void UpdateBooksTitle(int isbn, string title)
        {
            var editingBook = _db.Books.FirstOrDefault(b => b.Isbn == isbn);
            if (editingBook == null)
                return;
            editingBook.Title = title;
            _db.Books.Update(editingBook);
            _db.SaveChanges();
        }

        public void UpdateBooksAuthor(int isbn, string author)
        {
            var editingBook = _db.Books.FirstOrDefault(b => b.Isbn == isbn);
            if (editingBook == null)
                return;
            editingBook.Author = author;
            _db.Books.Update(editingBook);
            _db.SaveChanges();
        }

        public void UpdateBooksPagesNumber(int isbn, int pagesNum)
        {
            var editingBook = _db.Books.FirstOrDefault(b => b.Isbn == isbn);
            if (editingBook == null)
                return;
            editingBook.PagesNum = pagesNum;
            _db.Books.Update(editingBook);
            _db.SaveChanges();
        }

        public void UpdateBooksPubYear(int isbn, int pubYear)
        {
            var editingBook = _db.Books.FirstOrDefault(b => b.Isbn == isbn);
            if (editingBook == null)
                return;
            editingBook.PubYear = pubYear;
            _db.Books.Update(editingBook);
            _db.SaveChanges();
        }

        public void UpdateBooksPubName(int isbn, string pubName)
        {
            var editingBook = _db.Books.FirstOrDefault(b => b.Isbn == isbn);
            if (editingBook == null)
                return;
            editingBook.PubName = pubName;
            _db.Books.Update(editingBook);
            _db.SaveChanges();
        }

        /// <summary>
        /// Получение книги по ее
        /// идентификационному номеру (ISBN)
        /// и номеру копии.
        /// </summary>
        /// <returns>возвращает копию с данными
        /// ISBN и номером</returns>
        public Copy FindCopyByIsbnAndCopyNumber(int isbn, int copyNum)
            => _db.Copies.FirstOrDefault(o => (o.Isbn == isbn) &&
                                              (o.CopyNumber == copyNum))!;

        /// <summary>
        /// Добавление объекта класса Copy
        /// в таблицу из базы данных.
        /// </summary>
        /// <param name="copy">объект
        /// класса Copy</param>
        /// <returns>возвращает true,
        /// если операция прошла успешно</returns>
        private bool AddNewCopy(Copy copy)
        {
            try
            {
                _db.Copies.Add(copy);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Создание нового объекта класса Copy
        /// для добавления его в таблицу.
        /// </summary>
        /// <param name="isbn">уникальный номер книги</param>
        /// <param name="copyNumber">номер копии книги</param>
        /// <param name="shelfPosition">позиция на полке</param>
        /// <returns>возвращает true, если операция
        /// добавления прошла успешно</returns>
        public bool AddNewCopy(int isbn, int copyNumber, int shelfPosition)
        {
            var res = _db.Copies.FirstOrDefault(o => (o.Isbn == isbn) &&
                                                     (o.CopyNumber == copyNumber));
            if (res != null)
                return false;
            Copy copy = new()
            {
                Isbn = isbn,
                CopyNumber = copyNumber,
                ShelfPosition = shelfPosition
            };
            return AddNewCopy(copy);
        }

        /// <summary>
        /// Удаление всех копий книги по ISBN
        /// книги.
        /// </summary>
        /// <param name="isbn">уникальный номер
        /// книги</param>
        public void DeleteCopyByIsbn(int isbn)
        {
            var res = (from copy in _db.Copies
                where copy.Isbn == isbn
                select copy).ToList();
            if (res == null || res.Count <= 0)
                return;

            foreach (var copy in res)
            {
                _db.Copies.Remove(copy);
            }

            _db.SaveChanges();
        }

        /// <summary>
        /// Удаление конкретной копии конкретной книги.
        /// </summary>
        /// <param name="isbn">уникальный номер книги</param>
        /// <param name="copyNumber">номер копии книги</param>
        public void DeleteCopyByIsbnAndCopyNumber(int isbn, int copyNumber)
        {
            var res = (from copy in _db.Copies
                where copy.Isbn == isbn
                      && copy.CopyNumber == copyNumber
                select copy).ToList();

            if (res == null || res.Count <= 0)
                return;

            foreach (var copy in res)
            {
                _db.Copies.Remove(copy);
            }

            _db.SaveChanges();
        }

        public void UpdateCopyNumber(int isbn, int oldCopyNumber, int newCopyNumber)
        {
            var editingCopy = _db.Copies.FirstOrDefault(c => (c.Isbn == isbn) &&
                                                             (c.CopyNumber == oldCopyNumber));
            if (editingCopy == null)
                return;
            editingCopy.CopyNumber = newCopyNumber;
            _db.Copies.Update(editingCopy);

            var borrowings = (from borrow in _db.Borrowings
                where borrow.Isbn == isbn &&
                      borrow.CopyNumber == oldCopyNumber
                select borrow).ToList();

            foreach (var item in borrowings)
            {
                item.CopyNumber = newCopyNumber;
                _db.Borrowings.Update(item);
            }

            _db.SaveChanges();
        }

        public void UpdateCopyShelfPosition(int isbn, int oldShelfPosition, int newShelfPosition)
        {
            var editingCopy = _db.Copies.FirstOrDefault(c => (c.Isbn == isbn) &&
                                                             (c.ShelfPosition == oldShelfPosition));
            if (editingCopy == null)
                return;
            editingCopy.ShelfPosition = newShelfPosition;
            _db.Copies.Update(editingCopy);

            _db.SaveChanges();
        }

        /// <summary>
        /// Получение читателя по его
        /// идентификационному номеру.
        /// </summary>
        /// <param name="readerNum">
        /// идентификационный номер</param>
        /// <returns>возвращает читателя с
        /// Id равным readerNum</returns>
        public Reader FindReaderByReaderNumber(int readerNum)
            => _db.Readers.FirstOrDefault(o => o.Id == readerNum)!;

        /// <summary>
        /// Добавление объекта класса Reader
        /// в таблицу из базы данных.
        /// </summary>
        /// <param name="reader">объект
        /// класса Reader</param>
        /// <returns>возвращает true,
        /// если операция прошла успешно</returns>
        private bool AddNewReader(Reader reader)
        {
            try
            {
                _db.Readers.Add(reader);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Создание нового объекта класса Reader
        /// для добавления его в таблицу.
        /// </summary>
        /// <returns>возвращает true, если операция
        /// добавления прошла успешно</returns>
        public bool AddNewReader(int id, string lastName, string firstName,
            string address, DateTime birthDate)
        {
            var res = _db.Readers.FirstOrDefault(o => o.Id == id);
            if (res != null)
                return false;
            Reader reader = new()
            {
                Id = id,
                LastName = lastName,
                FirstName = firstName,
                Address = address,
                BirthDate = birthDate
            };
            return AddNewReader(reader);
        }

        /// <summary>
        /// Удаление читатетя по его идентификационному
        /// номеру.
        /// </summary>
        public void DeleteReaderById(int id)
        {
            var res = (from reader in _db.Readers
                where reader.Id == id
                select reader).ToList();

            if (res == null || res.Count <= 0)
                return;
            foreach (var reader in res)
            {
                _db.Readers.Remove(reader);
            }

            _db.SaveChanges();
        }

        /// <summary>
        /// Получение бронирования по составному ключу.
        /// </summary>
        /// <param name="readerNum">номер читателя</param>
        /// <param name="isbn">уникальный номер
        /// книги</param>
        /// <param name="copyNum">номер копии</param>
        /// <returns>возвращает объект класса
        /// Borrowing с указанным ключом</returns>
        public Borrowing FindBorrowingByHoleKey(int readerNum, int isbn, int copyNum)
            => _db.Borrowings.FirstOrDefault(o => (o.ReaderNr == readerNum) &&
                                                  (o.Isbn == isbn) &&
                                                  (o.CopyNumber == copyNum))!;

        /// <summary>
        /// Получение книг, которые когда-либо
        /// бронировал читатель.
        /// </summary>
        /// <param name="readerNum">Id читатетя</param>
        /// <returns>возвращает список ISBN
        /// книг</returns>
        public List<int> FindBooksWhichWereBorrowedByReader(int readerNum)
            => (from borrow in _db.Borrowings
                where borrow.ReaderNr == readerNum
                select borrow.Isbn)!.ToList();

        /// <summary>
        /// Получение читателей, когда-либо бронировавших
        /// данную книгу.
        /// </summary>
        /// <param name="isbn">уникальный номер
        /// книги</param>
        /// <returns>возвращает список читателей</returns>
        public List<int> FindReadersWhoBorrowedBook(int isbn)
            => (from borrow in _db.Borrowings
                where borrow.Isbn == isbn
                select borrow.ReaderNr)!.ToList();

        /// <summary>
        /// Получение читателей, когда-либо бронировавших
        /// данный экземпляр книги.
        /// </summary>
        /// <param name="isbn">уникальный номер
        /// книги</param>
        /// <param name="copyNum">номер копии
        /// книги</param>
        /// <returns>возвращает список читателей</returns>
        public List<int> FindReadersWhoBorrowedCopy(int isbn, int copyNum)
            => (from borrow in _db.Borrowings
                where borrow.Isbn == isbn &&
                      borrow.CopyNumber == copyNum
                select borrow.ReaderNr)!.ToList();

        /// <summary>
        /// Получение бронирования по ISBN книги.
        /// </summary>
        /// <param name="isbn">уникальный номер 
        /// книги</param>
        /// <returns>возвращает объект класса
        /// Borrowing с указанным isbn</returns>
        public Borrowing FindBorrowingByReader(int isbn)
            => _db.Borrowings.FirstOrDefault(o => o.Isbn == isbn)!;

        private bool AddNewBorrowing(Borrowing borrowing)
        {
            try
            {
                _db.Borrowings.Add(borrowing);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool AddNewBorrowing(int readerNr, int isbn, int copyNumber, DateTime returnDate)
        {
            var res = _db.Borrowings.FirstOrDefault(b => b.ReaderNr == readerNr &&
                                                         b.Isbn == isbn &&
                                                         b.CopyNumber == copyNumber);
            if (res != null)
                return false;
            Borrowing borrowing = new()
            {
                ReaderNr = readerNr,
                Isbn = isbn,
                CopyNumber = copyNumber,
                ReturnDate = returnDate
            };
            return AddNewBorrowing(borrowing);
        }

        public void DeleteBorrowingByHoleKey(int readerNr, int isbn, int copyNumber)
        {
            var res = (from borrow in _db.Borrowings
                where borrow.ReaderNr == readerNr &&
                      borrow.Isbn == isbn &&
                      borrow.CopyNumber == copyNumber
                select borrow).ToList();

            if (res == null || res.Count <= 0)
                return;
            foreach (var borrow in res)
            {
                _db.Borrowings.Remove(borrow);
            }

            _db.SaveChanges();
        }

        public void DeleteBorrowingByReader(int readerNr)
        {
            var res = (from borrow in _db.Borrowings
                where borrow.ReaderNr == readerNr
                select borrow).ToList();

            if (res == null || res.Count <= 0)
                return;
            foreach (var borrow in res)
            {
                _db.Borrowings.Remove(borrow);
            }

            _db.SaveChanges();
        }

        public void DeleteBorrowingByBook(int isbn)
        {
            var res = (from borrow in _db.Borrowings
                where borrow.Isbn == isbn
                select borrow).ToList();

            if (res == null || res.Count <= 0)
                return;
            foreach (var borrow in res)
            {
                _db.Borrowings.Remove(borrow);
            }

            _db.SaveChanges();
        }

        public void DeleteBorrowingByCopy(int isbn, int copyNumber)
        {
            var res = (from borrow in _db.Borrowings
                where borrow.Isbn == isbn &&
                      borrow.CopyNumber == copyNumber
                select borrow).ToList();

            if (res == null || res.Count <= 0)
                return;
            foreach (var borrow in res)
            {
                _db.Borrowings.Remove(borrow);
            }

            _db.SaveChanges();
        }

        public void UpdateBorrowingReturnDate(int readerNr, int isbn, int copyNumber, DateTime returnDate)
        {
            var editingBorrowing = _db.Borrowings.FirstOrDefault(b => b.ReaderNr == readerNr &&
                                                                      b.Isbn == isbn &&
                                                                      b.CopyNumber == copyNumber);
            if (editingBorrowing == null)
                return;

            editingBorrowing.ReturnDate = returnDate;
            _db.Borrowings.Update(editingBorrowing);

            _db.SaveChanges();
        }
    }
}