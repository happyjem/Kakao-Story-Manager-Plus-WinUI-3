using KSMP.Controls;
using KSMP.Utils;
using Microsoft.UI.Xaml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static KSMP.ApiHandler.DataType.CommentData;

namespace KSMP;

public sealed partial class TimelineWindow : Window
{
	private static List<TimelineWindow> s_instances = new();

	public TimelineControl Control;
	public string PostId { get; private set; }

	private TimelineWindow(PostData postData)
	{
		s_instances.Add(this);
		PostId = postData.id;

		InitializeComponent();

		AppWindow.SetIcon(Path.Combine(App.BinaryDirectory, "icon.ico"));
		Control = new TimelineControl(this, postData, false, true);
		FrMain.Content = Control;
	}

	public static bool HasInstanceContainsId(string id) => s_instances.Any(x => x.PostId == id);
	public static TimelineWindow FindTimelineWindowByPostId(string id) => s_instances.FirstOrDefault(x => x.PostId == id);
	public static TimelineWindow GetTimelineWindow(PostData postData) => s_instances.FirstOrDefault(x => x.PostId == postData.id) ?? new TimelineWindow(postData);

	private void OnWindowClosed(object sender, WindowEventArgs args) => s_instances.Remove(this);

	private async void OnPreviewKeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
	{
		var isControlDown = Common.IsModifierDown();

		if (e.Key == Windows.System.VirtualKey.Escape)
			Close();
		if ((isControlDown && e.Key == Windows.System.VirtualKey.R) || e.Key == Windows.System.VirtualKey.F5)
			await Control.RefreshContent(true);
		else if (isControlDown && e.Key == Windows.System.VirtualKey.S)
			await Control.SharePost();
		else if (isControlDown && e.Key == Windows.System.VirtualKey.E)
			await Control.EditPost();
		else if (isControlDown && e.Key == Windows.System.VirtualKey.D)
			await Control.DeletePost();
		else if (isControlDown && e.Key == Windows.System.VirtualKey.W)
			Close();
	}
}
