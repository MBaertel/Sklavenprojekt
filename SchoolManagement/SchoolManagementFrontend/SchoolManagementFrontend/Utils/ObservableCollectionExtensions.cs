using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.Utils
{
    internal static class ObservableCollectionExtensions
    {
        public static ObservableCollection<T> Replace<T>(this ObservableCollection<T> collection,IEnumerable<T> replaceWith)
        {
            collection.Clear();
            foreach (var item in replaceWith)
            {
                collection.Add(item);
            }
            return collection;
        }
    }
}
