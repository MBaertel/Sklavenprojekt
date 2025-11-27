using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementBackend.InputModels;
using SchoolManagementInfrastructure.EF;
using SchoolManagementInfrastructure.EF.Models;

namespace SchoolManagementBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseExamController(SchoolManagementContext context) : ControllerBase
{
    [HttpGet("GetExam")]
    public IActionResult GetExam(Guid examId)
    {
        var efClassExam = context.ClassExams.SingleOrDefault(x => x.Id == examId);
        if (efClassExam == null)
        {
            return NotFound($"ClassExam with Id {examId} not found.");
        }
        return Ok(efClassExam);
    }

    [HttpGet("GetExams")]
    public IActionResult GetExams([FromQuery]List<Guid> examIdList)
    {
        var efExams = new List<EFClassExam>();
        foreach (var exam in examIdList)
        {
            var found = context.ClassExams.Where(x => x.Id == exam).ToList();
            efExams.AddRange(found);
        }
        return Ok(efExams);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostExam([FromBody]BaseExamInput examInput)
    {
        var exam = examInput.ToEf();
        context.Add(exam);
        await context.SaveChangesAsync();
        return Ok(exam.Id);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateExam(Guid examId, [FromBody] BaseExamInput examInput)
    {
        var exam = examInput.ToEf();
        var efExam = await context.ClassExams.FirstOrDefaultAsync(x => x.Id == examId);
        if (efExam == null)
        {
            return NotFound($"Exam with Id {examId} not found.");
        }
        efExam.Date = exam.Date;
        await context.SaveChangesAsync();
        return Ok(exam.Id);
    }
    
    [HttpPut("{baseExamId:guid}/classes")]
    public async Task<IActionResult> AddClassToBaseExam(Guid baseExamId, [FromBody] ICollection<Guid> classIds)
    {
        var efBaseExam = await context.ClassExams.FirstOrDefaultAsync(x => x.Id == baseExamId);
        if (efBaseExam == null) return NotFound($"BaseExam with Id {baseExamId} not found.");
        
        var classes = await context.Class.Where(x => classIds.Contains(x.Id)).ToListAsync();
        if (classes.Count == 0) return NotFound("No classes with specified IDs found.");

        context.ClassExams.AddRange(classes.Select(x => new EFClassExam
        {
            Class = x,
        }));
        await context.SaveChangesAsync();
        return Ok(efBaseExam);
    }
    
    [HttpPut("{baseExamId:guid}/subjects")]
    public async Task<IActionResult> AddSubjectToBaseExam(Guid baseExamId, [FromBody] ICollection<Guid> subjectIds)
    {
        var efBaseExam = await context.ClassExams.FirstOrDefaultAsync(x => x.Id == baseExamId);
        if (efBaseExam == null) return NotFound($"BaseExam with Id {baseExamId} not found.");
        
        var subjects = await context.Subjects.Where(x => subjectIds.Contains(x.Id)).ToListAsync();
        if (subjects.Count == 0) return NotFound("No subjects with specified IDs found.");
        
        context.ClassExams.AddRange(subjects.Select(x => new EFClassExam
        {
            Subject = x,
        }));
        await context.SaveChangesAsync();
        return Ok(efBaseExam.Id);
    }
}