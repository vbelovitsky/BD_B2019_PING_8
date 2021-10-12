SELECT
    category_name
FROM
    category
WHERE
    parent_cat IS NOT NULL;