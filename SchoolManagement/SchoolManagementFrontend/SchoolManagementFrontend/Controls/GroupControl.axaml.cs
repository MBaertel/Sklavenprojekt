using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Templates;
using System.Collections;

namespace SchoolManagementFrontend;

public partial class GroupControl : UserControl
{
    public static readonly StyledProperty<string> HeaderTextProperty =
            AvaloniaProperty.Register<GroupControl, string>(nameof(HeaderText));

    public static readonly StyledProperty<IEnumerable> ItemsSourceProperty =
        AvaloniaProperty.Register<GroupControl, IEnumerable>(nameof(ItemsSource));

    public static readonly StyledProperty<bool> IsExpandedProperty =
        AvaloniaProperty.Register<GroupControl, bool>(nameof(IsExpanded));

    public static readonly StyledProperty<DataTemplate> DataTemplateProperty =
        AvaloniaProperty.Register<GroupControl,DataTemplate>(nameof(DataTemplate));

    public static readonly StyledProperty<object> SelectedItemProperty =
        AvaloniaProperty.Register<GroupControl, object>(nameof(SelectedItem));

    public string HeaderText
    {
        get => GetValue(HeaderTextProperty);
        set => SetValue(HeaderTextProperty, value);
    }

    public IEnumerable ItemsSource
    {
        get => GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public bool IsExpanded
    {
        get => GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    public DataTemplate DataTemplate
    {
        get => (DataTemplate)GetValue(DataTemplateProperty);
        set => SetValue(DataTemplateProperty, value);
    }

    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public GroupControl()
    {
        InitializeComponent();
    }
}