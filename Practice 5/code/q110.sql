WITH RECURSIVE search_path (category_name, parent_cat) AS (
    SELECT
        *
    FROM
        category
    WHERE
        parent_cat = 'Sport'
    UNION
    ALL
    SELECT
        c.*
    FROM
        category c
        INNER JOIN search_path s ON c.parent_cat = s.category_name
)
SELECT
    *
FROM
    search_path;