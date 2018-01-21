using DAL;
using Entities.Models;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace BLL.Repository
{
    public class MessageRepo : RepositoryBase<Entity.Entities.Message, long>
    {
        public MessageRepo()
        {
            return;
            try
            {
                MyContext db = new MyContext();
                db.Messages.Add(new Entity.Entities.Message()
                {
                    Level = -5,
                    SendBy = "46f0cc49-62d0-4ffc-9a96-af5f9a5da0ae",
                    SentTo = "46f0cc49-62d0-4ffc-9a96-af5f9a5da0ae"
                });
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public class FaultReportConfirmationRepo : RepositoryBase<FaultReportConfirmation, int> { }
        public class FaultReportsRepo : RepositoryBase<FaultReports, int> { }
        public class SurveysRepo : RepositoryBase<Surveys, int> { }
        public class TechnicionsRepo : RepositoryBase<Technicions, int> { }
        public class WorksRepo : RepositoryBase<Works, int> { }
        public class WorksReportsRepo : RepositoryBase<WorksReports, int> { }
        public class ImagesRepo : RepositoryBase<Images, int> { }
        public class BranchesRepo : RepositoryBase<Branches, int> { }
    }
}

