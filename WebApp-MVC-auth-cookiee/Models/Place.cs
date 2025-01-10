namespace WebApp_MVC_auth_cookiee.Models
{
    public class Place
    {
        public int Id { get; set; } // Идентификатор места
        public string Name { get; set; } // Название места
        public string? Description { get; set; } // Описание места
        public int UserId { get; set; } // Идентификатор пользователя, который создал запись
        public string? Photo { get; set; } // Путь к фотографии
        public double Latitude { get; set; } // Широта
        public double Longitude { get; set; } // Долгота
        public List<Rating>? Ratings { get; set; } // Список оценок
        public List<Comment>? Comments { get; set; } // Список комментариев

        // Свойство для подсчета средней оценки
        public double AverageRating => Ratings?.Count > 0 ? Ratings.Average(r => r.Value) : 0;

    }
}
