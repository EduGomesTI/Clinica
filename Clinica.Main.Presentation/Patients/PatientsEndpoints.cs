using Carter;
using Clinica.Main.Application.Patients.Commands;
using Clinica.Main.Application.Patients.Queries;
using Clinica.Main.Application.Patients.Responses;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Clinica.Main.Presentation.Patients
{
    public sealed class PatientsEndpoints : CarterModule
    {
        public PatientsEndpoints() : base("/api/patients")
        {
            IncludeInOpenApi();
            WithTags("Patients");
            WithMetadata(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
            WithMetadata(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
            WithMetadata(new ProducesAttribute("application/json"));
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/login", async (LoginPatientCommand request, ISender sender) =>
            {
                var response = await sender.Send(request);

                if (response.ErrorDetails!.Count > 0)
                    return Results.BadRequest(response.ErrorDetails);

                return Results.Ok(response);
            })
                .WithMetadata(new ProducesResponseTypeAttribute(typeof(LoginPatientResponse), StatusCodes.Status200OK));

            app.MapGet("/getAll/{isPaged}/{pageStart}/{pageSize}",
                async (string isPaged, int pageStart, int pageSize, ISender sender) =>
                {
                    _ = bool.TryParse(isPaged, out bool pageParsed);

                    GetAllPatientsQuery query = new();

                    if (pageParsed)
                    {
                        query.Page = true;
                        query.PageStart = pageStart;
                        query.PageSize = pageSize;
                    }

                    var response = await sender.Send(query);

                    if (response.ErrorDetails!.Count > 0)
                        return Results.BadRequest(response.ErrorDetails);

                    return Results.Ok(response);
                })
                .WithMetadata(new ProducesResponseTypeAttribute(typeof(IEnumerable<GetPatientResponse>), StatusCodes.Status200OK));

            app.MapGet("/getById/{id}", async (Guid id, ISender sender) =>
            {
                GetPatientByIdQuery query = new(id);

                var response = await sender.Send(query);

                if (response.ErrorDetails!.Count > 0)
                    return Results.BadRequest(response.ErrorDetails);

                return Results.Ok(response);
            })
                .WithMetadata(new ProducesResponseTypeAttribute(typeof(GetPatientResponse), StatusCodes.Status200OK));

            app.MapPost("/create", async (CreatePatientCommand request, ISender sender) =>
            {
                var response = await sender.Send(request);

                if (response.ErrorDetails!.Count > 0)
                    return Results.BadRequest(response.ErrorDetails);

                return Results.Ok(response);
            })
                .WithMetadata(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            app.MapPatch("/update", async (UpdatePatientCommand request, ISender sender) =>
            {
                var response = await sender.Send(request);

                if (response.ErrorDetails!.Count > 0)
                    return Results.BadRequest(response.ErrorDetails);

                return Results.Ok(response);
            })
                .WithMetadata(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            app.MapPatch("/softDelete", async (SoftDeletePatientCommand request, ISender sender) =>
            {
                var response = await sender.Send(request);

                if (response.ErrorDetails!.Count > 0)
                    return Results.BadRequest(response.ErrorDetails);

                return Results.Ok(response);
            })
                .WithMetadata(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));
        }
    }
}