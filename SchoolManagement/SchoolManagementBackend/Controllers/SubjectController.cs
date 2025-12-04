using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementBackend.InputModels;
using SchoolManagementInfrastructure.EF;
using SchoolManagementInfrastructure.EF.Models;

namespace SchoolManagementBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubjectController(SchoolManagementContext context) : ControllerBase
{
    [HttpGet("GetSubject")]
    public async Task<IActionResult> GetSubject(Guid subjectId)
    {
        var subjectEf = await context.Subjects.SingleOrDefaultAsync(x => x.Id == subjectId);
        if (subjectEf == null)
        {
            return NotFound($"Subject with Id {subjectId} not found.");
        }
        return Ok(subjectEf);
    }

    [HttpGet("GetSubjects")]
    public async Task<IActionResult> GetSubjects([FromQuery]List<Guid> subjectIdList)
    {
        var efSubjects = new List<EFSubject>();
        var notFound = new List<Guid>();
        foreach (var subjectId in subjectIdList)
        {
            var foundEfSubject = await context.Subjects.SingleOrDefaultAsync(x => x.Id == subjectId);
            if (foundEfSubject == null)
            {
                notFound.Add(subjectId);
                continue;
            }
            efSubjects.Add(foundEfSubject);
        }

        if (notFound.Count > 0)
        {
            var json = JsonSerializer.Serialize(notFound);
            return NotFound($"Following Ids of Subjects not found: {json}");
        }
        return Ok(efSubjects);
    }

    [HttpPost]
    public async Task<IActionResult> PostSubject([FromBody] SubjectInput subjectInput)
    {
        var subject = subjectInput.ToEf();
        context.Add(subject);
        await context.SaveChangesAsync();
        return Ok(subject.Id);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateSubject(Guid subjectId, [FromBody] SubjectInput subjectInput)
    {
        var subject = subjectInput.ToEf();
        var efSubject = context.Subjects.Include(efSubject => efSubject.BaseSubject).FirstOrDefault(x => x.Id == subjectId);
        if (efSubject == null) return NotFound($"Subject with Id {subjectId} not found.");
        efSubject.BaseSubject.Name = subjectInput.Name;
        await context.SaveChangesAsync();
        return Ok(subject.Id);
    }

    [HttpPut("{subjectId:guid}/classes")]
    public async Task<IActionResult> AddClassToSubject(Guid subjectId, [FromBody] ICollection<Guid> classIds)
    {
        var efSubject = await context.Subjects.Include(efSubject => efSubject.Teacher).FirstOrDefaultAsync(x => x.Id == subjectId);
        if (efSubject == null) return NotFound($"Subject with Id {subjectId} not found.");
        
        var classes = await context.Class.Where(x => classIds.Contains(x.Id)).ToListAsync();
        if (classes.Count == 0) return NotFound("No classes with specified IDs found");

        var classTeachers = classes.Select(x => new EFClassTeacher
        {
            Class = x,
            Teacher = efSubject.Teacher
        });
        foreach (var classTeacher in classTeachers)
        {
            efSubject.Teacher.ClassTeachers.Add(classTeacher);
        }
        await context.SaveChangesAsync();
        return Ok(efSubject);
    }
    
    [HttpDelete("DeleteSubjects")]
    public async Task<IActionResult> DeleteSubjects([FromQuery]List<Guid> subjectIdList)
    {
        var efSubjects = new List<EFSubject>();
        var notFound = new List<Guid>();
        foreach (var subjectId in subjectIdList)
        {
            var foundEfSubject = await context.Subjects.SingleOrDefaultAsync(x => x.Id == subjectId);
            if (foundEfSubject == null)
            {
                notFound.Add(subjectId);
                continue;
            }
            efSubjects.Add(foundEfSubject);
        }
        
        context.RemoveRange(efSubjects);
        await context.SaveChangesAsync().ConfigureAwait(false);

        if (notFound.Count > 0)
        {
            var json = JsonSerializer.Serialize(notFound);
            return NotFound($"Following Ids of Subjects not found: {json}");
        }
        return Ok(efSubjects);
    }
}