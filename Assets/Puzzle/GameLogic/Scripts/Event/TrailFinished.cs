namespace Puzzle.GameLogic.Scripts.Event{
	public class TrailFinished{
		public string PuzzleName{ get; }
		public bool IsSuccess{ get; }

		public TrailFinished(string puzzleName , bool isSuccess){
			PuzzleName = puzzleName;
			IsSuccess = isSuccess;
		}
	}
}