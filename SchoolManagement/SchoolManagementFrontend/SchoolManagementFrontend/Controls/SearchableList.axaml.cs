using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using SchoolManagementFrontend.Utils;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace SchoolManagementFrontend;

public partial class SearchableList : UserControl
{
    public static readonly StyledProperty<IEnumerable> ItemsSourceProperty =
        AvaloniaProperty.Register<SearchableList,IEnumerable>(nameof(ItemsSource));

    public static readonly StyledProperty<string> SearchTextProperty =
        AvaloniaProperty.Register<SearchableList,string>(nameof(SearchText));

    public static readonly StyledProperty<IDataTemplate?> ItemTemplateProperty =
            AvaloniaProperty.Register<SearchableList, IDataTemplate?>(nameof(ItemTemplate));


    public ObservableCollection<object> FilteredItems = new ObservableCollection<object>();
    public Func<object, string, bool>? FilterFunc { get; set; }

    public IEnumerable ItemsSource
    {
        get => (IEnumerable)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public string SearchText
    {
        get => (string)GetValue(SearchTextProperty);
        set => SetValue(SearchTextProperty, value);
    }

    public IDataTemplate ItemTemplate
    {
        get => (IDataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    public SearchableList()
    {
        InitializeComponent();
        if(this.DataContext != this) this.DataContext = this;

        SearchTextProperty.Changed.AddClassHandler<SearchableList>((s, e) => s.ApplyFilter());
        ItemsSourceProperty.Changed.AddClassHandler<SearchableList>((s, e) => s.ApplyFilter());
    }

    private void ApplyFilter()
    {
        if(ItemsSource == null)
        {
            FilteredItems.Clear();
            return;
        }
        if(SearchText == null || FilterFunc == null)
        {
            FilteredItems.Replace(ItemsSource.Cast<object>());
            return;
        }
        var filtered = ItemsSource.Cast<object>()
            .Where(x => FilterFunc(x,SearchText));
        FilteredItems.Replace(filtered);
    }
}