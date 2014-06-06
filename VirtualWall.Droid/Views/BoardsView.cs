using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Droid.Views;
using VirtualWall.Core.ViewModels;

namespace VirtualWall.Droid.Views 
{
    [Activity(Label = "Boards")]
    public class BoardsView : MvxActivity
    {
        private BindableProgress _bindableProgress;

        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.BoardsView);

            _bindableProgress = new BindableProgress(this);

            //var set = this.CreateBindingSet<BoardsView, BoardsViewModel>();
            //set.Bind(_bindableProgress).For(p => p.Visible).To(vm => vm.IsBusy);
            //set.Apply();
        }
    }
}