using SchoolManagementFrontend.Services;
using SchoolManagementFrontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.Pages
{
    public interface IPageDescriptor
    {
        public string Title { get; }
        public string IconPath { get; }
        public Type ViewModelType { get; }
        public List<string> RequiredRoles { get; }
    }

    public class PageDescriptor<T> : IPageDescriptor where T : ViewModelBase
    {
        public string Title { get; }
        public string IconPath { get; }
        public Type ViewModelType => typeof(T);
        public List<string> RequiredRoles { get; }

        public PageDescriptor(string title, string iconPath,List<string> requiredScopes = null)
        {
            Title = title;
            IconPath = iconPath;
            RequiredRoles = requiredScopes ?? new List<string>();
        }

        public override int GetHashCode()
        {
            return Title?.GetHashCode() ?? 0;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is null) return false;
            if (obj is PageDescriptor<T> other)
                return string.Equals(Title, other.Title, StringComparison.Ordinal);
            return false;
        }
    }
}
