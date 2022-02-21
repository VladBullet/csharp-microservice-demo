using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessingWorker
{
    using System;
    using System.Threading.Tasks;
    using Coravel.Invocable;
    using Microsoft.Extensions.Logging;
    using FluentEmail.Core;

    public class ProcessExcel : IInvocable
    {
        private readonly ILogger<ProcessExcel> logger;
        private readonly IJobConnector<ExcelImportJob> JobConnector;
        private readonly IFluentEmail email;

        public ProcessExcel(ILogger<ProcessExcel> logger, IJobConnector<ExcelImportJob> jobConnector, IFluentEmail email)
        {
            this.logger = logger;
            this.JobConnector = jobConnector;
            this.email = email;
        }

        public async Task Invoke()
        {
            var nextOrder = await JobConnector.GetNextJobAsync();


            //if (nextOrder != null)
            //{
            //    logger.LogInformation("Processing order {@nextOrder}", nextOrder);

            //    var emailTemplate =
            //        @"<p>Dear @Model.CustomerName,</p> 
            //    <p>Your order of @Model.QuantityOrdered @Model.ItemName has been received!</p>
            //    <p>Sincerely,<br>Roberts Dev Talk</p>";

            //    var newEmail = email
            //        .To(nextOrder.CustomerEmail)
            //        .Subject($"Thanks for your order {nextOrder.CustomerName}")
            //        .UsingTemplate<OrderInfo>(emailTemplate, nextOrder);

            //    await newEmail.SendAsync();
            //    await orderConnector.CompleteJob(nextOrder);

            //    logger.LogInformation($"Order {nextOrder.OrderId} processed and email sent");
            //}
        }
    }
}
