using BooksStoreApp.Models;
using BooksStoreApp.Services;
using BooksStoreApp.Views;

namespace BooksStoreApp;

public partial class MainPage : ContentPage
{
    public BooksServices booksServices { get; set; }
    public List<BookModel> BooksList { get; set; }
    public MainPage()
	{
		InitializeComponent();
        this.booksServices = new BooksServices();
        this.BooksList = new List<BookModel>();

	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        this.activityIndicator.IsRunning = true;

        this.BooksList = await this.booksServices.GetAllBooks();

        this.activityIndicator.IsRunning = false;

        if (this.BooksList != null)
            this.booksCV.ItemsSource = this.BooksList.ToList();

        this.booksCV.SelectedItem = null;
    }

    private async void addNewBookBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddBookView());
    }

    private async void editBookBtn_Clicked(object sender, EventArgs e)
    {
        if (this.BooksList.Count == 0)
            return;

        BookModel selectedBook =(BookModel)this.booksCV.SelectedItem;

        if (selectedBook != null)
            await Navigation.PushAsync(new EditBookView(selectedBook));
        else
            await DisplayAlert("Warning", "Please selected a book!", "OK");
    }

    private async void deleteBookBtn_Clicked(object sender, EventArgs e)
    {
        if (this.BooksList.Count == 0)
            return;

        BookModel selectedBook = (BookModel)this.booksCV.SelectedItem;

        if (selectedBook != null)
        {
            bool displayAlertResult = await DisplayAlert("Warning", "Do you really want to delete this book?", "Yes", "No");

            if(displayAlertResult)
            {
                bool deleteBookResult = await this.booksServices.DeleteBook(selectedBook);

                this.booksCV.ItemsSource = await this.booksServices.GetAllBooks();

                this.booksCV.SelectedItem = null;
            }
        }
        else
            await DisplayAlert("Warning", "Please selected a book!", "OK");
    }
}

