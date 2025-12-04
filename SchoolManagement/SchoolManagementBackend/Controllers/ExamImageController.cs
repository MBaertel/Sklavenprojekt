using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementBackend.InputModels;
using SchoolManagementInfrastructure.EF;
using SchoolManagementInfrastructure.EF.Models;

namespace SchoolManagementBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExamImageController(SchoolManagementContext context) : ControllerBase
{
    [HttpGet("GetExamImage")]
    public IActionResult GetExamImage(Guid imageId)
    {
        var efImage = context.ExamImages.SingleOrDefault(x => x.Id == imageId);
        if (efImage == null)
        {
            return NotFound($"ExamImage with Id {imageId} not found.");
        }
        return Ok(efImage);
    }

    [HttpGet("GetExamImages")]
    public IActionResult GetExamImages([FromQuery]List<Guid> imageIds)
    {
        var efExamImages = new List<EFExamImage>();
        var notFound = new List<Guid>();
        foreach (var image in imageIds)
        {
            var found = context.ExamImages.SingleOrDefault(x => x.Id == image);
            if (found == null)
            {
                notFound.Add(image);
                continue;
            }
            efExamImages.AddRange(found);
        }
        
        if (notFound.Count > 0)
        {
            var json = JsonSerializer.Serialize(notFound);
            return NotFound($"Following Ids of ExamImages not found: {json}");
        }
        
        return Ok(efExamImages);
    }

    [HttpPost("CreateExamImage")]
    public async Task<IActionResult> PostExamImage([FromBody] ExamImageInput input)
    {
        var examImage = input.ToEf();
        await context.AddAsync(examImage).ConfigureAwait(false);
        await context.SaveChangesAsync().ConfigureAwait(false);
        return Ok(examImage);
    }
    
    [HttpPut("UpdateExamImage")]
    public async Task<IActionResult> UpdateExamImage([FromBody] ExamImageInput input)
    {
        var examImage = input.ToEf();
        var efExamImage = context.ExamImages.SingleOrDefault(x => x.Id == examImage.Id);
        if (efExamImage == null)
        {
            return NotFound($"ExamImage with Id {examImage.Id} not found.");
        }
        
        efExamImage.Name = examImage.Name;
        efExamImage.Link = examImage.Link;
        efExamImage.ExamId = examImage.ExamId;
        efExamImage.Exam = examImage.Exam;
        
        await context.SaveChangesAsync().ConfigureAwait(false);
        return Ok(examImage);
    }

    [HttpDelete("DeleteExamImage")]
    public async Task<IActionResult> DeleteExamImage([FromQuery]List<Guid> imageIds)
    {
        var efExamImages = new List<EFExamImage>();
        var notFound = new List<Guid>();
        foreach (var image in imageIds)
        {
            var found = context.ExamImages.SingleOrDefault(x => x.Id == image);
            if (found == null)
            {
                notFound.Add(image);
                continue;
            }
            efExamImages.AddRange(found);
        }
        
        context.RemoveRange(efExamImages);
        await context.SaveChangesAsync().ConfigureAwait(false);
        
        if (notFound.Count > 0)
        {
            var json = JsonSerializer.Serialize(notFound);
            return NotFound($"Teachers were deleted, except following Ids of ExamImages because they were not found: {json}");
        }
        
        return Ok(efExamImages);
    }
}
