SELECT
    bk.*
FROM
    (
        SELECT
            isbn,
            count(*) AS amount
        FROM
            "copy"
        GROUP BY
            isbn
    ) t
    JOIN book bk ON bk.isbn = t.isbn
WHERE
    t.amount > 1;