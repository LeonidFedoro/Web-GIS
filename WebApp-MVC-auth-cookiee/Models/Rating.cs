namespace WebApp_MVC_auth_cookiee.Models
{
    public class Rating
    {
        public int Id { get; set; } // Идентификатор оценки
        public int UserId { get; set; } // Идентификатор пользователя, который оставил оценку
        public int PlaceId { get; set; } // Идентификатор места
        public int Value { get; set; } // Значение оценки (от 1 до 5)

        // Навигационное свойство
        public Place Place { get; set; }
    }
}
