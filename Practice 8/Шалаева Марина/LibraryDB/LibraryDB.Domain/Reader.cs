using System;

namespace LibraryDB.Domain
{
    public class Reader
    {
        // Идентификационный номер читателя.
        public int Id { get; set; }

        // Фамилия читателя.
        public string LastName { get; set; }

        // Имя читателя.
        public string FirstName { get; set; }

        // Адрес читателя.
        public string Address { get; set; }

        // День рождения читателя.
        public DateTime BirthDate { get; set; }
    }
}