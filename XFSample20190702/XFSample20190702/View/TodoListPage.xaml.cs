using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFSample20190702.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TodoListPage : DContentPage
	{
		public TodoListPage ()
		{
			InitializeComponent ();
		}
	}
}