INSERT INTO
    "borrowing"
VALUES
    (
        (
            SELECT
                reader_nr
            FROM
                reader
            WHERE
                first_name = 'Carolyn'
                AND last_name = 'Treske'
        ),
        '123456',
        4,
        NULL
    );