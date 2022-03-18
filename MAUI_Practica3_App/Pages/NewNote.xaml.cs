using MAUI_Practica3_App.Models;
using MAUI_Practica3_App.Services;

namespace MAUI_Practica3_App;

public partial class NewNote : ContentPage
{
    DBCosmosAzure service;

    public NewNote()
	{
		InitializeComponent();
        service = new DBCosmosAzure();
    }

	private async void OnSaveClicked(object sender, EventArgs e)
    {
		string title = txtContent.Text;
		string content = txtContent.Text;

        try
        {
            if (!title.Equals(""))
            {
                var myGUID = Guid.NewGuid();
                Note note = new Note()
                {
                    Id = myGUID.ToString(),
                    PartitionKey = title + myGUID.ToString(),
                    Title = title,
                    Content = content,
                    DateCreated = DateTime.Now
                };

                await service.SaveAsync(note, true);

                await Shell.Current.GoToAsync("..");
            }
            else
            {
                Console.WriteLine("Note must at least have a title!");
            }
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }

        
    }
}