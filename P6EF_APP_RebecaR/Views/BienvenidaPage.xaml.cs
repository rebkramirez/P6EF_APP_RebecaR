    using P6EF_APP_RebecaR.ViewModels;
    using P6EF_APP_RebecaR.Models;
    using Newtonsoft.Json;
    using System.Text;


    namespace P6EF_APP_RebecaR.Views;

public partial class BienvenidaPage : ContentPage
{
    public BienvenidaPage()
    {
        InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        string username = TxtUserName.Text;
        string userpassword = TxtUserPassword.Text;

        Console.WriteLine($"Usuario enviado: {username}");
        Console.WriteLine($"Contraseña enviada: {userpassword}");

        var userModel = new User(); 
        var user = await userModel.ValidateUserCredentialsAsync(username, userpassword); // Llama al método

        if (user != null)
        {
            await DisplayAlert("Inicio de sesión", "Inicio de sesión exitoso", "OK");
            await Navigation.PushAsync(new PreguntaPage());
        }
        else
        {
            await DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
        }
        //await Navigation.PushAsync(new PreguntaPage());
    }







    private void SwShowPassword_Toggled(object sender, ToggledEventArgs e)
    {
        TxtUserPassword.IsPassword = !SwShowPassword.IsToggled;
    }
}
