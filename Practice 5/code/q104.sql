SELECT
    *
FROM
    book
WHERE
    pages_num > 2 * (
        SELECT
            avg(pages_num)
        FROM
            book
    );