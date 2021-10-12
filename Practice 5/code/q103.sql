SELECT
    t.author
FROM
    (
        SELECT
            author,
            Count(*) books_no
        FROM
            book
        GROUP BY
            author
    ) AS t
WHERE
    t.books_no > 1;