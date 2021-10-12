WITH mk_books AS (
    SELECT
        isbn
    FROM
        book
    WHERE
        author = 'Mark Twain'
)
SELECT
    rd.*
FROM
    (
        SELECT
            reader_nr,
            count(*) AS amount
        FROM
            borrowing
        WHERE
            isbn IN (
                SELECT
                    *
                FROM
                    mk_books
            )
        GROUP BY
            reader_nr
    ) t
    JOIN reader rd ON rd.id = t.reader_nr
WHERE
    t.amount = (
        SELECT
            count(*)
        FROM
            mk_books
    );