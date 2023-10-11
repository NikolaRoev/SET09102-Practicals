using Test.Data;
using Test.Models;

namespace Test.Views;

[QueryProperty("Item", "Item")]
public partial class RolePage : ContentPage
{
	Role item;
	public Role Item
	{
		get => BindingContext as Role;
		set => BindingContext = value;
	}
    Database database;
    public RolePage(Database todoItemDatabase)
    {
        InitializeComponent();
        database = todoItemDatabase;
    }

    async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Item.Name))
        {
            await DisplayAlert("Name Required", "Please enter a name for the todo item.", "OK");
            return;
        }

        await database.AddItem(Item);
        await Shell.Current.GoToAsync("..");
    }

    async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (Item.ID == 0)
            return;
        await database.DeleteItem(Item);
        await Shell.Current.GoToAsync("..");
    }

    async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}