using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessingWorker
{
    public class ExcelImportJob
    {
        public int Id { get; set; }
        public string DriveFileLocation { get; set; }
        public string FileName { get; set; }
        public bool Completed { get; set; }
        public DateTime CompletedDate { get; set; }
        public string Error { get; set; }
    }
}
