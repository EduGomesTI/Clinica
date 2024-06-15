using Clinica.Base.Domain;
using Clinica.Main.Application.Specialty.Responses;
using MediatR;

namespace Clinica.Main.Application.Specialty.Queries
{
    public sealed record GetAllSpecialtiesQyery : IRequest<ValueResult<IEnumerable<GetSpecialtyResponse>>>
    {
        public bool Page { get; set; }

        public int PageStart { get; set; }

        public int PageSize { get; set; }

        public GetAllSpecialtiesQyery(bool page = false, int pageStart = 1, int pageSize = 1)
        {
            Page = page;
            PageStart = pageStart;
            PageSize = pageSize;
        }
    }
}