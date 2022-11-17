namespace API.Utilities;

    public class UserParams{
        public int MinAge {get; set;}
        public int MaxAge {get; set;}
        public string Gender {get; set;} = null!;
    public string? CurrentUsername {get; set;}
    public string OrderBy {get; set;} = "created";
}
