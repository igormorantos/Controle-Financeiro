using AppControleFinanceiro.Repositories;

namespace AppControleFinanceiro.Views;

public partial class TransactionList : ContentPage
{
    private TransactionAdd _transactictionAdd;
	private TransactionEdit _transactionEdit;
	private ITransactionRepository _repository;
		
	public TransactionList(TransactionAdd transactictionAdd, TransactionEdit transactictionEdit, ITransactionRepository repository)
	{
        _transactictionAdd = transactictionAdd;
		_transactionEdit = transactictionEdit;
		_repository = repository;

		InitializeComponent();

		CollectionViewTransaction.ItemsSource = _repository.GetAll();
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