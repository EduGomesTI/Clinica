using Clinica.Base.Domain;
using Clinica.Main.Application.Scheduling.Responses;
using MediatR;

namespace Clinica.Main.Application.Schedulings.Queries
{
    public sealed record GetAllSchedulingsQuery : IRequest<ValueResult<IEnumerable<GetSchedulingResponse>>>
    {
        public bool Page { get; set; }

        public int PageStart { get; set; }

        public int PageSize { get; set; }

        public GetAllSchedulingsQuery(
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