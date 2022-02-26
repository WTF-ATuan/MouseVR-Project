using Environment.Scripts;
using Sirenix.OdinInspector;

namespace Environment.Editor{
	public class EditEnvironmentData{
		[ShowInInspector] [InlineEditor(Expanded = true)]
		public EnvironmentData data;
	}
}