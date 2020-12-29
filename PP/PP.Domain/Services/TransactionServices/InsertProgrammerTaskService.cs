using PP.Domain.Models;

namespace PP.Domain.Services.TransactionServices
{
    public class InsertProgrammerTaskService : IInsertProgrammerTaskService
    {
        private readonly IDataService<ProgrammerTask> _taskService;

        public InsertProgrammerTaskService(IDataService<ProgrammerTask> taskService)
        {
            _taskService = taskService;
        }
    }
}