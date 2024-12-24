using OidcServer.Models;

namespace OidcServer.Repositories
{
    public interface ICodeItemRepository
    {
        CodeItem? FindByCode(string code);
        void Add(string code, CodeItem codeItem);
        void Delete(string code);
    }
}
