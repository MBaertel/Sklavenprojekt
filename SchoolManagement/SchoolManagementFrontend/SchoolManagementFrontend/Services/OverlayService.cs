using SchoolManagementFrontend.Services.Interface;
using SchoolManagementFrontend.ViewModels;
using SchoolManagementFrontend.ViewModels.Overlays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.Services
{
    public class OverlayService : IOverlayService
    {
        private readonly MainWindowViewModel _viewModel;

        public OverlayService(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void CloseOverlay()
        {
            _viewModel.CloseOverlay();
        }

        public void ShowOverlay(IOverlay overlay)
        {
            _viewModel.ShowOverlay(overlay);
        }
    }
}
