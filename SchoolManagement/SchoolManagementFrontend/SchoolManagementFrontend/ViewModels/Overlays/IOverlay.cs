using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SchoolManagementFrontend.ViewModels.Overlays
{
    public interface IOverlay
    {
        event Action RequestClose;
        string Title { get; }
        void OnClosed() { }
        void Close();
    }
}
