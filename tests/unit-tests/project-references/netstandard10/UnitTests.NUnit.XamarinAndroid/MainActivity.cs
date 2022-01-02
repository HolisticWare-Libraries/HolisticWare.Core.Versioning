using System.Reflection;

using Android.App;
using Android.OS;

namespace UnitTests.NUnit.XamarinAndroid
{
    [Activity(Label = "UnitTests.NUnit.XamarinAndroid", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            // tests can be inside the main assembly
            //AddTest(Assembly.GetExecutingAssembly());
            // or in any reference assemblies
            // AddTest (typeof (Your.Library.TestClass).Assembly);

            // Once you called base.OnCreate(), you cannot add more assemblies.
            base.OnCreate(bundle);
        }
    }
}
