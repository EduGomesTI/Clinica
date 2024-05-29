using Clinica.Base.Domain;
using Clinica.Main.Application.Patients.Responses;
using MediatR;

namespace Clinica.Main.Application.Patients.Queries
{
    public sealed record GetAllPatientsQuery : IRequest<ValueResult<IEnumerable<GetPatientResponse>>>
    {
        public bool Page { get; set; }

        public int PageStart { get; set; }

        public int PageSize { get; set; }

        public GetAllPatientsQuery(
            bool page = false,
            int pageStart = 1,
            int pageSize = 1)
        {
            Page = page;
            PageStart = pageStart;
            PageSize = pageSize;
        }
    }
}