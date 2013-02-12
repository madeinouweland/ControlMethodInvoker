using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ControlMethodInvokerTest {

    public sealed class NameList : Control {

        public object Names {
            get { return (object)GetValue(NamesProperty); }
            set { SetValue(NamesProperty, value); }
        }

        public static readonly DependencyProperty NamesProperty =
            DependencyProperty.Register("Names",
                                        typeof(object),
                                        typeof(NameList),
                                        new PropertyMetadata(null));

        public ICommand EditCommand {
            get { return (ICommand)GetValue(EditCommandProperty); }
            set { SetValue(EditCommandProperty, value); }
        }

        public static readonly DependencyProperty EditCommandProperty =
            DependencyProperty.Register("EditCommand",
                                        typeof(ICommand),
                                        typeof(NameList),
                                        new PropertyMetadata(null));

        public NameList() {
            this.DefaultStyleKey = typeof(NameList);
        }

        public void Edit(object sender) {
            EditCommand.Execute(((Button)sender).DataContext);
        }
    }
}
