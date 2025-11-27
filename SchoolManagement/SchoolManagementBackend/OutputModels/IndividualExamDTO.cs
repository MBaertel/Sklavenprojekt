namespace SchoolManagementBackend.OutputModels
{
    public class IndividualExamDTO
    {
        public Guid Id { get; set; }

        public Guid BaseExamId { get; set; }

        public int Score { get; set; }

        public int Grade { get; set; }

        public static IndividualExamDTO FromEf(IndividualExamDTO dto)
        {
            return new IndividualExamDTO()
            {
                Id = dto.Id,
                BaseExamId = dto.BaseExamId,
                Score = dto.Score,
                Grade = dto.Grade
            };
        }
    }
}
