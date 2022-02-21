using System;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Dapper;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using OrderProcessingWorker;
using Serilog;

public class ExcelJobConnector : IJobConnector<ExcelImportJob>
{
    private readonly ILogger<ExcelJobConnector> logger;
    private readonly MySqlConnection databaseConnection;
    private const string SqlCommand_GetNextJob = @"SELECT * FROM deltatra_client_admin.import_excel_queue
                                                        where Completed IS NOT NULL
                                                          AND Completed = false 
                                                          ORDER BY ID
                                                        limit 1;";

    private const string SqlCommand_CompleteJob = @"Update deltatra_client_admin.import_excel_queue
                                                    set Completed = true,
	                                                CompletedDate = @CompletedDate,
                                                    Error = @Error
                                                    WHERE Id = @Id;";

    public ExcelJobConnector(ILogger<ExcelJobConnector> logger)
    {
        this.logger = logger;

        var connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION");
        this.databaseConnection = new MySqlConnector.MySqlConnection(connectionString);

    }

    public async Task<ExcelImportJob> GetNextJobAsync()
    {
        await databaseConnection.OpenAsync();
        var nextJob = await databaseConnection.QuerySingleAsync<ExcelImportJob>(SqlCommand_GetNextJob);
        if (nextJob != null)
        {
            try
            {
                return nextJob;
            }
            catch (Exception e)
            {
                Log.Error(e, "Something went wrong when querying the database for a new job");
                nextJob.Error = e.Message;
                await CompleteJobAsync(nextJob, JobCompletionType.Error);
                return null;
            }
            finally
            {
                await databaseConnection.CloseAsync();
            }
        }
        await databaseConnection.CloseAsync();
        return null;

    }

    public async Task CompleteJobAsync(ExcelImportJob job, JobCompletionType completionType)
    {
        //await orderQueueClient.DeleteMessageAsync(order.QueueMessageId, order.QueuePopReceipt);
    }
}