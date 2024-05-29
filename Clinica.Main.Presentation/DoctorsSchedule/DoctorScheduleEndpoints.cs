using Carter;
using Clinica.Main.Application.DoctorsSchedule.Commands;
using Clinica.Main.Application.DoctorsSchedule.Queries;
using Clinica.Main.Application.DoctorsSchedule.Responses;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Clinica.Main.Presentation.DoctorsSchedule
{
    public sealed class DoctorScheduleEndpoints : CarterModule
    {
        public DoctorScheduleEndpoints() : base("/api/doctorsSchedule")
        {
            IncludeInOpenApi();
            WithTags("Doctors Schedule");
            WithMetadata(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
            WithMetadata(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
            WithMetadata(new ProducesAttribute("application/json"));
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/getByDoctorId/{doctorId}", async (Guid doctorId, ISender sender) =>
            {
                GetDoctorSchedulesByDoctorIdQuery query = new(doctorId);

                var response = await sender.Send(query);

                return Results.Ok(response);
            }).WithMetadata(new ProducesResponseTypeAttribute(typeof(IEnumerable<GetDoctorScheduleResponse>), StatusCodes.Status200OK));

            app.MapPost("/create", async (CreateDoctorScheduleCommand request, ISender sender) =>
            {
                var response = await sender.Send(request);

                return Results.Ok(response);
            }).WithMetadata(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            app.MapPatch("/update", async (UpdateDoctorScheduleCommand request, ISender sender) =>
            {
                var response = await sender.Send(request);

                return Results.Ok(response);
            }).WithMetadata(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));

            app.MapPatch("/softDelete", async (SoftDeleteDoctorScheduleCommand request, ISender sender) =>
            {
                var response = await sender.Send(request);

                return Results.Ok(response);
            }).WithMetadata(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));
        }
    }
}