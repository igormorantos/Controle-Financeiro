using AppControleFinanceiro.Models;
using AppControleFinanceiro.Repositories;
using System.Text;

namespace AppControleFinanceiro.Views;

public partial class TransactionAdd : ContentPage
{
    private readonly ITransactionRepository _respository;
	public TransactionAdd(ITransactionRepository respository)
	{
		InitializeComponent();
        _respository = respository;
    }

    private void TapGestuRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		Navigation.PopModalAsync();
    }

    private void OnButtonClicked_Save(object sender, EventArgs e)
    {
        if (IsValidData() == false) return;
        
        SaveTransactionInDataBase();
        
        //TODO - FECHAR TELA
        Navigation.PopModalAsync();

        var count = _respository.GetAll().Count;
        
        App.Current.MainPage.DisplayAlert("Mensagem!", $"Existem {count} registros no banco", "OK");
    }

    private void SaveTransactionInDataBase()
    {

        Transaction transactionm = new Transaction()
        {
            Name = EntryName.Text,
            Type = RadioInCome.IsChecked ? TransactionType.Income : TransactionType.Expense,
            Date = DatePicker.Date,
            Value = double.Parse(EntryValue.Text)
        };

        _respository.Add(transactionm);
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
        if(!string.IsNullOrEmpty(EntryValue.Text) && !double.TryParse(EntryValue.Text, out result))
        {
            sb.AppendLine("o campo 'Valor' é invalido");
            valid = false;
        }
        if(valid == false)
        {
            LabelError.IsVisible = true;
            LabelError.Text = sb.ToString();
        }

        return valid;
    }
}