namespace MilkStoreWepAPI.Service
{
    public interface IElasticsearchService<T>
    {
        Task<string> CreateDocumentAsync(T document);

        Task<string> DeleteDocumentAsync(int id);   

        Task<T> GetDocumentAsync(int id);

        Task<IEnumerable<T>> GetAllDocumentsAsync();

        Task<string> UpdateDocumentAsync(T document);

    }
}
