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
ORDER BY
    books_no DESC
LIMIT
    1;