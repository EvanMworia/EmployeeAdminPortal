namespace EmployeePortal.Models.DTO_s
{
    public class ResponseDTO
    {
        public string? Message { get; set; }
        public object Result { get; set; } = default!;
        //public bool IsSuccess { get; set; } = false;
    }
}
