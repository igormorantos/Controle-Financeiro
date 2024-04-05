namespace AppControleFinanceiro.Views;

public partial class TransactionList : ContentPage
{
    private TransactionAdd _transactictionAdd;
	private TransactionEdit _transactionEdit;
	
	public TransactionList(TransactionAdd transactictionAdd, TransactionEdit transactictionEdit)
	{
        this._transactictionAdd = transactictionAdd;
		this._transactionEdit = transactictionEdit; ;
		InitializeComponent();
	}

	private void OnButtonClickToTransactionAdd(object sender, EventArgs args)
	{
		Navigation.PushModalAsync(_transactictionAdd);

    }

    private void OnButtonClickToTransactionEdit(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(_transactionEdit);
    }
}