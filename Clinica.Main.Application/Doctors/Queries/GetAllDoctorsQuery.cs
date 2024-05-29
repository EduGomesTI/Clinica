using Clinica.Base.Domain;
using Clinica.Main.Application.Doctors.Responses;
using MediatR;

namespace Clinica.Main.Application.Doctors.Queries
{
    public sealed record GetAllDoctorsQuery : IRequest<ValueResult<IEnumerable<GetDoctorResponse>>>
    {
        public bool Page { get; set; }

        public int PageStart { get; set; }

        public int PageSize { get; set; }

        public GetAllDoctorsQuery(bool page = false, int pageStart = 1, int pageSize = 1)
        {
            Page = page;
            PageStart = pageStart;
            PageSize = pageSize;
        }
    }
}