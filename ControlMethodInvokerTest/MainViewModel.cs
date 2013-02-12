using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ControlMethodInvokerTest {
   public class MainViewModel : ObservableObject{
       public ObservableCollection<string> Names { get; set; }
       public ICommand EditCommand { get; set; }
       public MainViewModel() {
           EditCommand = new DelegateCommand<object>(x => {
               SelectedName ="Selected: "+ x;
           });
           Names = new ObservableCollection<string>();
           Names.Add("Vera");
           Names.Add("Chuck");
           Names.Add("Dave");
       }

       private string selectedName;
       public string SelectedName {
           get { return selectedName; }
           set {
               if (selectedName != value) {
                   selectedName = value;
                   OnPropertyChanged(() => this.SelectedName);
               }
           }
       }

    }
}
