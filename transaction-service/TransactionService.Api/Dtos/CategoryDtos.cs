namespace TransactionService.Api.Dtos;

public class CreateCategoryRequest
{
    public int UserId { get; set; }

    public string Name { get; set; } = string.Empty;
}

public class UpdateCategoryRequest
{
    public int UserId { get; set; }

    public string Name { get; set; } = string.Empty;
}

public class CategoryResponse
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = string.Empty;
}
