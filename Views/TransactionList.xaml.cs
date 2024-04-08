using AppControleFinanceiro.Models;
using AppControleFinanceiro.Repositories;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls;

namespace AppControleFinanceiro.Views;

public partial class TransactionList : ContentPage
{
	private ITransactionRepository _repository;


	public TransactionList(ITransactionRepository repository)
	{
		_repository = repository;

		InitializeComponent();

		Reload();

		WeakReferenceMessenger.Default.Register<string>(this, (e, msg) =>
		{
			Reload();
		});
	}

	private void OnButtonClickToTransactionAdd(object sender, EventArgs args)
	{
		var transactictionAdd = Handler.MauiContext.Services.GetService<TransactionAdd>();
		Navigation.PushModalAsync(transactictionAdd);
	}


	private void TappedGestureRecognizerTapped_To_TransactionEdit(object sender, TappedEventArgs e)
	{
		var grid = (Grid)sender;
		var gesture = (TapGestureRecognizer)grid.GestureRecognizers[0];
		Transaction transaction = (Transaction)gesture.CommandParameter;

        var transactionEdit = Handler.MauiContext.Services.GetService<TransactionEdit>();
        transactionEdit.SetTransactionToEdit(transaction);
        Navigation.PushModalAsync(transactionEdit);
    }

	private void Reload()
	{
		var items = _repository.GetAll();
		CollectionViewTransaction.ItemsSource = _repository.GetAll();
		double income = items.Where(a => a.Type == Models.TransactionType.Income).Sum(a => a.Value);
		double expense = items.Where(a => a.Type == Models.TransactionType.Expense).Sum(a => a.Value);
		double balance = income - expense;

		LabelIncome.Text = income.ToString("C");
		LabelExpense.Text = expense.ToString("C");
		LabelBalance.Text = balance.ToString("C");
	}

    private async void TapGestureRecognizerTapped_ToDelete(object sender, TappedEventArgs e)
    {
		await AnimationBorder((Border)sender, true);
		bool result = await App.Current.MainPage.DisplayAlert("Excluir", "Tem certeza que deseja excluir?", "Sim", "N�o");

		if(result)
		{
			Transaction transaction = (Transaction)e.Parameter;
			_repository.Delete(transaction);
			Reload();
		}
		else
		{
			await AnimationBorder((Border)sender, false);
		}
    }
	private Color _borderOriginalBackgroundColor;
	private String _labelOriginalText;

    private async Task AnimationBorder(Border border, bool IsDeleteAnimation)
	{
        var label = (Label)border.Content;

        if (IsDeleteAnimation)
		{
            _borderOriginalBackgroundColor = border.BackgroundColor;
			_labelOriginalText = label.Text;

            await border.RotateYTo(90, 500);

			border.BackgroundColor = Colors.Red;
			label.TextColor = Colors.White;
			label.Text = "X";
            await border.RotateYTo(180, 500);
        }
		else
		{
            await border.RotateYTo(90, 500);
			border.BackgroundColor = _borderOriginalBackgroundColor;
            label.TextColor = Colors.Black;
			label.Text = _labelOriginalText;
            await border.RotateYTo(0, 500);
        }
	}
}