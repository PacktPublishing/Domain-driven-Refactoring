namespace BrewUp.Shared.Entities
{
    public class LastEventPosition : EntityBase
	{
		public ulong CommitPosition { get; set; }
		public ulong PreparePosition { get; set; }
	}
}
