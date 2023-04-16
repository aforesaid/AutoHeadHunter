namespace HeadHunterManager.Core.HeadHunter.Exceptions;

public class ResumeAlreadyTouchedException : Exception
{
    public ResumeAlreadyTouchedException() 
        : base("Невозможно поднять резюме, повторите через 4 часа.")
    { }
}