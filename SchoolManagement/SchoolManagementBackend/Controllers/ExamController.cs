using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementBackend.InputModels;
using SchoolManagementInfrastructure.EF;
using SchoolManagementInfrastructure.EF.Models;

namespace SchoolManagementBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExamController(SchoolManagementContext context) : ControllerBase
{
        [HttpGet("GetIndividualExam")]
        public IActionResult GetIndividualExam(Guid examId)
        {
            var efExam = context.IndividualExams.SingleOrDefault(x => x.Id == examId);
            if (efExam == null)
            {
                return NotFound($"Exam with Id {examId} not found.");
            }
            return Ok(efExam);
        }

        [HttpGet("GetIndividualExams")]
        public IActionResult GetIndividualExams(List<Guid> examIds)
        {
            var efIndividualExams = new List<EFIndividualExam>();
            foreach (var exam in examIds)
            {
                var found = context.IndividualExams.Where(x => x.Id == exam).ToList();
                efIndividualExams.AddRange(found);
            }
            return Ok(efIndividualExams);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateIndividualExam([FromBody] IndividualExamInput  input)
        {
            var student = await context.Students
                .SingleOrDefaultAsync(x => x.Id == input.StudentId);
            if (student == null) return NotFound($"Student with Id {input.StudentId} not found.");
            
            var baseExam = await context.ClassExams
                .SingleOrDefaultAsync(x => x.SubjectId == input.BaseExamId);
            if (baseExam == null) return NotFound($"Base Exam with Id {input.BaseExamId} not found.");
            
            var efIndividualExam = input.ToEf(student, baseExam);
            await context.IndividualExams.AddAsync(efIndividualExam);
            await context.SaveChangesAsync();
            return Ok(efIndividualExam.Id);
        }
        
        [HttpDelete("DeleteIndividualExams")]
        public async Task<IActionResult> DeleteIndividualExams(List<Guid> examIds)
        {
            var efIndividualExams = new List<EFIndividualExam>();
            foreach (var exam in examIds)
            {
                var found = context.IndividualExams.Where(x => x.Id == exam).ToList();
                efIndividualExams.AddRange(found);
            }
            context.RemoveRange(efIndividualExams);
            await context.SaveChangesAsync().ConfigureAwait(false);

            return Ok(efIndividualExams);
        }
        
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateIndividualExam(Guid id, [FromBody] IndividualExamInput input)
        {
            var student = await context.Students
                .SingleOrDefaultAsync(x => x.Id == input.StudentId);
            if (student == null) return NotFound($"Student with Id {input.StudentId} not found.");
            
            var baseExam = await context.ClassExams
                .SingleOrDefaultAsync(x => x.SubjectId == input.BaseExamId);
            if (baseExam == null) return NotFound($"Base Exam with Id {input.BaseExamId} not found.");
            
            var efIndividualExam = input.ToEf(student, baseExam);
            efIndividualExam.BaseExam = baseExam;
            efIndividualExam.BaseExamId = baseExam.Id;
            efIndividualExam.Student = student;
            efIndividualExam.StudentId = student.Id;
            efIndividualExam.Score = input.Score;
            efIndividualExam.Grade = input.Grade;
            efIndividualExam.Images = input.Images;
            
            await context.SaveChangesAsync();
            return Ok(efIndividualExam.Id);
        }
        
}