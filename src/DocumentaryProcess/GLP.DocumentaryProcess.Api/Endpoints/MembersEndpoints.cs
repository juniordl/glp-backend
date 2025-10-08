using GLP_DocumentaryProcess.Infrastructure.Persistence.Repositories;
using GLP_DocumentaryProcess.Infrastructure.Persistence.UnitOfWork;
using GLP.DocumentaryProcess.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GLP.DocumentaryProcess.Api;

public static class MembersEndpoints
{
    public static IEndpointRouteBuilder MapMemberEndpoints(this IEndpointRouteBuilder app)
    {
        var g = app.MapGroup("/members");
         
        g.MapPost("/", async ([FromBody] CreateMemberRequest req, IRepository<Member> repository, IUnitOfWork unitOfWork, CancellationToken ct) =>
        {

            var member = new Member()
            {
                FullName = req.FullName,
                DocumentNumber = req.DocumentNumber,
                MemberTypeId = req.MemberTypeId,
                UserCreation = "test", 
                UserModification = "test"
            };

            await repository.Add(member, ct);
            await unitOfWork.SaveChanges(ct);
            return Results.Created();
        });
        
        g.MapPut("/", async ([FromBody] UpdateMemberRequest req, IRepository<Member> repository, IUnitOfWork unitOfWork, CancellationToken ct) =>
        {

            var member = new Member()
            {
                Id = req.Id,
                FullName = req.FullName,
                DocumentNumber = req.DocumentNumber,
                MemberTypeId = req.MemberTypeId,
                UserCreation = "test", 
                UserModification = "test"
            };

            repository.Update(member);
            await unitOfWork.SaveChanges(ct);
            return Results.Ok(member);
        });
        return app;
    }
}