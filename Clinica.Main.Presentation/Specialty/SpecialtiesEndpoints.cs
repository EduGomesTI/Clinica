using Carter;
using Clinica.Main.Application.Specialty.Commands;
using Clinica.Main.Application.Specialty.Queries;
using Clinica.Main.Application.Specialty.Responses;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Clinica.Main.Presentation.Specialty
{
    public sealed class SpecialtiesEndpoints : CarterModule
    {
        public SpecialtiesEndpoints() : base("/api/specialties")
        {
            IncludeInOpenApi();
            WithTags("Specialties");
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

                    GetAllSpecialtiesQyery query = new();

                    if (pageParsed)
                    {
                        query.Page = true;
                        query.PageStart = pageStart;
                        query.PageSize = pageSize;
                    }

                    var response = await sender.Send(query);

                    return Results.Ok(response);
                }).WithMetadata(new ProducesResponseTypeAttribute(typeof(IEnumerable<GetSpecialtyResponse>), StatusCodes.Status200OK));

            app.MapGet("/getById/{id}", async (Guid id, ISender sender) =>
            {
                GetSpecialtyByIdQuery query = new(id);

                var response = await sender.Send(query);

                return Results.Ok(response);
            }).WithMetadata(new ProducesResponseTypeAttribute(typeof(GetSpecialtyResponse), StatusCodes.Status200OK));

            app.MapPost("/create", async (CreateSpecialtyCommand request, ISender sender) =>
            {
                var response = await sender.Send(request);

                return Results.Ok(response);
            }).WithMetadata(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            app.MapPatch("/update", async (UpdateSpecialtyCommand request, ISender sender) =>
            {
                var response = await sender.Send(request);

                return Results.Ok(response);
            }).WithMetadata(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            app.MapPatch("/softDelete", async (SoftDeleteSpecialtyCommand request, ISender sender) =>
            {
                var response = await sender.Send(request);

                return Results.Ok(response);
            }).WithMetadata(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));
        }
    }
}