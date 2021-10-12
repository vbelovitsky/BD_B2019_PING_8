DELETE FROM
    book
WHERE
    extract(
        year
        FROM
            pub_year
    ) > 2020;