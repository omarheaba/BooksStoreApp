using BooksStoreApp.Models;
using BooksStoreApp.Services;

namespace BooksStoreApp.Views;

public partial class EditBookView : ContentPage
{
    public BooksServices booksServices { get; set; }
    public AuthorsServices authorsServices { get; set; }

    public BookModel SelectedBook { get; set; }
    public EditBookView(BookModel _selectedBook)
	{
		InitializeComponent();
        this.booksServices = new BooksServices();
        this.authorsServices = new AuthorsServices();

        this.SelectedBook = _selectedBook;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        List<AuthorModel> authorsList = await this.authorsServices.GetAllAuthors();

        this.authorsPicker.ItemsSource = authorsList.ToList();

        this.bookTitleEntry.Text = this.SelectedBook.BookTitle;

        this.authorsPicker.SelectedItem = authorsList.First(author => author.AuthorId == this.SelectedBook.AuthorId);

    }

    private async void saveChangesBtn_Clicked(object sender, EventArgs e)
    {
        AuthorModel selectedAuthor = (AuthorModel)this.authorsPicker.SelectedItem;

        this.SelectedBook.BookTitle = this.bookTitleEntry.Text;
        this.SelectedBook.AuthorId = selectedAuthor.AuthorId;

        BookModel editBookResult = await this.booksServices.EditBook(this.SelectedBook);

        await Navigation.PopAsync();
    }
}