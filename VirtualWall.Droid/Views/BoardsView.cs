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

        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.BoardsView);            

        }
    }
}