using OrderProcessingWorker;
using System.Threading.Tasks;

public interface IJobConnector<TDocument> where TDocument : class
{
    Task<TDocument> GetNextJobAsync();
    Task CompleteJobAsync(TDocument order, JobCompletionType completionType);
}