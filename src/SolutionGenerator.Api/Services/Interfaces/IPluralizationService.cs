namespace SolutionGenerator.Services
{
    public interface IPluralizationService
    {
        bool IsPlural(string text);

        bool IsSingular(string text);

        string ToPlural(string text);

        string ToSingular(string text);
    }
}
