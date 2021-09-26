# Задача 1

В реляционной схеме любое отношение имеет хотя бы один ключ, чтобы он мог однозначно индетифицировал кортеж (потому что одинаковых кортежей быть не может, т.к повтор приводит к возникновению ошибок) и отношения в схеме (из-за наследования от соединенных объектов). Иными словами, должен быть хотя бы один ключ, иначе было бы невозможно индетифицировать отношения в реляционной схеме.

# Задача 2

* 2.1
* 
https://github.com/mikamurasaki2/BD_B2019_PING_8/blob/master/Practice2/Task2_1.png

PK *key*

Entities:

Categories: { *categ_id: int*; name: string }

Book: { *ISBN: int*; title: string; author: string; year: datetime; numOfPages: int }

Publisher: { *publ_id: int*; name: string; address: string }

Copy: { *copy_id: int*; position: int }

Rent: { *rent_id: int*; dateReturn: datetime }

Reader: { *read_id: int*; firstName: string; secondName: string; birthday: datetime; address: string }


FK **key**

Relations:

Categories: { *categ_id: int*; name: string; **parent_id: int** }

Book: { *ISBN: int*; title: string; author: string; year: datetime; numOfPages: int; **categ_id: int; publ_id: int** }

Publisher: { *publ_id: int*; name: string; address: string }

Copy: { *copy_id: int*; position: int; **ISBN: int** }

Rent: { *rent_id: int*; dateReturn: datetime; **read_id: int; copy_id: int** }

Reader: { *read_id: int*; firstName: string; secondName: string; birthday: datetime; address: string }
