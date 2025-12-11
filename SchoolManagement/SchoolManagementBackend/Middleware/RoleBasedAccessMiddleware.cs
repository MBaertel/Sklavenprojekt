using Microsoft.EntityFrameworkCore;
using SchoolManagementDomain.Core.Models.User.Roles;
using SchoolManagementInfrastructure.EF;

namespace SchoolManagementBackend.Middleware;

public class RoleBasedAccessMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, SchoolManagementContext db)
    {
        if (context.User?.Identity?.IsAuthenticated != true)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        var internalIdStr = context.User.FindFirst("internal_id")?.Value;
        if (!Guid.TryParse(internalIdStr, out Guid internalUserId))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            return;
        }

        var dbUser = await db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == internalUserId);
        if (dbUser == null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        if (dbUser.Role == Role.Administrator)
        {
            await next(context);
            return;
        }

        var path = context.Request.Path.Value?.ToLower();

        if (dbUser.Role == Role.Teacher)
        {
            var teacherClasses = await db.ClassTeachers
                .Where(x => x.TeacherId == internalUserId)
                .Select(x => x.ClassId)
                .ToListAsync();

            if (path.StartsWith("/api/classes"))
            {
                var classIds = ExtractIds(context, "id", "classId", "classIdList");
                if (classIds.Count > 0)
                {
                    foreach (var id in classIds)
                    {
                        if (!teacherClasses.Contains(id))
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            return;
                        }
                    }
                }
            }

            if (path.StartsWith("/api/baseexam"))
            {
                var examIds = ExtractIds(context, "id", "examId", "examIdList", "baseExamId");
                if (examIds.Count > 0)
                {
                    foreach (var examId in examIds)
                    {
                        var classId = await db.ClassExams
                            .Where(x => x.Id == examId)
                            .Select(x => x.ClassId)
                            .FirstOrDefaultAsync();

                        if (classId != Guid.Empty && !teacherClasses.Contains(classId))
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            return;
                        }
                    }
                }
            }

            if (path.StartsWith("/api/exam"))
            {
                var examIds = ExtractIds(context, "id", "examId", "examIds");
                if (examIds.Count > 0)
                {
                    foreach (var examId in examIds)
                    {
                        var classId = await db.ClassExams
                            .Where(x => x.Id == examId)
                            .Select(x => x.ClassId)
                            .FirstOrDefaultAsync();

                        if (classId != Guid.Empty && !teacherClasses.Contains(classId))
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            return;
                        }
                    }
                }
            }

            if (path.StartsWith("/api/get/subject"))
            {
                var subjectIds = ExtractIds(context, "subjectId", "subjectIdList");
                if (subjectIds.Count > 0)
                {
                    foreach (var subjectId in subjectIds)
                    {
                        var teacherId = await db.Subjects
                            .Where(x => x.Id == subjectId)
                            .Select(x => x.TeacherId)
                            .FirstOrDefaultAsync();

                        if (teacherId != internalUserId)
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            return;
                        }
                    }
                }
            }

            if (path.StartsWith("/api/students"))
            {
                var studentIds = ExtractIds(context, "id", "studentIdList");
                if (studentIds.Count > 0)
                {
                    foreach (var studentId in studentIds)
                    {
                        var classId = await db.ClassStudents
                            .Where(x => x.StudentId == studentId)
                            .Select(x => x.ClassId)
                            .FirstOrDefaultAsync();

                        if (classId != Guid.Empty && !teacherClasses.Contains(classId))
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            return;
                        }
                    }
                }
            }
        }

        await next(context);
    }

    private List<Guid> ExtractIds(HttpContext context, params string[] keys)
    {
        var result = new List<Guid>();

        foreach (var key in keys)
        {
            if (context.Request.RouteValues.TryGetValue(key, out var routeVal))
            {
                if (Guid.TryParse(routeVal.ToString(), out var parsed))
                    result.Add(parsed);
            }

            if (context.Request.Query.TryGetValue(key, out var queryVals))
            {
                foreach (var val in queryVals)
                {
                    if (Guid.TryParse(val, out var parsed))
                        result.Add(parsed);
                }
            }
        }

        return result;
    }
}
