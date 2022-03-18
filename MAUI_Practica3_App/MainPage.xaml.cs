
using MAUI_Practica3_App.Models;
using MAUI_Practica3_App.Services;

namespace MAUI_Practica3_App;

public partial class MainPage : ContentPage
{
	DBCosmosAzure service;

	public MainPage()
	{
		InitializeComponent();
		service = new DBCosmosAzure();
		//initiateDB();
		//loadNotes();
		Routing.RegisterRoute(nameof(NewNote), typeof(NewNote));
	}

	private async void initiateDB()
    {
		await service.CreateDatabase();
		await service.CreateDocumentCollection();
	}

	private async void loadNotes()
    {
		List<Note> notes = await service.GetAllAsync();
		lvNotes.ItemsSource = notes;
    }

	private async void onAddNoteClicked(object sender, EventArgs e)
    {
		
		var route = $"{nameof(NewNote)}";
		await Shell.Current.GoToAsync(route);
    }
}

