using SchoolManagementFrontend.ViewModels.Overlays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.Services.Interface
{
    public interface IOverlayService
    {
        void ShowOverlay(IOverlay overlay);
        void CloseOverlay();
    }
}
