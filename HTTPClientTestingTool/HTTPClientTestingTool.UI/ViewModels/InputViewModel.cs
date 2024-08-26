using HTTPClientTestingTool.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPClientTestingTool.UI.ViewModels;

class InputViewModel : ViewModelBase
{
    public InputViewModel()
    {
        SelectedMethod = EHttpMethods.Get;
    }

    public string URL { get; set; } = string.Empty;

    public string Headers { get; set; } = string.Empty;

    public string RequestBody { get; set; } = string.Empty;

    private EHttpMethods _selectedMethod;

    public EHttpMethods SelectedMethod
    {
        get => _selectedMethod;
        set
        {
            _selectedMethod = value;
            OnPropertyChanged(nameof(SelectedMethod));
        }
    }

    public IEnumerable<EHttpMethods> Methods
    {
        get
        {
            return Enum.GetValues(typeof(EHttpMethods))
                .Cast<EHttpMethods>();
        }
    }
}
