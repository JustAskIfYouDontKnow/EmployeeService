namespace Application.DTO;

public class DepartmentDto : AbstractDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public int? Floor { get; set; }
}