using System;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ControlMethodInvokerTest {
    public class ControlMethodInvoker {

        #region MethodPath Attached Property

        /// <summary> 
        /// Identifies the MethodPath attachted property. This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty MethodPathProperty =
            DependencyProperty.RegisterAttached("MethodPath",
                                                typeof(string),
                                                typeof(ControlMethodInvoker),
                                                new PropertyMetadata(null, OnMethodPathChanged));

        /// <summary>
        /// MethodPath changed handler. 
        /// </summary>
        /// <param name="d">FrameworkElement that changed its MethodPath attached property.</param>
        /// <param name="e">DependencyPropertyChangedEventArgs with the new and old value.</param> 
        private static void OnMethodPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var source = d as Button;
            if (source != null) {
                var value = (string)e.NewValue;
                source.Click += (s, a) => {
                    var path = value.Split(':');
                    Type t = Type.GetType(path[0]);
                    object tc = null;
                    try {
                        tc = GetVisualParent(source, t);
                    } catch {
                        throw new Exception("Cannot find method '" + path[1] + "' in templated control '" + path[0] + "'. Please check your MethodPath (namespace.controltype:methodname) property in the XAML");
                    }
                    MethodInfo methodInfo = tc.GetType().GetRuntimeMethod(path[1], new Type[1] { typeof(object) });
                    ParameterInfo[] parameters = methodInfo.GetParameters();
                    methodInfo.Invoke(tc, new object[] { source });
                };
            }
        }

        private static object GetVisualParent(DependencyObject elem, Type type) {
            if (elem == null) {
                throw new ArgumentNullException("elem");
            } else if (elem.GetType() == type) {
                return (object)elem;
            } else {
                return GetVisualParent(VisualTreeHelper.GetParent(elem), type);
            }
        }

        /// <summary>
        /// Gets the value of the MethodPath attached property from the specified FrameworkElement.
        /// </summary>
        public static string GetMethodPath(DependencyObject obj) {
            return (string)obj.GetValue(MethodPathProperty);
        }


        /// <summary>
        /// Sets the value of the MethodPath attached property to the specified FrameworkElement.
        /// </summary>
        /// <param name="obj">The object on which to set the MethodPath attached property.</param>
        /// <param name="value">The property value to set.</param>
        public static void SetMethodPath(DependencyObject obj, string value) {
            obj.SetValue(MethodPathProperty, value);
        }

        #endregion MethodPath Attached Property


    }
}
