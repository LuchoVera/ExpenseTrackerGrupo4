namespace ExpenseTrackerGrupo4.src.Presentation.DTOs
{
    public class CreateUpdateGoalDto
    {
        public decimal GoalAmount { get; set; }
        public DateTime Deadline { get; set; }
        public decimal CurrentAmount { get; set; }
    }
}
