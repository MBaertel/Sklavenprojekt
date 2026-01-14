using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Input;
using SchoolManagementFrontend.ViewModels.MainPages.ExamsPage;
using System.Windows.Input;

namespace SchoolManagementFrontend;

public partial class ExamSlideControl : UserControl
{
    public static readonly StyledProperty<ICommand?> CloseCommandProperty =
        AvaloniaProperty.Register<ExamSlideControl, ICommand?>(nameof(CloseCommand));

    public static readonly StyledProperty<IndividualExamViewModel?> ExamProperty =
        AvaloniaProperty.Register<ExamSlideControl, IndividualExamViewModel?>(nameof(Exam));

    public IndividualExamViewModel? Exam
    {
        get => GetValue(ExamProperty);
        set => SetValue(ExamProperty, value);
    }

    public ICommand? CloseCommand
    {
        get => GetValue(CloseCommandProperty);
        set => SetValue(CloseCommandProperty, value);
    }

    public ExamSlideControl()
    {
        InitializeComponent();
    }
}