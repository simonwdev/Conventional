﻿SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE IS_NULLABLE = 'NO'
AND COLUMN_NAME = '{COLUMN_NAME}'
