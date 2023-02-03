using Domain.EntityArea;
using Domain.TransactionArea;

namespace Domain
{
    public interface IJsonManager
    {
        void SaveTransaction(Transaction transaction);
        void SaveEntity(Entity entity);
        string GetTransactionStr(int id);
        Entity GetEntity(Guid id);
    }
}
