namespace AppControleFinanceiro.Views;

public partial class TransactionEdit : ContentPage
{
	public TransactionEdit()
	{
		InitializeComponent();
	}

    private void TapGestuRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();
    }
}