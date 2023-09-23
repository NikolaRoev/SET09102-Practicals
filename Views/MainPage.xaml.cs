using System.Collections.ObjectModel;
using Test.Data;
using Test.Models;

namespace Test.Views;

public partial class MainPage : ContentPage
{
    Database database;
    public ObservableCollection<Role> Items { get; set; } = new();
    public MainPage(Database todoItemDatabase)
	{
		InitializeComponent();
        BindingContext = this;
        database = todoItemDatabase;
    }


    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var items = await database.GetAll();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Items.Clear();
            foreach (var item in items)
                Items.Add(item);
        });
    }
    async void OnItemAdded(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RolePage), true, new Dictionary<string, object>
        {
            ["Item"] = new Role()
        });
    }

    private async void  CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Role item)
            return;

        await Shell.Current.GoToAsync(nameof(RolePage), true, new Dictionary<string, object>
        {
            ["Item"] = item
        });
    }
}

