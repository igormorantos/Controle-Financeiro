using AppControleFinanceiro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppControleFinanceiro.Repositories
{
    public interface ITransactionRepository
    {
        public List<Transaction> GetAll();

        public void Add(Transaction transaction);

        public void Update(Transaction transaction);

        public void Delete(Transaction transaction);
    }
}
