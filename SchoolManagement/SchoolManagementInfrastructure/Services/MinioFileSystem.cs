using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementInfrastructure.Services
{
    public class MinioFileSystem : IFileSystem
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public MinioFileSystem()
        {
            _bucketName = System.Environment.GetEnvironmentVariable("MINIO_BUCKET_NAME");

            var accessKey = System.Environment.GetEnvironmentVariable("MINIO_ACCESS_KEY");
            var secretKey = System.Environment.GetEnvironmentVariable("MINIO_SECRET_KEY");

            var config = new AmazonS3Config
            {
                ServiceURL = System.Environment.GetEnvironmentVariable("MINIO_ENDPOINT"),
                ForcePathStyle = true,
            };

            _s3Client = new AmazonS3Client(accessKey,secretKey,config);
        }

        public async Task<string> SaveFile(string filename, byte[] content)
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = filename,
                InputStream = new MemoryStream(content),
                ContentType = "image/png",
                CannedACL = S3CannedACL.PublicRead
            };
            await _s3Client.PutObjectAsync(putRequest);

            return $"{_s3Client.Config.ServiceURL}/{_bucketName}/{filename}";
        }

        public async Task<byte[]> GetFile(string filename)
        {
            var response = await _s3Client.GetObjectAsync(_bucketName, filename);

            using var ms = new MemoryStream();
            await response.ResponseStream.CopyToAsync(ms);
            return ms.ToArray();
        }

        public async Task DeleteFile(string filename)
        {
            await _s3Client.DeleteObjectAsync(_bucketName, filename);
        }
    }
}
