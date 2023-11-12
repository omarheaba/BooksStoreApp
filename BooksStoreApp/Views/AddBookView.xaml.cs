using BooksStoreApp.Models;
using BooksStoreApp.Services;

namespace BooksStoreApp.Views;

public partial class AddBookView : ContentPage
{
    public BooksServices booksServices { get; set; }
    public AuthorsServices authorsServices { get; set; }
    public AddBookView()
	{
		InitializeComponent();
        this.booksServices = new BooksServices();
        this.authorsServices = new AuthorsServices();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        List<AuthorModel> authorsList = await this.authorsServices.GetAllAuthors();

        if (authorsList != null)
            this.authorsPicker.ItemsSource = authorsList.ToList();
    }

    private async void saveBookBtn_Clicked(object sender, EventArgs e)
    {
        AuthorModel selectedAuthor = (AuthorModel)this.authorsPicker.SelectedItem;

        BookModel bookInfo = new BookModel();

        bookInfo.BookTitle = this.bookTitleEntry.Text;
        bookInfo.AuthorId = selectedAuthor.AuthorId;

        BookModel addBookResult = await this.booksServices.AddNewBook(bookInfo);

        await Navigation.PopAsync();
    }
}