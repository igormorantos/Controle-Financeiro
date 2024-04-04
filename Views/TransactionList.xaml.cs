namespace AppControleFinanceiro.Views;

public partial class TransactionList : ContentPage
{
	public TransactionList()
	{
		InitializeComponent();
	}

	private void OnButtonClickToTransactionAdd(object sender, EventArgs args)
	{
		App.Current.MainPage = new TransactionAdd();

    }

    private void OnButtonClickToTransactionEdit(object sender, EventArgs e)
    {
        App.Current.MainPage = new TransactionEdit();
    }
}