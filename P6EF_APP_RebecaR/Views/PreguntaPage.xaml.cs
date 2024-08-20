using P6EF_APP_RebecaR.Models;

namespace P6EF_APP_RebecaR.Views;

public partial class PreguntaPage : ContentPage
{

    private User _user;
    public PreguntaPage()
    {
    InitializeComponent();
     //_user = user;

    BindingContext = _user;
    }

    private void PickerUser_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void PickerAskStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }
}