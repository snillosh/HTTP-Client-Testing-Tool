using HTTPClientTestingTool.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPClientTestingTool.UI.ViewModels
{
    class InputViewModel : ViewModelBase
    {
        public string URL { get; set; } = string.Empty;

        public EHttpMethods HttpMethods { get; set; } = EHttpMethods.Get;

        public string Headers { get; set; } = string.Empty;

        public string RequestBody { get; set; } = string.Empty;
    }
}
