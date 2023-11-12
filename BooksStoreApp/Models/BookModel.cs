

namespace BooksStoreApp.Models
{
    public class BookModel
    {
        public int BookId { get; set; }

        public string BookTitle { get; set; }

        public int AuthorId { get; set; }
        public virtual AuthorModel Author { get; set; }
    }
}
