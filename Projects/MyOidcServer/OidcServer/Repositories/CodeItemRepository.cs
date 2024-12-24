using OidcServer.Models;

namespace OidcServer.Repositories
{
    public class CodeItemRepository : ICodeItemRepository
    {
        private readonly Dictionary<string, CodeItem> items = [];

        public void Add(string code, CodeItem codeItem)
        {
            items[code] = codeItem;
        }

        public void Delete(string code)
        {
            items.Remove(code);
        }

        public CodeItem? FindByCode(string code)
        {
            return items.TryGetValue(code, out var codeItem) ? codeItem : null;
        }
    }
}
