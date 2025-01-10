namespace WebApp_MVC_auth_cookiee.Models
{
    public class Comment
    {
        public int Id { get; set; } // Идентификатор комментария
        public int UserId { get; set; } // Идентификатор пользователя, который оставил комментарий
        public int PlaceId { get; set; } // Идентификатор места
        public string Text { get; set; } // Текст комментария

        // Навигационное свойство
        public Place Place { get; set; }
    }
}
