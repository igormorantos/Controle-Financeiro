using AppControleFinanceiro.Models;
using AppControleFinanceiro.Repositories;
using CommunityToolkit.Mvvm.Messaging;
using System.Text;

namespace AppControleFinanceiro.Views;

public partial class TransactionEdit : ContentPage
{
    private Transaction _transaction;
    private readonly ITransactionRepository _respository;
    public TransactionEdit(ITransactionRepository respository)
    {
        InitializeComponent();
        _respository = respository;
    }

    public void SetTransactionToEdit(Transaction transaction)
    {
        _transaction = transaction;

        if (transaction.Type == TransactionType.Income)
        {
            RadioIncome.IsChecked = true;
        }
        else if (transaction.Type == TransactionType.Expense)
        {
            RadioExpense.IsChecked = true;
        }

        EntryName.Text = transaction.Name;

        DatePickerDate.Date = transaction.Date.Date;

        EntryValue.Text = transaction.Value.ToString();
    }

    private void TapGestuRecognizerTapped_ToClose(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private void OnButtonClicked_Save(object sender, EventArgs e)
    {
        if (IsValidData() == false) return;

        SaveTransactionInDataBase();


        Navigation.PopModalAsync();
        WeakReferenceMessenger.Default.Send<string>(string.Empty);

        var count = _respository.GetAll().Count;

        App.Current.MainPage.DisplayAlert("Mensagem!", "Edição feita com sucesso!", "OK");
    }

    private void SaveTransactionInDataBase()
    {

        Transaction transactionm = new Transaction()
        {
            Id = _transaction.Id,
            Name = EntryName.Text,
            Type = RadioIncome.IsChecked ? TransactionType.Income : TransactionType.Expense,
            Date = DatePickerDate.Date,
            Value = double.Parse(EntryValue.Text)
        };

        _respository.Update(transactionm);
    }

    private bool IsValidData()
    {
        bool valid = true;
        StringBuilder sb = new StringBuilder();

        if (string.IsNullOrEmpty(EntryName.Text) || string.IsNullOrWhiteSpace(EntryName.Text))
        {
            sb.AppendLine("o campo 'Nome' deve ser preenchido");
            valid = false;
        }

        if (string.IsNullOrEmpty(EntryValue.Text) || string.IsNullOrWhiteSpace(EntryValue.Text))
        {
            sb.AppendLine("o campo 'valor' deve ser preenchido");
            valid = false;
        }

        double result;
        if (!string.IsNullOrEmpty(EntryValue.Text) && !double.TryParse(EntryValue.Text, out result))
        {
            sb.AppendLine("o campo 'Valor' é invalido");
            valid = false;
        }
        if (valid == false)
        {
            LabelError.IsVisible = true;
            LabelError.Text = sb.ToString();
        }

        return valid;
    }
}
