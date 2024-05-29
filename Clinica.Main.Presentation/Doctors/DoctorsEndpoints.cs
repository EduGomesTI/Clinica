using Carter;
using Clinica.Main.Application.Doctors.Commands;
using Clinica.Main.Application.Doctors.Queries;
using Clinica.Main.Application.Doctors.Responses;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Clinica.Main.Presentation.Doctors
{
    public sealed class DoctorsEndpoints : CarterModule
    {
        public DoctorsEndpoints() : base("/api/doctors")
        {
            IncludeInOpenApi();
            WithTags("Doctors");
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

                    GetAllDoctorsQuery query = new();

                    if (pageParsed)
                    {
                        query.Page = true;
                        query.PageStart = pageStart;
                        query.PageSize = pageSize;
                    }

                    var response = await sender.Send(query);

                    return Results.Ok(response);
                }).WithMetadata(new ProducesResponseTypeAttribute(typeof(IEnumerable<GetDoctorResponse>), StatusCodes.Status200OK));

            app.MapGet("/getById/{id}", async (Guid id, ISender sender) =>
            {
                GetDoctorById query = new(id);

                var response = await sender.Send(query);

                return Results.Ok(response);
            }).WithMetadata(new ProducesResponseTypeAttribute(typeof(GetDoctorResponse), StatusCodes.Status200OK));

            app.MapPost("/create", async (CreateDoctorCommand request, ISender sender) =>
            {
                var response = await sender.Send(request);

                return Results.Ok(response);
            }).WithMetadata(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            app.MapPatch("/update", async (UpdateDoctorCommand request, ISender sender) =>
            {
                var response = await sender.Send(request);

                return Results.Ok(response);
            }).WithMetadata(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            app.MapPatch("/softDelete", async (SoftDeleteDoctorCommand request, ISender sender) =>
            {
                var response = await sender.Send(request);

                return Results.Ok(response);
            }).WithMetadata(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));
        }
    }
}