namespace MovieRecs.Models
{
    public class ResponseAPI
    {
        public List<MovieResultModel>? Results { get; set; }
    }

    public class MovieResultModel
    {
        public string? imdb_id { get; set; }
        public string? title { get; set; }
    }
}
