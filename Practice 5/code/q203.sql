UPDATE
    Borrowing
SET
    return_date = return_date + make_interval(days => 30)
FROM
    book bk
    JOIN bookcat bc ON bc.isbn = bk.isbn
WHERE
     Borrowing.isbn = bk.isbn
    AND Borrowing.return_date >= '2016-01-01'
    AND bc.category_name = 'Базы данных';