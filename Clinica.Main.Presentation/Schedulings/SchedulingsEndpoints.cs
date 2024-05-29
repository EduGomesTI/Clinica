using Carter;
using Clinica.Main.Application.Scheduling.Responses;
using Clinica.Main.Application.Schedulings.Commands;
using Clinica.Main.Application.Schedulings.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Clinica.Main.Presentation.Schedulings
{
    public sealed class SchedulingsEndpoints : CarterModule
    {
        public SchedulingsEndpoints() : base("/api/schedulings")
        {
            IncludeInOpenApi();
            WithTags("Schedulings");
            WithMetadata(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
            WithMetadata(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
            WithMetadata(new ProducesAttribute("application/json"));
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/getAll/{isPaged}/{pageStart}/{pageSize}",
            async (string isPaged, int pageStart, int pageSize, ISender sender) =>
            {
                _ = bool.TryParse(isPaged, out bool pageParsed);

                GetAllSchedulingsQuery query = new();

                if (pageParsed)
                {
                    query.Page = true;
                    query.PageStart = pageStart;
                    query.PageSize = pageSize;
                }

                var response = await sender.Send(query);

                return Results.Ok(response);
            }).WithMetadata(new ProducesResponseTypeAttribute(typeof(IEnumerable<GetSchedulingResponse>), StatusCodes.Status200OK));

            app.MapGet("getByDoctorId/{doctorId}", async (Guid doctorId, ISender sender) =>
            {
                GetSchedulingByDoctorIdQuery query = new(doctorId);

                var response = await sender.Send(query);

                return Results.Ok(response);
            }).WithMetadata(new ProducesResponseTypeAttribute(typeof(IEnumerable<GetSchedulingResponse>), StatusCodes.Status200OK));

            app.MapGet("getByPatientId/{patientId}", async (Guid patientId, ISender sender) =>
            {
                GetSchedulingByPatientIdQuery query = new(patientId);

                var response = await sender.Send(query);

                return Results.Ok(response);
            }).WithMetadata(new ProducesResponseTypeAttribute(typeof(IEnumerable<GetSchedulingResponse>), StatusCodes.Status200OK));

            app.MapPost("/create", async (CreateSchedulingCommand request, ISender sender) =>
            {
                var response = await sender.Send(request);

                return Results.Ok(response);
            }).WithMetadata(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            app.MapPatch("/update", async (UpdateSchedulingCommand request, ISender sender) =>
            {
                var response = await sender.Send(request);

                return Results.Ok(response);
            }).WithMetadata(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));
        }
    }
}