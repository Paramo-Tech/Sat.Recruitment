using Sat.Recruitment.UseCasesAbstractions;

namespace Sat.Recruitment.Presenter
{
    public interface IPresenter<FormatDataType>
    {
        public FormatDataType Content { get; }
    }
}