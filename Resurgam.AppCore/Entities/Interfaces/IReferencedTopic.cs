namespace Resurgam.AppCore.Entities.Interfaces
{
    public interface IReferencedTopic : IBaseEntity
    {
        int ProjectId { get; set; }
        int ParentTopicId { get; set; }
        Topic ParentTopic { get; set; }
        int ChildTopicId { get; set; }
        Topic ChildTopic { get; set; }
    }
}
