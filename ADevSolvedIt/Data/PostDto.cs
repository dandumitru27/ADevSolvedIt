namespace ADevSolvedIt.Data
{
    public class PostDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Slug { get; set; }

        public string Tags { get; set; }

        public DateTime LastEditDate { get; set; }

        public string UserEmailHash { get; set; }

        public string UserName { get; set; }

        public int ThanksCount { get; set; }

        public int Views { get; set; }

        public int CommentCount { get; set; }
    }
}
