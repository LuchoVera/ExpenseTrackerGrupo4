namespace ExpenseTrackerGrupo4.src.Domain.Entities;

public class GoalNotificationLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public Guid GoalId { get; set; }
        public bool Notified50 { get; set; }
        public bool Notified100 { get; set; }
    }
