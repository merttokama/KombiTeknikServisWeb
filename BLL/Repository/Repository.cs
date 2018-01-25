using DAL;
using Entities.Models;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace BLL.Repository
{
    public class MessageRepo : RepositoryBase<Entity.Entities.Message, long>  { }
    public class FaultReportConfirmationRepo : RepositoryBase<FaultReportConfirmation, int> { }
    public class FaultReportsRepo : RepositoryBase<FaultReports, int> { }
    public class SurveysRepo : RepositoryBase<Surveys, int> { }
    public class TechnicionsRepo : RepositoryBase<Technicions, int> { }
    public class WorksRepo : RepositoryBase<Works, int> { }
    public class WorksReportsRepo : RepositoryBase<WorksReports, int> { }
    public class ImagesRepo : RepositoryBase<Images, int> { }
}

