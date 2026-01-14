using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementInfrastructure.Services
{
    public interface IFileSystem
    {
        Task<string> SaveFile(string filename, byte[] content);
        Task<byte[]> GetFile(string filename);
        Task DeleteFile(string filename);
    }
}
