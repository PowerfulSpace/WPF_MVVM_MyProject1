using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xaml;

namespace WpfApp4.ViewModels.Base
{
    internal abstract class ViewModel : MarkupExtension, INotifyPropertyChanged
    {



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;

            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }


        #region Расширяемая разметка для нашей модели. + Установили возможность достучаться до xml разметки, что в архитектуре не желательно
       

        public override object ProvideValue(IServiceProvider serviceProvider)  // теперь у нас view модель стала расширением разметки
        {

            var value_target_service = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget; // целевой обьект к которому обращаеся представление
            var value_object_service = serviceProvider.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider; // корень(наше окно)

            OnInitialized(value_target_service?.TargetObject, value_target_service?.TargetProperty, value_object_service?.RootObject);


            return this;
        }

        private WeakReference _TargetRef;
        private WeakReference _RootRef;

        public object TargetObject => _TargetRef.Target;
        public object RootObject => _RootRef.Target;

        protected virtual void OnInitialized(object Target, object Property, object Root)
        {
            _TargetRef = new WeakReference(Target);
            _RootRef = new WeakReference(Root);
        }

        #endregion

    }
}
