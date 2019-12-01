using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Service
{
    public interface IBusyIndicatorService
    {
        void SetBusy(string text = "");
        void ResetBusy();
        void Busy(bool isBusy, string text = "");
        void SetSnackbar(string text);
    }
}
