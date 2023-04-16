namespace HeadHunterManager.Core.HeadHunter.Exceptions;

public class VacancyRespondLimitedException : Exception
{
    public VacancyRespondLimitedException() 
    : base("Невозможно совершить отклик по причине ограниченного количества запросов.")
    { }
}